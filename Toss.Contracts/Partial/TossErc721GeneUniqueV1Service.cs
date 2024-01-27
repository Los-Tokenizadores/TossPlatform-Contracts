using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossErc721GeneUniqueV1;

public partial class TossErc721GeneUniqueV1Service : IErc721Service {
	public string Address => ContractHandler.ContractAddress;
}