// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { IERC721 } from "@openzeppelin/contracts/token/ERC721/IERC721.sol";
import { IERC20Permit } from "@openzeppelin/contracts/token/ERC20/extensions/IERC20Permit.sol";
import { SafeERC20, IERC20 } from "@openzeppelin/contracts/token/ERC20/Utils/SafeERC20.sol";
import { IERC165 } from "@openzeppelin/contracts/utils/introspection/IERC165.sol";
import { PausableUpgradeable } from "@openzeppelin/contracts-upgradeable/utils/PausableUpgradeable.sol";
import { AccessControlUpgradeable } from "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import { ReentrancyGuardUpgradeable } from "@openzeppelin/contracts-upgradeable/utils/ReentrancyGuardUpgradeable.sol";
import { TossUUPSUpgradeable } from "./TossUUPSUpgradeable.sol";
import { ITossMarket } from "../Interfaces/ITossMarket.sol";
import { TossWhitelistClient } from "./TossWhitelistClient.sol";
import { ITossErc721Market } from "../Interfaces/ITossErc721Market.sol";
import "../Interfaces/TossErrors.sol";

struct Royalty {
    uint16 cut;
    address destination;
}

abstract contract TossMarketBase is ITossMarket, TossWhitelistClient, PausableUpgradeable, AccessControlUpgradeable, ReentrancyGuardUpgradeable, TossUUPSUpgradeable {
    /// @custom:storage-location erc7201:tossplatform.storage.TossMarketBase
    struct TossMarketBaseStorage {
        address erc20BankAddress;
        IERC20 erc20;
        uint16 marketCut;
        mapping(address => Erc721Market) erc721Markets;
    }

    // keccak256(abi.encode(uint256(keccak256("tossplatform.storage.TossMarketBase")) - 1)) & ~bytes32(uint256(0xff))
    bytes32 private constant TossMarketBaseStorageLocation = 0xdceca8311a055028be07277e7c9e8760875027fca343beefe7b8eae623484f00;

    function _getTossMarketBaseStorage() internal pure returns (TossMarketBaseStorage storage $) {
        assembly {
            $.slot := TossMarketBaseStorageLocation
        }
    }

    using SafeERC20 for IERC20;

    uint16 public constant CUT_PRECISION = 10_000;
    uint8 public constant MAX_ROYALTY_LENGTH = 10;

    bytes32 public constant PAUSER_ROLE = keccak256("PAUSER_ROLE");
    bytes32 public constant UPGRADER_ROLE = keccak256("UPGRADER_ROLE");

    struct Erc721Market {
        bool active;
        Royalty[] royalties;
        mapping(uint256 => SellOffer) offers;
    }

    struct SellOffer {
        uint128 price;
        uint128 startedAt;
        address owner;
    }

    event SellOfferCreated(address indexed owner, address indexed erc721Address, uint256 indexed tokenId, uint128 startedAt, uint128 price);
    event SellOfferSold(address indexed owner, address indexed erc721Address, uint256 indexed tokenId, uint128 startedAt, uint128 price, address buyer);
    event SellOfferCancelled(address indexed owner, address indexed erc721Address, uint256 indexed tokenId, uint128 startedAt);

    error TossMarketNotOwnerOfErc721(address sender, address owner);
    error TossMarketIsOwnerOfErc721(address sender);
    error TossMarketErc721NotActive(address erc721);
    error TossMarketErc721AlreadyActive(address erc721);
    error TossMarketErc721NotOnSell(address erc721, uint256 tokenId);
    error TossMarketSellPriceChange(uint128 realPrice, uint128 userPrice);
    error TossMarketRoyaltyCutOutOfRange(uint16 value, address destination);
    error TossMarketRoyaltyLengthOutOfRange(uint8 max, uint256 amount);

    function __TossMarketBase_init(IERC20 erc20_, uint16 marketCut_, address bankAddress_) internal onlyInitializing {
        __Pausable_init();
        __AccessControl_init();
        __ReentrancyGuard_init();
        __TossUUPSUpgradeable_init();
        __TossMarketBase_init_unchained(erc20_, marketCut_, bankAddress_);
    }

    function __TossMarketBase_init_unchained(IERC20 erc20_, uint16 marketCut_, address bankAddress_) internal onlyInitializing {
        if (address(erc20_) == address(0)) {
            revert TossAddressIsZero("erc20");
        }
        if (address(bankAddress_) == address(0)) {
            revert TossAddressIsZero("bank");
        }
        if (marketCut_ > CUT_PRECISION) {
            revert TossCutOutOfRange(marketCut_);
        }

        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);
        _grantRole(PAUSER_ROLE, msg.sender);

        TossMarketBaseStorage storage $ = _getTossMarketBaseStorage();
        $.erc20BankAddress = bankAddress_;
        $.erc20 = erc20_;
        $.marketCut = marketCut_;
    }

    function _authorizeUpgrade(address newImplementation) internal override onlyRole(UPGRADER_ROLE) { }

    function onERC721Received(address, address, uint256, bytes calldata) external pure returns (bytes4) {
        return this.onERC721Received.selector;
    }

    function supportsInterface(bytes4 interfaceId) public view virtual override(IERC165, AccessControlUpgradeable) returns (bool) {
        return interfaceId == type(ITossMarket).interfaceId || super.supportsInterface(interfaceId);
    }

    function pause() external onlyRole(PAUSER_ROLE) {
        _pause();
    }

    function unpause() external onlyRole(PAUSER_ROLE) {
        _unpause();
    }

    function setWhitelist(address newAddress) external override onlyRole(DEFAULT_ADMIN_ROLE) {
        _setWhitelist(newAddress);
    }

    function getErc20() external view returns (IERC20) {
        return _getTossMarketBaseStorage().erc20;
    }

    function getErc20BankAddress() external view returns (address bankAddress) {
        return _getTossMarketBaseStorage().erc20BankAddress;
    }

    function setErc20BankAddress(address newAddress) external onlyRole(DEFAULT_ADMIN_ROLE) {
        if (newAddress == address(0)) {
            revert TossAddressIsZero("bank");
        }
        _getTossMarketBaseStorage().erc20BankAddress = newAddress;
    }

    function getMarketCut() external view returns (uint16 cut) {
        return _getTossMarketBaseStorage().marketCut;
    }

    function setMarketCut(uint16 cut) external onlyRole(DEFAULT_ADMIN_ROLE) {
        if (cut > CUT_PRECISION) {
            revert TossCutOutOfRange(cut);
        }
        _getTossMarketBaseStorage().marketCut = cut;
    }

    function addErc721Market(address erc721Address, Royalty[] memory royalties) external onlyRole(DEFAULT_ADMIN_ROLE) {
        if (erc721Address == address(0)) {
            revert TossAddressIsZero("erc721");
        }

        uint256 royaltyLength = royalties.length;
        if (royaltyLength > MAX_ROYALTY_LENGTH) {
            revert TossMarketRoyaltyLengthOutOfRange(MAX_ROYALTY_LENGTH, royaltyLength);
        }

        if (erc721Address.code.length > 0 && !IERC721(erc721Address).supportsInterface(type(ITossErc721Market).interfaceId)) {
            revert TossUnsupportedInterface("ITossErc721Market");
        }

        TossMarketBaseStorage storage $ = _getTossMarketBaseStorage();
        Erc721Market storage erc721Market = $.erc721Markets[erc721Address];
        if (erc721Market.active) {
            revert TossMarketErc721AlreadyActive(erc721Address);
        }

        uint16 totalCut;
        for (uint256 i = 0; i < royaltyLength; i++) {
            address destination = royalties[i].destination;
            if (destination == address(0)) {
                revert TossAddressIsZero("Royalty Destination");
            }
            uint16 cut = royalties[i].cut;
            if (cut == 0 || cut > CUT_PRECISION) {
                revert TossMarketRoyaltyCutOutOfRange(cut, destination);
            }
            totalCut += cut;
        }

        totalCut += $.marketCut;
        if (totalCut > CUT_PRECISION) {
            revert TossCutOutOfRange(totalCut);
        }

        erc721Market.active = true;
        for (uint256 i = 0; i < royaltyLength; i++) {
            erc721Market.royalties.push(royalties[i]);
        }
    }

    function removeErc721Market(address erc721Address) external onlyRole(DEFAULT_ADMIN_ROLE) {
        TossMarketBaseStorage storage $ = _getTossMarketBaseStorage();
        Erc721Market storage erc721Market = $.erc721Markets[erc721Address];
        uint256 royaltyLength = erc721Market.royalties.length;
        if (!erc721Market.active && royaltyLength == 0) {
            revert TossMarketErc721NotActive(erc721Address);
        }

        erc721Market.active = false;
        for (uint256 i = royaltyLength; i > 0; --i) {
            erc721Market.royalties.pop();
        }
    }

    function getErc721Market(address erc721Address) external view returns (bool active, Royalty[] memory royalties) {
        TossMarketBaseStorage storage $ = _getTossMarketBaseStorage();
        Erc721Market storage erc721Market = $.erc721Markets[erc721Address];
        active = erc721Market.active;
        royalties = erc721Market.royalties;
    }

    function createSellOffer(uint256 tokenId, uint128 price, address owner) external virtual override nonReentrant whenNotPaused isInWhitelist(owner) {
        TossMarketBaseStorage storage $ = _getTossMarketBaseStorage();
        address erc721Address = msg.sender;
        Erc721Market storage erc721Market = $.erc721Markets[erc721Address];
        if (!erc721Market.active) {
            revert TossMarketErc721NotActive(erc721Address);
        }

        uint128 startedAt = uint128(block.timestamp);
        erc721Market.offers[tokenId] = SellOffer({ price: price, startedAt: startedAt, owner: owner });

        emit SellOfferCreated(owner, erc721Address, tokenId, startedAt, price);

        IERC721(erc721Address).safeTransferFrom(owner, address(this), tokenId);
    }

    function buy(address erc721Address, uint256 tokenId, uint128 price) external {
        buyInternal(erc721Address, tokenId, price);
    }

    function buyWithPermit(address erc721Address, uint256 tokenId, uint128 price, uint256 amount, uint256 deadline, uint8 v, bytes32 r, bytes32 s) external {
        IERC20Permit(address(_getTossMarketBaseStorage().erc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        buyInternal(erc721Address, tokenId, price);
    }

    function buyInternal(address erc721Address, uint256 tokenId, uint128 buyPrice) private nonReentrant whenNotPaused isInWhitelist(msg.sender) {
        TossMarketBaseStorage storage $ = _getTossMarketBaseStorage();
        Erc721Market storage erc721Market = $.erc721Markets[erc721Address];
        if (!erc721Market.active) {
            revert TossMarketErc721NotActive(erc721Address);
        }

        SellOffer memory sellOffer = erc721Market.offers[tokenId];
        if (!onSell(sellOffer.startedAt)) {
            revert TossMarketErc721NotOnSell(erc721Address, tokenId);
        }

        address owner = sellOffer.owner;
        if (msg.sender == owner) {
            revert TossMarketIsOwnerOfErc721(msg.sender);
        }

        uint128 price = sellOffer.price;
        if (price != buyPrice) {
            revert TossMarketSellPriceChange(price, buyPrice);
        }

        delete erc721Market.offers[tokenId];

        emit SellOfferSold(owner, erc721Address, tokenId, sellOffer.startedAt, price, msg.sender);

        splitAndTransfer($, erc721Market, price, owner);

        IERC721(erc721Address).safeTransferFrom(address(this), msg.sender, tokenId);
    }

    function splitAndTransfer(TossMarketBaseStorage storage $, Erc721Market storage erc721Market, uint128 price, address owner) private {
        uint128 marketAmount = (price * $.marketCut / CUT_PRECISION);
        uint128 ownerAmount = price - marketAmount;

        uint16 totalCut;
        uint128 totalCutAmount;
        uint256 royaltyLength = erc721Market.royalties.length;
        uint128[] memory royaltyAmounts = new uint128[](royaltyLength);
        for (uint256 i = 0; i < royaltyLength; i++) {
            uint16 cut = erc721Market.royalties[i].cut;
            totalCutAmount += royaltyAmounts[i] = (price * cut / CUT_PRECISION);
            totalCut += cut;
        }

        totalCut += $.marketCut;
        if (totalCut > CUT_PRECISION) {
            revert TossCutOutOfRange(totalCut);
        }

        ownerAmount -= totalCutAmount;

        $.erc20.safeTransferFrom(msg.sender, owner, ownerAmount);
        $.erc20.safeTransferFrom(msg.sender, $.erc20BankAddress, marketAmount);

        for (uint256 i = 0; i < royaltyLength; i++) {
            $.erc20.safeTransferFrom(msg.sender, erc721Market.royalties[i].destination, royaltyAmounts[i]);
        }
    }

    function cancel(address erc721Address, uint256 tokenId) external whenNotPaused {
        cancelSellOffer(erc721Address, tokenId, true);
    }

    function cancelWhenPaused(address erc721Address, uint256 tokenId) external whenPaused onlyRole(DEFAULT_ADMIN_ROLE) {
        cancelSellOffer(erc721Address, tokenId, false);
    }

    function cancelSellOffer(address erc721Address, uint256 tokenId, bool validateOwner) internal nonReentrant {
        TossMarketBaseStorage storage $ = _getTossMarketBaseStorage();
        Erc721Market storage erc721Market = $.erc721Markets[erc721Address];
        SellOffer memory sellOffer = erc721Market.offers[tokenId];
        uint128 startedAt = sellOffer.startedAt;
        if (!onSell(startedAt)) {
            revert TossMarketErc721NotOnSell(erc721Address, tokenId);
        }

        address owner = sellOffer.owner;
        if (validateOwner && msg.sender != owner) {
            revert TossMarketNotOwnerOfErc721(msg.sender, owner);
        }

        delete erc721Market.offers[tokenId];

        emit SellOfferCancelled(owner, erc721Address, tokenId, startedAt);

        IERC721(erc721Address).safeTransferFrom(address(this), owner, tokenId);
    }

    function get(address erc721Address, uint256 tokenId) external view returns (address owner, uint128 price, uint128 startedAt) {
        SellOffer memory sellOffer = _getTossMarketBaseStorage().erc721Markets[erc721Address].offers[tokenId];
        startedAt = sellOffer.startedAt;
        if (!onSell(startedAt)) {
            revert TossMarketErc721NotOnSell(erc721Address, tokenId);
        }
        owner = sellOffer.owner;
        price = sellOffer.price;
    }

    function getPrice(address erc721Address, uint256 tokenId) external view returns (uint256 price) {
        SellOffer memory sellOffer = _getTossMarketBaseStorage().erc721Markets[erc721Address].offers[tokenId];
        if (!onSell(sellOffer.startedAt)) {
            revert TossMarketErc721NotOnSell(erc721Address, tokenId);
        }
        return sellOffer.price;
    }

    function onSell(uint128 startedAt) internal pure returns (bool) {
        return startedAt >= 1;
    }
}
