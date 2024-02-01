// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { UUPSUpgradeable } from "@openzeppelin/contracts-upgradeable/proxy/utils/UUPSUpgradeable.sol";
import { ERC1967Utils } from "@openzeppelin/contracts/proxy/ERC1967/ERC1967Utils.sol";

abstract contract TossUUPSUpgradeable is UUPSUpgradeable {
    function __TossUUPSUpgradeable_init() internal onlyInitializing {
        __UUPSUpgradeable_init();
    }

    function getImplementation() external view returns (address implementation) {
        return ERC1967Utils.getImplementation();
    }
}
