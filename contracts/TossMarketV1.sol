// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import "./Bases/TossMarketBase.sol";

contract TossMarketV1 is TossMarketBase {
    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossMarketV1_init(address erc20Address_, uint16 marketCut_) initializer public {
        __TossMarketBase_init(erc20Address_, marketCut_);
    }
}