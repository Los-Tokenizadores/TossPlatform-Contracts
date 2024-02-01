using Nethereum.RPC.Eth.DTOs;
using System.Numerics;
using Toss.Contracts.TossSellerV1.ContractDefinition;

namespace Toss.Contracts.Interfaces;

public interface ISellerService : IHasContractHandler, IHasWhitelist {
	Task<TransactionReceipt> BuyErc721RequestAndWaitForReceiptAsync(string erc721Address, byte amount, CancellationTokenSource cancellationToken = null);
	Task<TransactionReceipt> BuyErc721WithPermitRequestAndWaitForReceiptAsync(string erc721, byte buyAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null);
	Task<TransactionReceipt> ConvertToOffchainRequestAndWaitForReceiptAsync(BigInteger bigAmount, CancellationTokenSource cancellationToken = null);
	Task<TransactionReceipt> ConvertToOffchainWithPermitRequestAndWaitForReceiptAsync(BigInteger erc20Amount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null);
	Task<ushort> GetConvertToErc20CutQueryAsync(BlockParameter blockParameter = null);
	Task<uint> GetConvertToErc20MinAmountQueryAsync(BlockParameter blockParameter = null);
	Task<uint> GetConvertToErc20RateQueryAsync(BlockParameter blockParameter = null);
	Task<ushort> GetConvertToOffchainCutQueryAsync(BlockParameter blockParameter = null);
	Task<BigInteger> GetConvertToOffchainMinAmountQueryAsync(BlockParameter blockParameter = null);
	Task<uint> GetConvertToOffchainRateQueryAsync(BlockParameter blockParameter = null);
	Task<string> GetErc20QueryAsync(BlockParameter blockParameter = null);
	Task<GetErc721SellsOutputDTO> GetErc721SellsQueryAsync(string erc721, BlockParameter blockParameter = null);
}