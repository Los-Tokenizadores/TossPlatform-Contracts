// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossMarketTest is BaseTest {
    function test_initialization(uint16 cut) public {
        vm.assume(cut <= 10_000);
        uint64 amount = 10;
        TossErc20V1 erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossMarketV1 market = DeployWithProxyUtil.tossMarketV1(IERC20(address(erc20)), cut);

        assertEq(address(market.erc20()), address(erc20));
    }
}
