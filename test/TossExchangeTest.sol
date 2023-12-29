// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossExchangeTest is BaseTest {
    function test_initialization(uint128 externalMinAmount, uint128 internalMinAmount, uint64 rate) public {
        vm.assume(externalMinAmount > 0);
        vm.assume(internalMinAmount > 0);
        vm.assume(rate > 0);

        uint256 amount = 10 ether;
        TossErc20V1 externalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossErc20V1 internalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossExchangeV1 exchange = DeployWithProxyUtil.tossExchangeV1(IERC20(address(externalErc20)), externalMinAmount, internalErc20, internalMinAmount, rate);

        assertEq(address(exchange.getExternalErc20()), address(externalErc20));
        assertEq(address(exchange.getInternalErc20()), address(internalErc20));
    }

    function test_initializationTier(uint128 externalMinAmount, uint128 internalMinAmount, uint64 rate) public {
        vm.assume(externalMinAmount > 0);
        vm.assume(internalMinAmount > 0);
        vm.assume(rate > 0);
        uint64 year = 2023;
        uint256 amount = 10 ether;
        TossErc20V1 externalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossErc20V1 internalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossExchangeTierV1 exchange = DeployWithProxyUtil.tossExchangeTierV1(IERC20(address(externalErc20)), externalMinAmount, internalErc20, internalMinAmount, rate, year);

        assertEq(address(exchange.getExternalErc20()), address(externalErc20));
        assertEq(address(exchange.getInternalErc20()), address(internalErc20));
    }

    function test_tierConvertToInternalWithPermit(uint128 amount) public {
        uint128 externalMinAmount = 1 ether;
        uint128 internalMinAmount = 1 ether;
        uint64 rate = 10_000;
        uint256 mintAmount = 10 ether;
        vm.assume(amount <= mintAmount && amount >= internalMinAmount);

        TossErc20V1 externalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", mintAmount);
        TossErc20V1 internalErc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", 0);
        TossExchangeTierV1 exchange = DeployWithProxyUtil.tossExchangeTierV1(IERC20(address(externalErc20)), externalMinAmount, internalErc20, internalMinAmount, rate, 2023);
        internalErc20.grantRole(internalErc20.MINTER_ROLE(), address(exchange));
        exchange.setTierLimit(1, amount);
        exchange.setUserTier(owner, 1);
        SigUtils.Permit memory permit = SigUtils.Permit({ owner: owner, spender: address(exchange), value: amount * 20, nonce: externalErc20.nonces(owner), deadline: 1 days });
        SigUtils sigUtils = new SigUtils(externalErc20.DOMAIN_SEPARATOR());
        bytes32 digest = sigUtils.getTypedDataHash(permit);
        (uint8 v, bytes32 r, bytes32 s) = vm.sign(ownerPrivateKey, digest);

        exchange.convertToInternalWithPermit(amount, permit.value, permit.deadline, v, r, s);
        assertEq(internalErc20.balanceOf(owner), amount);
        assertEq(externalErc20.balanceOf(address(exchange)), amount);
        assertEq(externalErc20.balanceOf(owner), mintAmount - amount);
    }
}
