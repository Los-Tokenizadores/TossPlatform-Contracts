// SPDX-License-Identifier: MIT
pragma solidity 0.8.23;

import { IERC721Receiver } from "@openzeppelin/contracts/token/ERC721/IERC721Receiver.sol";

interface ITossMarket is IERC721Receiver {
    function createSellOffer(uint256 tokenId, uint128 price, address owner) external;
}
