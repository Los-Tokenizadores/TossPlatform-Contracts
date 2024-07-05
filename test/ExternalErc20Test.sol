// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";
import { ExternalErc20 } from "../src/ExernalErc20.sol";

contract ExternalErc20Test is BaseTest {
    function test_construct() public {
        new ExternalErc20();
    }
}
