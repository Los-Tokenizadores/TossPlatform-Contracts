// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossExchangeTierTest is BaseTest {
    TossErc20V1 private externalErc20;
    TossErc20V1 private internalErc20;
    TossExchangeTierV1 private exchange;
    uint256 private mintAmount = 99_999 ether;
    uint128 constant depositMinAmount = 1;
    uint128 constant withdrawMinAmount = 1;

    function setUp() public override {
        super.setUp();
        externalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", mintAmount);
        internalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", 0);
        exchange = DeployWithProxyUtil.tossExchangeTierV1(IERC20(address(externalErc20)), depositMinAmount, internalErc20, withdrawMinAmount, 2024);
        internalErc20.grantRole(internalErc20.MINTER_ROLE(), address(exchange));
    }

    function test_upgrade() public {
        TossExchangeTierV1 exchangeImp = new TossExchangeTierV1();
        assertNotEq(exchange.getImplementation(), address(exchangeImp));
        exchange.upgradeToAndCall(address(exchangeImp), "");
        assertEq(exchange.getImplementation(), address(exchangeImp));
        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, exchange.UPGRADER_ROLE()));
        exchange.upgradeToAndCall(address(exchangeImp), "");
    }

    function test_initialization(uint128 depositMinAmount_, uint128 withdrawMinAmount_, uint64 year) public {
        vm.assume(depositMinAmount_ > 0);
        vm.assume(withdrawMinAmount_ > 0);

        TossExchangeTierV1 exchangeInit = DeployWithProxyUtil.tossExchangeTierV1(IERC20(address(externalErc20)), depositMinAmount_, internalErc20, withdrawMinAmount_, year);

        assertEq(address(exchangeInit.getExternalErc20()), address(externalErc20));
        assertEq(address(exchangeInit.getInternalErc20()), address(internalErc20));
        assertEq(exchangeInit.getDepositMinAmount(), depositMinAmount_);
        assertEq(exchangeInit.getWithdrawMinAmount(), withdrawMinAmount_);

        assertEq(exchangeInit.currentYear(), year);
    }

    function test_depositWithPermit(uint128 amount) public {
        amount = uint128(bound(amount, depositMinAmount, mintAmount));

        exchange.setTierLimit(1, amount);
        exchange.setUserTier(owner, 1);

        SigUtils.Permit memory permit = SigUtils.signPermit(owner, ownerPrivateKey, address(exchange), amount, 1 days, externalErc20);
        exchange.depositWithPermit(amount, permit.value, permit.deadline, permit.v, permit.r, permit.s);

        assertEq(internalErc20.balanceOf(owner), uint256(amount), "internal owner balance");
        assertEq(externalErc20.balanceOf(address(exchange)), amount, "external exchange balance");
        assertEq(externalErc20.balanceOf(owner), mintAmount - amount, "external owner balance");
        assertEq(exchange.getUserBalance(owner), amount, "balance");
    }

    function test_depositAndWithdraw(uint128 amount) public {
        amount = uint128(bound(amount, depositMinAmount, mintAmount));

        exchange.setTierLimit(1, amount);
        exchange.setUserTier(owner, 1);

        externalErc20.approve(address(exchange), amount);
        exchange.deposit(amount);

        assertEq(internalErc20.balanceOf(owner), uint256(amount), "internal owner balance");
        assertEq(externalErc20.balanceOf(address(exchange)), amount, "external exchange balance");
        assertEq(externalErc20.balanceOf(owner), mintAmount - amount, "external owner balance");

        uint256 internalAmount = internalErc20.balanceOf(owner);
        internalErc20.approve(address(exchange), internalAmount);
        exchange.withdraw(uint128(internalAmount));

        assertEq(internalErc20.balanceOf(owner), 0, "internal owner balance");
        assertEq(externalErc20.balanceOf(address(exchange)), 0, "external exchange balance");
        assertEq(externalErc20.balanceOf(owner), mintAmount, "external owner balance");
    }

    function test_depositConsumeLimit(uint128 amount) public {
        amount = uint128(bound(amount, depositMinAmount, mintAmount));

        exchange.setTierLimit(1, amount);
        exchange.setUserTier(owner, 1);
        assertEq(exchange.getUserTier(owner), 1);

        externalErc20.approve(address(exchange), amount);
        exchange.deposit(amount);
        internalErc20.approve(address(exchange), amount);
        exchange.withdraw(uint128(amount));
        externalErc20.approve(address(exchange), amount);

        assertEq(internalErc20.balanceOf(owner), 0, "internal owner balance");
        assertEq(externalErc20.balanceOf(address(exchange)), 0, "external exchange balance");
        assertEq(externalErc20.balanceOf(owner), mintAmount, "external owner balance");
        vm.expectRevert(TossExchangeTierV1.TossExchangeTierYearLimitReach.selector);
        exchange.deposit(amount);
        exchange.setYear(exchange.currentYear() + 1);

        assertEq(exchange.getUserBalance(owner), 0, "balance");
        exchange.deposit(amount);
        assertEq(internalErc20.balanceOf(owner), uint256(amount), "internal owner balance");
        assertEq(externalErc20.balanceOf(address(exchange)), amount, "external exchange balance");
        assertEq(externalErc20.balanceOf(owner), mintAmount - amount, "external owner balance");
    }

    function test_setYear(uint64 year) public {
        vm.assume(year != exchange.currentYear());
        exchange.setYear(year);
        assertEq(year, exchange.currentYear());

        vm.expectRevert(TossExchangeTierV1.TossExchangeTierYearIsTheSame.selector);
        exchange.setYear(year);

        assertEq(year, exchange.currentYear());

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, exchange.YEAR_ROLE()));
        exchange.setYear(year);
    }

    function test_setTierLimit(uint8 tier, uint128 limit) public {
        uint8 tierMax = exchange.TIER_MAX_LENGTH();
        tier = uint8(bound(tier, 1, tierMax - 1));
        exchange.setTierLimit(tier, limit);
        assertEq(exchange.tiers(tier), limit);

        vm.expectRevert(TossExchangeTierV1.TossExchangeTierTier0CantChange.selector);
        exchange.setTierLimit(0, limit);

        vm.expectRevert(abi.encodeWithSelector(TossExchangeTierV1.TossExchangeTierInvalidTier.selector, tierMax));
        exchange.setTierLimit(tierMax, limit);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, exchange.TIER_ROLE()));
        exchange.setTierLimit(tier, limit);
    }

    function test_setIserTier() public {
        exchange.setTierLimit(1, 1);
        exchange.setUserTier(alice, 1);
        assertEq(exchange.getUserTier(alice), 1);
        exchange.setUserTier(alice, 0);
        assertEq(exchange.getUserTier(alice), 0);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, exchange.TIER_ROLE()));
        exchange.setUserTier(owner, 1);
        assertEq(exchange.getUserTier(owner), 0);
    }
}
