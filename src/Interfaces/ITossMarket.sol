// SPDX-License-Identifier: MIT
pragma solidity ^0.8.17;

import "@openzeppelin/contracts-upgradeable/token/ERC721/IERC721ReceiverUpgradeable.sol";

interface ITossMarket is IERC721ReceiverUpgradeable {
    function create(uint256 tokenId, uint128 price, address owner) external;
}
