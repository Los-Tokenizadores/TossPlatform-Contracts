// SPDX-License-Identifier: MIT
pragma solidity ^0.8.19;

import "./Bases/TossWhitelistBase.sol";

contract TossWhitelistV1 is TossWhitelistBase {
    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossWhitelistV1_init() initializer public {
        __TossWhitelistBase_init();
    }
}