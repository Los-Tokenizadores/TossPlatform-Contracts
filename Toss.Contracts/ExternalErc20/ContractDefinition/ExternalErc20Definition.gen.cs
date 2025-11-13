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
using Toss.Contracts.ExternalErc20.ContractDefinition;

namespace Toss.Contracts.ExternalErc20.ContractDefinition
{


    public partial class ExternalErc20Deployment : ExternalErc20DeploymentBase
    {
        public ExternalErc20Deployment() : base(BYTECODE) { }
        public ExternalErc20Deployment(string byteCode) : base(byteCode) { }
    }

    public class ExternalErc20DeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "6101606040818152346200041557620000188262000419565b5f9182815281516200002a8162000419565b8381528251936200003b8562000419565b80855283516001600160401b0394909190828101868111848210176200040157815260019182845260209283850195603160f81b87528051898111620003ed576003908154928484811c94168015620003e2575b88851014620003ce578190601f948581116200037b575b508890858311600114620003195787926200030d575b50505f1982841b1c191690841b1781555b8a51918a8311620002f95760049b8c548581811c91168015620002ee575b89821014620002da5782811162000292575b50879184116001146200022c57938394918492879562000220575b50501b925f19911b1c19161788555b620001328662000449565b94610120958652620001448562000615565b96610140978852848151910120948560e0525190209761010098808a524660a0528351948501957f8b73c3c69bb8fe3d512ecc4cf759cc79239f7b179b0ffacaa9a75d522b39400f87528486015260608501524660808501523060a085015260a0845260c0840197848910908911176200020d5750508590525190206080523060c052610de59384620007c3853960805184610b0b015260a05184610bd7015260c05184610ad5015260e05184610b5a01525183610b800152518261039c015251816103c60152f35b634e487b7160e01b825260419052602490fd5b015193505f8062000118565b8c865287862092919084601f198116885b8b898383106200027a575050501062000260575b50505050811b01885562000127565b01519060f8845f19921b161c191690555f80808062000251565b8686015189559097019694850194889350016200023d565b8d87528887208380870160051c8201928b8810620002d0575b0160051c019086905b828110620002c4575050620000fd565b888155018690620002b4565b92508192620002ab565b50634e487b7160e01b865260228d52602486fd5b90607f1690620000eb565b634e487b7160e01b85526041600452602485fd5b015190505f80620000bc565b8488528988208794509190601f198416895b8c8282106200036457505084116200034c575b505050811b018155620000cd565b01515f1983861b60f8161c191690555f80806200033e565b8385015186558a979095019493840193016200032b565b9091508387528887208580850160051c8201928b8610620003c4575b918891869594930160051c01915b828110620003b5575050620000a6565b898155859450889101620003a5565b9250819262000397565b634e487b7160e01b86526022600452602486fd5b93607f16936200008f565b634e487b7160e01b84526041600452602484fd5b634e487b7160e01b83526041600452602483fd5b5f80fd5b602081019081106001600160401b038211176200043557604052565b634e487b7160e01b5f52604160045260245ffd5b80516020919082811015620004e5575090601f8251116200048657808251920151908083106200047857501790565b825f19910360031b1b161790565b90604051809263305a27a960e01b82528060048301528251908160248401525f935b828510620004cb575050604492505f838284010152601f80199101168101030190fd5b8481018201518686016044015293810193859350620004a8565b6001600160401b03811162000435576005928354926001938481811c911680156200060a575b83821014620005f657601f8111620005c2575b5081601f84116001146200055c57509282939183925f9462000550575b50501b915f199060031b1c191617905560ff90565b015192505f806200053b565b919083601f198116875f52845f20945f905b88838310620005a757505050106200058e575b505050811b01905560ff90565b01515f1960f88460031b161c191690555f808062000581565b8587015188559096019594850194879350908101906200056e565b855f5284601f845f209201871c820191601f8601881c015b828110620005ea5750506200051e565b5f8155018590620005da565b634e487b7160e01b5f52602260045260245ffd5b90607f16906200050b565b805160209081811015620006a15750601f8251116200064257808251920151908083106200047857501790565b90604051809263305a27a960e01b82528060048301528251908160248401525f935b82851062000687575050604492505f838284010152601f80199101168101030190fd5b848101820151868601604401529381019385935062000664565b906001600160401b0382116200043557600654926001938481811c91168015620007b7575b83821014620005f657601f811162000780575b5081601f84116001146200071857509282939183925f946200070c575b50501b915f199060031b1c19161760065560ff90565b015192505f80620006f6565b919083601f19811660065f52845f20945f905b888383106200076557505050106200074c575b505050811b0160065560ff90565b01515f1960f88460031b161c191690555f80806200073e565b8587015188559096019594850194879350908101906200072b565b60065f5284601f845f20920160051c820191601f860160051c015b828110620007ab575050620006d9565b5f81550185906200079b565b90607f1690620006c656fe6080604081815260049182361015610015575f80fd5b5f92833560e01c91826306fdde031461069057508163095ea7b31461066657816318160ddd1461064757816323b872dd14610551578163313ce567146105355781633644e5151461051157816370a08231146104da5781637ecebe00146104a257816384b0196e1461038457816395d89b4114610295578163a9059cbb14610264578163d505accf146100fc575063dd62ed3e146100b1575f80fd5b346100f857806003193601126100f857806020926100cd61079f565b6100d56107b9565b6001600160a01b0391821683526001865283832091168252845220549051908152f35b5080fd5b839150346100f85760e03660031901126100f85761011861079f565b6101206107b9565b906044359260643560843560ff81168103610260578142116102495760018060a01b0390818516928389526007602052898920908154916001830190558a519060208201927f6e71edae12b1b97f4d1f60370fef10105fa2faae0126114a169c64845d6126c98452868d840152858a1660608401528a608084015260a083015260c082015260c0815260e0810181811067ffffffffffffffff821117610236578b52519020610204916101fb916101d5610ad2565b908c519161190160f01b83526002830152602282015260c43591604260a43592206109c1565b90929192610a4f565b1681810361021b5786610218878787610934565b80f35b87516325c0072360e11b815292830152602482015260449150fd5b634e487b7160e01b8b526041875260248bfd5b875163313c898160e11b8152808401839052602490fd5b8680fd5b5050346100f857806003193601126100f85760209061028e61028461079f565b6024359033610859565b5160018152f35b91905034610380578260031936011261038057805191838154906102b8826107cf565b8086529260019280841690811561035557506001146102f9575b6102f586866102e3828b0383610837565b51918291602083526020830190610761565b0390f35b815294507f8a35acfbc15ff81a39ae7d344fd709f28e8600b4aa8c65c6b64bfe7fe36bd19b5b82861061033d575050506102e38260206102f595820101945f6102d2565b8054602087870181019190915290950194810161031f565b90506102f59750869350602092506102e394915060ff191682840152151560051b820101945f6102d2565b8280fd5b919050346103805782600319360112610380576103c07f0000000000000000000000000000000000000000000000000000000000000000610bfd565b926103ea7f0000000000000000000000000000000000000000000000000000000000000000610cf7565b908251926020928385019585871067ffffffffffffffff88111761048f57509280610445838896610438998b9996528686528151998a99600f60f81b8b5260e0868c015260e08b0190610761565b91898303908a0152610761565b924660608801523060808801528460a088015286840360c088015251928381520193925b82811061047857505050500390f35b835185528695509381019392810192600101610469565b634e487b7160e01b845260419052602483fd5b5050346100f85760203660031901126100f85760209181906001600160a01b036104ca61079f565b1681526007845220549051908152f35b5050346100f85760203660031901126100f85760209181906001600160a01b0361050261079f565b16815280845220549051908152f35b5050346100f857816003193601126100f85760209061052e610ad2565b9051908152f35b5050346100f857816003193601126100f8576020905160128152f35b905082346106445760603660031901126106445761056d61079f565b6105756107b9565b916044359360018060a01b038316808352600160205286832033845260205286832054915f1983036105b0575b60208861028e898989610859565b8683106106185781156106015733156105ea5750825260016020908152868320338452815291869020908590039055829061028e876105a2565b8751634a1406b160e11b8152908101849052602490fd5b875163e602df0560e01b8152908101849052602490fd5b8751637dc7a0d960e11b8152339181019182526020820193909352604081018790528291506060010390fd5b80fd5b5050346100f857816003193601126100f8576020906002549051908152f35b5050346100f857806003193601126100f85760209061028e61068661079f565b6024359033610934565b84908434610380578260031936011261038057826003546106b0816107cf565b8085529160019180831690811561073957506001146106dc575b5050506102e3826102f5940383610837565b9450600385527fc2575a0e9e593c00f959f8c92f12db2869c3395a3b0502d05e2516446f71f85b5b828610610721575050506102e38260206102f595820101946106ca565b80546020878701810191909152909501948101610704565b6102f59750869350602092506102e394915060ff191682840152151560051b820101946106ca565b91908251928382525f5b84811061078b575050825f602080949584010152601f8019910116010190565b60208183018101518483018201520161076b565b600435906001600160a01b03821682036107b557565b5f80fd5b602435906001600160a01b03821682036107b557565b90600182811c921680156107fd575b60208310146107e957565b634e487b7160e01b5f52602260045260245ffd5b91607f16916107de565b6040810190811067ffffffffffffffff82111761082357604052565b634e487b7160e01b5f52604160045260245ffd5b90601f8019910116810190811067ffffffffffffffff82111761082357604052565b916001600160a01b0380841692831561091c5716928315610904575f90838252816020526040822054908382106108d2575091604082827fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef958760209652828652038282205586815220818154019055604051908152a3565b60405163391434e360e21b81526001600160a01b03919091166004820152602481019190915260448101839052606490fd5b60405163ec442f0560e01b81525f6004820152602490fd5b604051634b637e8f60e11b81525f6004820152602490fd5b6001600160a01b039081169182156109a957169182156109915760207f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b92591835f526001825260405f20855f5282528060405f2055604051908152a3565b604051634a1406b160e11b81525f6004820152602490fd5b60405163e602df0560e01b81525f6004820152602490fd5b91907f7fffffffffffffffffffffffffffffff5d576e7357a4501ddfe92f46681b20a08411610a4457926020929160ff6080956040519485521684840152604083015260608201525f92839182805260015afa15610a385780516001600160a01b03811615610a2f57918190565b50809160019190565b604051903d90823e3d90fd5b5050505f9160039190565b6004811015610abe5780610a61575050565b60018103610a7b5760405163f645eedf60e01b8152600490fd5b60028103610a9c5760405163fce698f760e01b815260048101839052602490fd5b600314610aa65750565b602490604051906335e2f38360e21b82526004820152fd5b634e487b7160e01b5f52602160045260245ffd5b307f00000000000000000000000000000000000000000000000000000000000000006001600160a01b03161480610bd4575b15610b2d577f000000000000000000000000000000000000000000000000000000000000000090565b60405160208101907f8b73c3c69bb8fe3d512ecc4cf759cc79239f7b179b0ffacaa9a75d522b39400f82527f000000000000000000000000000000000000000000000000000000000000000060408201527f000000000000000000000000000000000000000000000000000000000000000060608201524660808201523060a082015260a0815260c0810181811067ffffffffffffffff8211176108235760405251902090565b507f00000000000000000000000000000000000000000000000000000000000000004614610b04565b60ff8114610c3b5760ff811690601f8211610c295760405191610c1f83610807565b8252602082015290565b604051632cd44ac360e21b8152600490fd5b50604051600554815f610c4d836107cf565b80835292600190818116908115610cd55750600114610c77575b50610c7492500382610837565b90565b60055f90815291507f036b6384b5eca791c62761152d0c79bb0604c104a5fb6f4eb0703f3154bb3db05b848310610cba5750610c7493505081016020015f610c67565b81935090816020925483858901015201910190918492610ca1565b905060209250610c7494915060ff191682840152151560051b8201015f610c67565b60ff8114610d195760ff811690601f8211610c295760405191610c1f83610807565b50604051600654815f610d2b836107cf565b80835292600190818116908115610cd55750600114610d515750610c7492500382610837565b60065f90815291507ff652222313e28459528d920b65115c16c04f3efc82aaedc97be59f3f377c0d3f5b848310610d945750610c7493505081016020015f610c67565b81935090816020925483858901015201910190918492610d7b56fea26469706673582212204b5974cde3bb5a32b9a50d071d3db0a2a6c4268f014a6fffe9bf2a3e91606b8b64736f6c63430008140033";
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

    public partial class EIP712DomainChangedEventDTO : EIP712DomainChangedEventDTOBase { }

    [Event("EIP712DomainChanged")]
    public class EIP712DomainChangedEventDTOBase : IEventDTO
    {
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
}
