using Toss.Common;

namespace Toss.Contracts.Interfaces;

public interface IContractDefinition {
	static abstract ContractTypes ContractType { get; }
	static abstract string ContractName { get; }
	static abstract string SourceCode { get; }
	static abstract string CompileVersion { get; }
	static abstract int Version { get; }
	static abstract int ClientVersion { get; }

	static string LoadSourceCode(string name) {
		Stream stream = typeof(IContractDefinition).Assembly.GetManifestResourceStream($"Toss.Contracts.Flatten.{name}.sol");
		StreamReader sr = new(stream!);
		return sr.ReadToEnd();
	}
}