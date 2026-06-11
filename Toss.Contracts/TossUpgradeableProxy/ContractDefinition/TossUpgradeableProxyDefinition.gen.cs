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
using Toss.Contracts.TossUpgradeableProxy.ContractDefinition;

namespace Toss.Contracts.TossUpgradeableProxy.ContractDefinition
{


    public partial class TossUpgradeableProxyDeployment : TossUpgradeableProxyDeploymentBase
    {
        public TossUpgradeableProxyDeployment() : base(BYTECODE) { }
        public TossUpgradeableProxyDeployment(string byteCode) : base(byteCode) { }
    }

    public class TossUpgradeableProxyDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60806040526102ca803803806100148161018e565b92833981019060408183031261018a5780516001600160a01b03811680820361018a5760208381015190936001600160401b03821161018a570184601f8201121561018a5780519061006d610068836101c7565b61018e565b9582875285838301011161018a5784905f5b8381106101765750505f9186010152813b1561015e577f360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbc80546001600160a01b03191682179055604051907fbc7cd75a20ee27fd9adebab32041f755214dbc6bffa90cc0225b39da2e5c2d3b5f80a283511561014057505f80848461012796519101845af4903d15610137573d610118610068826101c7565b9081525f81943d92013e6101e2565b505b604051608490816102468239f35b606092506101e2565b925050503461014f5750610129565b63b398979f60e01b8152600490fd5b60249060405190634c9c8ce360e01b82526004820152fd5b81810183015188820184015286920161007f565b5f80fd5b6040519190601f01601f191682016001600160401b038111838210176101b357604052565b634e487b7160e01b5f52604160045260245ffd5b6001600160401b0381116101b357601f01601f191660200190565b9061020957508051156101f757805190602001fd5b604051630a12f52160e11b8152600490fd5b8151158061023c575b61021a575090565b604051639996b31560e01b81526001600160a01b039091166004820152602490fd5b50803b1561021256fe60806040527f360894a13ba1a3210667c828492db98dca3e2076cc3735a920a3ca505d382bbc545f90819081906001600160a01b0316368280378136915af43d82803e15604a573d90f35b3d90fdfea26469706673582212207f70b8ba81c43b630b4afc5b158dc03f832cba4f443841166604a4442f547db564736f6c63430008140033";
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
