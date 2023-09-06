// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.19;

import {Test} from "forge-std/Test.sol";
import "./DeployWithProxyUtil.sol";

contract TossWhitelistTest is Test {
    address owner = makeAddr("owner");
    address alice = makeAddr("alice");
    address bob = makeAddr("bob");

    function setUp() public {
        vm.deal(owner, 1000 ether);
        vm.startPrank(owner);
    }

    function test_initialization() public {
        TossWhitelistV1 whitelist = DeployWithProxyUtil.tossWhitelistV1();
        whitelist.set(alice, true);
        assertTrue(whitelist.isInWhitelist(alice));
        whitelist.set(alice, false);
        assertFalse(whitelist.isInWhitelist(alice));
    }
}
