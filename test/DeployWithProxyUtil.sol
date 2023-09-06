// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.19;

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
import "@openzeppelin/contracts/token/ERC20/Utils/SafeERC20.sol";

library DeployWithProxyUtil {
    using SafeERC20 for IERC20;

    function tossErc20V1(string memory name, string memory symbol, uint64 amount) public returns (TossErc20V1) {
        TossUpgradeableProxy proxy =
        new TossUpgradeableProxy(address(new TossErc20V1()), abi.encodeCall(TossErc20V1.__TossErc20V1_init, (name, symbol,  amount)));

        return TossErc20V1(address(proxy));
    }

    function tossSellerV1(IERC20 erc20) public returns (TossSellerV1) {
        TossUpgradeableProxy proxy =
        new TossUpgradeableProxy(address(new TossSellerV1()), abi.encodeCall(TossSellerV1.__TossSellerV1_init, (erc20)));

        return TossSellerV1(address(proxy));
    }

    function tossErc721GeneV1(string memory name, string memory symbol) public returns (TossErc721GeneV1) {
        TossUpgradeableProxy proxy =
        new TossUpgradeableProxy(address(new TossErc721GeneV1()), abi.encodeCall(TossErc721GeneV1.__TossErc721GeneV1_init, (name, symbol)));

        return TossErc721GeneV1(address(proxy));
    }

    function tossErc721GeneUniqueV1(string memory name, string memory symbol) public returns (TossErc721GeneUniqueV1) {
        TossUpgradeableProxy proxy =
        new TossUpgradeableProxy(address(new TossErc721GeneUniqueV1()), abi.encodeCall(TossErc721GeneUniqueV1.__TossErc721GeneUniqueV1_init, (name, symbol)));

        return TossErc721GeneUniqueV1(address(proxy));
    }

    function tossErc721MarketV1(string memory name, string memory symbol) public returns (TossErc721MarketV1) {
        TossUpgradeableProxy proxy =
        new TossUpgradeableProxy(address(new TossErc721MarketV1()), abi.encodeCall(TossErc721MarketV1.__TossErc721MarketV1_init, (name, symbol)));

        return TossErc721MarketV1(address(proxy));
    }

    function tossMarketV1(IERC20 erc20, uint16 marketCut) public returns (TossMarketV1) {
        TossUpgradeableProxy proxy =
        new TossUpgradeableProxy(address(new TossMarketV1()), abi.encodeCall(TossMarketV1.__TossMarketV1_init, (erc20, marketCut)));

        return TossMarketV1(address(proxy));
    }

    function tossExchangeV1(
        IERC20 externalErc20,
        uint256 externalMinAmount,
        TossErc20Base internalErc20,
        uint256 internalMinAmount,
        uint64 rate
    ) public returns (TossExchangeV1) {
        TossUpgradeableProxy proxy =
        new TossUpgradeableProxy(address(new TossExchangeV1()), abi.encodeCall(TossExchangeV1.__TossExchangeV1_init, (externalErc20, externalMinAmount, internalErc20, internalMinAmount, rate)));

        return TossExchangeV1(address(proxy));
    }

    function tossExchangeTierV1(
        IERC20 externalErc20,
        uint256 externalMinAmount,
        TossErc20Base internalErc20,
        uint256 internalMinAmount,
        uint64 rate,
        uint64 year
    ) public returns (TossExchangeTierV1) {
        TossUpgradeableProxy proxy =
        new TossUpgradeableProxy(address(new TossExchangeTierV1()), abi.encodeCall(TossExchangeTierV1.__TossExchangeTierV1_init, (externalErc20, externalMinAmount, internalErc20, internalMinAmount, rate, year)));

        return TossExchangeTierV1(address(proxy));
    }

    function tossWhitelistV1() public returns (TossWhitelistV1) {
        TossUpgradeableProxy proxy =
        new TossUpgradeableProxy(address(new TossWhitelistV1()), abi.encodeCall(TossWhitelistV1.__TossWhitelistV1_init, ()));

        return TossWhitelistV1(address(proxy));
    }
}
