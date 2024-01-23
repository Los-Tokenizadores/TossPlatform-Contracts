// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";
import { MockErc20Decimals } from "./utils/MockErc20Decimals.sol";
import { MockErc20TransferCommision } from "./utils/MockErc20TransferCommision.sol";

contract TossExchangeTest is BaseTest {
    TossErc20V1 private externalErc20;
    TossErc20V1 private internalErc20;
    TossExchangeV1 private exchange;
    uint256 private mintAmount = 99_999 ether;
    uint128 constant depositMinAmount = 1;
    uint128 constant withdrawMinAmount = 1;

    function setUp() public override {
        super.setUp();
        externalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", mintAmount);
        internalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", 0);
        exchange = DeployWithProxyUtil.tossExchangeV1(IERC20(address(externalErc20)), depositMinAmount, internalErc20, withdrawMinAmount);
        internalErc20.grantRole(internalErc20.MINTER_ROLE(), address(exchange));
    }

    function test_upgrade() public {
        TossExchangeV1 exchangeImp = new TossExchangeV1();
        assertNotEq(exchange.getImplementation(), address(exchangeImp));
        exchange.upgradeToAndCall(address(exchangeImp), "");
        assertEq(exchange.getImplementation(), address(exchangeImp));
        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, exchange.UPGRADER_ROLE()));
        exchange.upgradeToAndCall(address(exchangeImp), "");
    }

    function test_initialization(uint128 depositMinAmount_, uint128 withdrawMinAmount_) public {
        vm.assume(depositMinAmount_ > 0);
        vm.assume(withdrawMinAmount_ > 0);

        TossExchangeV1 exchangeInit = DeployWithProxyUtil.tossExchangeV1(IERC20(address(externalErc20)), depositMinAmount_, internalErc20, withdrawMinAmount_);

        assertEq(address(exchangeInit.getExternalErc20()), address(externalErc20));
        assertEq(address(exchangeInit.getInternalErc20()), address(internalErc20));
        assertEq(exchangeInit.getDepositMinAmount(), depositMinAmount_);
        assertEq(exchangeInit.getWithdrawMinAmount(), withdrawMinAmount_);
    }

    function test_initializationRevert() public {
        address exchangeImp = exchange.getImplementation();

        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "external"));
        new TossUpgradeableProxy(exchangeImp, abi.encodeCall(TossExchangeV1.__TossExchangeV1_init, (IERC20(address(0)), depositMinAmount, internalErc20, withdrawMinAmount)));

        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "internal"));
        new TossUpgradeableProxy(
            exchangeImp, abi.encodeCall(TossExchangeV1.__TossExchangeV1_init, (IERC20(externalErc20), depositMinAmount, TossErc20Base(address(0)), withdrawMinAmount))
        );

        vm.expectRevert(TossExchangeBase.TossExchangeExternalAndInternalErc20AreEqual.selector);
        new TossUpgradeableProxy(exchangeImp, abi.encodeCall(TossExchangeV1.__TossExchangeV1_init, (IERC20(externalErc20), depositMinAmount, externalErc20, withdrawMinAmount)));

        MockErc20Decimals mockDecimals = new MockErc20Decimals(3);
        vm.expectRevert(TossExchangeBase.TossExchangeExternalAndInternalErc20HaveDifferentDecimalAmount.selector);
        new TossUpgradeableProxy(exchangeImp, abi.encodeCall(TossExchangeV1.__TossExchangeV1_init, (IERC20(mockDecimals), depositMinAmount, internalErc20, withdrawMinAmount)));

        vm.expectRevert(abi.encodeWithSelector(TossValueIsZero.selector, "deposit min"));
        new TossUpgradeableProxy(exchangeImp, abi.encodeCall(TossExchangeV1.__TossExchangeV1_init, (IERC20(externalErc20), 0, internalErc20, withdrawMinAmount)));

        vm.expectRevert(abi.encodeWithSelector(TossValueIsZero.selector, "withdraw min"));
        new TossUpgradeableProxy(exchangeImp, abi.encodeCall(TossExchangeV1.__TossExchangeV1_init, (IERC20(externalErc20), depositMinAmount, internalErc20, 0)));
    }

    function test_depositAndWithdrawPermit(uint128 amount) public {
        amount = uint128(bound(amount, depositMinAmount, mintAmount));

        SigUtils.Permit memory permit = SigUtils.signPermit(owner, ownerPrivateKey, address(exchange), amount, 1 days, externalErc20);
        exchange.depositWithPermit(amount, permit.value, permit.deadline, permit.v, permit.r, permit.s);
        assert(exchange.validState());

        assertEq(internalErc20.balanceOf(owner), uint256(amount), "internal owner balance");
        assertEq(externalErc20.balanceOf(address(exchange)), amount, "external exchange balance");
        assertEq(externalErc20.balanceOf(owner), mintAmount - amount, "external owner balance");

        uint256 internalAmount = internalErc20.balanceOf(owner);
        permit = SigUtils.signPermit(owner, ownerPrivateKey, address(exchange), internalAmount, 1 days, internalErc20);
        exchange.withdrawWithPermit(uint128(internalAmount), permit.value, permit.deadline, permit.v, permit.r, permit.s);
        assert(exchange.validState());

        assertEq(internalErc20.balanceOf(owner), 0, "internal owner balance");
        assertEq(externalErc20.balanceOf(address(exchange)), 0, "external exchange balance");
        assertEq(externalErc20.balanceOf(owner), mintAmount, "external owner balance");
    }

    function test_depositAndWithdraw(uint128 amount) public {
        amount = uint128(bound(amount, depositMinAmount, mintAmount));
        externalErc20.approve(address(exchange), amount);
        exchange.deposit(amount);
        assert(exchange.validState());

        assertEq(internalErc20.balanceOf(owner), uint256(amount), "internal owner balance");
        assertEq(externalErc20.balanceOf(address(exchange)), amount, "external exchange balance");
        assertEq(externalErc20.balanceOf(owner), mintAmount - amount, "external owner balance");

        uint256 internalAmount = internalErc20.balanceOf(owner);
        internalErc20.approve(address(exchange), internalAmount);
        exchange.withdraw(uint128(internalAmount));
        assert(exchange.validState());

        assertEq(internalErc20.balanceOf(owner), 0, "internal owner balance");
        assertEq(externalErc20.balanceOf(address(exchange)), 0, "external exchange balance");
        assertEq(externalErc20.balanceOf(owner), mintAmount, "external owner balance");
    }

    function test_depositAndWithdrawPauseUnpause(uint128 amount) public {
        amount = uint128(bound(amount, depositMinAmount, mintAmount));
        externalErc20.approve(address(exchange), amount);
        exchange.pause();
        vm.expectRevert(abi.encodeWithSelector(PausableUpgradeable.EnforcedPause.selector));
        exchange.deposit(amount);
        exchange.unpause();
        exchange.deposit(amount);
        assert(exchange.validState());

        assertEq(internalErc20.balanceOf(owner), uint256(amount), "internal owner balance");
        assertEq(externalErc20.balanceOf(address(exchange)), amount, "external exchange balance");
        assertEq(externalErc20.balanceOf(owner), mintAmount - amount, "external owner balance");

        uint256 internalAmount = internalErc20.balanceOf(owner);
        internalErc20.approve(address(exchange), internalAmount);
        exchange.pause();
        vm.expectRevert(abi.encodeWithSelector(PausableUpgradeable.EnforcedPause.selector));
        exchange.withdraw(uint128(internalAmount));
        exchange.unpause();
        exchange.withdraw(uint128(internalAmount));
        assert(exchange.validState());

        assertEq(internalErc20.balanceOf(owner), 0, "internal owner balance");
        assertEq(externalErc20.balanceOf(address(exchange)), 0, "external exchange balance");
        assertEq(externalErc20.balanceOf(owner), mintAmount, "external owner balance");
    }

    function test_depositAndWithdrawWhitelist(uint128 amount) public {
        exchange.setWhitelist(address(whitelist));
        amount = uint128(bound(amount, depositMinAmount, mintAmount));
        externalErc20.approve(address(exchange), amount);
        whitelist.set(owner, false);
        vm.expectRevert(abi.encodeWithSelector(TossWhitelistClient.TossWhitelistNotInWhitelist.selector, owner));
        exchange.deposit(amount);
        whitelist.set(owner, true);
        exchange.deposit(amount);
        assert(exchange.validState());

        assertEq(internalErc20.balanceOf(owner), uint256(amount), "internal owner balance");
        assertEq(externalErc20.balanceOf(address(exchange)), amount, "external exchange balance");
        assertEq(externalErc20.balanceOf(owner), mintAmount - amount, "external owner balance");

        uint256 internalAmount = internalErc20.balanceOf(owner);
        internalErc20.approve(address(exchange), internalAmount);
        whitelist.set(owner, false);
        vm.expectRevert(abi.encodeWithSelector(TossWhitelistClient.TossWhitelistNotInWhitelist.selector, owner));
        exchange.withdraw(uint128(internalAmount));
        whitelist.set(owner, true);
        exchange.withdraw(uint128(internalAmount));
        assert(exchange.validState());

        assertEq(internalErc20.balanceOf(owner), 0, "internal owner balance");
        assertEq(externalErc20.balanceOf(address(exchange)), 0, "external exchange balance");
        assertEq(externalErc20.balanceOf(owner), mintAmount, "external owner balance");
    }

    function test_depositAndWithdrawCheckMin(uint128 minAmount) public {
        minAmount = uint128(bound(minAmount, 3, mintAmount));
        exchange.setDepositMinAmount(minAmount);
        externalErc20.approve(address(exchange), minAmount);
        vm.expectRevert(abi.encodeWithSelector(TossExchangeBase.TossExchangeAmounIsLessThanMin.selector, "deposit", minAmount - 1, minAmount));
        exchange.deposit(minAmount - 1);
        exchange.deposit(minAmount);
        assert(exchange.validState());

        assertEq(internalErc20.balanceOf(owner), uint256(minAmount), "internal owner balance");
        assertEq(externalErc20.balanceOf(address(exchange)), minAmount, "external exchange balance");
        assertEq(externalErc20.balanceOf(owner), mintAmount - minAmount, "external owner balance");

        uint256 internalAmount = internalErc20.balanceOf(owner);
        internalErc20.approve(address(exchange), internalAmount);
        exchange.setWithdrawMinAmount(minAmount - 1);
        vm.expectRevert(abi.encodeWithSelector(TossExchangeBase.TossExchangeAmounIsLessThanMin.selector, "withdraw", minAmount - 2, minAmount - 1));
        exchange.withdraw(uint128(internalAmount - 2));
        exchange.withdraw(uint128(internalAmount - 1));
        assert(exchange.validState());

        assertEq(internalErc20.balanceOf(owner), 1, "internal owner balance");
        assertEq(externalErc20.balanceOf(address(exchange)), 1, "external exchange balance");
        assertEq(externalErc20.balanceOf(owner), mintAmount - 1, "external owner balance");
    }

    function test_setDepositMin(uint128 min) public {
        vm.assume(min > 0);
        exchange.setDepositMinAmount(min);
        assertEq(min, exchange.getDepositMinAmount());

        vm.expectRevert(abi.encodeWithSelector(TossValueIsZero.selector, "deposit min"));
        exchange.setDepositMinAmount(0);

        assertEq(min, exchange.getDepositMinAmount());
    }

    function test_setWithdrawMin(uint128 min) public {
        vm.assume(min > 0);
        exchange.setWithdrawMinAmount(min);
        assertEq(min, exchange.getWithdrawMinAmount());

        vm.expectRevert(abi.encodeWithSelector(TossValueIsZero.selector, "withdraw min"));
        exchange.setWithdrawMinAmount(0);

        assertEq(min, exchange.getWithdrawMinAmount());
    }

    function test_invalidateStateRevert(uint128 amount) public {
        uint256 commision = 10;
        amount = uint128(bound(amount, commision * 10, mintAmount - commision * 5));
        MockErc20TransferCommision externalErc20Commision = new MockErc20TransferCommision(mintAmount, commision);
        assertEq(externalErc20Commision.balanceOf(owner), mintAmount);
        TossExchangeV1 exchangeInvalid = DeployWithProxyUtil.tossExchangeV1(IERC20(address(externalErc20Commision)), depositMinAmount, internalErc20, withdrawMinAmount);
        internalErc20.grantRole(internalErc20.MINTER_ROLE(), address(exchangeInvalid));

        externalErc20Commision.approve(address(exchangeInvalid), amount);
        vm.expectRevert(abi.encodeWithSelector(TossExchangeBase.TossExchangeInvalidState.selector, amount - commision, amount));
        exchangeInvalid.deposit(amount);
        externalErc20Commision.transfer(address(exchangeInvalid), commision);
        exchangeInvalid.deposit(amount);

        assertEq(internalErc20.balanceOf(owner), uint256(amount), "internal owner balance");
        assertEq(externalErc20Commision.balanceOf(address(exchangeInvalid)), amount, "external exchange balance");
        assertEq(externalErc20Commision.balanceOf(owner), mintAmount - amount - commision, "external owner balance");

        uint256 internalAmount = internalErc20.balanceOf(owner);
        internalErc20.approve(address(exchangeInvalid), internalAmount);
        exchangeInvalid.withdraw(uint128(internalAmount));

        assertEq(internalErc20.balanceOf(owner), 0, "internal owner balance");
        assertEq(externalErc20Commision.balanceOf(address(exchangeInvalid)), 0, "external exchange balance");
        assertEq(externalErc20Commision.balanceOf(owner), mintAmount - commision * 2, "external owner balance");

        externalErc20Commision.transfer(address(exchangeInvalid), amount);
        internalErc20.mint(address(owner), amount);

        internalErc20.approve(address(exchangeInvalid), amount);
        vm.expectRevert(abi.encodeWithSelector(TossExchangeBase.TossExchangeInvalidState.selector, 0, commision));
        exchangeInvalid.withdraw(uint128(amount - commision));
    }
}
