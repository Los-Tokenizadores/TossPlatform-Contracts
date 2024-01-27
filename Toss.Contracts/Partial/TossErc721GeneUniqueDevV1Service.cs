using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossErc721GeneUniqueDevV1;

public partial class TossErc721GeneUniqueDevV1Service : IErc721Service {
	public string Address => ContractHandler.ContractAddress;
}