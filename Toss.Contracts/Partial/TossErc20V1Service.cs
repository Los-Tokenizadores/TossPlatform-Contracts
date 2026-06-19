using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossErc20V1;

public partial class TossErc20V1Service : IErc20Service {
	private int? cacheDecimals;
	public string Address => ContractHandler.ContractAddress;


	public async Task<int> GetDecimalsAsync() {
		cacheDecimals ??= await DecimalsQueryAsync();
		return cacheDecimals.Value;
	}
}