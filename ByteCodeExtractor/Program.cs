using System.Text.Json;

string bytecodePath = $"Toss.Contracts/ByteCode/{args[0]}";
#if DEBUG
bytecodePath = $"../../../../{bytecodePath}";
#endif

string[] dir = Directory.GetDirectories(bytecodePath, "*", SearchOption.AllDirectories);
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

dir = Directory.GetFiles(bytecodePath, "*.json", SearchOption.AllDirectories);

foreach (string fileName in dir) {
	string name = fileName.Replace('\\', '/');
	Abi? abi = JsonSerializer.Deserialize<Abi>(File.Open(name, FileMode.Open), jsonOptions);
	if (abi is null) {
		Console.WriteLine($"Abi is null for File: {name}");
		continue;
	}
	FileStream file = File.Create($"{bytecodePath}/{Path.GetFileNameWithoutExtension(name)}.json");
	JsonSerializer.Serialize(file, new ByteCodeVersion(
		abi.Metadata.Settings.EvmVersion,
		$"v{abi.Metadata.Compiler.Version}",
		abi.Metadata.Settings.Optimizer.Enabled,
		abi.Metadata.Settings.Optimizer.Runs,
		abi.Bytecode.Object[2..])
	);
	file.Close();
	string parentDir = Directory.GetParent(name)!.FullName;
	Directory.Delete(parentDir, true);
}


record Abi(ByteCode Bytecode, Metadata Metadata);
record ByteCode(string Object);
record Metadata(Compiler Compiler, Settings Settings);
record Compiler(string Version);
record Settings(string EvmVersion, Optimizer Optimizer);
record Optimizer(bool Enabled, int Runs);
record ByteCodeVersion(string EvmVersion, string CompilerVersion, bool Optimizer, int OptimizedRuns, string Bytecode);
