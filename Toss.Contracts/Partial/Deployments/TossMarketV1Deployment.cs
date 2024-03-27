using Toss.Common;
using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossMarketV1.ContractDefinition;
public partial class TossMarketV1Deployment : IContractDefinition {
	public static ContractTypes ContractType => ContractTypes.Market;
	public static int Version => 1;
	public static int ClientVersion => 1;
	public static string ContractName => "TossMarketV1";
	public static string SourceCode => sourceCode ??= IContractDefinition.LoadSourceCode(ContractName);
	private static string sourceCode;
}
