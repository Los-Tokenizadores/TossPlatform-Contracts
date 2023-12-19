// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import { Test } from "forge-std/Test.sol";
import "./DeployWithProxyUtil.sol";

abstract contract BaseTest is Test {
    uint256 internal constant ownerPrivateKey = 0x1;
    uint256 internal constant alicePrivateKey = 0x2;
    uint256 internal constant bobPrivateKey = 0x3;

    address internal owner = vm.addr(ownerPrivateKey);
    address internal alice = vm.addr(alicePrivateKey);
    address internal bob = vm.addr(bobPrivateKey);

    function setUp() public virtual {
        vm.deal(owner, 1000 ether);
        vm.startPrank(owner);
    }
}
