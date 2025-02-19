using CommandLine;

public class Representation
{
	[Option("representation-data", HelpText = "Path to representation files (comma-separated)", Separator = ',')]
	public List<string> RepresentationData { get; set; } = new List<string>();
	[Option("representation-type", HelpText = "Representation type")]
	public string? RepresentationType { get; set; }
	[Option("representation-id", HelpText = "Representation identifier")]
	public string? RepresentationId { get; set; }

	public Representation() {
		RepresentationData = new List<string>();
		RepresentationType = null;
		RepresentationId = null;
	}

	public Representation(List<string> data, string? type = null, string? id = null) : base() {
		RepresentationData.AddRange(data);
		RepresentationType = type;
		RepresentationId = id;
	}

	public void PrintDetails() {
		Console.WriteLine($"Representation Data: [{string.Join(", ", RepresentationData)}]");
		Console.WriteLine($"Representation Type: {RepresentationType ?? "Default Type"}");
		Console.WriteLine($"Representation ID: {RepresentationId ?? "Default ID"}");
	}
}
