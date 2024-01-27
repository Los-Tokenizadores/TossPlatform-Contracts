using Nethereum.RPC.Eth.DTOs;
using System.Numerics;

namespace Toss.Contracts.Interfaces;

public interface IExchangeService : IHasContractHandler, IHasWhitelist {
	Task<TransactionReceipt> WithdrawRequestAndWaitForReceiptAsync(BigInteger internalAmount, CancellationTokenSource cancellationToken = null);
	Task<TransactionReceipt> WithdrawWithPermitRequestAndWaitForReceiptAsync(BigInteger internalAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null);
	Task<TransactionReceipt> DepositRequestAndWaitForReceiptAsync(BigInteger amount, CancellationTokenSource cancellationToken = null);
	Task<TransactionReceipt> DepositWithPermitRequestAndWaitForReceiptAsync(BigInteger externalAmount, BigInteger amount, BigInteger deadline, byte v, byte[] r, byte[] s, CancellationTokenSource cancellationToken = null);
	Task<string> GetExternalErc20QueryAsync(BlockParameter blockParameter = null);
	Task<string> GetInternalErc20QueryAsync(BlockParameter blockParameter = null);
	Task<BigInteger> GetDepositMinAmountQueryAsync(BlockParameter blockParameter = null);
	Task<TransactionReceipt> SetDepositMinAmountRequestAndWaitForReceiptAsync(BigInteger value, CancellationTokenSource cancellationToken = null);
	Task<BigInteger> GetWithdrawMinAmountQueryAsync(BlockParameter blockParameter = null);
	Task<TransactionReceipt> SetWithdrawMinAmountRequestAndWaitForReceiptAsync(BigInteger value, CancellationTokenSource cancellationToken = null);
}