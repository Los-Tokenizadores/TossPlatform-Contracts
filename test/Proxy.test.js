const { deployContractAndProxyAsync, alignRight } = require("./helpers/utils");
var chai = require('chai');
var expect = chai.expect;
var BN = require('bn.js');
var bnChai = require('bn-chai');
const { assert } = require("chai");
chai.use(bnChai(BN));

const CoinV1 = artifacts.require("CoinV1");
const SellerV1 = artifacts.require("SellerV1");
const NftGeneV1 = artifacts.require("NftGeneV1");
const NftGeneUniqueV1 = artifacts.require("NftGeneUniqueV1");
const MarketV1 = artifacts.require("MarketV1");
const WhitelistV1 = artifacts.require("WhitelistV1");
const StableExchangeV1 = artifacts.require("StableExchangeV1");

contract("Proxy", (accounts) => {
    let [owner, bob] = accounts;

    it("Gas de contratos", async () => {
        const coin = await deployContractAndProxyAsync(CoinV1, new CoinV1('').contract.methods.__CoinV1_init('Coin', 'C', 0), true);

        const seller = await deployContractAndProxyAsync(SellerV1, new SellerV1('').contract.methods.__SellerV1_init(coin.address), true);

        const nftGene = await deployContractAndProxyAsync(NftGeneV1, new NftGeneV1('').contract.methods.__NftGeneV1_init("Nft","N", seller.address), true);
        
        const nftGeneUnique = await deployContractAndProxyAsync(NftGeneUniqueV1, new NftGeneUniqueV1('').contract.methods.__NftGeneUniqueV1_init("Nft","N", seller.address), true);
        
        const market = await deployContractAndProxyAsync(MarketV1, new MarketV1('').contract.methods.__MarketV1_init(coin.address, 0), true);
        
        const whitelist = await deployContractAndProxyAsync(WhitelistV1, new WhitelistV1('').contract.methods.__WhitelistV1_init(), true);
        
        const stableExchange = await deployContractAndProxyAsync(StableExchangeV1, new StableExchangeV1('').contract.methods.__StableExchangeV1_init(seller.address, coin.address), true);
    });

    // async function MarketAsync(MarketContract) {
    //     const coin = await deployContractAndProxyAsync(CoinV1, new CoinV1('').contract.methods.__CoinV1_init("Coin","c", 300_000_000));
    //     const seller = await deployContractAndProxyAsync(SellerV1, new SellerV1('').contract.methods.__SellerV1_init(coin.address));
    //     const nftGene = await deployContractAndProxyAsync(NftGeneV1, new NftGeneV1('').contract.methods.__NftGeneV1_init("Nft","N", seller.address));

    //     let results = {};
    //     const market = await deployContractAndProxyAsync(MarketContract, new MarketContract('').contract.methods.__MarketBase_init(coin.address, 0), true);
       
    //     await nftGene.grantRole(await nftGene.MINTER_ROLE(), seller.address);
       
    //     await seller.setNftSell(nftGene.address, web3.utils.toWei('1'), 1);
        
    //     await coin.approve(seller.address, web3.utils.toWei('1000000'), { from: owner });
    //     await nftGene.addGenes([1] , { from: owner });
    //     await seller.createNft(nftGene.address, 1, { from: owner });
    //     await coin.transfer(bob, web3.utils.toWei('1000'), { from: owner })
        
    //     await nftGene.setMarket(market.address);

    //     let tokenId = await nftGene.tokenOfOwnerByIndex(owner, 0, { from: owner });
    //     let price = web3.utils.toWei('1');
        
    //     results.sell = await nftGene.sellOffer.estimateGas(tokenId, price, { from: owner });
    //     await nftGene.sellOffer(tokenId, price, { from: owner });
    //     results.cancel = await market.cancel.estimateGas(nftGene.address, tokenId, { from: owner });
        
    //     let currentPrice = await market.getPrice(nftGene.address, tokenId);
    //     await coin.approve(market.address, web3.utils.toWei('99999999999999'), { from: bob });
        
    //     results.buy = await market.buy.estimateGas(nftGene.address, tokenId, currentPrice, { from: bob });
    //     await market.buy(nftGene.address, tokenId, currentPrice, { from: bob });

    //     //results.changeCut = await market.changeMarketCut.estimateGas(0);
    //     return results;
    // }

    // function logResult(r, r2, rOpt) {
    //     console.log(`\n\r\t${"Method".padEnd(10)}${alignRight("Base",10)}${alignRight("Opt", 10).padEnd(10)}${alignRight("Percent",11)}`);
    //     logLine('create', r.sell, r2.sell, rOpt.sell);
    //     logLine('cancel', r.cancel, r2.cancel, rOpt.cancel);
    //     logLine('buy', r.buy, r2.buy, rOpt.buy);
    //     //logLine('changeCut', r.changeCut, r2.changeCut, rOpt.changeCut);
    // }
    
    // function logLine(text, v, v2, vOpt) {
    //     console.log(`\t${text.padEnd(10)}${alignRight(vOpt - v, 10)}${alignRight(vOpt - v2, 10).padEnd(10)}${alignRight(((vOpt - v) * 100 / v).toFixed(1), 10)}%`);
    // }

    // it.skip("Gas Market y Market2", async () => {
    //     const rOpt = await MarketAsync(MarketBaseOpt);
    //     //const r2 = await MarketAsync(MarketBase2);
    //     const r = await MarketAsync(MarketBase);
    //     let r2 = [];
    //     r2.sell = 0;
    //     r2.cancel = 0;
    //     r2.buy = 0;
    //     r2.changeCut = 0;
    //     logResult(r, r2, rOpt);
    // });

    it("Crear CoinV1", async () => {
        const coin = await deployContractAndProxyAsync(CoinV1, new CoinV1('').contract.methods.__CoinV1_init('Coin', 'C', 99999999));
        await coin.mint(owner, web3.utils.toWei('100'));
        let balance = await coin.balanceOf(owner);
    });

    it("NftGeneV1", async () => {
        const nftV1 = await deployContractAndProxyAsync(NftGeneV1, new NftGeneV1('').contract.methods.__NftGeneV1_init("Nft","N", owner));
    });
});