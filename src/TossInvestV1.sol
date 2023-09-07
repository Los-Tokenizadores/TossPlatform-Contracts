// SPDX-License-Identifier: MIT
pragma solidity ^0.8.19;

import "./Bases/TossInvestBase.sol";

contract TossInvestV1 is TossInvestBase {
    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossInvestV1_init(
        IERC20 erc20_,
        TossErc721MarketV1 erc721Implementation_,
        address platformAddress,
        uint16 platformCut,
        string memory erc721baseUri
    ) public initializer {
        __TossInvestBase_init(erc20_, erc721Implementation_, platformAddress, platformCut, erc721baseUri);
    }
}
