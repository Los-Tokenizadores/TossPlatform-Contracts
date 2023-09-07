// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.19;

import { Test } from "forge-std/Test.sol";
import "./DeployWithProxyUtil.sol";

contract TossMarketTest is Test {
    address owner = makeAddr("owner");
    address alice = makeAddr("alice");
    address bob = makeAddr("bob");

    function setUp() public {
        vm.deal(owner, 1000 ether);
        vm.startPrank(owner);
    }

    function test_initialization(uint16 cut) public {
        vm.assume(cut <= 10_000);
        uint64 amount = 10;
        TossErc20V1 erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossMarketV1 market = DeployWithProxyUtil.tossMarketV1(IERC20(address(erc20)), cut);

        assertEq(address(market.erc20()), address(erc20));
    }
}
