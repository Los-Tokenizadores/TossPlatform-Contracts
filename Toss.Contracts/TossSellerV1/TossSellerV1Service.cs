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
using Toss.Contracts.TossSellerV1.ContractDefinition;

namespace Toss.Contracts.TossSellerV1
{
    public partial class TossSellerV1Service
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.IWeb3 web3, TossSellerV1Deployment tossSellerV1Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<TossSellerV1Deployment>().SendRequestAndWaitForReceiptAsync(tossSellerV1Deployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.IWeb3 web3, TossSellerV1Deployment tossSellerV1Deployment)
        {
            return web3.Eth.GetContractDeploymentHandler<TossSellerV1Deployment>().SendRequestAsync(tossSellerV1Deployment);
        }

        public static async Task<TossSellerV1Service> DeployContractAndGetServiceAsync(Nethereum.Web3.IWeb3 web3, TossSellerV1Deployment tossSellerV1Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, tossSellerV1Deployment, cancellationTokenSource);
            return new TossSellerV1Service(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public TossSellerV1Service(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public TossSellerV1Service(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<byte[]> ConvertRoleQueryAsync(ConvertRoleFunction convertRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ConvertRoleFunction, byte[]>(convertRoleFunction, blockParameter);
        }

        
        public Task<byte[]> ConvertRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ConvertRoleFunction, byte[]>(null, blockParameter);
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

        public Task<byte> SellMaxLimitQueryAsync(SellMaxLimitFunction sellMaxLimitFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SellMaxLimitFunction, byte>(sellMaxLimitFunction, blockParameter);
        }

        
        public Task<byte> SellMaxLimitQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SellMaxLimitFunction, byte>(null, blockParameter);
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

        public Task<string> Tosssellerv1InitRequestAsync(Tosssellerv1InitFunction tosssellerv1InitFunction)
        {
             return ContractHandler.SendRequestAsync(tosssellerv1InitFunction);
        }

        public Task<TransactionReceipt> Tosssellerv1InitRequestAndWaitForReceiptAsync(Tosssellerv1InitFunction tosssellerv1InitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tosssellerv1InitFunction, cancellationToken);
        }

        public Task<string> Tosssellerv1InitRequestAsync(string erc20)
        {
            var tosssellerv1InitFunction = new Tosssellerv1InitFunction();
                tosssellerv1InitFunction.Erc20 = erc20;
            
             return ContractHandler.SendRequestAsync(tosssellerv1InitFunction);
        }

        public Task<TransactionReceipt> Tosssellerv1InitRequestAndWaitForReceiptAsync(string erc20, CancellationTokenSource cancellationToken = null)
        {
            var tosssellerv1InitFunction = new Tosssellerv1InitFunction();
                tosssellerv1InitFunction.Erc20 = erc20;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tosssellerv1InitFunction, cancellationToken);
        }

        public Task<string> BuyErc721RequestAsync(BuyErc721Function buyErc721Function)
        {
             return ContractHandler.SendRequestAsync(buyErc721Function);
        }

        public Task<TransactionReceipt> BuyErc721RequestAndWaitForReceiptAsync(BuyErc721Function buyErc721Function, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(buyErc721Function, cancellationToken);
        }

        public Task<string> BuyErc721RequestAsync(string erc721, byte amount)
        {
            var buyErc721Function = new BuyErc721Function();
                buyErc721Function.Erc721 = erc721;
                buyErc721Function.Amount = amount;
            
             return ContractHandler.SendRequestAsync(buyErc721Function);
        }

        public Task<TransactionReceipt> BuyErc721RequestAndWaitForReceiptAsync(string erc721, byte amount, CancellationTokenSource cancellationToken = null)
        {
            var buyErc721Function = new BuyErc721Function();
                buyErc721Function.Erc721 = erc721;
                buyErc721Function.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(buyErc721Function, cancellationToken);
        }

        public Task<string> BuyErc721WithPermitRequestAsync(BuyErc721WithPermitFunction buyErc721WithPermitFunction)
        {
             return ContractHandler.SendRequestAsync(buyErc721WithPermitFunction);
        }

        public Task<TransactionReceipt> BuyErc721WithPermitRequestAndWaitForReceiptAsync(BuyErc721WithPermitFunction buyErc721WithPermitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(buyErc721WithPermitFunction, cancellationToken);
        }

        public Task<string> BuyErc721WithPermitRequestAsync(string erc721, byte buyAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s)
        {
            var buyErc721WithPermitFunction = new BuyErc721WithPermitFunction();
                buyErc721WithPermitFunction.Erc721 = erc721;
                buyErc721WithPermitFunction.BuyAmount = buyAmount;
                buyErc721WithPermitFunction.Amount = amount;
                buyErc721WithPermitFunction.Deadline = deadline;
                buyErc721WithPermitFunction.V = v;
                buyErc721WithPermitFunction.R = r;
                buyErc721WithPermitFunction.S = s;
            
             return ContractHandler.SendRequestAsync(buyErc721WithPermitFunction);
        }

        public Task<TransactionReceipt> BuyErc721WithPermitRequestAndWaitForReceiptAsync(string erc721, byte buyAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null)
        {
            var buyErc721WithPermitFunction = new BuyErc721WithPermitFunction();
                buyErc721WithPermitFunction.Erc721 = erc721;
                buyErc721WithPermitFunction.BuyAmount = buyAmount;
                buyErc721WithPermitFunction.Amount = amount;
                buyErc721WithPermitFunction.Deadline = deadline;
                buyErc721WithPermitFunction.V = v;
                buyErc721WithPermitFunction.R = r;
                buyErc721WithPermitFunction.S = s;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(buyErc721WithPermitFunction, cancellationToken);
        }

        public Task<string> ConvertToErc20RequestAsync(ConvertToErc20Function convertToErc20Function)
        {
             return ContractHandler.SendRequestAsync(convertToErc20Function);
        }

        public Task<TransactionReceipt> ConvertToErc20RequestAndWaitForReceiptAsync(ConvertToErc20Function convertToErc20Function, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(convertToErc20Function, cancellationToken);
        }

        public Task<string> ConvertToErc20RequestAsync(string user, uint offchainAmount)
        {
            var convertToErc20Function = new ConvertToErc20Function();
                convertToErc20Function.User = user;
                convertToErc20Function.OffchainAmount = offchainAmount;
            
             return ContractHandler.SendRequestAsync(convertToErc20Function);
        }

        public Task<TransactionReceipt> ConvertToErc20RequestAndWaitForReceiptAsync(string user, uint offchainAmount, CancellationTokenSource cancellationToken = null)
        {
            var convertToErc20Function = new ConvertToErc20Function();
                convertToErc20Function.User = user;
                convertToErc20Function.OffchainAmount = offchainAmount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(convertToErc20Function, cancellationToken);
        }

        public Task<string> ConvertToOffchainRequestAsync(ConvertToOffchainFunction convertToOffchainFunction)
        {
             return ContractHandler.SendRequestAsync(convertToOffchainFunction);
        }

        public Task<TransactionReceipt> ConvertToOffchainRequestAndWaitForReceiptAsync(ConvertToOffchainFunction convertToOffchainFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(convertToOffchainFunction, cancellationToken);
        }

        public Task<string> ConvertToOffchainRequestAsync(BigInteger erc20Amount)
        {
            var convertToOffchainFunction = new ConvertToOffchainFunction();
                convertToOffchainFunction.Erc20Amount = erc20Amount;
            
             return ContractHandler.SendRequestAsync(convertToOffchainFunction);
        }

        public Task<TransactionReceipt> ConvertToOffchainRequestAndWaitForReceiptAsync(BigInteger erc20Amount, CancellationTokenSource cancellationToken = null)
        {
            var convertToOffchainFunction = new ConvertToOffchainFunction();
                convertToOffchainFunction.Erc20Amount = erc20Amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(convertToOffchainFunction, cancellationToken);
        }

        public Task<string> ConvertToOffchainWithPermitRequestAsync(ConvertToOffchainWithPermitFunction convertToOffchainWithPermitFunction)
        {
             return ContractHandler.SendRequestAsync(convertToOffchainWithPermitFunction);
        }

        public Task<TransactionReceipt> ConvertToOffchainWithPermitRequestAndWaitForReceiptAsync(ConvertToOffchainWithPermitFunction convertToOffchainWithPermitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(convertToOffchainWithPermitFunction, cancellationToken);
        }

        public Task<string> ConvertToOffchainWithPermitRequestAsync(BigInteger erc20Amount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s)
        {
            var convertToOffchainWithPermitFunction = new ConvertToOffchainWithPermitFunction();
                convertToOffchainWithPermitFunction.Erc20Amount = erc20Amount;
                convertToOffchainWithPermitFunction.Amount = amount;
                convertToOffchainWithPermitFunction.Deadline = deadline;
                convertToOffchainWithPermitFunction.V = v;
                convertToOffchainWithPermitFunction.R = r;
                convertToOffchainWithPermitFunction.S = s;
            
             return ContractHandler.SendRequestAsync(convertToOffchainWithPermitFunction);
        }

        public Task<TransactionReceipt> ConvertToOffchainWithPermitRequestAndWaitForReceiptAsync(BigInteger erc20Amount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null)
        {
            var convertToOffchainWithPermitFunction = new ConvertToOffchainWithPermitFunction();
                convertToOffchainWithPermitFunction.Erc20Amount = erc20Amount;
                convertToOffchainWithPermitFunction.Amount = amount;
                convertToOffchainWithPermitFunction.Deadline = deadline;
                convertToOffchainWithPermitFunction.V = v;
                convertToOffchainWithPermitFunction.R = r;
                convertToOffchainWithPermitFunction.S = s;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(convertToOffchainWithPermitFunction, cancellationToken);
        }

        public Task<ushort> GetConvertToErc20CutQueryAsync(GetConvertToErc20CutFunction getConvertToErc20CutFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetConvertToErc20CutFunction, ushort>(getConvertToErc20CutFunction, blockParameter);
        }

        
        public Task<ushort> GetConvertToErc20CutQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetConvertToErc20CutFunction, ushort>(null, blockParameter);
        }

        public Task<uint> GetConvertToErc20MinAmountQueryAsync(GetConvertToErc20MinAmountFunction getConvertToErc20MinAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetConvertToErc20MinAmountFunction, uint>(getConvertToErc20MinAmountFunction, blockParameter);
        }

        
        public Task<uint> GetConvertToErc20MinAmountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetConvertToErc20MinAmountFunction, uint>(null, blockParameter);
        }

        public Task<uint> GetConvertToErc20RateQueryAsync(GetConvertToErc20RateFunction getConvertToErc20RateFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetConvertToErc20RateFunction, uint>(getConvertToErc20RateFunction, blockParameter);
        }

        
        public Task<uint> GetConvertToErc20RateQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetConvertToErc20RateFunction, uint>(null, blockParameter);
        }

        public Task<ushort> GetConvertToOffchainCutQueryAsync(GetConvertToOffchainCutFunction getConvertToOffchainCutFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetConvertToOffchainCutFunction, ushort>(getConvertToOffchainCutFunction, blockParameter);
        }

        
        public Task<ushort> GetConvertToOffchainCutQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetConvertToOffchainCutFunction, ushort>(null, blockParameter);
        }

        public Task<BigInteger> GetConvertToOffchainMinAmountQueryAsync(GetConvertToOffchainMinAmountFunction getConvertToOffchainMinAmountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetConvertToOffchainMinAmountFunction, BigInteger>(getConvertToOffchainMinAmountFunction, blockParameter);
        }

        
        public Task<BigInteger> GetConvertToOffchainMinAmountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetConvertToOffchainMinAmountFunction, BigInteger>(null, blockParameter);
        }

        public Task<uint> GetConvertToOffchainRateQueryAsync(GetConvertToOffchainRateFunction getConvertToOffchainRateFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetConvertToOffchainRateFunction, uint>(getConvertToOffchainRateFunction, blockParameter);
        }

        
        public Task<uint> GetConvertToOffchainRateQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetConvertToOffchainRateFunction, uint>(null, blockParameter);
        }

        public Task<string> GetErc20QueryAsync(GetErc20Function getErc20Function, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc20Function, string>(getErc20Function, blockParameter);
        }

        
        public Task<string> GetErc20QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc20Function, string>(null, blockParameter);
        }

        public Task<string> GetErc20BankAddressQueryAsync(GetErc20BankAddressFunction getErc20BankAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc20BankAddressFunction, string>(getErc20BankAddressFunction, blockParameter);
        }

        
        public Task<string> GetErc20BankAddressQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc20BankAddressFunction, string>(null, blockParameter);
        }

        public Task<GetErc721SellsOutputDTO> GetErc721SellsQueryAsync(GetErc721SellsFunction getErc721SellsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetErc721SellsFunction, GetErc721SellsOutputDTO>(getErc721SellsFunction, blockParameter);
        }

        public Task<GetErc721SellsOutputDTO> GetErc721SellsQueryAsync(string erc721, BlockParameter blockParameter = null)
        {
            var getErc721SellsFunction = new GetErc721SellsFunction();
                getErc721SellsFunction.Erc721 = erc721;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetErc721SellsFunction, GetErc721SellsOutputDTO>(getErc721SellsFunction, blockParameter);
        }

        public Task<string> GetImplementationQueryAsync(GetImplementationFunction getImplementationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetImplementationFunction, string>(getImplementationFunction, blockParameter);
        }

        
        public Task<string> GetImplementationQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetImplementationFunction, string>(null, blockParameter);
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

        public Task<string> SetConvertToErc20CutRequestAsync(SetConvertToErc20CutFunction setConvertToErc20CutFunction)
        {
             return ContractHandler.SendRequestAsync(setConvertToErc20CutFunction);
        }

        public Task<TransactionReceipt> SetConvertToErc20CutRequestAndWaitForReceiptAsync(SetConvertToErc20CutFunction setConvertToErc20CutFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setConvertToErc20CutFunction, cancellationToken);
        }

        public Task<string> SetConvertToErc20CutRequestAsync(ushort newCut)
        {
            var setConvertToErc20CutFunction = new SetConvertToErc20CutFunction();
                setConvertToErc20CutFunction.NewCut = newCut;
            
             return ContractHandler.SendRequestAsync(setConvertToErc20CutFunction);
        }

        public Task<TransactionReceipt> SetConvertToErc20CutRequestAndWaitForReceiptAsync(ushort newCut, CancellationTokenSource cancellationToken = null)
        {
            var setConvertToErc20CutFunction = new SetConvertToErc20CutFunction();
                setConvertToErc20CutFunction.NewCut = newCut;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setConvertToErc20CutFunction, cancellationToken);
        }

        public Task<string> SetConvertToErc20MinAmountRequestAsync(SetConvertToErc20MinAmountFunction setConvertToErc20MinAmountFunction)
        {
             return ContractHandler.SendRequestAsync(setConvertToErc20MinAmountFunction);
        }

        public Task<TransactionReceipt> SetConvertToErc20MinAmountRequestAndWaitForReceiptAsync(SetConvertToErc20MinAmountFunction setConvertToErc20MinAmountFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setConvertToErc20MinAmountFunction, cancellationToken);
        }

        public Task<string> SetConvertToErc20MinAmountRequestAsync(uint minAmount)
        {
            var setConvertToErc20MinAmountFunction = new SetConvertToErc20MinAmountFunction();
                setConvertToErc20MinAmountFunction.MinAmount = minAmount;
            
             return ContractHandler.SendRequestAsync(setConvertToErc20MinAmountFunction);
        }

        public Task<TransactionReceipt> SetConvertToErc20MinAmountRequestAndWaitForReceiptAsync(uint minAmount, CancellationTokenSource cancellationToken = null)
        {
            var setConvertToErc20MinAmountFunction = new SetConvertToErc20MinAmountFunction();
                setConvertToErc20MinAmountFunction.MinAmount = minAmount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setConvertToErc20MinAmountFunction, cancellationToken);
        }

        public Task<string> SetConvertToErc20RateRequestAsync(SetConvertToErc20RateFunction setConvertToErc20RateFunction)
        {
             return ContractHandler.SendRequestAsync(setConvertToErc20RateFunction);
        }

        public Task<TransactionReceipt> SetConvertToErc20RateRequestAndWaitForReceiptAsync(SetConvertToErc20RateFunction setConvertToErc20RateFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setConvertToErc20RateFunction, cancellationToken);
        }

        public Task<string> SetConvertToErc20RateRequestAsync(uint newRate)
        {
            var setConvertToErc20RateFunction = new SetConvertToErc20RateFunction();
                setConvertToErc20RateFunction.NewRate = newRate;
            
             return ContractHandler.SendRequestAsync(setConvertToErc20RateFunction);
        }

        public Task<TransactionReceipt> SetConvertToErc20RateRequestAndWaitForReceiptAsync(uint newRate, CancellationTokenSource cancellationToken = null)
        {
            var setConvertToErc20RateFunction = new SetConvertToErc20RateFunction();
                setConvertToErc20RateFunction.NewRate = newRate;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setConvertToErc20RateFunction, cancellationToken);
        }

        public Task<string> SetConvertToOffchainCutRequestAsync(SetConvertToOffchainCutFunction setConvertToOffchainCutFunction)
        {
             return ContractHandler.SendRequestAsync(setConvertToOffchainCutFunction);
        }

        public Task<TransactionReceipt> SetConvertToOffchainCutRequestAndWaitForReceiptAsync(SetConvertToOffchainCutFunction setConvertToOffchainCutFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setConvertToOffchainCutFunction, cancellationToken);
        }

        public Task<string> SetConvertToOffchainCutRequestAsync(ushort newCut)
        {
            var setConvertToOffchainCutFunction = new SetConvertToOffchainCutFunction();
                setConvertToOffchainCutFunction.NewCut = newCut;
            
             return ContractHandler.SendRequestAsync(setConvertToOffchainCutFunction);
        }

        public Task<TransactionReceipt> SetConvertToOffchainCutRequestAndWaitForReceiptAsync(ushort newCut, CancellationTokenSource cancellationToken = null)
        {
            var setConvertToOffchainCutFunction = new SetConvertToOffchainCutFunction();
                setConvertToOffchainCutFunction.NewCut = newCut;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setConvertToOffchainCutFunction, cancellationToken);
        }

        public Task<string> SetConvertToOffchainMinAmountRequestAsync(SetConvertToOffchainMinAmountFunction setConvertToOffchainMinAmountFunction)
        {
             return ContractHandler.SendRequestAsync(setConvertToOffchainMinAmountFunction);
        }

        public Task<TransactionReceipt> SetConvertToOffchainMinAmountRequestAndWaitForReceiptAsync(SetConvertToOffchainMinAmountFunction setConvertToOffchainMinAmountFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setConvertToOffchainMinAmountFunction, cancellationToken);
        }

        public Task<string> SetConvertToOffchainMinAmountRequestAsync(BigInteger minAmount)
        {
            var setConvertToOffchainMinAmountFunction = new SetConvertToOffchainMinAmountFunction();
                setConvertToOffchainMinAmountFunction.MinAmount = minAmount;
            
             return ContractHandler.SendRequestAsync(setConvertToOffchainMinAmountFunction);
        }

        public Task<TransactionReceipt> SetConvertToOffchainMinAmountRequestAndWaitForReceiptAsync(BigInteger minAmount, CancellationTokenSource cancellationToken = null)
        {
            var setConvertToOffchainMinAmountFunction = new SetConvertToOffchainMinAmountFunction();
                setConvertToOffchainMinAmountFunction.MinAmount = minAmount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setConvertToOffchainMinAmountFunction, cancellationToken);
        }

        public Task<string> SetConvertToOffchainRateRequestAsync(SetConvertToOffchainRateFunction setConvertToOffchainRateFunction)
        {
             return ContractHandler.SendRequestAsync(setConvertToOffchainRateFunction);
        }

        public Task<TransactionReceipt> SetConvertToOffchainRateRequestAndWaitForReceiptAsync(SetConvertToOffchainRateFunction setConvertToOffchainRateFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setConvertToOffchainRateFunction, cancellationToken);
        }

        public Task<string> SetConvertToOffchainRateRequestAsync(uint newRate)
        {
            var setConvertToOffchainRateFunction = new SetConvertToOffchainRateFunction();
                setConvertToOffchainRateFunction.NewRate = newRate;
            
             return ContractHandler.SendRequestAsync(setConvertToOffchainRateFunction);
        }

        public Task<TransactionReceipt> SetConvertToOffchainRateRequestAndWaitForReceiptAsync(uint newRate, CancellationTokenSource cancellationToken = null)
        {
            var setConvertToOffchainRateFunction = new SetConvertToOffchainRateFunction();
                setConvertToOffchainRateFunction.NewRate = newRate;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setConvertToOffchainRateFunction, cancellationToken);
        }

        public Task<string> SetErc20BankAddressRequestAsync(SetErc20BankAddressFunction setErc20BankAddressFunction)
        {
             return ContractHandler.SendRequestAsync(setErc20BankAddressFunction);
        }

        public Task<TransactionReceipt> SetErc20BankAddressRequestAndWaitForReceiptAsync(SetErc20BankAddressFunction setErc20BankAddressFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setErc20BankAddressFunction, cancellationToken);
        }

        public Task<string> SetErc20BankAddressRequestAsync(string newAddress)
        {
            var setErc20BankAddressFunction = new SetErc20BankAddressFunction();
                setErc20BankAddressFunction.NewAddress = newAddress;
            
             return ContractHandler.SendRequestAsync(setErc20BankAddressFunction);
        }

        public Task<TransactionReceipt> SetErc20BankAddressRequestAndWaitForReceiptAsync(string newAddress, CancellationTokenSource cancellationToken = null)
        {
            var setErc20BankAddressFunction = new SetErc20BankAddressFunction();
                setErc20BankAddressFunction.NewAddress = newAddress;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setErc20BankAddressFunction, cancellationToken);
        }

        public Task<string> SetErc721SellRequestAsync(SetErc721SellFunction setErc721SellFunction)
        {
             return ContractHandler.SendRequestAsync(setErc721SellFunction);
        }

        public Task<TransactionReceipt> SetErc721SellRequestAndWaitForReceiptAsync(SetErc721SellFunction setErc721SellFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setErc721SellFunction, cancellationToken);
        }

        public Task<string> SetErc721SellRequestAsync(string erc721, BigInteger price, byte maxAmount)
        {
            var setErc721SellFunction = new SetErc721SellFunction();
                setErc721SellFunction.Erc721 = erc721;
                setErc721SellFunction.Price = price;
                setErc721SellFunction.MaxAmount = maxAmount;
            
             return ContractHandler.SendRequestAsync(setErc721SellFunction);
        }

        public Task<TransactionReceipt> SetErc721SellRequestAndWaitForReceiptAsync(string erc721, BigInteger price, byte maxAmount, CancellationTokenSource cancellationToken = null)
        {
            var setErc721SellFunction = new SetErc721SellFunction();
                setErc721SellFunction.Erc721 = erc721;
                setErc721SellFunction.Price = price;
                setErc721SellFunction.MaxAmount = maxAmount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setErc721SellFunction, cancellationToken);
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

        public Task<string> WithdrawBalanceRequestAsync(WithdrawBalanceFunction withdrawBalanceFunction)
        {
             return ContractHandler.SendRequestAsync(withdrawBalanceFunction);
        }

        public Task<TransactionReceipt> WithdrawBalanceRequestAndWaitForReceiptAsync(WithdrawBalanceFunction withdrawBalanceFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawBalanceFunction, cancellationToken);
        }

        public Task<string> WithdrawBalanceRequestAsync(BigInteger amount)
        {
            var withdrawBalanceFunction = new WithdrawBalanceFunction();
                withdrawBalanceFunction.Amount = amount;
            
             return ContractHandler.SendRequestAsync(withdrawBalanceFunction);
        }

        public Task<TransactionReceipt> WithdrawBalanceRequestAndWaitForReceiptAsync(BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var withdrawBalanceFunction = new WithdrawBalanceFunction();
                withdrawBalanceFunction.Amount = amount;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(withdrawBalanceFunction, cancellationToken);
        }
    }
}
