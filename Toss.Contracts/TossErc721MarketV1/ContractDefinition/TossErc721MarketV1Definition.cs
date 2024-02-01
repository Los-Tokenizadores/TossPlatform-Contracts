using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace Toss.Contracts.TossErc721MarketV1.ContractDefinition
{


    public partial class TossErc721MarketV1Deployment : TossErc721MarketV1DeploymentBase
    {
        public TossErc721MarketV1Deployment() : base(BYTECODE) { }
        public TossErc721MarketV1Deployment(string byteCode) : base(byteCode) { }
    }

    public class TossErc721MarketV1DeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60a06040523060805234801562000014575f80fd5b506200001f6200002f565b620000296200002f565b620000e3565b7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a00805468010000000000000000900460ff1615620000805760405163f92ee8a960e01b815260040160405180910390fd5b80546001600160401b0390811614620000e05780546001600160401b0319166001600160401b0390811782556040519081527fc7f505b2f371ae2175ee4913f4499e1f2633a7b5936321eed1cdaeb6115181d29060200160405180910390a15b50565b6080516127cd6200010a5f395f81816113340152818161135d01526114aa01526127cd5ff3fe60806040526004361061021d575f3560e01c80638456cb591161011e578063ad3cb1cc116100a8578063d547741f1161006d578063d547741f14610623578063e63ab1e914610642578063e985e9c514610662578063f1be167914610681578063f72c0d8b14610695575f80fd5b8063ad3cb1cc1461056e578063b88d4fde1461059e578063c87b56dd146105bd578063d01f63f5146105dc578063d5391393146105f0575f80fd5b8063a0bcfc7f116100ee578063a0bcfc7f146104ea578063a144819414610509578063a217fddf14610528578063a22cb4651461053b578063aaf10f421461055a575f80fd5b80638456cb5914610484578063854cff2f1461049857806391d14854146104b757806395d89b41146104d6575f80fd5b806336568abe116101aa5780635c975abb1161016f5780635c975abb146103e55780636352211e146104085780636c128a21146104275780636dcea85f1461044657806370a0823114610465575f80fd5b806336568abe1461036c5780633f4ba83a1461038b57806342842e0e1461039f5780634f1ef286146103be57806352d1902d146103d1575f80fd5b80630cac36b2116101f05780630cac36b2146102ce57806323b872dd146102e2578063248a9ca314610301578063251a29551461032e5780632f2ff15d1461034d575f80fd5b806301ffc9a71461022157806306fdde0314610255578063081812fc14610276578063095ea7b3146102ad575b5f80fd5b34801561022c575f80fd5b5061024061023b3660046120eb565b6106c8565b60405190151581526020015b60405180910390f35b348015610260575f80fd5b506102696106d8565b60405161024c9190612153565b348015610281575f80fd5b50610295610290366004612165565b610779565b6040516001600160a01b03909116815260200161024c565b3480156102b8575f80fd5b506102cc6102c7366004612190565b61078d565b005b3480156102d9575f80fd5b5061026961079c565b3480156102ed575f80fd5b506102cc6102fc3660046121ba565b6107b6565b34801561030c575f80fd5b5061032061031b366004612165565b610844565b60405190815260200161024c565b348015610339575f80fd5b506102cc610348366004612295565b610864565b348015610358575f80fd5b506102cc6103673660046122f5565b610974565b348015610377575f80fd5b506102cc6103863660046122f5565b610990565b348015610396575f80fd5b506102cc6109c8565b3480156103aa575f80fd5b506102cc6103b93660046121ba565b6109ea565b6102cc6103cc366004612323565b610a04565b3480156103dc575f80fd5b50610320610a1f565b3480156103f0575f80fd5b505f805160206127388339815191525460ff16610240565b348015610413575f80fd5b50610295610422366004612165565b610a3a565b348015610432575f80fd5b506102cc610441366004612366565b610a44565b348015610451575f80fd5b506102cc610460366004612394565b610b24565b348015610470575f80fd5b5061032061047f366004612394565b610c30565b34801561048f575f80fd5b506102cc610c88565b3480156104a3575f80fd5b506102cc6104b2366004612394565b610ca7565b3480156104c2575f80fd5b506102406104d13660046122f5565b610cba565b3480156104e1575f80fd5b50610269610cf0565b3480156104f5575f80fd5b506102cc6105043660046123af565b610d2e565b348015610514575f80fd5b506102cc610523366004612190565b610d63565b348015610533575f80fd5b506103205f81565b348015610546575f80fd5b506102cc6105553660046123ee565b610deb565b348015610565575f80fd5b50610295610df6565b348015610579575f80fd5b50610269604051806040016040528060058152602001640352e302e360dc1b81525081565b3480156105a9575f80fd5b506102cc6105b836600461241a565b610e04565b3480156105c8575f80fd5b506102696105d7366004612165565b610e1b565b3480156105e7575f80fd5b50610295610e80565b3480156105fb575f80fd5b506103207f9f2df0fed2c77648de5860a4cc508cd0818c85b8b8a1ab4ceeef8d981c8956a681565b34801561062e575f80fd5b506102cc61063d3660046122f5565b610eb2565b34801561064d575f80fd5b506103205f805160206126f883398151915281565b34801561066d575f80fd5b5061024061067c366004612482565b610ece565b34801561068c575f80fd5b50610295610f1a565b3480156106a0575f80fd5b506103207f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e381565b5f6106d282610f2e565b92915050565b5f805160206126b883398151915280546060919081906106f7906124ae565b80601f0160208091040260200160405190810160405280929190818152602001828054610723906124ae565b801561076e5780601f106107455761010080835404028352916020019161076e565b820191905f5260205f20905b81548152906001019060200180831161075157829003601f168201915b505050505091505090565b5f61078382610f52565b506106d282610f89565b610798828233610fc2565b5050565b60605f6107a881610fcf565b6107b0610fd9565b91505090565b6001600160a01b0382166107e457604051633250574960e11b81525f60048201526024015b60405180910390fd5b5f6107f0838333611078565b9050836001600160a01b0316816001600160a01b03161461083e576040516364283d7b60e01b81526001600160a01b03808616600483015260248201849052821660448201526064016107db565b50505050565b5f9081525f80516020612718833981519152602052604090206001015490565b7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a008054600160401b810460ff16159067ffffffffffffffff165f811580156108a95750825b90505f8267ffffffffffffffff1660011480156108c55750303b155b9050811580156108d3575080155b156108f15760405163f92ee8a960e01b815260040160405180910390fd5b845467ffffffffffffffff19166001178555831561091b57845460ff60401b1916600160401b1785555b6109258787611176565b831561096b57845460ff60401b19168555604051600181527fc7f505b2f371ae2175ee4913f4499e1f2633a7b5936321eed1cdaeb6115181d29060200160405180910390a15b50505050505050565b61097d82610844565b61098681610fcf565b61083e83836111b0565b6001600160a01b03811633146109b95760405163334bd91960e11b815260040160405180910390fd5b6109c38282611251565b505050565b5f805160206126f88339815191526109df81610fcf565b6109e76112ca565b50565b6109c383838360405180602001604052805f815250610e04565b610a0c611329565b610a15826113b9565b61079882826113e3565b5f610a2861149f565b505f805160206126d883398151915290565b5f6106d282610f52565b610a4c6114e8565b610a5461151f565b5f8051602061277883398151915280546001600160a01b0316610a8a5760405163503b6c8360e11b815260040160405180910390fd5b8054610aa0906001600160a01b03168433610fc2565b80546040516325a0399f60e01b8152600481018590526001600160801b03841660248201523360448201526001600160a01b03909116906325a0399f906064015f604051808303815f87803b158015610af7575f80fd5b505af1158015610b09573d5f803e3d5ffd5b505050505061079860015f8051602061275883398151915255565b610b2c6114e8565b5f610b3681610fcf565b6001600160a01b03821615801590610bb857506040516301ffc9a760e01b81526325a0399f60e01b60048201526001600160a01b038316906301ffc9a790602401602060405180830381865afa158015610b92573d5f803e3d5ffd5b505050506040513d601f19601f82011682018060405250810190610bb691906124e6565b155b15610bf457604051631e1b455160e01b815260206004820152600b60248201526a12551bdcdcd3585c9ad95d60aa1b60448201526064016107db565b505f8051602061277883398151915280546001600160a01b0319166001600160a01b03831617905560015f805160206127588339815191525550565b5f5f805160206126b88339815191526001600160a01b038316610c68576040516322718ad960e21b81525f60048201526024016107db565b6001600160a01b039092165f908152600390920160205250604090205490565b5f805160206126f8833981519152610c9f81610fcf565b6109e7611562565b5f610cb181610fcf565b610798826115aa565b5f9182525f80516020612718833981519152602090815260408084206001600160a01b0393909316845291905290205460ff1690565b7f80bb2b638cc20bc4d0a60d66940f3ab4a00c1d7b313497ca82fb0b4ab007930180546060915f805160206126b8833981519152916106f7906124ae565b5f610d3881610fcf565b7f7632e33b12507a4855f6678ca0e8955963b6dfcb053a9dd743817368145254016109c3838261254e565b610d6b6114e8565b7f9f2df0fed2c77648de5860a4cc508cd0818c85b8b8a1ab4ceeef8d981c8956a6610d9581610fcf565b60405182906001600160a01b038516907f0ce3610e89a4bb9ec9359763f99110ed52a4abaea0b62028a1637e242ca2768b905f90a3610dd483836115ee565b5061079860015f8051602061275883398151915255565b610798338383611607565b5f610dff6116b6565b905090565b610e0f8484846107b6565b61083e848484846116ca565b6060610e2682610f52565b505f610e30610fd9565b90505f815111610e4e5760405180602001604052805f815250610e79565b80610e58846117f0565b604051602001610e6992919061260a565b6040516020818303038152906040525b9392505050565b5f7fe32c7d464d01bf4699c6877240dec52b29adf1b2f9e414a502cf561c22d22d005b546001600160a01b0316919050565b610ebb82610844565b610ec481610fcf565b61083e8383611251565b6001600160a01b039182165f9081527f80bb2b638cc20bc4d0a60d66940f3ab4a00c1d7b313497ca82fb0b4ab00793056020908152604080832093909416825291909152205460ff1690565b5f5f80516020612778833981519152610ea3565b5f6001600160e01b03198216637965db0b60e01b14806106d257506106d282611880565b5f80610f5d836118cf565b90506001600160a01b0381166106d257604051637e27328960e01b8152600481018490526024016107db565b5f9081527f80bb2b638cc20bc4d0a60d66940f3ab4a00c1d7b313497ca82fb0b4ab007930460205260409020546001600160a01b031690565b6109c38383836001611908565b6109e78133611a1b565b60605f805160206127788339815191526001018054610ff7906124ae565b80601f0160208091040260200160405190810160405280929190818152602001828054611023906124ae565b801561106e5780601f106110455761010080835404028352916020019161106e565b820191905f5260205f20905b81548152906001019060200180831161105157829003601f168201915b5050505050905090565b5f837fe32c7d464d01bf4699c6877240dec52b29adf1b2f9e414a502cf561c22d22d006001600160a01b038216158015906110bc575080546001600160a01b031615155b8015611130575080546040516304fec10960e11b81526001600160a01b038481166004830152909116906309fd821290602401602060405180830381865afa15801561110a573d5f803e3d5ffd5b505050506040513d601f19601f8201168201806040525081019061112e91906124e6565b155b1561115957604051632dbcdb8b60e01b81526001600160a01b03831660048201526024016107db565b61116161151f565b61116c868686611a54565b9695505050505050565b61117e611a70565b6111888282611ab9565b611190611acb565b611198611adb565b6111a0611ae3565b6111a8611af3565b610798611b03565b5f5f805160206127188339815191526111c98484610cba565b611248575f848152602082815260408083206001600160a01b03871684529091529020805460ff191660011790556111fe3390565b6001600160a01b0316836001600160a01b0316857f2f8788117e7eff1d82e926ec794901d17c78024a50270940304540a733656f0d60405160405180910390a460019150506106d2565b5f9150506106d2565b5f5f8051602061271883398151915261126a8484610cba565b15611248575f848152602082815260408083206001600160a01b0387168085529252808320805460ff1916905551339287917ff6391f5c32d9c69d2a47ea670b442974b53935d1edc7fd64eb21e047a839171b9190a460019150506106d2565b6112d2611b83565b5f80516020612738833981519152805460ff191681557f5db9ee0a495bf2e6ff9c91a7834c1ba4fdd244a5e8aa4e537bd38aeae4b073aa335b6040516001600160a01b03909116815260200160405180910390a150565b306001600160a01b037f000000000000000000000000000000000000000000000000000000000000000016148061139957507f00000000000000000000000000000000000000000000000000000000000000006001600160a01b031661138d6116b6565b6001600160a01b031614155b156113b75760405163703e46dd60e11b815260040160405180910390fd5b565b7f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e361079881610fcf565b816001600160a01b03166352d1902d6040518163ffffffff1660e01b8152600401602060405180830381865afa92505050801561143d575060408051601f3d908101601f1916820190925261143a91810190612638565b60015b61146557604051634c9c8ce360e01b81526001600160a01b03831660048201526024016107db565b5f805160206126d8833981519152811461149557604051632a87526960e21b8152600481018290526024016107db565b6109c38383611bb2565b306001600160a01b037f000000000000000000000000000000000000000000000000000000000000000016146113b75760405163703e46dd60e11b815260040160405180910390fd5b5f8051602061275883398151915280546001190161151957604051633ee5aeb560e01b815260040160405180910390fd5b60029055565b5f805160206127388339815191525460ff16156113b75760405163d93c066560e01b815260040160405180910390fd5b60015f8051602061275883398151915255565b61156a61151f565b5f80516020612738833981519152805460ff191660011781557f62e78cea01bee320cd4e420270b5ea74000d11b0c9f74754ebdbfc544b05a2583361130b565b807fe32c7d464d01bf4699c6877240dec52b29adf1b2f9e414a502cf561c22d22d005b80546001600160a01b0319166001600160a01b039290921691909117905550565b610798828260405180602001604052805f815250611c07565b5f805160206126b88339815191526001600160a01b03831661164757604051630b61174360e31b81526001600160a01b03841660048201526024016107db565b6001600160a01b038481165f818152600584016020908152604080832094881680845294825291829020805460ff191687151590811790915591519182527f17307eab39ab6107e8899845ad3d59bd9653f200f220920489ca2b5937696c31910160405180910390a350505050565b5f5f805160206126d8833981519152610ea3565b6001600160a01b0383163b1561083e57604051630a85bd0160e11b81526001600160a01b0384169063150b7a029061170c90339088908790879060040161264f565b6020604051808303815f875af1925050508015611746575060408051601f3d908101601f1916820190925261174391810190612681565b60015b6117ad573d808015611773576040519150601f19603f3d011682016040523d82523d5f602084013e611778565b606091505b5080515f036117a557604051633250574960e11b81526001600160a01b03851660048201526024016107db565b805181602001fd5b6001600160e01b03198116630a85bd0160e11b146117e957604051633250574960e11b81526001600160a01b03851660048201526024016107db565b5050505050565b60605f6117fc83611c1d565b60010190505f8167ffffffffffffffff81111561181b5761181b6121f8565b6040519080825280601f01601f191660200182016040528015611845576020820181803683370190505b5090508181016020015b5f19016f181899199a1a9b1b9c1cb0b131b232b360811b600a86061a8153600a850494508461184f57509392505050565b5f6001600160e01b031982166380ac58cd60e01b14806118b057506001600160e01b03198216635b5e139f60e01b145b806106d257506301ffc9a760e01b6001600160e01b03198316146106d2565b5f9081527f80bb2b638cc20bc4d0a60d66940f3ab4a00c1d7b313497ca82fb0b4ab007930260205260409020546001600160a01b031690565b5f805160206126b8833981519152818061192a57506001600160a01b03831615155b156119eb575f61193985610f52565b90506001600160a01b038416158015906119655750836001600160a01b0316816001600160a01b031614155b801561197857506119768185610ece565b155b156119a15760405163a9fbf51f60e01b81526001600160a01b03851660048201526024016107db565b82156119e95784866001600160a01b0316826001600160a01b03167f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b92560405160405180910390a45b505b5f93845260040160205250506040902080546001600160a01b0319166001600160a01b0392909216919091179055565b611a258282610cba565b6107985760405163e2517d3f60e01b81526001600160a01b0382166004820152602481018390526044016107db565b5f611a5d61151f565b611a68848484611cf4565b949350505050565b7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a0054600160401b900460ff166113b757604051631afcd79f60e31b815260040160405180910390fd5b611ac1611a70565b6107988282611df6565b611ad3611a70565b6113b7611e26565b6113b7611a70565b611aeb611a70565b6113b7611e46565b611afb611a70565b6113b7611adb565b611b0b611a70565b611b155f336111b0565b50611b407f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e3336111b0565b50611b585f805160206126f8833981519152336111b0565b506109e77f9f2df0fed2c77648de5860a4cc508cd0818c85b8b8a1ab4ceeef8d981c8956a6336111b0565b5f805160206127388339815191525460ff166113b757604051638dfc202b60e01b815260040160405180910390fd5b611bbb82611e4e565b6040516001600160a01b038316907fbc7cd75a20ee27fd9adebab32041f755214dbc6bffa90cc0225b39da2e5c2d3b905f90a2805115611bff576109c38282611e97565b610798611f09565b611c118383611f28565b6109c35f8484846116ca565b5f8072184f03e93ff9f4daa797ed6e38ed64bf6a1f0160401b8310611c5b5772184f03e93ff9f4daa797ed6e38ed64bf6a1f0160401b830492506040015b6d04ee2d6d415b85acef81000000008310611c87576d04ee2d6d415b85acef8100000000830492506020015b662386f26fc100008310611ca557662386f26fc10000830492506010015b6305f5e1008310611cbd576305f5e100830492506008015b6127108310611cd157612710830492506004015b60648310611ce3576064830492506002015b600a83106106d25760010192915050565b5f5f805160206126b883398151915281611d0d856118cf565b90506001600160a01b03841615611d2957611d29818587611f89565b6001600160a01b03811615611d6557611d445f865f80611908565b6001600160a01b0381165f908152600383016020526040902080545f190190555b6001600160a01b03861615611d95576001600160a01b0386165f9081526003830160205260409020805460010190555b5f85815260028301602052604080822080546001600160a01b0319166001600160a01b038a811691821790925591518893918516917fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef91a495945050505050565b611dfe611a70565b5f805160206126b883398151915280611e17848261254e565b506001810161083e838261254e565b611e2e611a70565b5f80516020612738833981519152805460ff19169055565b61154f611a70565b806001600160a01b03163b5f03611e8357604051634c9c8ce360e01b81526001600160a01b03821660048201526024016107db565b805f805160206126d88339815191526115cd565b60605f80846001600160a01b031684604051611eb3919061269c565b5f60405180830381855af49150503d805f8114611eeb576040519150601f19603f3d011682016040523d82523d5f602084013e611ef0565b606091505b5091509150611f00858383611fed565b95945050505050565b34156113b75760405163b398979f60e01b815260040160405180910390fd5b6001600160a01b038216611f5157604051633250574960e11b81525f60048201526024016107db565b5f611f5d83835f611078565b90506001600160a01b038116156109c3576040516339e3563760e11b81525f60048201526024016107db565b611f94838383612049565b6109c3576001600160a01b038316611fc257604051637e27328960e01b8152600481018290526024016107db565b60405163177e802f60e01b81526001600160a01b0383166004820152602481018290526044016107db565b60608261200257611ffd826120ad565b610e79565b815115801561201957506001600160a01b0384163b155b1561204257604051639996b31560e01b81526001600160a01b03851660048201526024016107db565b5080610e79565b5f6001600160a01b03831615801590611a685750826001600160a01b0316846001600160a01b0316148061208257506120828484610ece565b80611a685750826001600160a01b031661209b83610f89565b6001600160a01b031614949350505050565b8051156120bd5780518082602001fd5b604051630a12f52160e11b815260040160405180910390fd5b6001600160e01b0319811681146109e7575f80fd5b5f602082840312156120fb575f80fd5b8135610e79816120d6565b5f5b83811015612120578181015183820152602001612108565b50505f910152565b5f815180845261213f816020860160208601612106565b601f01601f19169290920160200192915050565b602081525f610e796020830184612128565b5f60208284031215612175575f80fd5b5035919050565b6001600160a01b03811681146109e7575f80fd5b5f80604083850312156121a1575f80fd5b82356121ac8161217c565b946020939093013593505050565b5f805f606084860312156121cc575f80fd5b83356121d78161217c565b925060208401356121e78161217c565b929592945050506040919091013590565b634e487b7160e01b5f52604160045260245ffd5b5f82601f83011261221b575f80fd5b813567ffffffffffffffff80821115612236576122366121f8565b604051601f8301601f19908116603f0116810190828211818310171561225e5761225e6121f8565b81604052838152866020858801011115612276575f80fd5b836020870160208301375f602085830101528094505050505092915050565b5f80604083850312156122a6575f80fd5b823567ffffffffffffffff808211156122bd575f80fd5b6122c98683870161220c565b935060208501359150808211156122de575f80fd5b506122eb8582860161220c565b9150509250929050565b5f8060408385031215612306575f80fd5b8235915060208301356123188161217c565b809150509250929050565b5f8060408385031215612334575f80fd5b823561233f8161217c565b9150602083013567ffffffffffffffff81111561235a575f80fd5b6122eb8582860161220c565b5f8060408385031215612377575f80fd5b8235915060208301356001600160801b0381168114612318575f80fd5b5f602082840312156123a4575f80fd5b8135610e798161217c565b5f602082840312156123bf575f80fd5b813567ffffffffffffffff8111156123d5575f80fd5b611a688482850161220c565b80151581146109e7575f80fd5b5f80604083850312156123ff575f80fd5b823561240a8161217c565b91506020830135612318816123e1565b5f805f806080858703121561242d575f80fd5b84356124388161217c565b935060208501356124488161217c565b925060408501359150606085013567ffffffffffffffff81111561246a575f80fd5b6124768782880161220c565b91505092959194509250565b5f8060408385031215612493575f80fd5b823561249e8161217c565b915060208301356123188161217c565b600181811c908216806124c257607f821691505b6020821081036124e057634e487b7160e01b5f52602260045260245ffd5b50919050565b5f602082840312156124f6575f80fd5b8151610e79816123e1565b601f8211156109c3575f81815260208120601f850160051c810160208610156125275750805b601f850160051c820191505b8181101561254657828155600101612533565b505050505050565b815167ffffffffffffffff811115612568576125686121f8565b61257c8161257684546124ae565b84612501565b602080601f8311600181146125af575f84156125985750858301515b5f19600386901b1c1916600185901b178555612546565b5f85815260208120601f198616915b828110156125dd578886015182559484019460019091019084016125be565b50858210156125fa57878501515f19600388901b60f8161c191681555b5050505050600190811b01905550565b5f835161261b818460208801612106565b83519083019061262f818360208801612106565b01949350505050565b5f60208284031215612648575f80fd5b5051919050565b6001600160a01b03858116825284166020820152604081018390526080606082018190525f9061116c90830184612128565b5f60208284031215612691575f80fd5b8151610e79816120d6565b5f82516126ad818460208701612106565b919091019291505056fe80bb2b638cc20bc4d0a60d66940f3ab4a00c1d7b313497ca82fb0b4ab0079300360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbc65d7a28e3265b37a6474929f336521b332c1681b933f6cb9f3376673440d862a02dd7bc7dec4dceedda775e58dd541e08a116c6c53815c0bd028192f7b626800cd5ed15c6e187e77e9aee88184c21f4f2182ab5827cb3b7e07fbedcd63f033009b779b17422d0df92223018b32b4d1fa46e071723d6817e2486d003becc55f007632e33b12507a4855f6678ca0e8955963b6dfcb053a9dd74381736814525400a264697066735822122076f1153e0f047169c3ecd69e64c220135f42cecb0152e15d2a3da341954d063b64736f6c63430008140033";
        public TossErc721MarketV1DeploymentBase() : base(BYTECODE) { }
        public TossErc721MarketV1DeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class DefaultAdminRoleFunction : DefaultAdminRoleFunctionBase { }

    [Function("DEFAULT_ADMIN_ROLE", "bytes32")]
    public class DefaultAdminRoleFunctionBase : FunctionMessage
    {

    }

    public partial class MinterRoleFunction : MinterRoleFunctionBase { }

    [Function("MINTER_ROLE", "bytes32")]
    public class MinterRoleFunctionBase : FunctionMessage
    {

    }

    public partial class PauserRoleFunction : PauserRoleFunctionBase { }

    [Function("PAUSER_ROLE", "bytes32")]
    public class PauserRoleFunctionBase : FunctionMessage
    {

    }

    public partial class UpgraderRoleFunction : UpgraderRoleFunctionBase { }

    [Function("UPGRADER_ROLE", "bytes32")]
    public class UpgraderRoleFunctionBase : FunctionMessage
    {

    }

    public partial class UpgradeInterfaceVersionFunction : UpgradeInterfaceVersionFunctionBase { }

    [Function("UPGRADE_INTERFACE_VERSION", "string")]
    public class UpgradeInterfaceVersionFunctionBase : FunctionMessage
    {

    }

    public partial class Tosserc721marketv1InitFunction : Tosserc721marketv1InitFunctionBase { }

    [Function("__TossErc721MarketV1_init")]
    public class Tosserc721marketv1InitFunctionBase : FunctionMessage
    {
        [Parameter("string", "name_", 1)]
        public virtual string Name { get; set; }
        [Parameter("string", "symbol_", 2)]
        public virtual string Symbol { get; set; }
    }

    public partial class ApproveFunction : ApproveFunctionBase { }

    [Function("approve")]
    public class ApproveFunctionBase : FunctionMessage
    {
        [Parameter("address", "to", 1)]
        public virtual string To { get; set; }
        [Parameter("uint256", "tokenId", 2)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class BalanceOfFunction : BalanceOfFunctionBase { }

    [Function("balanceOf", "uint256")]
    public class BalanceOfFunctionBase : FunctionMessage
    {
        [Parameter("address", "owner", 1)]
        public virtual string Owner { get; set; }
    }

    public partial class CreateSellOfferFunction : CreateSellOfferFunctionBase { }

    [Function("createSellOffer")]
    public class CreateSellOfferFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "tokenId", 1)]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("uint128", "price", 2)]
        public virtual BigInteger Price { get; set; }
    }

    public partial class GetApprovedFunction : GetApprovedFunctionBase { }

    [Function("getApproved", "address")]
    public class GetApprovedFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "tokenId", 1)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class GetBaseUriFunction : GetBaseUriFunctionBase { }

    [Function("getBaseUri", "string")]
    public class GetBaseUriFunctionBase : FunctionMessage
    {

    }

    public partial class GetImplementationFunction : GetImplementationFunctionBase { }

    [Function("getImplementation", "address")]
    public class GetImplementationFunctionBase : FunctionMessage
    {

    }

    public partial class GetMarketFunction : GetMarketFunctionBase { }

    [Function("getMarket", "address")]
    public class GetMarketFunctionBase : FunctionMessage
    {

    }

    public partial class GetRoleAdminFunction : GetRoleAdminFunctionBase { }

    [Function("getRoleAdmin", "bytes32")]
    public class GetRoleAdminFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "role", 1)]
        public virtual byte[] Role { get; set; }
    }

    public partial class GetWhitelistFunction : GetWhitelistFunctionBase { }

    [Function("getWhitelist", "address")]
    public class GetWhitelistFunctionBase : FunctionMessage
    {

    }

    public partial class GrantRoleFunction : GrantRoleFunctionBase { }

    [Function("grantRole")]
    public class GrantRoleFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "role", 1)]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "account", 2)]
        public virtual string Account { get; set; }
    }

    public partial class HasRoleFunction : HasRoleFunctionBase { }

    [Function("hasRole", "bool")]
    public class HasRoleFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "role", 1)]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "account", 2)]
        public virtual string Account { get; set; }
    }

    public partial class IsApprovedForAllFunction : IsApprovedForAllFunctionBase { }

    [Function("isApprovedForAll", "bool")]
    public class IsApprovedForAllFunctionBase : FunctionMessage
    {
        [Parameter("address", "owner", 1)]
        public virtual string Owner { get; set; }
        [Parameter("address", "operator", 2)]
        public virtual string Operator { get; set; }
    }

    public partial class NameFunction : NameFunctionBase { }

    [Function("name", "string")]
    public class NameFunctionBase : FunctionMessage
    {

    }

    public partial class OwnerOfFunction : OwnerOfFunctionBase { }

    [Function("ownerOf", "address")]
    public class OwnerOfFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "tokenId", 1)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class PauseFunction : PauseFunctionBase { }

    [Function("pause")]
    public class PauseFunctionBase : FunctionMessage
    {

    }

    public partial class PausedFunction : PausedFunctionBase { }

    [Function("paused", "bool")]
    public class PausedFunctionBase : FunctionMessage
    {

    }

    public partial class ProxiableUUIDFunction : ProxiableUUIDFunctionBase { }

    [Function("proxiableUUID", "bytes32")]
    public class ProxiableUUIDFunctionBase : FunctionMessage
    {

    }

    public partial class RenounceRoleFunction : RenounceRoleFunctionBase { }

    [Function("renounceRole")]
    public class RenounceRoleFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "role", 1)]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "callerConfirmation", 2)]
        public virtual string CallerConfirmation { get; set; }
    }

    public partial class RevokeRoleFunction : RevokeRoleFunctionBase { }

    [Function("revokeRole")]
    public class RevokeRoleFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "role", 1)]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "account", 2)]
        public virtual string Account { get; set; }
    }

    public partial class SafeMintFunction : SafeMintFunctionBase { }

    [Function("safeMint")]
    public class SafeMintFunctionBase : FunctionMessage
    {
        [Parameter("address", "to", 1)]
        public virtual string To { get; set; }
        [Parameter("uint256", "id", 2)]
        public virtual BigInteger Id { get; set; }
    }

    public partial class SafeTransferFromFunction : SafeTransferFromFunctionBase { }

    [Function("safeTransferFrom")]
    public class SafeTransferFromFunctionBase : FunctionMessage
    {
        [Parameter("address", "from", 1)]
        public virtual string From { get; set; }
        [Parameter("address", "to", 2)]
        public virtual string To { get; set; }
        [Parameter("uint256", "tokenId", 3)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class SafeTransferFrom1Function : SafeTransferFrom1FunctionBase { }

    [Function("safeTransferFrom")]
    public class SafeTransferFrom1FunctionBase : FunctionMessage
    {
        [Parameter("address", "from", 1)]
        public virtual string From { get; set; }
        [Parameter("address", "to", 2)]
        public virtual string To { get; set; }
        [Parameter("uint256", "tokenId", 3)]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("bytes", "data", 4)]
        public virtual byte[] Data { get; set; }
    }

    public partial class SetApprovalForAllFunction : SetApprovalForAllFunctionBase { }

    [Function("setApprovalForAll")]
    public class SetApprovalForAllFunctionBase : FunctionMessage
    {
        [Parameter("address", "operator", 1)]
        public virtual string Operator { get; set; }
        [Parameter("bool", "approved", 2)]
        public virtual bool Approved { get; set; }
    }

    public partial class SetBaseUriFunction : SetBaseUriFunctionBase { }

    [Function("setBaseUri")]
    public class SetBaseUriFunctionBase : FunctionMessage
    {
        [Parameter("string", "baseUri_", 1)]
        public virtual string Baseuri { get; set; }
    }

    public partial class SetMarketFunction : SetMarketFunctionBase { }

    [Function("setMarket")]
    public class SetMarketFunctionBase : FunctionMessage
    {
        [Parameter("address", "market", 1)]
        public virtual string Market { get; set; }
    }

    public partial class SetWhitelistFunction : SetWhitelistFunctionBase { }

    [Function("setWhitelist")]
    public class SetWhitelistFunctionBase : FunctionMessage
    {
        [Parameter("address", "newAddress", 1)]
        public virtual string NewAddress { get; set; }
    }

    public partial class SupportsInterfaceFunction : SupportsInterfaceFunctionBase { }

    [Function("supportsInterface", "bool")]
    public class SupportsInterfaceFunctionBase : FunctionMessage
    {
        [Parameter("bytes4", "interfaceId", 1)]
        public virtual byte[] InterfaceId { get; set; }
    }

    public partial class SymbolFunction : SymbolFunctionBase { }

    [Function("symbol", "string")]
    public class SymbolFunctionBase : FunctionMessage
    {

    }

    public partial class TokenURIFunction : TokenURIFunctionBase { }

    [Function("tokenURI", "string")]
    public class TokenURIFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "tokenId", 1)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class TransferFromFunction : TransferFromFunctionBase { }

    [Function("transferFrom")]
    public class TransferFromFunctionBase : FunctionMessage
    {
        [Parameter("address", "from", 1)]
        public virtual string From { get; set; }
        [Parameter("address", "to", 2)]
        public virtual string To { get; set; }
        [Parameter("uint256", "tokenId", 3)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class UnpauseFunction : UnpauseFunctionBase { }

    [Function("unpause")]
    public class UnpauseFunctionBase : FunctionMessage
    {

    }

    public partial class UpgradeToAndCallFunction : UpgradeToAndCallFunctionBase { }

    [Function("upgradeToAndCall")]
    public class UpgradeToAndCallFunctionBase : FunctionMessage
    {
        [Parameter("address", "newImplementation", 1)]
        public virtual string NewImplementation { get; set; }
        [Parameter("bytes", "data", 2)]
        public virtual byte[] Data { get; set; }
    }

    public partial class ApprovalEventDTO : ApprovalEventDTOBase { }

    [Event("Approval")]
    public class ApprovalEventDTOBase : IEventDTO
    {
        [Parameter("address", "owner", 1, true )]
        public virtual string Owner { get; set; }
        [Parameter("address", "approved", 2, true )]
        public virtual string Approved { get; set; }
        [Parameter("uint256", "tokenId", 3, true )]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class ApprovalForAllEventDTO : ApprovalForAllEventDTOBase { }

    [Event("ApprovalForAll")]
    public class ApprovalForAllEventDTOBase : IEventDTO
    {
        [Parameter("address", "owner", 1, true )]
        public virtual string Owner { get; set; }
        [Parameter("address", "operator", 2, true )]
        public virtual string Operator { get; set; }
        [Parameter("bool", "approved", 3, false )]
        public virtual bool Approved { get; set; }
    }

    public partial class CreatedEventDTO : CreatedEventDTOBase { }

    [Event("Created")]
    public class CreatedEventDTOBase : IEventDTO
    {
        [Parameter("address", "account", 1, true )]
        public virtual string Account { get; set; }
        [Parameter("uint256", "tokenId", 2, true )]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class InitializedEventDTO : InitializedEventDTOBase { }

    [Event("Initialized")]
    public class InitializedEventDTOBase : IEventDTO
    {
        [Parameter("uint64", "version", 1, false )]
        public virtual ulong Version { get; set; }
    }

    public partial class PausedEventDTO : PausedEventDTOBase { }

    [Event("Paused")]
    public class PausedEventDTOBase : IEventDTO
    {
        [Parameter("address", "account", 1, false )]
        public virtual string Account { get; set; }
    }

    public partial class RoleAdminChangedEventDTO : RoleAdminChangedEventDTOBase { }

    [Event("RoleAdminChanged")]
    public class RoleAdminChangedEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "role", 1, true )]
        public virtual byte[] Role { get; set; }
        [Parameter("bytes32", "previousAdminRole", 2, true )]
        public virtual byte[] PreviousAdminRole { get; set; }
        [Parameter("bytes32", "newAdminRole", 3, true )]
        public virtual byte[] NewAdminRole { get; set; }
    }

    public partial class RoleGrantedEventDTO : RoleGrantedEventDTOBase { }

    [Event("RoleGranted")]
    public class RoleGrantedEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "role", 1, true )]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "account", 2, true )]
        public virtual string Account { get; set; }
        [Parameter("address", "sender", 3, true )]
        public virtual string Sender { get; set; }
    }

    public partial class RoleRevokedEventDTO : RoleRevokedEventDTOBase { }

    [Event("RoleRevoked")]
    public class RoleRevokedEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "role", 1, true )]
        public virtual byte[] Role { get; set; }
        [Parameter("address", "account", 2, true )]
        public virtual string Account { get; set; }
        [Parameter("address", "sender", 3, true )]
        public virtual string Sender { get; set; }
    }

    public partial class TransferEventDTO : TransferEventDTOBase { }

    [Event("Transfer")]
    public class TransferEventDTOBase : IEventDTO
    {
        [Parameter("address", "from", 1, true )]
        public virtual string From { get; set; }
        [Parameter("address", "to", 2, true )]
        public virtual string To { get; set; }
        [Parameter("uint256", "tokenId", 3, true )]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class UnpausedEventDTO : UnpausedEventDTOBase { }

    [Event("Unpaused")]
    public class UnpausedEventDTOBase : IEventDTO
    {
        [Parameter("address", "account", 1, false )]
        public virtual string Account { get; set; }
    }

    public partial class UpgradedEventDTO : UpgradedEventDTOBase { }

    [Event("Upgraded")]
    public class UpgradedEventDTOBase : IEventDTO
    {
        [Parameter("address", "implementation", 1, true )]
        public virtual string Implementation { get; set; }
    }

    public partial class AccessControlBadConfirmationError : AccessControlBadConfirmationErrorBase { }
    [Error("AccessControlBadConfirmation")]
    public class AccessControlBadConfirmationErrorBase : IErrorDTO
    {
    }

    public partial class AccessControlUnauthorizedAccountError : AccessControlUnauthorizedAccountErrorBase { }

    [Error("AccessControlUnauthorizedAccount")]
    public class AccessControlUnauthorizedAccountErrorBase : IErrorDTO
    {
        [Parameter("address", "account", 1)]
        public virtual string Account { get; set; }
        [Parameter("bytes32", "neededRole", 2)]
        public virtual byte[] NeededRole { get; set; }
    }

    public partial class AddressEmptyCodeError : AddressEmptyCodeErrorBase { }

    [Error("AddressEmptyCode")]
    public class AddressEmptyCodeErrorBase : IErrorDTO
    {
        [Parameter("address", "target", 1)]
        public virtual string Target { get; set; }
    }

    public partial class ERC1967InvalidImplementationError : ERC1967InvalidImplementationErrorBase { }

    [Error("ERC1967InvalidImplementation")]
    public class ERC1967InvalidImplementationErrorBase : IErrorDTO
    {
        [Parameter("address", "implementation", 1)]
        public virtual string Implementation { get; set; }
    }

    public partial class ERC1967NonPayableError : ERC1967NonPayableErrorBase { }
    [Error("ERC1967NonPayable")]
    public class ERC1967NonPayableErrorBase : IErrorDTO
    {
    }

    public partial class ERC721IncorrectOwnerError : ERC721IncorrectOwnerErrorBase { }

    [Error("ERC721IncorrectOwner")]
    public class ERC721IncorrectOwnerErrorBase : IErrorDTO
    {
        [Parameter("address", "sender", 1)]
        public virtual string Sender { get; set; }
        [Parameter("uint256", "tokenId", 2)]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("address", "owner", 3)]
        public virtual string Owner { get; set; }
    }

    public partial class ERC721InsufficientApprovalError : ERC721InsufficientApprovalErrorBase { }

    [Error("ERC721InsufficientApproval")]
    public class ERC721InsufficientApprovalErrorBase : IErrorDTO
    {
        [Parameter("address", "operator", 1)]
        public virtual string Operator { get; set; }
        [Parameter("uint256", "tokenId", 2)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class ERC721InvalidApproverError : ERC721InvalidApproverErrorBase { }

    [Error("ERC721InvalidApprover")]
    public class ERC721InvalidApproverErrorBase : IErrorDTO
    {
        [Parameter("address", "approver", 1)]
        public virtual string Approver { get; set; }
    }

    public partial class ERC721InvalidOperatorError : ERC721InvalidOperatorErrorBase { }

    [Error("ERC721InvalidOperator")]
    public class ERC721InvalidOperatorErrorBase : IErrorDTO
    {
        [Parameter("address", "operator", 1)]
        public virtual string Operator { get; set; }
    }

    public partial class ERC721InvalidOwnerError : ERC721InvalidOwnerErrorBase { }

    [Error("ERC721InvalidOwner")]
    public class ERC721InvalidOwnerErrorBase : IErrorDTO
    {
        [Parameter("address", "owner", 1)]
        public virtual string Owner { get; set; }
    }

    public partial class ERC721InvalidReceiverError : ERC721InvalidReceiverErrorBase { }

    [Error("ERC721InvalidReceiver")]
    public class ERC721InvalidReceiverErrorBase : IErrorDTO
    {
        [Parameter("address", "receiver", 1)]
        public virtual string Receiver { get; set; }
    }

    public partial class ERC721InvalidSenderError : ERC721InvalidSenderErrorBase { }

    [Error("ERC721InvalidSender")]
    public class ERC721InvalidSenderErrorBase : IErrorDTO
    {
        [Parameter("address", "sender", 1)]
        public virtual string Sender { get; set; }
    }

    public partial class ERC721NonexistentTokenError : ERC721NonexistentTokenErrorBase { }

    [Error("ERC721NonexistentToken")]
    public class ERC721NonexistentTokenErrorBase : IErrorDTO
    {
        [Parameter("uint256", "tokenId", 1)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class EnforcedPauseError : EnforcedPauseErrorBase { }
    [Error("EnforcedPause")]
    public class EnforcedPauseErrorBase : IErrorDTO
    {
    }

    public partial class ExpectedPauseError : ExpectedPauseErrorBase { }
    [Error("ExpectedPause")]
    public class ExpectedPauseErrorBase : IErrorDTO
    {
    }

    public partial class FailedInnerCallError : FailedInnerCallErrorBase { }
    [Error("FailedInnerCall")]
    public class FailedInnerCallErrorBase : IErrorDTO
    {
    }

    public partial class InvalidInitializationError : InvalidInitializationErrorBase { }
    [Error("InvalidInitialization")]
    public class InvalidInitializationErrorBase : IErrorDTO
    {
    }

    public partial class NotInitializingError : NotInitializingErrorBase { }
    [Error("NotInitializing")]
    public class NotInitializingErrorBase : IErrorDTO
    {
    }

    public partial class ReentrancyGuardReentrantCallError : ReentrancyGuardReentrantCallErrorBase { }
    [Error("ReentrancyGuardReentrantCall")]
    public class ReentrancyGuardReentrantCallErrorBase : IErrorDTO
    {
    }

    public partial class TossErc721MarketNotSetError : TossErc721MarketNotSetErrorBase { }
    [Error("TossErc721MarketNotSet")]
    public class TossErc721MarketNotSetErrorBase : IErrorDTO
    {
    }

    public partial class TossUnsupportedInterfaceError : TossUnsupportedInterfaceErrorBase { }

    [Error("TossUnsupportedInterface")]
    public class TossUnsupportedInterfaceErrorBase : IErrorDTO
    {
        [Parameter("string", "name", 1)]
        public virtual string Name { get; set; }
    }

    public partial class TossWhitelistNotInWhitelistError : TossWhitelistNotInWhitelistErrorBase { }

    [Error("TossWhitelistNotInWhitelist")]
    public class TossWhitelistNotInWhitelistErrorBase : IErrorDTO
    {
        [Parameter("address", "address_", 1)]
        public virtual string Address { get; set; }
    }

    public partial class UUPSUnauthorizedCallContextError : UUPSUnauthorizedCallContextErrorBase { }
    [Error("UUPSUnauthorizedCallContext")]
    public class UUPSUnauthorizedCallContextErrorBase : IErrorDTO
    {
    }

    public partial class UUPSUnsupportedProxiableUUIDError : UUPSUnsupportedProxiableUUIDErrorBase { }

    [Error("UUPSUnsupportedProxiableUUID")]
    public class UUPSUnsupportedProxiableUUIDErrorBase : IErrorDTO
    {
        [Parameter("bytes32", "slot", 1)]
        public virtual byte[] Slot { get; set; }
    }

    public partial class DefaultAdminRoleOutputDTO : DefaultAdminRoleOutputDTOBase { }

    [FunctionOutput]
    public class DefaultAdminRoleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class MinterRoleOutputDTO : MinterRoleOutputDTOBase { }

    [FunctionOutput]
    public class MinterRoleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class PauserRoleOutputDTO : PauserRoleOutputDTOBase { }

    [FunctionOutput]
    public class PauserRoleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class UpgraderRoleOutputDTO : UpgraderRoleOutputDTOBase { }

    [FunctionOutput]
    public class UpgraderRoleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class UpgradeInterfaceVersionOutputDTO : UpgradeInterfaceVersionOutputDTOBase { }

    [FunctionOutput]
    public class UpgradeInterfaceVersionOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }





    public partial class BalanceOfOutputDTO : BalanceOfOutputDTOBase { }

    [FunctionOutput]
    public class BalanceOfOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class GetApprovedOutputDTO : GetApprovedOutputDTOBase { }

    [FunctionOutput]
    public class GetApprovedOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetBaseUriOutputDTO : GetBaseUriOutputDTOBase { }

    [FunctionOutput]
    public class GetBaseUriOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "baseUri", 1)]
        public virtual string BaseUri { get; set; }
    }

    public partial class GetImplementationOutputDTO : GetImplementationOutputDTOBase { }

    [FunctionOutput]
    public class GetImplementationOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "implementation", 1)]
        public virtual string Implementation { get; set; }
    }

    public partial class GetMarketOutputDTO : GetMarketOutputDTOBase { }

    [FunctionOutput]
    public class GetMarketOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "marketAddress", 1)]
        public virtual string MarketAddress { get; set; }
    }

    public partial class GetRoleAdminOutputDTO : GetRoleAdminOutputDTOBase { }

    [FunctionOutput]
    public class GetRoleAdminOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class GetWhitelistOutputDTO : GetWhitelistOutputDTOBase { }

    [FunctionOutput]
    public class GetWhitelistOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "whitelistAddress", 1)]
        public virtual string WhitelistAddress { get; set; }
    }



    public partial class HasRoleOutputDTO : HasRoleOutputDTOBase { }

    [FunctionOutput]
    public class HasRoleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class IsApprovedForAllOutputDTO : IsApprovedForAllOutputDTOBase { }

    [FunctionOutput]
    public class IsApprovedForAllOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class NameOutputDTO : NameOutputDTOBase { }

    [FunctionOutput]
    public class NameOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class OwnerOfOutputDTO : OwnerOfOutputDTOBase { }

    [FunctionOutput]
    public class OwnerOfOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }



    public partial class PausedOutputDTO : PausedOutputDTOBase { }

    [FunctionOutput]
    public class PausedOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class ProxiableUUIDOutputDTO : ProxiableUUIDOutputDTOBase { }

    [FunctionOutput]
    public class ProxiableUUIDOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }



















    public partial class SupportsInterfaceOutputDTO : SupportsInterfaceOutputDTOBase { }

    [FunctionOutput]
    public class SupportsInterfaceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class SymbolOutputDTO : SymbolOutputDTOBase { }

    [FunctionOutput]
    public class SymbolOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class TokenURIOutputDTO : TokenURIOutputDTOBase { }

    [FunctionOutput]
    public class TokenURIOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }






}
