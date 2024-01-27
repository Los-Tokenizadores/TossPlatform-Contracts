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
    public partial class TossInvestV1Service
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

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public TossInvestV1Service(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public TossInvestV1Service(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<ushort> CutPrecisionQueryAsync(CutPrecisionFunction cutPrecisionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CutPrecisionFunction, ushort>(cutPrecisionFunction, blockParameter);
        }

        
        public Task<ushort> CutPrecisionQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CutPrecisionFunction, ushort>(null, blockParameter);
        }

        public Task<byte[]> DefaultAdminRoleQueryAsync(DefaultAdminRoleFunction defaultAdminRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DefaultAdminRoleFunction, byte[]>(defaultAdminRoleFunction, blockParameter);
        }

        
        public Task<byte[]> DefaultAdminRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DefaultAdminRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> PauserRoleQueryAsync(PauserRoleFunction pauserRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PauserRoleFunction, byte[]>(pauserRoleFunction, blockParameter);
        }

        
        public Task<byte[]> PauserRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PauserRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> ProjectRoleQueryAsync(ProjectRoleFunction projectRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProjectRoleFunction, byte[]>(projectRoleFunction, blockParameter);
        }

        
        public Task<byte[]> ProjectRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProjectRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> UpgraderRoleQueryAsync(UpgraderRoleFunction upgraderRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UpgraderRoleFunction, byte[]>(upgraderRoleFunction, blockParameter);
        }

        
        public Task<byte[]> UpgraderRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UpgraderRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<string> UpgradeInterfaceVersionQueryAsync(UpgradeInterfaceVersionFunction upgradeInterfaceVersionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UpgradeInterfaceVersionFunction, string>(upgradeInterfaceVersionFunction, blockParameter);
        }

        
        public Task<string> UpgradeInterfaceVersionQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<UpgradeInterfaceVersionFunction, string>(null, blockParameter);
        }

        public Task<string> Tossinvestv1InitRequestAsync(Tossinvestv1InitFunction tossinvestv1InitFunction)
        {
             return ContractHandler.SendRequestAsync(tossinvestv1InitFunction);
        }

        public Task<TransactionReceipt> Tossinvestv1InitRequestAndWaitForReceiptAsync(Tossinvestv1InitFunction tossinvestv1InitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tossinvestv1InitFunction, cancellationToken);
        }

        public Task<string> Tossinvestv1InitRequestAsync(string erc20, string erc721implementation, string platformAddress, ushort platformCut, string erc721baseUri)
        {
            var tossinvestv1InitFunction = new Tossinvestv1InitFunction();
                tossinvestv1InitFunction.Erc20 = erc20;
                tossinvestv1InitFunction.Erc721implementation = erc721implementation;
                tossinvestv1InitFunction.PlatformAddress = platformAddress;
                tossinvestv1InitFunction.PlatformCut = platformCut;
                tossinvestv1InitFunction.Erc721baseUri = erc721baseUri;
            
             return ContractHandler.SendRequestAsync(tossinvestv1InitFunction);
        }

        public Task<TransactionReceipt> Tossinvestv1InitRequestAndWaitForReceiptAsync(string erc20, string erc721implementation, string platformAddress, ushort platformCut, string erc721baseUri, CancellationTokenSource cancellationToken = null)
        {
            var tossinvestv1InitFunction = new Tossinvestv1InitFunction();
                tossinvestv1InitFunction.Erc20 = erc20;
                tossinvestv1InitFunction.Erc721implementation = erc721implementation;
                tossinvestv1InitFunction.PlatformAddress = platformAddress;
                tossinvestv1InitFunction.PlatformCut = platformCut;
                tossinvestv1InitFunction.Erc721baseUri = erc721baseUri;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tossinvestv1InitFunction, cancellationToken);
        }

        public Task<string> AddProjectRequestAsync(AddProjectFunction addProjectFunction)
        {
             return ContractHandler.SendRequestAsync(addProjectFunction);
        }

        public Task<TransactionReceipt> AddProjectRequestAndWaitForReceiptAsync(AddProjectFunction addProjectFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addProjectFunction, cancellationToken);
        }

        public Task<string> AddProjectRequestAsync(string name, string symbol, uint targetAmount, uint maxAmount, BigInteger price, ulong startAt, ulong finishAt, string projectWallet)
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
            
             return ContractHandler.SendRequestAsync(addProjectFunction);
        }

        public Task<TransactionReceipt> AddProjectRequestAndWaitForReceiptAsync(string name, string symbol, uint targetAmount, uint maxAmount, BigInteger price, ulong startAt, ulong finishAt, string projectWallet, CancellationTokenSource cancellationToken = null)
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
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addProjectFunction, cancellationToken);
        }

        public Task<string> ChangeProjectRequestAsync(ChangeProjectFunction changeProjectFunction)
        {
             return ContractHandler.SendRequestAsync(changeProjectFunction);
        }

        public Task<TransactionReceipt> ChangeProjectRequestAndWaitForReceiptAsync(ChangeProjectFunction changeProjectFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(changeProjectFunction, cancellationToken);
        }

        public Task<string> ChangeProjectRequestAsync(BigInteger projectId, string name, string symbol, uint targetAmount, uint maxAmount, BigInteger price, ulong startAt, ulong finishAt, string projectWallet)
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
            
             return ContractHandler.SendRequestAsync(changeProjectFunction);
        }

        public Task<TransactionReceipt> ChangeProjectRequestAndWaitForReceiptAsync(BigInteger projectId, string name, string symbol, uint targetAmount, uint maxAmount, BigInteger price, ulong startAt, ulong finishAt, string projectWallet, CancellationTokenSource cancellationToken = null)
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
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(changeProjectFunction, cancellationToken);
        }

        public Task<string> ConfirmRequestAsync(ConfirmFunction confirmFunction)
        {
             return ContractHandler.SendRequestAsync(confirmFunction);
        }

        public Task<TransactionReceipt> ConfirmRequestAndWaitForReceiptAsync(ConfirmFunction confirmFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(confirmFunction, cancellationToken);
        }

        public Task<string> ConfirmRequestAsync(BigInteger projectId)
        {
            var confirmFunction = new ConfirmFunction();
                confirmFunction.ProjectId = projectId;
            
             return ContractHandler.SendRequestAsync(confirmFunction);
        }

        public Task<TransactionReceipt> ConfirmRequestAndWaitForReceiptAsync(BigInteger projectId, CancellationTokenSource cancellationToken = null)
        {
            var confirmFunction = new ConfirmFunction();
                confirmFunction.ProjectId = projectId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(confirmFunction, cancellationToken);
        }

        public Task<string> FinishRequestAsync(FinishFunction finishFunction)
        {
             return ContractHandler.SendRequestAsync(finishFunction);
        }

        public Task<TransactionReceipt> FinishRequestAndWaitForReceiptAsync(FinishFunction finishFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(finishFunction, cancellationToken);
        }

        public Task<string> FinishRequestAsync(BigInteger projectId)
        {
            var finishFunction = new FinishFunction();
                finishFunction.ProjectId = projectId;
            
             return ContractHandler.SendRequestAsync(finishFunction);
        }

        public Task<TransactionReceipt> FinishRequestAndWaitForReceiptAsync(BigInteger projectId, CancellationTokenSource cancellationToken = null)
        {
            var finishFunction = new FinishFunction();
                finishFunction.ProjectId = projectId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(finishFunction, cancellationToken);
        }

        public Task<string> GetErc20QueryAsync(GetErc20Function getErc20Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc20Function, string>(getErc20Function, blockParameter);
        }

        
        public Task<string> GetErc20QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc20Function, string>(null, blockParameter);
        }

        public Task<string> GetErc721BaseUriQueryAsync(GetErc721BaseUriFunction getErc721BaseUriFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc721BaseUriFunction, string>(getErc721BaseUriFunction, blockParameter);
        }

        
        public Task<string> GetErc721BaseUriQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc721BaseUriFunction, string>(null, blockParameter);
        }

        public Task<string> GetErc721ImplementationQueryAsync(GetErc721ImplementationFunction getErc721ImplementationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc721ImplementationFunction, string>(getErc721ImplementationFunction, blockParameter);
        }

        
        public Task<string> GetErc721ImplementationQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc721ImplementationFunction, string>(null, blockParameter);
        }

        public Task<string> GetImplementationQueryAsync(GetImplementationFunction getImplementationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetImplementationFunction, string>(getImplementationFunction, blockParameter);
        }

        
        public Task<string> GetImplementationQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetImplementationFunction, string>(null, blockParameter);
        }

        public Task<GetProjectOutputDTO> GetProjectQueryAsync(GetProjectFunction getProjectFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetProjectFunction, GetProjectOutputDTO>(getProjectFunction, blockParameter);
        }

        public Task<GetProjectOutputDTO> GetProjectQueryAsync(BigInteger projectId, BlockParameter blockParameter = null)
        {
            var getProjectFunction = new GetProjectFunction();
                getProjectFunction.ProjectId = projectId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetProjectFunction, GetProjectOutputDTO>(getProjectFunction, blockParameter);
        }

        public Task<GetProjectByErc721AddressOutputDTO> GetProjectByErc721AddressQueryAsync(GetProjectByErc721AddressFunction getProjectByErc721AddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetProjectByErc721AddressFunction, GetProjectByErc721AddressOutputDTO>(getProjectByErc721AddressFunction, blockParameter);
        }

        public Task<GetProjectByErc721AddressOutputDTO> GetProjectByErc721AddressQueryAsync(string erc721Address, BlockParameter blockParameter = null)
        {
            var getProjectByErc721AddressFunction = new GetProjectByErc721AddressFunction();
                getProjectByErc721AddressFunction.Erc721Address = erc721Address;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetProjectByErc721AddressFunction, GetProjectByErc721AddressOutputDTO>(getProjectByErc721AddressFunction, blockParameter);
        }

        public Task<string> GetProjectInvestorQueryAsync(GetProjectInvestorFunction getProjectInvestorFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetProjectInvestorFunction, string>(getProjectInvestorFunction, blockParameter);
        }

        
        public Task<string> GetProjectInvestorQueryAsync(BigInteger projectId, BigInteger index, BlockParameter blockParameter = null)
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

        
        public Task<byte[]> GetRoleAdminQueryAsync(byte[] role, BlockParameter blockParameter = null)
        {
            var getRoleAdminFunction = new GetRoleAdminFunction();
                getRoleAdminFunction.Role = role;
            
            return ContractHandler.QueryAsync<GetRoleAdminFunction, byte[]>(getRoleAdminFunction, blockParameter);
        }

        public Task<string> GetWhitelistQueryAsync(GetWhitelistFunction getWhitelistFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWhitelistFunction, string>(getWhitelistFunction, blockParameter);
        }

        
        public Task<string> GetWhitelistQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWhitelistFunction, string>(null, blockParameter);
        }

        public Task<string> GrantRoleRequestAsync(GrantRoleFunction grantRoleFunction)
        {
             return ContractHandler.SendRequestAsync(grantRoleFunction);
        }

        public Task<TransactionReceipt> GrantRoleRequestAndWaitForReceiptAsync(GrantRoleFunction grantRoleFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(grantRoleFunction, cancellationToken);
        }

        public Task<string> GrantRoleRequestAsync(byte[] role, string account)
        {
            var grantRoleFunction = new GrantRoleFunction();
                grantRoleFunction.Role = role;
                grantRoleFunction.Account = account;
            
             return ContractHandler.SendRequestAsync(grantRoleFunction);
        }

        public Task<TransactionReceipt> GrantRoleRequestAndWaitForReceiptAsync(byte[] role, string account, CancellationTokenSource cancellationToken = null)
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

        
        public Task<bool> HasRoleQueryAsync(byte[] role, string account, BlockParameter blockParameter = null)
        {
            var hasRoleFunction = new HasRoleFunction();
                hasRoleFunction.Role = role;
                hasRoleFunction.Account = account;
            
            return ContractHandler.QueryAsync<HasRoleFunction, bool>(hasRoleFunction, blockParameter);
        }

        public Task<string> InvestRequestAsync(InvestFunction investFunction)
        {
             return ContractHandler.SendRequestAsync(investFunction);
        }

        public Task<TransactionReceipt> InvestRequestAndWaitForReceiptAsync(InvestFunction investFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(investFunction, cancellationToken);
        }

        public Task<string> InvestRequestAsync(BigInteger projectId, ushort amount)
        {
            var investFunction = new InvestFunction();
                investFunction.ProjectId = projectId;
                investFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(investFunction);
        }

        public Task<TransactionReceipt> InvestRequestAndWaitForReceiptAsync(BigInteger projectId, ushort amount, CancellationTokenSource cancellationToken = null)
        {
            var investFunction = new InvestFunction();
                investFunction.ProjectId = projectId;
                investFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(investFunction, cancellationToken);
        }

        public Task<string> InvestWithPermitRequestAsync(InvestWithPermitFunction investWithPermitFunction)
        {
             return ContractHandler.SendRequestAsync(investWithPermitFunction);
        }

        public Task<TransactionReceipt> InvestWithPermitRequestAndWaitForReceiptAsync(InvestWithPermitFunction investWithPermitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(investWithPermitFunction, cancellationToken);
        }

        public Task<string> InvestWithPermitRequestAsync(BigInteger projectId, ushort investAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s)
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

        public Task<TransactionReceipt> InvestWithPermitRequestAndWaitForReceiptAsync(BigInteger projectId, ushort investAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null)
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

        public Task<string> PauseRequestAsync(PauseFunction pauseFunction)
        {
             return ContractHandler.SendRequestAsync(pauseFunction);
        }

        public Task<string> PauseRequestAsync()
        {
             return ContractHandler.SendRequestAsync<PauseFunction>();
        }

        public Task<TransactionReceipt> PauseRequestAndWaitForReceiptAsync(PauseFunction pauseFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(pauseFunction, cancellationToken);
        }

        public Task<TransactionReceipt> PauseRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<PauseFunction>(null, cancellationToken);
        }

        public Task<bool> PausedQueryAsync(PausedFunction pausedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PausedFunction, bool>(pausedFunction, blockParameter);
        }

        
        public Task<bool> PausedQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PausedFunction, bool>(null, blockParameter);
        }

        public Task<BigInteger> ProjectAmountQueryAsync(ProjectAmountFunction projectAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProjectAmountFunction, BigInteger>(projectAmountFunction, blockParameter);
        }

        
        public Task<BigInteger> ProjectAmountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProjectAmountFunction, BigInteger>(null, blockParameter);
        }

        public Task<byte[]> ProxiableUUIDQueryAsync(ProxiableUUIDFunction proxiableUUIDFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProxiableUUIDFunction, byte[]>(proxiableUUIDFunction, blockParameter);
        }

        
        public Task<byte[]> ProxiableUUIDQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProxiableUUIDFunction, byte[]>(null, blockParameter);
        }

        public Task<string> RenounceRoleRequestAsync(RenounceRoleFunction renounceRoleFunction)
        {
             return ContractHandler.SendRequestAsync(renounceRoleFunction);
        }

        public Task<TransactionReceipt> RenounceRoleRequestAndWaitForReceiptAsync(RenounceRoleFunction renounceRoleFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(renounceRoleFunction, cancellationToken);
        }

        public Task<string> RenounceRoleRequestAsync(byte[] role, string callerConfirmation)
        {
            var renounceRoleFunction = new RenounceRoleFunction();
                renounceRoleFunction.Role = role;
                renounceRoleFunction.CallerConfirmation = callerConfirmation;
            
             return ContractHandler.SendRequestAsync(renounceRoleFunction);
        }

        public Task<TransactionReceipt> RenounceRoleRequestAndWaitForReceiptAsync(byte[] role, string callerConfirmation, CancellationTokenSource cancellationToken = null)
        {
            var renounceRoleFunction = new RenounceRoleFunction();
                renounceRoleFunction.Role = role;
                renounceRoleFunction.CallerConfirmation = callerConfirmation;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(renounceRoleFunction, cancellationToken);
        }

        public Task<string> RevokeRoleRequestAsync(RevokeRoleFunction revokeRoleFunction)
        {
             return ContractHandler.SendRequestAsync(revokeRoleFunction);
        }

        public Task<TransactionReceipt> RevokeRoleRequestAndWaitForReceiptAsync(RevokeRoleFunction revokeRoleFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(revokeRoleFunction, cancellationToken);
        }

        public Task<string> RevokeRoleRequestAsync(byte[] role, string account)
        {
            var revokeRoleFunction = new RevokeRoleFunction();
                revokeRoleFunction.Role = role;
                revokeRoleFunction.Account = account;
            
             return ContractHandler.SendRequestAsync(revokeRoleFunction);
        }

        public Task<TransactionReceipt> RevokeRoleRequestAndWaitForReceiptAsync(byte[] role, string account, CancellationTokenSource cancellationToken = null)
        {
            var revokeRoleFunction = new RevokeRoleFunction();
                revokeRoleFunction.Role = role;
                revokeRoleFunction.Account = account;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(revokeRoleFunction, cancellationToken);
        }

        public Task<string> SetErc721BaseUriRequestAsync(SetErc721BaseUriFunction setErc721BaseUriFunction)
        {
             return ContractHandler.SendRequestAsync(setErc721BaseUriFunction);
        }

        public Task<TransactionReceipt> SetErc721BaseUriRequestAndWaitForReceiptAsync(SetErc721BaseUriFunction setErc721BaseUriFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setErc721BaseUriFunction, cancellationToken);
        }

        public Task<string> SetErc721BaseUriRequestAsync(string erc721baseuri)
        {
            var setErc721BaseUriFunction = new SetErc721BaseUriFunction();
                setErc721BaseUriFunction.Erc721baseuri = erc721baseuri;
            
             return ContractHandler.SendRequestAsync(setErc721BaseUriFunction);
        }

        public Task<TransactionReceipt> SetErc721BaseUriRequestAndWaitForReceiptAsync(string erc721baseuri, CancellationTokenSource cancellationToken = null)
        {
            var setErc721BaseUriFunction = new SetErc721BaseUriFunction();
                setErc721BaseUriFunction.Erc721baseuri = erc721baseuri;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setErc721BaseUriFunction, cancellationToken);
        }

        public Task<string> SetErc721ImplementationRequestAsync(SetErc721ImplementationFunction setErc721ImplementationFunction)
        {
             return ContractHandler.SendRequestAsync(setErc721ImplementationFunction);
        }

        public Task<TransactionReceipt> SetErc721ImplementationRequestAndWaitForReceiptAsync(SetErc721ImplementationFunction setErc721ImplementationFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setErc721ImplementationFunction, cancellationToken);
        }

        public Task<string> SetErc721ImplementationRequestAsync(string newImplementation)
        {
            var setErc721ImplementationFunction = new SetErc721ImplementationFunction();
                setErc721ImplementationFunction.NewImplementation = newImplementation;
            
             return ContractHandler.SendRequestAsync(setErc721ImplementationFunction);
        }

        public Task<TransactionReceipt> SetErc721ImplementationRequestAndWaitForReceiptAsync(string newImplementation, CancellationTokenSource cancellationToken = null)
        {
            var setErc721ImplementationFunction = new SetErc721ImplementationFunction();
                setErc721ImplementationFunction.NewImplementation = newImplementation;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setErc721ImplementationFunction, cancellationToken);
        }

        public Task<string> SetWhitelistRequestAsync(SetWhitelistFunction setWhitelistFunction)
        {
             return ContractHandler.SendRequestAsync(setWhitelistFunction);
        }

        public Task<TransactionReceipt> SetWhitelistRequestAndWaitForReceiptAsync(SetWhitelistFunction setWhitelistFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setWhitelistFunction, cancellationToken);
        }

        public Task<string> SetWhitelistRequestAsync(string newAddress)
        {
            var setWhitelistFunction = new SetWhitelistFunction();
                setWhitelistFunction.NewAddress = newAddress;
            
             return ContractHandler.SendRequestAsync(setWhitelistFunction);
        }

        public Task<TransactionReceipt> SetWhitelistRequestAndWaitForReceiptAsync(string newAddress, CancellationTokenSource cancellationToken = null)
        {
            var setWhitelistFunction = new SetWhitelistFunction();
                setWhitelistFunction.NewAddress = newAddress;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setWhitelistFunction, cancellationToken);
        }

        public Task<bool> SupportsInterfaceQueryAsync(SupportsInterfaceFunction supportsInterfaceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SupportsInterfaceFunction, bool>(supportsInterfaceFunction, blockParameter);
        }

        
        public Task<bool> SupportsInterfaceQueryAsync(byte[] interfaceId, BlockParameter blockParameter = null)
        {
            var supportsInterfaceFunction = new SupportsInterfaceFunction();
                supportsInterfaceFunction.InterfaceId = interfaceId;
            
            return ContractHandler.QueryAsync<SupportsInterfaceFunction, bool>(supportsInterfaceFunction, blockParameter);
        }

        public Task<string> UnpauseRequestAsync(UnpauseFunction unpauseFunction)
        {
             return ContractHandler.SendRequestAsync(unpauseFunction);
        }

        public Task<string> UnpauseRequestAsync()
        {
             return ContractHandler.SendRequestAsync<UnpauseFunction>();
        }

        public Task<TransactionReceipt> UnpauseRequestAndWaitForReceiptAsync(UnpauseFunction unpauseFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(unpauseFunction, cancellationToken);
        }

        public Task<TransactionReceipt> UnpauseRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<UnpauseFunction>(null, cancellationToken);
        }

        public Task<string> UpgradeToAndCallRequestAsync(UpgradeToAndCallFunction upgradeToAndCallFunction)
        {
             return ContractHandler.SendRequestAsync(upgradeToAndCallFunction);
        }

        public Task<TransactionReceipt> UpgradeToAndCallRequestAndWaitForReceiptAsync(UpgradeToAndCallFunction upgradeToAndCallFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(upgradeToAndCallFunction, cancellationToken);
        }

        public Task<string> UpgradeToAndCallRequestAsync(string newImplementation, byte[] data)
        {
            var upgradeToAndCallFunction = new UpgradeToAndCallFunction();
                upgradeToAndCallFunction.NewImplementation = newImplementation;
                upgradeToAndCallFunction.Data = data;
            
             return ContractHandler.SendRequestAsync(upgradeToAndCallFunction);
        }

        public Task<TransactionReceipt> UpgradeToAndCallRequestAndWaitForReceiptAsync(string newImplementation, byte[] data, CancellationTokenSource cancellationToken = null)
        {
            var upgradeToAndCallFunction = new UpgradeToAndCallFunction();
                upgradeToAndCallFunction.NewImplementation = newImplementation;
                upgradeToAndCallFunction.Data = data;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(upgradeToAndCallFunction, cancellationToken);
        }
    }
}
