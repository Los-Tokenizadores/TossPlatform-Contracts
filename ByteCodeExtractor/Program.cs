using System.Text.Json;

string bytecodePath = $"Toss.Contracts/ByteCode/{args[0]}";
#if DEBUG
bytecodePath = $"../../../../{bytecodePath}";
#endif

string[] dir = Directory.GetDirectories(bytecodePath, "*", SearchOption.TopDirectoryOnly);
foreach (string dirName in dir) {
	string name = dirName.Replace('\\', '/');
	if (name.Contains($"{bytecodePath}/Toss")
		&& !name.Contains("Test")
		&& !name.Contains("UUPS")
		&& !name.Contains("Base")
		&& !name.Contains("Client")) {
		continue;
	}
	Directory.Delete(dirName, true);
}

JsonSerializerOptions jsonOptions = new() {
	PropertyNameCaseInsensitive = true,
};

dir = Directory.GetFiles(bytecodePath, "Toss*.json", SearchOption.AllDirectories);

foreach (string fileName in dir) {
	string name = fileName.Replace('\\', '/');
	FileStream inputStream = File.Open(name, FileMode.Open);
	Abi? abi = JsonSerializer.Deserialize<Abi>(inputStream, jsonOptions);
	if (abi is null) {
		Console.WriteLine($"Abi is null for File: {name}");
		inputStream.Dispose();
		continue;
	}
	inputStream.Dispose();
	FileStream outputStream = File.Create($"{bytecodePath}/{Path.GetFileNameWithoutExtension(name)}.json");
	JsonSerializer.Serialize(outputStream, new ByteCodeVersion(
		abi.Metadata.Settings.EvmVersion,
		$"v{abi.Metadata.Compiler.Version}",
		abi.Metadata.Settings.Optimizer.Enabled,
		abi.Metadata.Settings.Optimizer.Runs,
		abi.Bytecode.Object[2..])
	);
	outputStream.Dispose();
	string parentDir = Directory.GetParent(name)!.FullName;
	Directory.Delete(parentDir, true);
}


internal record Abi(ByteCode Bytecode, Metadata Metadata);

internal record ByteCode(string Object);

internal record Metadata(Compiler Compiler, Settings Settings);

internal record Compiler(string Version);

internal record Settings(string EvmVersion, Optimizer Optimizer);

internal record Optimizer(bool Enabled, int Runs);

internal record ByteCodeVersion(string EvmVersion, string CompilerVersion, bool Optimizer, int OptimizedRuns, string Bytecode);