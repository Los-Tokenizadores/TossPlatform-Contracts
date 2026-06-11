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
    public partial class TossExchangeTierV1Service: TossExchangeTierV1ServiceBase
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

        public TossExchangeTierV1Service(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
        {
        }

    }


    public partial class TossExchangeTierV1ServiceBase: ContractWeb3ServiceBase
    {

        public TossExchangeTierV1ServiceBase(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
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

        public Task<byte[]> PauserRoleQueryAsync(PauserRoleFunction pauserRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PauserRoleFunction, byte[]>(pauserRoleFunction, blockParameter);
        }

        
        public virtual Task<byte[]> PauserRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PauserRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte> TierMaxLengthQueryAsync(TierMaxLengthFunction tierMaxLengthFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TierMaxLengthFunction, byte>(tierMaxLengthFunction, blockParameter);
        }

        
        public virtual Task<byte> TierMaxLengthQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TierMaxLengthFunction, byte>(null, blockParameter);
        }

        public Task<byte[]> TierRoleQueryAsync(TierRoleFunction tierRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TierRoleFunction, byte[]>(tierRoleFunction, blockParameter);
        }

        
        public virtual Task<byte[]> TierRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TierRoleFunction, byte[]>(null, blockParameter);
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

        public Task<byte[]> YearRoleQueryAsync(YearRoleFunction yearRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<YearRoleFunction, byte[]>(yearRoleFunction, blockParameter);
        }

        
        public virtual Task<byte[]> YearRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<YearRoleFunction, byte[]>(null, blockParameter);
        }

        public virtual Task<string> Tossexchangetierv1InitRequestAsync(Tossexchangetierv1InitFunction tossexchangetierv1InitFunction)
        {
             return ContractHandler.SendRequestAsync(tossexchangetierv1InitFunction);
        }

        public virtual Task<TransactionReceipt> Tossexchangetierv1InitRequestAndWaitForReceiptAsync(Tossexchangetierv1InitFunction tossexchangetierv1InitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tossexchangetierv1InitFunction, cancellationToken);
        }

        public virtual Task<string> Tossexchangetierv1InitRequestAsync(string externalerc20, BigInteger depositminamount, string internalerc20, BigInteger withdrawminamount, ulong year)
        {
            var tossexchangetierv1InitFunction = new Tossexchangetierv1InitFunction();
                tossexchangetierv1InitFunction.Externalerc20 = externalerc20;
                tossexchangetierv1InitFunction.Depositminamount = depositminamount;
                tossexchangetierv1InitFunction.Internalerc20 = internalerc20;
                tossexchangetierv1InitFunction.Withdrawminamount = withdrawminamount;
                tossexchangetierv1InitFunction.Year = year;
            
             return ContractHandler.SendRequestAsync(tossexchangetierv1InitFunction);
        }

        public virtual Task<TransactionReceipt> Tossexchangetierv1InitRequestAndWaitForReceiptAsync(string externalerc20, BigInteger depositminamount, string internalerc20, BigInteger withdrawminamount, ulong year, CancellationTokenSource cancellationToken = null)
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

        
        public virtual Task<ulong> CurrentYearQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CurrentYearFunction, ulong>(null, blockParameter);
        }

        public virtual Task<string> DepositRequestAsync(DepositFunction depositFunction)
        {
             return ContractHandler.SendRequestAsync(depositFunction);
        }

        public virtual Task<TransactionReceipt> DepositRequestAndWaitForReceiptAsync(DepositFunction depositFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(depositFunction, cancellationToken);
        }

        public virtual Task<string> DepositRequestAsync(BigInteger externalAmount)
        {
            var depositFunction = new DepositFunction();
                depositFunction.ExternalAmount = externalAmount;
            
             return ContractHandler.SendRequestAsync(depositFunction);
        }

        public virtual Task<TransactionReceipt> DepositRequestAndWaitForReceiptAsync(BigInteger externalAmount, CancellationTokenSource cancellationToken = null)
        {
            var depositFunction = new DepositFunction();
                depositFunction.ExternalAmount = externalAmount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(depositFunction, cancellationToken);
        }

        public virtual Task<string> DepositWithPermitRequestAsync(DepositWithPermitFunction depositWithPermitFunction)
        {
             return ContractHandler.SendRequestAsync(depositWithPermitFunction);
        }

        public virtual Task<TransactionReceipt> DepositWithPermitRequestAndWaitForReceiptAsync(DepositWithPermitFunction depositWithPermitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(depositWithPermitFunction, cancellationToken);
        }

        public virtual Task<string> DepositWithPermitRequestAsync(BigInteger externalAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s)
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

        public virtual Task<TransactionReceipt> DepositWithPermitRequestAndWaitForReceiptAsync(BigInteger externalAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null)
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

        
        public virtual Task<BigInteger> GetDepositMinAmountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetDepositMinAmountFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> GetExternalErc20QueryAsync(GetExternalErc20Function getExternalErc20Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetExternalErc20Function, string>(getExternalErc20Function, blockParameter);
        }

        
        public virtual Task<string> GetExternalErc20QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetExternalErc20Function, string>(null, blockParameter);
        }

        public Task<string> GetImplementationQueryAsync(GetImplementationFunction getImplementationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetImplementationFunction, string>(getImplementationFunction, blockParameter);
        }

        
        public virtual Task<string> GetImplementationQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetImplementationFunction, string>(null, blockParameter);
        }

        public Task<string> GetInternalErc20QueryAsync(GetInternalErc20Function getInternalErc20Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetInternalErc20Function, string>(getInternalErc20Function, blockParameter);
        }

        
        public virtual Task<string> GetInternalErc20QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetInternalErc20Function, string>(null, blockParameter);
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

        public Task<BigInteger> GetUserBalanceQueryAsync(GetUserBalanceFunction getUserBalanceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetUserBalanceFunction, BigInteger>(getUserBalanceFunction, blockParameter);
        }

        
        public virtual Task<BigInteger> GetUserBalanceQueryAsync(string user, BlockParameter blockParameter = null)
        {
            var getUserBalanceFunction = new GetUserBalanceFunction();
                getUserBalanceFunction.User = user;
            
            return ContractHandler.QueryAsync<GetUserBalanceFunction, BigInteger>(getUserBalanceFunction, blockParameter);
        }

        public Task<byte> GetUserTierQueryAsync(GetUserTierFunction getUserTierFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetUserTierFunction, byte>(getUserTierFunction, blockParameter);
        }

        
        public virtual Task<byte> GetUserTierQueryAsync(string user, BlockParameter blockParameter = null)
        {
            var getUserTierFunction = new GetUserTierFunction();
                getUserTierFunction.User = user;
            
            return ContractHandler.QueryAsync<GetUserTierFunction, byte>(getUserTierFunction, blockParameter);
        }

        public Task<string> GetWhitelistQueryAsync(GetWhitelistFunction getWhitelistFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWhitelistFunction, string>(getWhitelistFunction, blockParameter);
        }

        
        public virtual Task<string> GetWhitelistQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWhitelistFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> GetWithdrawMinAmountQueryAsync(GetWithdrawMinAmountFunction getWithdrawMinAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWithdrawMinAmountFunction, BigInteger>(getWithdrawMinAmountFunction, blockParameter);
        }

        
        public virtual Task<BigInteger> GetWithdrawMinAmountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWithdrawMinAmountFunction, BigInteger>(null, blockParameter);
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

        public virtual Task<string> SetDepositMinAmountRequestAsync(SetDepositMinAmountFunction setDepositMinAmountFunction)
        {
             return ContractHandler.SendRequestAsync(setDepositMinAmountFunction);
        }

        public virtual Task<TransactionReceipt> SetDepositMinAmountRequestAndWaitForReceiptAsync(SetDepositMinAmountFunction setDepositMinAmountFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setDepositMinAmountFunction, cancellationToken);
        }

        public virtual Task<string> SetDepositMinAmountRequestAsync(BigInteger value)
        {
            var setDepositMinAmountFunction = new SetDepositMinAmountFunction();
                setDepositMinAmountFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setDepositMinAmountFunction);
        }

        public virtual Task<TransactionReceipt> SetDepositMinAmountRequestAndWaitForReceiptAsync(BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var setDepositMinAmountFunction = new SetDepositMinAmountFunction();
                setDepositMinAmountFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setDepositMinAmountFunction, cancellationToken);
        }

        public virtual Task<string> SetTierLimitRequestAsync(SetTierLimitFunction setTierLimitFunction)
        {
             return ContractHandler.SendRequestAsync(setTierLimitFunction);
        }

        public virtual Task<TransactionReceipt> SetTierLimitRequestAndWaitForReceiptAsync(SetTierLimitFunction setTierLimitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setTierLimitFunction, cancellationToken);
        }

        public virtual Task<string> SetTierLimitRequestAsync(byte tier, BigInteger limit)
        {
            var setTierLimitFunction = new SetTierLimitFunction();
                setTierLimitFunction.Tier = tier;
                setTierLimitFunction.Limit = limit;
            
             return ContractHandler.SendRequestAsync(setTierLimitFunction);
        }

        public virtual Task<TransactionReceipt> SetTierLimitRequestAndWaitForReceiptAsync(byte tier, BigInteger limit, CancellationTokenSource cancellationToken = null)
        {
            var setTierLimitFunction = new SetTierLimitFunction();
                setTierLimitFunction.Tier = tier;
                setTierLimitFunction.Limit = limit;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setTierLimitFunction, cancellationToken);
        }

        public virtual Task<string> SetUserTierRequestAsync(SetUserTierFunction setUserTierFunction)
        {
             return ContractHandler.SendRequestAsync(setUserTierFunction);
        }

        public virtual Task<TransactionReceipt> SetUserTierRequestAndWaitForReceiptAsync(SetUserTierFunction setUserTierFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setUserTierFunction, cancellationToken);
        }

        public virtual Task<string> SetUserTierRequestAsync(string user, byte tier)
        {
            var setUserTierFunction = new SetUserTierFunction();
                setUserTierFunction.User = user;
                setUserTierFunction.Tier = tier;
            
             return ContractHandler.SendRequestAsync(setUserTierFunction);
        }

        public virtual Task<TransactionReceipt> SetUserTierRequestAndWaitForReceiptAsync(string user, byte tier, CancellationTokenSource cancellationToken = null)
        {
            var setUserTierFunction = new SetUserTierFunction();
                setUserTierFunction.User = user;
                setUserTierFunction.Tier = tier;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setUserTierFunction, cancellationToken);
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

        public virtual Task<string> SetWithdrawMinAmountRequestAsync(SetWithdrawMinAmountFunction setWithdrawMinAmountFunction)
        {
             return ContractHandler.SendRequestAsync(setWithdrawMinAmountFunction);
        }

        public virtual Task<TransactionReceipt> SetWithdrawMinAmountRequestAndWaitForReceiptAsync(SetWithdrawMinAmountFunction setWithdrawMinAmountFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setWithdrawMinAmountFunction, cancellationToken);
        }

        public virtual Task<string> SetWithdrawMinAmountRequestAsync(BigInteger value)
        {
            var setWithdrawMinAmountFunction = new SetWithdrawMinAmountFunction();
                setWithdrawMinAmountFunction.Value = value;
            
             return ContractHandler.SendRequestAsync(setWithdrawMinAmountFunction);
        }

        public virtual Task<TransactionReceipt> SetWithdrawMinAmountRequestAndWaitForReceiptAsync(BigInteger value, CancellationTokenSource cancellationToken = null)
        {
            var setWithdrawMinAmountFunction = new SetWithdrawMinAmountFunction();
                setWithdrawMinAmountFunction.Value = value;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setWithdrawMinAmountFunction, cancellationToken);
        }

        public virtual Task<string> SetYearRequestAsync(SetYearFunction setYearFunction)
        {
             return ContractHandler.SendRequestAsync(setYearFunction);
        }

        public virtual Task<TransactionReceipt> SetYearRequestAndWaitForReceiptAsync(SetYearFunction setYearFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setYearFunction, cancellationToken);
        }

        public virtual Task<string> SetYearRequestAsync(ulong year)
        {
            var setYearFunction = new SetYearFunction();
                setYearFunction.Year = year;
            
             return ContractHandler.SendRequestAsync(setYearFunction);
        }

        public virtual Task<TransactionReceipt> SetYearRequestAndWaitForReceiptAsync(ulong year, CancellationTokenSource cancellationToken = null)
        {
            var setYearFunction = new SetYearFunction();
                setYearFunction.Year = year;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setYearFunction, cancellationToken);
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

        public Task<BigInteger> TiersQueryAsync(TiersFunction tiersFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TiersFunction, BigInteger>(tiersFunction, blockParameter);
        }

        
        public virtual Task<BigInteger> TiersQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var tiersFunction = new TiersFunction();
                tiersFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<TiersFunction, BigInteger>(tiersFunction, blockParameter);
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

        public Task<bool> ValidStateQueryAsync(ValidStateFunction validStateFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ValidStateFunction, bool>(validStateFunction, blockParameter);
        }

        
        public virtual Task<bool> ValidStateQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ValidStateFunction, bool>(null, blockParameter);
        }

        public virtual Task<string> WithdrawRequestAsync(WithdrawFunction withdrawFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawFunction);
        }

        public virtual Task<TransactionReceipt> WithdrawRequestAndWaitForReceiptAsync(WithdrawFunction withdrawFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawFunction, cancellationToken);
        }

        public virtual Task<string> WithdrawRequestAsync(BigInteger amount)
        {
            var withdrawFunction = new WithdrawFunction();
                withdrawFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(withdrawFunction);
        }

        public virtual Task<TransactionReceipt> WithdrawRequestAndWaitForReceiptAsync(BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var withdrawFunction = new WithdrawFunction();
                withdrawFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawFunction, cancellationToken);
        }

        public virtual Task<string> WithdrawWithPermitRequestAsync(WithdrawWithPermitFunction withdrawWithPermitFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawWithPermitFunction);
        }

        public virtual Task<TransactionReceipt> WithdrawWithPermitRequestAndWaitForReceiptAsync(WithdrawWithPermitFunction withdrawWithPermitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawWithPermitFunction, cancellationToken);
        }

        public virtual Task<string> WithdrawWithPermitRequestAsync(BigInteger internalAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s)
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

        public virtual Task<TransactionReceipt> WithdrawWithPermitRequestAndWaitForReceiptAsync(BigInteger internalAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null)
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

        public override List<Type> GetAllFunctionTypes()
        {
            return new List<Type>
            {
                typeof(DefaultAdminRoleFunction),
                typeof(PauserRoleFunction),
                typeof(TierMaxLengthFunction),
                typeof(TierRoleFunction),
                typeof(UpgraderRoleFunction),
                typeof(UpgradeInterfaceVersionFunction),
                typeof(YearRoleFunction),
                typeof(Tossexchangetierv1InitFunction),
                typeof(CurrentYearFunction),
                typeof(DepositFunction),
                typeof(DepositWithPermitFunction),
                typeof(GetDepositMinAmountFunction),
                typeof(GetExternalErc20Function),
                typeof(GetImplementationFunction),
                typeof(GetInternalErc20Function),
                typeof(GetRoleAdminFunction),
                typeof(GetUserBalanceFunction),
                typeof(GetUserTierFunction),
                typeof(GetWhitelistFunction),
                typeof(GetWithdrawMinAmountFunction),
                typeof(GrantRoleFunction),
                typeof(HasRoleFunction),
                typeof(PauseFunction),
                typeof(PausedFunction),
                typeof(ProxiableUUIDFunction),
                typeof(RenounceRoleFunction),
                typeof(RevokeRoleFunction),
                typeof(SetDepositMinAmountFunction),
                typeof(SetTierLimitFunction),
                typeof(SetUserTierFunction),
                typeof(SetWhitelistFunction),
                typeof(SetWithdrawMinAmountFunction),
                typeof(SetYearFunction),
                typeof(SupportsInterfaceFunction),
                typeof(TiersFunction),
                typeof(UnpauseFunction),
                typeof(UpgradeToAndCallFunction),
                typeof(ValidStateFunction),
                typeof(WithdrawFunction),
                typeof(WithdrawWithPermitFunction)
            };
        }

        public override List<Type> GetAllEventTypes()
        {
            return new List<Type>
            {
                typeof(DepositedEventDTO),
                typeof(InitializedEventDTO),
                typeof(PausedEventDTO),
                typeof(RoleAdminChangedEventDTO),
                typeof(RoleGrantedEventDTO),
                typeof(RoleRevokedEventDTO),
                typeof(UnpausedEventDTO),
                typeof(UpgradedEventDTO),
                typeof(WithdrawnEventDTO),
                typeof(YearChangedEventDTO)
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
                typeof(TossExchangeAmounIsLessThanMinError),
                typeof(TossExchangeExternalAndInternalErc20AreEqualError),
                typeof(TossExchangeExternalAndInternalErc20HaveDifferentDecimalAmountError),
                typeof(TossExchangeInvalidStateError),
                typeof(TossExchangeTierOutOfRangeError),
                typeof(TossExchangeTierTier0CantChangeError),
                typeof(TossExchangeTierYearIsTheSameError),
                typeof(TossExchangeTierYearLimitReachError),
                typeof(TossValueIsZeroError),
                typeof(TossWhitelistNotInWhitelistError),
                typeof(UUPSUnauthorizedCallContextError),
                typeof(UUPSUnsupportedProxiableUUIDError)
            };
        }
    }
}
