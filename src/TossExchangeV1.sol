// SPDX-License-Identifier: MIT
pragma solidity ^0.8.19;

import "./Bases/TossExchangeBase.sol";

contract TossExchangeV1 is TossExchangeBase {
    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossExchangeV1_init(
        IERC20 externalErc20_,
        uint256 externalMinAmount_,
        TossErc20Base internalErc20_,
        uint256 internalMinAmount_,
        uint64 rate_
    ) public initializer {
        __TossExchangeBase_init(externalErc20_, externalMinAmount_, internalErc20_, internalMinAmount_, rate_);
    }
}
