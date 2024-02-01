using Nethereum.RPC.Eth.DTOs;

namespace Toss.Contracts.Interfaces;

public interface IHasWhitelist {
	Task<string> GetWhitelistQueryAsync(BlockParameter blockParameter = null);
	Task<TransactionReceipt> SetWhitelistRequestAndWaitForReceiptAsync(string newAddress, CancellationTokenSource cancellationToken = null);
}