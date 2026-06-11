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
    public partial class TossMarketV1Service: TossMarketV1ServiceBase
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

        public TossMarketV1Service(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
        {
        }

    }


    public partial class TossMarketV1ServiceBase: ContractWeb3ServiceBase
    {

        public TossMarketV1ServiceBase(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
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

        public Task<byte> MaxRoyaltyLengthQueryAsync(MaxRoyaltyLengthFunction maxRoyaltyLengthFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MaxRoyaltyLengthFunction, byte>(maxRoyaltyLengthFunction, blockParameter);
        }

        
        public virtual Task<byte> MaxRoyaltyLengthQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MaxRoyaltyLengthFunction, byte>(null, blockParameter);
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

        public virtual Task<string> Tossmarketv1InitRequestAsync(Tossmarketv1InitFunction tossmarketv1InitFunction)
        {
             return ContractHandler.SendRequestAsync(tossmarketv1InitFunction);
        }

        public virtual Task<TransactionReceipt> Tossmarketv1InitRequestAndWaitForReceiptAsync(Tossmarketv1InitFunction tossmarketv1InitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tossmarketv1InitFunction, cancellationToken);
        }

        public virtual Task<string> Tossmarketv1InitRequestAsync(string erc20, ushort marketcut, string bankaddress)
        {
            var tossmarketv1InitFunction = new Tossmarketv1InitFunction();
                tossmarketv1InitFunction.Erc20 = erc20;
                tossmarketv1InitFunction.Marketcut = marketcut;
                tossmarketv1InitFunction.Bankaddress = bankaddress;
            
             return ContractHandler.SendRequestAsync(tossmarketv1InitFunction);
        }

        public virtual Task<TransactionReceipt> Tossmarketv1InitRequestAndWaitForReceiptAsync(string erc20, ushort marketcut, string bankaddress, CancellationTokenSource cancellationToken = null)
        {
            var tossmarketv1InitFunction = new Tossmarketv1InitFunction();
                tossmarketv1InitFunction.Erc20 = erc20;
                tossmarketv1InitFunction.Marketcut = marketcut;
                tossmarketv1InitFunction.Bankaddress = bankaddress;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(tossmarketv1InitFunction, cancellationToken);
        }

        public virtual Task<string> AddErc721MarketRequestAsync(AddErc721MarketFunction addErc721MarketFunction)
        {
             return ContractHandler.SendRequestAsync(addErc721MarketFunction);
        }

        public virtual Task<TransactionReceipt> AddErc721MarketRequestAndWaitForReceiptAsync(AddErc721MarketFunction addErc721MarketFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addErc721MarketFunction, cancellationToken);
        }

        public virtual Task<string> AddErc721MarketRequestAsync(string erc721Address, List<Royalty> royalties)
        {
            var addErc721MarketFunction = new AddErc721MarketFunction();
                addErc721MarketFunction.Erc721Address = erc721Address;
                addErc721MarketFunction.Royalties = royalties;
            
             return ContractHandler.SendRequestAsync(addErc721MarketFunction);
        }

        public virtual Task<TransactionReceipt> AddErc721MarketRequestAndWaitForReceiptAsync(string erc721Address, List<Royalty> royalties, CancellationTokenSource cancellationToken = null)
        {
            var addErc721MarketFunction = new AddErc721MarketFunction();
                addErc721MarketFunction.Erc721Address = erc721Address;
                addErc721MarketFunction.Royalties = royalties;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addErc721MarketFunction, cancellationToken);
        }

        public virtual Task<string> BuyRequestAsync(BuyFunction buyFunction)
        {
             return ContractHandler.SendRequestAsync(buyFunction);
        }

        public virtual Task<TransactionReceipt> BuyRequestAndWaitForReceiptAsync(BuyFunction buyFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(buyFunction, cancellationToken);
        }

        public virtual Task<string> BuyRequestAsync(string erc721Address, BigInteger tokenId, BigInteger price)
        {
            var buyFunction = new BuyFunction();
                buyFunction.Erc721Address = erc721Address;
                buyFunction.TokenId = tokenId;
                buyFunction.Price = price;
            
             return ContractHandler.SendRequestAsync(buyFunction);
        }

        public virtual Task<TransactionReceipt> BuyRequestAndWaitForReceiptAsync(string erc721Address, BigInteger tokenId, BigInteger price, CancellationTokenSource cancellationToken = null)
        {
            var buyFunction = new BuyFunction();
                buyFunction.Erc721Address = erc721Address;
                buyFunction.TokenId = tokenId;
                buyFunction.Price = price;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(buyFunction, cancellationToken);
        }

        public virtual Task<string> BuyWithPermitRequestAsync(BuyWithPermitFunction buyWithPermitFunction)
        {
             return ContractHandler.SendRequestAsync(buyWithPermitFunction);
        }

        public virtual Task<TransactionReceipt> BuyWithPermitRequestAndWaitForReceiptAsync(BuyWithPermitFunction buyWithPermitFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(buyWithPermitFunction, cancellationToken);
        }

        public virtual Task<string> BuyWithPermitRequestAsync(string erc721Address, BigInteger tokenId, BigInteger price, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s)
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

        public virtual Task<TransactionReceipt> BuyWithPermitRequestAndWaitForReceiptAsync(string erc721Address, BigInteger tokenId, BigInteger price, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null)
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

        public virtual Task<string> CancelRequestAsync(CancelFunction cancelFunction)
        {
             return ContractHandler.SendRequestAsync(cancelFunction);
        }

        public virtual Task<TransactionReceipt> CancelRequestAndWaitForReceiptAsync(CancelFunction cancelFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelFunction, cancellationToken);
        }

        public virtual Task<string> CancelRequestAsync(string erc721Address, BigInteger tokenId)
        {
            var cancelFunction = new CancelFunction();
                cancelFunction.Erc721Address = erc721Address;
                cancelFunction.TokenId = tokenId;
            
             return ContractHandler.SendRequestAsync(cancelFunction);
        }

        public virtual Task<TransactionReceipt> CancelRequestAndWaitForReceiptAsync(string erc721Address, BigInteger tokenId, CancellationTokenSource cancellationToken = null)
        {
            var cancelFunction = new CancelFunction();
                cancelFunction.Erc721Address = erc721Address;
                cancelFunction.TokenId = tokenId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelFunction, cancellationToken);
        }

        public virtual Task<string> CancelWhenPausedRequestAsync(CancelWhenPausedFunction cancelWhenPausedFunction)
        {
             return ContractHandler.SendRequestAsync(cancelWhenPausedFunction);
        }

        public virtual Task<TransactionReceipt> CancelWhenPausedRequestAndWaitForReceiptAsync(CancelWhenPausedFunction cancelWhenPausedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelWhenPausedFunction, cancellationToken);
        }

        public virtual Task<string> CancelWhenPausedRequestAsync(string erc721Address, BigInteger tokenId)
        {
            var cancelWhenPausedFunction = new CancelWhenPausedFunction();
                cancelWhenPausedFunction.Erc721Address = erc721Address;
                cancelWhenPausedFunction.TokenId = tokenId;
            
             return ContractHandler.SendRequestAsync(cancelWhenPausedFunction);
        }

        public virtual Task<TransactionReceipt> CancelWhenPausedRequestAndWaitForReceiptAsync(string erc721Address, BigInteger tokenId, CancellationTokenSource cancellationToken = null)
        {
            var cancelWhenPausedFunction = new CancelWhenPausedFunction();
                cancelWhenPausedFunction.Erc721Address = erc721Address;
                cancelWhenPausedFunction.TokenId = tokenId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelWhenPausedFunction, cancellationToken);
        }

        public virtual Task<string> CreateSellOfferRequestAsync(CreateSellOfferFunction createSellOfferFunction)
        {
             return ContractHandler.SendRequestAsync(createSellOfferFunction);
        }

        public virtual Task<TransactionReceipt> CreateSellOfferRequestAndWaitForReceiptAsync(CreateSellOfferFunction createSellOfferFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createSellOfferFunction, cancellationToken);
        }

        public virtual Task<string> CreateSellOfferRequestAsync(BigInteger tokenId, BigInteger price, string owner)
        {
            var createSellOfferFunction = new CreateSellOfferFunction();
                createSellOfferFunction.TokenId = tokenId;
                createSellOfferFunction.Price = price;
                createSellOfferFunction.Owner = owner;
            
             return ContractHandler.SendRequestAsync(createSellOfferFunction);
        }

        public virtual Task<TransactionReceipt> CreateSellOfferRequestAndWaitForReceiptAsync(BigInteger tokenId, BigInteger price, string owner, CancellationTokenSource cancellationToken = null)
        {
            var createSellOfferFunction = new CreateSellOfferFunction();
                createSellOfferFunction.TokenId = tokenId;
                createSellOfferFunction.Price = price;
                createSellOfferFunction.Owner = owner;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createSellOfferFunction, cancellationToken);
        }

        public virtual Task<GetOutputDTO> GetQueryAsync(GetFunction getFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetFunction, GetOutputDTO>(getFunction, blockParameter);
        }

        public virtual Task<GetOutputDTO> GetQueryAsync(string erc721Address, BigInteger tokenId, BlockParameter blockParameter = null)
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

        
        public virtual Task<string> GetErc20QueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc20Function, string>(null, blockParameter);
        }

        public Task<string> GetErc20BankAddressQueryAsync(GetErc20BankAddressFunction getErc20BankAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc20BankAddressFunction, string>(getErc20BankAddressFunction, blockParameter);
        }

        
        public virtual Task<string> GetErc20BankAddressQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetErc20BankAddressFunction, string>(null, blockParameter);
        }

        public virtual Task<GetErc721MarketOutputDTO> GetErc721MarketQueryAsync(GetErc721MarketFunction getErc721MarketFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetErc721MarketFunction, GetErc721MarketOutputDTO>(getErc721MarketFunction, blockParameter);
        }

        public virtual Task<GetErc721MarketOutputDTO> GetErc721MarketQueryAsync(string erc721Address, BlockParameter blockParameter = null)
        {
            var getErc721MarketFunction = new GetErc721MarketFunction();
                getErc721MarketFunction.Erc721Address = erc721Address;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetErc721MarketFunction, GetErc721MarketOutputDTO>(getErc721MarketFunction, blockParameter);
        }

        public Task<string> GetImplementationQueryAsync(GetImplementationFunction getImplementationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetImplementationFunction, string>(getImplementationFunction, blockParameter);
        }

        
        public virtual Task<string> GetImplementationQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetImplementationFunction, string>(null, blockParameter);
        }

        public Task<ushort> GetMarketCutQueryAsync(GetMarketCutFunction getMarketCutFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMarketCutFunction, ushort>(getMarketCutFunction, blockParameter);
        }

        
        public virtual Task<ushort> GetMarketCutQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMarketCutFunction, ushort>(null, blockParameter);
        }

        public Task<BigInteger> GetPriceQueryAsync(GetPriceFunction getPriceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetPriceFunction, BigInteger>(getPriceFunction, blockParameter);
        }

        
        public virtual Task<BigInteger> GetPriceQueryAsync(string erc721Address, BigInteger tokenId, BlockParameter blockParameter = null)
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

        public Task<byte[]> OnERC721ReceivedQueryAsync(OnERC721ReceivedFunction onERC721ReceivedFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OnERC721ReceivedFunction, byte[]>(onERC721ReceivedFunction, blockParameter);
        }

        
        public virtual Task<byte[]> OnERC721ReceivedQueryAsync(string returnValue1, string returnValue2, BigInteger returnValue3, byte[] returnValue4, BlockParameter blockParameter = null)
        {
            var onERC721ReceivedFunction = new OnERC721ReceivedFunction();
                onERC721ReceivedFunction.ReturnValue1 = returnValue1;
                onERC721ReceivedFunction.ReturnValue2 = returnValue2;
                onERC721ReceivedFunction.ReturnValue3 = returnValue3;
                onERC721ReceivedFunction.ReturnValue4 = returnValue4;
            
            return ContractHandler.QueryAsync<OnERC721ReceivedFunction, byte[]>(onERC721ReceivedFunction, blockParameter);
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

        public virtual Task<string> RemoveErc721MarketRequestAsync(RemoveErc721MarketFunction removeErc721MarketFunction)
        {
             return ContractHandler.SendRequestAsync(removeErc721MarketFunction);
        }

        public virtual Task<TransactionReceipt> RemoveErc721MarketRequestAndWaitForReceiptAsync(RemoveErc721MarketFunction removeErc721MarketFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(removeErc721MarketFunction, cancellationToken);
        }

        public virtual Task<string> RemoveErc721MarketRequestAsync(string erc721Address)
        {
            var removeErc721MarketFunction = new RemoveErc721MarketFunction();
                removeErc721MarketFunction.Erc721Address = erc721Address;
            
             return ContractHandler.SendRequestAsync(removeErc721MarketFunction);
        }

        public virtual Task<TransactionReceipt> RemoveErc721MarketRequestAndWaitForReceiptAsync(string erc721Address, CancellationTokenSource cancellationToken = null)
        {
            var removeErc721MarketFunction = new RemoveErc721MarketFunction();
                removeErc721MarketFunction.Erc721Address = erc721Address;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(removeErc721MarketFunction, cancellationToken);
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

        public virtual Task<string> SetErc20BankAddressRequestAsync(SetErc20BankAddressFunction setErc20BankAddressFunction)
        {
             return ContractHandler.SendRequestAsync(setErc20BankAddressFunction);
        }

        public virtual Task<TransactionReceipt> SetErc20BankAddressRequestAndWaitForReceiptAsync(SetErc20BankAddressFunction setErc20BankAddressFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setErc20BankAddressFunction, cancellationToken);
        }

        public virtual Task<string> SetErc20BankAddressRequestAsync(string newAddress)
        {
            var setErc20BankAddressFunction = new SetErc20BankAddressFunction();
                setErc20BankAddressFunction.NewAddress = newAddress;
            
             return ContractHandler.SendRequestAsync(setErc20BankAddressFunction);
        }

        public virtual Task<TransactionReceipt> SetErc20BankAddressRequestAndWaitForReceiptAsync(string newAddress, CancellationTokenSource cancellationToken = null)
        {
            var setErc20BankAddressFunction = new SetErc20BankAddressFunction();
                setErc20BankAddressFunction.NewAddress = newAddress;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setErc20BankAddressFunction, cancellationToken);
        }

        public virtual Task<string> SetMarketCutRequestAsync(SetMarketCutFunction setMarketCutFunction)
        {
             return ContractHandler.SendRequestAsync(setMarketCutFunction);
        }

        public virtual Task<TransactionReceipt> SetMarketCutRequestAndWaitForReceiptAsync(SetMarketCutFunction setMarketCutFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMarketCutFunction, cancellationToken);
        }

        public virtual Task<string> SetMarketCutRequestAsync(ushort cut)
        {
            var setMarketCutFunction = new SetMarketCutFunction();
                setMarketCutFunction.Cut = cut;
            
             return ContractHandler.SendRequestAsync(setMarketCutFunction);
        }

        public virtual Task<TransactionReceipt> SetMarketCutRequestAndWaitForReceiptAsync(ushort cut, CancellationTokenSource cancellationToken = null)
        {
            var setMarketCutFunction = new SetMarketCutFunction();
                setMarketCutFunction.Cut = cut;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setMarketCutFunction, cancellationToken);
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
                typeof(MaxRoyaltyLengthFunction),
                typeof(PauserRoleFunction),
                typeof(UpgraderRoleFunction),
                typeof(UpgradeInterfaceVersionFunction),
                typeof(Tossmarketv1InitFunction),
                typeof(AddErc721MarketFunction),
                typeof(BuyFunction),
                typeof(BuyWithPermitFunction),
                typeof(CancelFunction),
                typeof(CancelWhenPausedFunction),
                typeof(CreateSellOfferFunction),
                typeof(GetFunction),
                typeof(GetErc20Function),
                typeof(GetErc20BankAddressFunction),
                typeof(GetErc721MarketFunction),
                typeof(GetImplementationFunction),
                typeof(GetMarketCutFunction),
                typeof(GetPriceFunction),
                typeof(GetRoleAdminFunction),
                typeof(GetWhitelistFunction),
                typeof(GrantRoleFunction),
                typeof(HasRoleFunction),
                typeof(OnERC721ReceivedFunction),
                typeof(PauseFunction),
                typeof(PausedFunction),
                typeof(ProxiableUUIDFunction),
                typeof(RemoveErc721MarketFunction),
                typeof(RenounceRoleFunction),
                typeof(RevokeRoleFunction),
                typeof(SetErc20BankAddressFunction),
                typeof(SetMarketCutFunction),
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
                typeof(RoleAdminChangedEventDTO),
                typeof(RoleGrantedEventDTO),
                typeof(RoleRevokedEventDTO),
                typeof(SellOfferCancelledEventDTO),
                typeof(SellOfferCreatedEventDTO),
                typeof(SellOfferSoldEventDTO),
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
                typeof(TossMarketErc721AlreadyActiveError),
                typeof(TossMarketErc721NotActiveError),
                typeof(TossMarketErc721NotOnSellError),
                typeof(TossMarketIsOwnerOfErc721Error),
                typeof(TossMarketNotOwnerOfErc721Error),
                typeof(TossMarketRoyaltyCutOutOfRangeError),
                typeof(TossMarketRoyaltyLengthOutOfRangeError),
                typeof(TossMarketSellPriceChangeError),
                typeof(TossUnsupportedInterfaceError),
                typeof(TossWhitelistNotInWhitelistError),
                typeof(UUPSUnauthorizedCallContextError),
                typeof(UUPSUnsupportedProxiableUUIDError)
            };
        }
    }
}
