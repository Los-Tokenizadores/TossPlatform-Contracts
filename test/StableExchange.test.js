const { deployContractAndProxyAsync, shouldThrowAsync } = require("./helpers/utils");
var chai = require('chai');
var expect = chai.expect;
var BN = require('bn.js');
var bnChai = require('bn-chai');
const { assert } = require("chai");
chai.use(bnChai(BN));

const CoinV1 = artifacts.require("CoinV1");
const WhitelistV1 = artifacts.require("WhitelistV1");
const StableExchangeV1 = artifacts.require("StableExchangeV1");

const ZERO_ADDRESS = '0x0000000000000000000000000000000000000000';

contract("Proxy", (accounts) => {
    let [owner] = accounts;

    let internal;
    let stable;
    let stableExchange;
    let whitelist;

    beforeEach(async () => {
        stable = await deployContractAndProxyAsync(CoinV1, new CoinV1('').contract.methods.__CoinV1_init('Stable', 'S', 99999999), true);
        internal = await deployContractAndProxyAsync(CoinV1, new CoinV1('').contract.methods.__CoinV1_init('Coin', 'C', 0), true);
        stableExchange = await deployContractAndProxyAsync(StableExchangeV1, new StableExchangeV1('').contract.methods.__StableExchangeV1_init(stable.address, internal.address), true);
        whitelist = await deployContractAndProxyAsync(WhitelistV1, new WhitelistV1('').contract.methods.__WhitelistV1_init());

        await internal.setWhitelist(whitelist.address);
        await whitelist.set(owner, true);
        //await whitelist.set(stableExchange.address, true);
        await internal.grantRole(await internal.MINTER_ROLE(), stableExchange.address);
    });
    
    
    it("Convertir de Stable a Internal 1000 y volver a stable quedando todos los balances iniciales", async () => {
        await stable.approve(stableExchange.address, 1000);
        console.log(await stableExchange.convertToInternal.estimateGas(1000));
        await stableExchange.convertToInternal(1000);

        expect(await internal.balanceOf(owner)).to.eq.BN(1000);
    
        await internal.approve(stableExchange.address, 1000);
        console.log(await stableExchange.convertToStable.estimateGas(1000));
        await stableExchange.convertToStable(1000);

        expect(await stable.balanceOf(owner)).to.eq.BN(web3.utils.toWei('99999999'));
    });

    it("Transferir a addres 0 deberia fallar", async () => {
        await stable.approve(stableExchange.address, 1000);
        console.log(await stableExchange.convertToInternal.estimateGas(1000));
        await stableExchange.convertToInternal(1000);

        expect(await internal.balanceOf(owner)).to.eq.BN(1000);
    
        await shouldThrowAsync(internal.transfer(ZERO_ADDRESS, 1000), "Reason given: ERC20: transfer to the zero address");
    });
});