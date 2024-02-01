using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossWhitelistV1;

public partial class TossWhitelistV1Service : IWhitelistService {
	public string Address => ContractHandler.ContractAddress;
}
