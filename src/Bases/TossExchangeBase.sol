// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { AccessControlUpgradeable } from "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import { IERC20Permit } from "@openzeppelin/contracts/token/ERC20/extensions/IERC20Permit.sol";
import { SafeERC20, IERC20 } from "@openzeppelin/contracts/token/ERC20/Utils/SafeERC20.sol";
import { TossUUPSUpgradeable } from "./TossUUPSUpgradeable.sol";
import { TossErc20Base } from "./TossErc20Base.sol";
import { ITossExchange } from "../Interfaces/ITossExchange.sol";
import { TossWhitelistClient } from "./TossWhitelistClient.sol";

abstract contract TossExchangeBase is ITossExchange, TossWhitelistClient, AccessControlUpgradeable, TossUUPSUpgradeable {
    /// @custom:storage-location erc7201:tossplatform.storage.TossExchangeBase
    struct TossExchangeBaseStorage {
        uint256 externalMinAmount;
        uint256 internalMinAmount;
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

    event ConvertedToInternal(address indexed account, uint256 externalAmount, uint256 internalAmount);
    event ConvertedToExternal(address indexed account, uint256 externalAmount, uint256 internalAmount);

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossExchangeBase_init(
        IERC20 externalErc20_,
        uint256 externalMinAmount_,
        TossErc20Base internalErc20_,
        uint256 internalMinAmount_,
        uint64 rate_
    ) public onlyInitializing {
        __AccessControl_init();
        __TossUUPSUpgradeable_init();
        __TossExchangeBase_init_unchained(externalErc20_, externalMinAmount_, internalErc20_, internalMinAmount_, rate_);
    }

    function __TossExchangeBase_init_unchained(
        IERC20 externalErc20_,
        uint256 externalMinAmount_,
        TossErc20Base internalErc20_,
        uint256 internalMinAmount_,
        uint64 rate_
    ) public onlyInitializing {
        require(address(externalErc20_) != address(0), "external erc20 address is empty");
        require(address(internalErc20_) != address(0), "internal erc20 address is empty");
        require(address(externalErc20_) != address(internalErc20_), "external and internal erc20 are the same");
        require(rate_ > 0, "rate needs to be greater than 0");
        require(externalMinAmount_ > 0, "external min needs to be greater than 0");
        require(internalMinAmount_ > 0, "internal min needs to be greater than 0");
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

    function getExternalMinAmount() external view returns (uint256) {
        return _getTossExchangeBaseStorage().externalMinAmount;
    }

    function setExternalMinAmount(uint256 value) external virtual onlyRole(DEFAULT_ADMIN_ROLE) {
        require(value > 0, "min needs to be greater than 0");
        _getTossExchangeBaseStorage().externalMinAmount = value;
    }

    function getInternalMinAmount() external view returns (uint256) {
        return _getTossExchangeBaseStorage().internalMinAmount;
    }

    function setInternalMinAmount(uint256 value) external virtual onlyRole(DEFAULT_ADMIN_ROLE) {
        require(value > 0, "min needs to be greater than 0");
        _getTossExchangeBaseStorage().internalMinAmount = value;
    }

    function getExternalErc20() external view returns (IERC20) {
        return _getTossExchangeBaseStorage().externalErc20;
    }

    function getInternalErc20() external view returns (TossErc20Base) {
        return _getTossExchangeBaseStorage().internalErc20;
    }

    function getRate() external view returns (uint64) {
        return _getTossExchangeBaseStorage().rate;
    }

    function convertToInternal(uint128 externalAmount) public virtual {
        _convertToInternal(externalAmount, externalAmount * _getTossExchangeBaseStorage().rate / 10_000);
    }

    function convertToInternalWithPermit(uint128 externalAmount, uint256 amount, uint256 deadline, uint8 v, bytes32 r, bytes32 s) public virtual {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        IERC20Permit(address($.externalErc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        _convertToInternal(externalAmount, externalAmount * $.rate / 10_000);
    }

    function convertToExternal(uint128 internalAmount) public virtual {
        _convertToExternal(internalAmount * 10_000 / _getTossExchangeBaseStorage().rate, internalAmount);
    }

    function convertToExternalWithPermit(uint128 internalAmount, uint256 amount, uint256 deadline, uint8 v, bytes32 r, bytes32 s) public virtual {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        IERC20Permit(address($.internalErc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        _convertToExternal(internalAmount * 10_000 / $.rate, internalAmount);
    }

    function _convertToInternal(uint128 externalAmount, uint128 internalAmount) internal virtual isInWhitelist(msg.sender) {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        require(internalAmount >= $.internalMinAmount, "amount need greater than the minimum amount");
        require($.externalErc20.balanceOf(msg.sender) >= externalAmount, "insufficient balance");

        $.externalErc20.safeTransferFrom(msg.sender, address(this), externalAmount);
        $.internalErc20.mint(msg.sender, internalAmount);

        emit ConvertedToInternal(msg.sender, externalAmount, internalAmount);
    }

    function _convertToExternal(uint128 externalAmount, uint128 internalAmount) internal virtual isInWhitelist(msg.sender) {
        TossExchangeBaseStorage storage $ = _getTossExchangeBaseStorage();
        require(externalAmount >= $.externalMinAmount, "amount need greater than the minimum amount");
        require($.internalErc20.balanceOf(msg.sender) >= internalAmount, "insufficient balance");

        $.internalErc20.burnFrom(msg.sender, internalAmount);
        $.externalErc20.safeTransfer(msg.sender, externalAmount);

        emit ConvertedToExternal(msg.sender, externalAmount, internalAmount);
    }
}
