// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import { Test } from "forge-std/Test.sol";
import "./DeployWithProxyUtil.sol";

abstract contract BaseTest is Test {
    address internal owner = makeAddr("owner");
    address internal alice = makeAddr("alice");
    address internal bob = makeAddr("bob");

    function setUp() public virtual {
        vm.deal(owner, 1000 ether);
        vm.startPrank(owner);
    }
}
