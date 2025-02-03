using Mets;
using System.Xml.Serialization;

namespace dotnet_eark_sip_tests;
public class MetsSerializationTests : IDisposable {
	private readonly string sampleXmlPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "METS", "METS_example.xml");
	private readonly string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "serialization_tests");

	public MetsSerializationTests() {
		Directory.CreateDirectory(outputPath);
	}

	/// <summary>
	/// Test deserialization of a METS XML file.
	/// </summary>
	[Fact]
	public void Deserialize_ValidMetsXml_ShouldMatchExpectedValues() {
		Assert.True(File.Exists(sampleXmlPath), "Sample METS file not found");
		string xmlContent = File.ReadAllText(sampleXmlPath);

		XmlSerializer serializer = new(typeof(Mets.Mets));
		Mets.Mets? metsObject;

		using (StringReader reader = new(xmlContent)) {
			object? obj = serializer.Deserialize(reader);
			if (obj != null) {
				metsObject = (Mets.Mets)obj;
			} else {
				metsObject = null;
			}
		}

		Assert.NotNull(metsObject);
		Assert.Equal("2006-10-19T00:00:00.001", metsObject.MetsHdr.Createdate.ToString("yyyy-MM-ddTHH:mm:ss.FFF"));
		Assert.Single(metsObject.MetsHdr.Agent);
		Assert.Equal(MetsTypeMetsHdrAgentRole.CREATOR, metsObject.MetsHdr.Agent[0].Role);

		Assert.Single(metsObject.DmdSec);
		Assert.Equal(IMetadataMdtype.MODS, metsObject.DmdSec[0].MdWrap.Mdtype);

		Assert.Single(metsObject.AmdSec);
		Assert.Single(metsObject.AmdSec[0].TechMd);
		Assert.Single(metsObject.AmdSec[0].RightsMd);

		Assert.Equal(2, metsObject.FileSec.FileGrp.Count);
		Assert.Single(metsObject.FileSec.FileGrp[1].File);
		Assert.Equal("FID2", metsObject.FileSec.FileGrp[1].File[0].Id);

		Assert.Single(metsObject.StructMap);
		Assert.Single(metsObject.StructMap[0].Div.Fptr);
		Assert.Equal("FID2", metsObject.StructMap[0].Div.Fptr[0].Fileid);
	}

	/// <summary>
	/// Test serialization of a METS object and verify output.
	/// </summary>
	[Fact]
	public void Serialize_MetsObject_ShouldProduceValidXml()
	{
		// Generate basic METS object
		Mets.Mets mets = new()
		{
			MetsHdr = new MetsTypeMetsHdr { Createdate = DateTime.Parse("2024-01-29T12:00:00") },
			SchemaLocation = "http://www.loc.gov/METS/ http://www.loc.gov/standards/mets/mets.xsd" +
				" https://DILCIS.eu/XML/METS/CSIPExtensionMETS https://earkcsip.dilcis.eu/schema/DILCISExtensionMETS.xsd" +
				" https://DILCIS.eu/XML/METS/SIPExtensionMETS https://earkcsip.dilcis.eu/schema/DILCISExtensionSIPMETS.xsd"
		};

		mets.MetsHdr.Agent.Add(new MetsTypeMetsHdrAgent {
			Role = MetsTypeMetsHdrAgentRole.CREATOR,
			Type = MetsTypeMetsHdrAgentType.INDIVIDUAL,
			Name = "John Doe"
		});

		mets.FileSec = new();
		mets.FileSec.FileGrp.Add(new MetsTypeFileSecFileGrp { Use = "images" });
		mets.FileSec.FileGrp[0].File.Add(new FileType {
			Id = "F1",
			Mimetype = "image/jpeg"
		});

		string xmlOutputPath = Path.Combine(outputPath, "METS_serialization_test.xml");

		// Serialize and write to file
		XmlSerializer serializer = new(typeof(Mets.Mets));
		string xmlOutput;
		
		using (StringWriter writer = new())
		{
			serializer.Serialize(writer, mets);
			xmlOutput = writer.ToString();
			File.WriteAllText(xmlOutputPath, xmlOutput);
		}

		// Check if the file was created
		Assert.True(File.Exists(xmlOutputPath), "Output XML file was not created!");

		// Ensure the output XML contains expected data
		Assert.Contains("<metsHdr", xmlOutput);
		Assert.Contains("CREATOR", xmlOutput);
		Assert.Contains("images", xmlOutput);
		Assert.Contains("image/jpeg", xmlOutput);
	}

	// Clean up
	public void Dispose() {
		if (Directory.Exists(outputPath)) {
			foreach (var file in Directory.GetFiles(outputPath)) {
				File.Delete(file);
			}
		}

		Directory.Delete(outputPath);
	}
}

