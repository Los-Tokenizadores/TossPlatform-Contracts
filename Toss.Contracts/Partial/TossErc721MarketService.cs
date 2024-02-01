using Nethereum.RPC.Eth.DTOs;
using System.Numerics;
using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossErc721MarketV1;

public partial class TossErc721MarketV1Service : IErc721Service {
	public string Address => ContractHandler.ContractAddress;

	public Task<TransactionReceipt> AddGenesRequestAndWaitForReceiptAsync(List<BigInteger> list, CancellationTokenSource cancellationToken = null) {
		throw new NotImplementedException();
	}

	public Task<BigInteger> GetRangeGeneLengthQueryAsync(BlockParameter blockParameter = null) {
		return Task.FromResult(BigInteger.Zero);
	}

	public Task<List<BigInteger>> GetRangeGeneQueryAsync(BlockParameter blockParameter = null) {
		throw new NotImplementedException();
	}
}
