// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossErc721Test is BaseTest {
    TossErc721GeneV1 erc721;
    string constant name = "Erc 721 Gene";
    string constant symbol = "Erc 721 Symbol";

    function setUp() public override {
        super.setUp();
        erc721 = DeployWithProxyUtil.tossErc721GeneV1(name, symbol);
    }

    function test_initializationNameAndSymbol() public {
        assertEq(erc721.name(), name);
        assertEq(erc721.symbol(), symbol);
    }
}
