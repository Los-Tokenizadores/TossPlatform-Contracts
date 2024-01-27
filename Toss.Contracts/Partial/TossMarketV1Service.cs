using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossMarketV1;

public partial class TossMarketV1Service : IMarketService {
	public string Address => ContractHandler.ContractAddress;
}
