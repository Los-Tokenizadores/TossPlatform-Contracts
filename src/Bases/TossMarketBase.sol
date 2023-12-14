// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { IERC721 } from "@openzeppelin/contracts/token/ERC721/IERC721.sol";
import { SafeERC20, IERC20 } from "@openzeppelin/contracts/token/ERC20/Utils/SafeERC20.sol";
import { PausableUpgradeable } from "@openzeppelin/contracts-upgradeable/utils/PausableUpgradeable.sol";
import { AccessControlUpgradeable } from "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import { TossUUPSUpgradeable } from "./TossUUPSUpgradeable.sol";
import { ITossMarket } from "../Interfaces/ITossMarket.sol";
import { TossWhitelistClient } from "./TossWhitelistClient.sol";

abstract contract TossMarketBase is ITossMarket, TossWhitelistClient, PausableUpgradeable, AccessControlUpgradeable, TossUUPSUpgradeable {
    using SafeERC20 for IERC20;

    bytes32 public constant PAUSER_ROLE = keccak256("PAUSER_ROLE");
    bytes32 public constant EXTRACT_ROLE = keccak256("EXTRACT_ROLE");
    bytes32 public constant UPGRADER_ROLE = keccak256("UPGRADER_ROLE");
    bytes32 public constant ERC721_SELLER_ROLE = keccak256("ERC721_SELLER_ROLE");

    struct SellOffer {
        uint128 price;
        uint128 startedAt;
        address owner;
    }

    mapping(address => mapping(uint256 => SellOffer)) private erc721Markets;

    address private erc20BankAddress;

    IERC20 public erc20;
    uint16 private marketCut;

    event SellOfferCreated(address indexed owner, address indexed erc721Address, uint256 indexed tokenId, uint128 startedAt, uint128 price);

    event SellOfferSold(address indexed owner, address indexed erc721Address, uint256 indexed tokenId, uint128 startedAt, uint128 price, address buyer);

    event SellOfferCancelled(address indexed owner, address indexed erc721Address, uint256 indexed tokenId, uint128 startedAt);

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function onERC721Received(address, address, uint256, bytes calldata) external pure returns (bytes4) {
        return this.onERC721Received.selector;
    }

    function __TossMarketBase_init(IERC20 erc20_, uint16 marketCut_) public initializer {
        require(address(erc20_) != address(0));
        require(marketCut_ <= 10_000, "cut required between 0 and 10000");

        __Pausable_init();
        __AccessControl_init();
        __TossUUPSUpgradeable_init();

        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);
        _grantRole(PAUSER_ROLE, msg.sender);
        _grantRole(EXTRACT_ROLE, msg.sender);

        erc20BankAddress = msg.sender;
        erc20 = erc20_;
        marketCut = marketCut_;
    }

    function _authorizeUpgrade(address newImplementation) internal override onlyRole(UPGRADER_ROLE) { }

    function getWhitelist() external view onlyRole(DEFAULT_ADMIN_ROLE) returns (address) {
        return whitelistAddress;
    }

    function setWhitelist(address newAddress) external onlyRole(DEFAULT_ADMIN_ROLE) {
        whitelistAddress = newAddress;
    }

    function getErc20BankAddress() external view onlyRole(EXTRACT_ROLE) returns (address) {
        return erc20BankAddress;
    }

    function setErc20BankAddress(address newAddress) external onlyRole(DEFAULT_ADMIN_ROLE) {
        require(newAddress != address(0));
        erc20BankAddress = newAddress;
    }

    function withdrawBalance(uint256 amount) external onlyRole(EXTRACT_ROLE) {
        require(amount <= IERC20(erc20).balanceOf(address(this)), "insufficient balance");
        IERC20(erc20).safeTransfer(erc20BankAddress, amount);
    }

    function getMarketCut() external view onlyRole(DEFAULT_ADMIN_ROLE) returns (uint16) {
        return marketCut;
    }

    function changeMarketCut(uint16 cut) external onlyRole(DEFAULT_ADMIN_ROLE) {
        require(cut <= 10_000, "cut required between 0 and 10000");
        marketCut = cut;
    }

    function createSellOffer(uint256 tokenId, uint128 price, address owner) public virtual override whenNotPaused onlyRole(ERC721_SELLER_ROLE) {
        address erc721Address = msg.sender;
        require(IERC721(erc721Address).ownerOf(tokenId) == owner, "Not token owner");

        IERC721(erc721Address).safeTransferFrom(owner, address(this), tokenId);
        uint128 startedAt = uint128(block.timestamp);
        erc721Markets[erc721Address][tokenId] = SellOffer({ price: price, startedAt: startedAt, owner: owner });

        emit SellOfferCreated(owner, erc721Address, tokenId, startedAt, price);
    }

    function buy(address erc721Address, uint256 tokenId, uint128 amount) external virtual whenNotPaused isInWhitelist(msg.sender) {
        SellOffer memory sellOffer = erc721Markets[erc721Address][tokenId];

        uint128 startedAt = sellOffer.startedAt;
        require(onSell(startedAt), "not on sale");

        address owner = sellOffer.owner;
        require(msg.sender != owner, "owner is trying to buy his own");

        uint128 price = sellOffer.price;
        require(price == amount, "price is not the same");

        delete erc721Markets[erc721Address][tokenId];

        IERC20(erc20).safeTransferFrom(msg.sender, address(this), amount);
        IERC20(erc20).safeTransfer(owner, priceMinusCut(marketCut, price));
        IERC721(erc721Address).safeTransferFrom(address(this), msg.sender, tokenId);

        emit SellOfferSold(owner, erc721Address, tokenId, startedAt, price, msg.sender);
    }

    function cancel(address erc721Address, uint256 tokenId) external {
        cancelSellOffer(erc721Address, tokenId, true);
    }

    function cancelWhenPaused(address erc721Address, uint256 tokenId) external whenPaused onlyRole(DEFAULT_ADMIN_ROLE) {
        cancelSellOffer(erc721Address, tokenId, false);
    }

    function get(address erc721Address, uint256 tokenId) external view returns (address owner, uint256 price, uint128 startedAt) {
        SellOffer memory sellOffer = erc721Markets[erc721Address][tokenId];
        startedAt = sellOffer.startedAt;
        require(onSell(startedAt), "not on sale");
        owner = sellOffer.owner;
        price = sellOffer.price;
    }

    function getPrice(address erc721Address, uint256 tokenId) external view returns (uint256 price) {
        SellOffer memory sellOffer = erc721Markets[erc721Address][tokenId];
        require(onSell(sellOffer.startedAt), "not on sale");
        price = sellOffer.price;
    }

    function cancelSellOffer(address erc721Address, uint256 tokenId, bool validateOwner) internal {
        SellOffer memory sellOffer = erc721Markets[erc721Address][tokenId];
        uint128 startedAt = sellOffer.startedAt;
        require(onSell(startedAt), "not on sale");
        address owner = sellOffer.owner;
        if (validateOwner) {
            require(owner == msg.sender, "not the owner");
        }
        delete erc721Markets[erc721Address][tokenId];
        IERC721(erc721Address).safeTransferFrom(address(this), owner, tokenId);
        emit SellOfferCancelled(owner, erc721Address, tokenId, startedAt);
    }

    function onSell(uint128 startedAt) internal pure returns (bool) {
        return startedAt >= 1;
    }

    function priceMinusCut(uint16 marketCut_, uint256 price) internal pure returns (uint256) {
        return price - (price * marketCut_ / 10_000);
    }
}
