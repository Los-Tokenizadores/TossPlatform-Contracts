// SPDX-License-Identifier: MIT
pragma solidity ^0.8.19;

import "./Bases/TossExchangeBase.sol";

contract TossExchangeV1 is TossExchangeBase {
    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossExchangeV1_init(
        address externalErc20Address_,
        uint256 externalMinAmount_,
        address internalErc20Address_,
        uint256 internalMinAmount_,
        uint64 rate_
    ) public initializer {
        __TossExchangeBase_init(
            externalErc20Address_, externalMinAmount_, internalErc20Address_, internalMinAmount_, rate_
        );
    }
}
