using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossSellerV1;

public partial class TossSellerV1Service : ISellerService {
	public string Address => ContractHandler.ContractAddress;
}
