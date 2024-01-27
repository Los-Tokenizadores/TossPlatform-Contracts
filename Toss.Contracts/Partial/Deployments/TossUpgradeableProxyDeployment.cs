using Toss.Common;
using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossUpgradeableProxy.ContractDefinition;
public partial class TossUpgradeableProxyDeployment : IContractDefinition {
	public static ContractTypes ContractType => ContractTypes.Proxy;
	public static int Version => 1;
	public static int ClientVersion => 1;
	public static string ContractName => "TossUpgradeableProxy";
	public static string CompileVersion => SolidityCompilerVersions.V0_8_20;
	public static string SourceCode => sourceCode ??= IContractDefinition.LoadSourceCode(ContractName);
	private static string sourceCode;
}
