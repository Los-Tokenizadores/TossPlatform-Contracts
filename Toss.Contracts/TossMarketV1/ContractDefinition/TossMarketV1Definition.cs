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

namespace Toss.Contracts.TossMarketV1.ContractDefinition
{


    public partial class TossMarketV1Deployment : TossMarketV1DeploymentBase
    {
        public TossMarketV1Deployment() : base(BYTECODE) { }
        public TossMarketV1Deployment(string byteCode) : base(byteCode) { }
    }

    public class TossMarketV1DeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60a06040523060805234801562000014575f80fd5b506200001f6200002f565b620000296200002f565b620000e3565b7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a00805468010000000000000000900460ff1615620000805760405163f92ee8a960e01b815260040160405180910390fd5b80546001600160401b0390811614620000e05780546001600160401b0319166001600160401b0390811782556040519081527fc7f505b2f371ae2175ee4913f4499e1f2633a7b5936321eed1cdaeb6115181d29060200160405180910390a15b50565b6080516125c76200010a5f395f8181611584015281816115ad01526116f801526125c75ff3fe608060405260043610610207575f3560e01c806391d1485411610113578063d01f63f51161009d578063e6d1de5b1161006d578063e6d1de5b1461064d578063f72c0d8b1461066c578063f80395c71461069f578063f92680de146106ca578063fb2657d3146106e9575f80fd5b8063d01f63f5146105db578063d547741f146105ef578063da76d5cd1461060e578063e63ab1e91461062d575f80fd5b8063a1d13e4c116100e3578063a1d13e4c1461050a578063a217fddf14610529578063aaf10f421461053c578063ad3cb1cc14610550578063b464631b1461058d575f80fd5b806391d14854146104855780639612567f146104a457806398590ef9146104cc578063a14a599d146104eb575f80fd5b8063449e815d116101945780636e882326116101645780636e882326146103d757806374dadfc51461040a5780637af148c0146104295780638456cb5914610452578063854cff2f14610466575f80fd5b8063449e815d1461036e5780634f1ef2861461038d57806352d1902d146103a05780635c975abb146103b4575f80fd5b806325a0399f116101da57806325a0399f146102d15780632f2ff15d146102f057806336568abe1461030f5780633c8ac3c21461032e5780633f4ba83a1461035a575f80fd5b806301ffc9a71461020b578063150b7a021461023f57806319ae417714610283578063248a9ca3146102a4575b5f80fd5b348015610216575f80fd5b5061022a610225366004611f95565b610709565b60405190151581526020015b60405180910390f35b34801561024a575f80fd5b5061026a610259366004611fd0565b630a85bd0160e11b95945050505050565b6040516001600160e01b03199091168152602001610236565b34801561028e575f80fd5b506102a261029d366004612082565b610733565b005b3480156102af575f80fd5b506102c36102be3660046120fa565b6107da565b604051908152602001610236565b3480156102dc575f80fd5b506102a26102eb366004612111565b6107fa565b3480156102fb575f80fd5b506102a261030a36600461214e565b610a6c565b34801561031a575f80fd5b506102a261032936600461214e565b610a8e565b348015610339575f80fd5b50610342610ac1565b6040516001600160a01b039091168152602001610236565b348015610365575f80fd5b506102a2610af6565b348015610379575f80fd5b506102c361038836600461217c565b610b18565b6102a261039b3660046121ba565b610bc2565b3480156103ab575f80fd5b506102c3610be1565b3480156103bf575f80fd5b505f805160206125128339815191525460ff1661022a565b3480156103e2575f80fd5b506102c37f95363a1c6133d50d1055ee4d2e6cec2452848b5d944421f2e5b97e2342fcc3ad81565b348015610415575f80fd5b506102a2610424366004612278565b610bfc565b348015610434575f80fd5b505f805160206124b2833981519152546001600160a01b0316610342565b34801561045d575f80fd5b506102a2610c75565b348015610471575f80fd5b506102a2610480366004612278565b610c94565b348015610490575f80fd5b5061022a61049f36600461214e565b610ca7565b3480156104af575f80fd5b506104b961271081565b60405161ffff9091168152602001610236565b3480156104d7575f80fd5b506102a26104e636600461217c565b610cdd565b3480156104f6575f80fd5b506102a26105053660046122a4565b610cf1565b348015610515575f80fd5b506102a26105243660046122bd565b610d57565b348015610534575f80fd5b506102c35f81565b348015610547575f80fd5b50610342610e67565b34801561055b575f80fd5b50610580604051806040016040528060058152602001640352e302e360dc1b81525081565b6040516102369190612312565b348015610598575f80fd5b506105ac6105a736600461217c565b610e75565b604080516001600160a01b0390941684526001600160801b039283166020850152911690820152606001610236565b3480156105e6575f80fd5b50610342610f24565b3480156105fa575f80fd5b506102a261060936600461214e565b610f43565b348015610619575f80fd5b506102a26106283660046120fa565b610f5f565b348015610638575f80fd5b506102c35f805160206124d283398151915281565b348015610658575f80fd5b506102a2610667366004612344565b610fcc565b348015610677575f80fd5b506102c37f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e381565b3480156106aa575f80fd5b505f805160206124b283398151915254600160a01b900461ffff166104b9565b3480156106d5575f80fd5b506102a26106e436600461217c565b610fd7565b3480156106f4575f80fd5b506102c35f8051602061255283398151915281565b5f6001600160e01b031982166325a0399f60e01b148061072d575061072d82610ff4565b92915050565b5f805160206125728339815191526001015460405163d505accf60e01b8152336004820152306024820152604481018790526064810186905260ff8516608482015260a4810184905260c481018390526001600160a01b039091169063d505accf9060e4015f604051808303815f87803b1580156107af575f80fd5b505af11580156107c1573d5f803e3d5ffd5b505050506107d0888888611028565b5050505050505050565b5f9081525f805160206124f2833981519152602052604090206001015490565b61080261137a565b61080a6113b1565b805f805160206124528339815191526001600160a01b0382161580159061083a575080546001600160a01b031615155b80156108ae575080546040516304fec10960e11b81526001600160a01b038481166004830152909116906309fd821290602401602060405180830381865afa158015610888573d5f803e3d5ffd5b505050506040513d601f19601f820116820180604052508101906108ac919061237f565b155b156108dc57604051632dbcdb8b60e01b81526001600160a01b03831660048201526024015b60405180910390fd5b7f95363a1c6133d50d1055ee4d2e6cec2452848b5d944421f2e5b97e2342fcc3ad610906816113e3565b604080516060810182526001600160801b0387811682524290811660208301526001600160a01b038716928201929092523391905f805160206125728339815191526001600160a01b038481165f818152600293909301602090815260408085208e86528252938490208551868301516001600160801b03918216600160801b9183169190910217825595850151600190910180546001600160a01b03191691851691909117905583518686168152948c16908501528b939092918a16917fa9916b84e36c865c02ba63430bf14d3148e20c4bc22751f287840deb3cb22781910160405180910390a4604051632142170760e11b81526001600160a01b038316906342842e0e90610a1f90899030908d9060040161239e565b5f604051808303815f87803b158015610a36575f80fd5b505af1158015610a48573d5f803e3d5ffd5b505050505050505050610a6760015f8051602061253283398151915255565b505050565b610a75826107da565b610a7e816113e3565b610a888383611400565b50505050565b6001600160a01b0381163314610ab75760405163334bd91960e11b815260040160405180910390fd5b610a6782826114a1565b5f5f80516020612552833981519152610ad9816113e3565b50505f80516020612572833981519152546001600160a01b031690565b5f805160206124d2833981519152610b0d816113e3565b610b1561151a565b50565b6001600160a01b038281165f9081525f80516020612492833981519152602090815260408083208584528252808320815160608101835281546001600160801b038082168352600160801b90910416938101849052600191820154909516918501919091529192911115610bb1576040516330b4eb6f60e21b81526001600160a01b0385166004820152602481018490526044016108d3565b516001600160801b03169392505050565b610bca611579565b610bd382611607565b610bdd8282611631565b5050565b5f610bea6116ed565b505f8051602061247283398151915290565b5f610c06816113e3565b6001600160a01b038216610c4657604051631cfe685560e11b81526004016108d39060208082526004908201526362616e6b60e01b604082015260600190565b505f8051602061257283398151915280546001600160a01b0319166001600160a01b0392909216919091179055565b5f805160206124d2833981519152610c8c816113e3565b610b15611736565b5f610c9e816113e3565b610bdd8261177e565b5f9182525f805160206124f2833981519152602090815260408084206001600160a01b0393909316845291905290205460ff1690565b610ce56113b1565b610bdd828260016117af565b5f610cfb816113e3565b61271061ffff83161115610d2857604051630db90fd760e11b815261ffff831660048201526024016108d3565b505f805160206124b2833981519152805461ffff909216600160a01b0261ffff60a01b19909216919091179055565b7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a008054600160401b810460ff16159067ffffffffffffffff165f81158015610d9c5750825b90505f8267ffffffffffffffff166001148015610db85750303b155b905081158015610dc6575080155b15610de45760405163f92ee8a960e01b815260040160405180910390fd5b845467ffffffffffffffff191660011785558315610e0e57845460ff60401b1916600160401b1785555b610e188787611999565b8315610e5e57845460ff60401b19168555604051600181527fc7f505b2f371ae2175ee4913f4499e1f2633a7b5936321eed1cdaeb6115181d29060200160405180910390a15b50505050505050565b5f610e706119cb565b905090565b6001600160a01b038281165f9081525f80516020612492833981519152602090815260408083208584528252808320815160608101835281546001600160801b038082168352600160801b909104169381018490526001918201549095169185019190915291928392821015610f10576040516330b4eb6f60e21b81526001600160a01b0387166004820152602481018690526044016108d3565b604081015190519096909550909350915050565b5f5f805160206124528339815191525b546001600160a01b0316919050565b610f4c826107da565b610f55816113e3565b610a8883836114a1565b610f6761137a565b5f80516020612552833981519152610f7e816113e3565b5f8051602061257283398151915280545f805160206124b283398151915254610fb4916001600160a01b039182169116856119df565b5050610b1560015f8051602061253283398151915255565b610a67838383611028565b610fdf611a3e565b5f610fe9816113e3565b610a6783835f6117af565b5f6001600160e01b03198216637965db0b60e01b148061072d57506301ffc9a760e01b6001600160e01b031983161461072d565b61103061137a565b6110386113b1565b335f80516020612452833981519152811580159061105f575080546001600160a01b031615155b80156110d3575080546040516304fec10960e11b81526001600160a01b038481166004830152909116906309fd821290602401602060405180830381865afa1580156110ad573d5f803e3d5ffd5b505050506040513d601f19601f820116820180604052508101906110d1919061237f565b155b156110fc57604051632dbcdb8b60e01b81526001600160a01b03831660048201526024016108d3565b6001600160a01b038581165f9081525f8051602061249283398151915260209081526040808320888452825291829020825160608101845281546001600160801b038082168352600160801b90910416928101839052600191820154909416928401929092525f8051602061257283398151915292918110156111a4576040516330b4eb6f60e21b81526001600160a01b0389166004820152602481018890526044016108d3565b60408201516001600160a01b03811633036111d45760405163e0b5e6fd60e01b81523360048201526024016108d3565b82516001600160801b038082169089161461121557604051631f25ffe960e11b81526001600160801b038083166004830152891660248201526044016108d3565b6001600160a01b038a81165f81815260028801602090815260408083208e84528252808320928355600190920180546001600160a01b031916905581516001600160801b038881168252861691810191909152338183015290518c938616917f57082ef871637d4099ebc6e727ed1f088c265823259a58312f1e788cb7c98ad5919081900360600190a460018501546112c2906001600160a01b031633306001600160801b038516611a6d565b611300826112eb8760010160149054906101000a900461ffff16846001600160801b0316611a95565b60018801546001600160a01b031691906119df565b604051632142170760e11b81526001600160a01b038b16906342842e0e9061133090309033908e9060040161239e565b5f604051808303815f87803b158015611347575f80fd5b505af1158015611359573d5f803e3d5ffd5b5050505050505050505050610a6760015f8051602061253283398151915255565b5f805160206125328339815191528054600119016113ab57604051633ee5aeb560e01b815260040160405180910390fd5b60029055565b5f805160206125128339815191525460ff16156113e15760405163d93c066560e01b815260040160405180910390fd5b565b610b158133611ac2565b60015f8051602061253283398151915255565b5f5f805160206124f28339815191526114198484610ca7565b611498575f848152602082815260408083206001600160a01b03871684529091529020805460ff1916600117905561144e3390565b6001600160a01b0316836001600160a01b0316857f2f8788117e7eff1d82e926ec794901d17c78024a50270940304540a733656f0d60405160405180910390a4600191505061072d565b5f91505061072d565b5f5f805160206124f28339815191526114ba8484610ca7565b15611498575f848152602082815260408083206001600160a01b0387168085529252808320805460ff1916905551339287917ff6391f5c32d9c69d2a47ea670b442974b53935d1edc7fd64eb21e047a839171b9190a4600191505061072d565b611522611a3e565b5f80516020612512833981519152805460ff191681557f5db9ee0a495bf2e6ff9c91a7834c1ba4fdd244a5e8aa4e537bd38aeae4b073aa335b6040516001600160a01b03909116815260200160405180910390a150565b306001600160a01b037f00000000000000000000000000000000000000000000000000000000000000001614806115e957507f00000000000000000000000000000000000000000000000000000000000000006001600160a01b03166115dd6119cb565b6001600160a01b031614155b156113e15760405163703e46dd60e11b815260040160405180910390fd5b7f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e3610bdd816113e3565b816001600160a01b03166352d1902d6040518163ffffffff1660e01b8152600401602060405180830381865afa92505050801561168b575060408051601f3d908101601f19168201909252611688918101906123c2565b60015b6116b357604051634c9c8ce360e01b81526001600160a01b03831660048201526024016108d3565b5f8051602061247283398151915281146116e357604051632a87526960e21b8152600481018290526024016108d3565b610a678383611afb565b306001600160a01b037f000000000000000000000000000000000000000000000000000000000000000016146113e15760405163703e46dd60e11b815260040160405180910390fd5b61173e6113b1565b5f80516020612512833981519152805460ff191660011781557f62e78cea01bee320cd4e420270b5ea74000d11b0c9f74754ebdbfc544b05a2583361155b565b805f805160206124528339815191525b80546001600160a01b0319166001600160a01b039290921691909117905550565b6117b761137a565b6001600160a01b038381165f9081525f8051602061249283398151915260209081526040808320868452825291829020825160608101845281546001600160801b038082168352600160801b90910416928101839052600191820154909416928401929092525f80516020612572833981519152929181101561185f576040516330b4eb6f60e21b81526001600160a01b0387166004820152602481018690526044016108d3565b604082015184801561187a5750336001600160a01b03821614155b156118a95760405163d7ebabb760e01b81523360048201526001600160a01b03821660248201526044016108d3565b6001600160a01b038781165f81815260028701602090815260408083208b84528252808320928355600190920180546001600160a01b031916905590516001600160801b038616815289938516917f36339059d4a4365312af724d888b8df1cf44c105e8cb14ebdf6015d4727d9b16910160405180910390a4604051632142170760e11b81526001600160a01b038816906342842e0e9061195290309085908b9060040161239e565b5f604051808303815f87803b158015611969575f80fd5b505af115801561197b573d5f803e3d5ffd5b5050505050505050610a6760015f8051602061253283398151915255565b6119a1611b50565b6119a9611b99565b6119b1611ba9565b6119b9611bb1565b6119c1611bc1565b610bdd8282611bd1565b5f5f80516020612472833981519152610f34565b6040516001600160a01b03838116602483015260448201839052610a6791859182169063a9059cbb906064015b604051602081830303815290604052915060e01b6020820180516001600160e01b038381831617835250505050611d07565b5f805160206125128339815191525460ff166113e157604051638dfc202b60e01b815260040160405180910390fd5b610a8884856001600160a01b03166323b872dd868686604051602401611a0c9392919061239e565b5f612710611aa761ffff8516846123ed565b611ab19190612404565b611abb9083612423565b9392505050565b611acc8282610ca7565b610bdd5760405163e2517d3f60e01b81526001600160a01b0382166004820152602481018390526044016108d3565b611b0482611d68565b6040516001600160a01b038316907fbc7cd75a20ee27fd9adebab32041f755214dbc6bffa90cc0225b39da2e5c2d3b905f90a2805115611b4857610a678282611db1565b610bdd611e23565b7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a0054600160401b900460ff166113e157604051631afcd79f60e31b815260040160405180910390fd5b611ba1611b50565b6113e1611e42565b6113e1611b50565b611bb9611b50565b6113e1611e62565b611bc9611b50565b6113e1611ba9565b611bd9611b50565b6001600160a01b038216611c1857604051631cfe685560e11b8152602060048201526005602482015264065726332360dc1b60448201526064016108d3565b61271061ffff82161115611c4557604051630db90fd760e11b815261ffff821660048201526024016108d3565b611c4f5f33611400565b50611c7a7f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e333611400565b50611c925f805160206124d283398151915233611400565b50611caa5f8051602061255283398151915233611400565b505f8051602061257283398151915280546001600160a01b031916331790555f805160206124b2833981519152805461ffff92909216600160a01b026001600160b01b03199092166001600160a01b039390931692909217179055565b5f611d1b6001600160a01b03841683611e6a565b905080515f14158015611d3f575080806020019051810190611d3d919061237f565b155b15610a6757604051635274afe760e01b81526001600160a01b03841660048201526024016108d3565b806001600160a01b03163b5f03611d9d57604051634c9c8ce360e01b81526001600160a01b03821660048201526024016108d3565b805f8051602061247283398151915261178e565b60605f80846001600160a01b031684604051611dcd9190612436565b5f60405180830381855af49150503d805f8114611e05576040519150601f19603f3d011682016040523d82523d5f602084013e611e0a565b606091505b5091509150611e1a858383611e77565b95945050505050565b34156113e15760405163b398979f60e01b815260040160405180910390fd5b611e4a611b50565b5f80516020612512833981519152805460ff19169055565b6113ed611b50565b6060611abb83835f611ed3565b606082611e8c57611e8782611f6c565b611abb565b8151158015611ea357506001600160a01b0384163b155b15611ecc57604051639996b31560e01b81526001600160a01b03851660048201526024016108d3565b5092915050565b606081471015611ef85760405163cd78605960e01b81523060048201526024016108d3565b5f80856001600160a01b03168486604051611f139190612436565b5f6040518083038185875af1925050503d805f8114611f4d576040519150601f19603f3d011682016040523d82523d5f602084013e611f52565b606091505b5091509150611f62868383611e77565b9695505050505050565b805115611f7c5780518082602001fd5b604051630a12f52160e11b815260040160405180910390fd5b5f60208284031215611fa5575f80fd5b81356001600160e01b031981168114611abb575f80fd5b6001600160a01b0381168114610b15575f80fd5b5f805f805f60808688031215611fe4575f80fd5b8535611fef81611fbc565b94506020860135611fff81611fbc565b935060408601359250606086013567ffffffffffffffff80821115612022575f80fd5b818801915088601f830112612035575f80fd5b813581811115612043575f80fd5b896020828501011115612054575f80fd5b9699959850939650602001949392505050565b80356001600160801b038116811461207d575f80fd5b919050565b5f805f805f805f80610100898b03121561209a575f80fd5b88356120a581611fbc565b9750602089013596506120ba60408a01612067565b9550606089013594506080890135935060a089013560ff811681146120dd575f80fd5b979a969950949793969295929450505060c08201359160e0013590565b5f6020828403121561210a575f80fd5b5035919050565b5f805f60608486031215612123575f80fd5b8335925061213360208501612067565b9150604084013561214381611fbc565b809150509250925092565b5f806040838503121561215f575f80fd5b82359150602083013561217181611fbc565b809150509250929050565b5f806040838503121561218d575f80fd5b823561219881611fbc565b946020939093013593505050565b634e487b7160e01b5f52604160045260245ffd5b5f80604083850312156121cb575f80fd5b82356121d681611fbc565b9150602083013567ffffffffffffffff808211156121f2575f80fd5b818501915085601f830112612205575f80fd5b813581811115612217576122176121a6565b604051601f8201601f19908116603f0116810190838211818310171561223f5761223f6121a6565b81604052828152886020848701011115612257575f80fd5b826020860160208301375f6020848301015280955050505050509250929050565b5f60208284031215612288575f80fd5b8135611abb81611fbc565b803561ffff8116811461207d575f80fd5b5f602082840312156122b4575f80fd5b611abb82612293565b5f80604083850312156122ce575f80fd5b82356122d981611fbc565b91506122e760208401612293565b90509250929050565b5f5b8381101561230a5781810151838201526020016122f2565b50505f910152565b602081525f82518060208401526123308160408501602087016122f0565b601f01601f19169190910160400192915050565b5f805f60608486031215612356575f80fd5b833561236181611fbc565b92506020840135915061237660408501612067565b90509250925092565b5f6020828403121561238f575f80fd5b81518015158114611abb575f80fd5b6001600160a01b039384168152919092166020820152604081019190915260600190565b5f602082840312156123d2575f80fd5b5051919050565b634e487b7160e01b5f52601160045260245ffd5b808202811582820484141761072d5761072d6123d9565b5f8261241e57634e487b7160e01b5f52601260045260245ffd5b500490565b8181038181111561072d5761072d6123d9565b5f82516124478184602087016122f0565b919091019291505056fee32c7d464d01bf4699c6877240dec52b29adf1b2f9e414a502cf561c22d22d00360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbcdceca8311a055028be07277e7c9e8760875027fca343beefe7b8eae623484f02dceca8311a055028be07277e7c9e8760875027fca343beefe7b8eae623484f0165d7a28e3265b37a6474929f336521b332c1681b933f6cb9f3376673440d862a02dd7bc7dec4dceedda775e58dd541e08a116c6c53815c0bd028192f7b626800cd5ed15c6e187e77e9aee88184c21f4f2182ab5827cb3b7e07fbedcd63f033009b779b17422d0df92223018b32b4d1fa46e071723d6817e2486d003becc55f002ede4698af956dbb29bc6b3e56b3753d31b8851ba94d3ee864d8a0a779ccb310dceca8311a055028be07277e7c9e8760875027fca343beefe7b8eae623484f00a264697066735822122086f0e1fa53e9013e865552bb61f3a815c41f8c885654d5b4c509de9f041dafd964736f6c63430008140033";
        public TossMarketV1DeploymentBase() : base(BYTECODE) { }
        public TossMarketV1DeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class CutPrecisionFunction : CutPrecisionFunctionBase { }

    [Function("CUT_PRECISION", "uint16")]
    public class CutPrecisionFunctionBase : FunctionMessage
    {

    }

    public partial class DefaultAdminRoleFunction : DefaultAdminRoleFunctionBase { }

    [Function("DEFAULT_ADMIN_ROLE", "bytes32")]
    public class DefaultAdminRoleFunctionBase : FunctionMessage
    {

    }

    public partial class Erc721SellerRoleFunction : Erc721SellerRoleFunctionBase { }

    [Function("ERC721_SELLER_ROLE", "bytes32")]
    public class Erc721SellerRoleFunctionBase : FunctionMessage
    {

    }

    public partial class ExtractRoleFunction : ExtractRoleFunctionBase { }

    [Function("EXTRACT_ROLE", "bytes32")]
    public class ExtractRoleFunctionBase : FunctionMessage
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

    public partial class Tossmarketv1InitFunction : Tossmarketv1InitFunctionBase { }

    [Function("__TossMarketV1_init")]
    public class Tossmarketv1InitFunctionBase : FunctionMessage
    {
        [Parameter("address", "erc20_", 1)]
        public virtual string Erc20 { get; set; }
        [Parameter("uint16", "marketCut_", 2)]
        public virtual ushort Marketcut { get; set; }
    }

    public partial class BuyFunction : BuyFunctionBase { }

    [Function("buy")]
    public class BuyFunctionBase : FunctionMessage
    {
        [Parameter("address", "erc721Address", 1)]
        public virtual string Erc721Address { get; set; }
        [Parameter("uint256", "tokenId", 2)]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("uint128", "price", 3)]
        public virtual BigInteger Price { get; set; }
    }

    public partial class BuyWithPermitFunction : BuyWithPermitFunctionBase { }

    [Function("buyWithPermit")]
    public class BuyWithPermitFunctionBase : FunctionMessage
    {
        [Parameter("address", "erc721Address", 1)]
        public virtual string Erc721Address { get; set; }
        [Parameter("uint256", "tokenId", 2)]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("uint128", "price", 3)]
        public virtual BigInteger Price { get; set; }
        [Parameter("uint256", "amount", 4)]
        public virtual BigInteger Amount { get; set; }
        [Parameter("uint256", "deadline", 5)]
        public virtual BigInteger Deadline { get; set; }
        [Parameter("uint8", "v", 6)]
        public virtual byte V { get; set; }
        [Parameter("bytes32", "r", 7)]
        public virtual byte[] R { get; set; }
        [Parameter("bytes32", "s", 8)]
        public virtual byte[] S { get; set; }
    }

    public partial class CancelFunction : CancelFunctionBase { }

    [Function("cancel")]
    public class CancelFunctionBase : FunctionMessage
    {
        [Parameter("address", "erc721Address", 1)]
        public virtual string Erc721Address { get; set; }
        [Parameter("uint256", "tokenId", 2)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class CancelWhenPausedFunction : CancelWhenPausedFunctionBase { }

    [Function("cancelWhenPaused")]
    public class CancelWhenPausedFunctionBase : FunctionMessage
    {
        [Parameter("address", "erc721Address", 1)]
        public virtual string Erc721Address { get; set; }
        [Parameter("uint256", "tokenId", 2)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class CreateSellOfferFunction : CreateSellOfferFunctionBase { }

    [Function("createSellOffer")]
    public class CreateSellOfferFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "tokenId", 1)]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("uint128", "price", 2)]
        public virtual BigInteger Price { get; set; }
        [Parameter("address", "owner", 3)]
        public virtual string Owner { get; set; }
    }

    public partial class GetFunction : GetFunctionBase { }

    [Function("get", typeof(GetOutputDTO))]
    public class GetFunctionBase : FunctionMessage
    {
        [Parameter("address", "erc721Address", 1)]
        public virtual string Erc721Address { get; set; }
        [Parameter("uint256", "tokenId", 2)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class GetErc20Function : GetErc20FunctionBase { }

    [Function("getErc20", "address")]
    public class GetErc20FunctionBase : FunctionMessage
    {

    }

    public partial class GetErc20BankAddressFunction : GetErc20BankAddressFunctionBase { }

    [Function("getErc20BankAddress", "address")]
    public class GetErc20BankAddressFunctionBase : FunctionMessage
    {

    }

    public partial class GetImplementationFunction : GetImplementationFunctionBase { }

    [Function("getImplementation", "address")]
    public class GetImplementationFunctionBase : FunctionMessage
    {

    }

    public partial class GetMarketCutFunction : GetMarketCutFunctionBase { }

    [Function("getMarketCut", "uint16")]
    public class GetMarketCutFunctionBase : FunctionMessage
    {

    }

    public partial class GetPriceFunction : GetPriceFunctionBase { }

    [Function("getPrice", "uint256")]
    public class GetPriceFunctionBase : FunctionMessage
    {
        [Parameter("address", "erc721Address", 1)]
        public virtual string Erc721Address { get; set; }
        [Parameter("uint256", "tokenId", 2)]
        public virtual BigInteger TokenId { get; set; }
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

    public partial class OnERC721ReceivedFunction : OnERC721ReceivedFunctionBase { }

    [Function("onERC721Received", "bytes4")]
    public class OnERC721ReceivedFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
        [Parameter("address", "", 2)]
        public virtual string ReturnValue2 { get; set; }
        [Parameter("uint256", "", 3)]
        public virtual BigInteger ReturnValue3 { get; set; }
        [Parameter("bytes", "", 4)]
        public virtual byte[] ReturnValue4 { get; set; }
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

    public partial class SetErc20BankAddressFunction : SetErc20BankAddressFunctionBase { }

    [Function("setErc20BankAddress")]
    public class SetErc20BankAddressFunctionBase : FunctionMessage
    {
        [Parameter("address", "newAddress", 1)]
        public virtual string NewAddress { get; set; }
    }

    public partial class SetMarketCutFunction : SetMarketCutFunctionBase { }

    [Function("setMarketCut")]
    public class SetMarketCutFunctionBase : FunctionMessage
    {
        [Parameter("uint16", "cut", 1)]
        public virtual ushort Cut { get; set; }
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

    public partial class WithdrawBalanceFunction : WithdrawBalanceFunctionBase { }

    [Function("withdrawBalance")]
    public class WithdrawBalanceFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "amount", 1)]
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

    public partial class SellOfferCancelledEventDTO : SellOfferCancelledEventDTOBase { }

    [Event("SellOfferCancelled")]
    public class SellOfferCancelledEventDTOBase : IEventDTO
    {
        [Parameter("address", "owner", 1, true )]
        public virtual string Owner { get; set; }
        [Parameter("address", "erc721Address", 2, true )]
        public virtual string Erc721Address { get; set; }
        [Parameter("uint256", "tokenId", 3, true )]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("uint128", "startedAt", 4, false )]
        public virtual BigInteger StartedAt { get; set; }
    }

    public partial class SellOfferCreatedEventDTO : SellOfferCreatedEventDTOBase { }

    [Event("SellOfferCreated")]
    public class SellOfferCreatedEventDTOBase : IEventDTO
    {
        [Parameter("address", "owner", 1, true )]
        public virtual string Owner { get; set; }
        [Parameter("address", "erc721Address", 2, true )]
        public virtual string Erc721Address { get; set; }
        [Parameter("uint256", "tokenId", 3, true )]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("uint128", "startedAt", 4, false )]
        public virtual BigInteger StartedAt { get; set; }
        [Parameter("uint128", "price", 5, false )]
        public virtual BigInteger Price { get; set; }
    }

    public partial class SellOfferSoldEventDTO : SellOfferSoldEventDTOBase { }

    [Event("SellOfferSold")]
    public class SellOfferSoldEventDTOBase : IEventDTO
    {
        [Parameter("address", "owner", 1, true )]
        public virtual string Owner { get; set; }
        [Parameter("address", "erc721Address", 2, true )]
        public virtual string Erc721Address { get; set; }
        [Parameter("uint256", "tokenId", 3, true )]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("uint128", "startedAt", 4, false )]
        public virtual BigInteger StartedAt { get; set; }
        [Parameter("uint128", "price", 5, false )]
        public virtual BigInteger Price { get; set; }
        [Parameter("address", "buyer", 6, false )]
        public virtual string Buyer { get; set; }
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

    public partial class TossCutOutOfRangeError : TossCutOutOfRangeErrorBase { }

    [Error("TossCutOutOfRange")]
    public class TossCutOutOfRangeErrorBase : IErrorDTO
    {
        [Parameter("uint16", "value", 1)]
        public virtual ushort Value { get; set; }
    }

    public partial class TossMarketErc721NotOnSellError : TossMarketErc721NotOnSellErrorBase { }

    [Error("TossMarketErc721NotOnSell")]
    public class TossMarketErc721NotOnSellErrorBase : IErrorDTO
    {
        [Parameter("address", "erc721", 1)]
        public virtual string Erc721 { get; set; }
        [Parameter("uint256", "tokenId", 2)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class TossMarketIsOwnerOfErc721Error : TossMarketIsOwnerOfErc721ErrorBase { }

    [Error("TossMarketIsOwnerOfErc721")]
    public class TossMarketIsOwnerOfErc721ErrorBase : IErrorDTO
    {
        [Parameter("address", "sender", 1)]
        public virtual string Sender { get; set; }
    }

    public partial class TossMarketNotOwnerOfErc721Error : TossMarketNotOwnerOfErc721ErrorBase { }

    [Error("TossMarketNotOwnerOfErc721")]
    public class TossMarketNotOwnerOfErc721ErrorBase : IErrorDTO
    {
        [Parameter("address", "sender", 1)]
        public virtual string Sender { get; set; }
        [Parameter("address", "owner", 2)]
        public virtual string Owner { get; set; }
    }

    public partial class TossMarketSellPriceChangeError : TossMarketSellPriceChangeErrorBase { }

    [Error("TossMarketSellPriceChange")]
    public class TossMarketSellPriceChangeErrorBase : IErrorDTO
    {
        [Parameter("uint128", "realPrice", 1)]
        public virtual BigInteger RealPrice { get; set; }
        [Parameter("uint128", "userPrice", 2)]
        public virtual BigInteger UserPrice { get; set; }
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

    public partial class CutPrecisionOutputDTO : CutPrecisionOutputDTOBase { }

    [FunctionOutput]
    public class CutPrecisionOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint16", "", 1)]
        public virtual ushort ReturnValue1 { get; set; }
    }

    public partial class DefaultAdminRoleOutputDTO : DefaultAdminRoleOutputDTOBase { }

    [FunctionOutput]
    public class DefaultAdminRoleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class Erc721SellerRoleOutputDTO : Erc721SellerRoleOutputDTOBase { }

    [FunctionOutput]
    public class Erc721SellerRoleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class ExtractRoleOutputDTO : ExtractRoleOutputDTOBase { }

    [FunctionOutput]
    public class ExtractRoleOutputDTOBase : IFunctionOutputDTO 
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













    public partial class GetOutputDTO : GetOutputDTOBase { }

    [FunctionOutput]
    public class GetOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "owner", 1)]
        public virtual string Owner { get; set; }
        [Parameter("uint128", "price", 2)]
        public virtual BigInteger Price { get; set; }
        [Parameter("uint128", "startedAt", 3)]
        public virtual BigInteger StartedAt { get; set; }
    }

    public partial class GetErc20OutputDTO : GetErc20OutputDTOBase { }

    [FunctionOutput]
    public class GetErc20OutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetErc20BankAddressOutputDTO : GetErc20BankAddressOutputDTOBase { }

    [FunctionOutput]
    public class GetErc20BankAddressOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "bankAddress", 1)]
        public virtual string BankAddress { get; set; }
    }

    public partial class GetImplementationOutputDTO : GetImplementationOutputDTOBase { }

    [FunctionOutput]
    public class GetImplementationOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "implementation", 1)]
        public virtual string Implementation { get; set; }
    }

    public partial class GetMarketCutOutputDTO : GetMarketCutOutputDTOBase { }

    [FunctionOutput]
    public class GetMarketCutOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint16", "cut", 1)]
        public virtual ushort Cut { get; set; }
    }

    public partial class GetPriceOutputDTO : GetPriceOutputDTOBase { }

    [FunctionOutput]
    public class GetPriceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "price", 1)]
        public virtual BigInteger Price { get; set; }
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

    public partial class OnERC721ReceivedOutputDTO : OnERC721ReceivedOutputDTOBase { }

    [FunctionOutput]
    public class OnERC721ReceivedOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes4", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
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






}
