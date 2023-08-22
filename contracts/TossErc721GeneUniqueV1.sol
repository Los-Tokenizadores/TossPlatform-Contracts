// SPDX-License-Identifier: MIT
pragma solidity ^0.8.19;

import "./Bases/TossErc721GeneBase.sol";

contract TossErc721GeneUniqueV1 is TossErc721GeneBase {
    mapping(uint256 => uint256) private uniqueGene;

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossErc721GeneUniqueV1_init(string memory name_, string memory symbol_) initializer public {
        __TossErc721GeneBase_init(name_, symbol_);
    }

    function addGenes(uint256[] memory genes) override external onlyRole(MINTER_ROLE) {
        uint256 i = 0;
        for(; i < genes.length;) {
            if(uniqueGene[genes[i]] == 0) {
                uniqueGene[genes[i]] = 1;
                rangeOfGene.push(genes[i]);
            }
            unchecked { ++i; }
        }
    }
}