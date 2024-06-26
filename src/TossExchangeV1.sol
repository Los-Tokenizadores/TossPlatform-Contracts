// SPDX-License-Identifier: MIT
pragma solidity 0.8.20;

import { IERC20 } from "@openzeppelin/contracts/token/ERC20/IERC20.sol";
import { TossErc20Base } from "./Bases/TossErc20Base.sol";
import { TossExchangeBase } from "./Bases/TossExchangeBase.sol";

contract TossExchangeV1 is TossExchangeBase {
    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossExchangeV1_init(IERC20 externalErc20_, uint128 depositMinAmount_, TossErc20Base internalErc20_, uint128 withdrawMinAmount_) public initializer {
        __TossExchangeBase_init(externalErc20_, depositMinAmount_, internalErc20_, withdrawMinAmount_);
    }
}
