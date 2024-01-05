// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossExchangeTest is BaseTest {
    function test_initialization(uint128 depositMinAmount, uint128 withdrawMinAmount) public {
        vm.assume(depositMinAmount > 0);
        vm.assume(withdrawMinAmount > 0);

        uint256 amount = 10 ether;
        TossErc20V1 externalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossErc20V1 internalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossExchangeV1 exchange = DeployWithProxyUtil.tossExchangeV1(IERC20(address(externalErc20)), depositMinAmount, internalErc20, withdrawMinAmount);

        assertEq(address(exchange.getExternalErc20()), address(externalErc20));
        assertEq(address(exchange.getInternalErc20()), address(internalErc20));
    }

    function test_initializationTier(uint128 depositMinAmount, uint128 withdrawMinAmount) public {
        vm.assume(depositMinAmount > 0);
        vm.assume(withdrawMinAmount > 0);
        uint64 year = 2023;
        uint256 amount = 10 ether;
        TossErc20V1 externalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossErc20V1 internalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossExchangeTierV1 exchange = DeployWithProxyUtil.tossExchangeTierV1(IERC20(address(externalErc20)), depositMinAmount, internalErc20, withdrawMinAmount, year);

        assertEq(address(exchange.getExternalErc20()), address(externalErc20));
        assertEq(address(exchange.getInternalErc20()), address(internalErc20));
    }

    function test_tierDepositWithPermit(uint128 amount) public {
        vm.assume(amount > 0);

        TossErc20V1 externalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossErc20V1 internalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", 0);
        TossExchangeTierV1 exchange = DeployWithProxyUtil.tossExchangeTierV1(IERC20(address(externalErc20)), 1, internalErc20, 1, 2023);
        internalErc20.grantRole(internalErc20.MINTER_ROLE(), address(exchange));
        exchange.setTierLimit(1, amount);
        exchange.setUserTier(owner, 1);
        SigUtils.Permit memory permit = SigUtils.Permit({ owner: owner, spender: address(exchange), value: amount, nonce: externalErc20.nonces(owner), deadline: 1 days });
        SigUtils sigUtils = new SigUtils(externalErc20.DOMAIN_SEPARATOR());
        bytes32 digest = sigUtils.getTypedDataHash(permit);
        (uint8 v, bytes32 r, bytes32 s) = vm.sign(ownerPrivateKey, digest);

        exchange.depositWithPermit(amount, permit.value, permit.deadline, v, r, s);
        assertEq(internalErc20.balanceOf(owner), uint256(amount));
        assertEq(externalErc20.balanceOf(address(exchange)), amount);
        assertEq(externalErc20.balanceOf(owner), 0);
    }

    function test_tierDepositAndWithdraw(uint128 amount) public {
        uint128 depositMinAmount = 1;
        uint128 withdrawMinAmount = 1;

        uint256 mintAmount = 100 ether;
        amount = uint128(bound(amount, depositMinAmount, mintAmount));
        TossErc20V1 externalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", mintAmount);
        TossErc20V1 internalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", 0);
        TossExchangeTierV1 exchange = DeployWithProxyUtil.tossExchangeTierV1(IERC20(address(externalErc20)), depositMinAmount, internalErc20, withdrawMinAmount, 2023);
        internalErc20.grantRole(internalErc20.MINTER_ROLE(), address(exchange));

        exchange.setTierLimit(1, amount);
        exchange.setUserTier(owner, 1);

        externalErc20.approve(address(exchange), amount);
        exchange.deposit(amount);

        uint256 internalAmount = internalErc20.balanceOf(owner);
        assertEq(internalErc20.balanceOf(owner), uint256(amount));
        assertEq(externalErc20.balanceOf(address(exchange)), amount);
        assertEq(externalErc20.balanceOf(owner), mintAmount - amount);

        internalErc20.approve(address(exchange), internalAmount);
        exchange.withdraw(uint128(internalAmount));

        assertEq(internalErc20.balanceOf(owner), 0);
        assertEq(externalErc20.balanceOf(address(exchange)), 0);
        assertEq(externalErc20.balanceOf(owner), mintAmount);
    }
}
