const { deployContractAndProxyAsync } = require("./helpers/utils");
var chai = require('chai');
var expect = chai.expect;
var BN = require('bn.js');
var bnChai = require('bn-chai');
const { assert } = require("chai");
chai.use(bnChai(BN));

const NftGeneUniqueV1 = artifacts.require("NftGeneUniqueV1");

contract("NftGeneUniqueV1", (accounts) => {
    let [owner] = accounts;
    let nftGeneUnique;
   
    beforeEach(async () => {
        nftGeneUnique = await deployContractAndProxyAsync(NftGeneUniqueV1, new NftGeneUniqueV1('').contract.methods.__NftGeneUniqueV1_init("Nft", "N", owner), true);
    });

    it("agregar 1 gene", async () => {
        let receipt = await nftGeneUnique.addGenes([1], { from: owner });
        console.log(`NftGeneUniqueV1 addGenes Gas: ${receipt.receipt.gasUsed}`);
        const length = await nftGeneUnique.getRangeGeneLength({ from: owner });
        //console.log(`NftGeneUniqueV1 getRangeGeneLength Gas: ${await nftGeneUniqueProxy.getRangeGeneLength.estimateGas({ from: owner })}`);
        assert.equal(length.toNumber(), 1);
        const result = await nftGeneUnique.getRangeGene({ from: owner });
        assert.equal(result[0].toNumber(), 1);
    });

    it("agregar 16 genes", async () => {
        let receipt = await nftGeneUnique.addGenes([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16], { from: owner });
        console.log(`NftGeneUniqueV1 addGenes Gas: ${receipt.receipt.gasUsed}`);
        const length = await nftGeneUnique.getRangeGeneLength({ from: owner });
        assert.equal(length.toNumber(), 16);
        const result = await nftGeneUnique.getRangeGene({ from: owner });
        assert.equal(result[0].toNumber(), 1);
        assert.equal(result[1].toNumber(), 2);
        assert.equal(result[2].toNumber(), 3);
    });

    it("agregar 3 genes de los cuales 2 son iguales", async () => {
        let receipt = await nftGeneUnique.addGenes([1, 2, 2], { from: owner });
        console.log(`NftGeneUniqueV1 addGenes Gas: ${receipt.receipt.gasUsed}`);
        const length = await nftGeneUnique.getRangeGeneLength({ from: owner });
        assert.equal(length.toNumber(), 2);
        const result = await nftGeneUnique.getRangeGene({ from: owner });
        assert.equal(result[0].toNumber(), 1);
        assert.equal(result[1].toNumber(), 2);
    });

    it("agregar 3 genes de los cuales 3 son iguales", async () => {
        let receipt = await nftGeneUnique.addGenes([5, 5, 5], { from: owner });
        console.log(`NftGeneUniqueV1 addGenes Gas: ${receipt.receipt.gasUsed}`);
        const length = await nftGeneUnique.getRangeGeneLength({ from: owner });
        assert.equal(length.toNumber(), 1);
        const result = await nftGeneUnique.getRangeGene({ from: owner });
        assert.equal(result[0].toNumber(), 5);
    });

    it("agregar 1 gene, mintear nft y volver agregar el mismo gene y que no se agregue", async () => {
        let receipt = await nftGeneUnique.addGenes([69], { from: owner });
        console.log(`NftGeneUniqueV1 addGenes Gas: ${receipt.receipt.gasUsed}`);
        receipt = await nftGeneUnique.createNft(owner, { from: owner });
        console.log(`NftGeneUniqueV1 createNft Gas: ${receipt.receipt.gasUsed}`);
        await nftGeneUnique.addGenes([69], { from: owner });
        const length = await nftGeneUnique.getRangeGeneLength({ from: owner });
        assert.equal(length.toNumber(), 0);
    });

    it("agregar genes primer gene y ultimo, mintear los nft", async () => {
        let genes = [
            "0",
            "18446744073709551615",
        ];
        
        await nftGeneUnique.addGenes(genes, { from: owner });
        let length = await nftGeneUnique.getRangeGeneLength({ from: owner });
        assert.equal(length, genes.length);

        await nftGeneUnique.createNft(owner, { from: owner });
        await nftGeneUnique.createNft(owner, { from: owner });

        length = await nftGeneUnique.getRangeGeneLength({ from: owner });
        assert.equal(length, 0);
    });
});