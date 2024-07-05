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
using Toss.Contracts.TossUpgradeableProxy.ContractDefinition;

namespace Toss.Contracts.TossUpgradeableProxy
{
    public partial class TossUpgradeableProxyService: ContractWeb3ServiceBase
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.IWeb3 web3, TossUpgradeableProxyDeployment tossUpgradeableProxyDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<TossUpgradeableProxyDeployment>().SendRequestAndWaitForReceiptAsync(tossUpgradeableProxyDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.IWeb3 web3, TossUpgradeableProxyDeployment tossUpgradeableProxyDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<TossUpgradeableProxyDeployment>().SendRequestAsync(tossUpgradeableProxyDeployment);
        }

        public static async Task<TossUpgradeableProxyService> DeployContractAndGetServiceAsync(Nethereum.Web3.IWeb3 web3, TossUpgradeableProxyDeployment tossUpgradeableProxyDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, tossUpgradeableProxyDeployment, cancellationTokenSource);
            return new TossUpgradeableProxyService(web3, receipt.ContractAddress);
        }

        public TossUpgradeableProxyService(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
        {
        }

        public override List<Type> GetAllFunctionTypes()
        {
            return new List<Type>
            {

            };
        }

        public override List<Type> GetAllEventTypes()
        {
            return new List<Type>
            {
                typeof(UpgradedEventDTO)
            };
        }

        public override List<Type> GetAllErrorTypes()
        {
            return new List<Type>
            {
                typeof(AddressEmptyCodeError),
                typeof(ERC1967InvalidImplementationError),
                typeof(ERC1967NonPayableError),
                typeof(FailedInnerCallError)
            };
        }
    }
}
