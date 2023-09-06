// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.19;

import {Test} from "forge-std/Test.sol";
import "../src/TossErc20V1.sol";
import "../src/TossErc721GeneV1.sol";
import "../src/TossErc721GeneUniqueV1.sol";
import "../src/TossErc721MarketV1.sol";
import "../src/TossExchangeTierV1.sol";
import "../src/TossExchangeV1.sol";
import "../src/TossInvestV1.sol";
import "../src/TossMarketV1.sol";
import "../src/TossSellerV1.sol";
import "../src/TossWhitelistV1.sol";

contract DeployContractsTest is Test {
    address owner = makeAddr("owner");

    function setUp() public {
        vm.deal(owner, 1000 ether);
        vm.startPrank(owner);
    }

    function test_Erc20V1() public {
        new TossErc20V1();
    }

    function test_Erc721GeneV1() public {
        new TossErc721GeneV1();
    }

    function test_Erc721GeneUniqueV1() public {
        new TossErc721GeneUniqueV1();
    }

    function test_Erc721MarketV1() public {
        new TossErc721MarketV1();
    }

    function test_ExchangeTierV1() public {
        new TossExchangeTierV1();
    }

    function test_ExchangeV1() public {
        new TossExchangeV1();
    }

    function test_InvestV1() public {
        new TossInvestV1();
    }

    function test_MarketV1() public {
        new TossMarketV1();
    }

    function test_SellerV1() public {
        new TossSellerV1();
    }

    function test_WhitelistV1() public {
        new TossWhitelistV1();
    }
}
