using IP;
using Mets;

namespace dotnet_eark_sip_tests
{
    public class MetadataTests
    {
        [Fact]
        public void TestIPMetadataCreation()
        {
            // Test IPMetadata constructors and getters/setters
            var metadataFile = new IPFile("dummy.txt");

            // Test empty constructor (used for serialization)
            var emptyMetadata = new IPMetadata();
            Assert.NotNull(emptyMetadata);

            // Test with only file
            var metadataWithFile = new IPMetadata(metadataFile);
            Assert.NotNull(metadataWithFile);
            Assert.NotNull(metadataWithFile.GetID());
            Assert.Equal(metadataFile, metadataWithFile.GetMetadata());
            Assert.Equal(IMetadataMdtype.OTHER, metadataWithFile.GetMetadataType()._GetType());
            Assert.Equal(EnumUtils.GetXmlEnumName(MetadataStatus.CURRENT), metadataWithFile.GetMetadataStatus());

            // Test with file and type
            var dcType = IMetadataMdtype.DC;
            var metadataWithFileAndType = new IPMetadata(metadataFile);
            metadataWithFileAndType.SetMetadataType(dcType);
            Assert.Equal(dcType, metadataWithFileAndType.GetMetadataType()._GetType());

            // Test with ID, file and type
            var id = "test-id-123";
            var metadataWithIdFileAndType = new IPMetadata(metadataFile);
            metadataWithIdFileAndType.SetID(id);
            metadataWithIdFileAndType.SetMetadataType(dcType);
            Assert.Equal(id, metadataWithIdFileAndType.GetID());

            // Test setters
            var metadata = new IPMetadata();
            metadata.SetID("custom-id");
            metadata.SetMetadata(metadataFile);
            metadata.SetMetadataType(IMetadataMdtype.PREMIS);
            metadata.SetMetadataStatus(MetadataStatus.SUPERSEDED);
            metadata.SetCreateDate(DateTime.Now);

            Assert.Equal("custom-id", metadata.GetID());
            Assert.Equal(metadataFile, metadata.GetMetadata());
            Assert.Equal(IMetadataMdtype.PREMIS, metadata.GetMetadataType()._GetType());
            Assert.Equal(EnumUtils.GetXmlEnumName(MetadataStatus.SUPERSEDED), metadata.GetMetadataStatus());
            Assert.NotNull(metadata.ToString());
        }

        [Fact]
        public void TestAddMetadataToIP()
        {
            // Create a simple SIP
            var sip = new EARKSIP("SIP_ID_1", IPContentType.GetMIXED(), IPContentInformationType.GetMIXED(), "2.1.0");

            // Create metadata files
            var descriptiveFile = new IPFile("descriptive.txt");
            var technicalFile = new IPFile("technical.txt");
            var rightsFile = new IPFile("rights.txt");
            var sourceFile = new IPFile("source.txt");

            // Add descriptive metadata
            var descriptive = new IPDescriptiveMetadata(descriptiveFile, new MetadataType(IMetadataMdtype.DC), null);
            sip.AddDescriptiveMetadata(descriptive);
            Assert.Single(sip.GetDescriptiveMetadata());
            Assert.Equal(descriptive, sip.GetDescriptiveMetadata().First());

            // Add preservation metadata
            var preservation = new IPMetadata(descriptiveFile);
            preservation.SetMetadataType(IMetadataMdtype.PREMIS);
            sip.AddPreservationMetadata(preservation);
            Assert.Single(sip.GetPreservationMetadata());
            Assert.Equal(preservation, sip.GetPreservationMetadata().First());

            // Add technical metadata
            var technical = new IPMetadata(technicalFile);
            technical.SetMetadataType(IMetadataMdtype.NISOIMG);
            sip.AddTechnicalMetadata(technical);
            Assert.Single(sip.GetTechnicalMetadata());
            Assert.Equal(technical, sip.GetTechnicalMetadata().First());

            // Add source metadata
            var source = new IPMetadata(sourceFile);
            source.SetMetadataType(IMetadataMdtype.DC);
            sip.AddSourceMetadata(source);
            Assert.Single(sip.GetSourceMetadata());
            Assert.Equal(source, sip.GetSourceMetadata().First());

            // Add rights metadata
            var rights = new IPMetadata(rightsFile);
            rights.SetMetadataType(IMetadataMdtype.DC);
            sip.AddRightsMetadata(rights);
            Assert.Single(sip.GetRightsMetadata());
            Assert.Equal(rights, sip.GetRightsMetadata().First());

            // Add other metadata
            var other = new IPMetadata(descriptiveFile);
            other.SetMetadataType(IMetadataMdtype.OTHER);
            sip.AddOtherMetadata(other);
            Assert.Single(sip.GetOtherMetadata());
            Assert.Equal(other, sip.GetOtherMetadata().First());
        }

        [Fact]
        public void TestAddMetadataToRepresentation()
        {
            // Create a simple SIP
            var sip = new EARKSIP("SIP_ID_1", IPContentType.GetMIXED(), IPContentInformationType.GetMIXED(), "2.1.0");

            // Create a representation
            var representation = new IPRepresentation("representation-1");
            sip.AddRepresentation(representation);

            // Create metadata files
            var descriptiveFile = new IPFile("descriptive.txt");
            var technicalFile = new IPFile("technical.txt");
            var rightsFile = new IPFile("rights.txt");
            var sourceFile = new IPFile("source.txt");

            // Add descriptive metadata to representation
            var descriptive = new IPDescriptiveMetadata(descriptiveFile, new MetadataType(IMetadataMdtype.DC), null);
            sip.AddDescriptiveMetadataToRepresentation("representation-1", descriptive);
            Assert.Single(representation.DescriptiveMetadata);
            Assert.Equal(descriptive, representation.DescriptiveMetadata.First());

            // Add preservation metadata to representation
            var preservation = new IPMetadata(descriptiveFile);
            preservation.SetMetadataType(IMetadataMdtype.PREMIS);
            sip.AddPreservationMetadataToRepresentation("representation-1", preservation);
            Assert.Single(representation.PreservationMetadata);
            Assert.Equal(preservation, representation.PreservationMetadata.First());

            // Add technical metadata directly to representation
            var technical = new IPMetadata(technicalFile);
            technical.SetMetadataType(IMetadataMdtype.NISOIMG);
            representation.AddTechnicalMetadata(technical);
            Assert.Single(representation.TechnicalMetadata);
            Assert.Equal(technical, representation.TechnicalMetadata.First());

            // Add source metadata directly to representation
            var source = new IPMetadata(sourceFile);
            source.SetMetadataType(IMetadataMdtype.DC);
            representation.AddSourceMetadata(source);
            Assert.Single(representation.SourceMetadata);
            Assert.Equal(source, representation.SourceMetadata.First());

            // Add rights metadata to representation
            var rights = new IPMetadata(rightsFile);
            rights.SetMetadataType(IMetadataMdtype.DC);
            sip.AddRightsMetadataToRepresentation("representation-1", rights);
            Assert.Single(representation.RightsMetadata);
            Assert.Equal(rights, representation.RightsMetadata.First());

            // Add other metadata to representation
            var other = new IPMetadata(descriptiveFile);
            other.SetMetadataType(IMetadataMdtype.OTHER);
            sip.AddOtherMetadataToRepresentation("representation-1", other);
            Assert.Single(representation.OtherMetadata);
            Assert.Equal(other, representation.OtherMetadata.First());
        }

        [Fact]
        public void TestMetadataTypeEnum()
        {
            // Test MetadataType constructors and getters/setters
            var dcType = new MetadataType(IMetadataMdtype.DC);
            Assert.Equal(IMetadataMdtype.DC, dcType._GetType());
            Assert.Equal(string.Empty, dcType.GetOtherType());

            // Test with string value that matches enum
            var modsType = new MetadataType("MODS");
            Assert.Equal(IMetadataMdtype.MODS, modsType._GetType());
            Assert.Equal(string.Empty, dcType.GetOtherType());

            // Test with string value that doesn't match enum
            var customType = "CUSTOM_TYPE";
            var customMetadataType = new MetadataType(customType);
            Assert.Equal(IMetadataMdtype.OTHER, customMetadataType._GetType());
            Assert.Equal(customType, customMetadataType.GetOtherType());

            // Test setter
            var newCustomType = "NEW_CUSTOM_TYPE";
            customMetadataType.SetOtherType(newCustomType);
            Assert.Equal(newCustomType, customMetadataType.GetOtherType());
        }
    }
}
