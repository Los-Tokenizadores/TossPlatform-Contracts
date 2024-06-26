// SPDX-License-Identifier: MIT
pragma solidity 0.8.20;

import { TossWhitelistBase } from "./Bases/TossWhitelistBase.sol";

contract TossWhitelistV1 is TossWhitelistBase {
    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossWhitelistV1_init() public initializer {
        __TossWhitelistBase_init();
    }
}
