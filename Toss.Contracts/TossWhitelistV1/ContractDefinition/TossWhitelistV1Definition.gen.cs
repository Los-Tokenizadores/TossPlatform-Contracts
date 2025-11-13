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
using Toss.Contracts.TossWhitelistV1.ContractDefinition;

namespace Toss.Contracts.TossWhitelistV1.ContractDefinition
{


    public partial class TossWhitelistV1Deployment : TossWhitelistV1DeploymentBase
    {
        public TossWhitelistV1Deployment() : base(BYTECODE) { }
        public TossWhitelistV1Deployment(string byteCode) : base(byteCode) { }
    }

    public class TossWhitelistV1DeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60a080604052346100cc57306080527ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a009081549060ff8260401c166100bd57506001600160401b036002600160401b031982821601610078575b604051610d3f90816100d182396080518181816103f701526104c70152f35b6001600160401b031990911681179091556040519081527fc7f505b2f371ae2175ee4913f4499e1f2633a7b5936321eed1cdaeb6115181d290602090a15f8080610059565b63f92ee8a960e01b8152600490fd5b5f80fdfe6080604081815260049182361015610015575f80fd5b5f92833560e01c91826301ffc9a7146108a25750816309fd821214610845578163248a9ca31461080e5781632f2ff15d146107e457816335e3b25a1461073057816336568abe146106ea5781634f1ef2861461044b57816352d1902d146103e257816391d148541461038f578163a217fddf14610374578163aaf10f421461033f578163ad3cb1cc146102a3578163d547741f14610257578163f18eb10214610104575063f72c0d8b146100c7575f80fd5b34610100578160031936011261010057602090517f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e38152f35b5080fd5b9050346102535782600319360112610253577ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a0090815460ff81851c16159167ffffffffffffffff82168015908161024b575b6001149081610241575b159081610238575b5061022a575067ffffffffffffffff19811660011783558161020b575b5061018e610c88565b610196610c88565b61019e610c88565b6101a6610c88565b6101ae610c88565b6101b7336109c3565b506101c133610a61565b506101ca578280f35b805468ff00000000000000001916905551600181527fc7f505b2f371ae2175ee4913f4499e1f2633a7b5936321eed1cdaeb6115181d290602090a15f808280f35b68ffffffffffffffffff1916680100000000000000011782555f610185565b845163f92ee8a960e01b8152fd5b9050155f610168565b303b159150610160565b849150610156565b8280fd5b9190503461025357806003193601126102535761029f913561029a600161027c61090f565b938387525f80516020610cea83398151915260205286200154610977565b610ba5565b5080f35b9050346102535782600319360112610253578151908282019082821067ffffffffffffffff83111761032c5750825260058152602090640352e302e360dc1b8282015282519382859384528251928382860152825b84811061031657505050828201840152601f01601f19168101030190f35b81810183015188820188015287955082016102f8565b634e487b7160e01b855260419052602484fd5b5050346101005781600319360112610100575f80516020610cca8339815191525490516001600160a01b039091168152602090f35b50503461010057816003193601126101005751908152602090f35b9050346102535781600319360112610253578160209360ff926103b061090f565b903582525f80516020610cea83398151915286528282206001600160a01b039091168252855220549151911615158152f35b828434610448578060031936011261044857507f00000000000000000000000000000000000000000000000000000000000000006001600160a01b0316300361043b57602090515f80516020610cca8339815191528152f35b5163703e46dd60e11b8152fd5b80fd5b91809150600319360112610253576104616108f5565b90602493843567ffffffffffffffff81116101005736602382011215610100578085013561048e8161095b565b9461049b85519687610925565b81865260209182870193368a83830101116106e6578186928b8693018737880101526001600160a01b037f000000000000000000000000000000000000000000000000000000000000000081163081149081156106cb575b506106bb577f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e38086525f80516020610cea8339815191528452868620338752845260ff87872054161561069e575081169585516352d1902d60e01b815283818a818b5afa86918161066b575b5061057b575050505050505191634c9c8ce360e01b8352820152fd5b9088888894938c5f80516020610cca833981519152918281036106565750853b15610642575080546001600160a01b031916821790558451889392917fbc7cd75a20ee27fd9adebab32041f755214dbc6bffa90cc0225b39da2e5c2d3b8580a282511561062457505061029f9582915190845af4913d1561061a573d61060c6106038261095b565b92519283610925565b81528581943d92013e610c25565b5060609250610c25565b95509550505050503461063657505080f35b63b398979f60e01b8152fd5b8651634c9c8ce360e01b8152808501849052fd5b8751632a87526960e21b815280860191909152fd5b9091508481813d8311610697575b6106838183610925565b810103126106935751905f61055f565b8680fd5b503d610679565b865163e2517d3f60e01b815233818b0152808b0191909152604490fd5b855163703e46dd60e11b81528890fd5b9050815f80516020610cca833981519152541614155f6104f3565b8580fd5b83833461010057806003193601126101005761070461090f565b90336001600160a01b03831603610721575061029f919235610ba5565b5163334bd91960e11b81528390fd5b9190503461025357806003193601126102535761074b6108f5565b602435928315158094036107e0578480525f80516020610cea83398151915260205282852033865260205260ff8386205416156107c3575060018060a01b031683527fcebc1a3b8ad63e53791efc9ab8cbf968aecbbb9586b3861b648d859ddf84980060205282209060ff8019835416911617905580f35b6044908584519163e2517d3f60e01b835233908301526024820152fd5b8480fd5b9190503461025357806003193601126102535761029f9135610809600161027c61090f565b610b21565b90503461025357602036600319011261025357816020936001923581525f80516020610cea83398151915285522001549051908152f35b5050346101005760203660031901126101005760209160ff9082906001600160a01b036108706108f5565b1681527fcebc1a3b8ad63e53791efc9ab8cbf968aecbbb9586b3861b648d859ddf849800855220541690519015158152f35b849134610253576020366003190112610253573563ffffffff60e01b81168091036102535760209250637965db0b60e01b81149081156108e4575b5015158152f35b6301ffc9a760e01b149050836108dd565b600435906001600160a01b038216820361090b57565b5f80fd5b602435906001600160a01b038216820361090b57565b90601f8019910116810190811067ffffffffffffffff82111761094757604052565b634e487b7160e01b5f52604160045260245ffd5b67ffffffffffffffff811161094757601f01601f191660200190565b805f525f80516020610cea83398151915260205260405f20335f5260205260ff60405f205416156109a55750565b6044906040519063e2517d3f60e01b82523360048301526024820152fd5b6001600160a01b03165f8181527fb7db2dd08fcb62d0c9e08c51941cae53c267786a0b75803fb7960902fc8ef97d60205260408120549091905f80516020610cea8339815191529060ff16610a5c578280526020526040822081835260205260408220600160ff1982541617905533917f2f8788117e7eff1d82e926ec794901d17c78024a50270940304540a733656f0d8180a4600190565b505090565b6001600160a01b03165f8181527fab71e3f32666744d246edff3f96e4bdafee2e9867098cdd118a979a7464786a860205260408120549091907f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e3905f80516020610cea8339815191529060ff16610b1b578184526020526040832082845260205260408320600160ff198254161790557f2f8788117e7eff1d82e926ec794901d17c78024a50270940304540a733656f0d339380a4600190565b50505090565b905f918083525f80516020610cea83398151915280602052604084209260018060a01b03169283855260205260ff604085205416155f14610b1b578184526020526040832082845260205260408320600160ff198254161790557f2f8788117e7eff1d82e926ec794901d17c78024a50270940304540a733656f0d339380a4600190565b905f918083525f80516020610cea83398151915280602052604084209260018060a01b03169283855260205260ff6040852054165f14610b1b57818452602052604083208284526020526040832060ff1981541690557ff6391f5c32d9c69d2a47ea670b442974b53935d1edc7fd64eb21e047a839171b339380a4600190565b90610c4c5750805115610c3a57805190602001fd5b604051630a12f52160e11b8152600490fd5b81511580610c7f575b610c5d575090565b604051639996b31560e01b81526001600160a01b039091166004820152602490fd5b50803b15610c55565b60ff7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a005460401c1615610cb757565b604051631afcd79f60e31b8152600490fdfe360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbc02dd7bc7dec4dceedda775e58dd541e08a116c6c53815c0bd028192f7b626800a26469706673582212207cfcecd0ad0f082c1807ae3ef43511313eb159f426bd9b87e3dcc02bc81f5e5764736f6c63430008140033";
        public TossWhitelistV1DeploymentBase() : base(BYTECODE) { }
        public TossWhitelistV1DeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class DefaultAdminRoleFunction : DefaultAdminRoleFunctionBase { }

    [Function("DEFAULT_ADMIN_ROLE", "bytes32")]
    public class DefaultAdminRoleFunctionBase : FunctionMessage
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

    public partial class Tosswhitelistv1InitFunction : Tosswhitelistv1InitFunctionBase { }

    [Function("__TossWhitelistV1_init")]
    public class Tosswhitelistv1InitFunctionBase : FunctionMessage
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

    public partial class IsInWhitelistFunction : IsInWhitelistFunctionBase { }

    [Function("isInWhitelist", "bool")]
    public class IsInWhitelistFunctionBase : FunctionMessage
    {
        [Parameter("address", "address_", 1)]
        public virtual string Address { get; set; }
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

    public partial class SetFunction : SetFunctionBase { }

    [Function("set")]
    public class SetFunctionBase : FunctionMessage
    {
        [Parameter("address", "address_", 1)]
        public virtual string Address { get; set; }
        [Parameter("bool", "enabled", 2)]
        public virtual bool Enabled { get; set; }
    }

    public partial class SupportsInterfaceFunction : SupportsInterfaceFunctionBase { }

    [Function("supportsInterface", "bool")]
    public class SupportsInterfaceFunctionBase : FunctionMessage
    {
        [Parameter("bytes4", "interfaceId", 1)]
        public virtual byte[] InterfaceId { get; set; }
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

    public partial class DefaultAdminRoleOutputDTO : DefaultAdminRoleOutputDTOBase { }

    [FunctionOutput]
    public class DefaultAdminRoleOutputDTOBase : IFunctionOutputDTO 
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



    public partial class HasRoleOutputDTO : HasRoleOutputDTOBase { }

    [FunctionOutput]
    public class HasRoleOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class IsInWhitelistOutputDTO : IsInWhitelistOutputDTOBase { }

    [FunctionOutput]
    public class IsInWhitelistOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "value", 1)]
        public virtual bool Value { get; set; }
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



    public partial class InitializedEventDTO : InitializedEventDTOBase { }

    [Event("Initialized")]
    public class InitializedEventDTOBase : IEventDTO
    {
        [Parameter("uint64", "version", 1, false )]
        public virtual ulong Version { get; set; }
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
