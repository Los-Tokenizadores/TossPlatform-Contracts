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

namespace Toss.Contracts.TossExchangeV1.ContractDefinition
{


    public partial class TossExchangeV1Deployment : TossExchangeV1DeploymentBase
    {
        public TossExchangeV1Deployment() : base(BYTECODE) { }
        public TossExchangeV1Deployment(string byteCode) : base(byteCode) { }
    }

    public class TossExchangeV1DeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60a06040523060805234801562000014575f80fd5b506200001f6200002f565b620000296200002f565b620000e3565b7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a00805468010000000000000000900460ff1615620000805760405163f92ee8a960e01b815260040160405180910390fd5b80546001600160401b0390811614620000e05780546001600160401b0319166001600160401b0390811782556040519081527fc7f505b2f371ae2175ee4913f4499e1f2633a7b5936321eed1cdaeb6115181d29060200160405180910390a15b50565b60805161221d6200010a5f395f8181610fd701528181611000015261114d015261221d5ff3fe6080604052600436106101c5575f3560e01c80635c975abb116100f2578063aaf10f4211610092578063e63ab1e911610062578063e63ab1e914610529578063f72c0d8b14610549578063f88a8c1b1461057c578063fa630bc61461059b575f80fd5b8063aaf10f42146104a5578063ad3cb1cc146104b9578063d01f63f5146104f6578063d547741f1461050a575f80fd5b8063854cff2f116100cd578063854cff2f1461043557806391d14854146104545780639d63396414610473578063a217fddf14610492575f80fd5b80635c975abb146103ea57806360c6ec6f1461040d5780638456cb5914610421575f80fd5b806326812853116101685780633f4ba83a116101385780633f4ba83a146103905780634f1ef286146103a457806352d1902d146103b757806354469aea146103cb575f80fd5b806326812853146103035780632a673983146103225780632f2ff15d1461035257806336568abe14610371575f80fd5b80630915c22d116101a35780630915c22d1461025b5780630af5d6931461027a57806318da415114610299578063248a9ca3146102d6575f80fd5b806301ffc9a7146101c957806302387a7b146101fd578063081fab881461021e575b5f80fd5b3480156101d4575f80fd5b506101e86101e3366004611d8c565b6105c4565b60405190151581526020015b60405180910390f35b348015610208575f80fd5b5061021c610217366004611dce565b6105fa565b005b348015610229575f80fd5b505f805160206121c8833981519152546001600160801b03165b6040516001600160801b0390911681526020016101f4565b348015610266575f80fd5b5061021c610275366004611dce565b610606565b348015610285575f80fd5b5061021c610294366004611dfb565b61068b565b3480156102a4575f80fd5b505f80516020612128833981519152546001600160a01b03165b6040516001600160a01b0390911681526020016101f4565b3480156102e1575f80fd5b506102f56102f0366004611e50565b61079f565b6040519081526020016101f4565b34801561030e575f80fd5b5061021c61031d366004611dce565b6107bf565b34801561032d575f80fd5b505f805160206121c883398151915254600160801b90046001600160801b0316610243565b34801561035d575f80fd5b5061021c61036c366004611e67565b610848565b34801561037c575f80fd5b5061021c61038b366004611e67565b61086a565b34801561039b575f80fd5b5061021c6108a2565b61021c6103b2366004611ea9565b6108c1565b3480156103c2575f80fd5b506102f56108e0565b3480156103d6575f80fd5b5061021c6103e5366004611dce565b6108fb565b3480156103f5575f80fd5b505f805160206121a88339815191525460ff166101e8565b348015610418575f80fd5b506101e8610904565b34801561042c575f80fd5b5061021c61099b565b348015610440575f80fd5b5061021c61044f366004611f67565b6109ba565b34801561045f575f80fd5b506101e861046e366004611e67565b6109cd565b34801561047e575f80fd5b5061021c61048d366004611f90565b610a03565b34801561049d575f80fd5b506102f55f81565b3480156104b0575f80fd5b506102be610a97565b3480156104c4575f80fd5b506104e9604051806040016040528060058152602001640352e302e360dc1b81525081565b6040516101f49190612008565b348015610501575f80fd5b506102be610aa5565b348015610515575f80fd5b5061021c610524366004611e67565b610ac4565b348015610534575f80fd5b506102f55f8051602061216883398151915281565b348015610554575f80fd5b506102f57f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e381565b348015610587575f80fd5b5061021c610596366004611f90565b610ae0565b3480156105a6575f80fd5b505f80516020612108833981519152546001600160a01b03166102be565b5f6001600160e01b03198216637965db0b60e01b14806105f457506301ffc9a760e01b6001600160e01b03198316145b92915050565b61060381610b67565b50565b5f61061081610e15565b816001600160801b03165f0361065d57604051635304ab4b60e01b815260206004820152600c60248201526b3bb4ba34323930bb9036b4b760a11b60448201526064015b60405180910390fd5b815f805160206121c883398151915280546001600160801b03928316600160801b0292169190911790555050565b7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a008054600160401b810460ff16159067ffffffffffffffff165f811580156106d05750825b90505f8267ffffffffffffffff1660011480156106ec5750303b155b9050811580156106fa575080155b156107185760405163f92ee8a960e01b815260040160405180910390fd5b845467ffffffffffffffff19166001178555831561074257845460ff60401b1916600160401b1785555b61074e89898989610e1f565b831561079457845460ff60401b19168555604051600181527fc7f505b2f371ae2175ee4913f4499e1f2633a7b5936321eed1cdaeb6115181d29060200160405180910390a15b505050505050505050565b5f9081525f80516020612188833981519152602052604090206001015490565b5f6107c981610e15565b816001600160801b03165f0361081057604051635304ab4b60e01b815260206004820152600b60248201526a3232b837b9b4ba1036b4b760a91b6044820152606401610654565b505f805160206121c883398151915280546fffffffffffffffffffffffffffffffff19166001600160801b0392909216919091179055565b6108518261079f565b61085a81610e15565b6108648383610e53565b50505050565b6001600160a01b03811633146108935760405163334bd91960e11b815260040160405180910390fd5b61089d8282610ef4565b505050565b5f805160206121688339815191526108b981610e15565b610603610f6d565b6108c9610fcc565b6108d28261105c565b6108dc8282611086565b5050565b5f6108e9611142565b505f8051602061214883398151915290565b6106038161118b565b6040805160a0810182525f805160206121c883398151915280546001600160801b038082168452600160801b9091041660208301525f80516020612128833981519152546001600160a01b03908116938301939093525f80516020612108833981519152549283166060830152600160a01b90920467ffffffffffffffff1660808201525f919061099590836113fa565b91505090565b5f805160206121688339815191526109b281610e15565b610603611512565b5f6109c481610e15565b6108dc8261155a565b5f9182525f80516020612188833981519152602090815260408084206001600160a01b0393909316845291905290205460ff1690565b5f805160206121288339815191525460405163d505accf60e01b81525f805160206121c8833981519152916001600160a01b03169063d505accf90610a5890339030908b908b908b908b908b9060040161203a565b5f604051808303815f87803b158015610a6f575f80fd5b505af1158015610a81573d5f803e3d5ffd5b50505050610a8e8761118b565b50505050505050565b5f610aa061158b565b905090565b5f5f805160206120e88339815191525b546001600160a01b0316919050565b610acd8261079f565b610ad681610e15565b6108648383610ef4565b5f805160206121088339815191525460405163d505accf60e01b81525f805160206121c8833981519152916001600160a01b03169063d505accf90610b3590339030908b908b908b908b908b9060040161203a565b5f604051808303815f87803b158015610b4c575f80fd5b505af1158015610b5e573d5f803e3d5ffd5b50505050610a8e875b610b6f61159f565b610b776115e9565b335f805160206120e88339815191528115801590610b9e575080546001600160a01b031615155b8015610c12575080546040516304fec10960e11b81526001600160a01b038481166004830152909116906309fd821290602401602060405180830381865afa158015610bec573d5f803e3d5ffd5b505050506040513d601f19601f82011682018060405250810190610c10919061207b565b155b15610c3b57604051632dbcdb8b60e01b81526001600160a01b0383166004820152602401610654565b5f805160206121c88339815191528054600160801b90046001600160801b039081169085161015610cbc57805460405163b31eb5cd60e01b8152606060048201526008606482015267776974686472617760c01b60848201526001600160801b038087166024830152600160801b909204909116604482015260a401610654565b6040516001600160801b0385169033907f7084f5476618d8e60b11ef0d7d3f06914655adb8793e28ff7f018d4c76d505d5905f90a3600281015460405163079cc67960e41b81523360048201526001600160801b03861660248201526001600160a01b03909116906379cc6790906044015f604051808303815f87803b158015610d44575f80fd5b505af1158015610d56573d5f803e3d5ffd5b5050506001820154610d7c91506001600160a01b0316336001600160801b038716611619565b6040805160a08101825282546001600160801b038082168352600160801b9091041660208201526001808401546001600160a01b039081169383019390935260028401549283166060830152600160a01b90920467ffffffffffffffff166080820152610de8916113fa565b5050505061060360017f9b779b17422d0df92223018b32b4d1fa46e071723d6817e2486d003becc55f0055565b610603813361169e565b610e276116d7565b610e2f611720565b610e37611730565b610e3f611738565b610e47611748565b61086484848484611758565b5f5f80516020612188833981519152610e6c84846109cd565b610eeb575f848152602082815260408083206001600160a01b03871684529091529020805460ff19166001179055610ea13390565b6001600160a01b0316836001600160a01b0316857f2f8788117e7eff1d82e926ec794901d17c78024a50270940304540a733656f0d60405160405180910390a460019150506105f4565b5f9150506105f4565b5f5f80516020612188833981519152610f0d84846109cd565b15610eeb575f848152602082815260408083206001600160a01b0387168085529252808320805460ff1916905551339287917ff6391f5c32d9c69d2a47ea670b442974b53935d1edc7fd64eb21e047a839171b9190a460019150506105f4565b610f75611a43565b5f805160206121a8833981519152805460ff191681557f5db9ee0a495bf2e6ff9c91a7834c1ba4fdd244a5e8aa4e537bd38aeae4b073aa335b6040516001600160a01b03909116815260200160405180910390a150565b306001600160a01b037f000000000000000000000000000000000000000000000000000000000000000016148061103c57507f00000000000000000000000000000000000000000000000000000000000000006001600160a01b031661103061158b565b6001600160a01b031614155b1561105a5760405163703e46dd60e11b815260040160405180910390fd5b565b7f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e36108dc81610e15565b816001600160a01b03166352d1902d6040518163ffffffff1660e01b8152600401602060405180830381865afa9250505080156110e0575060408051601f3d908101601f191682019092526110dd9181019061209a565b60015b61110857604051634c9c8ce360e01b81526001600160a01b0383166004820152602401610654565b5f80516020612148833981519152811461113857604051632a87526960e21b815260048101829052602401610654565b61089d8383611a72565b306001600160a01b037f0000000000000000000000000000000000000000000000000000000000000000161461105a5760405163703e46dd60e11b815260040160405180910390fd5b61119361159f565b61119b6115e9565b335f805160206120e883398151915281158015906111c2575080546001600160a01b031615155b8015611236575080546040516304fec10960e11b81526001600160a01b038481166004830152909116906309fd821290602401602060405180830381865afa158015611210573d5f803e3d5ffd5b505050506040513d601f19601f82011682018060405250810190611234919061207b565b155b1561125f57604051632dbcdb8b60e01b81526001600160a01b0383166004820152602401610654565b5f805160206121c883398151915280546001600160801b0390811690851610156112d057805460405163b31eb5cd60e01b815260606004820152600760648201526619195c1bdcda5d60ca1b60848201526001600160801b038087166024830152909116604482015260a401610654565b6040516001600160801b0385169033907f2da466a7b24304f47e87fa2e1e5a81b9831ce54fec19055ce277ca2f39ba42c4905f90a36001810154611328906001600160a01b031633306001600160801b038816611ac7565b60028101546040516340c10f1960e01b81523360048201526001600160801b03861660248201526001600160a01b03909116906340c10f19906044015f604051808303815f87803b15801561137b575f80fd5b505af115801561138d573d5f803e3d5ffd5b50506040805160a08101825284546001600160801b038082168352600160801b9091041660208201526001808601546001600160a01b039081169383019390935260028601549283166060830152600160a01b90920467ffffffffffffffff166080820152610de8935091505b60408083015190516370a0823160e01b81523060048201525f9182916001600160a01b03909116906370a0823190602401602060405180830381865afa158015611446573d5f803e3d5ffd5b505050506040513d601f19601f8201168201806040525081019061146a919061209a565b90505f84606001516001600160a01b03166318160ddd6040518163ffffffff1660e01b8152600401602060405180830381865afa1580156114ad573d5f803e3d5ffd5b505050506040513d601f19601f820116820180604052508101906114d1919061209a565b9050808210801590816114e15750845b15611509576040516341f4c3e960e11b81526004810184905260248101839052604401610654565b95945050505050565b61151a6115e9565b5f805160206121a8833981519152805460ff191660011781557f62e78cea01bee320cd4e420270b5ea74000d11b0c9f74754ebdbfc544b05a25833610fae565b805f805160206120e88339815191525b80546001600160a01b0319166001600160a01b039290921691909117905550565b5f5f80516020612148833981519152610ab5565b7f9b779b17422d0df92223018b32b4d1fa46e071723d6817e2486d003becc55f008054600119016115e357604051633ee5aeb560e01b815260040160405180910390fd5b60029055565b5f805160206121a88339815191525460ff161561105a5760405163d93c066560e01b815260040160405180910390fd5b6040516001600160a01b0383811660248301526044820183905261089d91859182169063a9059cbb906064015b604051602081830303815290604052915060e01b6020820180516001600160e01b038381831617835250505050611b00565b60017f9b779b17422d0df92223018b32b4d1fa46e071723d6817e2486d003becc55f0055565b6116a882826109cd565b6108dc5760405163e2517d3f60e01b81526001600160a01b038216600482015260248101839052604401610654565b7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a0054600160401b900460ff1661105a57604051631afcd79f60e31b815260040160405180910390fd5b6117286116d7565b61105a611b61565b61105a6116d7565b6117406116d7565b61105a611b81565b6117506116d7565b61105a611730565b6117606116d7565b6001600160a01b0384166117a257604051631cfe685560e11b8152602060048201526008602482015267195e1d195c9b985b60c21b6044820152606401610654565b6001600160a01b0382166117e457604051631cfe685560e11b81526020600482015260086024820152671a5b9d195c9b985b60c21b6044820152606401610654565b816001600160a01b0316846001600160a01b0316036118155760405162097e5960e51b815260040160405180910390fd5b816001600160a01b031663313ce5676040518163ffffffff1660e01b8152600401602060405180830381865afa158015611851573d5f803e3d5ffd5b505050506040513d601f19601f8201168201806040525081019061187591906120b1565b60ff16846001600160a01b031663313ce5676040518163ffffffff1660e01b8152600401602060405180830381865afa1580156118b4573d5f803e3d5ffd5b505050506040513d601f19601f820116820180604052508101906118d891906120b1565b60ff16146118f957604051638dbfe54f60e01b815260040160405180910390fd5b826001600160801b03165f0361194057604051635304ab4b60e01b815260206004820152600b60248201526a3232b837b9b4ba1036b4b760a91b6044820152606401610654565b806001600160801b03165f0361198857604051635304ab4b60e01b815260206004820152600c60248201526b3bb4ba34323930bb9036b4b760a11b6044820152606401610654565b6119925f33610e53565b506119bd7f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e333610e53565b506119d55f8051602061216883398151915233610e53565b505f8051602061212883398151915280546001600160a01b039586166001600160a01b0319918216179091555f805160206121088339815191528054939095169216919091179092556001600160801b03918216600160801b029116175f805160206121c883398151915255565b5f805160206121a88339815191525460ff1661105a57604051638dfc202b60e01b815260040160405180910390fd5b611a7b82611b89565b6040516001600160a01b038316907fbc7cd75a20ee27fd9adebab32041f755214dbc6bffa90cc0225b39da2e5c2d3b905f90a2805115611abf5761089d8282611bd2565b6108dc611c3b565b6040516001600160a01b0384811660248301528381166044830152606482018390526108649186918216906323b872dd90608401611646565b5f611b146001600160a01b03841683611c5a565b905080515f14158015611b38575080806020019051810190611b36919061207b565b155b1561089d57604051635274afe760e01b81526001600160a01b0384166004820152602401610654565b611b696116d7565b5f805160206121a8833981519152805460ff19169055565b6116786116d7565b806001600160a01b03163b5f03611bbe57604051634c9c8ce360e01b81526001600160a01b0382166004820152602401610654565b805f8051602061214883398151915261156a565b60605f80846001600160a01b031684604051611bee91906120cc565b5f60405180830381855af49150503d805f8114611c26576040519150601f19603f3d011682016040523d82523d5f602084013e611c2b565b606091505b5091509150611509858383611c6e565b341561105a5760405163b398979f60e01b815260040160405180910390fd5b6060611c6783835f611cca565b9392505050565b606082611c8357611c7e82611d63565b611c67565b8151158015611c9a57506001600160a01b0384163b155b15611cc357604051639996b31560e01b81526001600160a01b0385166004820152602401610654565b5092915050565b606081471015611cef5760405163cd78605960e01b8152306004820152602401610654565b5f80856001600160a01b03168486604051611d0a91906120cc565b5f6040518083038185875af1925050503d805f8114611d44576040519150601f19603f3d011682016040523d82523d5f602084013e611d49565b606091505b5091509150611d59868383611c6e565b9695505050505050565b805115611d735780518082602001fd5b604051630a12f52160e11b815260040160405180910390fd5b5f60208284031215611d9c575f80fd5b81356001600160e01b031981168114611c67575f80fd5b80356001600160801b0381168114611dc9575f80fd5b919050565b5f60208284031215611dde575f80fd5b611c6782611db3565b6001600160a01b0381168114610603575f80fd5b5f805f8060808587031215611e0e575f80fd5b8435611e1981611de7565b9350611e2760208601611db3565b92506040850135611e3781611de7565b9150611e4560608601611db3565b905092959194509250565b5f60208284031215611e60575f80fd5b5035919050565b5f8060408385031215611e78575f80fd5b823591506020830135611e8a81611de7565b809150509250929050565b634e487b7160e01b5f52604160045260245ffd5b5f8060408385031215611eba575f80fd5b8235611ec581611de7565b9150602083013567ffffffffffffffff80821115611ee1575f80fd5b818501915085601f830112611ef4575f80fd5b813581811115611f0657611f06611e95565b604051601f8201601f19908116603f01168101908382118183101715611f2e57611f2e611e95565b81604052828152886020848701011115611f46575f80fd5b826020860160208301375f6020848301015280955050505050509250929050565b5f60208284031215611f77575f80fd5b8135611c6781611de7565b60ff81168114610603575f80fd5b5f805f805f8060c08789031215611fa5575f80fd5b611fae87611db3565b955060208701359450604087013593506060870135611fcc81611f82565b9598949750929560808101359460a0909101359350915050565b5f5b83811015612000578181015183820152602001611fe8565b50505f910152565b602081525f8251806020840152612026816040850160208701611fe6565b601f01601f19169190910160400192915050565b6001600160a01b0397881681529590961660208601526040850193909352606084019190915260ff16608083015260a082015260c081019190915260e00190565b5f6020828403121561208b575f80fd5b81518015158114611c67575f80fd5b5f602082840312156120aa575f80fd5b5051919050565b5f602082840312156120c1575f80fd5b8151611c6781611f82565b5f82516120dd818460208701611fe6565b919091019291505056fee32c7d464d01bf4699c6877240dec52b29adf1b2f9e414a502cf561c22d22d00776aa79b629bec5fcbeeac6d13e962169e431b12b2282b55578c981ec7fe3b02776aa79b629bec5fcbeeac6d13e962169e431b12b2282b55578c981ec7fe3b01360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbc65d7a28e3265b37a6474929f336521b332c1681b933f6cb9f3376673440d862a02dd7bc7dec4dceedda775e58dd541e08a116c6c53815c0bd028192f7b626800cd5ed15c6e187e77e9aee88184c21f4f2182ab5827cb3b7e07fbedcd63f03300776aa79b629bec5fcbeeac6d13e962169e431b12b2282b55578c981ec7fe3b00a26469706673582212207b06a89dd57e0c0146598e49ab1e6b829e12cacfebe65498584c2c50010b8a2064736f6c63430008140033";
        public TossExchangeV1DeploymentBase() : base(BYTECODE) { }
        public TossExchangeV1DeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class DefaultAdminRoleFunction : DefaultAdminRoleFunctionBase { }

    [Function("DEFAULT_ADMIN_ROLE", "bytes32")]
    public class DefaultAdminRoleFunctionBase : FunctionMessage
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

    public partial class Tossexchangev1InitFunction : Tossexchangev1InitFunctionBase { }

    [Function("__TossExchangeV1_init")]
    public class Tossexchangev1InitFunctionBase : FunctionMessage
    {
        [Parameter("address", "externalErc20_", 1)]
        public virtual string Externalerc20 { get; set; }
        [Parameter("uint128", "depositMinAmount_", 2)]
        public virtual BigInteger Depositminamount { get; set; }
        [Parameter("address", "internalErc20_", 3)]
        public virtual string Internalerc20 { get; set; }
        [Parameter("uint128", "withdrawMinAmount_", 4)]
        public virtual BigInteger Withdrawminamount { get; set; }
    }

    public partial class DepositFunction : DepositFunctionBase { }

    [Function("deposit")]
    public class DepositFunctionBase : FunctionMessage
    {
        [Parameter("uint128", "amount", 1)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class DepositWithPermitFunction : DepositWithPermitFunctionBase { }

    [Function("depositWithPermit")]
    public class DepositWithPermitFunctionBase : FunctionMessage
    {
        [Parameter("uint128", "externalAmount", 1)]
        public virtual BigInteger ExternalAmount { get; set; }
        [Parameter("uint256", "amount", 2)]
        public virtual BigInteger Amount { get; set; }
        [Parameter("uint256", "deadline", 3)]
        public virtual BigInteger Deadline { get; set; }
        [Parameter("uint8", "v", 4)]
        public virtual byte V { get; set; }
        [Parameter("bytes32", "r", 5)]
        public virtual byte[] R { get; set; }
        [Parameter("bytes32", "s", 6)]
        public virtual byte[] S { get; set; }
    }

    public partial class GetDepositMinAmountFunction : GetDepositMinAmountFunctionBase { }

    [Function("getDepositMinAmount", "uint128")]
    public class GetDepositMinAmountFunctionBase : FunctionMessage
    {

    }

    public partial class GetExternalErc20Function : GetExternalErc20FunctionBase { }

    [Function("getExternalErc20", "address")]
    public class GetExternalErc20FunctionBase : FunctionMessage
    {

    }

    public partial class GetImplementationFunction : GetImplementationFunctionBase { }

    [Function("getImplementation", "address")]
    public class GetImplementationFunctionBase : FunctionMessage
    {

    }

    public partial class GetInternalErc20Function : GetInternalErc20FunctionBase { }

    [Function("getInternalErc20", "address")]
    public class GetInternalErc20FunctionBase : FunctionMessage
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

    public partial class GetWithdrawMinAmountFunction : GetWithdrawMinAmountFunctionBase { }

    [Function("getWithdrawMinAmount", "uint128")]
    public class GetWithdrawMinAmountFunctionBase : FunctionMessage
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

    public partial class SetDepositMinAmountFunction : SetDepositMinAmountFunctionBase { }

    [Function("setDepositMinAmount")]
    public class SetDepositMinAmountFunctionBase : FunctionMessage
    {
        [Parameter("uint128", "value", 1)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class SetWhitelistFunction : SetWhitelistFunctionBase { }

    [Function("setWhitelist")]
    public class SetWhitelistFunctionBase : FunctionMessage
    {
        [Parameter("address", "newAddress", 1)]
        public virtual string NewAddress { get; set; }
    }

    public partial class SetWithdrawMinAmountFunction : SetWithdrawMinAmountFunctionBase { }

    [Function("setWithdrawMinAmount")]
    public class SetWithdrawMinAmountFunctionBase : FunctionMessage
    {
        [Parameter("uint128", "value", 1)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class SupportsInterfaceFunction : SupportsInterfaceFunctionBase { }

    [Function("supportsInterface", "bool")]
    public class SupportsInterfaceFunctionBase : FunctionMessage
    {
        [Parameter("bytes4", "interfaceId", 1)]
        public virtual byte[] InterfaceId { get; set; }
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

    public partial class ValidStateFunction : ValidStateFunctionBase { }

    [Function("validState", "bool")]
    public class ValidStateFunctionBase : FunctionMessage
    {

    }

    public partial class WithdrawFunction : WithdrawFunctionBase { }

    [Function("withdraw")]
    public class WithdrawFunctionBase : FunctionMessage
    {
        [Parameter("uint128", "amount", 1)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class WithdrawWithPermitFunction : WithdrawWithPermitFunctionBase { }

    [Function("withdrawWithPermit")]
    public class WithdrawWithPermitFunctionBase : FunctionMessage
    {
        [Parameter("uint128", "internalAmount", 1)]
        public virtual BigInteger InternalAmount { get; set; }
        [Parameter("uint256", "amount", 2)]
        public virtual BigInteger Amount { get; set; }
        [Parameter("uint256", "deadline", 3)]
        public virtual BigInteger Deadline { get; set; }
        [Parameter("uint8", "v", 4)]
        public virtual byte V { get; set; }
        [Parameter("bytes32", "r", 5)]
        public virtual byte[] R { get; set; }
        [Parameter("bytes32", "s", 6)]
        public virtual byte[] S { get; set; }
    }

    public partial class DepositedEventDTO : DepositedEventDTOBase { }

    [Event("Deposited")]
    public class DepositedEventDTOBase : IEventDTO
    {
        [Parameter("address", "account", 1, true )]
        public virtual string Account { get; set; }
        [Parameter("uint256", "amount", 2, true )]
        public virtual BigInteger Amount { get; set; }
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

    public partial class WithdrawnEventDTO : WithdrawnEventDTOBase { }

    [Event("Withdrawn")]
    public class WithdrawnEventDTOBase : IEventDTO
    {
        [Parameter("address", "account", 1, true )]
        public virtual string Account { get; set; }
        [Parameter("uint256", "amount", 2, true )]
        public virtual BigInteger Amount { get; set; }
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

    public partial class AddressInsufficientBalanceError : AddressInsufficientBalanceErrorBase { }

    [Error("AddressInsufficientBalance")]
    public class AddressInsufficientBalanceErrorBase : IErrorDTO
    {
        [Parameter("address", "account", 1)]
        public virtual string Account { get; set; }
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

    public partial class SafeERC20FailedOperationError : SafeERC20FailedOperationErrorBase { }

    [Error("SafeERC20FailedOperation")]
    public class SafeERC20FailedOperationErrorBase : IErrorDTO
    {
        [Parameter("address", "token", 1)]
        public virtual string Token { get; set; }
    }

    public partial class TossAddressIsZeroError : TossAddressIsZeroErrorBase { }

    [Error("TossAddressIsZero")]
    public class TossAddressIsZeroErrorBase : IErrorDTO
    {
        [Parameter("string", "parameter", 1)]
        public virtual string Parameter { get; set; }
    }

    public partial class TossExchangeAmounIsLessThanMinError : TossExchangeAmounIsLessThanMinErrorBase { }

    [Error("TossExchangeAmounIsLessThanMin")]
    public class TossExchangeAmounIsLessThanMinErrorBase : IErrorDTO
    {
        [Parameter("string", "parameter", 1)]
        public virtual string Parameter { get; set; }
        [Parameter("uint256", "amount", 2)]
        public virtual BigInteger Amount { get; set; }
        [Parameter("uint256", "min", 3)]
        public virtual BigInteger Min { get; set; }
    }

    public partial class TossExchangeExternalAndInternalErc20AreEqualError : TossExchangeExternalAndInternalErc20AreEqualErrorBase { }
    [Error("TossExchangeExternalAndInternalErc20AreEqual")]
    public class TossExchangeExternalAndInternalErc20AreEqualErrorBase : IErrorDTO
    {
    }

    public partial class TossExchangeExternalAndInternalErc20HaveDifferentDecimalAmountError : TossExchangeExternalAndInternalErc20HaveDifferentDecimalAmountErrorBase { }
    [Error("TossExchangeExternalAndInternalErc20HaveDifferentDecimalAmount")]
    public class TossExchangeExternalAndInternalErc20HaveDifferentDecimalAmountErrorBase : IErrorDTO
    {
    }

    public partial class TossExchangeInvalidStateError : TossExchangeInvalidStateErrorBase { }

    [Error("TossExchangeInvalidState")]
    public class TossExchangeInvalidStateErrorBase : IErrorDTO
    {
        [Parameter("uint256", "externalAmount", 1)]
        public virtual BigInteger ExternalAmount { get; set; }
        [Parameter("uint256", "internalAmount", 2)]
        public virtual BigInteger InternalAmount { get; set; }
    }

    public partial class TossValueIsZeroError : TossValueIsZeroErrorBase { }

    [Error("TossValueIsZero")]
    public class TossValueIsZeroErrorBase : IErrorDTO
    {
        [Parameter("string", "parameter", 1)]
        public virtual string Parameter { get; set; }
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







    public partial class GetDepositMinAmountOutputDTO : GetDepositMinAmountOutputDTOBase { }

    [FunctionOutput]
    public class GetDepositMinAmountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint128", "minAmount", 1)]
        public virtual BigInteger MinAmount { get; set; }
    }

    public partial class GetExternalErc20OutputDTO : GetExternalErc20OutputDTOBase { }

    [FunctionOutput]
    public class GetExternalErc20OutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "erc20", 1)]
        public virtual string Erc20 { get; set; }
    }

    public partial class GetImplementationOutputDTO : GetImplementationOutputDTOBase { }

    [FunctionOutput]
    public class GetImplementationOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "implementation", 1)]
        public virtual string Implementation { get; set; }
    }

    public partial class GetInternalErc20OutputDTO : GetInternalErc20OutputDTOBase { }

    [FunctionOutput]
    public class GetInternalErc20OutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "erc20", 1)]
        public virtual string Erc20 { get; set; }
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

    public partial class GetWithdrawMinAmountOutputDTO : GetWithdrawMinAmountOutputDTOBase { }

    [FunctionOutput]
    public class GetWithdrawMinAmountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint128", "minAmount", 1)]
        public virtual BigInteger MinAmount { get; set; }
    }



    public partial class HasRoleOutputDTO : HasRoleOutputDTOBase { }

    [FunctionOutput]
    public class HasRoleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
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





    public partial class ValidStateOutputDTO : ValidStateOutputDTOBase { }

    [FunctionOutput]
    public class ValidStateOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "valid", 1)]
        public virtual bool Valid { get; set; }
    }




}
