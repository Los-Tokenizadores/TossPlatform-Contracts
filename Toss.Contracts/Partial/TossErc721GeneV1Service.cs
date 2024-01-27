using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossErc721GeneV1;

public partial class TossErc721GeneV1Service : IErc721Service {
	public string Address => ContractHandler.ContractAddress;
}
