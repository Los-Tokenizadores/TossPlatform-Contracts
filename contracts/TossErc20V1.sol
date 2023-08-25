// SPDX-License-Identifier: MIT
pragma solidity ^0.8.19;

import "./Bases/TossErc20Base.sol";

contract TossErc20V1 is TossErc20Base {
    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossErc20V1_init(string memory name, string memory symbol, uint64 amount) initializer public {
        __TossErc20Base_init(name, symbol, amount);
    }
}