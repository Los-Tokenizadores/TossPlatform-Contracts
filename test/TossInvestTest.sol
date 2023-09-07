// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.19;

import { Test } from "forge-std/Test.sol";
import "./DeployWithProxyUtil.sol";

contract TossInvestTest is Test {
    address owner = makeAddr("owner");
    address alice = makeAddr("alice");
    address bob = makeAddr("bob");

    function setUp() public {
        vm.deal(owner, 1000 ether);
        vm.startPrank(owner);
    }

    function test_initialization() public {
        uint64 amount = 10;
        string memory uri = "uritest";
        TossErc721MarketV1 erc721Implementation = new TossErc721MarketV1();
        TossErc20V1 erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossInvestV1 invest = DeployWithProxyUtil.tossInvestV1(IERC20(address(erc20)), erc721Implementation, alice, 1000, uri);

        assertEq(address(invest.erc20()), address(erc20));
        assertEq(address(invest.getErc721Implementation()), address(erc721Implementation));
        assertEq(invest.erc721baseUri(), uri);
    }
}
