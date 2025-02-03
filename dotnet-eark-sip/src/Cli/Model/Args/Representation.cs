using Mono.Options;

public class Representation
{
	public List<string> RepresentationData { get; set; }
	public string RepresentationType { get; set; }
	public string RepresentationId { get; set; }

	public Representation() {
		RepresentationData = new List<string>();
		RepresentationType = null;
		RepresentationId = null;
	}

	public Representation(List<string> data, string type = null, string id = null) : base() {
		RepresentationData.AddRange(data);
		RepresentationType = type;
		RepresentationId = id;
	}

	public void ParseArguments(string[] args) {
		bool showHelp = false;

		var options = new OptionSet {
			{ "representation-data=", "Path to representation files (comma-separated)", d => RepresentationData.AddRange(d.Split(',')) },
			{ "representation-type=", "Representation type", t => RepresentationType = t },
			{ "representation-id=", "Representation identifier", id => RepresentationId = id },
			{ "h|help", "Show help message", h => showHelp = h != null }
		};

		try {
			options.Parse(args);
		}
		catch (OptionException e) {
			Console.WriteLine($"Error: {e.Message}");
			Console.WriteLine("Use --help for usage information.");
			return;
		}

		if (showHelp) {
			Console.WriteLine("Usage: app --representation-data <file1,file2> --representation-type <type> --representation-id <id>");
			options.WriteOptionDescriptions(Console.Out);
			return;
		}
	}

	public void PrintDetails() {
		Console.WriteLine($"Representation Data: {string.Join(", ", RepresentationData)}");
		Console.WriteLine($"Representation Type: {RepresentationType}");
		Console.WriteLine($"Representation ID: {RepresentationId ?? "Default ID"}");
	}
}
