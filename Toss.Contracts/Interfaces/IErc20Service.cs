using Nethereum.RPC.Eth.DTOs;
using System.Numerics;

namespace Toss.Contracts.Interfaces;

public interface IErc20Service : IHasContractHandler, IHasWhitelist {
	Task<int> GetDecimalsAsync();

	Task<BigInteger> AllowanceQueryAsync(string owner, string spender, BlockParameter blockParameter = null);
	Task<BigInteger> NoncesQueryAsync(string owner, BlockParameter blockParameter = null);
	Task<TransactionReceipt> ApproveRequestAndWaitForReceiptAsync(string spender, BigInteger amount, CancellationTokenSource cancellationToken = null);
	Task<BigInteger> BalanceOfQueryAsync(string account, BlockParameter blockParameter = null);
	Task<string> NameQueryAsync(BlockParameter blockParameter = null);
	Task<string> SymbolQueryAsync(BlockParameter blockParameter = null);
	Task<BigInteger> TotalSupplyQueryAsync(BlockParameter blockParameter = null);
	Task<TransactionReceipt> TransferRequestAndWaitForReceiptAsync(string to, BigInteger amount, CancellationTokenSource cancellationToken = null);
	Task<TransactionReceipt> PermitRequestAndWaitForReceiptAsync(string owner, string spender, BigInteger value, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null);
}