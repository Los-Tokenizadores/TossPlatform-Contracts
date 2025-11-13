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
using Toss.Contracts.TossInvestV1.ContractDefinition;

namespace Toss.Contracts.TossInvestV1
{
    public partial class TossInvestV1Service: TossInvestV1ServiceBase
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.IWeb3 web3, TossInvestV1Deployment tossInvestV1Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<TossInvestV1Deployment>().SendRequestAndWaitForReceiptAsync(tossInvestV1Deployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.IWeb3 web3, TossInvestV1Deployment tossInvestV1Deployment)
        {
            return web3.Eth.GetContractDeploymentHandler<TossInvestV1Deployment>().SendRequestAsync(tossInvestV1Deployment);
        }

        public static async Task<TossInvestV1Service> DeployContractAndGetServiceAsync(Nethereum.Web3.IWeb3 web3, TossInvestV1Deployment tossInvestV1Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, tossInvestV1Deployment, cancellationTokenSource);
            return new TossInvestV1Service(web3, receipt.ContractAddress);
        }

        public TossInvestV1Service(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
        {
        }

    }


    public partial class TossInvestV1ServiceBase: ContractWeb3ServiceBase
    {

        public TossInvestV1ServiceBase(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
        {
        }

        public Task<ushort> CutPrecisionQueryAsync(CutPrecisionFunction cutPrecisionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CutPrecisionFunction, ushort>(cutPrecisionFunction, blockParameter);
        }

        
        public virtual Task<ushort> CutPrecisionQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CutPrecisionFunction, ushort>(null, blockParameter);
        }

        public Task<byte[]> DefaultAdminRoleQueryAsync(DefaultAdminRoleFunction defaultAdminRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DefaultAdminRoleFunction, byte[]>(defaultAdminRoleFunction, blockParameter);
        }

        
        public virtual Task<byte[]> DefaultAdminRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DefaultAdminRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> PauserRoleQueryAsync(PauserRoleFunction pauserRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PauserRoleFunction, byte[]>(pauserRoleFunction, blockParameter);
        }

        
        public virtual Task<byte[]> PauserRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PauserRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> ProjectRoleQueryAsync(ProjectRoleFunction projectRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProjectRoleFunction, byte[]>(projectRoleFunction, blockParameter);
        }

        
        public virtual Task<byte[]> ProjectRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProjectRoleFunction, byte[]>(null, blockParameter);
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

        public virtual Task<string> Tossinvestv1InitRequestAsync(Tossinvestv1InitFunction tossinvestv1InitFunction)
        {
             return ContractHandler.SendRequestAsync(tossinvestv1InitFunction);
        }

        public virtual Task<TransactionReceipt> Tossinvestv1InitRequestAndWaitForReceiptAsync(Tossinvestv1InitFunction tossinvestv1InitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tossinvestv1InitFunction, cancellationToken);
        }

        public virtual Task<string> Tossinvestv1InitRequestAsync(string erc20, string erc721implementation, string platformAddress, string erc721baseUri)
        {
            var tossinvestv1InitFunction = new Tossinvestv1InitFunction();
                tossinvestv1InitFunction.Erc20 = erc20;
                tossinvestv1InitFunction.Erc721implementation = erc721implementation;
                tossinvestv1InitFunction.PlatformAddress = platformAddress;
                tossinvestv1InitFunction.Erc721baseUri = erc721baseUri;
            
             return ContractHandler.SendRequestAsync(tossinvestv1InitFunction);
        }

        public virtual Task<TransactionReceipt> Tossinvestv1InitRequestAndWaitForReceiptAsync(string erc20, string erc721implementation, string platformAddress, string erc721baseUri, CancellationTokenSource cancellationToken = null)
        {
            var tossinvestv1InitFunction = new Tossinvestv1InitFunction();
                tossinvestv1InitFunction.Erc20 = erc20;
                tossinvestv1InitFunction.Erc721implementation = erc721implementation;
                tossinvestv1InitFunction.PlatformAddress = platformAddress;
                tossinvestv1InitFunction.Erc721baseUri = erc721baseUri;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tossinvestv1InitFunction, cancellationToken);
        }

        public virtual Task<string> AddProjectRequestAsync(AddProjectFunction addProjectFunction)
        {
             return ContractHandler.SendRequestAsync(addProjectFunction);
        }

        public virtual Task<TransactionReceipt> AddProjectRequestAndWaitForReceiptAsync(AddProjectFunction addProjectFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addProjectFunction, cancellationToken);
        }

        public virtual Task<string> AddProjectRequestAsync(string name, string symbol, uint targetAmount, uint maxAmount, BigInteger price, ulong startAt, ulong finishAt, string projectWallet, ushort platformCut)
        {
            var addProjectFunction = new AddProjectFunction();
                addProjectFunction.Name = name;
                addProjectFunction.Symbol = symbol;
                addProjectFunction.TargetAmount = targetAmount;
                addProjectFunction.MaxAmount = maxAmount;
                addProjectFunction.Price = price;
                addProjectFunction.StartAt = startAt;
                addProjectFunction.FinishAt = finishAt;
                addProjectFunction.ProjectWallet = projectWallet;
                addProjectFunction.PlatformCut = platformCut;
            
             return ContractHandler.SendRequestAsync(addProjectFunction);
        }

        public virtual Task<TransactionReceipt> AddProjectRequestAndWaitForReceiptAsync(string name, string symbol, uint targetAmount, uint maxAmount, BigInteger price, ulong startAt, ulong finishAt, string projectWallet, ushort platformCut, CancellationTokenSource cancellationToken = null)
        {
            var addProjectFunction = new AddProjectFunction();
                addProjectFunction.Name = name;
                addProjectFunction.Symbol = symbol;
                addProjectFunction.TargetAmount = targetAmount;
                addProjectFunction.MaxAmount = maxAmount;
                addProjectFunction.Price = price;
                addProjectFunction.StartAt = startAt;
                addProjectFunction.FinishAt = finishAt;
                addProjectFunction.ProjectWallet = projectWallet;
                addProjectFunction.PlatformCut = platformCut;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addProjectFunction, cancellationToken);
        }

        public virtual Task<string> ChangeProjectRequestAsync(ChangeProjectFunction changeProjectFunction)
        {
             return ContractHandler.SendRequestAsync(changeProjectFunction);
        }

        public virtual Task<TransactionReceipt> ChangeProjectRequestAndWaitForReceiptAsync(ChangeProjectFunction changeProjectFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(changeProjectFunction, cancellationToken);
        }

        public virtual Task<string> ChangeProjectRequestAsync(BigInteger projectId, string name, string symbol, uint targetAmount, uint maxAmount, BigInteger price, ulong startAt, ulong finishAt, string projectWallet, ushort platformCut)
        {
            var changeProjectFunction = new ChangeProjectFunction();
                changeProjectFunction.ProjectId = projectId;
                changeProjectFunction.Name = name;
                changeProjectFunction.Symbol = symbol;
                changeProjectFunction.TargetAmount = targetAmount;
                changeProjectFunction.MaxAmount = maxAmount;
                changeProjectFunction.Price = price;
                changeProjectFunction.StartAt = startAt;
                changeProjectFunction.FinishAt = finishAt;
                changeProjectFunction.ProjectWallet = projectWallet;
                changeProjectFunction.PlatformCut = platformCut;
            
             return ContractHandler.SendRequestAsync(changeProjectFunction);
        }

        public virtual Task<TransactionReceipt> ChangeProjectRequestAndWaitForReceiptAsync(BigInteger projectId, string name, string symbol, uint targetAmount, uint maxAmount, BigInteger price, ulong startAt, ulong finishAt, string projectWallet, ushort platformCut, CancellationTokenSource cancellationToken = null)
        {
            var changeProjectFunction = new ChangeProjectFunction();
                changeProjectFunction.ProjectId = projectId;
                changeProjectFunction.Name = name;
                changeProjectFunction.Symbol = symbol;
                changeProjectFunction.TargetAmount = targetAmount;
                changeProjectFunction.MaxAmount = maxAmount;
                changeProjectFunction.Price = price;
                changeProjectFunction.StartAt = startAt;
                changeProjectFunction.FinishAt = finishAt;
                changeProjectFunction.ProjectWallet = projectWallet;
                changeProjectFunction.PlatformCut = platformCut;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(changeProjectFunction, cancellationToken);
        }

        public virtual Task<string> ConfirmRequestAsync(ConfirmFunction confirmFunction)
        {
             return ContractHandler.SendRequestAsync(confirmFunction);
        }

        public virtual Task<TransactionReceipt> ConfirmRequestAndWaitForReceiptAsync(ConfirmFunction confirmFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(confirmFunction, cancellationToken);
        }

        public virtual Task<string> ConfirmRequestAsync(BigInteger projectId)
        {
            var confirmFunction = new ConfirmFunction();
                confirmFunction.ProjectId = projectId;
            
             return ContractHandler.SendRequestAsync(confirmFunction);
        }

        public virtual Task<TransactionReceipt> ConfirmRequestAndWaitForReceiptAsync(BigInteger projectId, CancellationTokenSource cancellationToken = null)
        {
            var confirmFunction = new ConfirmFunction();
                confirmFunction.ProjectId = projectId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(confirmFunction, cancellationToken);
        }

        public virtual Task<string> FinishRequestAsync(FinishFunction finishFunction)
        {
             return ContractHandler.SendRequestAsync(finishFunction);
        }

        public virtual Task<TransactionReceipt> FinishRequestAndWaitForReceiptAsync(FinishFunction finishFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(finishFunction, cancellationToken);
        }

        public virtual Task<string> FinishRequestAsync(BigInteger projectId)
        {
            var finishFunction = new FinishFunction();
                finishFunction.ProjectId = projectId;
            
             return ContractHandler.SendRequestAsync(finishFunction);
        }

        public virtual Task<TransactionReceipt> FinishRequestAndWaitForReceiptAsync(BigInteger projectId, CancellationTokenSource cancellationToken = null)
        {
            var finishFunction = new FinishFunction();
                finishFunction.ProjectId = projectId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(finishFunction, cancellationToken);
        }

        public Task<string> GetErc20QueryAsync(GetErc20Function getErc20Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc20Function, string>(getErc20Function, blockParameter);
        }

        
        public virtual Task<string> GetErc20QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc20Function, string>(null, blockParameter);
        }

        public Task<string> GetErc721BaseUriQueryAsync(GetErc721BaseUriFunction getErc721BaseUriFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc721BaseUriFunction, string>(getErc721BaseUriFunction, blockParameter);
        }

        
        public virtual Task<string> GetErc721BaseUriQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc721BaseUriFunction, string>(null, blockParameter);
        }

        public Task<string> GetErc721ImplementationQueryAsync(GetErc721ImplementationFunction getErc721ImplementationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc721ImplementationFunction, string>(getErc721ImplementationFunction, blockParameter);
        }

        
        public virtual Task<string> GetErc721ImplementationQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc721ImplementationFunction, string>(null, blockParameter);
        }

        public Task<string> GetImplementationQueryAsync(GetImplementationFunction getImplementationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetImplementationFunction, string>(getImplementationFunction, blockParameter);
        }

        
        public virtual Task<string> GetImplementationQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetImplementationFunction, string>(null, blockParameter);
        }

        public virtual Task<GetProjectOutputDTO> GetProjectQueryAsync(GetProjectFunction getProjectFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetProjectFunction, GetProjectOutputDTO>(getProjectFunction, blockParameter);
        }

        public virtual Task<GetProjectOutputDTO> GetProjectQueryAsync(BigInteger projectId, BlockParameter blockParameter = null)
        {
            var getProjectFunction = new GetProjectFunction();
                getProjectFunction.ProjectId = projectId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetProjectFunction, GetProjectOutputDTO>(getProjectFunction, blockParameter);
        }

        public virtual Task<GetProjectByErc721AddressOutputDTO> GetProjectByErc721AddressQueryAsync(GetProjectByErc721AddressFunction getProjectByErc721AddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetProjectByErc721AddressFunction, GetProjectByErc721AddressOutputDTO>(getProjectByErc721AddressFunction, blockParameter);
        }

        public virtual Task<GetProjectByErc721AddressOutputDTO> GetProjectByErc721AddressQueryAsync(string erc721Address, BlockParameter blockParameter = null)
        {
            var getProjectByErc721AddressFunction = new GetProjectByErc721AddressFunction();
                getProjectByErc721AddressFunction.Erc721Address = erc721Address;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetProjectByErc721AddressFunction, GetProjectByErc721AddressOutputDTO>(getProjectByErc721AddressFunction, blockParameter);
        }

        public Task<string> GetProjectInvestorQueryAsync(GetProjectInvestorFunction getProjectInvestorFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetProjectInvestorFunction, string>(getProjectInvestorFunction, blockParameter);
        }

        
        public virtual Task<string> GetProjectInvestorQueryAsync(BigInteger projectId, BigInteger index, BlockParameter blockParameter = null)
        {
            var getProjectInvestorFunction = new GetProjectInvestorFunction();
                getProjectInvestorFunction.ProjectId = projectId;
                getProjectInvestorFunction.Index = index;
            
            return ContractHandler.QueryAsync<GetProjectInvestorFunction, string>(getProjectInvestorFunction, blockParameter);
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

        public virtual Task<string> InvestRequestAsync(InvestFunction investFunction)
        {
             return ContractHandler.SendRequestAsync(investFunction);
        }

        public virtual Task<TransactionReceipt> InvestRequestAndWaitForReceiptAsync(InvestFunction investFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(investFunction, cancellationToken);
        }

        public virtual Task<string> InvestRequestAsync(BigInteger projectId, ushort amount)
        {
            var investFunction = new InvestFunction();
                investFunction.ProjectId = projectId;
                investFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(investFunction);
        }

        public virtual Task<TransactionReceipt> InvestRequestAndWaitForReceiptAsync(BigInteger projectId, ushort amount, CancellationTokenSource cancellationToken = null)
        {
            var investFunction = new InvestFunction();
                investFunction.ProjectId = projectId;
                investFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(investFunction, cancellationToken);
        }

        public virtual Task<string> InvestWithPermitRequestAsync(InvestWithPermitFunction investWithPermitFunction)
        {
             return ContractHandler.SendRequestAsync(investWithPermitFunction);
        }

        public virtual Task<TransactionReceipt> InvestWithPermitRequestAndWaitForReceiptAsync(InvestWithPermitFunction investWithPermitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(investWithPermitFunction, cancellationToken);
        }

        public virtual Task<string> InvestWithPermitRequestAsync(BigInteger projectId, ushort investAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s)
        {
            var investWithPermitFunction = new InvestWithPermitFunction();
                investWithPermitFunction.ProjectId = projectId;
                investWithPermitFunction.InvestAmount = investAmount;
                investWithPermitFunction.Amount = amount;
                investWithPermitFunction.Deadline = deadline;
                investWithPermitFunction.V = v;
                investWithPermitFunction.R = r;
                investWithPermitFunction.S = s;
            
             return ContractHandler.SendRequestAsync(investWithPermitFunction);
        }

        public virtual Task<TransactionReceipt> InvestWithPermitRequestAndWaitForReceiptAsync(BigInteger projectId, ushort investAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null)
        {
            var investWithPermitFunction = new InvestWithPermitFunction();
                investWithPermitFunction.ProjectId = projectId;
                investWithPermitFunction.InvestAmount = investAmount;
                investWithPermitFunction.Amount = amount;
                investWithPermitFunction.Deadline = deadline;
                investWithPermitFunction.V = v;
                investWithPermitFunction.R = r;
                investWithPermitFunction.S = s;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(investWithPermitFunction, cancellationToken);
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

        public Task<BigInteger> ProjectAmountQueryAsync(ProjectAmountFunction projectAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProjectAmountFunction, BigInteger>(projectAmountFunction, blockParameter);
        }

        
        public virtual Task<BigInteger> ProjectAmountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProjectAmountFunction, BigInteger>(null, blockParameter);
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

        public virtual Task<string> SetErc721BaseUriRequestAsync(SetErc721BaseUriFunction setErc721BaseUriFunction)
        {
             return ContractHandler.SendRequestAsync(setErc721BaseUriFunction);
        }

        public virtual Task<TransactionReceipt> SetErc721BaseUriRequestAndWaitForReceiptAsync(SetErc721BaseUriFunction setErc721BaseUriFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setErc721BaseUriFunction, cancellationToken);
        }

        public virtual Task<string> SetErc721BaseUriRequestAsync(string erc721baseuri)
        {
            var setErc721BaseUriFunction = new SetErc721BaseUriFunction();
                setErc721BaseUriFunction.Erc721baseuri = erc721baseuri;
            
             return ContractHandler.SendRequestAsync(setErc721BaseUriFunction);
        }

        public virtual Task<TransactionReceipt> SetErc721BaseUriRequestAndWaitForReceiptAsync(string erc721baseuri, CancellationTokenSource cancellationToken = null)
        {
            var setErc721BaseUriFunction = new SetErc721BaseUriFunction();
                setErc721BaseUriFunction.Erc721baseuri = erc721baseuri;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setErc721BaseUriFunction, cancellationToken);
        }

        public virtual Task<string> SetErc721ImplementationRequestAsync(SetErc721ImplementationFunction setErc721ImplementationFunction)
        {
             return ContractHandler.SendRequestAsync(setErc721ImplementationFunction);
        }

        public virtual Task<TransactionReceipt> SetErc721ImplementationRequestAndWaitForReceiptAsync(SetErc721ImplementationFunction setErc721ImplementationFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setErc721ImplementationFunction, cancellationToken);
        }

        public virtual Task<string> SetErc721ImplementationRequestAsync(string newImplementation)
        {
            var setErc721ImplementationFunction = new SetErc721ImplementationFunction();
                setErc721ImplementationFunction.NewImplementation = newImplementation;
            
             return ContractHandler.SendRequestAsync(setErc721ImplementationFunction);
        }

        public virtual Task<TransactionReceipt> SetErc721ImplementationRequestAndWaitForReceiptAsync(string newImplementation, CancellationTokenSource cancellationToken = null)
        {
            var setErc721ImplementationFunction = new SetErc721ImplementationFunction();
                setErc721ImplementationFunction.NewImplementation = newImplementation;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setErc721ImplementationFunction, cancellationToken);
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
                typeof(CutPrecisionFunction),
                typeof(DefaultAdminRoleFunction),
                typeof(PauserRoleFunction),
                typeof(ProjectRoleFunction),
                typeof(UpgraderRoleFunction),
                typeof(UpgradeInterfaceVersionFunction),
                typeof(Tossinvestv1InitFunction),
                typeof(AddProjectFunction),
                typeof(ChangeProjectFunction),
                typeof(ConfirmFunction),
                typeof(FinishFunction),
                typeof(GetErc20Function),
                typeof(GetErc721BaseUriFunction),
                typeof(GetErc721ImplementationFunction),
                typeof(GetImplementationFunction),
                typeof(GetProjectFunction),
                typeof(GetProjectByErc721AddressFunction),
                typeof(GetProjectInvestorFunction),
                typeof(GetRoleAdminFunction),
                typeof(GetWhitelistFunction),
                typeof(GrantRoleFunction),
                typeof(HasRoleFunction),
                typeof(InvestFunction),
                typeof(InvestWithPermitFunction),
                typeof(PauseFunction),
                typeof(PausedFunction),
                typeof(ProjectAmountFunction),
                typeof(ProxiableUUIDFunction),
                typeof(RenounceRoleFunction),
                typeof(RevokeRoleFunction),
                typeof(SetErc721BaseUriFunction),
                typeof(SetErc721ImplementationFunction),
                typeof(SetWhitelistFunction),
                typeof(SupportsInterfaceFunction),
                typeof(UnpauseFunction),
                typeof(UpgradeToAndCallFunction)
            };
        }

        public override List<Type> GetAllEventTypes()
        {
            return new List<Type>
            {
                typeof(InitializedEventDTO),
                typeof(PausedEventDTO),
                typeof(ProjectAddedEventDTO),
                typeof(ProjectConfirmedEventDTO),
                typeof(ProjectErc721CreatedEventDTO),
                typeof(ProjectFinishedEventDTO),
                typeof(ProjectInvestedEventDTO),
                typeof(RoleAdminChangedEventDTO),
                typeof(RoleGrantedEventDTO),
                typeof(RoleRevokedEventDTO),
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
                typeof(AddressInsufficientBalanceError),
                typeof(ERC1967InvalidImplementationError),
                typeof(ERC1967NonPayableError),
                typeof(EnforcedPauseError),
                typeof(ExpectedPauseError),
                typeof(FailedInnerCallError),
                typeof(InvalidInitializationError),
                typeof(NotInitializingError),
                typeof(ReentrancyGuardReentrantCallError),
                typeof(SafeERC20FailedOperationError),
                typeof(TossAddressIsZeroError),
                typeof(TossCutOutOfRangeError),
                typeof(TossInvestInvalidErc721ImplementationError),
                typeof(TossInvestNotProjectOwnerError),
                typeof(TossInvestProcessFinishedError),
                typeof(TossInvestProjectAlreadyFinishedError),
                typeof(TossInvestProjectFullInvestedError),
                typeof(TossInvestProjectIsConfirmedError),
                typeof(TossInvestProjectIsFinishedError),
                typeof(TossInvestProjectIsNotConfirmedError),
                typeof(TossInvestProjectNotExistError),
                typeof(TossInvestProjectNotFinishedError),
                typeof(TossInvestProjectNotFoundByErc721Error),
                typeof(TossInvestProjectNotStartedError),
                typeof(TossInvestProjectStartAtGreaterThanFinishAtError),
                typeof(TossInvestProjectStartAtLessThanCurrentDateError),
                typeof(TossInvestProjectTargetIsGreaterThanMaxError),
                typeof(TossValueIsZeroError),
                typeof(TossWhitelistNotInWhitelistError),
                typeof(UUPSUnauthorizedCallContextError),
                typeof(UUPSUnsupportedProxiableUUIDError)
            };
        }
    }
}
