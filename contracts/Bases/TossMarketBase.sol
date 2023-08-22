// SPDX-License-Identifier: MIT
pragma solidity ^0.8.17;

import "@openzeppelin/contracts/token/ERC721/IERC721.sol";
import "@openzeppelin/contracts/token/ERC20/Utils/SafeERC20.sol";
import "@openzeppelin/contracts-upgradeable/security/PausableUpgradeable.sol";
import "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import "./TossUUPSUpgradeable.sol";
import "../Interfaces/ITossMarket.sol";
import "./TossWhitelistClient.sol";

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
    
    mapping (address => mapping (uint256 => SellOffer)) private erc721Markets;

    address private erc20BankAddress;

    address public erc20Address;
    uint16 private marketCut;

    event SellOfferCreated(address indexed owner, address indexed erc721Address, uint256 indexed tokenId, uint128 startedAt, uint128 price);
    event SellOfferSuccessful(address indexed owner, address indexed erc721Address, uint256 indexed tokenId, uint128 startedAt, uint128 price, address winner);
    event SellOfferCancelled(address indexed owner, address indexed erc721Address, uint256 indexed tokenId, uint128 startedAt);

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function onERC721Received(address, address, uint256, bytes calldata) external pure returns (bytes4){
        return this.onERC721Received.selector;
    }

    function __TossMarketBase_init(address erc20Address_, uint16 marketCut_) initializer public {
        require(erc20Address_ != address(0));
        require(marketCut_ <= 10000, "cut required between 0 and 10000");

        __Pausable_init();
        __AccessControl_init();
        __TossUUPSUpgradeable_init();

        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);
        _grantRole(PAUSER_ROLE, msg.sender);
        _grantRole(EXTRACT_ROLE, msg.sender);

        erc20BankAddress = msg.sender;
        erc20Address = erc20Address_;
        marketCut = marketCut_;
    }

    function _authorizeUpgrade(address newImplementation) internal onlyRole(UPGRADER_ROLE) override {}
    
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
        //require(IERC20(erc20Address).balanceOf(address(this)) == 0, "withdraw balance before change erc20");
        erc20BankAddress = newAddress;
    } 

    function withdrawBalance(uint256 amount) external onlyRole(EXTRACT_ROLE) {
        require(amount <= IERC20(erc20Address).balanceOf(address(this)), "insufficient balance");
        IERC20(erc20Address).safeTransfer(erc20BankAddress, amount);
    }
    
    function getMarketCut() external view onlyRole(DEFAULT_ADMIN_ROLE) returns (uint16) {
        return marketCut;
    }

    function changeMarketCut(uint16 cut) external onlyRole(DEFAULT_ADMIN_ROLE) {
        require(cut <= 10000, "cut required between 0 and 10000");
        marketCut = cut;
    }

    function create(uint256 tokenId, uint128 price, address owner) virtual override public whenNotPaused onlyRole(ERC721_SELLER_ROLE) {
        address erc721Address = msg.sender;
        require(IERC721(erc721Address).ownerOf(tokenId) == owner, "Not token owner");

        IERC721(erc721Address).safeTransferFrom(owner, address(this), tokenId);
        uint128 startedAt = uint128(block.timestamp);
         erc721Markets[erc721Address][tokenId] = SellOffer({
            price: price,
            startedAt: startedAt, 
            owner: owner
        });

        emit SellOfferCreated(owner, erc721Address, tokenId, startedAt, price);
    }

    function buy(address erc721Address, uint256 tokenId, uint128 amount) virtual external whenNotPaused {
        require(isInWhitelist(msg.sender), "wallet not in whitelist");
        SellOffer memory sellOffer = erc721Markets[erc721Address][tokenId];
        
        uint128 startedAt = sellOffer.startedAt;
        require(onSell(startedAt), "not on sale");
        
        address owner = sellOffer.owner;
        require(msg.sender != owner, "owner is trying to buy his own");
        
        uint128 price = sellOffer.price;
        require(price == amount, "price is not the same");
        
        IERC20(erc20Address).safeTransferFrom(msg.sender, address(this), amount);
        IERC20(erc20Address).safeTransfer(owner, priceMinusCut(marketCut, price));
        IERC721(erc721Address).safeTransferFrom(address(this), msg.sender, tokenId);

        delete erc721Markets[erc721Address][tokenId];

        emit SellOfferSuccessful(owner, erc721Address, tokenId, startedAt, price, msg.sender);
    }

    function cancel(address erc721Address, uint256 tokenId) external {
        cancelSellOffer(erc721Address, tokenId, true);
    }
    
    function cancelWhenPaused(address erc721Address, uint256 tokenId) whenPaused onlyRole(DEFAULT_ADMIN_ROLE) external {
        cancelSellOffer(erc721Address, tokenId, false);
    }

    function get(address erc721Address, uint256 tokenId) external view returns (address owner, uint256 price, uint256 startedAt) {
        SellOffer memory sellOffer = erc721Markets[erc721Address][tokenId];
        uint128 startedAt_ = sellOffer.startedAt;
        require(onSell(startedAt_), "not on sale");
        return (
            sellOffer.owner,
            sellOffer.price,
            startedAt_
        );
    }

    function getPrice(address erc721Address, uint256 tokenId) external view returns (uint256) {
        SellOffer memory sellOffer = erc721Markets[erc721Address][tokenId];
        require(onSell(sellOffer.startedAt), "not on sale");
        return sellOffer.price;
    }

    function cancelSellOffer(address erc721Address, uint256 tokenId,  bool validateOwner) internal {
        SellOffer memory sellOffer = erc721Markets[erc721Address][tokenId];
        uint128 startedAt = sellOffer.startedAt;
        require(onSell(startedAt), "not on sale");
        address owner = sellOffer.owner;
        if(validateOwner){
            require(owner == msg.sender, "not the owner");
        }        
        IERC721(erc721Address).safeTransferFrom(address(this), owner, tokenId);
        emit SellOfferCancelled(owner, erc721Address, tokenId, startedAt);
        delete erc721Markets[erc721Address][tokenId];
    }

    function onSell(uint128 startedAt) internal pure returns (bool) {
        return startedAt >= 1;
    }

    function priceMinusCut(uint16 marketCut_, uint256 price) internal pure returns (uint256) {
        return price - (price * marketCut_ / 10000);
    }

    /**
     * @dev This empty reserved space is put in place to allow future versions to add new
     * variables without shifting down storage in the inheritance chain.
     * See https://docs.openzeppelin.com/contracts/4.x/upgradeable#storage_gaps
     */
    uint256[50] private __gap;
}