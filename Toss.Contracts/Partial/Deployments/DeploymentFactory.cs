using System.Text.Json;
using Toss.Contracts.Interfaces;

namespace Toss.Contracts.Partial.Deployments;
public record ByteCodeVersion(string EvmVersion, string CompilerVersion, bool Optimizer, int OptimizedRuns, string Bytecode);
public static class DeploymentFactory {
	private static readonly Dictionary<string, Dictionary<string, ByteCodeVersion>> byteCodeVersionsByEvmAndContract = [];

	public static TDeployment Get<TDeployment>(string evmVersion) where TDeployment : IContractDefinition {
		ByteCodeVersion byteCodeVersion = GetByteCode<TDeployment>(evmVersion);
		TDeployment deployment = (TDeployment)Activator.CreateInstance(typeof(TDeployment), byteCodeVersion.Bytecode);
		return deployment;
	}

	public static ByteCodeVersion GetByteCode<TDeployment>(string evmVersion) where TDeployment : IContractDefinition {
		if (!byteCodeVersionsByEvmAndContract.TryGetValue(evmVersion, out Dictionary<string, ByteCodeVersion> contracts)) {
			contracts = [];
			byteCodeVersionsByEvmAndContract.Add(evmVersion, contracts);
		}

		if (!contracts.TryGetValue(TDeployment.ContractName, out ByteCodeVersion byteCodeVersion)) {
			byteCodeVersion = LoadCode(TDeployment.ContractName, evmVersion);
			contracts.Add(TDeployment.ContractName, byteCodeVersion);
		}

		return byteCodeVersion;
	}

	private static ByteCodeVersion LoadCode(string name, string evmVersion) {
		using Stream stream = typeof(IContractDefinition).Assembly.GetManifestResourceStream($"Toss.Contracts.ByteCode.{evmVersion}.{name}.json");
		return JsonSerializer.Deserialize<ByteCodeVersion>(stream);
	}
}
