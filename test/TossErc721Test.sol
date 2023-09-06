// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.19;

import {Test} from "forge-std/Test.sol";
import "./DeployWithProxyUtil.sol";

contract TossErc721Test is Test {
    address owner = makeAddr("owner");
    address alice = makeAddr("alice");
    address bob = makeAddr("bob");

    function setUp() public {
        vm.deal(owner, 1000 ether);
        vm.startPrank(owner);
    }

    function test_initializationGene() public {
        string memory name = "Erc 721 Gene";
        string memory symbol = "Erc 721 Symbol";
        TossErc721GeneV1 erc721 = DeployWithProxyUtil.tossErc721GeneV1(name, symbol);
        assertEq(erc721.name(), name);
        assertEq(erc721.symbol(), symbol);
    }

    function test_initializationGeneUnique() public {
        string memory name = "Erc 721 Unique";
        string memory symbol = "Erc 721 Symbol";
        TossErc721GeneUniqueV1 erc721 = DeployWithProxyUtil.tossErc721GeneUniqueV1(name, symbol);
        assertEq(erc721.name(), name);
        assertEq(erc721.symbol(), symbol);
    }

    function test_initializationMarket() public {
        string memory name = "Erc 721 Market";
        string memory symbol = "Erc 721 Symbol";
        TossErc721MarketV1 erc721 = DeployWithProxyUtil.tossErc721MarketV1(name, symbol);
        (name, symbol);
        assertEq(erc721.name(), name);
        assertEq(erc721.symbol(), symbol);
    }
}
