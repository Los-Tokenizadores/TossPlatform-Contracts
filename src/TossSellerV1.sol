// SPDX-License-Identifier: MIT
pragma solidity ^0.8.19;

import "./Bases/TossSellerBase.sol";

contract TossSellerV1 is TossSellerBase {
    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossSellerV1_init(IERC20 erc20) public initializer {
        __TossSellerBase_init(erc20);
    }
}
