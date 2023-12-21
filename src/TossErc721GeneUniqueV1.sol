// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { TossErc721GeneBase } from "./Bases/TossErc721GeneBase.sol";

contract TossErc721GeneUniqueV1 is TossErc721GeneBase {
    mapping(uint256 => uint256) private uniqueGene;

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossErc721GeneUniqueV1_init(string memory name_, string memory symbol_) public initializer {
        __TossErc721GeneBase_init(name_, symbol_);
    }

    function addGenes(uint256[] memory genes) external override onlyRole(MINTER_ROLE) {
        TossErc721GeneBaseStorage storage $ = _getTossErc721GeneBaseStorage();

        for (uint256 i; i < genes.length;) {
            if (uniqueGene[genes[i]] == 0) {
                uniqueGene[genes[i]] = 1;
                $.rangeOfGene.push(genes[i]);
            }
            unchecked {
                ++i;
            }
        }
    }
}
