using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossExchangeV1;

public partial class TossExchangeV1Service : IExchangeService {
	public string Address => ContractHandler.ContractAddress;
}
