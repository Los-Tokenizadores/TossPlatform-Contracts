// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import { TossErc20V1, TossErc20Base } from "../src/TossErc20V1.sol";
import { TossErc721GeneV1 } from "../src/TossErc721GeneV1.sol";
import { TossErc721GeneUniqueV1 } from "../src/TossErc721GeneUniqueV1.sol";
import { TossErc721MarketV1 } from "../src/TossErc721MarketV1.sol";
import { TossExchangeTierV1 } from "../src/TossExchangeTierV1.sol";
import { TossExchangeV1 } from "../src/TossExchangeV1.sol";
import { TossInvestV1 } from "../src/TossInvestV1.sol";
import { TossMarketV1 } from "../src/TossMarketV1.sol";
import { TossSellerV1 } from "../src/TossSellerV1.sol";
import { TossWhitelistV1 } from "../src/TossWhitelistV1.sol";
import { TossUpgradeableProxy } from "../src/TossUpgradeableProxy.sol";
import { SafeERC20, IERC20 } from "@openzeppelin/contracts/token/ERC20/Utils/SafeERC20.sol";

library DeployWithProxyUtil {
    using SafeERC20 for IERC20;

    function tossErc20V1(string memory name, string memory symbol, uint64 amount) internal returns (TossErc20V1) {
        TossUpgradeableProxy proxy = new TossUpgradeableProxy(address(new TossErc20V1()), abi.encodeCall(TossErc20V1.__TossErc20V1_init, (name, symbol, amount)));

        return TossErc20V1(address(proxy));
    }

    function tossSellerV1(IERC20 erc20) internal returns (TossSellerV1) {
        TossUpgradeableProxy proxy = new TossUpgradeableProxy(address(new TossSellerV1()), abi.encodeCall(TossSellerV1.__TossSellerV1_init, (erc20)));

        return TossSellerV1(address(proxy));
    }

    function tossErc721GeneV1(string memory name, string memory symbol) internal returns (TossErc721GeneV1) {
        TossUpgradeableProxy proxy = new TossUpgradeableProxy(address(new TossErc721GeneV1()), abi.encodeCall(TossErc721GeneV1.__TossErc721GeneV1_init, (name, symbol)));

        return TossErc721GeneV1(address(proxy));
    }

    function tossErc721GeneUniqueV1(string memory name, string memory symbol) internal returns (TossErc721GeneUniqueV1) {
        TossUpgradeableProxy proxy =
            new TossUpgradeableProxy(address(new TossErc721GeneUniqueV1()), abi.encodeCall(TossErc721GeneUniqueV1.__TossErc721GeneUniqueV1_init, (name, symbol)));

        return TossErc721GeneUniqueV1(address(proxy));
    }

    function tossErc721MarketV1(string memory name, string memory symbol) internal returns (TossErc721MarketV1) {
        TossUpgradeableProxy proxy = new TossUpgradeableProxy(address(new TossErc721MarketV1()), abi.encodeCall(TossErc721MarketV1.__TossErc721MarketV1_init, (name, symbol)));

        return TossErc721MarketV1(address(proxy));
    }

    function tossMarketV1(IERC20 erc20, uint16 marketCut) internal returns (TossMarketV1) {
        TossUpgradeableProxy proxy = new TossUpgradeableProxy(address(new TossMarketV1()), abi.encodeCall(TossMarketV1.__TossMarketV1_init, (erc20, marketCut)));

        return TossMarketV1(address(proxy));
    }

    function tossExchangeV1(
        IERC20 externalErc20,
        uint128 externalMinAmount,
        TossErc20Base internalErc20,
        uint128 internalMinAmount,
        uint64 rate
    ) internal returns (TossExchangeV1) {
        TossUpgradeableProxy proxy = new TossUpgradeableProxy(
            address(new TossExchangeV1()), abi.encodeCall(TossExchangeV1.__TossExchangeV1_init, (externalErc20, externalMinAmount, internalErc20, internalMinAmount, rate))
        );

        return TossExchangeV1(address(proxy));
    }

    function tossExchangeTierV1(
        IERC20 externalErc20,
        uint128 externalMinAmount,
        TossErc20Base internalErc20,
        uint128 internalMinAmount,
        uint64 rate,
        uint64 year
    ) internal returns (TossExchangeTierV1) {
        TossUpgradeableProxy proxy = new TossUpgradeableProxy(
            address(new TossExchangeTierV1()),
            abi.encodeCall(TossExchangeTierV1.__TossExchangeTierV1_init, (externalErc20, externalMinAmount, internalErc20, internalMinAmount, rate, year))
        );

        return TossExchangeTierV1(address(proxy));
    }

    function tossWhitelistV1() internal returns (TossWhitelistV1) {
        TossUpgradeableProxy proxy = new TossUpgradeableProxy(address(new TossWhitelistV1()), abi.encodeCall(TossWhitelistV1.__TossWhitelistV1_init, ()));

        return TossWhitelistV1(address(proxy));
    }

    function tossInvestV1(
        IERC20 erc20,
        TossErc721MarketV1 erc721Implementation,
        address platformAddress,
        uint16 platformCut,
        string memory erc721baseUri
    ) internal returns (TossInvestV1) {
        TossUpgradeableProxy proxy = new TossUpgradeableProxy(
            address(new TossInvestV1()), abi.encodeCall(TossInvestV1.__TossInvestV1_init, (erc20, erc721Implementation, platformAddress, platformCut, erc721baseUri))
        );

        return TossInvestV1(address(proxy));
    }
}
