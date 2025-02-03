using Mono.Options;

public class RepresentationGroup
{
	public Representation Representation { get; set; }

  public RepresentationGroup() {
    Representation = new Representation();
  }

	public RepresentationGroup(Representation representation) {
		Representation = representation;
	}

	public void ParseArguments(string[] args) {
		bool showHelp = false;

		var options = new OptionSet {
			{ "representation-group", "Start a new representation group", x => {} },
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
			Console.WriteLine("Usage: app --representation-group --representation-data <file1,file2> --representation-type <type> --representation-id <id>");
			options.WriteOptionDescriptions(Console.Out);
			return;
		}
	}

	public void PrintDetails() {
    Console.WriteLine("Representation group:");
    Representation.PrintDetails();
	}
}
