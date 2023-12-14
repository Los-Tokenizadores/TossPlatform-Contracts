// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { IERC20 } from "@openzeppelin/contracts/token/ERC20/IERC20.sol";
import { TossMarketBase } from "./Bases/TossMarketBase.sol";

contract TossMarketV1 is TossMarketBase {
    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossMarketV1_init(IERC20 erc20_, uint16 marketCut_) public initializer {
        __TossMarketBase_init(erc20_, marketCut_);
    }
}
