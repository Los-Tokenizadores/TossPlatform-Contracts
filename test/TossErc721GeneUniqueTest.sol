// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossErc721Test is BaseTest {
    TossErc721GeneUniqueV1 erc721;
    string constant name = "Erc 721 Gene";
    string constant symbol = "Erc 721 Symbol";

    function setUp() public override {
        super.setUp();
        erc721 = DeployWithProxyUtil.tossErc721GeneUniqueV1(name, symbol);
    }

    function test_initializationNameAndSymbol() public {
        assertEq(erc721.name(), name);
        assertEq(erc721.symbol(), symbol);
    }

    function test_setMarketFailWithInvalidMarket() public {
        address marketAddress = erc721.getMarket();
        vm.expectRevert(abi.encodeWithSelector(TossUnsupportedInterface.selector, "ITossMarket"));
        erc721.setMarket(ITossMarket(address(erc721)));
        assertEq(erc721.getMarket(), marketAddress);
    }

    function test_setMarketFailWithNonContractAddress() public {
        address marketAddress = erc721.getMarket();
        vm.expectRevert();
        erc721.setMarket(ITossMarket(address(1)));
        assertEq(erc721.getMarket(), marketAddress);
    }

    function test_setMarketWithZero() public {
        erc721.setMarket(ITossMarket(address(0)));
        assertEq(erc721.getMarket(), address(0));
    }

    function test_setMarketWithMarket() public {
        TossErc20V1 erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", 10 ether);
        TossMarketV1 market = DeployWithProxyUtil.tossMarketV1(IERC20(address(erc20)), 1);
        erc721.setMarket(ITossMarket(market));
    }
}
