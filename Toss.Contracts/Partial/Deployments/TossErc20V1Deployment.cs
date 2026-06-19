using Toss.Common;
using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossErc20V1.ContractDefinition;

public partial class TossErc20V1Deployment : IContractDefinition {
	private static string sourceCode;
	public static ContractTypes ContractType => ContractTypes.Erc20Toss;
	public static bool IsProxy => false;
	public static int Version => 1;
	public static int ClientVersion => 1;
	public static string ContractName => "TossErc20V1";
	public static string SourceCode => sourceCode ??= IContractDefinition.LoadSourceCode(ContractName);
}