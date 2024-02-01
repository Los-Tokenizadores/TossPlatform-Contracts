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

namespace Toss.Contracts.ExternalErc20.ContractDefinition
{


    public partial class ExternalErc20Deployment : ExternalErc20DeploymentBase
    {
        public ExternalErc20Deployment() : base(BYTECODE) { }
        public ExternalErc20Deployment(string byteCode) : base(byteCode) { }
    }

    public class ExternalErc20DeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "61016060405234801562000011575f80fd5b5060408051602080820183525f8083528351808501855260018152603160f81b81840152845180840186528281528551938401909552908252919283929160036200005d83826200024f565b5060046200006c82826200024f565b506200007e915083905060056200012c565b610120526200008f8160066200012c565b61014052815160208084019190912060e052815190820120610100524660a0526200011c60e05161010051604080517f8b73c3c69bb8fe3d512ecc4cf759cc79239f7b179b0ffacaa9a75d522b39400f60208201529081019290925260608201524660808201523060a08201525f9060c00160405160208183030381529060405280519060200120905090565b60805250503060c0525062000388565b5f6020835110156200014b57620001438362000164565b90506200015e565b816200015884826200024f565b5060ff90505b92915050565b5f80829050601f815111156200019a578260405163305a27a960e01b815260040162000191919062000317565b60405180910390fd5b8051620001a78262000364565b179392505050565b634e487b7160e01b5f52604160045260245ffd5b600181811c90821680620001d857607f821691505b602082108103620001f757634e487b7160e01b5f52602260045260245ffd5b50919050565b601f8211156200024a575f81815260208120601f850160051c81016020861015620002255750805b601f850160051c820191505b81811015620002465782815560010162000231565b5050505b505050565b81516001600160401b038111156200026b576200026b620001af565b62000283816200027c8454620001c3565b84620001fd565b602080601f831160018114620002b9575f8415620002a15750858301515b5f19600386901b1c1916600185901b17855562000246565b5f85815260208120601f198616915b82811015620002e957888601518255948401946001909101908401620002c8565b50858210156200030757878501515f19600388901b60f8161c191681555b5050505050600190811b01905550565b5f6020808352835180828501525f5b81811015620003445785810183015185820160400152820162000326565b505f604082860101526040601f19601f8301168501019250505092915050565b80516020808301519190811015620001f7575f1960209190910360031b1b16919050565b60805160a05160c05160e051610100516101205161014051610eac620003da5f395f6106e101525f6106b401525f61065d01525f61063501525f61059001525f6105ba01525f6105e40152610eac5ff3fe608060405234801561000f575f80fd5b50600436106100cb575f3560e01c806370a082311161008857806395d89b411161006357806395d89b41146101a2578063a9059cbb146101aa578063d505accf146101bd578063dd62ed3e146101d2575f80fd5b806370a082311461014c5780637ecebe001461017457806384b0196e14610187575f80fd5b806306fdde03146100cf578063095ea7b3146100ed57806318160ddd1461011057806323b872dd14610122578063313ce567146101355780633644e51514610144575b5f80fd5b6100d761020a565b6040516100e49190610c2b565b60405180910390f35b6101006100fb366004610c5f565b61029a565b60405190151581526020016100e4565b6002545b6040519081526020016100e4565b610100610130366004610c87565b6102b3565b604051601281526020016100e4565b6101146102d6565b61011461015a366004610cc0565b6001600160a01b03165f9081526020819052604090205490565b610114610182366004610cc0565b6102e4565b61018f610301565b6040516100e49796959493929190610cd9565b6100d7610343565b6101006101b8366004610c5f565b610352565b6101d06101cb366004610d6d565b61035f565b005b6101146101e0366004610dda565b6001600160a01b039182165f90815260016020908152604080832093909416825291909152205490565b60606003805461021990610e0b565b80601f016020809104026020016040519081016040528092919081815260200182805461024590610e0b565b80156102905780601f1061026757610100808354040283529160200191610290565b820191905f5260205f20905b81548152906001019060200180831161027357829003601f168201915b5050505050905090565b5f336102a781858561049a565b60019150505b92915050565b5f336102c08582856104ac565b6102cb858585610527565b506001949350505050565b5f6102df610584565b905090565b6001600160a01b0381165f908152600760205260408120546102ad565b5f6060805f805f60606103126106ad565b61031a6106da565b604080515f80825260208201909252600f60f81b9b939a50919850469750309650945092509050565b60606004805461021990610e0b565b5f336102a7818585610527565b834211156103885760405163313c898160e11b8152600481018590526024015b60405180910390fd5b5f7f6e71edae12b1b97f4d1f60370fef10105fa2faae0126114a169c64845d6126c98888886103d38c6001600160a01b03165f90815260076020526040902080546001810190915590565b6040805160208101969096526001600160a01b0394851690860152929091166060840152608083015260a082015260c0810186905260e0016040516020818303038152906040528051906020012090505f61042d82610707565b90505f61043c82878787610733565b9050896001600160a01b0316816001600160a01b031614610483576040516325c0072360e11b81526001600160a01b0380831660048301528b16602482015260440161037f565b61048e8a8a8a61049a565b50505050505050505050565b6104a7838383600161075f565b505050565b6001600160a01b038381165f908152600160209081526040808320938616835292905220545f198114610521578181101561051357604051637dc7a0d960e11b81526001600160a01b0384166004820152602481018290526044810183905260640161037f565b61052184848484035f61075f565b50505050565b6001600160a01b03831661055057604051634b637e8f60e11b81525f600482015260240161037f565b6001600160a01b0382166105795760405163ec442f0560e01b81525f600482015260240161037f565b6104a7838383610831565b5f306001600160a01b037f0000000000000000000000000000000000000000000000000000000000000000161480156105dc57507f000000000000000000000000000000000000000000000000000000000000000046145b1561060657507f000000000000000000000000000000000000000000000000000000000000000090565b6102df604080517f8b73c3c69bb8fe3d512ecc4cf759cc79239f7b179b0ffacaa9a75d522b39400f60208201527f0000000000000000000000000000000000000000000000000000000000000000918101919091527f000000000000000000000000000000000000000000000000000000000000000060608201524660808201523060a08201525f9060c00160405160208183030381529060405280519060200120905090565b60606102df7f00000000000000000000000000000000000000000000000000000000000000006005610957565b60606102df7f00000000000000000000000000000000000000000000000000000000000000006006610957565b5f6102ad610713610584565b8360405161190160f01b8152600281019290925260228201526042902090565b5f805f8061074388888888610a00565b9250925092506107538282610ac8565b50909695505050505050565b6001600160a01b0384166107885760405163e602df0560e01b81525f600482015260240161037f565b6001600160a01b0383166107b157604051634a1406b160e11b81525f600482015260240161037f565b6001600160a01b038085165f908152600160209081526040808320938716835292905220829055801561052157826001600160a01b0316846001600160a01b03167f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b9258460405161082391815260200190565b60405180910390a350505050565b6001600160a01b03831661085b578060025f8282546108509190610e43565b909155506108cb9050565b6001600160a01b0383165f90815260208190526040902054818110156108ad5760405163391434e360e21b81526001600160a01b0385166004820152602481018290526044810183905260640161037f565b6001600160a01b0384165f9081526020819052604090209082900390555b6001600160a01b0382166108e757600280548290039055610905565b6001600160a01b0382165f9081526020819052604090208054820190555b816001600160a01b0316836001600160a01b03167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef8360405161094a91815260200190565b60405180910390a3505050565b606060ff83146109715761096a83610b84565b90506102ad565b81805461097d90610e0b565b80601f01602080910402602001604051908101604052809291908181526020018280546109a990610e0b565b80156109f45780601f106109cb576101008083540402835291602001916109f4565b820191905f5260205f20905b8154815290600101906020018083116109d757829003601f168201915b505050505090506102ad565b5f80807f7fffffffffffffffffffffffffffffff5d576e7357a4501ddfe92f46681b20a0841115610a3957505f91506003905082610abe565b604080515f808252602082018084528a905260ff891692820192909252606081018790526080810186905260019060a0016020604051602081039080840390855afa158015610a8a573d5f803e3d5ffd5b5050604051601f1901519150506001600160a01b038116610ab557505f925060019150829050610abe565b92505f91508190505b9450945094915050565b5f826003811115610adb57610adb610e62565b03610ae4575050565b6001826003811115610af857610af8610e62565b03610b165760405163f645eedf60e01b815260040160405180910390fd5b6002826003811115610b2a57610b2a610e62565b03610b4b5760405163fce698f760e01b81526004810182905260240161037f565b6003826003811115610b5f57610b5f610e62565b03610b80576040516335e2f38360e21b81526004810182905260240161037f565b5050565b60605f610b9083610bc1565b6040805160208082528183019092529192505f91906020820181803683375050509182525060208101929092525090565b5f60ff8216601f8111156102ad57604051632cd44ac360e21b815260040160405180910390fd5b5f81518084525f5b81811015610c0c57602081850181015186830182015201610bf0565b505f602082860101526020601f19601f83011685010191505092915050565b602081525f610c3d6020830184610be8565b9392505050565b80356001600160a01b0381168114610c5a575f80fd5b919050565b5f8060408385031215610c70575f80fd5b610c7983610c44565b946020939093013593505050565b5f805f60608486031215610c99575f80fd5b610ca284610c44565b9250610cb060208501610c44565b9150604084013590509250925092565b5f60208284031215610cd0575f80fd5b610c3d82610c44565b60ff60f81b881681525f602060e081840152610cf860e084018a610be8565b8381036040850152610d0a818a610be8565b606085018990526001600160a01b038816608086015260a0850187905284810360c086015285518082528387019250908301905f5b81811015610d5b57835183529284019291840191600101610d3f565b50909c9b505050505050505050505050565b5f805f805f805f60e0888a031215610d83575f80fd5b610d8c88610c44565b9650610d9a60208901610c44565b95506040880135945060608801359350608088013560ff81168114610dbd575f80fd5b9699959850939692959460a0840135945060c09093013592915050565b5f8060408385031215610deb575f80fd5b610df483610c44565b9150610e0260208401610c44565b90509250929050565b600181811c90821680610e1f57607f821691505b602082108103610e3d57634e487b7160e01b5f52602260045260245ffd5b50919050565b808201808211156102ad57634e487b7160e01b5f52601160045260245ffd5b634e487b7160e01b5f52602160045260245ffdfea264697066735822122051aca26ef66ca6e10c97416f36a2838503ce6ffa7da71431fa0920a035c40bce64736f6c63430008140033";
        public ExternalErc20DeploymentBase() : base(BYTECODE) { }
        public ExternalErc20DeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class DomainSeparatorFunction : DomainSeparatorFunctionBase { }

    [Function("DOMAIN_SEPARATOR", "bytes32")]
    public class DomainSeparatorFunctionBase : FunctionMessage
    {

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

    public partial class InvalidAccountNonceError : InvalidAccountNonceErrorBase { }

    [Error("InvalidAccountNonce")]
    public class InvalidAccountNonceErrorBase : IErrorDTO
    {
        [Parameter("address", "account", 1)]
        public virtual string Account { get; set; }
        [Parameter("uint256", "currentNonce", 2)]
        public virtual BigInteger CurrentNonce { get; set; }
    }

    public partial class InvalidShortStringError : InvalidShortStringErrorBase { }
    [Error("InvalidShortString")]
    public class InvalidShortStringErrorBase : IErrorDTO
    {
    }

    public partial class StringTooLongError : StringTooLongErrorBase { }

    [Error("StringTooLong")]
    public class StringTooLongErrorBase : IErrorDTO
    {
        [Parameter("string", "str", 1)]
        public virtual string Str { get; set; }
    }

    public partial class DomainSeparatorOutputDTO : DomainSeparatorOutputDTOBase { }

    [FunctionOutput]
    public class DomainSeparatorOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
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
