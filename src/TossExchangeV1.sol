// SPDX-License-Identifier: MIT
pragma solidity 0.8.23;

import { IERC20 } from "@openzeppelin/contracts/token/ERC20/IERC20.sol";
import { TossErc20Base } from "./Bases/TossErc20Base.sol";
import { TossExchangeBase } from "./Bases/TossExchangeBase.sol";

contract TossExchangeV1 is TossExchangeBase {
    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossExchangeV1_init(IERC20 externalErc20_, uint128 externalMinAmount_, TossErc20Base internalErc20_, uint128 internalMinAmount_) public initializer {
        __TossExchangeBase_init(externalErc20_, externalMinAmount_, internalErc20_, internalMinAmount_);
    }
}
