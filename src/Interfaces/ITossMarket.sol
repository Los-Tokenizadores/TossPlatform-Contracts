// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { IERC721Receiver } from "@openzeppelin/contracts/token/ERC721/IERC721Receiver.sol";
import { IERC165 } from "@openzeppelin/contracts/utils/introspection/IERC165.sol";

interface ITossMarket is IERC721Receiver, IERC165 {
    function createSellOffer(uint256 tokenId, uint128 price, address owner) external;
}
