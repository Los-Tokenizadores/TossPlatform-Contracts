// SPDX-License-Identifier: MIT
pragma solidity 0.8.23;

import { TossErc721GeneBase } from "./Bases/TossErc721GeneBase.sol";

contract TossErc721GeneV1 is TossErc721GeneBase {
    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossErc721GeneV1_init(string memory name_, string memory symbol_) public initializer {
        __TossErc721GeneBase_init(name_, symbol_);
    }
}
