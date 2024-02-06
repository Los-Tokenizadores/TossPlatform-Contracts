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

namespace Toss.Contracts.TossErc20V1.ContractDefinition
{


    public partial class TossErc20V1Deployment : TossErc20V1DeploymentBase
    {
        public TossErc20V1Deployment() : base(BYTECODE) { }
        public TossErc20V1Deployment(string byteCode) : base(byteCode) { }
    }

    public class TossErc20V1DeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60a060405230608052348015610013575f80fd5b5061001c610021565b6100d3565b7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a00805468010000000000000000900460ff16156100715760405163f92ee8a960e01b815260040160405180910390fd5b80546001600160401b03908116146100d05780546001600160401b0319166001600160401b0390811782556040519081527fc7f505b2f371ae2175ee4913f4499e1f2633a7b5936321eed1cdaeb6115181d29060200160405180910390a15b50565b60805161246c620000fa5f395f81816110490152818161107201526111bf015261246c5ff3fe608060405260043610610207575f3560e01c80637ecebe0011610113578063ad3cb1cc1161009d578063d547741f1161006d578063d547741f146105e3578063dd62ed3e14610602578063e63ab1e914610621578063ef1673c914610641578063f72c0d8b14610660575f80fd5b8063ad3cb1cc1461054d578063d01f63f51461057d578063d505accf14610591578063d5391393146105b0575f80fd5b806391d14854116100e357806391d14854146104bc57806395d89b41146104db578063a217fddf146104ef578063a9059cbb14610502578063aaf10f4214610521575f80fd5b80637ecebe00146104435780638456cb591461046257806384b0196e14610476578063854cff2f1461049d575f80fd5b806336568abe116101945780634f1ef286116101645780634f1ef286146103bb57806352d1902d146103ce5780635c975abb146103e257806370a082311461040557806379cc679014610424575f80fd5b806336568abe1461034a5780633f4ba83a1461036957806340c10f191461037d57806342966c681461039c575f80fd5b806323b872dd116101da57806323b872dd146102bc578063248a9ca3146102db5780632f2ff15d146102fa578063313ce5671461031b5780633644e51514610336575f80fd5b806301ffc9a71461020b57806306fdde031461023f578063095ea7b31461026057806318160ddd1461027f575b5f80fd5b348015610216575f80fd5b5061022a610225366004611dc1565b610693565b60405190151581526020015b60405180910390f35b34801561024a575f80fd5b506102536106c9565b6040516102369190611e35565b34801561026b575f80fd5b5061022a61027a366004611e62565b61076e565b34801561028a575f80fd5b507f52c63247e1f47db19d5ce0460030c497f067ca4cebf71ba98eeadabe20bace02545b604051908152602001610236565b3480156102c7575f80fd5b5061022a6102d6366004611e8a565b610785565b3480156102e6575f80fd5b506102ae6102f5366004611ec3565b6107aa565b348015610305575f80fd5b50610319610314366004611eda565b6107ca565b005b348015610326575f80fd5b5060405160128152602001610236565b348015610341575f80fd5b506102ae6107ec565b348015610355575f80fd5b50610319610364366004611eda565b6107fa565b348015610374575f80fd5b50610319610832565b348015610388575f80fd5b50610319610397366004611e62565b610854565b3480156103a7575f80fd5b506103196103b6366004611ec3565b610888565b6103196103c9366004611f8b565b610892565b3480156103d9575f80fd5b506102ae6108b1565b3480156103ed575f80fd5b505f805160206124178339815191525460ff1661022a565b348015610410575f80fd5b506102ae61041f366004611fe9565b6108cc565b34801561042f575f80fd5b5061031961043e366004611e62565b6108fc565b34801561044e575f80fd5b506102ae61045d366004611fe9565b610911565b34801561046d575f80fd5b5061031961091b565b348015610481575f80fd5b5061048a61093a565b6040516102369796959493929190612002565b3480156104a8575f80fd5b506103196104b7366004611fe9565b6109e8565b3480156104c7575f80fd5b5061022a6104d6366004611eda565b6109fb565b3480156104e6575f80fd5b50610253610a31565b3480156104fa575f80fd5b506102ae5f81565b34801561050d575f80fd5b5061022a61051c366004611e62565b610a6f565b34801561052c575f80fd5b50610535610a7c565b6040516001600160a01b039091168152602001610236565b348015610558575f80fd5b50610253604051806040016040528060058152602001640352e302e360dc1b81525081565b348015610588575f80fd5b50610535610a85565b34801561059c575f80fd5b506103196105ab366004612096565b610ab7565b3480156105bb575f80fd5b506102ae7f9f2df0fed2c77648de5860a4cc508cd0818c85b8b8a1ab4ceeef8d981c8956a681565b3480156105ee575f80fd5b506103196105fd366004611eda565b610c0c565b34801561060d575f80fd5b506102ae61061c366004612103565b610c28565b34801561062c575f80fd5b506102ae5f805160206123d783398151915281565b34801561064c575f80fd5b5061031961065b366004612149565b610c71565b34801561066b575f80fd5b506102ae7f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e381565b5f6001600160e01b03198216637965db0b60e01b14806106c357506301ffc9a760e01b6001600160e01b03198316145b92915050565b60605f5f805160206123778339815191525b90508060030180546106ec906121b1565b80601f0160208091040260200160405190810160405280929190818152602001828054610718906121b1565b80156107635780601f1061073a57610100808354040283529160200191610763565b820191905f5260205f20905b81548152906001019060200180831161074657829003601f168201915b505050505091505090565b5f3361077b818585610d83565b5060019392505050565b5f33610792858285610d90565b61079d858585610ded565b60019150505b9392505050565b5f9081525f805160206123f7833981519152602052604090206001015490565b6107d3826107aa565b6107dc81610e4a565b6107e68383610e54565b50505050565b5f6107f5610ef5565b905090565b6001600160a01b03811633146108235760405163334bd91960e11b815260040160405180910390fd5b61082d8282610efe565b505050565b5f805160206123d783398151915261084981610e4a565b610851610f77565b50565b7f9f2df0fed2c77648de5860a4cc508cd0818c85b8b8a1ab4ceeef8d981c8956a661087e81610e4a565b61082d8383610fd6565b610851338261100a565b61089a61103e565b6108a3826110ce565b6108ad82826110f8565b5050565b5f6108ba6111b4565b505f805160206123b783398151915290565b5f805f805160206123778339815191525b6001600160a01b039093165f9081526020939093525050604090205490565b610907823383610d90565b6108ad828261100a565b5f6106c3826111fd565b5f805160206123d783398151915261093281610e4a565b610851611225565b5f60608082808083815f80516020612397833981519152805490915015801561096557506001810154155b6109ae5760405162461bcd60e51b81526020600482015260156024820152741152540dcc4c8e88155b9a5b9a5d1a585b1a5e9959605a1b60448201526064015b60405180910390fd5b6109b661126d565b6109be6112ab565b604080515f80825260208201909252600f60f81b9c939b5091995046985030975095509350915050565b5f6109f281610e4a565b6108ad826112c1565b5f9182525f805160206123f7833981519152602090815260408084206001600160a01b0393909316845291905290205460ff1690565b7f52c63247e1f47db19d5ce0460030c497f067ca4cebf71ba98eeadabe20bace0480546060915f80516020612377833981519152916106ec906121b1565b5f3361077b818585610ded565b5f6107f5611305565b5f7fe32c7d464d01bf4699c6877240dec52b29adf1b2f9e414a502cf561c22d22d005b546001600160a01b0316919050565b83421115610adb5760405163313c898160e11b8152600481018590526024016109a5565b5f7f6e71edae12b1b97f4d1f60370fef10105fa2faae0126114a169c64845d6126c9888888610b458c6001600160a01b03165f9081527f5ab42ced628888259c08ac98db1eb0cf702fc1501344311d8b100cd1bfe4bb006020526040902080546001810190915590565b6040805160208101969096526001600160a01b0394851690860152929091166060840152608083015260a082015260c0810186905260e0016040516020818303038152906040528051906020012090505f610b9f82611319565b90505f610bae82878787611345565b9050896001600160a01b0316816001600160a01b031614610bf5576040516325c0072360e11b81526001600160a01b0380831660048301528b1660248201526044016109a5565b610c008a8a8a610d83565b50505050505050505050565b610c15826107aa565b610c1e81610e4a565b6107e68383610efe565b6001600160a01b039182165f9081527f52c63247e1f47db19d5ce0460030c497f067ca4cebf71ba98eeadabe20bace016020908152604080832093909416825291909152205490565b7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a008054600160401b810460ff16159067ffffffffffffffff165f81158015610cb65750825b90505f8267ffffffffffffffff166001148015610cd25750303b155b905081158015610ce0575080155b15610cfe5760405163f92ee8a960e01b815260040160405180910390fd5b845467ffffffffffffffff191660011785558315610d2857845460ff60401b1916600160401b1785555b610d33888888611371565b8315610d7957845460ff60401b19168555604051600181527fc7f505b2f371ae2175ee4913f4499e1f2633a7b5936321eed1cdaeb6115181d29060200160405180910390a15b5050505050505050565b61082d83838360016113b5565b5f610d9b8484610c28565b90505f1981146107e65781811015610ddf57604051637dc7a0d960e11b81526001600160a01b038416600482015260248101829052604481018390526064016109a5565b6107e684848484035f6113b5565b6001600160a01b038316610e1657604051634b637e8f60e11b81525f60048201526024016109a5565b6001600160a01b038216610e3f5760405163ec442f0560e01b81525f60048201526024016109a5565b61082d838383611499565b6108518133611584565b5f5f805160206123f7833981519152610e6d84846109fb565b610eec575f848152602082815260408083206001600160a01b03871684529091529020805460ff19166001179055610ea23390565b6001600160a01b0316836001600160a01b0316857f2f8788117e7eff1d82e926ec794901d17c78024a50270940304540a733656f0d60405160405180910390a460019150506106c3565b5f9150506106c3565b5f6107f56115bd565b5f5f805160206123f7833981519152610f1784846109fb565b15610eec575f848152602082815260408083206001600160a01b0387168085529252808320805460ff1916905551339287917ff6391f5c32d9c69d2a47ea670b442974b53935d1edc7fd64eb21e047a839171b9190a460019150506106c3565b610f7f611630565b5f80516020612417833981519152805460ff191681557f5db9ee0a495bf2e6ff9c91a7834c1ba4fdd244a5e8aa4e537bd38aeae4b073aa335b6040516001600160a01b03909116815260200160405180910390a150565b6001600160a01b038216610fff5760405163ec442f0560e01b81525f60048201526024016109a5565b6108ad5f8383611499565b6001600160a01b03821661103357604051634b637e8f60e11b81525f60048201526024016109a5565b6108ad825f83611499565b306001600160a01b037f00000000000000000000000000000000000000000000000000000000000000001614806110ae57507f00000000000000000000000000000000000000000000000000000000000000006001600160a01b03166110a2611305565b6001600160a01b031614155b156110cc5760405163703e46dd60e11b815260040160405180910390fd5b565b7f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e36108ad81610e4a565b816001600160a01b03166352d1902d6040518163ffffffff1660e01b8152600401602060405180830381865afa925050508015611152575060408051601f3d908101601f1916820190925261114f918101906121e9565b60015b61117a57604051634c9c8ce360e01b81526001600160a01b03831660048201526024016109a5565b5f805160206123b783398151915281146111aa57604051632a87526960e21b8152600481018290526024016109a5565b61082d838361165f565b306001600160a01b037f000000000000000000000000000000000000000000000000000000000000000016146110cc5760405163703e46dd60e11b815260040160405180910390fd5b5f807f5ab42ced628888259c08ac98db1eb0cf702fc1501344311d8b100cd1bfe4bb006108dd565b61122d6116b4565b5f80516020612417833981519152805460ff191660011781557f62e78cea01bee320cd4e420270b5ea74000d11b0c9f74754ebdbfc544b05a25833610fb8565b7fa16a46d94261c7517cc8ff89f61c0ce93598e3c849801011dee649a6a557d10280546060915f80516020612397833981519152916106ec906121b1565b60605f5f805160206123978339815191526106db565b807fe32c7d464d01bf4699c6877240dec52b29adf1b2f9e414a502cf561c22d22d005b80546001600160a01b0319166001600160a01b039290921691909117905550565b5f5f805160206123b7833981519152610aa8565b5f6106c3611325610ef5565b8360405161190160f01b8152600281019290925260228201526042902090565b5f805f80611355888888886116e4565b92509250925061136582826117ac565b50909695505050505050565b611379611864565b61138383836118ad565b61138b6118bf565b6113936118c7565b61139b6118bf565b6113a4836118d7565b6113ac611902565b61082d81611912565b5f805160206123778339815191526001600160a01b0385166113ec5760405163e602df0560e01b81525f60048201526024016109a5565b6001600160a01b03841661141557604051634a1406b160e11b81525f60048201526024016109a5565b6001600160a01b038086165f9081526001830160209081526040808320938816835292905220839055811561149257836001600160a01b0316856001600160a01b03167f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b9258560405161148991815260200190565b60405180910390a35b5050505050565b817fe32c7d464d01bf4699c6877240dec52b29adf1b2f9e414a502cf561c22d22d006001600160a01b038216158015906114dc575080546001600160a01b031615155b8015611550575080546040516304fec10960e11b81526001600160a01b038481166004830152909116906309fd821290602401602060405180830381865afa15801561152a573d5f803e3d5ffd5b505050506040513d601f19601f8201168201806040525081019061154e9190612200565b155b1561157957604051632dbcdb8b60e01b81526001600160a01b03831660048201526024016109a5565b61149285858561199d565b61158e82826109fb565b6108ad5760405163e2517d3f60e01b81526001600160a01b0382166004820152602481018390526044016109a5565b5f7f8b73c3c69bb8fe3d512ecc4cf759cc79239f7b179b0ffacaa9a75d522b39400f6115e76119b0565b6115ef611a18565b60408051602081019490945283019190915260608201524660808201523060a082015260c00160405160208183030381529060405280519060200120905090565b5f805160206124178339815191525460ff166110cc57604051638dfc202b60e01b815260040160405180910390fd5b61166882611a5a565b6040516001600160a01b038316907fbc7cd75a20ee27fd9adebab32041f755214dbc6bffa90cc0225b39da2e5c2d3b905f90a28051156116ac5761082d8282611aa3565b6108ad611b15565b5f805160206124178339815191525460ff16156110cc5760405163d93c066560e01b815260040160405180910390fd5b5f80807f7fffffffffffffffffffffffffffffff5d576e7357a4501ddfe92f46681b20a084111561171d57505f915060039050826117a2565b604080515f808252602082018084528a905260ff891692820192909252606081018790526080810186905260019060a0016020604051602081039080840390855afa15801561176e573d5f803e3d5ffd5b5050604051601f1901519150506001600160a01b03811661179957505f9250600191508290506117a2565b92505f91508190505b9450945094915050565b5f8260038111156117bf576117bf61221f565b036117c8575050565b60018260038111156117dc576117dc61221f565b036117fa5760405163f645eedf60e01b815260040160405180910390fd5b600282600381111561180e5761180e61221f565b0361182f5760405163fce698f760e01b8152600481018290526024016109a5565b60038260038111156118435761184361221f565b036108ad576040516335e2f38360e21b8152600481018290526024016109a5565b7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a0054600160401b900460ff166110cc57604051631afcd79f60e31b815260040160405180910390fd5b6118b5611864565b6108ad8282611b34565b6110cc611864565b6118cf611864565b6110cc611b84565b6118df611864565b61085181604051806040016040528060018152602001603160f81b815250611ba4565b61190a611864565b6110cc6118bf565b61191a611864565b6119245f33610e54565b5061194f7f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e333610e54565b506119675f805160206123d783398151915233610e54565b506119927f9f2df0fed2c77648de5860a4cc508cd0818c85b8b8a1ab4ceeef8d981c8956a633610e54565b506108513382610fd6565b6119a56116b4565b61082d838383611c03565b5f5f80516020612397833981519152816119c861126d565b8051909150156119e057805160209091012092915050565b815480156119ef579392505050565b7fc5d2460186f7233c927e7db2dcc703c0e500b653ca82273b7bfad8045d85a470935050505090565b5f5f8051602061239783398151915281611a306112ab565b805190915015611a4857805160209091012092915050565b600182015480156119ef579392505050565b806001600160a01b03163b5f03611a8f57604051634c9c8ce360e01b81526001600160a01b03821660048201526024016109a5565b805f805160206123b78339815191526112e4565b60605f80846001600160a01b031684604051611abf9190612233565b5f60405180830381855af49150503d805f8114611af7576040519150601f19603f3d011682016040523d82523d5f602084013e611afc565b606091505b5091509150611b0c858383611d3c565b95945050505050565b34156110cc5760405163b398979f60e01b815260040160405180910390fd5b611b3c611864565b5f805160206123778339815191527f52c63247e1f47db19d5ce0460030c497f067ca4cebf71ba98eeadabe20bace03611b75848261229b565b50600481016107e6838261229b565b611b8c611864565b5f80516020612417833981519152805460ff19169055565b611bac611864565b5f805160206123978339815191527fa16a46d94261c7517cc8ff89f61c0ce93598e3c849801011dee649a6a557d102611be5848261229b565b5060038101611bf4838261229b565b505f8082556001909101555050565b5f805160206123778339815191526001600160a01b038416611c3d5781816002015f828254611c329190612357565b90915550611cad9050565b6001600160a01b0384165f9081526020829052604090205482811015611c8f5760405163391434e360e21b81526001600160a01b038616600482015260248101829052604481018490526064016109a5565b6001600160a01b0385165f9081526020839052604090209083900390555b6001600160a01b038316611ccb576002810180548390039055611ce9565b6001600160a01b0383165f9081526020829052604090208054830190555b826001600160a01b0316846001600160a01b03167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef84604051611d2e91815260200190565b60405180910390a350505050565b606082611d5157611d4c82611d98565b6107a3565b8151158015611d6857506001600160a01b0384163b155b15611d9157604051639996b31560e01b81526001600160a01b03851660048201526024016109a5565b50806107a3565b805115611da85780518082602001fd5b604051630a12f52160e11b815260040160405180910390fd5b5f60208284031215611dd1575f80fd5b81356001600160e01b0319811681146107a3575f80fd5b5f5b83811015611e02578181015183820152602001611dea565b50505f910152565b5f8151808452611e21816020860160208601611de8565b601f01601f19169290920160200192915050565b602081525f6107a36020830184611e0a565b80356001600160a01b0381168114611e5d575f80fd5b919050565b5f8060408385031215611e73575f80fd5b611e7c83611e47565b946020939093013593505050565b5f805f60608486031215611e9c575f80fd5b611ea584611e47565b9250611eb360208501611e47565b9150604084013590509250925092565b5f60208284031215611ed3575f80fd5b5035919050565b5f8060408385031215611eeb575f80fd5b82359150611efb60208401611e47565b90509250929050565b634e487b7160e01b5f52604160045260245ffd5b5f67ffffffffffffffff80841115611f3257611f32611f04565b604051601f8501601f19908116603f01168101908282118183101715611f5a57611f5a611f04565b81604052809350858152868686011115611f72575f80fd5b858560208301375f602087830101525050509392505050565b5f8060408385031215611f9c575f80fd5b611fa583611e47565b9150602083013567ffffffffffffffff811115611fc0575f80fd5b8301601f81018513611fd0575f80fd5b611fdf85823560208401611f18565b9150509250929050565b5f60208284031215611ff9575f80fd5b6107a382611e47565b60ff60f81b881681525f602060e08184015261202160e084018a611e0a565b8381036040850152612033818a611e0a565b606085018990526001600160a01b038816608086015260a0850187905284810360c086015285518082528387019250908301905f5b8181101561208457835183529284019291840191600101612068565b50909c9b505050505050505050505050565b5f805f805f805f60e0888a0312156120ac575f80fd5b6120b588611e47565b96506120c360208901611e47565b95506040880135945060608801359350608088013560ff811681146120e6575f80fd5b9699959850939692959460a0840135945060c09093013592915050565b5f8060408385031215612114575f80fd5b61211d83611e47565b9150611efb60208401611e47565b5f82601f83011261213a575f80fd5b6107a383833560208501611f18565b5f805f6060848603121561215b575f80fd5b833567ffffffffffffffff80821115612172575f80fd5b61217e8783880161212b565b94506020860135915080821115612193575f80fd5b506121a08682870161212b565b925050604084013590509250925092565b600181811c908216806121c557607f821691505b6020821081036121e357634e487b7160e01b5f52602260045260245ffd5b50919050565b5f602082840312156121f9575f80fd5b5051919050565b5f60208284031215612210575f80fd5b815180151581146107a3575f80fd5b634e487b7160e01b5f52602160045260245ffd5b5f8251612244818460208701611de8565b9190910192915050565b601f82111561082d575f81815260208120601f850160051c810160208610156122745750805b601f850160051c820191505b8181101561229357828155600101612280565b505050505050565b815167ffffffffffffffff8111156122b5576122b5611f04565b6122c9816122c384546121b1565b8461224e565b602080601f8311600181146122fc575f84156122e55750858301515b5f19600386901b1c1916600185901b178555612293565b5f85815260208120601f198616915b8281101561232a5788860151825594840194600190910190840161230b565b508582101561234757878501515f19600388901b60f8161c191681555b5050505050600190811b01905550565b808201808211156106c357634e487b7160e01b5f52601160045260245ffdfe52c63247e1f47db19d5ce0460030c497f067ca4cebf71ba98eeadabe20bace00a16a46d94261c7517cc8ff89f61c0ce93598e3c849801011dee649a6a557d100360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbc65d7a28e3265b37a6474929f336521b332c1681b933f6cb9f3376673440d862a02dd7bc7dec4dceedda775e58dd541e08a116c6c53815c0bd028192f7b626800cd5ed15c6e187e77e9aee88184c21f4f2182ab5827cb3b7e07fbedcd63f03300a2646970667358221220eee33a773c114637da3c3c14bf9035a3a94bd1e9935a719aa51b027a22d2896064736f6c63430008140033";
        public TossErc20V1DeploymentBase() : base(BYTECODE) { }
        public TossErc20V1DeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class DefaultAdminRoleFunction : DefaultAdminRoleFunctionBase { }

    [Function("DEFAULT_ADMIN_ROLE", "bytes32")]
    public class DefaultAdminRoleFunctionBase : FunctionMessage
    {

    }

    public partial class DomainSeparatorFunction : DomainSeparatorFunctionBase { }

    [Function("DOMAIN_SEPARATOR", "bytes32")]
    public class DomainSeparatorFunctionBase : FunctionMessage
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

    public partial class Tosserc20v1InitFunction : Tosserc20v1InitFunctionBase { }

    [Function("__TossErc20V1_init")]
    public class Tosserc20v1InitFunctionBase : FunctionMessage
    {
        [Parameter("string", "name_", 1)]
        public virtual string Name { get; set; }
        [Parameter("string", "symbol_", 2)]
        public virtual string Symbol { get; set; }
        [Parameter("uint256", "amount_", 3)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class AllowanceFunction : AllowanceFunctionBase { }

    [Function("allowance", "uint256")]
    public class AllowanceFunctionBase : FunctionMessage
    {
        [Parameter("address", "owner", 1)]
        public virtual string Owner { get; set; }
        [Parameter("address", "spender", 2)]
        public virtual string Spender { get; set; }
    }

    public partial class ApproveFunction : ApproveFunctionBase { }

    [Function("approve", "bool")]
    public class ApproveFunctionBase : FunctionMessage
    {
        [Parameter("address", "spender", 1)]
        public virtual string Spender { get; set; }
        [Parameter("uint256", "value", 2)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class BalanceOfFunction : BalanceOfFunctionBase { }

    [Function("balanceOf", "uint256")]
    public class BalanceOfFunctionBase : FunctionMessage
    {
        [Parameter("address", "account", 1)]
        public virtual string Account { get; set; }
    }

    public partial class BurnFunction : BurnFunctionBase { }

    [Function("burn")]
    public class BurnFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "value", 1)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class BurnFromFunction : BurnFromFunctionBase { }

    [Function("burnFrom")]
    public class BurnFromFunctionBase : FunctionMessage
    {
        [Parameter("address", "account", 1)]
        public virtual string Account { get; set; }
        [Parameter("uint256", "value", 2)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class DecimalsFunction : DecimalsFunctionBase { }

    [Function("decimals", "uint8")]
    public class DecimalsFunctionBase : FunctionMessage
    {

    }

    public partial class Eip712DomainFunction : Eip712DomainFunctionBase { }

    [Function("eip712Domain", typeof(Eip712DomainOutputDTO))]
    public class Eip712DomainFunctionBase : FunctionMessage
    {

    }

    public partial class GetImplementationFunction : GetImplementationFunctionBase { }

    [Function("getImplementation", "address")]
    public class GetImplementationFunctionBase : FunctionMessage
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

    public partial class MintFunction : MintFunctionBase { }

    [Function("mint")]
    public class MintFunctionBase : FunctionMessage
    {
        [Parameter("address", "to", 1)]
        public virtual string To { get; set; }
        [Parameter("uint256", "amount", 2)]
        public virtual BigInteger Amount { get; set; }
    }

    public partial class NameFunction : NameFunctionBase { }

    [Function("name", "string")]
    public class NameFunctionBase : FunctionMessage
    {

    }

    public partial class NoncesFunction : NoncesFunctionBase { }

    [Function("nonces", "uint256")]
    public class NoncesFunctionBase : FunctionMessage
    {
        [Parameter("address", "owner", 1)]
        public virtual string Owner { get; set; }
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

    public partial class PermitFunction : PermitFunctionBase { }

    [Function("permit")]
    public class PermitFunctionBase : FunctionMessage
    {
        [Parameter("address", "owner", 1)]
        public virtual string Owner { get; set; }
        [Parameter("address", "spender", 2)]
        public virtual string Spender { get; set; }
        [Parameter("uint256", "value", 3)]
        public virtual BigInteger Value { get; set; }
        [Parameter("uint256", "deadline", 4)]
        public virtual BigInteger Deadline { get; set; }
        [Parameter("uint8", "v", 5)]
        public virtual byte V { get; set; }
        [Parameter("bytes32", "r", 6)]
        public virtual byte[] R { get; set; }
        [Parameter("bytes32", "s", 7)]
        public virtual byte[] S { get; set; }
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

    public partial class TotalSupplyFunction : TotalSupplyFunctionBase { }

    [Function("totalSupply", "uint256")]
    public class TotalSupplyFunctionBase : FunctionMessage
    {

    }

    public partial class TransferFunction : TransferFunctionBase { }

    [Function("transfer", "bool")]
    public class TransferFunctionBase : FunctionMessage
    {
        [Parameter("address", "to", 1)]
        public virtual string To { get; set; }
        [Parameter("uint256", "value", 2)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class TransferFromFunction : TransferFromFunctionBase { }

    [Function("transferFrom", "bool")]
    public class TransferFromFunctionBase : FunctionMessage
    {
        [Parameter("address", "from", 1)]
        public virtual string From { get; set; }
        [Parameter("address", "to", 2)]
        public virtual string To { get; set; }
        [Parameter("uint256", "value", 3)]
        public virtual BigInteger Value { get; set; }
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
        [Parameter("address", "spender", 2, true )]
        public virtual string Spender { get; set; }
        [Parameter("uint256", "value", 3, false )]
        public virtual BigInteger Value { get; set; }
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
        [Parameter("uint256", "value", 3, false )]
        public virtual BigInteger Value { get; set; }
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

    public partial class ECDSAInvalidSignatureError : ECDSAInvalidSignatureErrorBase { }
    [Error("ECDSAInvalidSignature")]
    public class ECDSAInvalidSignatureErrorBase : IErrorDTO
    {
    }

    public partial class ECDSAInvalidSignatureLengthError : ECDSAInvalidSignatureLengthErrorBase { }

    [Error("ECDSAInvalidSignatureLength")]
    public class ECDSAInvalidSignatureLengthErrorBase : IErrorDTO
    {
        [Parameter("uint256", "length", 1)]
        public virtual BigInteger Length { get; set; }
    }

    public partial class ECDSAInvalidSignatureSError : ECDSAInvalidSignatureSErrorBase { }

    [Error("ECDSAInvalidSignatureS")]
    public class ECDSAInvalidSignatureSErrorBase : IErrorDTO
    {
        [Parameter("bytes32", "s", 1)]
        public virtual byte[] S { get; set; }
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

    public partial class ERC20InsufficientAllowanceError : ERC20InsufficientAllowanceErrorBase { }

    [Error("ERC20InsufficientAllowance")]
    public class ERC20InsufficientAllowanceErrorBase : IErrorDTO
    {
        [Parameter("address", "spender", 1)]
        public virtual string Spender { get; set; }
        [Parameter("uint256", "allowance", 2)]
        public virtual BigInteger Allowance { get; set; }
        [Parameter("uint256", "needed", 3)]
        public virtual BigInteger Needed { get; set; }
    }

    public partial class ERC20InsufficientBalanceError : ERC20InsufficientBalanceErrorBase { }

    [Error("ERC20InsufficientBalance")]
    public class ERC20InsufficientBalanceErrorBase : IErrorDTO
    {
        [Parameter("address", "sender", 1)]
        public virtual string Sender { get; set; }
        [Parameter("uint256", "balance", 2)]
        public virtual BigInteger Balance { get; set; }
        [Parameter("uint256", "needed", 3)]
        public virtual BigInteger Needed { get; set; }
    }

    public partial class ERC20InvalidApproverError : ERC20InvalidApproverErrorBase { }

    [Error("ERC20InvalidApprover")]
    public class ERC20InvalidApproverErrorBase : IErrorDTO
    {
        [Parameter("address", "approver", 1)]
        public virtual string Approver { get; set; }
    }

    public partial class ERC20InvalidReceiverError : ERC20InvalidReceiverErrorBase { }

    [Error("ERC20InvalidReceiver")]
    public class ERC20InvalidReceiverErrorBase : IErrorDTO
    {
        [Parameter("address", "receiver", 1)]
        public virtual string Receiver { get; set; }
    }

    public partial class ERC20InvalidSenderError : ERC20InvalidSenderErrorBase { }

    [Error("ERC20InvalidSender")]
    public class ERC20InvalidSenderErrorBase : IErrorDTO
    {
        [Parameter("address", "sender", 1)]
        public virtual string Sender { get; set; }
    }

    public partial class ERC20InvalidSpenderError : ERC20InvalidSpenderErrorBase { }

    [Error("ERC20InvalidSpender")]
    public class ERC20InvalidSpenderErrorBase : IErrorDTO
    {
        [Parameter("address", "spender", 1)]
        public virtual string Spender { get; set; }
    }

    public partial class ERC2612ExpiredSignatureError : ERC2612ExpiredSignatureErrorBase { }

    [Error("ERC2612ExpiredSignature")]
    public class ERC2612ExpiredSignatureErrorBase : IErrorDTO
    {
        [Parameter("uint256", "deadline", 1)]
        public virtual BigInteger Deadline { get; set; }
    }

    public partial class ERC2612InvalidSignerError : ERC2612InvalidSignerErrorBase { }

    [Error("ERC2612InvalidSigner")]
    public class ERC2612InvalidSignerErrorBase : IErrorDTO
    {
        [Parameter("address", "signer", 1)]
        public virtual string Signer { get; set; }
        [Parameter("address", "owner", 2)]
        public virtual string Owner { get; set; }
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

    public partial class InvalidAccountNonceError : InvalidAccountNonceErrorBase { }

    [Error("InvalidAccountNonce")]
    public class InvalidAccountNonceErrorBase : IErrorDTO
    {
        [Parameter("address", "account", 1)]
        public virtual string Account { get; set; }
        [Parameter("uint256", "currentNonce", 2)]
        public virtual BigInteger CurrentNonce { get; set; }
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

    public partial class DomainSeparatorOutputDTO : DomainSeparatorOutputDTOBase { }

    [FunctionOutput]
    public class DomainSeparatorOutputDTOBase : IFunctionOutputDTO 
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



    public partial class AllowanceOutputDTO : AllowanceOutputDTOBase { }

    [FunctionOutput]
    public class AllowanceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class BalanceOfOutputDTO : BalanceOfOutputDTOBase { }

    [FunctionOutput]
    public class BalanceOfOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }





    public partial class DecimalsOutputDTO : DecimalsOutputDTOBase { }

    [FunctionOutput]
    public class DecimalsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
    }

    public partial class Eip712DomainOutputDTO : Eip712DomainOutputDTOBase { }

    [FunctionOutput]
    public class Eip712DomainOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes1", "fields", 1)]
        public virtual byte[] Fields { get; set; }
        [Parameter("string", "name", 2)]
        public virtual string Name { get; set; }
        [Parameter("string", "version", 3)]
        public virtual string Version { get; set; }
        [Parameter("uint256", "chainId", 4)]
        public virtual BigInteger ChainId { get; set; }
        [Parameter("address", "verifyingContract", 5)]
        public virtual string VerifyingContract { get; set; }
        [Parameter("bytes32", "salt", 6)]
        public virtual byte[] Salt { get; set; }
        [Parameter("uint256[]", "extensions", 7)]
        public virtual List<BigInteger> Extensions { get; set; }
    }

    public partial class GetImplementationOutputDTO : GetImplementationOutputDTOBase { }

    [FunctionOutput]
    public class GetImplementationOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "implementation", 1)]
        public virtual string Implementation { get; set; }
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



    public partial class NameOutputDTO : NameOutputDTOBase { }

    [FunctionOutput]
    public class NameOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class NoncesOutputDTO : NoncesOutputDTOBase { }

    [FunctionOutput]
    public class NoncesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
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

    public partial class TotalSupplyOutputDTO : TotalSupplyOutputDTOBase { }

    [FunctionOutput]
    public class TotalSupplyOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }








}
