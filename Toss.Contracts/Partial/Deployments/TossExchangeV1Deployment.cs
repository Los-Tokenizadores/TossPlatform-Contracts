using Toss.Common;
using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossExchangeV1.ContractDefinition;

public partial class TossExchangeV1Deployment : IContractDefinition {
	private static string sourceCode;
	public static ContractTypes ContractType => ContractTypes.Exchange;
	public static bool IsProxy => false;
	public static int Version => 1;
	public static int ClientVersion => 1;
	public static string ContractName => "TossExchangeV1";
	public static string SourceCode => sourceCode ??= IContractDefinition.LoadSourceCode(ContractName);
}