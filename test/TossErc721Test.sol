// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossErc721Test is BaseTest {
    function test_initializationGene() public {
        string memory name = "Erc 721 Gene";
        string memory symbol = "Erc 721 Symbol";
        TossErc721GeneV1 erc721 = DeployWithProxyUtil.tossErc721GeneV1(name, symbol);
        assertEq(erc721.name(), name);
        assertEq(erc721.symbol(), symbol);
    }

    function test_initializationGeneUnique() public {
        string memory name = "Erc 721 Unique";
        string memory symbol = "Erc 721 Symbol";
        TossErc721GeneUniqueV1 erc721 = DeployWithProxyUtil.tossErc721GeneUniqueV1(name, symbol);
        assertEq(erc721.name(), name);
        assertEq(erc721.symbol(), symbol);
    }

    function test_initializationMarket() public {
        string memory name = "Erc 721 Market";
        string memory symbol = "Erc 721 Symbol";
        TossErc721MarketV1 erc721 = DeployWithProxyUtil.tossErc721MarketV1(name, symbol);
        (name, symbol);
        assertEq(erc721.name(), name);
        assertEq(erc721.symbol(), symbol);
    }

    function test_setMarketFailWithInvalidMarket() public {
        TossErc721MarketV1 erc721 = DeployWithProxyUtil.tossErc721MarketV1("Erc721 Test", "E721T");
        address marketAddress = erc721.getMarket();
        vm.expectRevert(abi.encodeWithSelector(TossUnsupportedInterface.selector, "ITossMarket"));
        erc721.setMarket(ITossMarket(address(erc721)));
        assertEq(erc721.getMarket(), marketAddress);
    }

    function test_setMarketFailWithNonContractAddress() public {
        TossErc721MarketV1 erc721 = DeployWithProxyUtil.tossErc721MarketV1("Erc721 Test", "E721T");
        address marketAddress = erc721.getMarket();
        vm.expectRevert();
        erc721.setMarket(ITossMarket(address(1)));
        assertEq(erc721.getMarket(), marketAddress);
    }

    function test_setMarketWithZero() public {
        TossErc721MarketV1 erc721 = DeployWithProxyUtil.tossErc721MarketV1("Erc721 Test", "E721T");
        erc721.setMarket(ITossMarket(address(0)));
        assertEq(erc721.getMarket(), address(0));
    }

    function test_setMarketWithMarket() public {
        TossErc721MarketV1 erc721 = DeployWithProxyUtil.tossErc721MarketV1("Erc721 Test", "E721T");
        TossErc20V1 erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", 10 ether);
        TossMarketV1 market = DeployWithProxyUtil.tossMarketV1(IERC20(address(erc20)), 1);
        erc721.setMarket(ITossMarket(market));
    }
}
