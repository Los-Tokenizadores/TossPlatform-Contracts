// SPDX-License-Identifier: MIT
pragma solidity ^0.8.19;

import "./Bases/TossSellerBase.sol";

contract TossSellerV1 is TossSellerBase {
    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossSellerV1_init(address erc20Address) initializer public {
        __TossSellerBase_init(erc20Address);
    }
}