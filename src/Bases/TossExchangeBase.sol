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
    using SafeERC20 for IERC20;

    bytes32 public constant UPGRADER_ROLE = keccak256("UPGRADER_ROLE");

    uint256 public externalMinAmount;
    uint256 public internalMinAmount;

    IERC20 public externalErc20;
    TossErc20Base public internalErc20;

    uint64 public rate;

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
    ) public initializer {
        require(address(externalErc20_) != address(0), "external erc20 address is empty");
        require(address(internalErc20_) != address(0), "internal erc20 address is empty");
        require(address(externalErc20_) != address(internalErc20_), "external and internal erc20 are the same");
        require(rate_ > 0, "rate needs to be greater than 0");
        require(externalMinAmount_ > 0, "external min needs to be greater than 0");
        require(internalMinAmount_ > 0, "internal min needs to be greater than 0");

        __AccessControl_init();
        __TossUUPSUpgradeable_init();

        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);

        externalErc20 = externalErc20_;
        internalErc20 = internalErc20_;

        externalMinAmount = externalMinAmount_;
        internalMinAmount = internalMinAmount_;

        rate = rate_;
    }

    function _authorizeUpgrade(address newImplementation) internal override onlyRole(UPGRADER_ROLE) { }

    function getWhitelist() external view onlyRole(DEFAULT_ADMIN_ROLE) returns (address) {
        return whitelistAddress;
    }

    function setWhitelist(address newAddress) external onlyRole(DEFAULT_ADMIN_ROLE) {
        whitelistAddress = newAddress;
    }

    function setExternalMin(uint256 value) external virtual onlyRole(DEFAULT_ADMIN_ROLE) {
        require(value > 0, "min needs to be greater than 0");
        externalMinAmount = value;
    }

    function setInternalMin(uint256 value) external virtual onlyRole(DEFAULT_ADMIN_ROLE) {
        require(value > 0, "min needs to be greater than 0");
        internalMinAmount = value;
    }

    function convertToInternal(uint128 externalAmount) public virtual {
        _convertToInternal(externalAmount, externalAmount * rate / 10_000);
    }

    function convertToInternalWithPermit(uint128 externalAmount, uint256 amount, uint256 deadline, uint8 v, bytes32 r, bytes32 s) public virtual {
        IERC20Permit(address(externalErc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        _convertToInternal(externalAmount, externalAmount * rate / 10_000);
    }

    function convertToExternal(uint128 internalAmount) public virtual {
        _convertToExternal(internalAmount * 10_000 / rate, internalAmount);
    }

    function convertToExternalWithPermit(uint128 internalAmount, uint256 amount, uint256 deadline, uint8 v, bytes32 r, bytes32 s) public virtual {
        IERC20Permit(address(internalErc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        _convertToExternal(internalAmount * 10_000 / rate, internalAmount);
    }

    function _convertToInternal(uint128 externalAmount, uint128 internalAmount) internal virtual isInWhitelist(msg.sender) {
        require(internalAmount >= internalMinAmount, "amount need greater than the minimum amount");
        require(externalErc20.balanceOf(msg.sender) >= externalAmount, "insufficient balance");

        externalErc20.safeTransferFrom(msg.sender, address(this), externalAmount);
        internalErc20.mint(msg.sender, internalAmount);

        emit ConvertedToInternal(msg.sender, externalAmount, internalAmount);
    }

    function _convertToExternal(uint128 externalAmount, uint128 internalAmount) internal virtual isInWhitelist(msg.sender) {
        require(externalAmount >= externalMinAmount, "amount need greater than the minimum amount");
        require(internalErc20.balanceOf(msg.sender) >= internalAmount, "insufficient balance");

        internalErc20.burnFrom(msg.sender, internalAmount);
        externalErc20.safeTransfer(msg.sender, externalAmount);

        emit ConvertedToExternal(msg.sender, externalAmount, internalAmount);
    }
}
