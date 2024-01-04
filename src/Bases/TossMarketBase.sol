// SPDX-License-Identifier: MIT
pragma solidity 0.8.23;

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
import "../Interfaces/TossErrors.sol";

abstract contract TossMarketBase is ITossMarket, TossWhitelistClient, PausableUpgradeable, AccessControlUpgradeable, ReentrancyGuardUpgradeable, TossUUPSUpgradeable {
    /// @custom:storage-location erc7201:tossplatform.storage.TossMarketBase
    struct TossMarketBaseStorage {
        address erc20BankAddress;
        IERC20 erc20;
        uint16 marketCut;
        mapping(address => mapping(uint256 => SellOffer)) erc721Markets;
    }

    // keccak256(abi.encode(uint256(keccak256("tossplatform.storage.TossMarketBase")) - 1)) & ~bytes32(uint256(0xff))
    bytes32 private constant TossMarketBaseStorageLocation = 0xdceca8311a055028be07277e7c9e8760875027fca343beefe7b8eae623484f00;

    function _getTossMarketBaseStorage() internal pure returns (TossMarketBaseStorage storage $) {
        assembly {
            $.slot := TossMarketBaseStorageLocation
        }
    }

    using SafeERC20 for IERC20;

    uint256 private constant MARKET_CUT_PRECISION = 10_000;

    bytes32 public constant PAUSER_ROLE = keccak256("PAUSER_ROLE");
    bytes32 public constant EXTRACT_ROLE = keccak256("EXTRACT_ROLE");
    bytes32 public constant UPGRADER_ROLE = keccak256("UPGRADER_ROLE");
    bytes32 public constant ERC721_SELLER_ROLE = keccak256("ERC721_SELLER_ROLE");

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
    error TossMarketErc721NotOnSell(address erc721, uint256 tokenId);
    error TossMarketSellPriceChange(uint128 realPrice, uint128 userPrice);

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function onERC721Received(address, address, uint256, bytes calldata) external pure returns (bytes4) {
        return this.onERC721Received.selector;
    }

    function supportsInterface(bytes4 interfaceId) public view virtual override(IERC165, AccessControlUpgradeable) returns (bool) {
        return interfaceId == type(ITossMarket).interfaceId || super.supportsInterface(interfaceId);
    }

    function __TossMarketBase_init(IERC20 erc20_, uint16 marketCut_) public onlyInitializing {
        __Pausable_init();
        __AccessControl_init();
        __ReentrancyGuard_init();
        __TossUUPSUpgradeable_init();
        __TossMarketBase_init_unchained(erc20_, marketCut_);
    }

    function __TossMarketBase_init_unchained(IERC20 erc20_, uint16 marketCut_) public onlyInitializing {
        if (address(erc20_) == address(0)) {
            revert TossAddressIsZero("erc20");
        }
        if (marketCut_ > MARKET_CUT_PRECISION) {
            revert TossCutOutOfRange(marketCut_);
        }

        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);
        _grantRole(PAUSER_ROLE, msg.sender);
        _grantRole(EXTRACT_ROLE, msg.sender);

        TossMarketBaseStorage storage $ = _getTossMarketBaseStorage();
        $.erc20BankAddress = msg.sender;
        $.erc20 = erc20_;
        $.marketCut = marketCut_;
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

    function getErc20() external view returns (IERC20) {
        return _getTossMarketBaseStorage().erc20;
    }

    function getErc20BankAddress() external view onlyRole(EXTRACT_ROLE) returns (address bankAddress) {
        return _getTossMarketBaseStorage().erc20BankAddress;
    }

    function setErc20BankAddress(address newAddress) external onlyRole(DEFAULT_ADMIN_ROLE) {
        if (newAddress == address(0)) {
            revert TossAddressIsZero("bank");
        }
        _getTossMarketBaseStorage().erc20BankAddress = newAddress;
    }

    function withdrawBalance(uint256 amount) external onlyRole(EXTRACT_ROLE) nonReentrant {
        TossMarketBaseStorage storage $ = _getTossMarketBaseStorage();
        $.erc20.safeTransfer($.erc20BankAddress, amount);
    }

    function getMarketCut() external view onlyRole(DEFAULT_ADMIN_ROLE) returns (uint16 cut) {
        return _getTossMarketBaseStorage().marketCut;
    }

    function changeMarketCut(uint16 cut) external onlyRole(DEFAULT_ADMIN_ROLE) {
        if (cut > MARKET_CUT_PRECISION) {
            revert TossCutOutOfRange(cut);
        }
        _getTossMarketBaseStorage().marketCut = cut;
    }

    function createSellOffer(uint256 tokenId, uint128 price, address owner) external virtual override whenNotPaused onlyRole(ERC721_SELLER_ROLE) nonReentrant {
        address erc721Address = msg.sender;
        if (IERC721(erc721Address).ownerOf(tokenId) != owner) {
            revert TossMarketNotOwnerOfErc721(msg.sender, owner);
        }

        uint128 startedAt = uint128(block.timestamp);
        _getTossMarketBaseStorage().erc721Markets[erc721Address][tokenId] = SellOffer({ price: price, startedAt: startedAt, owner: owner });

        emit SellOfferCreated(owner, erc721Address, tokenId, startedAt, price);

        IERC721(erc721Address).safeTransferFrom(owner, address(this), tokenId);
    }

    function buy(address erc721Address, uint256 tokenId, uint128 price) external whenNotPaused isInWhitelist(msg.sender) nonReentrant {
        buyInternal(erc721Address, tokenId, price);
    }

    function buyWithPermit(
        address erc721Address,
        uint256 tokenId,
        uint128 price,
        uint256 amount,
        uint256 deadline,
        uint8 v,
        bytes32 r,
        bytes32 s
    ) external whenNotPaused isInWhitelist(msg.sender) nonReentrant {
        IERC20Permit(address(_getTossMarketBaseStorage().erc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        buyInternal(erc721Address, tokenId, price);
    }

    function buyInternal(address erc721Address, uint256 tokenId, uint128 buyPrice) private {
        TossMarketBaseStorage storage $ = _getTossMarketBaseStorage();
        SellOffer memory sellOffer = $.erc721Markets[erc721Address][tokenId];

        uint128 startedAt = sellOffer.startedAt;
        if (!onSell(startedAt)) {
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

        delete $.erc721Markets[erc721Address][tokenId];

        emit SellOfferSold(owner, erc721Address, tokenId, startedAt, price, msg.sender);

        $.erc20.safeTransferFrom(msg.sender, address(this), price);
        $.erc20.safeTransfer(owner, priceMinusCut($.marketCut, price));
        IERC721(erc721Address).safeTransferFrom(address(this), msg.sender, tokenId);
    }

    function cancel(address erc721Address, uint256 tokenId) external nonReentrant {
        cancelSellOffer(erc721Address, tokenId, true);
    }

    function cancelWhenPaused(address erc721Address, uint256 tokenId) external whenPaused onlyRole(DEFAULT_ADMIN_ROLE) nonReentrant {
        cancelSellOffer(erc721Address, tokenId, false);
    }

    function get(address erc721Address, uint256 tokenId) external view returns (address owner, uint256 price, uint128 startedAt) {
        SellOffer memory sellOffer = _getTossMarketBaseStorage().erc721Markets[erc721Address][tokenId];
        startedAt = sellOffer.startedAt;
        if (!onSell(startedAt)) {
            revert TossMarketErc721NotOnSell(erc721Address, tokenId);
        }
        owner = sellOffer.owner;
        price = sellOffer.price;
    }

    function getPrice(address erc721Address, uint256 tokenId) external view returns (uint256 price) {
        SellOffer memory sellOffer = _getTossMarketBaseStorage().erc721Markets[erc721Address][tokenId];
        if (!onSell(sellOffer.startedAt)) {
            revert TossMarketErc721NotOnSell(erc721Address, tokenId);
        }
        return sellOffer.price;
    }

    function cancelSellOffer(address erc721Address, uint256 tokenId, bool validateOwner) internal {
        TossMarketBaseStorage storage $ = _getTossMarketBaseStorage();
        SellOffer memory sellOffer = $.erc721Markets[erc721Address][tokenId];
        uint128 startedAt = sellOffer.startedAt;
        if (!onSell(startedAt)) {
            revert TossMarketErc721NotOnSell(erc721Address, tokenId);
        }

        address owner = sellOffer.owner;
        if (validateOwner && msg.sender != owner) {
            revert TossMarketNotOwnerOfErc721(msg.sender, owner);
        }

        delete $.erc721Markets[erc721Address][tokenId];

        emit SellOfferCancelled(owner, erc721Address, tokenId, startedAt);

        IERC721(erc721Address).safeTransferFrom(address(this), owner, tokenId);
    }

    function onSell(uint128 startedAt) internal pure returns (bool) {
        return startedAt >= 1;
    }

    function priceMinusCut(uint16 marketCut_, uint256 price) internal pure returns (uint256) {
        return price - (price * marketCut_ / MARKET_CUT_PRECISION);
    }
}
