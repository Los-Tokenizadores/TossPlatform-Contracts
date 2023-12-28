// SPDX-License-Identifier: MIT
pragma solidity 0.8.23;

import { TossErc721GeneUniqueV1 } from "../TossErc721GeneUniqueV1.sol";

contract TossErc721GeneUniqueDevV1 is TossErc721GeneUniqueV1 {
    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossErc721GeneUniqueDevV1_init(string memory name_, string memory symbol_) public initializer {
        __TossErc721GeneUniqueV1_init(name_, symbol_);
    }

    function adminTransfer(address to, uint256 tokenId) external onlyRole(DEFAULT_ADMIN_ROLE) {
        address from = ownerOf(tokenId);
        _transfer(from, to, tokenId);
    }
}
