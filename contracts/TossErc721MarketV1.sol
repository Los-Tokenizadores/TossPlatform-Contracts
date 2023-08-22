// SPDX-License-Identifier: MIT
pragma solidity ^0.8.19;

import "./Bases/TossErc721MarketBase.sol";

contract TossErc721MarketV1 is TossErc721MarketBase {
    event Created(address indexed account, uint256 indexed tokenId);

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossErc721MarketV1_init(string memory name_, string memory symbol_) initializer public {
        __TossErc721MarketBase_init(name_, symbol_);
    }
    
    function safeMint(address to, uint256 id) public onlyRole(MINTER_ROLE) {
        _safeMint(to, id);
        emit Created(to, id);
    }
}