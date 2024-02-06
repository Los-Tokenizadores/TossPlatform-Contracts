// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { ERC721Upgradeable } from "@openzeppelin/contracts-upgradeable/token/ERC721/ERC721Upgradeable.sol";
import { ERC721PausableUpgradeable } from "@openzeppelin/contracts-upgradeable/token/ERC721/extensions/ERC721PausableUpgradeable.sol";
import { AccessControlUpgradeable } from "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import { ReentrancyGuardUpgradeable } from "@openzeppelin/contracts-upgradeable/utils/ReentrancyGuardUpgradeable.sol";
import { TossUUPSUpgradeable } from "./TossUUPSUpgradeable.sol";
import { ITossMarket } from "../Interfaces/ITossMarket.sol";
import { TossWhitelistClient } from "./TossWhitelistClient.sol";
import "../Interfaces/TossErrors.sol";

abstract contract TossErc721MarketBase is
    TossWhitelistClient,
    ERC721Upgradeable,
    ERC721PausableUpgradeable,
    AccessControlUpgradeable,
    ReentrancyGuardUpgradeable,
    TossUUPSUpgradeable
{
    /// @custom:storage-location erc7201:tossplatform.storage.TossErc721MarketBase
    struct TossErc721MarketBaseStorage {
        ITossMarket market;
        string baseUri;
    }

    // keccak256(abi.encode(uint256(keccak256("tossplatform.storage.TossErc721MarketBase")) - 1)) & ~bytes32(uint256(0xff))
    bytes32 private constant TossErc721MarketBaseStorageLocation = 0x7632e33b12507a4855f6678ca0e8955963b6dfcb053a9dd74381736814525400;

    function _getTossErc721MarketBaseStorage() private pure returns (TossErc721MarketBaseStorage storage $) {
        assembly {
            $.slot := TossErc721MarketBaseStorageLocation
        }
    }

    error TossErc721MarketNotSet();

    bytes32 public constant PAUSER_ROLE = keccak256("PAUSER_ROLE");
    bytes32 public constant MINTER_ROLE = keccak256("MINTER_ROLE");
    bytes32 public constant UPGRADER_ROLE = keccak256("UPGRADER_ROLE");

    function __TossErc721MarketBase_init(string memory name_, string memory symbol_) internal onlyInitializing {
        __ERC721_init(name_, symbol_);
        __ERC721Pausable_init();
        __AccessControl_init();
        __ReentrancyGuard_init();
        __TossUUPSUpgradeable_init();
        __TossErc721MarketBase_init_unchained();
    }

    function __TossErc721MarketBase_init_unchained() internal onlyInitializing {
        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);
        _grantRole(PAUSER_ROLE, msg.sender);
        _grantRole(MINTER_ROLE, msg.sender);
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

    function _update(
        address to,
        uint256 tokenId,
        address auth
    ) internal virtual override(ERC721Upgradeable, ERC721PausableUpgradeable) isInWhitelist(to) whenNotPaused returns (address) {
        return super._update(to, tokenId, auth);
    }

    function _baseURI() internal view virtual override returns (string memory) {
        return _getTossErc721MarketBaseStorage().baseUri;
    }

    function getBaseUri() external view onlyRole(DEFAULT_ADMIN_ROLE) returns (string memory baseUri) {
        return _baseURI();
    }

    function setBaseUri(string memory baseUri_) external onlyRole(DEFAULT_ADMIN_ROLE) {
        _getTossErc721MarketBaseStorage().baseUri = baseUri_;
    }

    function supportsInterface(bytes4 interfaceId) public view virtual override(ERC721Upgradeable, AccessControlUpgradeable) returns (bool) {
        return super.supportsInterface(interfaceId);
    }

    function createSellOffer(uint256 tokenId, uint128 price) external nonReentrant whenNotPaused {
        TossErc721MarketBaseStorage storage $ = _getTossErc721MarketBaseStorage();
        if (address($.market) == address(0)) {
            revert TossErc721MarketNotSet();
        }
        _approve(address($.market), tokenId, msg.sender);
        $.market.createSellOffer(tokenId, price, msg.sender);
    }

    function getMarket() external view returns (address marketAddress) {
        return address(_getTossErc721MarketBaseStorage().market);
    }

    function setMarket(ITossMarket market) external nonReentrant onlyRole(DEFAULT_ADMIN_ROLE) {
        if (address(market) != address(0) && !market.supportsInterface(type(ITossMarket).interfaceId)) {
            revert TossUnsupportedInterface("ITossMarket");
        }
        _getTossErc721MarketBaseStorage().market = market;
    }
}
