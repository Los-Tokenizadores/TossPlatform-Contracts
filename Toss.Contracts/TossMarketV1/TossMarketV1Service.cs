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
using Toss.Contracts.TossMarketV1.ContractDefinition;

namespace Toss.Contracts.TossMarketV1
{
    public partial class TossMarketV1Service
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.IWeb3 web3, TossMarketV1Deployment tossMarketV1Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<TossMarketV1Deployment>().SendRequestAndWaitForReceiptAsync(tossMarketV1Deployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.IWeb3 web3, TossMarketV1Deployment tossMarketV1Deployment)
        {
            return web3.Eth.GetContractDeploymentHandler<TossMarketV1Deployment>().SendRequestAsync(tossMarketV1Deployment);
        }

        public static async Task<TossMarketV1Service> DeployContractAndGetServiceAsync(Nethereum.Web3.IWeb3 web3, TossMarketV1Deployment tossMarketV1Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, tossMarketV1Deployment, cancellationTokenSource);
            return new TossMarketV1Service(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public TossMarketV1Service(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public TossMarketV1Service(Nethereum.Web3.IWeb3 web3, string contractAddress)
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

        public Task<byte[]> Erc721SellerRoleQueryAsync(Erc721SellerRoleFunction erc721SellerRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<Erc721SellerRoleFunction, byte[]>(erc721SellerRoleFunction, blockParameter);
        }

        
        public Task<byte[]> Erc721SellerRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<Erc721SellerRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> ExtractRoleQueryAsync(ExtractRoleFunction extractRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ExtractRoleFunction, byte[]>(extractRoleFunction, blockParameter);
        }

        
        public Task<byte[]> ExtractRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ExtractRoleFunction, byte[]>(null, blockParameter);
        }

        public Task<byte[]> PauserRoleQueryAsync(PauserRoleFunction pauserRoleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PauserRoleFunction, byte[]>(pauserRoleFunction, blockParameter);
        }

        
        public Task<byte[]> PauserRoleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PauserRoleFunction, byte[]>(null, blockParameter);
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

        public Task<string> Tossmarketv1InitRequestAsync(Tossmarketv1InitFunction tossmarketv1InitFunction)
        {
             return ContractHandler.SendRequestAsync(tossmarketv1InitFunction);
        }

        public Task<TransactionReceipt> Tossmarketv1InitRequestAndWaitForReceiptAsync(Tossmarketv1InitFunction tossmarketv1InitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tossmarketv1InitFunction, cancellationToken);
        }

        public Task<string> Tossmarketv1InitRequestAsync(string erc20, ushort marketcut)
        {
            var tossmarketv1InitFunction = new Tossmarketv1InitFunction();
                tossmarketv1InitFunction.Erc20 = erc20;
                tossmarketv1InitFunction.Marketcut = marketcut;
            
             return ContractHandler.SendRequestAsync(tossmarketv1InitFunction);
        }

        public Task<TransactionReceipt> Tossmarketv1InitRequestAndWaitForReceiptAsync(string erc20, ushort marketcut, CancellationTokenSource cancellationToken = null)
        {
            var tossmarketv1InitFunction = new Tossmarketv1InitFunction();
                tossmarketv1InitFunction.Erc20 = erc20;
                tossmarketv1InitFunction.Marketcut = marketcut;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tossmarketv1InitFunction, cancellationToken);
        }

        public Task<string> BuyRequestAsync(BuyFunction buyFunction)
        {
             return ContractHandler.SendRequestAsync(buyFunction);
        }

        public Task<TransactionReceipt> BuyRequestAndWaitForReceiptAsync(BuyFunction buyFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(buyFunction, cancellationToken);
        }

        public Task<string> BuyRequestAsync(string erc721Address, BigInteger tokenId, BigInteger price)
        {
            var buyFunction = new BuyFunction();
                buyFunction.Erc721Address = erc721Address;
                buyFunction.TokenId = tokenId;
                buyFunction.Price = price;
            
             return ContractHandler.SendRequestAsync(buyFunction);
        }

        public Task<TransactionReceipt> BuyRequestAndWaitForReceiptAsync(string erc721Address, BigInteger tokenId, BigInteger price, CancellationTokenSource cancellationToken = null)
        {
            var buyFunction = new BuyFunction();
                buyFunction.Erc721Address = erc721Address;
                buyFunction.TokenId = tokenId;
                buyFunction.Price = price;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(buyFunction, cancellationToken);
        }

        public Task<string> BuyWithPermitRequestAsync(BuyWithPermitFunction buyWithPermitFunction)
        {
             return ContractHandler.SendRequestAsync(buyWithPermitFunction);
        }

        public Task<TransactionReceipt> BuyWithPermitRequestAndWaitForReceiptAsync(BuyWithPermitFunction buyWithPermitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(buyWithPermitFunction, cancellationToken);
        }

        public Task<string> BuyWithPermitRequestAsync(string erc721Address, BigInteger tokenId, BigInteger price, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s)
        {
            var buyWithPermitFunction = new BuyWithPermitFunction();
                buyWithPermitFunction.Erc721Address = erc721Address;
                buyWithPermitFunction.TokenId = tokenId;
                buyWithPermitFunction.Price = price;
                buyWithPermitFunction.Amount = amount;
                buyWithPermitFunction.Deadline = deadline;
                buyWithPermitFunction.V = v;
                buyWithPermitFunction.R = r;
                buyWithPermitFunction.S = s;
            
             return ContractHandler.SendRequestAsync(buyWithPermitFunction);
        }

        public Task<TransactionReceipt> BuyWithPermitRequestAndWaitForReceiptAsync(string erc721Address, BigInteger tokenId, BigInteger price, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null)
        {
            var buyWithPermitFunction = new BuyWithPermitFunction();
                buyWithPermitFunction.Erc721Address = erc721Address;
                buyWithPermitFunction.TokenId = tokenId;
                buyWithPermitFunction.Price = price;
                buyWithPermitFunction.Amount = amount;
                buyWithPermitFunction.Deadline = deadline;
                buyWithPermitFunction.V = v;
                buyWithPermitFunction.R = r;
                buyWithPermitFunction.S = s;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(buyWithPermitFunction, cancellationToken);
        }

        public Task<string> CancelRequestAsync(CancelFunction cancelFunction)
        {
             return ContractHandler.SendRequestAsync(cancelFunction);
        }

        public Task<TransactionReceipt> CancelRequestAndWaitForReceiptAsync(CancelFunction cancelFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelFunction, cancellationToken);
        }

        public Task<string> CancelRequestAsync(string erc721Address, BigInteger tokenId)
        {
            var cancelFunction = new CancelFunction();
                cancelFunction.Erc721Address = erc721Address;
                cancelFunction.TokenId = tokenId;
            
             return ContractHandler.SendRequestAsync(cancelFunction);
        }

        public Task<TransactionReceipt> CancelRequestAndWaitForReceiptAsync(string erc721Address, BigInteger tokenId, CancellationTokenSource cancellationToken = null)
        {
            var cancelFunction = new CancelFunction();
                cancelFunction.Erc721Address = erc721Address;
                cancelFunction.TokenId = tokenId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelFunction, cancellationToken);
        }

        public Task<string> CancelWhenPausedRequestAsync(CancelWhenPausedFunction cancelWhenPausedFunction)
        {
             return ContractHandler.SendRequestAsync(cancelWhenPausedFunction);
        }

        public Task<TransactionReceipt> CancelWhenPausedRequestAndWaitForReceiptAsync(CancelWhenPausedFunction cancelWhenPausedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelWhenPausedFunction, cancellationToken);
        }

        public Task<string> CancelWhenPausedRequestAsync(string erc721Address, BigInteger tokenId)
        {
            var cancelWhenPausedFunction = new CancelWhenPausedFunction();
                cancelWhenPausedFunction.Erc721Address = erc721Address;
                cancelWhenPausedFunction.TokenId = tokenId;
            
             return ContractHandler.SendRequestAsync(cancelWhenPausedFunction);
        }

        public Task<TransactionReceipt> CancelWhenPausedRequestAndWaitForReceiptAsync(string erc721Address, BigInteger tokenId, CancellationTokenSource cancellationToken = null)
        {
            var cancelWhenPausedFunction = new CancelWhenPausedFunction();
                cancelWhenPausedFunction.Erc721Address = erc721Address;
                cancelWhenPausedFunction.TokenId = tokenId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelWhenPausedFunction, cancellationToken);
        }

        public Task<string> CreateSellOfferRequestAsync(CreateSellOfferFunction createSellOfferFunction)
        {
             return ContractHandler.SendRequestAsync(createSellOfferFunction);
        }

        public Task<TransactionReceipt> CreateSellOfferRequestAndWaitForReceiptAsync(CreateSellOfferFunction createSellOfferFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createSellOfferFunction, cancellationToken);
        }

        public Task<string> CreateSellOfferRequestAsync(BigInteger tokenId, BigInteger price, string owner)
        {
            var createSellOfferFunction = new CreateSellOfferFunction();
                createSellOfferFunction.TokenId = tokenId;
                createSellOfferFunction.Price = price;
                createSellOfferFunction.Owner = owner;
            
             return ContractHandler.SendRequestAsync(createSellOfferFunction);
        }

        public Task<TransactionReceipt> CreateSellOfferRequestAndWaitForReceiptAsync(BigInteger tokenId, BigInteger price, string owner, CancellationTokenSource cancellationToken = null)
        {
            var createSellOfferFunction = new CreateSellOfferFunction();
                createSellOfferFunction.TokenId = tokenId;
                createSellOfferFunction.Price = price;
                createSellOfferFunction.Owner = owner;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createSellOfferFunction, cancellationToken);
        }

        public Task<GetOutputDTO> GetQueryAsync(GetFunction getFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetFunction, GetOutputDTO>(getFunction, blockParameter);
        }

        public Task<GetOutputDTO> GetQueryAsync(string erc721Address, BigInteger tokenId, BlockParameter blockParameter = null)
        {
            var getFunction = new GetFunction();
                getFunction.Erc721Address = erc721Address;
                getFunction.TokenId = tokenId;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetFunction, GetOutputDTO>(getFunction, blockParameter);
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

        public Task<string> GetImplementationQueryAsync(GetImplementationFunction getImplementationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetImplementationFunction, string>(getImplementationFunction, blockParameter);
        }

        
        public Task<string> GetImplementationQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetImplementationFunction, string>(null, blockParameter);
        }

        public Task<ushort> GetMarketCutQueryAsync(GetMarketCutFunction getMarketCutFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMarketCutFunction, ushort>(getMarketCutFunction, blockParameter);
        }

        
        public Task<ushort> GetMarketCutQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMarketCutFunction, ushort>(null, blockParameter);
        }

        public Task<BigInteger> GetPriceQueryAsync(GetPriceFunction getPriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetPriceFunction, BigInteger>(getPriceFunction, blockParameter);
        }

        
        public Task<BigInteger> GetPriceQueryAsync(string erc721Address, BigInteger tokenId, BlockParameter blockParameter = null)
        {
            var getPriceFunction = new GetPriceFunction();
                getPriceFunction.Erc721Address = erc721Address;
                getPriceFunction.TokenId = tokenId;
            
            return ContractHandler.QueryAsync<GetPriceFunction, BigInteger>(getPriceFunction, blockParameter);
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

        public Task<byte[]> OnERC721ReceivedQueryAsync(OnERC721ReceivedFunction onERC721ReceivedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OnERC721ReceivedFunction, byte[]>(onERC721ReceivedFunction, blockParameter);
        }

        
        public Task<byte[]> OnERC721ReceivedQueryAsync(string returnValue1, string returnValue2, BigInteger returnValue3, byte[] returnValue4, BlockParameter blockParameter = null)
        {
            var onERC721ReceivedFunction = new OnERC721ReceivedFunction();
                onERC721ReceivedFunction.ReturnValue1 = returnValue1;
                onERC721ReceivedFunction.ReturnValue2 = returnValue2;
                onERC721ReceivedFunction.ReturnValue3 = returnValue3;
                onERC721ReceivedFunction.ReturnValue4 = returnValue4;
            
            return ContractHandler.QueryAsync<OnERC721ReceivedFunction, byte[]>(onERC721ReceivedFunction, blockParameter);
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

        public Task<string> SetMarketCutRequestAsync(SetMarketCutFunction setMarketCutFunction)
        {
             return ContractHandler.SendRequestAsync(setMarketCutFunction);
        }

        public Task<TransactionReceipt> SetMarketCutRequestAndWaitForReceiptAsync(SetMarketCutFunction setMarketCutFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMarketCutFunction, cancellationToken);
        }

        public Task<string> SetMarketCutRequestAsync(ushort cut)
        {
            var setMarketCutFunction = new SetMarketCutFunction();
                setMarketCutFunction.Cut = cut;
            
             return ContractHandler.SendRequestAsync(setMarketCutFunction);
        }

        public Task<TransactionReceipt> SetMarketCutRequestAndWaitForReceiptAsync(ushort cut, CancellationTokenSource cancellationToken = null)
        {
            var setMarketCutFunction = new SetMarketCutFunction();
                setMarketCutFunction.Cut = cut;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMarketCutFunction, cancellationToken);
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
