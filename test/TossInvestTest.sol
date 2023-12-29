// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossInvestTest is BaseTest {
    function test_initialization() public {
        uint256 amount = 10 ether;
        string memory uri = "uritest";
        TossErc721MarketV1 erc721Implementation = new TossErc721MarketV1();
        TossErc20V1 erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossInvestV1 invest = DeployWithProxyUtil.tossInvestV1(IERC20(address(erc20)), erc721Implementation, alice, 1000, uri);

        assertEq(address(invest.getErc20()), address(erc20));
        assertEq(address(invest.getErc721Implementation()), address(erc721Implementation));
        assertEq(invest.getErc721BaseUri(), uri);
    }
}
