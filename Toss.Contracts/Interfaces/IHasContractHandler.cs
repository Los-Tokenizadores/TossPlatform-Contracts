using Nethereum.Contracts.ContractHandlers;

namespace Toss.Contracts.Interfaces;

public interface IHasContractHandler {
	ContractHandler ContractHandler { get; }
	string Address { get; }
}
