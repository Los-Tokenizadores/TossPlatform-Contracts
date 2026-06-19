using Toss.Common;
using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossInvestV1.ContractDefinition;

public partial class TossInvestV1Deployment : IContractDefinition {
	private static string sourceCode;
	public static ContractTypes ContractType => ContractTypes.Invest;
	public static bool IsProxy => false;
	public static int Version => 1;
	public static int ClientVersion => 1;
	public static string ContractName => "TossInvestV1";
	public static string SourceCode => sourceCode ??= IContractDefinition.LoadSourceCode(ContractName);
}