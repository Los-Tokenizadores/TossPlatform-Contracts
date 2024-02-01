using Toss.Common;
using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossWhitelistV1.ContractDefinition;
public partial class TossWhitelistV1Deployment : IContractDefinition {
	public static ContractTypes ContractType => ContractTypes.Whitelist;
	public static int Version => 1;
	public static int ClientVersion => 1;
	public static string ContractName => "TossWhitelistV1";
	public static string CompileVersion => SolidityCompilerVersions.V0_8_20;
	public static string SourceCode => sourceCode ??= IContractDefinition.LoadSourceCode(ContractName);
	private static string sourceCode;
}
