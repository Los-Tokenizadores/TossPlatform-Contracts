using Nethereum.RPC.Eth.DTOs;
using System.Numerics;

namespace Toss.Contracts.Interfaces;

public interface IErc721Service : IHasContractHandler, IHasWhitelist {
	Task<TransactionReceipt> AddGenesRequestAndWaitForReceiptAsync(List<BigInteger> list, CancellationTokenSource cancellationToken = null);
	Task<TransactionReceipt> CreateSellOfferRequestAndWaitForReceiptAsync(BigInteger tokenId, BigInteger price, CancellationTokenSource cancellationToken = null);
	Task<TransactionReceipt> SetMarketRequestAndWaitForReceiptAsync(string contractAddress, CancellationTokenSource cancellationToken = null);
	Task<BigInteger> BalanceOfQueryAsync(string selectedAccount, BlockParameter blockParameter = null);
	Task<BigInteger> GetRangeGeneLengthQueryAsync(BlockParameter blockParameter = null);
	Task<List<BigInteger>> GetRangeGeneQueryAsync(BlockParameter blockParameter = null);
	Task<string> NameQueryAsync(BlockParameter blockParameter = null);
	Task<string> GetMarketQueryAsync(BlockParameter blockParameter = null);

	Task<byte[]> MinterRoleQueryAsync(BlockParameter blockParameter = null);
	Task<bool> HasRoleQueryAsync(byte[] role, string account, BlockParameter blockParameter = null);
	Task<TransactionReceipt> GrantRoleRequestAndWaitForReceiptAsync(byte[] role, string account, CancellationTokenSource cancellationToken = null);
	Task<TransactionReceipt> RevokeRoleRequestAndWaitForReceiptAsync(byte[] role, string account, CancellationTokenSource cancellationToken = null);

	Task<string> GetBaseUriQueryAsync(BlockParameter blockParameter = null);
	Task<TransactionReceipt> SetBaseUriRequestAndWaitForReceiptAsync(string baseuri, CancellationTokenSource cancellationToken = null);
	Task<string> OwnerOfQueryAsync(BigInteger tokenId, BlockParameter blockParameter = null);
}
