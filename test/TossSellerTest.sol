// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossSellerTest is BaseTest {
    function test_initialization() public {
        uint64 amount = 10;
        TossErc20V1 erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossSellerV1 seller = DeployWithProxyUtil.tossSellerV1(IERC20(address(erc20)));

        assertEq(address(seller.erc20()), address(erc20));
    }
}
