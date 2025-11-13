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
using Toss.Contracts.TossExchangeV1.ContractDefinition;

namespace Toss.Contracts.TossExchangeV1.ContractDefinition
{


    public partial class TossExchangeV1Deployment : TossExchangeV1DeploymentBase
    {
        public TossExchangeV1Deployment() : base(BYTECODE) { }
        public TossExchangeV1Deployment(string byteCode) : base(byteCode) { }
    }

    public class TossExchangeV1DeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60a080604052346100cc57306080527ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a009081549060ff8260401c166100bd57506001600160401b036002600160401b031982821601610078575b6040516121ec90816100d18239608051818181610c0b0152610cd80152f35b6001600160401b031990911681179091556040519081527fc7f505b2f371ae2175ee4913f4499e1f2633a7b5936321eed1cdaeb6115181d290602090a15f8080610059565b63f92ee8a960e01b8152600490fd5b5f80fdfe608060409080825260049081361015610016575f80fd5b5f91823560e01c91826301ffc9a71461174f5750816302387a7b146115d7578163081fab88146115a35781630915c22d146115075781630af5d693146110e457816318da4151146110af578163248a9ca3146110775781632681285314610fe15781632a67398314610fb35781632f2ff15d14610f8957816336568abe14610f435781633f4ba83a14610ecb5781634f1ef28614610c5c57816352d1902d14610bf657816354469aea14610a755781635c975abb14610a4557816360c6ec6f1461090f5781638456cb59146108a8578163854cff2f1461085857816391d14854146108075781639d633964146105e657918093928263a217fddf146105cb578263aaf10f4214610596578263ad3cb1cc146104fa578263d01f63f5146104c5578263d547741f14610477578263e63ab1e91461043c578263f72c0d8b14610401578263f88a8c1b146101a957505063fa630bc614610172575f80fd5b346101a557816003193601126101a5575f805160206120f78339815191525490516001600160a01b039091168152602090f35b5080fd5b915091346103fd576101ba36611882565b5f805160206120f783398151915280546001600160a01b039a91979691959293908b169190823b156103f957895163d505accf60e01b815233818d019081523060208201526040810193909352606083019490945260ff909516608082015260a081019490945260c0840192909252918391839182908490829060e00103925af180156103ef576103d7575b5050610250611da9565b610258611d04565b61026133611f87565b5f805160206121978339815191525460801c936001600160801b0382169480861061039c5750908692918685519387337f7084f5476618d8e60b11ef0d7d3f06914655adb8793e28ff7f018d4c76d505d58880a35416803b156103985763079cc67960e41b8452339184019182526001600160801b0390921660208201528391839182908490829060400103925af1801561038e57610376575b505061033e925f80516020612117833981519152541690519163a9059cbb60e01b602084015233602484015260448301526044825261033982611828565b611e03565b61034e610349611d2e565b611e74565b5060017f9b779b17422d0df92223018b32b4d1fa46e071723d6817e2486d003becc55f005580f35b61037f906117e4565b61038a57835f6102fb565b8380fd5b83513d84823e3d90fd5b8480fd5b60a4925085606086519363b31eb5cd60e01b85528401526008606484015267776974686472617760c01b608484015260248301526044820152fd5b6103e0906117e4565b6103eb57855f610246565b8580fd5b85513d84823e3d90fd5b8680fd5b5050fd5b5082346101a557816003193601126101a557602090517f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e38152f35b5082346101a557816003193601126101a557602090517f65d7a28e3265b37a6474929f336521b332c1681b933f6cb9f3376673440d862a8152f35b83346104c157806003193601126104c1576104bd91356104b8600161049a6117b8565b938387525f8051602061215783398151915260205286200154611974565b611bf2565b5080f35b8280fd5b5082346101a557816003193601126101a5575f805160206120d78339815191525490516001600160a01b039091168152602090f35b8390346104c157826003193601126104c1578151908282019082821067ffffffffffffffff8311176105835750825260058152602090640352e302e360dc1b8282015282519382859384528251928382860152825b84811061056d57505050828201840152601f01601f19168101030190f35b818101830151888201880152879550820161054f565b604190634e487b7160e01b5f525260245ffd5b5082346101a557816003193601126101a5575f805160206121378339815191525490516001600160a01b039091168152602090f35b5082346101a557816003193601126101a55751908152602090f35b9050346101a5576105f636611882565b5f80516020612117833981519152805490999697956001600160a01b039593918616929091833b1561080357885163d505accf60e01b815233818a019081523060208201526040810194909452606084019590955260ff909516608083015260a082019490945260c081019390935290918791839182908490829060e00103925af180156107f9579086916107e5575b5050610690611da9565b610698611d04565b6106a133611f87565b5f80516020612197833981519152546001600160801b038581169791168088106107ab57506107249086978386519282337f2da466a7b24304f47e87fa2e1e5a81b9831ce54fec19055ce277ca2f39ba42c48c80a35416906323b872dd60e01b60208401523360248401523060448401526064830152606482526103398261180c565b5f805160206120f7833981519152541692833b156103985782516340c10f1960e01b8152339281019283526001600160801b03909116602083015292849184919082908490829060400103925af19081156107a2575061078b575b5061034e610349611d2e565b610794906117e4565b61079f57805f61077f565b80fd5b513d84823e3d90fd5b8360a49189606088519363b31eb5cd60e01b8552840152600760648401526619195c1bdcda5d60ca1b608484015260248301526044820152fd5b6107ee906117e4565b61039857845f610686565b84513d88823e3d90fd5b8a80fd5b839150346104c157816003193601126104c157816020936108266117b8565b923581525f805160206121578339815191528552209060018060a01b03165f52825260ff815f20541690519015158152f35b823461079f57602036600319011261079f576108726117ce565b61087a6118c2565b5f805160206120d783398151915280546001600160a01b0319166001600160a01b0390921691909117905580f35b8284346101a557816003193601126101a55760207f62e78cea01bee320cd4e420270b5ea74000d11b0c9f74754ebdbfc544b05a258916108e6611919565b6108ee611d04565b5f80516020612177833981519152805460ff1916600117905551338152a180f35b9190503461079f578060031936011261079f575061092b611d2e565b9160018060a01b03918282850151169282519485946370a0823160e01b8652308487015285602460209889935afa948515610a3b57908692915f96610a06575b506060015184516318160ddd60e01b81529291839185918391165afa9081156109fc575f916109cf575b508084109384806109c8575b6109af575050505190158152f35b60449351926341f4c3e960e11b84528301526024820152fd5b505f6109a1565b908582813d83116109f5575b6109e58183611844565b8101031261079f5750515f610995565b503d6109db565b83513d5f823e3d90fd5b919282819792973d8311610a34575b610a1f8183611844565b8101031261079f57505193859190606061096b565b503d610a15565b84513d5f823e3d90fd5b8284346101a557816003193601126101a55760209060ff5f80516020612177833981519152541690519015158152f35b8284346101a55760203660031901126101a557610a906117a2565b90610a99611da9565b610aa1611d04565b610aaa33611f87565b5f80516020612197833981519152546001600160801b03838116959116808610610bbe57508394610b44835182337f2da466a7b24304f47e87fa2e1e5a81b9831ce54fec19055ce277ca2f39ba42c48980a35f80516020612117833981519152546323b872dd60e01b60208301523360248301523060448301526064808301949094529281526001600160a01b039283166103398261180c565b5f805160206120f7833981519152541692833b156103985782516340c10f1960e01b8152339281019283526001600160801b03909116602083015292849184919082908490829060400103925af19081156107a25750610baa575061034e610349611d2e565b610bb3906117e4565b61079f57808261077f565b85606060a494519363b31eb5cd60e01b8552840152600760648401526619195c1bdcda5d60ca1b608484015260248301526044820152fd5b83833461079f578060031936011261079f57507f00000000000000000000000000000000000000000000000000000000000000006001600160a01b03163003610c4f57602090515f805160206121378339815191528152f35b5163703e46dd60e11b8152fd5b905082806003193601126104c157610c726117ce565b90602493843567ffffffffffffffff81116101a557366023820112156101a5578085013593610ca085611866565b610cac85519182611844565b85815260209586820193368a83830101116103eb578186928b8a93018737830101526001600160a01b037f00000000000000000000000000000000000000000000000000000000000000008116308114908115610eb0575b50610ea0577f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e3805f525f805160206121578339815191528852865f20335f52885260ff875f20541615610e83575085516352d1902d60e01b81529083169680828a818b5afa9182918793610e53575b5050610d8f5750505050505191634c9c8ce360e01b8352820152fd5b86899689925f8051602061213783398151915290818103610e3e5750853b15610e295780546001600160a01b0319168317905551869392917fbc7cd75a20ee27fd9adebab32041f755214dbc6bffa90cc0225b39da2e5c2d3b8580a2825115610e0d5750506104bd9382915190845af4610e07611c72565b91611ca1565b93509350505034610e1d57505080f35b63b398979f60e01b8152fd5b5051634c9c8ce360e01b815291820152859150fd5b848a91845191632a87526960e21b8352820152fd5b9080929350813d8311610e7c575b610e6b8183611844565b810103126103eb5751908a80610d73565b503d610e61565b865163e2517d3f60e01b815233818b0152808b0191909152604490fd5b855163703e46dd60e11b81528890fd5b9050815f80516020612137833981519152541614158a610d04565b839150346104c157826003193601126104c157610ee6611919565b5f805160206121778339815191529081549060ff821615610f35575060ff19169055513381527f5db9ee0a495bf2e6ff9c91a7834c1ba4fdd244a5e8aa4e537bd38aeae4b073aa90602090a180f35b8351638dfc202b60e01b8152fd5b8284346101a557806003193601126101a557610f5d6117b8565b90336001600160a01b03831603610f7a57506104bd919235611bf2565b5163334bd91960e11b81528390fd5b905082346104c157806003193601126104c1576104bd9135610fae600161049a6117b8565b611b81565b8284346101a557816003193601126101a5576020905f805160206121978339815191525460801c9051908152f35b839150346104c15760203660031901126104c1576001600160801b036110056117a2565b61100d6118c2565b169182156110385750505f80516020612197833981519152906001600160801b031982541617905580f35b611073925051918291635304ab4b60e01b8352820160609060208152600b60208201526a3232b837b9b4ba1036b4b760a91b60408201520190565b0390fd5b839150346104c15760203660031901126104c157816020936001923581525f8051602061215783398151915285522001549051908152f35b8284346101a557816003193601126101a5575f805160206121178339815191525490516001600160a01b039091168152602090f35b905082346104c15760803660031901126104c1576001600160a01b03918035838116929190839003610398576024356001600160801b03808216809203611503576044359586168096036103f95760643590811694858203611503577ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a009687549460ff86881c16159567ffffffffffffffff8116801590816114fb575b60011490816114f1575b1590816114e8575b506114d95767ffffffffffffffff1981166001178a55866114ba575b506111b861205c565b6111c061205c565b6111c861205c565b5f80516020612177833981519152805460ff191690556111e661205c565b6111ee61205c565b6111f661205c565b60017f9b779b17422d0df92223018b32b4d1fa46e071723d6817e2486d003becc55f005561122261205c565b61122a61205c565b61123261205c565b821561148b57811561145c5781831461144f57865163313ce56760e01b80825260209991908a828581895afa918215611445578d92611426575b5089519081528a818581885afa908d821561141b5760ff928392906113ee575b50169116036113df5785156113a5571561136957506112aa336119a2565b506112b433611a2d565b506112be33611ada565b506bffffffffffffffffffffffff60a01b915f8051602061211783398151915290838254161790555f805160206120f7833981519152918254161790555f8051602061219783398151915291878354926001600160801b03199060801b1692161717905561132a578380f35b7fc7f505b2f371ae2175ee4913f4499e1f2633a7b5936321eed1cdaeb6115181d29268ff00000000000000001981541690555160018152a18180808380f35b611073908751918291635304ab4b60e01b8352820160609060208152600c60208201526b3bb4ba34323930bb9036b4b760a11b60408201520190565b8751635304ab4b60e01b81526020818401818152600b918101919091526a3232b837b9b4ba1036b4b760a91b604082015281906060010390fd5b508651638dbfe54f60e01b8152fd5b61140e91508d803d10611414575b6114068183611844565b81019061209d565b8f61128c565b503d6113fc565b8b51903d90823e3d90fd5b61143e9192508b3d8d11611414576114068183611844565b908d61126c565b8a513d8f823e3d90fd5b865162097e5960e51b8152fd5b6064906020885191631cfe685560e11b835282015260086024820152671a5b9d195c9b985b60c21b6044820152fd5b6064906020885191631cfe685560e11b83528201526008602482015267195e1d195c9b985b60c21b6044820152fd5b68ffffffffffffffffff1916680100000000000000011789558a6111af565b50865163f92ee8a960e01b8152fd5b9050158c611193565b303b15915061118b565b889150611181565b5f80fd5b839150346104c15760203660031901126104c1576115236117a2565b9061152c6118c2565b6001600160801b0392838316156115675750505f80516020612197833981519152918254916001600160801b03199060801b16911617905580f35b611073925051918291635304ab4b60e01b8352820160609060208152600c60208201526b3bb4ba34323930bb9036b4b760a11b60408201520190565b8284346101a557816003193601126101a5576020906001600160801b035f8051602061219783398151915254169051908152f35b9050346101a55760203660031901126101a5576115f26117a2565b926115fb611da9565b611603611d04565b61160c33611f87565b5f805160206121978339815191525460801c916001600160801b03851692808410611716575081519083337f7084f5476618d8e60b11ef0d7d3f06914655adb8793e28ff7f018d4c76d505d58780a35f805160206120f7833981519152546001600160a01b039690871691823b156103f95763079cc67960e41b8452339084019081526001600160801b0390911660208201528591839182908490829060400103925af1801561170c576116f9575b5061033e92935f80516020612117833981519152541690519163a9059cbb60e01b602084015233602484015260448301526044825261033982611828565b9261170661033e946117e4565b926116bb565b82513d86823e3d90fd5b83606060a494519363b31eb5cd60e01b85528401526008606484015267776974686472617760c01b608484015260248301526044820152fd5b9150346104c15760203660031901126104c1573563ffffffff60e01b81168091036104c15760209250637965db0b60e01b8114908115611791575b5015158152f35b6301ffc9a760e01b1490508361178a565b600435906001600160801b038216820361150357565b602435906001600160a01b038216820361150357565b600435906001600160a01b038216820361150357565b67ffffffffffffffff81116117f857604052565b634e487b7160e01b5f52604160045260245ffd5b60a0810190811067ffffffffffffffff8211176117f857604052565b6080810190811067ffffffffffffffff8211176117f857604052565b90601f8019910116810190811067ffffffffffffffff8211176117f857604052565b67ffffffffffffffff81116117f857601f01601f191660200190565b60c0906003190112611503576004356001600160801b03811681036115035790602435906044359060643560ff8116810361150357906084359060a43590565b335f9081527fb7db2dd08fcb62d0c9e08c51941cae53c267786a0b75803fb7960902fc8ef97d602052604081205460ff16156118fb5750565b6044906040519063e2517d3f60e01b82523360048301526024820152fd5b335f9081527f75442b0a96088b5456bc4ed01394c96a4feec0f883c9494257d76b96ab1c9b6b60205260409020547f65d7a28e3265b37a6474929f336521b332c1681b933f6cb9f3376673440d862a9060ff16156118fb5750565b805f525f8051602061215783398151915260205260405f20335f5260205260ff60405f205416156118fb5750565b6001600160a01b03165f8181527fb7db2dd08fcb62d0c9e08c51941cae53c267786a0b75803fb7960902fc8ef97d60205260408120549091905f805160206121578339815191529060ff16611a28578280526020526040822081835260205260408220600160ff1982541617905533915f805160206120b78339815191528180a4600190565b505090565b6001600160a01b03165f8181527fab71e3f32666744d246edff3f96e4bdafee2e9867098cdd118a979a7464786a860205260408120549091907f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e3905f805160206121578339815191529060ff16611ad4578184526020526040832082845260205260408320600160ff198254161790555f805160206120b7833981519152339380a4600190565b50505090565b6001600160a01b03165f8181527f75442b0a96088b5456bc4ed01394c96a4feec0f883c9494257d76b96ab1c9b6b60205260408120549091907f65d7a28e3265b37a6474929f336521b332c1681b933f6cb9f3376673440d862a905f805160206121578339815191529060ff16611ad4578184526020526040832082845260205260408320600160ff198254161790555f805160206120b7833981519152339380a4600190565b905f918083525f8051602061215783398151915280602052604084209260018060a01b03169283855260205260ff604085205416155f14611ad4578184526020526040832082845260205260408320600160ff198254161790555f805160206120b7833981519152339380a4600190565b905f918083525f8051602061215783398151915280602052604084209260018060a01b03169283855260205260ff6040852054165f14611ad457818452602052604083208284526020526040832060ff1981541690557ff6391f5c32d9c69d2a47ea670b442974b53935d1edc7fd64eb21e047a839171b339380a4600190565b3d15611c9c573d90611c8382611866565b91611c916040519384611844565b82523d5f602084013e565b606090565b90611cc85750805115611cb657805190602001fd5b604051630a12f52160e11b8152600490fd5b81511580611cfb575b611cd9575090565b604051639996b31560e01b81526001600160a01b039091166004820152602490fd5b50803b15611cd1565b60ff5f805160206121778339815191525416611d1c57565b60405163d93c066560e01b8152600490fd5b60405190611d3b8261180c565b5f80516020612197833981519152546001600160801b0381168352608090811c60208401525f80516020612117833981519152546001600160a01b0390811660408501525f805160206120f783398151915254908116606085015260a01c67ffffffffffffffff1690830152565b7f9b779b17422d0df92223018b32b4d1fa46e071723d6817e2486d003becc55f006002815414611dd95760029055565b604051633ee5aeb560e01b8152600490fd5b90816020910312611503575180151581036115035790565b5f80611e2b9260018060a01b03169360208151910182865af1611e24611c72565b9083611ca1565b8051908115159182611e59575b5050611e415750565b60249060405190635274afe760e01b82526004820152fd5b611e6c9250602080918301019101611deb565b155f80611e38565b60408082015181516370a0823160e01b815230600482015291926020916001600160a01b03919083908590602490829086165afa938415611f7d575f94611f4d575b506060015184516318160ddd60e01b81529291829184916004918391165afa908115610a3b575f91611f21575b50809150821092838094611f19575b611efd575050501590565b6044935051916341f4c3e960e11b835260048301526024820152fd5b506001611ef2565b82813d8311611f46575b611f358183611844565b8101031261079f575051805f611ee3565b503d611f2b565b90938382813d8311611f76575b611f648183611844565b8101031261079f575051926004611eb6565b503d611f5a565b85513d5f823e3d90fd5b6001600160a01b03908116908115159081612042575b81611fc3575b50611fab5750565b60249060405190632dbcdb8b60e01b82526004820152fd5b602491506020905f805160206120d78339815191525416604051928380926304fec10960e11b82528660048301525afa908115612037575f91612009575b50155f611fa3565b61202a915060203d8111612030575b6120228183611844565b810190611deb565b5f612001565b503d612018565b6040513d5f823e3d90fd5b5f805160206120d783398151915254811615159150611f9d565b60ff7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a005460401c161561208b57565b604051631afcd79f60e31b8152600490fd5b90816020910312611503575160ff81168103611503579056fe2f8788117e7eff1d82e926ec794901d17c78024a50270940304540a733656f0de32c7d464d01bf4699c6877240dec52b29adf1b2f9e414a502cf561c22d22d00776aa79b629bec5fcbeeac6d13e962169e431b12b2282b55578c981ec7fe3b02776aa79b629bec5fcbeeac6d13e962169e431b12b2282b55578c981ec7fe3b01360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbc02dd7bc7dec4dceedda775e58dd541e08a116c6c53815c0bd028192f7b626800cd5ed15c6e187e77e9aee88184c21f4f2182ab5827cb3b7e07fbedcd63f03300776aa79b629bec5fcbeeac6d13e962169e431b12b2282b55578c981ec7fe3b00a264697066735822122046564205e87a0d99ddfa65fb4e8b01a32d65f166acdec8b033a0806af988c40164736f6c63430008140033";
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
}
