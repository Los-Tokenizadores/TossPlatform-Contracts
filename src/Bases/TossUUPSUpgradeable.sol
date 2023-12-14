// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { UUPSUpgradeable } from "@openzeppelin/contracts-upgradeable/proxy/utils/UUPSUpgradeable.sol";
import { ERC1967Utils } from "@openzeppelin/contracts/proxy/ERC1967/ERC1967Utils.sol";

abstract contract TossUUPSUpgradeable is UUPSUpgradeable {
    function __TossUUPSUpgradeable_init() public initializer {
        __UUPSUpgradeable_init();
    }

    function getImplementation() public view returns (address) {
        return ERC1967Utils.getImplementation();
    }
}