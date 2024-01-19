// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

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
        market = DeployWithProxyUtil.tossMarketV1(IERC20(address(erc20)), initialCut);
        market.grantRole(market.ERC721_SELLER_ROLE(), address(erc721));
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
        new TossUpgradeableProxy(address(marketImp), abi.encodeCall(TossMarketV1.__TossMarketV1_init, (IERC20(address(erc20)), 10_333)));
        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "erc20"));
        new TossUpgradeableProxy(address(marketImp), abi.encodeCall(TossMarketV1.__TossMarketV1_init, (IERC20(address(0)), 33)));
    }

    function test_createSellOfferAndCancel(uint128 price) public {
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

    function test_createSellOfferAndCancelPauseUnpause(uint128 price) public {
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

    function test_createSellOfferAndBuy(uint128 price) public {
        price = uint128(bound(price, 2, mintAmount));
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

        market.buy(address(erc721), 0, price);
        assertEq(erc721.balanceOf(alice), 1);
    }

    function test_createSellOfferAndBuyWithPermit(uint128 price) public {
        price = uint128(bound(price, 1, mintAmount));
        erc721.safeMint(owner, 0);
        erc721.createSellOffer(0, price);
        erc20.transfer(alice, price);

        vm.startPrank(alice);
        SigUtils.Permit memory permit = SigUtils.Permit({ owner: alice, spender: address(market), value: price * 20, nonce: erc20.nonces(owner), deadline: 1 days });
        SigUtils sigUtils = new SigUtils(erc20.DOMAIN_SEPARATOR());
        bytes32 digest = sigUtils.getTypedDataHash(permit);
        (uint8 v, bytes32 r, bytes32 s) = vm.sign(alicePrivateKey, digest);

        market.buyWithPermit(address(erc721), 0, price, permit.value, permit.deadline, v, r, s);
        assertEq(erc721.balanceOf(alice), 1);
    }

    function test_createSellOfferAndBuyPauseUnpause(uint128 price) public {
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

    function test_withdrawBalance(uint256 amount) public {
        amount = bound(amount, 1, mintAmount);
        market.setErc20BankAddress(alice);
        erc20.transfer(address(market), amount);
        market.withdrawBalance(amount);
        assertEq(erc20.balanceOf(alice), amount);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, market.EXTRACT_ROLE()));
        market.withdrawBalance(amount);
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
