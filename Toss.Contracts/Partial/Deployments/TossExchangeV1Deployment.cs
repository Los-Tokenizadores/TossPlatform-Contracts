using Toss.Common;
using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossExchangeV1.ContractDefinition;
public partial class TossExchangeV1Deployment : IContractDefinition {
	public static ContractTypes ContractType => ContractTypes.Exchange;
	public static int Version => 1;
	public static int ClientVersion => 1;
	public static string ContractName => "TossExchangeV1";
	public static string CompileVersion => SolidityCompilerVersions.V0_8_20;
	public static string SourceCode => sourceCode ??= IContractDefinition.LoadSourceCode(ContractName);
	private static string sourceCode;
}
