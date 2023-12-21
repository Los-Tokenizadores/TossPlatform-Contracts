// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { AccessControlUpgradeable } from "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import { TossUUPSUpgradeable } from "./TossUUPSUpgradeable.sol";
import { ITossWhitelist } from "../Interfaces/ITossWhitelist.sol";

abstract contract TossWhitelistBase is ITossWhitelist, AccessControlUpgradeable, TossUUPSUpgradeable {
    /// @custom:storage-location erc7201:tossplatform.storage.TossWhitelistBase
    struct TossWhitelistBaseStorage {
        mapping(address => bool) whitelist;
    }

    // keccak256(abi.encode(uint256(keccak256("tossplatform.storage.TossWhitelistBase")) - 1)) & ~bytes32(uint256(0xff))
    bytes32 private constant TossWhitelistBaseStorageLocation = 0xcebc1a3b8ad63e53791efc9ab8cbf968aecbbb9586b3861b648d859ddf849800;

    function _getTossWhitelistBaseStorage() internal pure returns (TossWhitelistBaseStorage storage $) {
        assembly {
            $.slot := TossWhitelistBaseStorageLocation
        }
    }

    bytes32 public constant UPGRADER_ROLE = keccak256("UPGRADER_ROLE");

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossWhitelistBase_init() public onlyInitializing {
        __AccessControl_init();
        __TossUUPSUpgradeable_init();
        __TossWhitelistBase_init_unchained();
    }

    function __TossWhitelistBase_init_unchained() public onlyInitializing {
        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);
    }

    function _authorizeUpgrade(address newImplementation) internal override onlyRole(UPGRADER_ROLE) { }

    function set(address address_, bool enabled) external onlyRole(DEFAULT_ADMIN_ROLE) {
        _getTossWhitelistBaseStorage().whitelist[address_] = enabled;
    }

    function isInWhitelist(address address_) external view override returns (bool value) {
        return _getTossWhitelistBaseStorage().whitelist[address_];
    }
}
