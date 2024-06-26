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

namespace Toss.Contracts.TossUpgradeableProxy.ContractDefinition
{


    public partial class TossUpgradeableProxyDeployment : TossUpgradeableProxyDeploymentBase
    {
        public TossUpgradeableProxyDeployment() : base(BYTECODE) { }
        public TossUpgradeableProxyDeployment(string byteCode) : base(byteCode) { }
    }

    public class TossUpgradeableProxyDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60806040526040516103f43803806103f483398101604081905261002291610262565b818161002e8282610037565b50505050610345565b61004082610095565b6040516001600160a01b038316907fbc7cd75a20ee27fd9adebab32041f755214dbc6bffa90cc0225b39da2e5c2d3b905f90a2805115610089576100848282610110565b505050565b610091610183565b5050565b806001600160a01b03163b5f036100cf57604051634c9c8ce360e01b81526001600160a01b03821660048201526024015b60405180910390fd5b7f360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbc80546001600160a01b0319166001600160a01b0392909216919091179055565b60605f80846001600160a01b03168460405161012c919061032a565b5f60405180830381855af49150503d805f8114610164576040519150601f19603f3d011682016040523d82523d5f602084013e610169565b606091505b50909250905061017a8583836101a4565b95945050505050565b34156101a25760405163b398979f60e01b815260040160405180910390fd5b565b6060826101b9576101b482610203565b6101fc565b81511580156101d057506001600160a01b0384163b155b156101f957604051639996b31560e01b81526001600160a01b03851660048201526024016100c6565b50805b9392505050565b8051156102135780518082602001fd5b604051630a12f52160e11b815260040160405180910390fd5b634e487b7160e01b5f52604160045260245ffd5b5f5b8381101561025a578181015183820152602001610242565b50505f910152565b5f8060408385031215610273575f80fd5b82516001600160a01b0381168114610289575f80fd5b60208401519092506001600160401b03808211156102a5575f80fd5b818501915085601f8301126102b8575f80fd5b8151818111156102ca576102ca61022c565b604051601f8201601f19908116603f011681019083821181831017156102f2576102f261022c565b8160405282815288602084870101111561030a575f80fd5b61031b836020830160208801610240565b80955050505050509250929050565b5f825161033b818460208701610240565b9190910192915050565b60a3806103515f395ff3fe6080604052600a600c565b005b60186014601a565b6050565b565b5f604b7f360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbc546001600160a01b031690565b905090565b365f80375f80365f845af43d5f803e8080156069573d5ff35b3d5ffdfea264697066735822122081a1fca39a851a645d7a2a96de58cce5db2a60db59f81c3255f005dfe43e109c64736f6c63430008140033";
        public TossUpgradeableProxyDeploymentBase() : base(BYTECODE) { }
        public TossUpgradeableProxyDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address", "_logic", 1)]
        public virtual string Logic { get; set; }
        [Parameter("bytes", "_data", 2)]
        public virtual byte[] Data { get; set; }
    }

    public partial class UpgradedEventDTO : UpgradedEventDTOBase { }

    [Event("Upgraded")]
    public class UpgradedEventDTOBase : IEventDTO
    {
        [Parameter("address", "implementation", 1, true )]
        public virtual string Implementation { get; set; }
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
}
