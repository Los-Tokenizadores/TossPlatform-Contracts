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
    public partial class TossUpgradeableProxyService
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

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public TossUpgradeableProxyService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public TossUpgradeableProxyService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }


    }
}
