// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import { Test } from "forge-std/Test.sol";
import { SigUtils } from "./utils/SigUtils.sol";
import "./DeployWithProxyUtil.sol";
import "../src/interfaces/TossErrors.sol";
import { TossWhitelistClient } from "../src/Bases/TossWhitelistClient.sol";
import { Initializable } from "@openzeppelin/contracts-upgradeable/proxy/utils/Initializable.sol";
import { PausableUpgradeable } from "@openzeppelin/contracts-upgradeable/utils/PausableUpgradeable.sol";
import { IAccessControl } from "@openzeppelin/contracts/access/IAccessControl.sol";
import { TossErc721MarketBase } from "../src/Bases/TossErc721MarketBase.sol";

abstract contract BaseTest is Test {
    uint256 internal constant ownerPrivateKey = 0x1;
    uint256 internal constant alicePrivateKey = 0x2;
    uint256 internal constant bobPrivateKey = 0x3;

    address internal owner = vm.addr(ownerPrivateKey);
    address internal alice = vm.addr(alicePrivateKey);
    address internal bob = vm.addr(bobPrivateKey);
    TossWhitelistV1 internal whitelist;

    function setUp() public virtual {
        vm.deal(owner, 1000 ether);
        vm.startPrank(owner);
        whitelist = DeployWithProxyUtil.tossWhitelistV1();
    }
}
