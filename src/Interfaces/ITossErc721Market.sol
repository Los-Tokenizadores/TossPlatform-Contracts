// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { ITossMarket } from "./ITossMarket.sol";

interface ITossErc721Market {
    function createSellOffer(uint256 tokenId, uint128 price) external;
    function getMarket() external view returns (address marketAddress);
    function setMarket(ITossMarket market) external;
}
