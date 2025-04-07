# .NET E-ARK SIP

[![Build Status](https://img.shields.io/github/actions/workflow/status/igfej-justica-gov-pt/dotnet-eark-sip/CI.yml?branch=main)](https://github.com/igfej-justica-gov-pt/dotnet-eark-sip/actions)  
[![Latest Release](https://img.shields.io/github/v/release/igfej-justica-gov-pt/dotnet-eark-sip)](https://github.com/igfej-justica-gov-pt/dotnet-eark-sip/releases/latest)  
[![License](https://img.shields.io/badge/license-EUPL-blue.svg)](#license)

A .NET library and CLI tool for creating E-ARK Submission Information Packages (SIPs). Providing support for E-ARK SIP formats (2.0.4, 2.1.0, 2.2.0), this project makes it easier to create and manage valid SIPs for long-term digital preservation using the E-ARK standards.

## Table of Contents

- [Introduction](#introduction)
- [Why .NET E-ARK SIP?](#why-net-e-ark-sip)
- [Features](#features)
- [Requirements](#requirements)
- [Usage](#usage)
  - [Use as a Command-line Tool](#use-as-a-command-line-tool)
    - [Available options](#available-options)
    - [Example](#example)
  - [Use as a .NET Library](#use-as-a-net-library)
    - [Example](#use-it-in-your-net-code)
- [Contributing](#contributing)
- [FAQ](#faq)
- [License](#license)
- [Credits](#credits)
- [Sponsorship](#sponsorship)

## Introduction

This repository provides a command-line interface (CLI) and a .NET library to create E-ARK Submission Information Packages **version 2.2.0**.

The _E-ARK Information Package_ specifications are maintained by the [DILCIS Board](http://www.dilcis.eu/). The DILCIS Board is an international group of experts dedicated to developing and maintaining interoperability specifications for the long-term preservation of digital content.

These specifications have been sponsored and are promoted by the European Commission under the [eArchiving Initiative](https://digital-strategy.ec.europa.eu/en/activities/earchiving).

## Why .NET E-ARK SIP?

- **Automation**: Easily script and automate the creation of E-ARK SIPs.
- **Integration**: Embed this library into your applications to implement powerful export and transfer processes between business applications and digital archives (OAIS). The library is open-source, but not viral!
- **Standards-Compliant**: Ensures compliance with DILCIS Board specifications.
- **CLI & Library**: Use it stand-alone or integrate into your .NET projects.
- **Improvise, adapt, overcome**: You are free to modify and enhance this library to meet your needs. Just remember, forks must be open source as well.

## Features

- **CLI Tool**: Easily create SIPs via a command-line interface.
- **.NET Library**: Integrate with your own projects by referencing the NuGet package or local DLL.
- **Checksums**: Automatic creates checksums (SHA-256 by default) for SIP files.
- **Multiple metadata files**: Add descriptive and preservation metadata, including external schemas.
- **Documentation**: Bundle documentation files within your SIP to better describe the contents of the submission.
- **Configurability**: Override default schemas, specify representation types, set custom IDs, and more.

## Requirements

- .NET Standard 2.0 (or higher)
- Windows, Linux, or macOS

Download the [latest release](https://github.com/igfej-justica-gov-pt/dotnet-eark-sip/releases/latest) to use as a standalone CLI tool, or reference it in your .NET project.

## Usage

You can use the **dotnet-eark-sip** as a **command-line tool** or as a **.NET library** (by referencing it in your project).

### Use as a Command-line Tool

1. Download the [latest release](https://github.com/igfej-justica-gov-pt/dotnet-eark-sip/releases/latest).
2. Unzip or place the tool in a directory of your choice.
3. Run the following command (adjusting paths accordingly):

```bash
dotnet-eark-sip-cli create
```

#### Available options

- **create**: Creates an E-ARK SIP (this is the default action, so it can be omitted).
- **-d, --documentation**: Path(s) to folder(s)/file(s) to add to the SIP’s documentation section.
- **-p, --path**: Path to save the SIP.
- **-a, --ancestors**: ID(s) of the SIP’s ancestors.
- **-C, --checksum**: Checksum algorithm (default is SHA-256).
- **-T, --target-only**: Adds only the files for the representations.
- **-v, --version**: E-ARK SIP specification version (default: 2.1.0).
- **--submitter-name**: The name of the submitter agent.
- **--submitter-id**: The identification code of the submitter agent.
- **--sip-id**: ID of the SIP.
- **--override-schema**: Overrides default schema.
- **-s, --strategy**: Write strategy (default: Zip).

##### Descriptive Metadata Options

- **--metadata-files**: Path(s) to metadata file(s), comma-separated. _Required if_ `--representation-data-lists` _is not set_.
- **--metadata-types**: Metadata type(s), comma-separated. _Required if_ `--metadata-files` _is set_.
- **--metadata-schemas**: Path(s) to metadata schema file(s), comma-separated.
- **--metadata-versions**: Metadata version(s), comma-separated.

##### Representation Options

- **--representation-data-lists**: Path(s) to file(s) for representation, comma-separated. _Required if_ `--metadata-files` _is not set_.
- **--representation-id**: Representation identifier(s), comma-separated. Defaults to `rep<number>` if not provided.
- **--representation-type**: Representation type(s), comma-separated.

#### Example

```bash
dotnet-eark-sip-cli create --metadata-files metadata.xml --metadata-types ead --metadata-schemas ead2002.xsd \
--representation-data-lists dataFile1.pdf,dataFolder1,dataFile2.png \
--sip-id sip1 --ancestors sip2,sip3 --documentation documentation1,documentationFolder \
--path outputFolder --submitter-name agent1 --submitter-id 123
```

### Use as a .NET Library

> **Note**: If you do not plan to use this as a .NET library, you can skip this section.

#### Install the package via NuGet

```bash
dotnet add package dotnet-eark-sip
```

#### Use it in your .NET code

```csharp
// Import dependencies
using IP;
using Mets;

// Start creating a SIP
SIP sip = new EARKSIP("SIP_1", IPContentType.GetMIXED(), IPContentInformationType.GetMIXED(), "2.2.0");

// Set the name of the software that is submitting the SIP (mandatory)
sip.AddsubmitterSoftwareAgent("Instituto de Gestão Financeira e Equipamentos da Justiça", "IGFEJ");

// Set optional human-readable description
sip.SetDescription("A full E-ARK SIP");

// Add descriptive metadata (SIP level)
string metadataPath = ".\\Resources\\EARK\\metadata_descriptive_dc.xml";
IPDescriptiveMetadata descriptiveMetadata = new(
   new IPFile(metadataPath),
   new MetadataType(IMetadataMdtype.DC),
   null
);
sip.AddDescriptiveMetadata(descriptiveMetadata);

// Add xml schema (SIP level)
string schemaPath = ".\\Resources\\EARK\\schema.xsd";
sip.AddSchema(new IPFile(schemaPath));

// Add a representation (status will be set to the default value, i.e. ORIGINAL)
IPRepresentation representation1 = new("representation 1");
sip.AddRepresentation(representation1);

// Add a file to the representation
string representationFilePath = ".\\Resources\\EARK\\documentation.pdf";
IPFile representationFile = new(representationFilePath);
representationFile.SetRenameTo("data_.pdf");
representation1.AddFile(representationFile);

// Add another representation
IPRepresentation representation2 = new("representation 2");
sip.AddRepresentation(representation2);

// Add a file to the representation and put it inside a folder
// called 'abc' which has a folder inside called 'def'
IPFile representationFile2 = new(representationFilePath);
representationFile2.SetRelativeFolders(["abc", "def"]);
representation.AddFile(representationFile2);

// Set optional related information about ancestors
sip.SetAncestors(["b6f24059-8973-4582-932d-eb0b2cb48f28"]);

// Build the SIP
ZipWriteStrategyFactory zipWriteStrategyFactory = new();
IWriteStrategy writeStrategy = zipWriteStrategyFactory.Create(outputPath);
string zipSIP = sip.Build(writeStrategy);
```

#### In-depth usage

Look into the repository’s [dotnet-eark-sip-examples](./dotnet-eark-sip-examples) folder for more in-depth usage patterns and advanced features.

## Contributing

Contributions are welcome! Here are the basic steps:

1. **Fork** the repository and clone your fork.
2. **Create** a new feature branch.
3. **Commit** your changes, ensuring you follow the existing style and conventions.
4. **Open a Pull Request (PR)** describing the changes you’ve made and why they’re needed.

<!-- For more details, see our [CONTRIBUTING.md](CONTRIBUTING.md). Please also be aware of our [Code of Conduct](CODE_OF_CONDUCT.md). -->

## FAQ

1. **Does this work on all platforms?**  
   Yes, it is built for .NET Standard 2.0, so it should work on Windows, macOS, and Linux.

2. **Are older versions of .NET supported?**  
   .NET Standard 2.0 is compatible with .NET 5 (and higher), .NET Core 2.0 (and higher), and .NET Framework 4.6.1 (with some limitations).

3. **What if I need an E-ARK version not listed?**  
   Feel free to open an issue or contribute via pull request.

## License

This project is licensed under the **European Union Public Licence (EUPL) version 1.2**. The EUPL grants you the following rights:

1. **Use and access**: You are free to download and use this software, in whole or in part, for any lawful purpose, subject to the terms of the licence.
2. **Modification**: You can modify the source code to suit your needs, and you are encouraged to contribute your improvements back to the community.
3. **Distribution**: You can redistribute the original code or your modified version(s) to others. When you do, you **must** share it under the EUPL or a compatible licence, making the source code available under equivalent conditions.

### Limitations and Requirements

- **Licence continuity**: If you distribute copies or substantial portions of this software, modified or unmodified, you must retain the original licence text and grant the same rights to the recipients.
- **No warranty**: The software is provided "as is", without warranty of any kind. The licensor disclaims all liability for damages arising out of its use to the fullest extent permitted by law.
- **Attribution**: You must keep all copyright notices and attribution statements intact in the source files and any accompanying documentation.

For full details, please refer to the [EUPL licence text](https://joinup.ec.europa.eu/collection/eupl/eupl-text-eupl-12).

## Credits

- Developer: José Boticas (KEEP SOLUTIONS)
- Project manager: Paulo Lima (KEEP SOLUTIONS)
- Consultants: Luís Faria and Miguel Ferreira (KEEP SOLUTIONS)
- Sponsor: Institute for the Financial Management and Infrastructures of Justice (IGFEJ)

## Sponsorship

The sponsor of this development was the Institute for the Financial Management and Infrastructures of Justice (**IGFEJ**) a public institute, endowed with administrative and financial autonomy and its own assets, which pursues the attributions of the Ministry of Justice, under its supervision and tutelage.

The funding was provided by:
![Sponsor](https://raw.githubusercontent.com/igfej-justica-gov-pt/dotnet-eark-sip/main/prr.png)
