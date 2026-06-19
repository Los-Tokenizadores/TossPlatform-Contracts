using Nethereum.RPC.Eth.DTOs;
using Toss.Contracts.Interfaces;

namespace Toss.Contracts.ExternalErc20;

public partial class ExternalErc20Service : IErc20Service {
	private int? cacheDecimals;
	public string Address => ContractHandler.ContractAddress;

	public async Task<int> GetDecimalsAsync() {
		cacheDecimals ??= await DecimalsQueryAsync();
		return cacheDecimals.Value;
	}

	public Task<TransactionReceipt> SetWhitelistRequestAndWaitForReceiptAsync(string newAddress, CancellationTokenSource cancellationToken = null) {
		throw new NotImplementedException();
	}

	public Task<string> GetWhitelistQueryAsync(BlockParameter blockParameter = null) {
		throw new NotImplementedException();
	}
}