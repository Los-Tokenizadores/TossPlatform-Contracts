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

namespace Toss.Contracts.TossWhitelistV1.ContractDefinition
{


    public partial class TossWhitelistV1Deployment : TossWhitelistV1DeploymentBase
    {
        public TossWhitelistV1Deployment() : base(BYTECODE) { }
        public TossWhitelistV1Deployment(string byteCode) : base(byteCode) { }
    }

    public class TossWhitelistV1DeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60a060405230608052348015610013575f80fd5b5061001c610021565b6100d3565b7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a00805468010000000000000000900460ff16156100715760405163f92ee8a960e01b815260040160405180910390fd5b80546001600160401b03908116146100d05780546001600160401b0319166001600160401b0390811782556040519081527fc7f505b2f371ae2175ee4913f4499e1f2633a7b5936321eed1cdaeb6115181d29060200160405180910390a15b50565b608051610e506100f95f395f81816107180152818161074101526108a90152610e505ff3fe6080604052600436106100e4575f3560e01c806352d1902d11610087578063ad3cb1cc11610057578063ad3cb1cc14610283578063d547741f146102c0578063f18eb102146102df578063f72c0d8b146102f3575f80fd5b806352d1902d1461021157806391d1485414610225578063a217fddf14610244578063aaf10f4214610257575f80fd5b80632f2ff15d116100c25780632f2ff15d1461019f57806335e3b25a146101c057806336568abe146101df5780634f1ef286146101fe575f80fd5b806301ffc9a7146100e857806309fd82121461011c578063248a9ca314610172575b5f80fd5b3480156100f3575f80fd5b50610107610102366004610baf565b610326565b60405190151581526020015b60405180910390f35b348015610127575f80fd5b50610107610136366004610bf1565b6001600160a01b03165f9081527fcebc1a3b8ad63e53791efc9ab8cbf968aecbbb9586b3861b648d859ddf849800602052604090205460ff1690565b34801561017d575f80fd5b5061019161018c366004610c0a565b61035c565b604051908152602001610113565b3480156101aa575f80fd5b506101be6101b9366004610c21565b61037c565b005b3480156101cb575f80fd5b506101be6101da366004610c4b565b61039e565b3480156101ea575f80fd5b506101be6101f9366004610c21565b6103f2565b6101be61020c366004610c98565b61042a565b34801561021c575f80fd5b50610191610449565b348015610230575f80fd5b5061010761023f366004610c21565b610464565b34801561024f575f80fd5b506101915f81565b348015610262575f80fd5b5061026b61049a565b6040516001600160a01b039091168152602001610113565b34801561028e575f80fd5b506102b3604051806040016040528060058152602001640352e302e360dc1b81525081565b6040516101139190610d76565b3480156102cb575f80fd5b506101be6102da366004610c21565b6104be565b3480156102ea575f80fd5b506101be6104da565b3480156102fe575f80fd5b506101917f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e381565b5f6001600160e01b03198216637965db0b60e01b148061035657506301ffc9a760e01b6001600160e01b03198316145b92915050565b5f9081525f80516020610dfb833981519152602052604090206001015490565b6103858261035c565b61038e816105e6565b61039883836105f3565b50505050565b5f6103a8816105e6565b506001600160a01b03919091165f9081527fcebc1a3b8ad63e53791efc9ab8cbf968aecbbb9586b3861b648d859ddf84980060205260409020805460ff1916911515919091179055565b6001600160a01b038116331461041b5760405163334bd91960e11b815260040160405180910390fd5b6104258282610694565b505050565b61043261070d565b61043b826107b3565b61044582826107dd565b5050565b5f61045261089e565b505f80516020610ddb83398151915290565b5f9182525f80516020610dfb833981519152602090815260408084206001600160a01b0393909316845291905290205460ff1690565b5f6104b95f80516020610ddb833981519152546001600160a01b031690565b905090565b6104c78261035c565b6104d0816105e6565b6103988383610694565b7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a008054600160401b810460ff16159067ffffffffffffffff165f8115801561051f5750825b90505f8267ffffffffffffffff16600114801561053b5750303b155b905081158015610549575080155b156105675760405163f92ee8a960e01b815260040160405180910390fd5b845467ffffffffffffffff19166001178555831561059157845460ff60401b1916600160401b1785555b6105996108e7565b83156105df57845460ff60401b19168555604051600181527fc7f505b2f371ae2175ee4913f4499e1f2633a7b5936321eed1cdaeb6115181d29060200160405180910390a15b5050505050565b6105f08133610907565b50565b5f5f80516020610dfb83398151915261060c8484610464565b61068b575f848152602082815260408083206001600160a01b03871684529091529020805460ff191660011790556106413390565b6001600160a01b0316836001600160a01b0316857f2f8788117e7eff1d82e926ec794901d17c78024a50270940304540a733656f0d60405160405180910390a46001915050610356565b5f915050610356565b5f5f80516020610dfb8339815191526106ad8484610464565b1561068b575f848152602082815260408083206001600160a01b0387168085529252808320805460ff1916905551339287917ff6391f5c32d9c69d2a47ea670b442974b53935d1edc7fd64eb21e047a839171b9190a46001915050610356565b306001600160a01b037f000000000000000000000000000000000000000000000000000000000000000016148061079357507f00000000000000000000000000000000000000000000000000000000000000006001600160a01b03166107875f80516020610ddb833981519152546001600160a01b031690565b6001600160a01b031614155b156107b15760405163703e46dd60e11b815260040160405180910390fd5b565b7f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e3610445816105e6565b816001600160a01b03166352d1902d6040518163ffffffff1660e01b8152600401602060405180830381865afa925050508015610837575060408051601f3d908101601f1916820190925261083491810190610da8565b60015b61086457604051634c9c8ce360e01b81526001600160a01b03831660048201526024015b60405180910390fd5b5f80516020610ddb833981519152811461089457604051632a87526960e21b81526004810182905260240161085b565b6104258383610940565b306001600160a01b037f000000000000000000000000000000000000000000000000000000000000000016146107b15760405163703e46dd60e11b815260040160405180910390fd5b6108ef610995565b6108f76109de565b6108ff6109e6565b6107b16109f6565b6109118282610464565b6104455760405163e2517d3f60e01b81526001600160a01b03821660048201526024810183905260440161085b565b61094982610a33565b6040516001600160a01b038316907fbc7cd75a20ee27fd9adebab32041f755214dbc6bffa90cc0225b39da2e5c2d3b905f90a280511561098d576104258282610a96565b610445610b08565b7ff0c57e16840df040f15088dc2f81fe391c3923bec73e23a9662efc9c229c6a0054600160401b900460ff166107b157604051631afcd79f60e31b815260040160405180910390fd5b6107b1610995565b6109ee610995565b6107b16109de565b6109fe610995565b610a085f336105f3565b506105f07f189ab7a9244df0848122154315af71fe140f3db0fe014031783b0946b8c9d2e3336105f3565b806001600160a01b03163b5f03610a6857604051634c9c8ce360e01b81526001600160a01b038216600482015260240161085b565b5f80516020610ddb83398151915280546001600160a01b0319166001600160a01b0392909216919091179055565b60605f80846001600160a01b031684604051610ab29190610dbf565b5f60405180830381855af49150503d805f8114610aea576040519150601f19603f3d011682016040523d82523d5f602084013e610aef565b606091505b5091509150610aff858383610b27565b95945050505050565b34156107b15760405163b398979f60e01b815260040160405180910390fd5b606082610b3c57610b3782610b86565b610b7f565b8151158015610b5357506001600160a01b0384163b155b15610b7c57604051639996b31560e01b81526001600160a01b038516600482015260240161085b565b50805b9392505050565b805115610b965780518082602001fd5b604051630a12f52160e11b815260040160405180910390fd5b5f60208284031215610bbf575f80fd5b81356001600160e01b031981168114610b7f575f80fd5b80356001600160a01b0381168114610bec575f80fd5b919050565b5f60208284031215610c01575f80fd5b610b7f82610bd6565b5f60208284031215610c1a575f80fd5b5035919050565b5f8060408385031215610c32575f80fd5b82359150610c4260208401610bd6565b90509250929050565b5f8060408385031215610c5c575f80fd5b610c6583610bd6565b915060208301358015158114610c79575f80fd5b809150509250929050565b634e487b7160e01b5f52604160045260245ffd5b5f8060408385031215610ca9575f80fd5b610cb283610bd6565b9150602083013567ffffffffffffffff80821115610cce575f80fd5b818501915085601f830112610ce1575f80fd5b813581811115610cf357610cf3610c84565b604051601f8201601f19908116603f01168101908382118183101715610d1b57610d1b610c84565b81604052828152886020848701011115610d33575f80fd5b826020860160208301375f6020848301015280955050505050509250929050565b5f5b83811015610d6e578181015183820152602001610d56565b50505f910152565b602081525f8251806020840152610d94816040850160208701610d54565b601f01601f19169190910160400192915050565b5f60208284031215610db8575f80fd5b5051919050565b5f8251610dd0818460208701610d54565b919091019291505056fe360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbc02dd7bc7dec4dceedda775e58dd541e08a116c6c53815c0bd028192f7b626800a2646970667358221220ef2d9660660ca82600337507244f6f323f585311fc51d2b147a9cd8d8972045664736f6c63430008140033";
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


}
