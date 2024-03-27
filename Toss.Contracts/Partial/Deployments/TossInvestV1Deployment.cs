using Toss.Common;
using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossInvestV1.ContractDefinition;
public partial class TossInvestV1Deployment : IContractDefinition {
	public static ContractTypes ContractType => ContractTypes.Invest;
	public static int Version => 1;
	public static int ClientVersion => 1;
	public static string ContractName => "TossInvestV1";
	public static string SourceCode => sourceCode ??= IContractDefinition.LoadSourceCode(ContractName);
	private static string sourceCode;
}
