using Toss.Common;
using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossErc721GeneV1.ContractDefinition;
public partial class TossErc721GeneV1Deployment : IContractDefinition {
	public static ContractTypes ContractType => ContractTypes.Erc721Gene;
	public static int Version => 1;
	public static int ClientVersion => 1;
	public static string ContractName => "TossErc721GeneV1";
	public static string SourceCode => sourceCode ??= IContractDefinition.LoadSourceCode(ContractName);
	private static string sourceCode;
}
