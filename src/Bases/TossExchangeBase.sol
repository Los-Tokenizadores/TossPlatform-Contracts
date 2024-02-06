// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { AccessControlUpgradeable } from "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import { PausableUpgradeable } from "@openzeppelin/contracts-upgradeable/utils/PausableUpgradeable.sol";
import { IERC20Permit } from "@openzeppelin/contracts/token/ERC20/extensions/IERC20Permit.sol";
import { SafeERC20, IERC20 } from "@openzeppelin/contracts/token/ERC20/Utils/SafeERC20.sol";
import { ReentrancyGuardUpgradeable } from "@openzeppelin/contracts-upgradeable/utils/ReentrancyGuardUpgradeable.sol";
import { TossUUPSUpgradeable } from "./TossUUPSUpgradeable.sol";
import { TossErc20Base } from "./TossErc20Base.sol";
import { TossWhitelistClient } from "./TossWhitelistClient.sol";
import "../Interfaces/TossErrors.sol";

abstract contract TossExchangeBase is TossWhitelistClient, PausableUpgradeable, AccessControlUpgradeable, ReentrancyGuardUpgradeable, TossUUPSUpgradeable {
    /// @custom:storage-location erc7201:tossplatform.storage.TossExchangeBase
    struct TossExchangeBaseStorage {
        uint128 depositMinAmount;
        uint128 withdrawMinAmount;
        IERC20 externalErc20;
        TossErc20Base internalErc20;
        uint64 rate;
    }

    // keccak256(abi.encode(uint256(keccak256("tossplatform.storage.TossExchangeBase")) - 1)) & ~bytes32(uint256(0xff))
    bytes32 private constant TossExchangeBaseStorageLocation = 0x776aa79b629bec5fcbeeac6d13e962169e431b12b2282b55578c981ec7fe3b00;

    function _getTossExchangeBaseStorage() private pure returns (TossExchangeBaseStorage storage $) {
        assembly {
            $.slot := TossExchangeBaseStorageLocation
        }
    }

    using SafeERC20 for IERC20;

    bytes32 public constant UPGRADER_ROLE = keccak256("UPGRADER_ROLE");
    bytes32 public constant PAUSER_ROLE = keccak256("PAUSER_ROLE");

    event Deposited(address indexed account, uint256 indexed amount);
    event Withdrawn(address indexed account, uint256 indexed amount);

    error TossExchangeAmounIsLessThanMin(string parameter, uint256 amount, uint256 min);
    error TossExchangeExternalAndInternalErc20AreEqual();
    error TossExchangeExternalAndInternalErc20HaveDifferentDecimalAmount();
    error TossExchangeInvalidState(uint256 externalAmount, uint256 internalAmount);

    function __TossExchangeBase_init(IERC20 externalErc20_, uint128 depositMinAmount_, TossErc20Base internalErc20_, uint128 withdrawMinAmount_) internal onlyInitializing {
        __Pausable_init();
        __AccessControl_init();
        __ReentrancyGuard_init();
        __TossUUPSUpgradeable_init();
        __TossExchangeBase_init_unchained(externalErc20_, depositMinAmount_, internalErc20_, withdrawMinAmount_);
    }

    function __TossExchangeBase_init_unchained(
        IERC20 externalErc20_,
        uint128 depositMinAmount_,
        TossErc20Base internalErc20_,
        uint128 withdrawMinAmount_
    ) internal onlyInitializing {
        if (address(externalErc20_) == address(0)) {
            revert TossAddressIsZero("external");
        }
        if (address(internalErc20_) == address(0)) {
            revert TossAddressIsZero("internal");
        }
        if (address(externalErc20_) == address(internalErc20_)) {
            revert TossExchangeExternalAndInternalErc20AreEqual();
        }
        if (TossErc20Base(address(externalErc20_)).decimals() != internalErc20_.decimals()) {
            revert TossExchangeExternalAndInternalErc20HaveDifferentDecimalAmount();
        }

        if (depositMinAmount_ == 0) {
            revert TossValueIsZero("deposit min");
        }
        if (withdrawMinAmount_ == 0) {
            revert TossValueIsZero("withdraw min");
        }
        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);
        _grantRole(PAUSER_ROLE, msg.sender);

        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        $.externalErc20 = externalErc20_;
        $.internalErc20 = internalErc20_;

        $.depositMinAmount = depositMinAmount_;
        $.withdrawMinAmount = withdrawMinAmount_;
    }

    function _authorizeUpgrade(address newImplementation) internal override onlyRole(UPGRADER_ROLE) { }

    function pause() external onlyRole(PAUSER_ROLE) {
        _pause();
    }

    function unpause() external onlyRole(PAUSER_ROLE) {
        _unpause();
    }

    function setWhitelist(address newAddress) external override onlyRole(DEFAULT_ADMIN_ROLE) {
        _setWhitelist(newAddress);
    }

    function getDepositMinAmount() external view returns (uint128 minAmount) {
        return _getTossExchangeBaseStorage().depositMinAmount;
    }

    function setDepositMinAmount(uint128 value) external virtual onlyRole(DEFAULT_ADMIN_ROLE) {
        if (value == 0) {
            revert TossValueIsZero("deposit min");
        }
        _getTossExchangeBaseStorage().depositMinAmount = value;
    }

    function getWithdrawMinAmount() external view returns (uint128 minAmount) {
        return _getTossExchangeBaseStorage().withdrawMinAmount;
    }

    function setWithdrawMinAmount(uint128 value) external virtual onlyRole(DEFAULT_ADMIN_ROLE) {
        if (value == 0) {
            revert TossValueIsZero("withdraw min");
        }
        _getTossExchangeBaseStorage().withdrawMinAmount = value;
    }

    function getExternalErc20() external view returns (IERC20 erc20) {
        return _getTossExchangeBaseStorage().externalErc20;
    }

    function getInternalErc20() external view returns (TossErc20Base erc20) {
        return _getTossExchangeBaseStorage().internalErc20;
    }

    function deposit(uint128 amount) external virtual {
        depositInternal(amount);
    }

    function depositWithPermit(uint128 externalAmount, uint256 amount, uint256 deadline, uint8 v, bytes32 r, bytes32 s) public virtual {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        IERC20Permit(address($.externalErc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        depositInternal(externalAmount);
    }

    function depositInternal(uint128 amount) internal virtual nonReentrant whenNotPaused isInWhitelist(msg.sender) {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        if (amount < $.depositMinAmount) {
            revert TossExchangeAmounIsLessThanMin("deposit", amount, $.depositMinAmount);
        }

        emit Deposited(msg.sender, amount);

        $.externalErc20.safeTransferFrom(msg.sender, address(this), amount);
        $.internalErc20.mint(msg.sender, amount);

        validStateInternal($, true);
    }

    function withdraw(uint128 amount) external virtual {
        withdrawInternal(amount);
    }

    function withdrawWithPermit(uint128 internalAmount, uint256 amount, uint256 deadline, uint8 v, bytes32 r, bytes32 s) public virtual {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        IERC20Permit(address($.internalErc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        withdrawInternal(internalAmount);
    }

    function withdrawInternal(uint128 amount) internal virtual nonReentrant whenNotPaused isInWhitelist(msg.sender) {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        if (amount < $.withdrawMinAmount) {
            revert TossExchangeAmounIsLessThanMin("withdraw", amount, $.withdrawMinAmount);
        }

        emit Withdrawn(msg.sender, amount);

        $.internalErc20.burnFrom(msg.sender, amount);
        $.externalErc20.safeTransfer(msg.sender, amount);

        validStateInternal($, true);
    }

    function validState() external view returns (bool valid) {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        return validStateInternal($, false);
    }

    function validStateInternal(TossExchangeBaseStorage memory $, bool revertWhenInvalid) private view returns (bool valid) {
        uint256 balance = $.externalErc20.balanceOf(address(this));
        uint256 totalSupply = $.internalErc20.totalSupply();
        bool state = balance >= totalSupply;
        if (!state && revertWhenInvalid) {
            revert TossExchangeInvalidState(balance, totalSupply);
        }
        return state;
    }
}
