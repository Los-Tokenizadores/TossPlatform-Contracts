// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossWhitelistTest is BaseTest {
    function test_initialization() public {
        TossWhitelistV1 whitelist = DeployWithProxyUtil.tossWhitelistV1();
        whitelist.set(alice, true);
        assertTrue(whitelist.isInWhitelist(alice));
        whitelist.set(alice, false);
        assertFalse(whitelist.isInWhitelist(alice));
    }
}
