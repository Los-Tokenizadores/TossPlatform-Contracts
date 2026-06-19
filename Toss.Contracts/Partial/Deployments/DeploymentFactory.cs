using System.Collections.Concurrent;
using System.Text.Json;
using Toss.Contracts.Interfaces;

namespace Toss.Contracts.Deployments;

public record ByteCodeVersion(string EvmVersion, string CompilerVersion, bool Optimizer, int OptimizedRuns, string Bytecode);

public static class DeploymentFactory {
	private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, ByteCodeVersion>> byteCodeVersionsByEvmAndContract = [];

	public static TDeployment Get<TDeployment>(string evmVersion) where TDeployment : IContractDefinition {
		ByteCodeVersion byteCodeVersion = GetByteCode<TDeployment>(evmVersion);
		TDeployment deployment = (TDeployment)Activator.CreateInstance(typeof(TDeployment), byteCodeVersion.Bytecode);
		return deployment;
	}

	public static ByteCodeVersion GetByteCode<TDeployment>(string evmVersion) where TDeployment : IContractDefinition {
		ConcurrentDictionary<string, ByteCodeVersion> contracts = byteCodeVersionsByEvmAndContract.GetOrAdd(evmVersion, []);
		return contracts.GetOrAdd(TDeployment.ContractName, LoadCode(TDeployment.ContractName, evmVersion));
	}

	private static ByteCodeVersion LoadCode(string name, string evmVersion) {
		using Stream stream = typeof(IContractDefinition).Assembly.GetManifestResourceStream($"Toss.Contracts.ByteCode.{evmVersion}.{name}.json");
		return JsonSerializer.Deserialize<ByteCodeVersion>(stream);
	}
}