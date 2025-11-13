using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Toss.Contracts.TossErc20V1.ContractDefinition;

namespace Toss.Contracts.TossErc20V1
{
    public partial class TossErc20V1Service: TossErc20V1ServiceBase
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.IWeb3 web3, TossErc20V1Deployment tossErc20V1Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<TossErc20V1Deployment>().SendRequestAndWaitForReceiptAsync(tossErc20V1Deployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.IWeb3 web3, TossErc20V1Deployment tossErc20V1Deployment)
        {
            return web3.Eth.GetContractDeploymentHandler<TossErc20V1Deployment>().SendRequestAsync(tossErc20V1Deployment);
        }

        public static async Task<TossErc20V1Service> DeployContractAndGetServiceAsync(Nethereum.Web3.IWeb3 web3, TossErc20V1Deployment tossErc20V1Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, tossErc20V1Deployment, cancellationTokenSource);
            return new TossErc20V1Service(web3, receipt.ContractAddress);
        }

        public TossErc20V1Service(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
        {
        }

    }


    public partial class TossErc20V1ServiceBase: ContractWeb3ServiceBase
    {

        public TossErc20V1ServiceBase(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
        {
        }

        public Task<byte[]> DefaultAdminRoleQueryAsync(DefaultAdminRoleFunction defaultAdminRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DefaultAdminRoleFunction, byte[]>(defaultAdminRoleFunction, blockParameter);
        }

        
        public virtual Task<byte[]> DefaultAdminRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DefaultAdminRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> DomainSeparatorQueryAsync(DomainSeparatorFunction domainSeparatorFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DomainSeparatorFunction, byte[]>(domainSeparatorFunction, blockParameter);
        }

        
        public virtual Task<byte[]> DomainSeparatorQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DomainSeparatorFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> MinterRoleQueryAsync(MinterRoleFunction minterRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MinterRoleFunction, byte[]>(minterRoleFunction, blockParameter);
        }

        
        public virtual Task<byte[]> MinterRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MinterRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> PauserRoleQueryAsync(PauserRoleFunction pauserRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PauserRoleFunction, byte[]>(pauserRoleFunction, blockParameter);
        }

        
        public virtual Task<byte[]> PauserRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PauserRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> UpgraderRoleQueryAsync(UpgraderRoleFunction upgraderRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UpgraderRoleFunction, byte[]>(upgraderRoleFunction, blockParameter);
        }

        
        public virtual Task<byte[]> UpgraderRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UpgraderRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<string> UpgradeInterfaceVersionQueryAsync(UpgradeInterfaceVersionFunction upgradeInterfaceVersionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UpgradeInterfaceVersionFunction, string>(upgradeInterfaceVersionFunction, blockParameter);
        }

        
        public virtual Task<string> UpgradeInterfaceVersionQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UpgradeInterfaceVersionFunction, string>(null, blockParameter);
        }

        public virtual Task<string> Tosserc20v1InitRequestAsync(Tosserc20v1InitFunction tosserc20v1InitFunction)
        {
             return ContractHandler.SendRequestAsync(tosserc20v1InitFunction);
        }

        public virtual Task<TransactionReceipt> Tosserc20v1InitRequestAndWaitForReceiptAsync(Tosserc20v1InitFunction tosserc20v1InitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tosserc20v1InitFunction, cancellationToken);
        }

        public virtual Task<string> Tosserc20v1InitRequestAsync(string name, string symbol, BigInteger amount)
        {
            var tosserc20v1InitFunction = new Tosserc20v1InitFunction();
                tosserc20v1InitFunction.Name = name;
                tosserc20v1InitFunction.Symbol = symbol;
                tosserc20v1InitFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(tosserc20v1InitFunction);
        }

        public virtual Task<TransactionReceipt> Tosserc20v1InitRequestAndWaitForReceiptAsync(string name, string symbol, BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var tosserc20v1InitFunction = new Tosserc20v1InitFunction();
                tosserc20v1InitFunction.Name = name;
                tosserc20v1InitFunction.Symbol = symbol;
                tosserc20v1InitFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tosserc20v1InitFunction, cancellationToken);
        }

        public Task<BigInteger> AllowanceQueryAsync(AllowanceFunction allowanceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AllowanceFunction, BigInteger>(allowanceFunction, blockParameter);
        }

        
        public virtual Task<BigInteger> AllowanceQueryAsync(string owner, string spender, BlockParameter blockParameter = null)
        {
            var allowanceFunction = new AllowanceFunction();
                allowanceFunction.Owner = owner;
                allowanceFunction.Spender = spender;
            
            return ContractHandler.QueryAsync<AllowanceFunction, BigInteger>(allowanceFunction, blockParameter);
        }

        public virtual Task<string> ApproveRequestAsync(ApproveFunction approveFunction)
        {
             return ContractHandler.SendRequestAsync(approveFunction);
        }

        public virtual Task<TransactionReceipt> ApproveRequestAndWaitForReceiptAsync(ApproveFunction approveFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(approveFunction, cancellationToken);
        }

        public virtual Task<string> ApproveRequestAsync(string spender, BigInteger value)
        {
            var approveFunction = new ApproveFunction();
                approveFunction.Spender = spender;
                approveFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(approveFunction);
        }

        public virtual Task<TransactionReceipt> ApproveRequestAndWaitForReceiptAsync(string spender, BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var approveFunction = new ApproveFunction();
                approveFunction.Spender = spender;
                approveFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(approveFunction, cancellationToken);
        }

        public Task<BigInteger> BalanceOfQueryAsync(BalanceOfFunction balanceOfFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction, blockParameter);
        }

        
        public virtual Task<BigInteger> BalanceOfQueryAsync(string account, BlockParameter blockParameter = null)
        {
            var balanceOfFunction = new BalanceOfFunction();
                balanceOfFunction.Account = account;
            
            return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction, blockParameter);
        }

        public virtual Task<string> BurnRequestAsync(BurnFunction burnFunction)
        {
             return ContractHandler.SendRequestAsync(burnFunction);
        }

        public virtual Task<TransactionReceipt> BurnRequestAndWaitForReceiptAsync(BurnFunction burnFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(burnFunction, cancellationToken);
        }

        public virtual Task<string> BurnRequestAsync(BigInteger value)
        {
            var burnFunction = new BurnFunction();
                burnFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(burnFunction);
        }

        public virtual Task<TransactionReceipt> BurnRequestAndWaitForReceiptAsync(BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var burnFunction = new BurnFunction();
                burnFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(burnFunction, cancellationToken);
        }

        public virtual Task<string> BurnFromRequestAsync(BurnFromFunction burnFromFunction)
        {
             return ContractHandler.SendRequestAsync(burnFromFunction);
        }

        public virtual Task<TransactionReceipt> BurnFromRequestAndWaitForReceiptAsync(BurnFromFunction burnFromFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(burnFromFunction, cancellationToken);
        }

        public virtual Task<string> BurnFromRequestAsync(string account, BigInteger value)
        {
            var burnFromFunction = new BurnFromFunction();
                burnFromFunction.Account = account;
                burnFromFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(burnFromFunction);
        }

        public virtual Task<TransactionReceipt> BurnFromRequestAndWaitForReceiptAsync(string account, BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var burnFromFunction = new BurnFromFunction();
                burnFromFunction.Account = account;
                burnFromFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(burnFromFunction, cancellationToken);
        }

        public Task<byte> DecimalsQueryAsync(DecimalsFunction decimalsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DecimalsFunction, byte>(decimalsFunction, blockParameter);
        }

        
        public virtual Task<byte> DecimalsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DecimalsFunction, byte>(null, blockParameter);
        }

        public virtual Task<Eip712DomainOutputDTO> Eip712DomainQueryAsync(Eip712DomainFunction eip712DomainFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<Eip712DomainFunction, Eip712DomainOutputDTO>(eip712DomainFunction, blockParameter);
        }

        public virtual Task<Eip712DomainOutputDTO> Eip712DomainQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<Eip712DomainFunction, Eip712DomainOutputDTO>(null, blockParameter);
        }

        public Task<string> GetImplementationQueryAsync(GetImplementationFunction getImplementationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetImplementationFunction, string>(getImplementationFunction, blockParameter);
        }

        
        public virtual Task<string> GetImplementationQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetImplementationFunction, string>(null, blockParameter);
        }

        public Task<byte[]> GetRoleAdminQueryAsync(GetRoleAdminFunction getRoleAdminFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetRoleAdminFunction, byte[]>(getRoleAdminFunction, blockParameter);
        }

        
        public virtual Task<byte[]> GetRoleAdminQueryAsync(byte[] role, BlockParameter blockParameter = null)
        {
            var getRoleAdminFunction = new GetRoleAdminFunction();
                getRoleAdminFunction.Role = role;
            
            return ContractHandler.QueryAsync<GetRoleAdminFunction, byte[]>(getRoleAdminFunction, blockParameter);
        }

        public Task<string> GetWhitelistQueryAsync(GetWhitelistFunction getWhitelistFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWhitelistFunction, string>(getWhitelistFunction, blockParameter);
        }

        
        public virtual Task<string> GetWhitelistQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWhitelistFunction, string>(null, blockParameter);
        }

        public virtual Task<string> GrantRoleRequestAsync(GrantRoleFunction grantRoleFunction)
        {
             return ContractHandler.SendRequestAsync(grantRoleFunction);
        }

        public virtual Task<TransactionReceipt> GrantRoleRequestAndWaitForReceiptAsync(GrantRoleFunction grantRoleFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(grantRoleFunction, cancellationToken);
        }

        public virtual Task<string> GrantRoleRequestAsync(byte[] role, string account)
        {
            var grantRoleFunction = new GrantRoleFunction();
                grantRoleFunction.Role = role;
                grantRoleFunction.Account = account;
            
             return ContractHandler.SendRequestAsync(grantRoleFunction);
        }

        public virtual Task<TransactionReceipt> GrantRoleRequestAndWaitForReceiptAsync(byte[] role, string account, CancellationTokenSource cancellationToken = null)
        {
            var grantRoleFunction = new GrantRoleFunction();
                grantRoleFunction.Role = role;
                grantRoleFunction.Account = account;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(grantRoleFunction, cancellationToken);
        }

        public Task<bool> HasRoleQueryAsync(HasRoleFunction hasRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<HasRoleFunction, bool>(hasRoleFunction, blockParameter);
        }

        
        public virtual Task<bool> HasRoleQueryAsync(byte[] role, string account, BlockParameter blockParameter = null)
        {
            var hasRoleFunction = new HasRoleFunction();
                hasRoleFunction.Role = role;
                hasRoleFunction.Account = account;
            
            return ContractHandler.QueryAsync<HasRoleFunction, bool>(hasRoleFunction, blockParameter);
        }

        public virtual Task<string> MintRequestAsync(MintFunction mintFunction)
        {
             return ContractHandler.SendRequestAsync(mintFunction);
        }

        public virtual Task<TransactionReceipt> MintRequestAndWaitForReceiptAsync(MintFunction mintFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(mintFunction, cancellationToken);
        }

        public virtual Task<string> MintRequestAsync(string to, BigInteger amount)
        {
            var mintFunction = new MintFunction();
                mintFunction.To = to;
                mintFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(mintFunction);
        }

        public virtual Task<TransactionReceipt> MintRequestAndWaitForReceiptAsync(string to, BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var mintFunction = new MintFunction();
                mintFunction.To = to;
                mintFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(mintFunction, cancellationToken);
        }

        public Task<string> NameQueryAsync(NameFunction nameFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NameFunction, string>(nameFunction, blockParameter);
        }

        
        public virtual Task<string> NameQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NameFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> NoncesQueryAsync(NoncesFunction noncesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<NoncesFunction, BigInteger>(noncesFunction, blockParameter);
        }

        
        public virtual Task<BigInteger> NoncesQueryAsync(string owner, BlockParameter blockParameter = null)
        {
            var noncesFunction = new NoncesFunction();
                noncesFunction.Owner = owner;
            
            return ContractHandler.QueryAsync<NoncesFunction, BigInteger>(noncesFunction, blockParameter);
        }

        public virtual Task<string> PauseRequestAsync(PauseFunction pauseFunction)
        {
             return ContractHandler.SendRequestAsync(pauseFunction);
        }

        public virtual Task<string> PauseRequestAsync()
        {
             return ContractHandler.SendRequestAsync<PauseFunction>();
        }

        public virtual Task<TransactionReceipt> PauseRequestAndWaitForReceiptAsync(PauseFunction pauseFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(pauseFunction, cancellationToken);
        }

        public virtual Task<TransactionReceipt> PauseRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<PauseFunction>(null, cancellationToken);
        }

        public Task<bool> PausedQueryAsync(PausedFunction pausedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PausedFunction, bool>(pausedFunction, blockParameter);
        }

        
        public virtual Task<bool> PausedQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PausedFunction, bool>(null, blockParameter);
        }

        public virtual Task<string> PermitRequestAsync(PermitFunction permitFunction)
        {
             return ContractHandler.SendRequestAsync(permitFunction);
        }

        public virtual Task<TransactionReceipt> PermitRequestAndWaitForReceiptAsync(PermitFunction permitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(permitFunction, cancellationToken);
        }

        public virtual Task<string> PermitRequestAsync(string owner, string spender, BigInteger value, BigInteger deadline, byte v, byte[] r, byte[] s)
        {
            var permitFunction = new PermitFunction();
                permitFunction.Owner = owner;
                permitFunction.Spender = spender;
                permitFunction.Value = value;
                permitFunction.Deadline = deadline;
                permitFunction.V = v;
                permitFunction.R = r;
                permitFunction.S = s;
            
             return ContractHandler.SendRequestAsync(permitFunction);
        }

        public virtual Task<TransactionReceipt> PermitRequestAndWaitForReceiptAsync(string owner, string spender, BigInteger value, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null)
        {
            var permitFunction = new PermitFunction();
                permitFunction.Owner = owner;
                permitFunction.Spender = spender;
                permitFunction.Value = value;
                permitFunction.Deadline = deadline;
                permitFunction.V = v;
                permitFunction.R = r;
                permitFunction.S = s;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(permitFunction, cancellationToken);
        }

        public Task<byte[]> ProxiableUUIDQueryAsync(ProxiableUUIDFunction proxiableUUIDFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProxiableUUIDFunction, byte[]>(proxiableUUIDFunction, blockParameter);
        }

        
        public virtual Task<byte[]> ProxiableUUIDQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProxiableUUIDFunction, byte[]>(null, blockParameter);
        }

        public virtual Task<string> RenounceRoleRequestAsync(RenounceRoleFunction renounceRoleFunction)
        {
             return ContractHandler.SendRequestAsync(renounceRoleFunction);
        }

        public virtual Task<TransactionReceipt> RenounceRoleRequestAndWaitForReceiptAsync(RenounceRoleFunction renounceRoleFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(renounceRoleFunction, cancellationToken);
        }

        public virtual Task<string> RenounceRoleRequestAsync(byte[] role, string callerConfirmation)
        {
            var renounceRoleFunction = new RenounceRoleFunction();
                renounceRoleFunction.Role = role;
                renounceRoleFunction.CallerConfirmation = callerConfirmation;
            
             return ContractHandler.SendRequestAsync(renounceRoleFunction);
        }

        public virtual Task<TransactionReceipt> RenounceRoleRequestAndWaitForReceiptAsync(byte[] role, string callerConfirmation, CancellationTokenSource cancellationToken = null)
        {
            var renounceRoleFunction = new RenounceRoleFunction();
                renounceRoleFunction.Role = role;
                renounceRoleFunction.CallerConfirmation = callerConfirmation;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(renounceRoleFunction, cancellationToken);
        }

        public virtual Task<string> RevokeRoleRequestAsync(RevokeRoleFunction revokeRoleFunction)
        {
             return ContractHandler.SendRequestAsync(revokeRoleFunction);
        }

        public virtual Task<TransactionReceipt> RevokeRoleRequestAndWaitForReceiptAsync(RevokeRoleFunction revokeRoleFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(revokeRoleFunction, cancellationToken);
        }

        public virtual Task<string> RevokeRoleRequestAsync(byte[] role, string account)
        {
            var revokeRoleFunction = new RevokeRoleFunction();
                revokeRoleFunction.Role = role;
                revokeRoleFunction.Account = account;
            
             return ContractHandler.SendRequestAsync(revokeRoleFunction);
        }

        public virtual Task<TransactionReceipt> RevokeRoleRequestAndWaitForReceiptAsync(byte[] role, string account, CancellationTokenSource cancellationToken = null)
        {
            var revokeRoleFunction = new RevokeRoleFunction();
                revokeRoleFunction.Role = role;
                revokeRoleFunction.Account = account;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(revokeRoleFunction, cancellationToken);
        }

        public virtual Task<string> SetWhitelistRequestAsync(SetWhitelistFunction setWhitelistFunction)
        {
             return ContractHandler.SendRequestAsync(setWhitelistFunction);
        }

        public virtual Task<TransactionReceipt> SetWhitelistRequestAndWaitForReceiptAsync(SetWhitelistFunction setWhitelistFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setWhitelistFunction, cancellationToken);
        }

        public virtual Task<string> SetWhitelistRequestAsync(string newAddress)
        {
            var setWhitelistFunction = new SetWhitelistFunction();
                setWhitelistFunction.NewAddress = newAddress;
            
             return ContractHandler.SendRequestAsync(setWhitelistFunction);
        }

        public virtual Task<TransactionReceipt> SetWhitelistRequestAndWaitForReceiptAsync(string newAddress, CancellationTokenSource cancellationToken = null)
        {
            var setWhitelistFunction = new SetWhitelistFunction();
                setWhitelistFunction.NewAddress = newAddress;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setWhitelistFunction, cancellationToken);
        }

        public Task<bool> SupportsInterfaceQueryAsync(SupportsInterfaceFunction supportsInterfaceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SupportsInterfaceFunction, bool>(supportsInterfaceFunction, blockParameter);
        }

        
        public virtual Task<bool> SupportsInterfaceQueryAsync(byte[] interfaceId, BlockParameter blockParameter = null)
        {
            var supportsInterfaceFunction = new SupportsInterfaceFunction();
                supportsInterfaceFunction.InterfaceId = interfaceId;
            
            return ContractHandler.QueryAsync<SupportsInterfaceFunction, bool>(supportsInterfaceFunction, blockParameter);
        }

        public Task<string> SymbolQueryAsync(SymbolFunction symbolFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SymbolFunction, string>(symbolFunction, blockParameter);
        }

        
        public virtual Task<string> SymbolQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SymbolFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> TotalSupplyQueryAsync(TotalSupplyFunction totalSupplyFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(totalSupplyFunction, blockParameter);
        }

        
        public virtual Task<BigInteger> TotalSupplyQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(null, blockParameter);
        }

        public virtual Task<string> TransferRequestAsync(TransferFunction transferFunction)
        {
             return ContractHandler.SendRequestAsync(transferFunction);
        }

        public virtual Task<TransactionReceipt> TransferRequestAndWaitForReceiptAsync(TransferFunction transferFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFunction, cancellationToken);
        }

        public virtual Task<string> TransferRequestAsync(string to, BigInteger value)
        {
            var transferFunction = new TransferFunction();
                transferFunction.To = to;
                transferFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(transferFunction);
        }

        public virtual Task<TransactionReceipt> TransferRequestAndWaitForReceiptAsync(string to, BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var transferFunction = new TransferFunction();
                transferFunction.To = to;
                transferFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFunction, cancellationToken);
        }

        public virtual Task<string> TransferFromRequestAsync(TransferFromFunction transferFromFunction)
        {
             return ContractHandler.SendRequestAsync(transferFromFunction);
        }

        public virtual Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(TransferFromFunction transferFromFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationToken);
        }

        public virtual Task<string> TransferFromRequestAsync(string from, string to, BigInteger value)
        {
            var transferFromFunction = new TransferFromFunction();
                transferFromFunction.From = from;
                transferFromFunction.To = to;
                transferFromFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(transferFromFunction);
        }

        public virtual Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(string from, string to, BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var transferFromFunction = new TransferFromFunction();
                transferFromFunction.From = from;
                transferFromFunction.To = to;
                transferFromFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationToken);
        }

        public virtual Task<string> UnpauseRequestAsync(UnpauseFunction unpauseFunction)
        {
             return ContractHandler.SendRequestAsync(unpauseFunction);
        }

        public virtual Task<string> UnpauseRequestAsync()
        {
             return ContractHandler.SendRequestAsync<UnpauseFunction>();
        }

        public virtual Task<TransactionReceipt> UnpauseRequestAndWaitForReceiptAsync(UnpauseFunction unpauseFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(unpauseFunction, cancellationToken);
        }

        public virtual Task<TransactionReceipt> UnpauseRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<UnpauseFunction>(null, cancellationToken);
        }

        public virtual Task<string> UpgradeToAndCallRequestAsync(UpgradeToAndCallFunction upgradeToAndCallFunction)
        {
             return ContractHandler.SendRequestAsync(upgradeToAndCallFunction);
        }

        public virtual Task<TransactionReceipt> UpgradeToAndCallRequestAndWaitForReceiptAsync(UpgradeToAndCallFunction upgradeToAndCallFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(upgradeToAndCallFunction, cancellationToken);
        }

        public virtual Task<string> UpgradeToAndCallRequestAsync(string newImplementation, byte[] data)
        {
            var upgradeToAndCallFunction = new UpgradeToAndCallFunction();
                upgradeToAndCallFunction.NewImplementation = newImplementation;
                upgradeToAndCallFunction.Data = data;
            
             return ContractHandler.SendRequestAsync(upgradeToAndCallFunction);
        }

        public virtual Task<TransactionReceipt> UpgradeToAndCallRequestAndWaitForReceiptAsync(string newImplementation, byte[] data, CancellationTokenSource cancellationToken = null)
        {
            var upgradeToAndCallFunction = new UpgradeToAndCallFunction();
                upgradeToAndCallFunction.NewImplementation = newImplementation;
                upgradeToAndCallFunction.Data = data;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(upgradeToAndCallFunction, cancellationToken);
        }

        public override List<Type> GetAllFunctionTypes()
        {
            return new List<Type>
            {
                typeof(DefaultAdminRoleFunction),
                typeof(DomainSeparatorFunction),
                typeof(MinterRoleFunction),
                typeof(PauserRoleFunction),
                typeof(UpgraderRoleFunction),
                typeof(UpgradeInterfaceVersionFunction),
                typeof(Tosserc20v1InitFunction),
                typeof(AllowanceFunction),
                typeof(ApproveFunction),
                typeof(BalanceOfFunction),
                typeof(BurnFunction),
                typeof(BurnFromFunction),
                typeof(DecimalsFunction),
                typeof(Eip712DomainFunction),
                typeof(GetImplementationFunction),
                typeof(GetRoleAdminFunction),
                typeof(GetWhitelistFunction),
                typeof(GrantRoleFunction),
                typeof(HasRoleFunction),
                typeof(MintFunction),
                typeof(NameFunction),
                typeof(NoncesFunction),
                typeof(PauseFunction),
                typeof(PausedFunction),
                typeof(PermitFunction),
                typeof(ProxiableUUIDFunction),
                typeof(RenounceRoleFunction),
                typeof(RevokeRoleFunction),
                typeof(SetWhitelistFunction),
                typeof(SupportsInterfaceFunction),
                typeof(SymbolFunction),
                typeof(TotalSupplyFunction),
                typeof(TransferFunction),
                typeof(TransferFromFunction),
                typeof(UnpauseFunction),
                typeof(UpgradeToAndCallFunction)
            };
        }

        public override List<Type> GetAllEventTypes()
        {
            return new List<Type>
            {
                typeof(ApprovalEventDTO),
                typeof(EIP712DomainChangedEventDTO),
                typeof(InitializedEventDTO),
                typeof(PausedEventDTO),
                typeof(RoleAdminChangedEventDTO),
                typeof(RoleGrantedEventDTO),
                typeof(RoleRevokedEventDTO),
                typeof(TransferEventDTO),
                typeof(UnpausedEventDTO),
                typeof(UpgradedEventDTO)
            };
        }

        public override List<Type> GetAllErrorTypes()
        {
            return new List<Type>
            {
                typeof(AccessControlBadConfirmationError),
                typeof(AccessControlUnauthorizedAccountError),
                typeof(AddressEmptyCodeError),
                typeof(ECDSAInvalidSignatureError),
                typeof(ECDSAInvalidSignatureLengthError),
                typeof(ECDSAInvalidSignatureSError),
                typeof(ERC1967InvalidImplementationError),
                typeof(ERC1967NonPayableError),
                typeof(ERC20InsufficientAllowanceError),
                typeof(ERC20InsufficientBalanceError),
                typeof(ERC20InvalidApproverError),
                typeof(ERC20InvalidReceiverError),
                typeof(ERC20InvalidSenderError),
                typeof(ERC20InvalidSpenderError),
                typeof(ERC2612ExpiredSignatureError),
                typeof(ERC2612InvalidSignerError),
                typeof(EnforcedPauseError),
                typeof(ExpectedPauseError),
                typeof(FailedInnerCallError),
                typeof(InvalidAccountNonceError),
                typeof(InvalidInitializationError),
                typeof(NotInitializingError),
                typeof(TossWhitelistNotInWhitelistError),
                typeof(UUPSUnauthorizedCallContextError),
                typeof(UUPSUnsupportedProxiableUUIDError)
            };
        }
    }
}
