# .NET E-ARK SIP

A .NET library for creating E-ARK SIPs
# E-ARK IP validation and manipulation tool and library

This project provides a command-line interface and .NET library to create E-ARK Submission Information Packages of different versions: 2.0.4, 2.1.0, 2.2.0.

The E-ARK Information Packages are maintained by the Digital Information LifeCycle Interoperability Standards Board (
DILCIS Board). DILCIS Board is an international group of experts committed to maintaining and sustaining a set of
interoperability specifications which allow for the transfer, long-term preservation, and reuse of digital information
regardless of the origin or type of the information.

More specifically, the DILCIS Board maintains specifications initially developed within the E-ARK Project (02.2014 -
01.2017):

- Common Specification for Information Packages
- E-ARK Submission Information Package (SIP)
- E-ARK Archival Information Package (AIP)
- E-ARK Dissemination Information Package (DIP)

The DILCIS Board collaborates closely with the Swiss Federal Archives in regard to the maintenance of the SIARD (
Software Independent Archiving of Relational Databases) specification.

For more information about the E-ARK Information Packages specifications, please visit http://www.dilcis.eu/

## Installation

### Requirements

* .NET Standard 2.0

Download the [latest release](https://github.com/keeps/dotnet-eark-sip/releases/latest) to use as a tool or check below how
to use it as a .NET Library.

## Usage

You can use the dotnet-eark-sip as a command-line tool or a .NET library.

### Use as a command-line tool

To use the dotnet-eark-sip command-line tool, need to download
the [latest release](https://github.com/keeps/dotnet-eark-sip/releases/latest). This tool can create a
valid EARK2 SIP.

To create an EARK-2 SIP have to use the following options:

* **create**, [OPTIONAL] This option is for the CLI to know that is to perform the creation of an EARK-2 SIP. It is the default option, so it can be ommitted.

* **-d** or **--documentation**, [OPTIONAL] Path(s) to folders or files to add in the documentation of SIP.
* **-p** or **--path**, [OPTIONAL] Path to save the SIP.
* **-a** or **--ancestors**, [OPTIONAL] ID(s) of the ancestors of the SIP.
* **-C** or **--checksum**, [OPTIONAL] Checksum algorithm (Default: SHA-256).
* **-d** or **--documentation**, [OPTIONAL] Path(s) to documentation file(s).
* **-T** or **--target-only**, [OPTIONAL] Adds only the files for the representations.
* **-v** or **--version**, [OPTIONAL] E-ARK SIP specification version (Default: 2.1.0)
* **--submitter-name**, [OPTIONAL] The name of the submitter agent.
* **--submitter-id**, [OPTIONAL] The identification code (ID) of the submitter agent.
* **--sip-id**, [OPTIONAL] ID of the SIP.
* **--override-schema**, [OPTIONAL] Overrides default schema.
* **-s** or **--strategy**, [OPTIONAL] Write strategy to be used (Default: Zip)

This is the descriptive metadata section:
* **--metadata-files** [OPTIONAL] Path(s) to descriptive metadata file(s) (comma-separated). Becomes REQUIRED if **--representation-data-lists** is not set.
* **--metadata-types**, [OPTIONAL] Descriptive metadata type(s) (comma-separated). Becomes REQUIRED if **--metadata-files** is set.
* **--metadata-schemas**, [OPTIONAL] Path(s) to descriptive metadata schema file(s) (comma-separated).
* **--metadata-versions**, [OPTIONAL] Descriptive metadata version(s) (comma-separated).

**NOTE:** If does not give the metadata version, the tool tries to obtain these values from the file
name in the following formats (file: **ead_2002.xml** -> result: metadata version: **2002**)

This is the representation section:
* **--representation-data-lists**, [OPTIONAL] Path(s) to representation file(s) (comma-separated). Becomes REQUIRED if **--metadata-files** is not set.
* **--representation-id**, [OPTIONAL] Representation identifier(s) (comma-separated). If not set a default value of rep<number> will be used.
* **--representation-type**, [OPTIONAL] Representation type(s) (comma-separated).

Examples:

### Full create SIP command with long options:

```bash
dotnet run -- create --metadata-file metadata.xml --metadata-type ead --metadata-schema ead2002.xsd \
--representation-data-lists dataFile1.pdf,dataFolder1,dataFile2.png \
--sip-id sip1 --ancestors sip2,sip3 --documentation documentation1,documentationFolder --path folder2 --submitter-name agent1 --submitter-id 123
```

<!-- ### Use as a .NET Library

* Using NuGet

1. Add the following library to your project

```bash
dotnet add package dotnet-eark-sip
```

* Not using NuGet, use the GitHub packages to [download the dependency](https://github.com/keeps/dotnet-eark-sip/packages).

#### Write some code

* Create a full E-ARK SIP

```c#
// 1) instantiate E-ARK SIP object
SIP sip = new EARKSIP(
  "SIP_1", IPContentType.GetMIXED(), IPContentInformationType.GetMIXED()
);
sip.AddCreatorSoftwareAgent("KEEPS dotnet-eark-sip", "1.0.0");

// 1.1) set optional human-readable description
sip.SetDescription("A full E-ARK SIP");

// 1.2) add descriptive metadata (SIP level)
IPDescriptiveMetadata metadataDescriptiveDC = new IPDescriptiveMetadata(
  new IPFile("src\\resources\\eark\\metadata_descriptive_dc.xml"),
  new MetadataType(MetadataTypeEnum.DC),
  null
);
sip.AddDescriptiveMetadata(metadataDescriptiveDC);

// 1.3) add preservation metadata (SIP level)
IPMetadata metadataPreservation = new IPMetadata(
new IPFile("src\\resources\\eark\\metadata_preservation_premis.xml"));
sip.AddPreservationMetadata(metadataPreservation);

// 1.4) add other metadata (SIP level)
IPFile metadataOtherFile = new IPFile("src\\resources\\eark\\metadata_other.txt");
// 1.4.1) optionally one may rename file final name
metadataOtherFile.SetRenameTo("metadata_other_renamed.txt");
IPMetadata metadataOther = new IPMetadata(metadataOtherFile);
sip.AddOtherMetadata(metadataOther);

// 1.5) add xml schema (SIP level)
sip.AddSchema(new IPFile("src\\resources\\eark\\schema.xsd"));

// 1.6) add documentation (SIP level)
sip.AddDocumentation(new IPFile("src\\resources\\eark\\documentation.pdf"));

// 1.7) set optional RODA related information about ancestors
sip.SetAncestors(["b6f24059-8973-4582-932d-eb0b2cb48f28"]);

// 1.8) add an agent (SIP level)
IPAgent agent = new IPAgent("Agent Name","OTHER","OTHER ROLE",CreatorType.INDIVIDUAL,"OTHER TYPE","",
IPAgentNoteTypeEnum.SOFTWARE_VERSION);
sip.AddAgent(agent);

// 1.9) add a representation (status will be set to the default value, i.e.,
// ORIGINAL)
IPRepresentation representation1 = new IPRepresentation("representation 1");
sip.AddRepresentation(representation1);

// 1.9.1) add a file to the representation
IPFile representationFile = new IPFile("src\\resources\\eark\\documentation.pdf");
representationFile.SetRenameTo("data.pdf");
representation1.AddFile(representationFile);

// 1.9.2) add a file to the representation and put it inside a folder
// called 'def' which is inside a folder called 'abc'
IPFile representationFile2 = new IPFile("src\\resources\\eark\\documentation.pdf");
representationFile2.SetRelativeFolders(Arrays.asList("abc","def"));
representation1.AddFile(representationFile2);

// 1.10) add a representation & define its status
IPRepresentation representation2 = new IPRepresentation("representation 2");
representation2.SetStatus(new RepresentationStatus(REPRESENTATION_STATUS_NORMALIZED));
sip.AddRepresentation(representation2);

// 1.10.1) add a file to the representation
IPFile representationFile3 = new IPFile("src\\resources\\eark\\documentation.pdf");
representationFile3.SetRenameTo("data3.pdf");
representation2.AddFile(representationFile3);

// 2) build SIP, providing an output directory
Path zipSIP = sip.Build(tempFolder);
```

**Note:** SIP implements the Observer Pattern. This way, if one wants to be notified of SIP build progress, one just
needs to implement SIPObserver interface and register itself in the SIP. Something like (just presenting some of the
events):

```c#
public class WhoWantsToBuildSIPAndBeNotified : SIPObserver {

  public void BuildSIP() {
    ...
    SIP sip = new EARKSIP("SIP_1", IPContentType.GetMIXED());
    sip.AddObserver(this);
    ...
  }

  public override void SipBuildPackagingStarted(int totalNumberOfFiles) {
    ...
  }

  public override void SipBuildPackagingCurrentStatus(int numberOfFilesAlreadyProcessed) {
    ...
  }
}
``` -->

### License and Intellectual Property

All contributions to this project are licensed under a EUPL license, which includes an explicit grant of patent rights, meaning that the developers who created or contributed to the code relinquish their patent rights with regard to any subsequent reuse of the software.

## Credits

- José Boticas (KEEP SOLUTIONS)

## License

EUPL
