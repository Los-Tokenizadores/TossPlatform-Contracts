const { shouldThrowAsync, deployContractAndProxyAsync } = require("./helpers/utils");
var chai = require('chai');
var expect = chai.expect;
var BN = require('bn.js');
var bnChai = require('bn-chai');
chai.use(bnChai(BN));

const CoinV1 = artifacts.require("CoinV1");
const SellerV1 = artifacts.require("SellerV1");
const NftGeneV1 = artifacts.require("NftGeneV1");
const MarketV1 = artifacts.require("MarketV1");
const WhitelistV1 = artifacts.require("WhitelistV1");
contract("Seller", (accounts) => {
    let [owner, bob] = accounts;
    let seller;
    let nftGene;
    let coin;
    let whitelist;

    beforeEach(async () => {
        whitelist = await deployContractAndProxyAsync(WhitelistV1, new WhitelistV1('').contract.methods.__WhitelistV1_init());
        coin = await deployContractAndProxyAsync(CoinV1, new CoinV1('').contract.methods.__CoinV1_init("Coin","c", 300_000_000));
        seller = await deployContractAndProxyAsync(SellerV1, new SellerV1('').contract.methods.__SellerV1_init(coin.address));
        nftGene = await deployContractAndProxyAsync(NftGeneV1, new NftGeneV1('').contract.methods.__NftGeneV1_init("Nft","N", seller.address));

        await whitelist.set(owner, true);
        await whitelist.set(bob, true);
        await nftGene.setWhitelist(whitelist.address);
        await seller.setWhitelist(whitelist.address);
        await seller.setNftSell(nftGene.address, web3.utils.toWei('1'), 1);
        await coin.transfer(seller.address, web3.utils.toWei('9999'));
        
        let nftMint = await nftGene.MINTER_ROLE();
        await nftGene.grantRole(nftMint, seller.address);
    });

    context("pruebas con nft", async () => {
        it("comprar un nft en la cuenta owner y verificar que cambio su balance", async () => {
            const genesAmount = 1;
            let genes = [];
            for (let i = 0; i < genesAmount; i++) {
                //genes.push(i);
                genes.push(Math.floor(Math.random() * 9999999999999));
            }
            await nftGene.addGenes(genes, { from: owner });
            //const balance = await coin.balanceOf(owner);
            await coin.approve(seller.address, '999999999999999999999999999', { from: owner });
            await seller.createNft(nftGene.address, genesAmount, { from: owner });
            // for (let i = 0; i < genesAmount; i++) {
            // }

            let nftBalance = await nftGene.balanceOf(owner);
            expect(genesAmount).to.eq.BN(nftBalance);
            // const newBalance = await coin.balanceOf(owner);
            // const price = await seller.nftPrices(nftGene.address);
            // const cost = new BN(price * genesAmount);
            // console.log(`${balance} - ${price} * ${genesAmount} = ${balance - cost}`);
            // const t = balance.sub(cost);
            // expect(t).to.eq.BN(newBalance);
        });
        
        it("comprar un nft en la cuenta bob y verificar que se descontó el precio", async () => {
            await nftGene.addGenes([1] , { from: owner });
            const nftSell = await seller.nftSells(nftGene.address);
            await coin.transfer(bob, nftSell.price, { from: owner });
            const balance = await coin.balanceOf(bob);
            let result = await coin.approve(seller.address, '999999999999999999999999999', { from: bob });
            result = await seller.createNft(nftGene.address, 1,{ from: bob });
            const newBalance = await coin.balanceOf(bob);
            expect(balance.sub(nftSell.price)).to.eq.BN(newBalance);
        });

        it("comprar un nft en la cuenta owner y obtenemos el nftData igual a 33", async () => {
            await nftGene.addGenes([33] , { from: owner });
            let result = await coin.approve(seller.address, '999999999999999999999999999', { from: owner });
            result = await seller.createNft(nftGene.address, 1,{ from: owner });
            let nftId = 0;
            let nftData = await nftGene.getNftData(nftId);
            expect(nftData.gene).to.eq.BN(33);
        });

        it("falla comprar un nft en la cuenta bob porque no tiene fondos", async () => {
            await nftGene.addGenes([1] , { from: owner });
            let result = await coin.approve(seller.address, '999999999999999999999999999', { from: bob });
            assert.equal(result.receipt.status, true);
            await shouldThrowAsync(seller.createNft(nftGene.address, 1, { from: bob }), "Reason given: insufficient balance to transaction");
        });

        it("falla comprar un nft en la cuenta owner porque no se pidio aprobacion", async () => {
            await nftGene.addGenes([1] , { from: owner });
            await shouldThrowAsync(seller.createNft(nftGene.address, 1, { from: owner }), "Reason given: ERC20: insufficient allowance");
        });
    });
});


contract("Market", (accounts) => {
    let [owner, bob] = accounts;
    let seller;
    let nftGene;
    let coin;
    let market;
    let whitelist;

    beforeEach(async () => {
        whitelist = await deployContractAndProxyAsync(WhitelistV1, new WhitelistV1('').contract.methods.__WhitelistV1_init());
        coin = await deployContractAndProxyAsync(CoinV1, new CoinV1('').contract.methods.__CoinV1_init("Coin","c", 300_000_000));
        seller = await deployContractAndProxyAsync(SellerV1, new SellerV1('').contract.methods.__SellerV1_init(coin.address));
        nftGene = await deployContractAndProxyAsync(NftGeneV1, new NftGeneV1('').contract.methods.__NftGeneV1_init("Nft","N", seller.address));
        market = await deployContractAndProxyAsync(MarketV1, new MarketV1('').contract.methods.__MarketV1_init(coin.address, 0));

        await whitelist.set(owner, true);
        await whitelist.set(bob, true);

        await nftGene.setMarket(market.address);
        await market.grantRole(await market.NFT_ROLE(), nftGene.address);
        await nftGene.grantRole(await nftGene.MINTER_ROLE(), seller.address);

        await seller.setNftSell(nftGene.address, web3.utils.toWei('1'), 1);

        await coin.approve(seller.address, web3.utils.toWei('1000000'), { from: owner });
        await nftGene.addGenes([1] , { from: owner });
        await seller.createNft(nftGene.address, 1, { from: owner });
        await coin.transfer(bob, web3.utils.toWei('100'), { from: owner })
    });

    context("pruebas con la nft", async () => {
        it("crea una venta de nft con el owner", async () => {
            let startNftCount = await nftGene.balanceOf(owner);
            let tokenId = 0;
            await nftGene.sellOffer(tokenId, 1, { from: owner });
            let nftCount = await nftGene.balanceOf(owner);
            expect(startNftCount-1).to.eq.BN(nftCount);
            let sellOffer = await market.get(nftGene.address, tokenId);
            expect(sellOffer.owner).to.equal(owner);
        });

        it("crea y cancela una venta con el owner", async () => {
            let startNftCount = await nftGene.balanceOf(owner);
            let tokenId = 0;
            await nftGene.sellOffer(tokenId, 1, { from: owner });
            await market.cancel(nftGene.address, tokenId, { from: owner });
            let nftReturnedCount = await nftGene.balanceOf(owner);
            expect(startNftCount).to.eq.BN(nftReturnedCount);
        });

        it("falla al intentar cancelar una venta de owner el usuario bob", async () => {
            let tokenId = 0;
            await nftGene.sellOffer(tokenId, 1, { from: owner });
            await shouldThrowAsync(market.cancel(nftGene.address, tokenId, { from: bob }), "Reason given: not the owner.");
        });

        it("crea una venta owner y bob lo compra", async () => {
            let tokenId = 0;
            let price = web3.utils.toWei('1');
            await nftGene.sellOffer(tokenId, price, { from: owner });
            let balance = await coin.balanceOf(bob);
            let currentPrice = await market.getPrice(nftGene.address, tokenId);
            expect(balance).to.be.gte.BN(currentPrice);
            await coin.approve(market.address, web3.utils.toWei('99999999999999'), { from: bob });
            await market.buy(nftGene.address, tokenId, currentPrice, { from: bob });
            let nftCount = await nftGene.balanceOf(bob);
            expect(1).to.eq.BN(nftCount);
        });

        it("crea una venta owner y falla por que bob intenta comprar a menor valor", async () => {
            let tokenId = 0;
            let price = web3.utils.toWei('1');
            await nftGene.sellOffer(tokenId, price, { from: owner });
            let balance = await coin.balanceOf(bob);
            let currentPrice = await market.getPrice(nftGene.address, tokenId);
            expect(balance).to.be.gte.BN(currentPrice);
            await coin.approve(market.address, web3.utils.toWei('99999999999999'), { from: bob });
            await shouldThrowAsync(market.buy(nftGene.address, tokenId, currentPrice.subn(1), { from: bob }), "Reason given: price is not the same.");
        });

        it("crea una venta owner y bob lo compra con un valor diferente y deberia fallar", async () => {
            let tokenId = 0;
            let price = web3.utils.toWei('1');
            await nftGene.sellOffer(tokenId, price, { from: owner });
            let balance = await coin.balanceOf(bob);
            let currentPrice = await market.getPrice(nftGene.address, tokenId);
            expect(balance).to.be.gte.BN(currentPrice);
            await coin.approve(market.address, web3.utils.toWei('99999999999999'), { from: bob });
            let newPrice = currentPrice.addn(100);
            await shouldThrowAsync(market.buy(nftGene.address, tokenId, newPrice, { from: bob }), "Reason given: price is not the same.");
        });
    });
});

contract("NftGene", (accounts) => {
    let [owner, bob] = accounts;
    let seller;
    let coin;
    let nftGene;
  
    beforeEach(async () => {
        coin = await deployContractAndProxyAsync(CoinV1, new CoinV1('').contract.methods.__CoinV1_init("Coin","c", 300_000_000));
        seller = await deployContractAndProxyAsync(SellerV1, new SellerV1('').contract.methods.__SellerV1_init(coin.address));
        nftGene = await deployContractAndProxyAsync(NftGeneV1, new NftGeneV1('').contract.methods.__NftGeneV1_init("Nft","N", seller.address));
   });

    it("agregar 1 gene", async () => {
        await nftGene.addGenes([1], { from: owner });
        const length = await nftGene.getRangeGeneLength({ from: owner });
        assert.equal(length.toNumber(), 1);
        const result = await nftGene.getRangeGene({ from: owner });
        assert.equal(result[0].toNumber(), 1);
    });

    it("agregar 3 genes", async () => {
        await nftGene.addGenes([1, 2, 3], { from: owner });
        const length = await nftGene.getRangeGeneLength({ from: owner });
        assert.equal(length.toNumber(), 3);
        const result = await nftGene.getRangeGene({ from: owner });
        assert.equal(result[0].toNumber(), 1);
        assert.equal(result[1].toNumber(), 2);
        assert.equal(result[2].toNumber(), 3);
    });

    it("agregar 3 genes de los cuales 2 son iguales", async () => {
        await nftGene.addGenes([1, 2, 2], { from: owner });
        const length = await nftGene.getRangeGeneLength({ from: owner });
        assert.equal(length.toNumber(), 3);
        const result = await nftGene.getRangeGene({ from: owner });
        assert.equal(result[0].toNumber(), 1);
        assert.equal(result[1].toNumber(), 2);
        assert.equal(result[2].toNumber(), 2);
    });

    it("agregar 3 genes de los cuales 3 son iguales", async () => {
        await nftGene.addGenes([5, 5, 5], { from: owner });
        const length = await nftGene.getRangeGeneLength({ from: owner });
        assert.equal(length.toNumber(), 3);
        const result = await nftGene.getRangeGene({ from: owner });
        assert.equal(result[0].toNumber(), 5);
        assert.equal(result[1].toNumber(), 5);
        assert.equal(result[2].toNumber(), 5);
    });

    it("agregar 1 genes, mintear el nft, agregar otro gene igual y se agrega", async () => {
        await nftGene.addGenes([8], { from: owner });
        await nftGene.createNft(owner, { from: owner });
        let length = await nftGene.getRangeGeneLength({ from: owner });
        assert.equal(length.toNumber(), 0);
        
        await nftGene.addGenes([8], { from: owner });
        length = await nftGene.getRangeGeneLength({ from: owner });
        assert.equal(length.toNumber(), 1);
    });
});