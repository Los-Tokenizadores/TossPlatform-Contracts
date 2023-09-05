// SPDX-License-Identifier: MIT
pragma solidity ^0.8.17;

interface ITossWhitelist {
    function isInWhitelist(address user) external view returns (bool);
}
