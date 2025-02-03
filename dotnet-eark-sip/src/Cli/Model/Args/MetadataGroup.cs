using Mono.Options;

public class MetadataGroup
{
	public Metadata Metadata { get; set; }

  public MetadataGroup() {
    Metadata = new Metadata();
  }

	public MetadataGroup(Metadata metadata) {
		Metadata = metadata;
	}

	public void ParseArguments(string[] args) {
		bool showHelp = false;

		var options = new OptionSet {
			{ "metadata-group", "Start a new metadata group", x => {} },
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
			Console.WriteLine("Usage: app --metadata-group --metadata-file <file1> --metadata-type <type> --metadata-version <version> --metadata-schema <schema>");
			options.WriteOptionDescriptions(Console.Out);
			return;
		}
	}

	public void PrintDetails() {
    Console.WriteLine("Metadata group:");
    Metadata.PrintDetails();
	}
}
