using Mets;
using System.Xml.Serialization;

namespace dotnet_eark_sip_tests;
public class MetsSerializationTests {
	private readonly string sampleXmlPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "METS_example.xml");
	// private readonly string outputPath = ".\\Output";

	/// <summary>
	/// Test deserialization of a METS XML file.
	/// </summary>
	[Fact]
	public void Deserialize_ValidMetsXml_ShouldMatchExpectedValues() {
		Assert.True(File.Exists(sampleXmlPath), "Sample METS file not found");
		string xmlContent = File.ReadAllText(sampleXmlPath);

		XmlSerializer serializer = new XmlSerializer(typeof(Mets.Mets));
		Mets.Mets? metsObject;

		using (StringReader reader = new StringReader(xmlContent)) {
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
}

