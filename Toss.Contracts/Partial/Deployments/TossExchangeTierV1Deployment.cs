using Toss.Common;
using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossExchangeTierV1.ContractDefinition;

public partial class TossExchangeTierV1Deployment : IContractDefinition {
	private static string sourceCode;
	public static ContractTypes ContractType => ContractTypes.ExchangeTier;
	public static bool IsProxy => false;
	public static int Version => 1;
	public static int ClientVersion => 1;
	public static string ContractName => "TossExchangeTierV1";
	public static string SourceCode => sourceCode ??= IContractDefinition.LoadSourceCode(ContractName);
}