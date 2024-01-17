// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossErc721MarketTest is BaseTest {
    TossErc721MarketV1 erc721;
    string constant name = "Erc 721 Gene";
    string constant symbol = "Erc 721 Symbol";

    function setUp() public override {
        super.setUp();
        erc721 = DeployWithProxyUtil.tossErc721MarketV1(name, symbol);
    }

    function test_upgrade() public {
        TossErc721MarketV1 erc721Init = new TossErc721MarketV1();
        assertNotEq(erc721.getImplementation(), address(erc721Init));
        erc721.upgradeToAndCall(address(erc721Init), "");
        assertEq(erc721.getImplementation(), address(erc721Init));
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

    function test_setBaseUri() public {
        assertEq(erc721.getBaseUri(), "");
        string memory baseUri = "test base uri";
        erc721.setBaseUri(baseUri);
        assertEq(erc721.getBaseUri(), baseUri);
    }

    function test_mint() public {
        assertEq(erc721.balanceOf(alice), 0);
        erc721.safeMint(alice, 1);
        assertEq(erc721.balanceOf(alice), 1);
    }

    function test_mintPauseUnpause() public {
        assertEq(erc721.balanceOf(alice), 0);
        erc721.safeMint(alice, 1);
        assertEq(erc721.balanceOf(alice), 1);
        erc721.pause();
        vm.expectRevert(PausableUpgradeable.EnforcedPause.selector);
        erc721.safeMint(alice, 2);
        erc721.unpause();
        erc721.safeMint(alice, 2);
        assertEq(erc721.balanceOf(alice), 2);
    }

    function test_transferPauseUnpause() public {
        assertEq(erc721.balanceOf(alice), 0);
        erc721.safeMint(owner, 1);
        erc721.safeMint(owner, 2);
        erc721.safeTransferFrom(owner, alice, 1);
        erc721.pause();
        assertEq(erc721.balanceOf(alice), 1);
        vm.expectRevert(PausableUpgradeable.EnforcedPause.selector);
        erc721.safeTransferFrom(owner, alice, 2);
        erc721.unpause();
        erc721.safeTransferFrom(owner, alice, 2);
        assertEq(erc721.balanceOf(alice), 2);
    }

    function test_transferWhitelist() public {
        erc721.setWhitelist(address(whitelist));
        whitelist.set(owner, true);
        assertEq(erc721.balanceOf(alice), 0);
        erc721.safeMint(owner, 1);
        vm.expectRevert(abi.encodeWithSelector(TossWhitelistClient.TossWhitelistNotInWhitelist.selector, alice));
        erc721.safeTransferFrom(owner, alice, 1);
        assertEq(erc721.balanceOf(alice), 0);

        whitelist.set(alice, true);
        erc721.safeTransferFrom(owner, alice, 1);
        assertEq(erc721.balanceOf(alice), 1);
    }

    function test_createSellOfferWithoutMarketRevert() public {
        erc721.safeMint(owner, 1);
        vm.expectRevert(TossErc721MarketBase.TossErc721MarketNotSet.selector);
        erc721.createSellOffer(1, 1 ether);
    }

    function test_createSellOffer() public {
        TossErc20V1 erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", 10 ether);
        TossMarketV1 market = DeployWithProxyUtil.tossMarketV1(IERC20(address(erc20)), 1);
        erc721.setMarket(ITossMarket(market));
        market.grantRole(market.ERC721_SELLER_ROLE(), address(erc721));
        erc721.safeMint(owner, 1);
        erc721.createSellOffer(1, 1 ether);
    }

    function test_createSellOfferWithoutRoleRevert() public {
        TossErc20V1 erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", 10 ether);
        TossMarketV1 market = DeployWithProxyUtil.tossMarketV1(IERC20(address(erc20)), 1);
        erc721.setMarket(ITossMarket(market));
        erc721.safeMint(owner, 1);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, address(erc721), market.ERC721_SELLER_ROLE()));
        erc721.createSellOffer(1, 1 ether);
    }
}
