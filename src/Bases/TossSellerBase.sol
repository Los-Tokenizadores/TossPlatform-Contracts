// SPDX-License-Identifier: MIT
pragma solidity 0.8.23;

import { PausableUpgradeable } from "@openzeppelin/contracts-upgradeable/utils/PausableUpgradeable.sol";
import { AccessControlUpgradeable } from "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import { SafeERC20, IERC20, IERC20Permit } from "@openzeppelin/contracts/token/ERC20/Utils/SafeERC20.sol";
import { ReentrancyGuardUpgradeable } from "@openzeppelin/contracts-upgradeable/utils/ReentrancyGuardUpgradeable.sol";
import { TossUUPSUpgradeable } from "./TossUUPSUpgradeable.sol";
import { ITossSellErc721 } from "../Interfaces/ITossSellErc721.sol";
import { TossWhitelistClient } from "./TossWhitelistClient.sol";
import "../Interfaces/TossErrors.sol";

abstract contract TossSellerBase is TossWhitelistClient, PausableUpgradeable, AccessControlUpgradeable, ReentrancyGuardUpgradeable, TossUUPSUpgradeable {
    /// @custom:storage-location erc7201:tossplatform.storage.TossSellerBase
    struct TossSellerBaseStorage {
        uint256 convertToOffchainMinAmount;
        uint32 convertToOffchainRate;
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

    struct Erc721SellInfo {
        uint128 price;
        uint8 maxAmount;
    }

    bytes32 public constant PAUSER_ROLE = keccak256("PAUSER_ROLE");
    bytes32 public constant CONVERT_ROLE = keccak256("CONVERT_ROLE");
    bytes32 public constant UPGRADER_ROLE = keccak256("UPGRADER_ROLE");
    uint8 private constant decimals = 18;
    uint8 private constant SELL_MAX_LIMIT = 40;
    uint16 private constant CUT_PRECISION = 10_000;

    event ConvertToOffchain(address indexed account, uint256 indexed erc20Amount, uint32 indexed offchainAmount);
    event ConvertToErc20(address indexed account, uint256 indexed erc20Amount, uint32 indexed offchainAmount);

    error TossSellerNotOnSell(address erc721);
    error TossSellerBuyAmountGreatherThanMax(address erc721, uint8 amount, uint8 max);
    error TossSellerBuyMaxAmountExceeded(uint8 amount, uint8 max);
    error TossSellConvertOffchainAmountLessThanMin(uint256 amount, uint256 min);
    error TossSellConvertErc20AmountLessThanMin(uint32 amount, uint32 min);

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossSellerBase_init(IERC20 erc20_) public onlyInitializing {
        __Pausable_init();
        __AccessControl_init();
        __ReentrancyGuard_init();
        __TossUUPSUpgradeable_init();
        __TossSellerBase_init_unchained(erc20_);
    }

    function __TossSellerBase_init_unchained(IERC20 erc20_) public onlyInitializing {
        if (address(erc20_) == address(0)) {
            revert TossAddressIsZero("erc20");
        }

        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);
        _grantRole(PAUSER_ROLE, msg.sender);
        _grantRole(CONVERT_ROLE, msg.sender);

        TossSellerBaseStorage storage $ = _getTossSellerBaseStorage();
        $.erc20 = erc20_;
        $.erc20BankAddress = msg.sender;

        $.convertToOffchainRate = 100;
        $.convertToOffchainMinAmount = 1 ether;
        $.convertToOffchainCut = 0;

        $.convertToErc20Rate = 500;
        $.convertToErc20MinAmount = 500;
        $.convertToErc20Cut = 500;
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

    function getErc20() external view returns (IERC20 erc20) {
        return _getTossSellerBaseStorage().erc20;
    }

    function getErc721Sells(ITossSellErc721 erc721) external view returns (Erc721SellInfo memory info) {
        return _getTossSellerBaseStorage().erc721Sells[erc721];
    }

    function getErc20BankAddress() external view onlyRole(DEFAULT_ADMIN_ROLE) returns (address bankAddress) {
        return _getTossSellerBaseStorage().erc20BankAddress;
    }

    function setErc20BankAddress(address newAddress) external onlyRole(DEFAULT_ADMIN_ROLE) {
        if (newAddress == address(0)) {
            revert TossAddressIsZero("bank");
        }
        _getTossSellerBaseStorage().erc20BankAddress = newAddress;
    }

    function withdrawBalance(uint256 amount) external onlyRole(DEFAULT_ADMIN_ROLE) nonReentrant {
        TossSellerBaseStorage storage $ = _getTossSellerBaseStorage();
        $.erc20.safeTransfer($.erc20BankAddress, amount);
    }

    function buyErc721(ITossSellErc721 erc721, uint8 amount) external isInWhitelist(msg.sender) nonReentrant {
        buyErc721Internal(erc721, amount);
    }

    function buyErc721WithPermit(
        ITossSellErc721 erc721,
        uint8 buyAmount,
        uint256 amount,
        uint256 deadline,
        uint8 v,
        bytes32 r,
        bytes32 s
    ) external isInWhitelist(msg.sender) nonReentrant {
        IERC20Permit(address(_getTossSellerBaseStorage().erc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        buyErc721Internal(erc721, buyAmount);
    }

    function buyErc721Internal(ITossSellErc721 erc721, uint8 amount) private {
        TossSellerBaseStorage storage $ = _getTossSellerBaseStorage();
        if (address(erc721) == address(0)) {
            revert TossAddressIsZero("erc721");
        }
        if (amount == 0) {
            revert TossValueIsZero("amount");
        }

        Erc721SellInfo memory erc721Sell = $.erc721Sells[erc721];
        if (erc721Sell.price == 0) {
            revert TossSellerNotOnSell(address(erc721));
        }
        if (amount > erc721Sell.maxAmount) {
            revert TossSellerBuyAmountGreatherThanMax(address(erc721), amount, erc721Sell.maxAmount);
        }

        uint256 price = uint256(erc721Sell.price) * amount;
        $.erc20.safeTransferFrom(msg.sender, address(this), price);

        erc721.sellErc721(msg.sender, amount);
    }

    function setErc721Sell(ITossSellErc721 erc721, uint128 price, uint8 maxAmount) external onlyRole(DEFAULT_ADMIN_ROLE) nonReentrant {
        if (address(erc721) == address(0)) {
            revert TossAddressIsZero("erc721");
        }
        if (!erc721.supportsInterface(type(ITossSellErc721).interfaceId)) {
            revert TossUnsupportedInterface("ITossSellErc721");
        }
        if (maxAmount == 0) {
            revert TossValueIsZero("maxAmount");
        }
        if (maxAmount > SELL_MAX_LIMIT) {
            revert TossSellerBuyMaxAmountExceeded(maxAmount, SELL_MAX_LIMIT);
        }
        _getTossSellerBaseStorage().erc721Sells[erc721] = Erc721SellInfo({ price: price, maxAmount: maxAmount });
    }

    function convertToOffchain(uint256 erc20Amount) external isInWhitelist(msg.sender) nonReentrant {
        convertToOffchainInternal(erc20Amount);
    }

    function convertToOffchainWithPermit(uint256 erc20Amount, uint256 amount, uint256 deadline, uint8 v, bytes32 r, bytes32 s) external isInWhitelist(msg.sender) nonReentrant {
        IERC20Permit(address(_getTossSellerBaseStorage().erc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        convertToOffchainInternal(erc20Amount);
    }

    function convertToOffchainInternal(uint256 erc20Amount) private {
        TossSellerBaseStorage storage $ = _getTossSellerBaseStorage();
        if (erc20Amount < $.convertToOffchainMinAmount) {
            revert TossSellConvertOffchainAmountLessThanMin(erc20Amount, $.convertToOffchainMinAmount);
        }

        uint256 offchainAmountBig = erc20Amount * $.convertToOffchainRate * (CUT_PRECISION - $.convertToOffchainCut) / CUT_PRECISION;
        uint32 offchainAmount = uint32(offchainAmountBig / 1 ether);

        emit ConvertToOffchain(msg.sender, erc20Amount, offchainAmount);

        $.erc20.safeTransferFrom(msg.sender, address(this), erc20Amount);
    }

    function convertToErc20(address user, uint32 offchainAmount) external onlyRole(CONVERT_ROLE) isInWhitelist(user) nonReentrant {
        TossSellerBaseStorage storage $ = _getTossSellerBaseStorage();
        if (user == address(0)) {
            revert TossAddressIsZero("user");
        }
        if (offchainAmount < $.convertToErc20MinAmount) {
            revert TossSellConvertErc20AmountLessThanMin(offchainAmount, $.convertToErc20MinAmount);
        }

        uint256 erc20Amount = offchainAmount * 1 ether * (CUT_PRECISION - $.convertToErc20Cut) / $.convertToErc20Rate / CUT_PRECISION;

        emit ConvertToErc20(user, erc20Amount, offchainAmount);

        $.erc20.safeTransfer(user, erc20Amount);
    }

    function getConvertToOffchainRate() external view returns (uint32 rate) {
        return _getTossSellerBaseStorage().convertToOffchainRate;
    }

    function setConvertToOffchainRate(uint32 newRate) external onlyRole(CONVERT_ROLE) {
        if (newRate == 0) {
            revert TossValueIsZero("rate");
        }
        _getTossSellerBaseStorage().convertToOffchainRate = newRate;
    }

    function getConvertToOffchainMinAmount() external view returns (uint256 minAmount) {
        return _getTossSellerBaseStorage().convertToOffchainMinAmount;
    }

    function setConvertToOffchainMinAmount(uint256 minAmount) external onlyRole(CONVERT_ROLE) {
        if (minAmount < 1 ether) {
            revert TossValueIsLessThanOneEther("min amount");
        }
        _getTossSellerBaseStorage().convertToOffchainMinAmount = minAmount;
    }

    function getConvertToOffchainCut() external view returns (uint16 cut) {
        return _getTossSellerBaseStorage().convertToOffchainCut;
    }

    function setConvertToOffchainCut(uint16 newCut) external onlyRole(CONVERT_ROLE) {
        if (newCut > CUT_PRECISION) {
            revert TossCutOutOfRange(newCut);
        }
        _getTossSellerBaseStorage().convertToOffchainCut = newCut;
    }

    function getConvertToErc20Rate() external view returns (uint32 rate) {
        return _getTossSellerBaseStorage().convertToErc20Rate;
    }

    function setConvertToErc20Rate(uint32 newRate) external onlyRole(CONVERT_ROLE) {
        if (newRate == 0) {
            revert TossValueIsZero("rate");
        }
        _getTossSellerBaseStorage().convertToErc20Rate = newRate;
    }

    function getConvertToErc20MinAmount() external view returns (uint32 minAmount) {
        return _getTossSellerBaseStorage().convertToErc20MinAmount;
    }

    function setConvertToErc20MinAmount(uint32 minAmount) external onlyRole(CONVERT_ROLE) {
        if (minAmount == 0) {
            revert TossValueIsZero("min amount");
        }
        _getTossSellerBaseStorage().convertToErc20MinAmount = minAmount;
    }

    function getConvertToErc20Cut() external view returns (uint16 cut) {
        return _getTossSellerBaseStorage().convertToErc20Cut;
    }

    function setConvertToErc20Cut(uint16 newCut) external onlyRole(CONVERT_ROLE) {
        if (newCut > CUT_PRECISION) {
            revert TossCutOutOfRange(newCut);
        }
        _getTossSellerBaseStorage().convertToErc20Cut = newCut;
    }
}
