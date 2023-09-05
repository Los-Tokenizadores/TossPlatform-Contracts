// SPDX-License-Identifier: MIT
pragma solidity ^0.8.17;

interface ITossExchange {
    function convertToInternal(uint128 amount) external;
    function convertToExternal(uint128 amount) external;
}