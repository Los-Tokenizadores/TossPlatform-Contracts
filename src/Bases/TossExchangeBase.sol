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

    uint128 private constant RATE_MIDDLE = 10_000;

    event ConvertedToInternal(address indexed account, uint256 indexed externalAmount, uint256 indexed internalAmount);
    event ConvertedToExternal(address indexed account, uint256 indexed externalAmount, uint256 indexed internalAmount);

    error TossExchangeExternalAndInternalErc20AreEqual();
    error TossExchangeRateIsZero();
    error TossExchangeMinAmountIsZero(string parameter);
    error TossExchangeAmounIsLessThanMin(string parameter, uint128 amount, uint128 min);

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossExchangeBase_init(
        IERC20 externalErc20_,
        uint128 externalMinAmount_,
        TossErc20Base internalErc20_,
        uint128 internalMinAmount_,
        uint64 rate_
    ) public onlyInitializing {
        __AccessControl_init();
        __ReentrancyGuard_init();
        __TossUUPSUpgradeable_init();
        __TossExchangeBase_init_unchained(externalErc20_, externalMinAmount_, internalErc20_, internalMinAmount_, rate_);
    }

    function __TossExchangeBase_init_unchained(
        IERC20 externalErc20_,
        uint128 externalMinAmount_,
        TossErc20Base internalErc20_,
        uint128 internalMinAmount_,
        uint64 rate_
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
        if (rate_ == 0) {
            revert TossExchangeRateIsZero();
        }
        if (externalMinAmount_ == 0) {
            revert TossExchangeMinAmountIsZero("external");
        }
        if (internalMinAmount_ == 0) {
            revert TossExchangeMinAmountIsZero("internal");
        }
        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);

        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        $.externalErc20 = externalErc20_;
        $.internalErc20 = internalErc20_;

        $.externalMinAmount = externalMinAmount_;
        $.internalMinAmount = internalMinAmount_;

        $.rate = rate_;
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
            revert TossExchangeMinAmountIsZero("external");
        }
        _getTossExchangeBaseStorage().externalMinAmount = value;
    }

    function getInternalMinAmount() external view returns (uint128 minAmount) {
        return _getTossExchangeBaseStorage().internalMinAmount;
    }

    function setInternalMinAmount(uint128 value) external virtual onlyRole(DEFAULT_ADMIN_ROLE) {
        if (value == 0) {
            revert TossExchangeMinAmountIsZero("internal");
        }
        _getTossExchangeBaseStorage().internalMinAmount = value;
    }

    function getExternalErc20() external view returns (IERC20 erc20) {
        return _getTossExchangeBaseStorage().externalErc20;
    }

    function getInternalErc20() external view returns (TossErc20Base erc20) {
        return _getTossExchangeBaseStorage().internalErc20;
    }

    function getRate() external view returns (uint64 rate) {
        return _getTossExchangeBaseStorage().rate;
    }

    function convertToInternal(uint128 externalAmount) external virtual nonReentrant {
        _convertToInternal(externalAmount);
    }

    function convertToInternalWithPermit(uint128 externalAmount, uint256 amount, uint256 deadline, uint8 v, bytes32 r, bytes32 s) public virtual nonReentrant {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        IERC20Permit(address($.externalErc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        _convertToInternal(externalAmount);
    }

    function convertToExternal(uint128 internalAmount) external virtual nonReentrant {
        _convertToExternal(internalAmount);
    }

    function convertToExternalWithPermit(uint128 internalAmount, uint256 amount, uint256 deadline, uint8 v, bytes32 r, bytes32 s) public virtual nonReentrant {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        IERC20Permit(address($.internalErc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        _convertToExternal(internalAmount);
    }

    function _convertToInternal(uint128 externalAmount) internal virtual isInWhitelist(msg.sender) {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        uint128 internalAmount = externalAmount * $.rate / RATE_MIDDLE;
        if (internalAmount < $.internalMinAmount) {
            revert TossExchangeAmounIsLessThanMin("internal", internalAmount, $.internalMinAmount);
        }

        emit ConvertedToInternal(msg.sender, externalAmount, internalAmount);

        $.externalErc20.safeTransferFrom(msg.sender, address(this), externalAmount);
        $.internalErc20.mint(msg.sender, internalAmount);
    }

    function _convertToExternal(uint128 internalAmount) internal virtual isInWhitelist(msg.sender) {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        uint128 externalAmount = internalAmount * RATE_MIDDLE / $.rate;
        if (externalAmount < $.externalMinAmount) {
            revert TossExchangeAmounIsLessThanMin("external", externalAmount, $.externalMinAmount);
        }

        emit ConvertedToExternal(msg.sender, externalAmount, internalAmount);

        $.internalErc20.burnFrom(msg.sender, internalAmount);
        $.externalErc20.safeTransfer(msg.sender, externalAmount);
    }
}
