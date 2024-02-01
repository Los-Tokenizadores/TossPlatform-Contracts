using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossExchangeTierV1;

public partial class TossExchangeTierV1Service : IExchangeService {
	public string Address => ContractHandler.ContractAddress;
}
