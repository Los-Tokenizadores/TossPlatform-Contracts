using Toss.Common;
using Toss.Contracts.Interfaces;

namespace Toss.Contracts.TossSellerV1.ContractDefinition;
public partial class TossSellerV1Deployment : IContractDefinition {
	public static ContractTypes ContractType => ContractTypes.Seller;
	public static int Version => 1;
	public static int ClientVersion => 1;
	public static string ContractName => "TossSellerV1";
	public static string SourceCode => sourceCode ??= IContractDefinition.LoadSourceCode(ContractName);
	private static string sourceCode;
}
