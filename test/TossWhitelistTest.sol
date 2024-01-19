// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossWhitelistTest is BaseTest {
    function test_initialization() public {
        whitelist.set(alice, true);
        assertTrue(whitelist.isInWhitelist(alice));
        whitelist.set(alice, false);
        assertFalse(whitelist.isInWhitelist(alice));
    }

    function test_upgrade() public {
        TossWhitelistV1 whitelistInit = new TossWhitelistV1();
        assertNotEq(whitelist.getImplementation(), address(whitelistInit));
        whitelist.upgradeToAndCall(address(whitelistInit), "");
        assertEq(whitelist.getImplementation(), address(whitelistInit));
        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, whitelist.UPGRADER_ROLE()));
        whitelist.upgradeToAndCall(address(whitelistInit), "");
    }

    function test_getWhitelist() public {
        TossErc20V1 erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", 0);
        erc20.setWhitelist(address(whitelist));
        assertEq(address(whitelist), erc20.getWhitelist());
    }
}
