// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { AccessControlUpgradeable } from "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import { TossUUPSUpgradeable } from "./TossUUPSUpgradeable.sol";
import { ITossWhitelist } from "../Interfaces/ITossWhitelist.sol";

abstract contract TossWhitelistBase is ITossWhitelist, AccessControlUpgradeable, TossUUPSUpgradeable {
    bytes32 public constant UPGRADER_ROLE = keccak256("UPGRADER_ROLE");

    mapping(address => bool) private whitelist;

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossWhitelistBase_init() public initializer {
        __AccessControl_init();
        __TossUUPSUpgradeable_init();

        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);
    }

    function _authorizeUpgrade(address newImplementation) internal override onlyRole(UPGRADER_ROLE) { }

    function set(address address_, bool enabled) external onlyRole(DEFAULT_ADMIN_ROLE) {
        whitelist[address_] = enabled;
    }

    function isInWhitelist(address address_) external view override returns (bool) {
        return whitelist[address_];
    }
}
