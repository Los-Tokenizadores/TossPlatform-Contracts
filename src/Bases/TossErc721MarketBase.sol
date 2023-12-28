// SPDX-License-Identifier: MIT
pragma solidity 0.8.23;

import { ERC721Upgradeable } from "@openzeppelin/contracts-upgradeable/token/ERC721/ERC721Upgradeable.sol";
import { ERC721PausableUpgradeable } from "@openzeppelin/contracts-upgradeable/token/ERC721/extensions/ERC721PausableUpgradeable.sol";
import { AccessControlUpgradeable } from "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import { TossUUPSUpgradeable } from "./TossUUPSUpgradeable.sol";
import { ITossMarket } from "../Interfaces/ITossMarket.sol";
import { TossWhitelistClient } from "./TossWhitelistClient.sol";

abstract contract TossErc721MarketBase is TossWhitelistClient, ERC721Upgradeable, ERC721PausableUpgradeable, AccessControlUpgradeable, TossUUPSUpgradeable {
    /// @custom:storage-location erc7201:tossplatform.storage.TossErc721MarketBase
    struct TossErc721MarketBaseStorage {
        address marketAddress;
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

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossErc721MarketBase_init(string memory name_, string memory symbol_) public onlyInitializing {
        __ERC721_init(name_, symbol_);
        __ERC721Pausable_init();
        __AccessControl_init();
        __TossUUPSUpgradeable_init();
        __TossErc721MarketBase_init_unchained();
    }

    function __TossErc721MarketBase_init_unchained() public onlyInitializing {
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

    function _update(address to, uint256 tokenId, address auth) internal virtual override(ERC721Upgradeable, ERC721PausableUpgradeable) whenNotPaused returns (address) {
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

    function supportsInterface(bytes4 interfaceId) public view override(ERC721Upgradeable, AccessControlUpgradeable) returns (bool) {
        return super.supportsInterface(interfaceId);
    }

    function createSellOffer(uint256 tokenId, uint128 price) external whenNotPaused {
        TossErc721MarketBaseStorage storage $ = _getTossErc721MarketBaseStorage();
        if ($.marketAddress == address(0)) {
            revert TossErc721MarketNotSet();
        }
        _approve($.marketAddress, tokenId, msg.sender);
        ITossMarket($.marketAddress).createSellOffer(tokenId, price, msg.sender);
    }

    function getMarket() external view returns (address marketAddress) {
        return _getTossErc721MarketBaseStorage().marketAddress;
    }

    function setMarket(address _address) external onlyRole(DEFAULT_ADMIN_ROLE) {
        _getTossErc721MarketBaseStorage().marketAddress = _address;
    }
}
