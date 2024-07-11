// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";
import { Royalty } from "../src/Bases/TossMarketBase.sol";

contract TossMarketTest is BaseTest {
    TossErc20V1 erc20;
    TossErc721MarketV1 erc721;
    TossMarketV1 market;
    uint16 initialCut = 10;
    uint256 mintAmount = 100 ether;

    function setUp() public override {
        super.setUp();
        erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", mintAmount);
        erc721 = DeployWithProxyUtil.tossErc721MarketV1("Erc721 Test", "E721T");
        market = DeployWithProxyUtil.tossMarketV1(IERC20(address(erc20)), initialCut, bob);
        erc721.setMarket(market);
    }

    function test_upgrade() public {
        TossMarketV1 marketInit = new TossMarketV1();
        assertNotEq(market.getImplementation(), address(marketInit));
        market.upgradeToAndCall(address(marketInit), "");
        assertEq(market.getImplementation(), address(marketInit));
        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, market.UPGRADER_ROLE()));
        market.upgradeToAndCall(address(marketInit), "");
    }

    function test_initializationAndRevert() public {
        assertEq(address(market.getErc20()), address(erc20));
        assertEq(market.getMarketCut(), initialCut);

        TossMarketV1 marketImp = new TossMarketV1();
        vm.expectRevert(abi.encodeWithSelector(TossCutOutOfRange.selector, 10_333));
        new TossUpgradeableProxy(address(marketImp), abi.encodeCall(TossMarketV1.__TossMarketV1_init, (IERC20(address(erc20)), 10_333, bob)));
        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "erc20"));
        new TossUpgradeableProxy(address(marketImp), abi.encodeCall(TossMarketV1.__TossMarketV1_init, (IERC20(address(0)), 33, bob)));
        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "bank"));
        new TossUpgradeableProxy(address(marketImp), abi.encodeCall(TossMarketV1.__TossMarketV1_init, (IERC20(address(erc20)), 33, address(0))));
    }

    function test_supportInterface() public view {
        assertTrue(market.supportsInterface(type(IAccessControl).interfaceId));
    }

    function test_createSellOfferNotActiveRevert() public {
        erc721.safeMint(owner, 0);
        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketErc721NotActive.selector, address(erc721)));
        erc721.createSellOffer(0, 1);
    }

    function test_addRemoveMarketAndChangeRoyalties() public {
        address erc721Address = address(erc721);
        market.addErc721Market(erc721Address, new Royalty[](0));
        (bool active, Royalty[] memory royalty) = market.getErc721Market(erc721Address);
        assertEq(active, true);
        assertEq(royalty.length, 0);
        market.removeErc721Market(erc721Address);
        (active, royalty) = market.getErc721Market(erc721Address);
        assertEq(active, false);

        Royalty[] memory royalties1 = new Royalty[](1);
        royalties1[0] = Royalty({ cut: 1, destination: bob });
        market.addErc721Market(erc721Address, royalties1);
        (active, royalty) = market.getErc721Market(erc721Address);
        assertEq(active, true);
        assertEq(royalty.length, 1);
        assertEq(royalty[0].cut, 1);
        assertEq(royalty[0].destination, bob);
        market.removeErc721Market(erc721Address);

        Royalty[] memory royalties6 = new Royalty[](6);
        royalties6[0] = Royalty({ cut: 1, destination: bob });
        royalties6[1] = Royalty({ cut: 10, destination: alice });
        royalties6[2] = Royalty({ cut: 100, destination: bob });
        royalties6[3] = Royalty({ cut: 1000, destination: bob });
        royalties6[4] = Royalty({ cut: 3000, destination: owner });
        royalties6[5] = Royalty({ cut: 5000, destination: bob });
        market.addErc721Market(erc721Address, royalties6);
        (active, royalty) = market.getErc721Market(erc721Address);
        for (uint256 i = 0; i < royalties6.length; i++) {
            assertEq(royalty[i].cut, royalties6[i].cut);
            assertEq(royalty[i].destination, royalties6[i].destination);
        }
        market.removeErc721Market(erc721Address);

        Royalty[] memory royalties10 = new Royalty[](10);
        royalties10[0] = Royalty({ cut: 100, destination: bob });
        royalties10[1] = Royalty({ cut: 1001, destination: alice });
        royalties10[2] = Royalty({ cut: 502, destination: bob });
        royalties10[3] = Royalty({ cut: 3, destination: bob });
        royalties10[4] = Royalty({ cut: 4, destination: bob });
        royalties10[5] = Royalty({ cut: 1005, destination: alice });
        royalties10[6] = Royalty({ cut: 1006, destination: owner });
        royalties10[7] = Royalty({ cut: 1007, destination: makeAddr("zaraza") });
        royalties10[8] = Royalty({ cut: 2008, destination: bob });
        royalties10[9] = Royalty({ cut: 3009, destination: bob });
        market.addErc721Market(erc721Address, royalties10);
        (active, royalty) = market.getErc721Market(erc721Address);
        for (uint256 i = 0; i < royalties10.length; i++) {
            assertEq(royalty[i].cut, royalties10[i].cut);
            assertEq(royalty[i].destination, royalties10[i].destination);
        }
    }

    function test_changeMarketCutExcededTotalMaxCutNotRevert() public {
        market.setMarketCut(5000);

        address erc721Address = address(erc721);
        Royalty[] memory royalties6 = new Royalty[](6);
        royalties6[0] = Royalty({ cut: 1, destination: bob });
        royalties6[1] = Royalty({ cut: 10, destination: alice });
        royalties6[2] = Royalty({ cut: 100, destination: bob });
        royalties6[3] = Royalty({ cut: 1000, destination: bob });
        royalties6[4] = Royalty({ cut: 3000, destination: owner });
        royalties6[5] = Royalty({ cut: 5000, destination: bob });

        vm.expectRevert(abi.encodeWithSelector(TossCutOutOfRange.selector, 5000 + 3000 + 1000 + 100 + 10 + 1 + 5000));
        market.addErc721Market(erc721Address, royalties6);

        market.setMarketCut(0);
        market.addErc721Market(erc721Address, royalties6);

        market.setMarketCut(5000);
    }

    function test_addMarketRevertOnInvalidParameters() public {
        uint8 maxRoyalty = market.MAX_ROYALTY_LENGTH();
        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketRoyaltyLengthOutOfRange.selector, maxRoyalty, maxRoyalty + 1));
        market.addErc721Market(address(erc721), new Royalty[](maxRoyalty + 1));

        market.addErc721Market(address(erc721), new Royalty[](0));
        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketErc721AlreadyActive.selector, address(erc721)));
        market.addErc721Market(address(erc721), new Royalty[](0));

        Royalty[] memory royalties1 = new Royalty[](1);
        royalties1[0] = Royalty({ cut: 100, destination: bob });
        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketErc721AlreadyActive.selector, address(erc721)));
        market.addErc721Market(address(erc721), royalties1);

        market.removeErc721Market(address(erc721));
        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketErc721NotActive.selector, address(erc721)));
        market.removeErc721Market(address(erc721));

        royalties1[0].cut = 0;
        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketRoyaltyCutOutOfRange.selector, 0, bob));
        market.addErc721Market(address(erc721), royalties1);

        royalties1[0].cut = 10_001;
        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketRoyaltyCutOutOfRange.selector, 10_001, bob));
        market.addErc721Market(address(erc721), royalties1);

        royalties1[0].cut = 10_000;
        market.setMarketCut(1);
        vm.expectRevert(abi.encodeWithSelector(TossCutOutOfRange.selector, 10_001));
        market.addErc721Market(address(erc721), royalties1);

        royalties1[0].destination = address(0);
        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "Royalty Destination"));
        market.addErc721Market(address(erc721), royalties1);

        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "erc721"));
        market.addErc721Market(address(0), royalties1);

        vm.expectRevert(abi.encodeWithSelector(TossUnsupportedInterface.selector, "ITossErc721Market"));
        market.addErc721Market(address(market), royalties1);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, market.DEFAULT_ADMIN_ROLE()));
        market.addErc721Market(address(erc721), royalties1);
    }

    function test_createSellOfferAndCancel(uint128 price) public {
        market.addErc721Market(address(erc721), new Royalty[](0));
        erc721.safeMint(owner, 0);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IERC721Errors.ERC721InvalidApprover.selector, alice));
        erc721.createSellOffer(0, price);

        vm.startPrank(owner);
        erc721.createSellOffer(0, price);
        (, uint128 marketPrice,) = market.get(address(erc721), 0);
        assertEq(price, marketPrice);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketNotOwnerOfErc721.selector, alice, owner));
        market.cancel(address(erc721), 0);

        vm.startPrank(owner);
        market.cancel(address(erc721), 0);

        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketErc721NotOnSell.selector, address(erc721), 0));
        market.get(address(erc721), 0);
    }

    function test_createSellOfferWithoutErc721MarketRevert(uint128 price) public {
        erc721.safeMint(owner, 0);

        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketErc721NotActive.selector, address(erc721)));
        erc721.createSellOffer(0, price);
    }

    function test_createSellOfferRemoveErc721MarketAndCancel(uint128 price) public {
        market.addErc721Market(address(erc721), new Royalty[](0));
        erc721.safeMint(owner, 0);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IERC721Errors.ERC721InvalidApprover.selector, alice));
        erc721.createSellOffer(0, price);

        vm.startPrank(owner);
        erc721.createSellOffer(0, price);
        (, uint128 marketPrice,) = market.get(address(erc721), 0);
        assertEq(price, marketPrice);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketNotOwnerOfErc721.selector, alice, owner));
        market.cancel(address(erc721), 0);

        vm.startPrank(owner);
        market.removeErc721Market(address(erc721));
        market.cancel(address(erc721), 0);
    }

    function test_createSellOfferAndCancelPauseUnpause(uint128 price) public {
        market.addErc721Market(address(erc721), new Royalty[](0));
        erc721.safeMint(owner, 0);
        market.pause();
        vm.expectRevert(abi.encodeWithSelector(PausableUpgradeable.EnforcedPause.selector));
        erc721.createSellOffer(0, price);
        market.unpause();
        erc721.createSellOffer(0, price);
        (, uint128 marketPrice,) = market.get(address(erc721), 0);
        assertEq(price, marketPrice);
        assertEq(price, market.getPrice(address(erc721), 0));
        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketNotOwnerOfErc721.selector, alice, owner));
        market.cancel(address(erc721), 0);

        vm.startPrank(owner);
        market.pause();
        vm.expectRevert(abi.encodeWithSelector(PausableUpgradeable.EnforcedPause.selector));
        market.cancel(address(erc721), 0);
        market.unpause();
        market.cancel(address(erc721), 0);

        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketErc721NotOnSell.selector, address(erc721), 0));
        market.get(address(erc721), 0);

        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketErc721NotOnSell.selector, address(erc721), 0));
        market.getPrice(address(erc721), 0);

        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketErc721NotOnSell.selector, address(erc721), 0));
        market.cancel(address(erc721), 0);

        erc721.createSellOffer(0, price);
        market.pause();
        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, market.DEFAULT_ADMIN_ROLE()));
        market.cancelWhenPaused(address(erc721), 0);

        vm.startPrank(owner);
        market.cancelWhenPaused(address(erc721), 0);
    }

    function test_createSellOfferAndBuyWithRoyalties1(uint128 price) public {
        Royalty[] memory royalties = new Royalty[](1);
        address royalty = makeAddr("royalty");
        royalties[0] = Royalty({ cut: 33, destination: royalty });
        market.addErc721Market(address(erc721), royalties);
        price = uint128(bound(price, 10_000, mintAmount));
        market.setErc20BankAddress(bob);
        erc721.safeMint(owner, 0);
        erc721.createSellOffer(0, price);
        erc20.transfer(alice, price);

        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketIsOwnerOfErc721.selector, owner));
        market.buy(address(erc721), 0, price);
        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketErc721NotOnSell.selector, address(erc721), 1));
        market.buy(address(erc721), 1, price);

        uint256 ownerBalance = erc20.balanceOf(owner);
        vm.startPrank(alice);
        erc20.approve(address(market), price);
        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketSellPriceChange.selector, price, 1));
        market.buy(address(erc721), 0, 1);

        market.buy(address(erc721), 0, price);
        uint256 bankCut = price * initialCut / 10_000;
        uint256 royaltyCut = price * royalties[0].cut / 10_000;
        assertEq(erc721.balanceOf(alice), 1);
        assertEq(erc20.balanceOf(bob), bankCut);
        assertEq(erc20.balanceOf(royalty), royaltyCut);
        assertEq(erc20.balanceOf(alice), 0);
        assertEq(erc20.balanceOf(owner), ownerBalance + price - bankCut - royaltyCut);
    }

    function test_createSellOfferAndBuyWithRoyalties3(uint128 price) public {
        Royalty[] memory royalties = new Royalty[](3);
        address royalty1 = makeAddr("royalty1");
        address royalty2 = makeAddr("royalty2");
        address royalty3 = makeAddr("royalty3");
        royalties[0] = Royalty({ cut: 7, destination: royalty1 });
        royalties[1] = Royalty({ cut: 333, destination: royalty2 });
        royalties[2] = Royalty({ cut: 987, destination: royalty3 });
        market.addErc721Market(address(erc721), royalties);
        price = uint128(bound(price, 10_000, mintAmount));
        market.setErc20BankAddress(bob);
        erc721.safeMint(owner, 0);
        erc721.createSellOffer(0, price);
        erc20.transfer(alice, price);

        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketIsOwnerOfErc721.selector, owner));
        market.buy(address(erc721), 0, price);
        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketErc721NotOnSell.selector, address(erc721), 1));
        market.buy(address(erc721), 1, price);

        uint256 ownerBalance = erc20.balanceOf(owner);
        vm.startPrank(alice);
        erc20.approve(address(market), price);
        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketSellPriceChange.selector, price, 1));
        market.buy(address(erc721), 0, 1);

        market.buy(address(erc721), 0, price);
        uint256 bankCut = price * initialCut / 10_000;
        uint256 royaltyCut1 = price * royalties[0].cut / 10_000;
        uint256 royaltyCut2 = price * royalties[1].cut / 10_000;
        uint256 royaltyCut3 = price * royalties[2].cut / 10_000;
        assertEq(erc721.balanceOf(alice), 1);
        assertEq(erc20.balanceOf(bob), bankCut);
        assertEq(erc20.balanceOf(royalty1), royaltyCut1);
        assertEq(erc20.balanceOf(royalty2), royaltyCut2);
        assertEq(erc20.balanceOf(royalty3), royaltyCut3);
        assertEq(erc20.balanceOf(alice), 0);
        assertEq(erc20.balanceOf(owner), ownerBalance + price - bankCut - royaltyCut1 - royaltyCut2 - royaltyCut3);
    }

    function test_createSellOfferAndBuyWithRoyaltiesAndCutExceded(uint128 price) public {
        Royalty[] memory royalties = new Royalty[](1);
        address royalty = makeAddr("royalty");
        royalties[0] = Royalty({ cut: 5000, destination: royalty });
        market.addErc721Market(address(erc721), royalties);
        price = uint128(bound(price, 10_000, mintAmount));
        market.setErc20BankAddress(bob);
        erc721.safeMint(owner, 0);
        erc721.createSellOffer(0, price);
        erc20.transfer(alice, price);

        market.setMarketCut(5001);

        vm.startPrank(alice);
        erc20.approve(address(market), price);
        vm.expectRevert(abi.encodeWithSelector(TossCutOutOfRange.selector, 10_001));
        market.buy(address(erc721), 0, price);
    }

    function test_createSellOfferAndBuy(uint128 price) public {
        market.addErc721Market(address(erc721), new Royalty[](0));
        price = uint128(bound(price, 2, mintAmount));
        market.setErc20BankAddress(bob);
        erc721.safeMint(owner, 0);
        erc721.createSellOffer(0, price);
        erc20.transfer(alice, price);

        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketIsOwnerOfErc721.selector, owner));
        market.buy(address(erc721), 0, price);
        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketErc721NotOnSell.selector, address(erc721), 1));
        market.buy(address(erc721), 1, price);

        vm.startPrank(alice);
        erc20.approve(address(market), price);
        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketSellPriceChange.selector, price, 1));
        market.buy(address(erc721), 0, 1);

        uint256 bankBalance = erc20.balanceOf(bob);
        market.buy(address(erc721), 0, price);
        uint256 bankCut = price * initialCut / 10_000;
        assertEq(erc721.balanceOf(alice), 1);
        assertEq(erc20.balanceOf(bob), bankBalance + bankCut);
    }

    function test_createSellOfferAndBuyWhenNotActiveRevertAndCanCancel(uint128 price) public {
        market.addErc721Market(address(erc721), new Royalty[](0));
        price = uint128(bound(price, 2, mintAmount));
        market.setErc20BankAddress(bob);
        erc721.safeMint(owner, 0);
        erc721.createSellOffer(0, price);
        erc20.transfer(alice, price);
        market.removeErc721Market(address(erc721));

        vm.startPrank(alice);
        erc20.approve(address(market), price);
        vm.expectRevert(abi.encodeWithSelector(TossMarketBase.TossMarketErc721NotActive.selector, address(erc721)));
        market.buy(address(erc721), 0, price);

        vm.startPrank(owner);
        market.cancel(address(erc721), 0);
    }

    function test_createSellOfferAndBuyWithPermit(uint128 price) public {
        market.addErc721Market(address(erc721), new Royalty[](0));
        price = uint128(bound(price, 1, mintAmount));
        erc721.safeMint(owner, 0);
        erc721.createSellOffer(0, price);
        erc20.transfer(alice, price);

        vm.startPrank(alice);
        SigUtils.Permit memory permit = SigUtils.signPermit(alice, alicePrivateKey, address(market), price * 20, 1 days, erc20);
        market.buyWithPermit(address(erc721), 0, price, permit.value, permit.deadline, permit.v, permit.r, permit.s);
        assertEq(erc721.balanceOf(alice), 1);
    }

    function test_createSellOfferAndBuyPauseUnpause(uint128 price) public {
        market.addErc721Market(address(erc721), new Royalty[](0));
        price = uint128(bound(price, 1, mintAmount));
        erc721.safeMint(alice, 0);
        vm.startPrank(alice);
        erc721.createSellOffer(0, price);
        vm.startPrank(owner);
        market.pause();
        erc20.approve(address(market), price);
        vm.expectRevert(abi.encodeWithSelector(PausableUpgradeable.EnforcedPause.selector));
        market.buy(address(erc721), 0, price);
        market.unpause();
        market.buy(address(erc721), 0, price);
        assertEq(erc721.balanceOf(owner), 1);
    }

    function test_createSellOfferAndBuyAndCancelWhitelist() public {
        market.addErc721Market(address(erc721), new Royalty[](0));
        whitelist.grantRole(whitelist.DEFAULT_ADMIN_ROLE(), alice);
        market.setWhitelist(address(whitelist));
        uint128 price = 1;
        erc721.safeMint(alice, 0);
        erc20.transfer(alice, 1);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(TossWhitelistClient.TossWhitelistNotInWhitelist.selector, alice));
        erc721.createSellOffer(0, price);
        whitelist.set(alice, true);
        erc721.createSellOffer(0, price);

        vm.startPrank(owner);
        erc20.approve(address(market), price);
        vm.expectRevert(abi.encodeWithSelector(TossWhitelistClient.TossWhitelistNotInWhitelist.selector, owner));
        market.buy(address(erc721), 0, price);
        whitelist.set(owner, true);
        market.buy(address(erc721), 0, price);
        assertEq(erc721.balanceOf(owner), 1);

        erc721.createSellOffer(0, price);
        whitelist.set(owner, false);
        market.cancel(address(erc721), 0);
    }

    function test_setBank() public {
        market.setErc20BankAddress(alice);
        assertEq(alice, market.getErc20BankAddress());

        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "bank"));
        market.setErc20BankAddress(address(0));

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, market.DEFAULT_ADMIN_ROLE()));
        market.setErc20BankAddress(alice);
    }

    function test_setMarketCut(uint16 newCut, uint16 newMaxCut) public {
        uint16 maxCut = market.CUT_PRECISION();
        newMaxCut = uint16(bound(newCut, maxCut + 1, type(uint16).max));
        vm.expectRevert(abi.encodeWithSelector(TossCutOutOfRange.selector, newMaxCut));
        market.setMarketCut(newMaxCut);

        newCut = uint16(bound(newCut, 0, maxCut));
        market.setMarketCut(newCut);
        assertEq(market.getMarketCut(), newCut);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, market.DEFAULT_ADMIN_ROLE()));
        market.setMarketCut(0);

        assertEq(market.getMarketCut(), newCut);
    }
}
