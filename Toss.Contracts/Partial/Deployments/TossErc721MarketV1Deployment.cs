using Toss.Common;
using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossErc721MarketV1.ContractDefinition;

public partial class TossErc721MarketV1Deployment : IContractDefinition {
	private static string sourceCode;
	public static ContractTypes ContractType => ContractTypes.Erc721Market;
	public static bool IsProxy => false;
	public static int Version => 1;
	public static int ClientVersion => 1;
	public static string ContractName => "TossErc721MarketV1";
	public static string SourceCode => sourceCode ??= IContractDefinition.LoadSourceCode(ContractName);
}