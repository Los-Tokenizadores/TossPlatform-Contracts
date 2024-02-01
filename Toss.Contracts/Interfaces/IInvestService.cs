using Nethereum.RPC.Eth.DTOs;
using System.Numerics;
using Toss.Contracts.TossInvestV1.ContractDefinition;

namespace Toss.Contracts.Interfaces;

public interface IInvestService : IHasContractHandler, IHasWhitelist {
	Task<string> GetErc20QueryAsync(BlockParameter blockParameter = null);
	Task<GetProjectOutputDTO> GetProjectQueryAsync(BigInteger projectId, BlockParameter blockParameter = null);
	Task<TransactionReceipt> InvestRequestAndWaitForReceiptAsync(BigInteger projectId, ushort amount, CancellationTokenSource cancellationToken = null);
	Task<TransactionReceipt> InvestWithPermitRequestAndWaitForReceiptAsync(BigInteger projectId, ushort investAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null);
	Task<TransactionReceipt> ConfirmRequestAndWaitForReceiptAsync(BigInteger projectId, CancellationTokenSource cancellationToken = null);
	Task<GetProjectByErc721AddressOutputDTO> GetProjectByErc721AddressQueryAsync(string erc721Address, BlockParameter blockParameter = null);
}