using Toss.Common;
using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossErc721GeneUniqueDevV1.ContractDefinition;
public partial class TossErc721GeneUniqueDevV1Deployment : IContractDefinition {
	public static ContractTypes ContractType => ContractTypes.Erc721GeneUniqueDev;
	public static int Version => 1;
	public static int ClientVersion => 1;
	public static string ContractName => "TossErc721GeneUniqueDevV1";
	public static string SourceCode => sourceCode ??= IContractDefinition.LoadSourceCode(ContractName);
	private static string sourceCode;
}
