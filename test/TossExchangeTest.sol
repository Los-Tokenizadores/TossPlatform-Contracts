// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossExchangeTest is BaseTest {
    function test_initialization(uint256 externalMinAmount, uint256 internalMinAmount, uint64 rate) public {
        vm.assume(externalMinAmount > 0);
        vm.assume(internalMinAmount > 0);
        vm.assume(rate > 0);

        uint64 amount = 10;
        TossErc20V1 externalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossErc20V1 internalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossExchangeV1 exchange = DeployWithProxyUtil.tossExchangeV1(IERC20(address(externalErc20)), externalMinAmount, internalErc20, internalMinAmount, rate);

        assertEq(address(exchange.externalErc20()), address(externalErc20));
        assertEq(address(exchange.internalErc20()), address(internalErc20));
    }

    function test_initializationTier(uint256 externalMinAmount, uint256 internalMinAmount, uint64 rate) public {
        vm.assume(externalMinAmount > 0);
        vm.assume(internalMinAmount > 0);
        vm.assume(rate > 0);
        uint64 year = 2023;
        uint64 amount = 10;
        TossErc20V1 externalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossErc20V1 internalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossExchangeTierV1 exchange = DeployWithProxyUtil.tossExchangeTierV1(IERC20(address(externalErc20)), externalMinAmount, internalErc20, internalMinAmount, rate, year);

        assertEq(address(exchange.externalErc20()), address(externalErc20));
        assertEq(address(exchange.internalErc20()), address(internalErc20));
    }
}
