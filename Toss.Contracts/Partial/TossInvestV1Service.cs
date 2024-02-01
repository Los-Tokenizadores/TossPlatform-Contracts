using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossInvestV1;

public partial class TossInvestV1Service : IInvestService {
	public string Address => ContractHandler.ContractAddress;
}
