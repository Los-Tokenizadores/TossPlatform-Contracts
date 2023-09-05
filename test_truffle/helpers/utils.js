const TossUpgradeableProxy = artifacts.require("TossUpgradeableProxy");

async function shouldThrowAsync(promise, expectError) {
    assert(expectError.length > 0, "expectError needs to have value")
    try {
        await promise;
    }
    catch (error) {
        assert(error.message.search(expectError) >= 0, "Expected throw, got '" + error + "' instead");
        return;
    }
    
    assert.fail("The contract did not throw.");
}

function alignRight(value, size){
    return value.toString().padStart(size).padEnd(size);
}

async function deployContractAndProxyAsync(contract, initData, log = false) {
    const name = contract.contractName;
    
    if(log) console.log(`\t${`${name},`.padEnd(30)}${alignRight(await contract.new.estimateGas(), 10)}`);
    const logic = await contract.new();
    if(log) console.log(`\t${`Proxy ${name},`.padEnd(30)}${alignRight(await TossUpgradeableProxy.new.estimateGas(logic.address, initData.encodeABI()), 10)}`);
    const proxy = await TossUpgradeableProxy.new(logic.address, initData.encodeABI());
    return await contract.at(proxy.address);
}

module.exports = {
    shouldThrowAsync,
    deployContractAndProxyAsync,
    alignRight
};
