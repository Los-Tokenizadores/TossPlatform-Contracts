using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossErc20V1;
public partial class TossErc20V1Service : IErc20Service {
	public string Address => ContractHandler.ContractAddress;

	private int? cacheDecimals = null;


	public async Task<int> GetDecimalsAsync() {
		cacheDecimals ??= await DecimalsQueryAsync();
		return cacheDecimals.Value;
	}
}
