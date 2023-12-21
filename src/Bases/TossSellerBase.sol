// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { PausableUpgradeable } from "@openzeppelin/contracts-upgradeable/utils/PausableUpgradeable.sol";
import { AccessControlUpgradeable } from "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import { SafeERC20, IERC20, IERC20Permit } from "@openzeppelin/contracts/token/ERC20/Utils/SafeERC20.sol";
import { TossUUPSUpgradeable } from "./TossUUPSUpgradeable.sol";
import { ITossSellErc721 } from "../Interfaces/ITossSellErc721.sol";
import { TossWhitelistClient } from "./TossWhitelistClient.sol";

abstract contract TossSellerBase is TossWhitelistClient, PausableUpgradeable, AccessControlUpgradeable, TossUUPSUpgradeable {
    /// @custom:storage-location erc7201:tossplatform.storage.TossSellerBase
    struct TossSellerBaseStorage {
        uint32 convertToOffchainRate;
        uint256 convertToOffchainMinAmount;
        uint16 convertToOffchainCut;
        uint32 convertToErc20Rate;
        uint32 convertToErc20MinAmount;
        uint16 convertToErc20Cut;
        IERC20 erc20;
        address erc20BankAddress;
        mapping(ITossSellErc721 => Erc721SellInfo) erc721Sells;
    }

    // keccak256(abi.encode(uint256(keccak256("tossplatform.storage.TossSellerBase")) - 1)) & ~bytes32(uint256(0xff))
    bytes32 private constant TossSellerBaseStorageLocation = 0x9d4cdb77c8b797a90b348a5d86e9085ba88d4b319afd2f19bbbfb13b22391200;

    function _getTossSellerBaseStorage() internal pure returns (TossSellerBaseStorage storage $) {
        assembly {
            $.slot := TossSellerBaseStorageLocation
        }
    }

    using SafeERC20 for IERC20;

    bytes32 public constant PAUSER_ROLE = keccak256("PAUSER_ROLE");
    bytes32 public constant CONVERT_ROLE = keccak256("CONVERT_ROLE");
    bytes32 public constant UPGRADER_ROLE = keccak256("UPGRADER_ROLE");
    uint8 private constant decimals = 18;

    struct Erc721SellInfo {
        uint128 price;
        uint8 maxAmount;
    }

    event ConvertToOffchain(address indexed account, uint256 erc20Amount, uint32 offchainAmount);
    event ConvertToErc20(address indexed account, uint256 erc20Amount, uint32 offchainAmount);

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossSellerBase_init(IERC20 erc20_) public onlyInitializing {
        __Pausable_init();
        __AccessControl_init();
        __TossUUPSUpgradeable_init();
        __TossSellerBase_init_unchained(erc20_);
    }

    function __TossSellerBase_init_unchained(IERC20 erc20_) public onlyInitializing {
        require(address(erc20_) != address(0));

        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);
        _grantRole(PAUSER_ROLE, msg.sender);
        _grantRole(CONVERT_ROLE, msg.sender);

        TossSellerBaseStorage storage $ = _getTossSellerBaseStorage();
        $.erc20 = erc20_;
        $.erc20BankAddress = msg.sender;

        $.convertToOffchainRate = 100;
        $.convertToOffchainMinAmount = 1 * 10 ** 18;
        $.convertToOffchainCut = 0;

        $.convertToErc20Rate = 500;
        $.convertToErc20MinAmount = 500;
        $.convertToErc20Cut = 500;
    }

    function _authorizeUpgrade(address newImplementation) internal override onlyRole(UPGRADER_ROLE) { }

    function setWhitelist(address newAddress) external override onlyRole(DEFAULT_ADMIN_ROLE) {
        _setWhitelist(newAddress);
    }

    function getErc20() external view returns (IERC20) {
        return _getTossSellerBaseStorage().erc20;
    }

    function getErc721Sells(ITossSellErc721 erc721) external view returns (Erc721SellInfo memory) {
        return _getTossSellerBaseStorage().erc721Sells[erc721];
    }

    function getErc20BankAddress() external view onlyRole(DEFAULT_ADMIN_ROLE) returns (address) {
        return _getTossSellerBaseStorage().erc20BankAddress;
    }

    function setErc20BankAddress(address newAddress) external onlyRole(DEFAULT_ADMIN_ROLE) {
        require(newAddress != address(0));
        _getTossSellerBaseStorage().erc20BankAddress = newAddress;
    }

    function withdrawBalance(uint256 amount) external onlyRole(DEFAULT_ADMIN_ROLE) {
        TossSellerBaseStorage storage $ = _getTossSellerBaseStorage();
        require(amount <= $.erc20.balanceOf(address(this)), "insufficient balance");
        $.erc20.safeTransfer($.erc20BankAddress, amount);
    }

    function buyErc721(ITossSellErc721 erc721, uint8 amount) external isInWhitelist(msg.sender) {
        buyErc721Internal(erc721, amount);
    }

    function buyErc721WithPermit(ITossSellErc721 erc721, uint8 buyAmount, uint256 amount, uint256 deadline, uint8 v, bytes32 r, bytes32 s) external isInWhitelist(msg.sender) {
        IERC20Permit(address(_getTossSellerBaseStorage().erc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        buyErc721Internal(erc721, buyAmount);
    }

    function buyErc721Internal(ITossSellErc721 erc721, uint8 amount) private {
        TossSellerBaseStorage storage $ = _getTossSellerBaseStorage();
        require(address(erc721) != address(0), "erc721 address invalid");
        require(amount > 0, "create amount needs to be greater than 0");

        Erc721SellInfo memory erc721Sell = $.erc721Sells[erc721];
        require(erc721Sell.price > 0, "erc721 not in sell in this seller");
        require(amount <= erc721Sell.maxAmount, "create amount needs to be less than assigned limit");

        uint256 price = uint256(erc721Sell.price * amount);
        uint256 balance = $.erc20.balanceOf(msg.sender);
        require(balance >= price, "insufficient balance to transaction");

        $.erc20.safeTransferFrom(msg.sender, address(this), price);

        erc721.sellErc721(msg.sender, amount);
    }

    function setErc721Sell(ITossSellErc721 erc721, uint128 price, uint8 maxAmount) external onlyRole(DEFAULT_ADMIN_ROLE) {
        require(address(erc721) != address(0), "invalid erc721 address");
        require(maxAmount > 0, "maxAmount needs to be greater than 0");
        require(maxAmount <= 40, "maxAmount needs to be less or equals than 40");
        _getTossSellerBaseStorage().erc721Sells[erc721] = Erc721SellInfo({ price: price, maxAmount: maxAmount });
    }

    function convertToOffchain(uint256 erc20Amount) external isInWhitelist(msg.sender) {
        convertToOffchainInternal(erc20Amount);
    }

    function convertToOffchainWithPermit(uint256 erc20Amount, uint256 amount, uint256 deadline, uint8 v, bytes32 r, bytes32 s) external isInWhitelist(msg.sender) {
        IERC20Permit(address(_getTossSellerBaseStorage().erc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        convertToOffchainInternal(erc20Amount);
    }

    function convertToOffchainInternal(uint256 erc20Amount) private {
        TossSellerBaseStorage storage $ = _getTossSellerBaseStorage();
        uint256 balance = $.erc20.balanceOf(msg.sender);
        require(balance >= erc20Amount, "insufficient balance");
        require(erc20Amount >= $.convertToOffchainMinAmount, "less than the minimum conversion value");

        $.erc20.safeTransferFrom(msg.sender, address(this), erc20Amount);

        uint256 offchainAmountBig = erc20Amount * $.convertToOffchainRate * (10_000 - $.convertToOffchainCut) / 10_000;
        uint32 offchainAmount = uint32(offchainAmountBig / (10 ** decimals));

        emit ConvertToOffchain(msg.sender, erc20Amount, offchainAmount);
    }

    function convertToErc20(address user, uint32 offchainAmount) external onlyRole(CONVERT_ROLE) isInWhitelist(user) {
        TossSellerBaseStorage storage $ = _getTossSellerBaseStorage();
        require(user != address(0));
        require(offchainAmount >= $.convertToErc20MinAmount, "less than the minimum conversion value");

        uint256 erc20Amount = offchainAmount * (10 ** decimals) / $.convertToErc20Rate * (10_000 - $.convertToErc20Cut) / 10_000;

        uint256 balance = $.erc20.balanceOf(address(this));
        require(balance >= erc20Amount, "insufficient balance");
        $.erc20.safeTransfer(user, erc20Amount);

        emit ConvertToErc20(user, erc20Amount, offchainAmount);
    }

    function getConvertToOffchainRate() external view returns (uint32) {
        return _getTossSellerBaseStorage().convertToOffchainRate;
    }

    function setConvertToOffchainRate(uint32 newRate) external onlyRole(CONVERT_ROLE) {
        require(newRate >= 1);
        _getTossSellerBaseStorage().convertToOffchainRate = newRate;
    }

    function getConvertToOffchainMinAmount() external view returns (uint256) {
        return _getTossSellerBaseStorage().convertToOffchainMinAmount;
    }

    function setConvertToOffchainMinAmount(uint256 newAmount) external onlyRole(CONVERT_ROLE) {
        require(newAmount >= 10 ** decimals);
        _getTossSellerBaseStorage().convertToOffchainMinAmount = newAmount;
    }

    function getConvertToOffchainCut() external view returns (uint16) {
        return _getTossSellerBaseStorage().convertToOffchainCut;
    }

    function setConvertToOffchainCut(uint16 newPercent) external onlyRole(CONVERT_ROLE) {
        require(newPercent >= 0 && newPercent <= 10_000);
        _getTossSellerBaseStorage().convertToOffchainCut = newPercent;
    }

    function getConvertToErc20Rate() external view returns (uint32) {
        return _getTossSellerBaseStorage().convertToErc20Rate;
    }

    function setConvertToErc20Rate(uint32 newRate) external onlyRole(CONVERT_ROLE) {
        require(newRate >= 1);
        _getTossSellerBaseStorage().convertToErc20Rate = newRate;
    }

    function getConvertToErc20MinAmount() external view returns (uint32) {
        return _getTossSellerBaseStorage().convertToErc20MinAmount;
    }

    function setConvertToErc20MinAmount(uint32 newAmount) external onlyRole(CONVERT_ROLE) {
        require(newAmount >= 1);
        _getTossSellerBaseStorage().convertToErc20MinAmount = newAmount;
    }

    function getConvertToErc20Cut() external view returns (uint16) {
        return _getTossSellerBaseStorage().convertToErc20Cut;
    }

    function setConvertToErc20Cut(uint16 newPercent) external onlyRole(CONVERT_ROLE) {
        require(newPercent >= 0 && newPercent <= 10_000);
        _getTossSellerBaseStorage().convertToErc20Cut = newPercent;
    }
}
