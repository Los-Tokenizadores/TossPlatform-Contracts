// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { ERC721PausableUpgradeable, ERC721Upgradeable } from "@openzeppelin/contracts-upgradeable/token/ERC721/extensions/ERC721PausableUpgradeable.sol";
import { AccessControlUpgradeable } from "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import { TossUUPSUpgradeable } from "./TossUUPSUpgradeable.sol";
import { ITossMarket } from "../Interfaces/ITossMarket.sol";
import { TossWhitelistClient } from "./TossWhitelistClient.sol";

abstract contract TossErc721MarketBase is TossWhitelistClient, ERC721Upgradeable, ERC721PausableUpgradeable, AccessControlUpgradeable, TossUUPSUpgradeable {
    bytes32 public constant PAUSER_ROLE = keccak256("PAUSER_ROLE");
    bytes32 public constant MINTER_ROLE = keccak256("MINTER_ROLE");
    bytes32 public constant UPGRADER_ROLE = keccak256("UPGRADER_ROLE");

    address public marketAddress;
    string private baseUri;

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossErc721MarketBase_init(string memory name_, string memory symbol_) public initializer {
        __ERC721_init(name_, symbol_);
        __ERC721Pausable_init();
        __AccessControl_init();
        __TossUUPSUpgradeable_init();

        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);
        _grantRole(PAUSER_ROLE, msg.sender);
        _grantRole(MINTER_ROLE, msg.sender);
    }

    function _authorizeUpgrade(address newImplementation) internal override onlyRole(UPGRADER_ROLE) { }

    function pause() public onlyRole(PAUSER_ROLE) {
        _pause();
    }

    function unpause() public onlyRole(PAUSER_ROLE) {
        _unpause();
    }

    function getWhitelist() external view onlyRole(DEFAULT_ADMIN_ROLE) returns (address) {
        return whitelistAddress;
    }

    function setWhitelist(address newAddress) external onlyRole(DEFAULT_ADMIN_ROLE) {
        whitelistAddress = newAddress;
    }

    function _update(address to, uint256 tokenId, address auth) internal virtual override(ERC721Upgradeable, ERC721PausableUpgradeable) whenNotPaused returns (address) {
        return super._update(to, tokenId, auth);
    }

    // The following functions are overrides required by Solidity.

    function _baseURI() internal view virtual override returns (string memory) {
        return baseUri;
    }

    function getBaseUri() external view onlyRole(DEFAULT_ADMIN_ROLE) returns (string memory) {
        return baseUri;
    }

    function setBaseUri(string memory baseUri_) external onlyRole(DEFAULT_ADMIN_ROLE) {
        baseUri = baseUri_;
    }

    function supportsInterface(bytes4 interfaceId) public view override(ERC721Upgradeable, AccessControlUpgradeable) returns (bool) {
        return super.supportsInterface(interfaceId);
    }

    function createSellOffer(uint256 tokenId, uint128 price) external whenNotPaused {
        require(marketAddress != address(0), "Market Address not set");
        require(msg.sender == ownerOf(tokenId), "Not tokenId owner");

        approve(marketAddress, tokenId);
        ITossMarket(marketAddress).createSellOffer(tokenId, price, msg.sender);
    }

    function setMarket(address _address) external onlyRole(DEFAULT_ADMIN_ROLE) {
        marketAddress = _address;
    }
}
