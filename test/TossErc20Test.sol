// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossErc20Test is BaseTest {
    TossErc20V1 erc20;
    uint256 amount = 10 ether;

    function setUp() public override {
        super.setUp();
        erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
    }

    function test_upgrade() public {
        TossErc20V1 erc20Init = new TossErc20V1();
        assertNotEq(erc20.getImplementation(), address(erc20Init));
        erc20.upgradeToAndCall(address(erc20Init), "");
        assertEq(erc20.getImplementation(), address(erc20Init));
        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, erc20.UPGRADER_ROLE()));
        erc20.upgradeToAndCall(address(erc20Init), "");
    }

    function testFuzz_initialization(uint256 amount_) public {
        TossErc20V1 erc20Init = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount_);
        assertEq(erc20Init.name(), "Erc20 Test");
        assertEq(erc20Init.symbol(), "E20T");
        assertEq(erc20Init.totalSupply(), amount_);
    }

    function test_transfer() public {
        assertEq(erc20.balanceOf(owner), amount);
        uint256 transferAmount = 1 ether;
        erc20.transfer(alice, transferAmount);

        assertEq(erc20.balanceOf(owner), amount - transferAmount);
        assertEq(erc20.balanceOf(alice), transferAmount);
    }

    function test_unpause() public {
        assertEq(erc20.balanceOf(owner), amount);
        uint256 transferAmount = 1 ether;
        erc20.transfer(alice, transferAmount);
        assertEq(erc20.balanceOf(alice), transferAmount);
        erc20.pause();
        vm.expectRevert(abi.encodeWithSelector(PausableUpgradeable.EnforcedPause.selector));
        erc20.transfer(alice, transferAmount);
        assertEq(erc20.balanceOf(alice), transferAmount);
        erc20.unpause();
        erc20.transfer(alice, transferAmount);
        assertEq(erc20.balanceOf(alice), transferAmount * 2);
    }

    function test_transferWhitelist() public {
        erc20.setWhitelist(address(whitelist));
        whitelist.set(owner, true);
        assertEq(erc20.balanceOf(owner), amount);
        uint256 transferAmount = 1 ether;
        vm.expectRevert(abi.encodeWithSelector(TossWhitelistClient.TossWhitelistNotInWhitelist.selector, alice));
        erc20.transfer(alice, transferAmount);
        whitelist.set(alice, true);
        erc20.transfer(alice, transferAmount);
        assertEq(erc20.balanceOf(alice), transferAmount);
        assertEq(erc20.balanceOf(owner), amount - transferAmount);
    }

    function test_transferFromWithApprove() public {
        assertEq(erc20.balanceOf(owner), amount);
        uint256 transferAmount = 1 ether;
        erc20.approve(alice, transferAmount);
        vm.startPrank(alice);
        erc20.transferFrom(owner, alice, transferAmount);
        assertEq(erc20.balanceOf(owner), amount - transferAmount);
        assertEq(erc20.balanceOf(alice), transferAmount);
    }

    function test_transferFromRevert() public {
        assertEq(erc20.balanceOf(owner), amount);
        uint256 transferAmount = 1 ether;
        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IERC20Errors.ERC20InsufficientAllowance.selector, alice, 0, transferAmount));
        erc20.transferFrom(owner, alice, transferAmount);
    }

    function test_transferWhenPauseFail() public {
        erc20.pause();
        uint256 transferAmount = 1 ether;
        vm.expectRevert(abi.encodeWithSelector(PausableUpgradeable.EnforcedPause.selector));
        erc20.transfer(alice, transferAmount);
    }

    function test_mintWhenPauseFail() public {
        erc20.pause();
        uint256 transferAmount = 1 ether;
        vm.expectRevert(abi.encodeWithSelector(PausableUpgradeable.EnforcedPause.selector));
        erc20.mint(alice, transferAmount);
    }

    function test_burn() public {
        uint256 transferAmount = 1 ether;
        erc20.burn(transferAmount);
        assertEq(erc20.balanceOf(owner), amount - transferAmount);
    }

    function test_burnFrom() public {
        uint256 transferAmount = 1 ether;
        erc20.transfer(alice, transferAmount);
        vm.startPrank(alice);
        erc20.approve(owner, transferAmount);
        vm.startPrank(owner);
        erc20.burnFrom(alice, transferAmount);
        assertEq(erc20.balanceOf(owner), amount - transferAmount);
    }

    function test_burnWhenPauseFail() public {
        erc20.pause();
        uint256 transferAmount = 1 ether;
        vm.expectRevert(abi.encodeWithSelector(PausableUpgradeable.EnforcedPause.selector));
        erc20.burn(transferAmount);
    }
}
