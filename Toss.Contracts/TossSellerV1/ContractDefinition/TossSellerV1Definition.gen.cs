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
using Toss.Contracts.TossSellerV1.ContractDefinition;

namespace Toss.Contracts.TossSellerV1.ContractDefinition
{


    public partial class TossSellerV1Deployment : TossSellerV1DeploymentBase
    {
        public TossSellerV1Deployment() : base(BYTECODE) { }
        public TossSellerV1Deployment(string byteCode) : base(byteCode) { }
    }

    public class TossSellerV1DeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60a080604052346100cc57306080527ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a009081549060ff8260401c166100bd57506001600160401b036002600160401b031982821601610078575b60405161266290816100d182396080518181816111da01526112a70152f35b6001600160401b031990911681179091556040519081527fc7f505b2f371ae2175ee4913f4499e1f2633a7b5936321eed1cdaeb6115181d290602090a15f8080610059565b63f92ee8a960e01b8152600490fd5b5f80fdfe6080604081815260049182361015610015575f80fd5b5f92833560e01c91826301ffc9a714611b5d575081630e041681146119ea57816314d75254146119af5781631a936b571461197d5781631c9b725414611902578163241dacd81461177d578163248a9ca3146117465781632d67ab5b1461171b5781632f2ff15d146116f157816330a71b6f146115f957816336568abe146115b35781633c8ac3c2146115765781633f4ba83a146114ff5781634576b95a146114cb5781634799e8c61461149a5781634f1ef2861461122b57816352d1902d146111c55781635c975abb146111955781636523e7f4146110f757816365b2ead814610f085781636615b27d14610ed457816370df41de14610c5557816374dadfc514610bcd5781637af148c014610b985781638456cb5914610b31578163854cff2f14610ae1578163888db7ce146109305781638fc00e38146108ca57816391d148541461087a5781639612567f1461085d578163a217fddf14610842578163a496fd541461064e578163aaf10f4214610619578163ad3cb1cc146105a2578163c0a254231461053b578163cc87e1e8146104b3578163cdccc6c814610483578163d01f63f51461044e578163d116ae26146103de578163d547741f14610392578163d922587714610311578163da76d5cd146102a357508063e63ab1e914610269578063ef7128e11461024e5763f72c0d8b14610211575f80fd5b3461024a578160031936011261024a57602090517f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e38152f35b5080fd5b503461024a578160031936011261024a576020905160288152f35b503461024a578160031936011261024a57602090517f65d7a28e3265b37a6474929f336521b332c1681b933f6cb9f3376673440d862a8152f35b83903461024a57602036600319011261024a576102fd906102c2612227565b6102ca611d32565b5f8051602061250d833981519152545f8051602061260d833981519152549135916001600160a01b039081169116612256565b60015f805160206125ed8339815191525580f35b90503461038e57602036600319011261038e5780359161ffff83169081840361038a5761033c611cb9565b6127108211610375575050505f8051602061258d8339815191529065ffff0000000082549160201b169065ffff00000000191617905580f35b51630db90fd760e11b81529182015260249150fd5b8480fd5b8280fd5b9190503461038e578060031936011261038e576103da91356103d560016103b7611bc6565b938387525f805160206125ad83398151915260205286200154611dc6565b6120eb565b5080f35b82843461044b57602036600319011261044b57610415906103fd611bb0565b906020845161040b81611c27565b8281520152611c81565b60ff82519161042383611c27565b548160206001600160801b03831694858152019160801c168152835192835251166020820152f35b80fd5b50503461024a578160031936011261024a575f8051602061254d8339815191525490516001600160a01b039091168152602090f35b50503461024a578160031936011261024a575f8051602061258d833981519152549051602091821c61ffff168152f35b90503461038e57602036600319011261038e5763ffffffff6104d3611bdc565b6104db611cb9565b169182156105035750505f8051602061258d8339815191529063ffffffff1982541617905580f35b610537925051918291635304ab4b60e01b835282016060906020815260046020820152637261746560e01b60408201520190565b0390fd5b90503461038e57602036600319011261038e57610556611bdc565b9161055f611cb9565b63ffffffff8316156105035750505f8051602061258d8339815191529069ffffffff00000000000082549160301b169069ffffffff000000000000191617905580f35b50503461024a578160031936011261024a5780516105bf81611c27565b60058152602090640352e302e360dc1b8282015282519382859384528251928382860152825b84811061060357505050828201840152601f01601f19168101030190f35b81810183015188820188015287955082016105e5565b50503461024a578160031936011261024a575f8051602061256d8339815191525490516001600160a01b039091168152602090f35b90503461038e57606036600319011261038e57610669611bb0565b91602435926001600160801b039182851680950361083e576044359360ff851680950361083a57610698612227565b6106a0611d32565b6001600160a01b038316801561081f57803b159081156107ab575b50610776578415610747576028851161072b5750906106ed9151946106df86611c27565b855260208501938452611c81565b92511682549160ff60801b905160801b169170ffffffffffffffffffffffffffffffffff19161717905560015f805160206125ed8339815191525580f35b84604492519163b8a1b5a360e01b835282015260286024820152fd5b60206064925191635304ab4b60e01b835282015260096024820152681b585e105b5bdd5b9d60ba1b6044820152fd5b60206064925191631e1b455160e01b8352820152600f60248201526e49546f737353656c6c45726337323160881b6044820152fd5b83516301ffc9a760e01b8152632d24dba360e11b848201529150602090829060249082905afa9081156108155788916107e7575b50155f6106bb565b610808915060203d811161080e575b6108008183611c43565b8101906122a8565b5f6107df565b503d6107f6565b83513d8a823e3d90fd5b509051631cfe685560e11b8152908190610537908201612331565b5f80fd5b8580fd5b50503461024a578160031936011261024a5751908152602090f35b50503461024a578160031936011261024a57602090516127108152f35b90503461038e578160031936011261038e5781602093610898611bc6565b923581525f805160206125ad8339815191528552209060018060a01b03165f52825260ff815f20541690519015158152f35b90503461038e57602036600319011261038e5780359161ffff83169081840361038a576108f5611cb9565b61271082116103755750505f8051602061258d833981519152805461ffff60701b191660709390931b61ffff60701b16929092179091555080f35b8391503461024a578260031936011261024a5761094b611bb0565b92610954611bef565b9161095d612227565b6109656121fd565b61096e336123e6565b6001600160a01b0394808616938415610ac95760ff16908115610a9c5761099490611c81565b8351906109a082611c27565b54906001600160801b039182811680835260ff602084019260801c16825215610a85575160ff16808411610a635750966109e1836109fb93899a5116612352565b905f8051602061250d833981519152541630903390612379565b833b1561038a57604485928385519687948593632d24dba360e11b8552339085015260248401525af1908115610a5a5750610a46575b5060015f805160206125ed8339815191525580f35b610a4f90611bff565b61044b578082610a31565b513d84823e3d90fd5b846064918589895193636dd3fceb60e11b855284015260248301526044820152fd5b855163340d052560e21b8152808601889052602490fd5b8351635304ab4b60e01b81526020818501526006602482015265185b5bdd5b9d60d21b6044820152606490fd5b8351631cfe685560e11b815280610537818601612331565b833461044b57602036600319011261044b57610afb611bb0565b610b03611d32565b5f8051602061254d83398151915280546001600160a01b0319166001600160a01b0390921691909117905580f35b50503461024a578160031936011261024a5760207f62e78cea01bee320cd4e420270b5ea74000d11b0c9f74754ebdbfc544b05a25891610b6f611d6b565b610b776121fd565b5f805160206125cd833981519152805460ff1916600117905551338152a180f35b50503461024a578160031936011261024a575f8051602061250d8339815191525490516001600160a01b039091168152602090f35b90503461038e57602036600319011261038e57610be8611bb0565b91610bf1611d32565b6001600160a01b03831615610c2b575f8051602061260d83398151915280546001600160a01b0319166001600160a01b0385161790558380f35b606492505190631cfe685560e11b825260208183015260248201526362616e6b60e01b6044820152fd5b90503461038e57602036600319011261038e5780356001600160a01b0381169190829003610ed0577ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a009182549160ff83861c16159267ffffffffffffffff811680159081610ec8575b6001149081610ebe575b159081610eb5575b50610ea65767ffffffffffffffff198116600117855583610e87575b50610cf56124ab565b610cfd6124ab565b610d056124ab565b5f805160206125cd833981519152805460ff19169055610d236124ab565b610d2b6124ab565b610d336124ab565b60015f805160206125ed83398151915255610d4c6124ab565b610d546124ab565b610d5c6124ab565b8115610e5b5750610d6c33611df4565b50610d7633611e7f565b50610d8033611f2c565b50610d8a33611fd3565b505f8051602061250d83398151915280546001600160a01b031990811690921790555f8051602061260d8339815191528054909116331790555f8051602061258d8339815191528054670de0b6b3a76400005f8051602061252d833981519152556fffffffffffffffffffffffffffffffff19166f01f4000001f4000001f4000000000064179055610e1a578280f35b805468ff00000000000000001916905551600181527fc7f505b2f371ae2175ee4913f4499e1f2633a7b5936321eed1cdaeb6115181d290602090a15f808280f35b6064906020865191631cfe685560e11b83528201526005602482015264065726332360dc1b6044820152fd5b68ffffffffffffffffff1916680100000000000000011784555f610cec565b50845163f92ee8a960e01b8152fd5b9050155f610cd0565b303b159150610cc8565b859150610cbe565b8380fd5b50503461024a578160031936011261024a5760209063ffffffff5f8051602061258d8339815191525460301c169051908152f35b8391503461024a5760e036600319011261024a57610f24611bb0565b92610f2d611bef565b916084359460ff8616860361083a575f8051602061250d833981519152805490966001600160a01b0391821690813b156110f357855163d505accf60e01b8152338682019081523060208201526044356040820152606435606082015260ff909216608083015260a43560a083015260c43560c083015291889183919082908490829060e00103925af180156110e9579087916110d5575b5050610fcf612227565b610fd76121fd565b610fe0336123e6565b8082169485156110bd5760ff1691821561109057610ffd90611c81565b84519061100982611c27565b54906001600160801b039182811680835260ff602084019260801c16825215611079575160ff1680851161105757509161104b84899a936109fb955116612352565b91309133915416612379565b85606491868a8a5193636dd3fceb60e11b855284015260248301526044820152fd5b865163340d052560e21b8152808701899052602490fd5b8451635304ab4b60e01b81526020818601526006602482015265185b5bdd5b9d60d21b6044820152606490fd5b8451631cfe685560e11b815280610537818701612331565b6110de90611bff565b61083e578588610fc5565b85513d89823e3d90fd5b8780fd5b90503461038e57602036600319011261038e57611112611bdc565b9161111b611cb9565b63ffffffff83161561115b5750505f8051602061258d833981519152805463ffffffff60501b191660509290921b63ffffffff60501b1691909117905580f35b610537925051918291635304ab4b60e01b8352820160609060208152600a6020820152691b5a5b88185b5bdd5b9d60b21b60408201520190565b50503461024a578160031936011261024a5760209060ff5f805160206125cd833981519152541690519015158152f35b82843461044b578060031936011261044b57507f00000000000000000000000000000000000000000000000000000000000000006001600160a01b0316300361121e57602090515f8051602061256d8339815191528152f35b5163703e46dd60e11b8152fd5b9180915060031936011261038e57611241611bb0565b90602493843567ffffffffffffffff811161024a573660238201121561024a57808501359361126f85611c65565b61127b85519182611c43565b85815260209586820193368a838301011161083e578186928b8a93018737830101526001600160a01b037f0000000000000000000000000000000000000000000000000000000000000000811630811490811561147f575b5061146f577f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e3805f525f805160206125ad8339815191528852865f20335f52885260ff875f20541615611452575085516352d1902d60e01b81529083169680828a818b5afa9182918793611422575b505061135e5750505050505191634c9c8ce360e01b8352820152fd5b86899689925f8051602061256d8339815191529081810361140d5750853b156113f85780546001600160a01b0319168317905551869392917fbc7cd75a20ee27fd9adebab32041f755214dbc6bffa90cc0225b39da2e5c2d3b8580a28251156113dc5750506103da9382915190845af46113d661216b565b9161219a565b935093505050346113ec57505080f35b63b398979f60e01b8152fd5b5051634c9c8ce360e01b815291820152859150fd5b848a91845191632a87526960e21b8352820152fd5b9080929350813d831161144b575b61143a8183611c43565b8101031261083e5751905f80611342565b503d611430565b865163e2517d3f60e01b815233818b0152808b0191909152604490fd5b855163703e46dd60e11b81528890fd5b9050815f8051602061256d833981519152541614155f6112d3565b50503461024a578160031936011261024a5760209063ffffffff5f8051602061258d83398151915254169051908152f35b50503461024a578160031936011261024a5760209063ffffffff5f8051602061258d8339815191525460501c169051908152f35b90503461038e578260031936011261038e57611519611d6b565b5f805160206125cd8339815191529081549060ff821615611568575060ff19169055513381527f5db9ee0a495bf2e6ff9c91a7834c1ba4fdd244a5e8aa4e537bd38aeae4b073aa90602090a180f35b8351638dfc202b60e01b8152fd5b50503461024a578160031936011261024a57602090611593611d32565b5f8051602061260d8339815191525490516001600160a01b039091168152f35b83833461024a578060031936011261024a576115cd611bc6565b90336001600160a01b038316036115ea57506103da9192356120eb565b5163334bd91960e11b81528390fd5b90503461038e57602036600319011261038e57803591611617612227565b61161f6121fd565b611628336123e6565b5f8051602061252d83398151915254908184106116d757846102fd855f8051602061258d83398151915254670de0b6b3a764000061271061168d63ffffffff9361167485821687612352565b9061168661ffff809260201c166123d2565b1690612352565b04041681337fa21a23c1d0982547cc8a34216be80fd3bbca603f7aef0d7f61e1092520564d988680a45f8051602061250d83398151915254309033906001600160a01b0316612379565b9260449351926302e8084d60e61b84528301526024820152fd5b9190503461038e578060031936011261038e576103da913561171660016103b7611bc6565b61207a565b50503461024a578160031936011261024a576020905f8051602061252d833981519152549051908152f35b90503461038e57602036600319011261038e57816020936001923581525f805160206125ad83398151915285522001549051908152f35b8391503461024a5760c036600319011261024a5780359060643560ff81168103610ed0575f8051602061250d83398151915280546001600160a01b0393919290841690813b156118fe57875163d505accf60e01b8152338482019081523060208201526024356040820152604435606082015260ff909216608083015260843560a083015260a43560c083015291879183919082908490829060e00103925af180156118f4576118e1575b50611831612227565b6118396121fd565b611842336123e6565b5f8051602061252d833981519152548085106118c45750506102fd9394505f8051602061258d83398151915254670de0b6b3a764000061271061189063ffffffff9361167485821689612352565b04041683337fa21a23c1d0982547cc8a34216be80fd3bbca603f7aef0d7f61e1092520564d988880a4309133915416612379565b86516302e8084d60e61b8152918201859052602482015260449150fd5b6118ed90959195611bff565b9386611828565b87513d88823e3d90fd5b8680fd5b90503461038e57602036600319011261038e57803591611920611cb9565b670de0b6b3a764000083106119435750505f8051602061252d8339815191525580f35b610537925051918291639c5078f760e01b8352820160609060208152600a6020820152691b5a5b88185b5bdd5b9d60b21b60408201520190565b50503461024a578160031936011261024a5760209061ffff5f8051602061258d8339815191525460701c169051908152f35b50503461024a578160031936011261024a57602090517f1756c7e27dc6f5417e166b12944d2f80fa1c2c632a7d5cdb8693f0bc2a286da38152f35b83833461024a578060031936011261024a57611a04611bb0565b6024359163ffffffff9283811680910361038a57611a20612227565b611a286121fd565b611a31836123e6565b611a39611cb9565b6001600160a01b03838116949092908515611b32575f8051602061258d8339815191525491818360501c1690818510611b16575050670de0b6b3a7640000808402908482041484151715611b0357611a9c9061ffff611686818660701c166123d2565b9160301c16908115611af0576102fd9697509061271091040480947f845e055953f11f149ff8c2f740831adaf7647ef0bc4ba8ec22d36888c2546ef38880a45f8051602061250d8339815191525416612256565b634e487b7160e01b875260128852602487fd5b634e487b7160e01b885260118952602488fd5b604491858b925192639bde92d960e01b84528301526024820152fd5b8151631cfe685560e11b81526020818a015260248101899052633ab9b2b960e11b6044820152606490fd5b84913461038e57602036600319011261038e573563ffffffff60e01b811680910361038e5760209250637965db0b60e01b8114908115611b9f575b5015158152f35b6301ffc9a760e01b14905083611b98565b600435906001600160a01b038216820361083a57565b602435906001600160a01b038216820361083a57565b6004359063ffffffff8216820361083a57565b6024359060ff8216820361083a57565b67ffffffffffffffff8111611c1357604052565b634e487b7160e01b5f52604160045260245ffd5b6040810190811067ffffffffffffffff821117611c1357604052565b90601f8019910116810190811067ffffffffffffffff821117611c1357604052565b67ffffffffffffffff8111611c1357601f01601f191660200190565b6001600160a01b03165f9081527f9d4cdb77c8b797a90b348a5d86e9085ba88d4b319afd2f19bbbfb13b223912046020526040902090565b335f9081527f0405792a0f634ab7483a68bde428e86d37884d579ff837813bc28a5f7ea2256860205260409020547f1756c7e27dc6f5417e166b12944d2f80fa1c2c632a7d5cdb8693f0bc2a286da39060ff1615611d145750565b6044906040519063e2517d3f60e01b82523360048301526024820152fd5b335f9081527fb7db2dd08fcb62d0c9e08c51941cae53c267786a0b75803fb7960902fc8ef97d602052604081205460ff1615611d145750565b335f9081527f75442b0a96088b5456bc4ed01394c96a4feec0f883c9494257d76b96ab1c9b6b60205260409020547f65d7a28e3265b37a6474929f336521b332c1681b933f6cb9f3376673440d862a9060ff1615611d145750565b805f525f805160206125ad83398151915260205260405f20335f5260205260ff60405f20541615611d145750565b6001600160a01b03165f8181527fb7db2dd08fcb62d0c9e08c51941cae53c267786a0b75803fb7960902fc8ef97d60205260408120549091905f805160206125ad8339815191529060ff16611e7a578280526020526040822081835260205260408220600160ff1982541617905533915f805160206124ed8339815191528180a4600190565b505090565b6001600160a01b03165f8181527fab71e3f32666744d246edff3f96e4bdafee2e9867098cdd118a979a7464786a860205260408120549091907f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e3905f805160206125ad8339815191529060ff16611f26578184526020526040832082845260205260408320600160ff198254161790555f805160206124ed833981519152339380a4600190565b50505090565b6001600160a01b03165f8181527f75442b0a96088b5456bc4ed01394c96a4feec0f883c9494257d76b96ab1c9b6b60205260408120549091907f65d7a28e3265b37a6474929f336521b332c1681b933f6cb9f3376673440d862a905f805160206125ad8339815191529060ff16611f26578184526020526040832082845260205260408320600160ff198254161790555f805160206124ed833981519152339380a4600190565b6001600160a01b03165f8181527f0405792a0f634ab7483a68bde428e86d37884d579ff837813bc28a5f7ea2256860205260408120549091907f1756c7e27dc6f5417e166b12944d2f80fa1c2c632a7d5cdb8693f0bc2a286da3905f805160206125ad8339815191529060ff16611f26578184526020526040832082845260205260408320600160ff198254161790555f805160206124ed833981519152339380a4600190565b905f918083525f805160206125ad83398151915280602052604084209260018060a01b03169283855260205260ff604085205416155f14611f26578184526020526040832082845260205260408320600160ff198254161790555f805160206124ed833981519152339380a4600190565b905f918083525f805160206125ad83398151915280602052604084209260018060a01b03169283855260205260ff6040852054165f14611f2657818452602052604083208284526020526040832060ff1981541690557ff6391f5c32d9c69d2a47ea670b442974b53935d1edc7fd64eb21e047a839171b339380a4600190565b3d15612195573d9061217c82611c65565b9161218a6040519384611c43565b82523d5f602084013e565b606090565b906121c157508051156121af57805190602001fd5b604051630a12f52160e11b8152600490fd5b815115806121f4575b6121d2575090565b604051639996b31560e01b81526001600160a01b039091166004820152602490fd5b50803b156121ca565b60ff5f805160206125cd833981519152541661221557565b60405163d93c066560e01b8152600490fd5b5f805160206125ed83398151915260028154146122445760029055565b604051633ee5aeb560e01b8152600490fd5b60405163a9059cbb60e01b60208201526001600160a01b039092166024830152604480830193909352918152608081019167ffffffffffffffff831182841017611c13576122a6926040526122c0565b565b9081602091031261083a5751801515810361083a5790565b5f806122e89260018060a01b03169360208151910182865af16122e161216b565b908361219a565b8051908115159182612316575b50506122fe5750565b60249060405190635274afe760e01b82526004820152fd5b61232992506020809183010191016122a8565b155f806122f5565b60609060208152600660208201526565726337323160d01b60408201520190565b8181029291811591840414171561236557565b634e487b7160e01b5f52601160045260245ffd5b6040516323b872dd60e01b60208201526001600160a01b03928316602482015292909116604483015260648083019390935291815260a081019181831067ffffffffffffffff841117611c13576122a6926040526122c0565b9061ffff8092166127100391821161236557565b6001600160a01b03908116908115159081612491575b81612422575b5061240a5750565b60249060405190632dbcdb8b60e01b82526004820152fd5b602491506020905f8051602061254d8339815191525416604051928380926304fec10960e11b82528660048301525afa908115612486575f91612468575b50155f612402565b612480915060203d811161080e576108008183611c43565b5f612460565b6040513d5f823e3d90fd5b5f8051602061254d833981519152548116151591506123fc565b60ff7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a005460401c16156124da57565b604051631afcd79f60e31b8152600490fdfe2f8788117e7eff1d82e926ec794901d17c78024a50270940304540a733656f0d9d4cdb77c8b797a90b348a5d86e9085ba88d4b319afd2f19bbbfb13b223912029d4cdb77c8b797a90b348a5d86e9085ba88d4b319afd2f19bbbfb13b22391200e32c7d464d01bf4699c6877240dec52b29adf1b2f9e414a502cf561c22d22d00360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbc9d4cdb77c8b797a90b348a5d86e9085ba88d4b319afd2f19bbbfb13b2239120102dd7bc7dec4dceedda775e58dd541e08a116c6c53815c0bd028192f7b626800cd5ed15c6e187e77e9aee88184c21f4f2182ab5827cb3b7e07fbedcd63f033009b779b17422d0df92223018b32b4d1fa46e071723d6817e2486d003becc55f009d4cdb77c8b797a90b348a5d86e9085ba88d4b319afd2f19bbbfb13b22391203a264697066735822122073c1c860a00f817ad0c42baeffff6786cd0e6d497d65922a84ba10ffffb2c36364736f6c63430008140033";
        public TossSellerV1DeploymentBase() : base(BYTECODE) { }
        public TossSellerV1DeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class ConvertRoleFunction : ConvertRoleFunctionBase { }

    [Function("CONVERT_ROLE", "bytes32")]
    public class ConvertRoleFunctionBase : FunctionMessage
    {

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

    public partial class PauserRoleFunction : PauserRoleFunctionBase { }

    [Function("PAUSER_ROLE", "bytes32")]
    public class PauserRoleFunctionBase : FunctionMessage
    {

    }

    public partial class SellMaxLimitFunction : SellMaxLimitFunctionBase { }

    [Function("SELL_MAX_LIMIT", "uint8")]
    public class SellMaxLimitFunctionBase : FunctionMessage
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

    public partial class Tosssellerv1InitFunction : Tosssellerv1InitFunctionBase { }

    [Function("__TossSellerV1_init")]
    public class Tosssellerv1InitFunctionBase : FunctionMessage
    {
        [Parameter("address", "erc20", 1)]
        public virtual string Erc20 { get; set; }
    }

    public partial class BuyErc721Function : BuyErc721FunctionBase { }

    [Function("buyErc721")]
    public class BuyErc721FunctionBase : FunctionMessage
    {
        [Parameter("address", "erc721", 1)]
        public virtual string Erc721 { get; set; }
        [Parameter("uint8", "amount", 2)]
        public virtual byte Amount { get; set; }
    }

    public partial class BuyErc721WithPermitFunction : BuyErc721WithPermitFunctionBase { }

    [Function("buyErc721WithPermit")]
    public class BuyErc721WithPermitFunctionBase : FunctionMessage
    {
        [Parameter("address", "erc721", 1)]
        public virtual string Erc721 { get; set; }
        [Parameter("uint8", "buyAmount", 2)]
        public virtual byte BuyAmount { get; set; }
        [Parameter("uint256", "amount", 3)]
        public virtual BigInteger Amount { get; set; }
        [Parameter("uint256", "deadline", 4)]
        public virtual BigInteger Deadline { get; set; }
        [Parameter("uint8", "v", 5)]
        public virtual byte V { get; set; }
        [Parameter("bytes32", "r", 6)]
        public virtual byte[] R { get; set; }
        [Parameter("bytes32", "s", 7)]
        public virtual byte[] S { get; set; }
    }

    public partial class ConvertToErc20Function : ConvertToErc20FunctionBase { }

    [Function("convertToErc20")]
    public class ConvertToErc20FunctionBase : FunctionMessage
    {
        [Parameter("address", "user", 1)]
        public virtual string User { get; set; }
        [Parameter("uint32", "offchainAmount", 2)]
        public virtual uint OffchainAmount { get; set; }
    }

    public partial class ConvertToOffchainFunction : ConvertToOffchainFunctionBase { }

    [Function("convertToOffchain")]
    public class ConvertToOffchainFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "erc20Amount", 1)]
        public virtual BigInteger Erc20Amount { get; set; }
    }

    public partial class ConvertToOffchainWithPermitFunction : ConvertToOffchainWithPermitFunctionBase { }

    [Function("convertToOffchainWithPermit")]
    public class ConvertToOffchainWithPermitFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "erc20Amount", 1)]
        public virtual BigInteger Erc20Amount { get; set; }
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

    public partial class GetConvertToErc20CutFunction : GetConvertToErc20CutFunctionBase { }

    [Function("getConvertToErc20Cut", "uint16")]
    public class GetConvertToErc20CutFunctionBase : FunctionMessage
    {

    }

    public partial class GetConvertToErc20MinAmountFunction : GetConvertToErc20MinAmountFunctionBase { }

    [Function("getConvertToErc20MinAmount", "uint32")]
    public class GetConvertToErc20MinAmountFunctionBase : FunctionMessage
    {

    }

    public partial class GetConvertToErc20RateFunction : GetConvertToErc20RateFunctionBase { }

    [Function("getConvertToErc20Rate", "uint32")]
    public class GetConvertToErc20RateFunctionBase : FunctionMessage
    {

    }

    public partial class GetConvertToOffchainCutFunction : GetConvertToOffchainCutFunctionBase { }

    [Function("getConvertToOffchainCut", "uint16")]
    public class GetConvertToOffchainCutFunctionBase : FunctionMessage
    {

    }

    public partial class GetConvertToOffchainMinAmountFunction : GetConvertToOffchainMinAmountFunctionBase { }

    [Function("getConvertToOffchainMinAmount", "uint256")]
    public class GetConvertToOffchainMinAmountFunctionBase : FunctionMessage
    {

    }

    public partial class GetConvertToOffchainRateFunction : GetConvertToOffchainRateFunctionBase { }

    [Function("getConvertToOffchainRate", "uint32")]
    public class GetConvertToOffchainRateFunctionBase : FunctionMessage
    {

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

    public partial class GetErc721SellsFunction : GetErc721SellsFunctionBase { }

    [Function("getErc721Sells", typeof(GetErc721SellsOutputDTO))]
    public class GetErc721SellsFunctionBase : FunctionMessage
    {
        [Parameter("address", "erc721", 1)]
        public virtual string Erc721 { get; set; }
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

    public partial class SetConvertToErc20CutFunction : SetConvertToErc20CutFunctionBase { }

    [Function("setConvertToErc20Cut")]
    public class SetConvertToErc20CutFunctionBase : FunctionMessage
    {
        [Parameter("uint16", "newCut", 1)]
        public virtual ushort NewCut { get; set; }
    }

    public partial class SetConvertToErc20MinAmountFunction : SetConvertToErc20MinAmountFunctionBase { }

    [Function("setConvertToErc20MinAmount")]
    public class SetConvertToErc20MinAmountFunctionBase : FunctionMessage
    {
        [Parameter("uint32", "minAmount", 1)]
        public virtual uint MinAmount { get; set; }
    }

    public partial class SetConvertToErc20RateFunction : SetConvertToErc20RateFunctionBase { }

    [Function("setConvertToErc20Rate")]
    public class SetConvertToErc20RateFunctionBase : FunctionMessage
    {
        [Parameter("uint32", "newRate", 1)]
        public virtual uint NewRate { get; set; }
    }

    public partial class SetConvertToOffchainCutFunction : SetConvertToOffchainCutFunctionBase { }

    [Function("setConvertToOffchainCut")]
    public class SetConvertToOffchainCutFunctionBase : FunctionMessage
    {
        [Parameter("uint16", "newCut", 1)]
        public virtual ushort NewCut { get; set; }
    }

    public partial class SetConvertToOffchainMinAmountFunction : SetConvertToOffchainMinAmountFunctionBase { }

    [Function("setConvertToOffchainMinAmount")]
    public class SetConvertToOffchainMinAmountFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "minAmount", 1)]
        public virtual BigInteger MinAmount { get; set; }
    }

    public partial class SetConvertToOffchainRateFunction : SetConvertToOffchainRateFunctionBase { }

    [Function("setConvertToOffchainRate")]
    public class SetConvertToOffchainRateFunctionBase : FunctionMessage
    {
        [Parameter("uint32", "newRate", 1)]
        public virtual uint NewRate { get; set; }
    }

    public partial class SetErc20BankAddressFunction : SetErc20BankAddressFunctionBase { }

    [Function("setErc20BankAddress")]
    public class SetErc20BankAddressFunctionBase : FunctionMessage
    {
        [Parameter("address", "newAddress", 1)]
        public virtual string NewAddress { get; set; }
    }

    public partial class SetErc721SellFunction : SetErc721SellFunctionBase { }

    [Function("setErc721Sell")]
    public class SetErc721SellFunctionBase : FunctionMessage
    {
        [Parameter("address", "erc721", 1)]
        public virtual string Erc721 { get; set; }
        [Parameter("uint128", "price", 2)]
        public virtual BigInteger Price { get; set; }
        [Parameter("uint8", "maxAmount", 3)]
        public virtual byte MaxAmount { get; set; }
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

    public partial class ConvertRoleOutputDTO : ConvertRoleOutputDTOBase { }

    [FunctionOutput]
    public class ConvertRoleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
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

    public partial class PauserRoleOutputDTO : PauserRoleOutputDTOBase { }

    [FunctionOutput]
    public class PauserRoleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class SellMaxLimitOutputDTO : SellMaxLimitOutputDTOBase { }

    [FunctionOutput]
    public class SellMaxLimitOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
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













    public partial class GetConvertToErc20CutOutputDTO : GetConvertToErc20CutOutputDTOBase { }

    [FunctionOutput]
    public class GetConvertToErc20CutOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint16", "cut", 1)]
        public virtual ushort Cut { get; set; }
    }

    public partial class GetConvertToErc20MinAmountOutputDTO : GetConvertToErc20MinAmountOutputDTOBase { }

    [FunctionOutput]
    public class GetConvertToErc20MinAmountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint32", "minAmount", 1)]
        public virtual uint MinAmount { get; set; }
    }

    public partial class GetConvertToErc20RateOutputDTO : GetConvertToErc20RateOutputDTOBase { }

    [FunctionOutput]
    public class GetConvertToErc20RateOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint32", "rate", 1)]
        public virtual uint Rate { get; set; }
    }

    public partial class GetConvertToOffchainCutOutputDTO : GetConvertToOffchainCutOutputDTOBase { }

    [FunctionOutput]
    public class GetConvertToOffchainCutOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint16", "cut", 1)]
        public virtual ushort Cut { get; set; }
    }

    public partial class GetConvertToOffchainMinAmountOutputDTO : GetConvertToOffchainMinAmountOutputDTOBase { }

    [FunctionOutput]
    public class GetConvertToOffchainMinAmountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "minAmount", 1)]
        public virtual BigInteger MinAmount { get; set; }
    }

    public partial class GetConvertToOffchainRateOutputDTO : GetConvertToOffchainRateOutputDTOBase { }

    [FunctionOutput]
    public class GetConvertToOffchainRateOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint32", "rate", 1)]
        public virtual uint Rate { get; set; }
    }

    public partial class GetErc20OutputDTO : GetErc20OutputDTOBase { }

    [FunctionOutput]
    public class GetErc20OutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "erc20", 1)]
        public virtual string Erc20 { get; set; }
    }

    public partial class GetErc20BankAddressOutputDTO : GetErc20BankAddressOutputDTOBase { }

    [FunctionOutput]
    public class GetErc20BankAddressOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "bankAddress", 1)]
        public virtual string BankAddress { get; set; }
    }

    public partial class GetErc721SellsOutputDTO : GetErc721SellsOutputDTOBase { }

    [FunctionOutput]
    public class GetErc721SellsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("tuple", "info", 1)]
        public virtual Erc721SellInfo Info { get; set; }
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







    public partial class ConvertToErc20EventDTO : ConvertToErc20EventDTOBase { }

    [Event("ConvertToErc20")]
    public class ConvertToErc20EventDTOBase : IEventDTO
    {
        [Parameter("address", "account", 1, true )]
        public virtual string Account { get; set; }
        [Parameter("uint256", "erc20Amount", 2, true )]
        public virtual BigInteger Erc20Amount { get; set; }
        [Parameter("uint32", "offchainAmount", 3, true )]
        public virtual uint OffchainAmount { get; set; }
    }

    public partial class ConvertToOffchainEventDTO : ConvertToOffchainEventDTOBase { }

    [Event("ConvertToOffchain")]
    public class ConvertToOffchainEventDTOBase : IEventDTO
    {
        [Parameter("address", "account", 1, true )]
        public virtual string Account { get; set; }
        [Parameter("uint256", "erc20Amount", 2, true )]
        public virtual BigInteger Erc20Amount { get; set; }
        [Parameter("uint32", "offchainAmount", 3, true )]
        public virtual uint OffchainAmount { get; set; }
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

    public partial class TossSellConvertErc20AmountLessThanMinError : TossSellConvertErc20AmountLessThanMinErrorBase { }

    [Error("TossSellConvertErc20AmountLessThanMin")]
    public class TossSellConvertErc20AmountLessThanMinErrorBase : IErrorDTO
    {
        [Parameter("uint32", "amount", 1)]
        public virtual uint Amount { get; set; }
        [Parameter("uint32", "min", 2)]
        public virtual uint Min { get; set; }
    }

    public partial class TossSellConvertOffchainAmountLessThanMinError : TossSellConvertOffchainAmountLessThanMinErrorBase { }

    [Error("TossSellConvertOffchainAmountLessThanMin")]
    public class TossSellConvertOffchainAmountLessThanMinErrorBase : IErrorDTO
    {
        [Parameter("uint256", "amount", 1)]
        public virtual BigInteger Amount { get; set; }
        [Parameter("uint256", "min", 2)]
        public virtual BigInteger Min { get; set; }
    }

    public partial class TossSellerBuyAmountGreatherThanMaxError : TossSellerBuyAmountGreatherThanMaxErrorBase { }

    [Error("TossSellerBuyAmountGreatherThanMax")]
    public class TossSellerBuyAmountGreatherThanMaxErrorBase : IErrorDTO
    {
        [Parameter("address", "erc721", 1)]
        public virtual string Erc721 { get; set; }
        [Parameter("uint8", "amount", 2)]
        public virtual byte Amount { get; set; }
        [Parameter("uint8", "max", 3)]
        public virtual byte Max { get; set; }
    }

    public partial class TossSellerBuyMaxAmountExceededError : TossSellerBuyMaxAmountExceededErrorBase { }

    [Error("TossSellerBuyMaxAmountExceeded")]
    public class TossSellerBuyMaxAmountExceededErrorBase : IErrorDTO
    {
        [Parameter("uint8", "amount", 1)]
        public virtual byte Amount { get; set; }
        [Parameter("uint8", "max", 2)]
        public virtual byte Max { get; set; }
    }

    public partial class TossSellerNotOnSellError : TossSellerNotOnSellErrorBase { }

    [Error("TossSellerNotOnSell")]
    public class TossSellerNotOnSellErrorBase : IErrorDTO
    {
        [Parameter("address", "erc721", 1)]
        public virtual string Erc721 { get; set; }
    }

    public partial class TossUnsupportedInterfaceError : TossUnsupportedInterfaceErrorBase { }

    [Error("TossUnsupportedInterface")]
    public class TossUnsupportedInterfaceErrorBase : IErrorDTO
    {
        [Parameter("string", "name", 1)]
        public virtual string Name { get; set; }
    }

    public partial class TossValueIsLessThanOneEtherError : TossValueIsLessThanOneEtherErrorBase { }

    [Error("TossValueIsLessThanOneEther")]
    public class TossValueIsLessThanOneEtherErrorBase : IErrorDTO
    {
        [Parameter("string", "parameter", 1)]
        public virtual string Parameter { get; set; }
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
}
