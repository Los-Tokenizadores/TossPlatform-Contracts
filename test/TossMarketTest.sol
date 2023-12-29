// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossMarketTest is BaseTest {
    function test_initialization(uint16 cut) public {
        vm.assume(cut <= 10_000);
        uint256 amount = 10 ether;
        TossErc20V1 erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossMarketV1 market = DeployWithProxyUtil.tossMarketV1(IERC20(address(erc20)), cut);

        assertEq(address(market.getErc20()), address(erc20));
    }

    function test_sellErc721(uint16 cut) public {
        vm.assume(cut <= 10_000);
        uint256 amount = 10 ether;
        TossErc20V1 erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossMarketV1 market = DeployWithProxyUtil.tossMarketV1(IERC20(address(erc20)), cut);
        TossErc721MarketV1 erc721 = DeployWithProxyUtil.tossErc721MarketV1("Erc721 Test", "E721T");

        market.grantRole(market.ERC721_SELLER_ROLE(), address(erc721));
        erc721.grantRole(erc721.MINTER_ROLE(), owner);
        erc721.safeMint(owner, 0);
        erc721.setMarket(market);

        erc721.createSellOffer(0, 1 ether);
        (, uint256 price,) = market.get(address(erc721), 0);
        assertEq(1 ether, price);
    }
}
