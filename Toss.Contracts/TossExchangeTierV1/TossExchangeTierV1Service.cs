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
using Toss.Contracts.TossExchangeTierV1.ContractDefinition;

namespace Toss.Contracts.TossExchangeTierV1
{
    public partial class TossExchangeTierV1Service
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.IWeb3 web3, TossExchangeTierV1Deployment tossExchangeTierV1Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<TossExchangeTierV1Deployment>().SendRequestAndWaitForReceiptAsync(tossExchangeTierV1Deployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.IWeb3 web3, TossExchangeTierV1Deployment tossExchangeTierV1Deployment)
        {
            return web3.Eth.GetContractDeploymentHandler<TossExchangeTierV1Deployment>().SendRequestAsync(tossExchangeTierV1Deployment);
        }

        public static async Task<TossExchangeTierV1Service> DeployContractAndGetServiceAsync(Nethereum.Web3.IWeb3 web3, TossExchangeTierV1Deployment tossExchangeTierV1Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, tossExchangeTierV1Deployment, cancellationTokenSource);
            return new TossExchangeTierV1Service(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public TossExchangeTierV1Service(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public TossExchangeTierV1Service(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
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

        public Task<byte> TierMaxLengthQueryAsync(TierMaxLengthFunction tierMaxLengthFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TierMaxLengthFunction, byte>(tierMaxLengthFunction, blockParameter);
        }

        
        public Task<byte> TierMaxLengthQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TierMaxLengthFunction, byte>(null, blockParameter);
        }

        public Task<byte[]> TierRoleQueryAsync(TierRoleFunction tierRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TierRoleFunction, byte[]>(tierRoleFunction, blockParameter);
        }

        
        public Task<byte[]> TierRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TierRoleFunction, byte[]>(null, blockParameter);
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

        public Task<byte[]> YearRoleQueryAsync(YearRoleFunction yearRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<YearRoleFunction, byte[]>(yearRoleFunction, blockParameter);
        }

        
        public Task<byte[]> YearRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<YearRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<string> Tossexchangetierv1InitRequestAsync(Tossexchangetierv1InitFunction tossexchangetierv1InitFunction)
        {
             return ContractHandler.SendRequestAsync(tossexchangetierv1InitFunction);
        }

        public Task<TransactionReceipt> Tossexchangetierv1InitRequestAndWaitForReceiptAsync(Tossexchangetierv1InitFunction tossexchangetierv1InitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tossexchangetierv1InitFunction, cancellationToken);
        }

        public Task<string> Tossexchangetierv1InitRequestAsync(string externalerc20, BigInteger depositminamount, string internalerc20, BigInteger withdrawminamount, ulong year)
        {
            var tossexchangetierv1InitFunction = new Tossexchangetierv1InitFunction();
                tossexchangetierv1InitFunction.Externalerc20 = externalerc20;
                tossexchangetierv1InitFunction.Depositminamount = depositminamount;
                tossexchangetierv1InitFunction.Internalerc20 = internalerc20;
                tossexchangetierv1InitFunction.Withdrawminamount = withdrawminamount;
                tossexchangetierv1InitFunction.Year = year;
            
             return ContractHandler.SendRequestAsync(tossexchangetierv1InitFunction);
        }

        public Task<TransactionReceipt> Tossexchangetierv1InitRequestAndWaitForReceiptAsync(string externalerc20, BigInteger depositminamount, string internalerc20, BigInteger withdrawminamount, ulong year, CancellationTokenSource cancellationToken = null)
        {
            var tossexchangetierv1InitFunction = new Tossexchangetierv1InitFunction();
                tossexchangetierv1InitFunction.Externalerc20 = externalerc20;
                tossexchangetierv1InitFunction.Depositminamount = depositminamount;
                tossexchangetierv1InitFunction.Internalerc20 = internalerc20;
                tossexchangetierv1InitFunction.Withdrawminamount = withdrawminamount;
                tossexchangetierv1InitFunction.Year = year;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tossexchangetierv1InitFunction, cancellationToken);
        }

        public Task<ulong> CurrentYearQueryAsync(CurrentYearFunction currentYearFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CurrentYearFunction, ulong>(currentYearFunction, blockParameter);
        }

        
        public Task<ulong> CurrentYearQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CurrentYearFunction, ulong>(null, blockParameter);
        }

        public Task<string> DepositRequestAsync(DepositFunction depositFunction)
        {
             return ContractHandler.SendRequestAsync(depositFunction);
        }

        public Task<TransactionReceipt> DepositRequestAndWaitForReceiptAsync(DepositFunction depositFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(depositFunction, cancellationToken);
        }

        public Task<string> DepositRequestAsync(BigInteger externalAmount)
        {
            var depositFunction = new DepositFunction();
                depositFunction.ExternalAmount = externalAmount;
            
             return ContractHandler.SendRequestAsync(depositFunction);
        }

        public Task<TransactionReceipt> DepositRequestAndWaitForReceiptAsync(BigInteger externalAmount, CancellationTokenSource cancellationToken = null)
        {
            var depositFunction = new DepositFunction();
                depositFunction.ExternalAmount = externalAmount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(depositFunction, cancellationToken);
        }

        public Task<string> DepositWithPermitRequestAsync(DepositWithPermitFunction depositWithPermitFunction)
        {
             return ContractHandler.SendRequestAsync(depositWithPermitFunction);
        }

        public Task<TransactionReceipt> DepositWithPermitRequestAndWaitForReceiptAsync(DepositWithPermitFunction depositWithPermitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(depositWithPermitFunction, cancellationToken);
        }

        public Task<string> DepositWithPermitRequestAsync(BigInteger externalAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s)
        {
            var depositWithPermitFunction = new DepositWithPermitFunction();
                depositWithPermitFunction.ExternalAmount = externalAmount;
                depositWithPermitFunction.Amount = amount;
                depositWithPermitFunction.Deadline = deadline;
                depositWithPermitFunction.V = v;
                depositWithPermitFunction.R = r;
                depositWithPermitFunction.S = s;
            
             return ContractHandler.SendRequestAsync(depositWithPermitFunction);
        }

        public Task<TransactionReceipt> DepositWithPermitRequestAndWaitForReceiptAsync(BigInteger externalAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null)
        {
            var depositWithPermitFunction = new DepositWithPermitFunction();
                depositWithPermitFunction.ExternalAmount = externalAmount;
                depositWithPermitFunction.Amount = amount;
                depositWithPermitFunction.Deadline = deadline;
                depositWithPermitFunction.V = v;
                depositWithPermitFunction.R = r;
                depositWithPermitFunction.S = s;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(depositWithPermitFunction, cancellationToken);
        }

        public Task<BigInteger> GetDepositMinAmountQueryAsync(GetDepositMinAmountFunction getDepositMinAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetDepositMinAmountFunction, BigInteger>(getDepositMinAmountFunction, blockParameter);
        }

        
        public Task<BigInteger> GetDepositMinAmountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetDepositMinAmountFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> GetExternalErc20QueryAsync(GetExternalErc20Function getExternalErc20Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetExternalErc20Function, string>(getExternalErc20Function, blockParameter);
        }

        
        public Task<string> GetExternalErc20QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetExternalErc20Function, string>(null, blockParameter);
        }

        public Task<string> GetImplementationQueryAsync(GetImplementationFunction getImplementationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetImplementationFunction, string>(getImplementationFunction, blockParameter);
        }

        
        public Task<string> GetImplementationQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetImplementationFunction, string>(null, blockParameter);
        }

        public Task<string> GetInternalErc20QueryAsync(GetInternalErc20Function getInternalErc20Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetInternalErc20Function, string>(getInternalErc20Function, blockParameter);
        }

        
        public Task<string> GetInternalErc20QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetInternalErc20Function, string>(null, blockParameter);
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

        public Task<BigInteger> GetUserBalanceQueryAsync(GetUserBalanceFunction getUserBalanceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetUserBalanceFunction, BigInteger>(getUserBalanceFunction, blockParameter);
        }

        
        public Task<BigInteger> GetUserBalanceQueryAsync(string user, BlockParameter blockParameter = null)
        {
            var getUserBalanceFunction = new GetUserBalanceFunction();
                getUserBalanceFunction.User = user;
            
            return ContractHandler.QueryAsync<GetUserBalanceFunction, BigInteger>(getUserBalanceFunction, blockParameter);
        }

        public Task<byte> GetUserTierQueryAsync(GetUserTierFunction getUserTierFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetUserTierFunction, byte>(getUserTierFunction, blockParameter);
        }

        
        public Task<byte> GetUserTierQueryAsync(string user, BlockParameter blockParameter = null)
        {
            var getUserTierFunction = new GetUserTierFunction();
                getUserTierFunction.User = user;
            
            return ContractHandler.QueryAsync<GetUserTierFunction, byte>(getUserTierFunction, blockParameter);
        }

        public Task<string> GetWhitelistQueryAsync(GetWhitelistFunction getWhitelistFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWhitelistFunction, string>(getWhitelistFunction, blockParameter);
        }

        
        public Task<string> GetWhitelistQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWhitelistFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> GetWithdrawMinAmountQueryAsync(GetWithdrawMinAmountFunction getWithdrawMinAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWithdrawMinAmountFunction, BigInteger>(getWithdrawMinAmountFunction, blockParameter);
        }

        
        public Task<BigInteger> GetWithdrawMinAmountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWithdrawMinAmountFunction, BigInteger>(null, blockParameter);
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

        public Task<string> SetDepositMinAmountRequestAsync(SetDepositMinAmountFunction setDepositMinAmountFunction)
        {
             return ContractHandler.SendRequestAsync(setDepositMinAmountFunction);
        }

        public Task<TransactionReceipt> SetDepositMinAmountRequestAndWaitForReceiptAsync(SetDepositMinAmountFunction setDepositMinAmountFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setDepositMinAmountFunction, cancellationToken);
        }

        public Task<string> SetDepositMinAmountRequestAsync(BigInteger value)
        {
            var setDepositMinAmountFunction = new SetDepositMinAmountFunction();
                setDepositMinAmountFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setDepositMinAmountFunction);
        }

        public Task<TransactionReceipt> SetDepositMinAmountRequestAndWaitForReceiptAsync(BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var setDepositMinAmountFunction = new SetDepositMinAmountFunction();
                setDepositMinAmountFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setDepositMinAmountFunction, cancellationToken);
        }

        public Task<string> SetTierLimitRequestAsync(SetTierLimitFunction setTierLimitFunction)
        {
             return ContractHandler.SendRequestAsync(setTierLimitFunction);
        }

        public Task<TransactionReceipt> SetTierLimitRequestAndWaitForReceiptAsync(SetTierLimitFunction setTierLimitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setTierLimitFunction, cancellationToken);
        }

        public Task<string> SetTierLimitRequestAsync(byte tier, BigInteger limit)
        {
            var setTierLimitFunction = new SetTierLimitFunction();
                setTierLimitFunction.Tier = tier;
                setTierLimitFunction.Limit = limit;
            
             return ContractHandler.SendRequestAsync(setTierLimitFunction);
        }

        public Task<TransactionReceipt> SetTierLimitRequestAndWaitForReceiptAsync(byte tier, BigInteger limit, CancellationTokenSource cancellationToken = null)
        {
            var setTierLimitFunction = new SetTierLimitFunction();
                setTierLimitFunction.Tier = tier;
                setTierLimitFunction.Limit = limit;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setTierLimitFunction, cancellationToken);
        }

        public Task<string> SetUserTierRequestAsync(SetUserTierFunction setUserTierFunction)
        {
             return ContractHandler.SendRequestAsync(setUserTierFunction);
        }

        public Task<TransactionReceipt> SetUserTierRequestAndWaitForReceiptAsync(SetUserTierFunction setUserTierFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setUserTierFunction, cancellationToken);
        }

        public Task<string> SetUserTierRequestAsync(string user, byte tier)
        {
            var setUserTierFunction = new SetUserTierFunction();
                setUserTierFunction.User = user;
                setUserTierFunction.Tier = tier;
            
             return ContractHandler.SendRequestAsync(setUserTierFunction);
        }

        public Task<TransactionReceipt> SetUserTierRequestAndWaitForReceiptAsync(string user, byte tier, CancellationTokenSource cancellationToken = null)
        {
            var setUserTierFunction = new SetUserTierFunction();
                setUserTierFunction.User = user;
                setUserTierFunction.Tier = tier;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setUserTierFunction, cancellationToken);
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

        public Task<string> SetWithdrawMinAmountRequestAsync(SetWithdrawMinAmountFunction setWithdrawMinAmountFunction)
        {
             return ContractHandler.SendRequestAsync(setWithdrawMinAmountFunction);
        }

        public Task<TransactionReceipt> SetWithdrawMinAmountRequestAndWaitForReceiptAsync(SetWithdrawMinAmountFunction setWithdrawMinAmountFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setWithdrawMinAmountFunction, cancellationToken);
        }

        public Task<string> SetWithdrawMinAmountRequestAsync(BigInteger value)
        {
            var setWithdrawMinAmountFunction = new SetWithdrawMinAmountFunction();
                setWithdrawMinAmountFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setWithdrawMinAmountFunction);
        }

        public Task<TransactionReceipt> SetWithdrawMinAmountRequestAndWaitForReceiptAsync(BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var setWithdrawMinAmountFunction = new SetWithdrawMinAmountFunction();
                setWithdrawMinAmountFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setWithdrawMinAmountFunction, cancellationToken);
        }

        public Task<string> SetYearRequestAsync(SetYearFunction setYearFunction)
        {
             return ContractHandler.SendRequestAsync(setYearFunction);
        }

        public Task<TransactionReceipt> SetYearRequestAndWaitForReceiptAsync(SetYearFunction setYearFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setYearFunction, cancellationToken);
        }

        public Task<string> SetYearRequestAsync(ulong year)
        {
            var setYearFunction = new SetYearFunction();
                setYearFunction.Year = year;
            
             return ContractHandler.SendRequestAsync(setYearFunction);
        }

        public Task<TransactionReceipt> SetYearRequestAndWaitForReceiptAsync(ulong year, CancellationTokenSource cancellationToken = null)
        {
            var setYearFunction = new SetYearFunction();
                setYearFunction.Year = year;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setYearFunction, cancellationToken);
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

        public Task<BigInteger> TiersQueryAsync(TiersFunction tiersFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TiersFunction, BigInteger>(tiersFunction, blockParameter);
        }

        
        public Task<BigInteger> TiersQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var tiersFunction = new TiersFunction();
                tiersFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<TiersFunction, BigInteger>(tiersFunction, blockParameter);
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

        public Task<bool> ValidStateQueryAsync(ValidStateFunction validStateFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ValidStateFunction, bool>(validStateFunction, blockParameter);
        }

        
        public Task<bool> ValidStateQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ValidStateFunction, bool>(null, blockParameter);
        }

        public Task<string> WithdrawRequestAsync(WithdrawFunction withdrawFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawFunction);
        }

        public Task<TransactionReceipt> WithdrawRequestAndWaitForReceiptAsync(WithdrawFunction withdrawFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawFunction, cancellationToken);
        }

        public Task<string> WithdrawRequestAsync(BigInteger amount)
        {
            var withdrawFunction = new WithdrawFunction();
                withdrawFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(withdrawFunction);
        }

        public Task<TransactionReceipt> WithdrawRequestAndWaitForReceiptAsync(BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var withdrawFunction = new WithdrawFunction();
                withdrawFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawFunction, cancellationToken);
        }

        public Task<string> WithdrawWithPermitRequestAsync(WithdrawWithPermitFunction withdrawWithPermitFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawWithPermitFunction);
        }

        public Task<TransactionReceipt> WithdrawWithPermitRequestAndWaitForReceiptAsync(WithdrawWithPermitFunction withdrawWithPermitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawWithPermitFunction, cancellationToken);
        }

        public Task<string> WithdrawWithPermitRequestAsync(BigInteger internalAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s)
        {
            var withdrawWithPermitFunction = new WithdrawWithPermitFunction();
                withdrawWithPermitFunction.InternalAmount = internalAmount;
                withdrawWithPermitFunction.Amount = amount;
                withdrawWithPermitFunction.Deadline = deadline;
                withdrawWithPermitFunction.V = v;
                withdrawWithPermitFunction.R = r;
                withdrawWithPermitFunction.S = s;
            
             return ContractHandler.SendRequestAsync(withdrawWithPermitFunction);
        }

        public Task<TransactionReceipt> WithdrawWithPermitRequestAndWaitForReceiptAsync(BigInteger internalAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null)
        {
            var withdrawWithPermitFunction = new WithdrawWithPermitFunction();
                withdrawWithPermitFunction.InternalAmount = internalAmount;
                withdrawWithPermitFunction.Amount = amount;
                withdrawWithPermitFunction.Deadline = deadline;
                withdrawWithPermitFunction.V = v;
                withdrawWithPermitFunction.R = r;
                withdrawWithPermitFunction.S = s;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawWithPermitFunction, cancellationToken);
        }
    }
}
