public class Representation
{
	public List<string> RepresentationData { get; set; }
	public string? RepresentationType { get; set; }
	public string? RepresentationId { get; set; }

	public Representation() {
		RepresentationData = new List<string>();
		RepresentationType = null;
		RepresentationId = null;
	}

	public Representation(List<string> data, string? type = null, string? id = null) : base() {
		RepresentationData = new List<string>(data);
		RepresentationType = string.IsNullOrEmpty(type) ? null : type;
		RepresentationId = string.IsNullOrEmpty(id) ? null : id;
	}

	public void PrintDetails() {
		Console.WriteLine($"Representation Data: [{string.Join(", ", RepresentationData)}]");
		Console.WriteLine($"Representation Type: {RepresentationType ?? "Default Type"}");
		Console.WriteLine($"Representation ID: {RepresentationId ?? "Default ID"}");
	}
}
