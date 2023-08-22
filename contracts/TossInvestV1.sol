// SPDX-License-Identifier: MIT
pragma solidity ^0.8.19;

import "./Bases/TossInvestBase.sol";

contract TossInvestV1 is TossInvestBase {
    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossInvestV1_init(address erc20Address, address erc721ImplementationAddress, address platformAddress, uint16 platformCut, string memory erc721baseUri) initializer public {
        __TossInvestBase_init(erc20Address, erc721ImplementationAddress, platformAddress, platformCut, erc721baseUri);
    }
}