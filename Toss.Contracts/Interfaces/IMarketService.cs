using Nethereum.RPC.Eth.DTOs;
using System.Numerics;

namespace Toss.Contracts.Interfaces;

public interface IMarketService : IHasContractHandler, IHasWhitelist {
	Task<TransactionReceipt> BuyRequestAndWaitForReceiptAsync(string address, BigInteger erc721Id, BigInteger price, CancellationTokenSource cancellationToken = null);
	Task<TransactionReceipt> BuyWithPermitRequestAndWaitForReceiptAsync(string erc721Address, BigInteger tokenId, BigInteger price, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null);
	Task<TransactionReceipt> CancelRequestAndWaitForReceiptAsync(string erc721Address, BigInteger erc721Id, CancellationTokenSource cancellationToken = null);
	Task<string> GetErc20QueryAsync(BlockParameter blockParameter = null);
	Task<BigInteger> GetPriceQueryAsync(string address, BigInteger erc721Id, BlockParameter blockParameter = null);
}