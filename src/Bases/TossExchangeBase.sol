// SPDX-License-Identifier: MIT
pragma solidity 0.8.23;

import { AccessControlUpgradeable } from "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import { IERC20Permit } from "@openzeppelin/contracts/token/ERC20/extensions/IERC20Permit.sol";
import { SafeERC20, IERC20 } from "@openzeppelin/contracts/token/ERC20/Utils/SafeERC20.sol";
import { ReentrancyGuardUpgradeable } from "@openzeppelin/contracts-upgradeable/utils/ReentrancyGuardUpgradeable.sol";
import { TossUUPSUpgradeable } from "./TossUUPSUpgradeable.sol";
import { TossErc20Base } from "./TossErc20Base.sol";
import { ITossExchange } from "../Interfaces/ITossExchange.sol";
import { TossWhitelistClient } from "./TossWhitelistClient.sol";
import "../Interfaces/TossErrors.sol";

abstract contract TossExchangeBase is ITossExchange, TossWhitelistClient, AccessControlUpgradeable, ReentrancyGuardUpgradeable, TossUUPSUpgradeable {
    /// @custom:storage-location erc7201:tossplatform.storage.TossExchangeBase
    struct TossExchangeBaseStorage {
        uint128 externalMinAmount;
        uint128 internalMinAmount;
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

    event ConvertedToInternal(address indexed account, uint256 indexed amount);
    event ConvertedToExternal(address indexed account, uint256 indexed amount);

    error TossExchangeAmounIsLessThanMin(string parameter, uint256 amount, uint256 min);
    error TossExchangeExternalAndInternalErc20AreEqual();
    error TossExchangeExternalAndInternalErc20HaveDifferentDecimalAmount();

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossExchangeBase_init(IERC20 externalErc20_, uint128 externalMinAmount_, TossErc20Base internalErc20_, uint128 internalMinAmount_) public onlyInitializing {
        __AccessControl_init();
        __ReentrancyGuard_init();
        __TossUUPSUpgradeable_init();
        __TossExchangeBase_init_unchained(externalErc20_, externalMinAmount_, internalErc20_, internalMinAmount_);
    }

    function __TossExchangeBase_init_unchained(
        IERC20 externalErc20_,
        uint128 externalMinAmount_,
        TossErc20Base internalErc20_,
        uint128 internalMinAmount_
    ) public onlyInitializing {
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

        if (externalMinAmount_ == 0) {
            revert TossValueIsZero("external min");
        }
        if (internalMinAmount_ == 0) {
            revert TossValueIsZero("internal min");
        }
        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);

        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        $.externalErc20 = externalErc20_;
        $.internalErc20 = internalErc20_;

        $.externalMinAmount = externalMinAmount_;
        $.internalMinAmount = internalMinAmount_;
    }

    function _authorizeUpgrade(address newImplementation) internal override onlyRole(UPGRADER_ROLE) { }

    function setWhitelist(address newAddress) external override onlyRole(DEFAULT_ADMIN_ROLE) {
        _setWhitelist(newAddress);
    }

    function getExternalMinAmount() external view returns (uint128 minAmount) {
        return _getTossExchangeBaseStorage().externalMinAmount;
    }

    function setExternalMinAmount(uint128 value) external virtual onlyRole(DEFAULT_ADMIN_ROLE) {
        if (value == 0) {
            revert TossValueIsZero("external min");
        }
        _getTossExchangeBaseStorage().externalMinAmount = value;
    }

    function getInternalMinAmount() external view returns (uint128 minAmount) {
        return _getTossExchangeBaseStorage().internalMinAmount;
    }

    function setInternalMinAmount(uint128 value) external virtual onlyRole(DEFAULT_ADMIN_ROLE) {
        if (value == 0) {
            revert TossValueIsZero("internal min");
        }
        _getTossExchangeBaseStorage().internalMinAmount = value;
    }

    function getExternalErc20() external view returns (IERC20 erc20) {
        return _getTossExchangeBaseStorage().externalErc20;
    }

    function getInternalErc20() external view returns (TossErc20Base erc20) {
        return _getTossExchangeBaseStorage().internalErc20;
    }

    function convertToInternal(uint128 amount) external virtual nonReentrant {
        _convertToInternal(amount);
    }

    function convertToInternalWithPermit(uint128 externalAmount, uint256 amount, uint256 deadline, uint8 v, bytes32 r, bytes32 s) public virtual nonReentrant {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        IERC20Permit(address($.externalErc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        _convertToInternal(externalAmount);
    }

    function _convertToInternal(uint128 amount) internal virtual isInWhitelist(msg.sender) {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        if (amount < $.externalMinAmount) {
            revert TossExchangeAmounIsLessThanMin("external", amount, $.externalMinAmount);
        }

        emit ConvertedToInternal(msg.sender, amount);

        $.externalErc20.safeTransferFrom(msg.sender, address(this), amount);
        $.internalErc20.mint(msg.sender, amount);

        assert(_validState($));
    }

    function convertToExternal(uint128 amount) external virtual nonReentrant {
        _convertToExternal(amount);
    }

    function convertToExternalWithPermit(uint128 internalAmount, uint256 amount, uint256 deadline, uint8 v, bytes32 r, bytes32 s) public virtual nonReentrant {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        IERC20Permit(address($.internalErc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        _convertToExternal(internalAmount);
    }

    function _convertToExternal(uint128 amount) internal virtual isInWhitelist(msg.sender) {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        if (amount < $.internalMinAmount) {
            revert TossExchangeAmounIsLessThanMin("internal", amount, $.internalMinAmount);
        }

        emit ConvertedToExternal(msg.sender, amount);

        $.internalErc20.burnFrom(msg.sender, amount);
        $.externalErc20.safeTransfer(msg.sender, amount);

        assert(_validState($));
    }

    function validState() external view returns (bool valid) {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        return _validState($);
    }

    function _validState(TossExchangeBaseStorage memory $) private view returns (bool valid) {
        return $.externalErc20.balanceOf(address(this)) >= $.internalErc20.totalSupply();
    }
}
