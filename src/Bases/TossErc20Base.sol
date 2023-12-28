// SPDX-License-Identifier: MIT
pragma solidity 0.8.23;

import { ERC20Upgradeable } from "@openzeppelin/contracts-upgradeable/token/ERC20/ERC20Upgradeable.sol";
import { ERC20BurnableUpgradeable } from "@openzeppelin/contracts-upgradeable/token/ERC20/extensions/ERC20BurnableUpgradeable.sol";
import { ERC20PausableUpgradeable } from "@openzeppelin/contracts-upgradeable/token/ERC20/extensions/ERC20PausableUpgradeable.sol";
import { ERC20PermitUpgradeable } from "@openzeppelin/contracts-upgradeable/token/ERC20/extensions/ERC20PermitUpgradeable.sol";
import { AccessControlUpgradeable } from "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import { TossUUPSUpgradeable } from "./TossUUPSUpgradeable.sol";
import { TossWhitelistClient } from "./TossWhitelistClient.sol";

abstract contract TossErc20Base is
    TossWhitelistClient,
    ERC20Upgradeable,
    ERC20BurnableUpgradeable,
    ERC20PausableUpgradeable,
    AccessControlUpgradeable,
    ERC20PermitUpgradeable,
    TossUUPSUpgradeable
{
    bytes32 public constant PAUSER_ROLE = keccak256("PAUSER_ROLE");
    bytes32 public constant MINTER_ROLE = keccak256("MINTER_ROLE");
    bytes32 public constant UPGRADER_ROLE = keccak256("UPGRADER_ROLE");

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossErc20Base_init(string memory name, string memory symbol, uint256 amount) public onlyInitializing {
        __ERC20_init(name, symbol);
        __ERC20Burnable_init();
        __ERC20Pausable_init();
        __AccessControl_init();
        __ERC20Permit_init(name);
        __TossUUPSUpgradeable_init();
        __TossErc20Base_init_unchained(amount);
    }

    function __TossErc20Base_init_unchained(uint256 amount) public onlyInitializing {
        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);
        _grantRole(PAUSER_ROLE, msg.sender);
        _grantRole(MINTER_ROLE, msg.sender);

        _mint(msg.sender, amount);
    }

    function _authorizeUpgrade(address newImplementation) internal override onlyRole(UPGRADER_ROLE) { }

    function _update(address from, address to, uint256 value) internal virtual override(ERC20Upgradeable, ERC20PausableUpgradeable) {
        super._update(from, to, value);
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

    function mint(address to, uint256 amount) external onlyRole(MINTER_ROLE) {
        _mint(to, amount);
    }
}
