# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.5] - 2025-12-29

### Fixed

- Improved handling of whitespace in metadata type checks.
- Fixed `IPFile` constructor to use the current directory when no path is provided.

### Added

- Support for technical, source, and rights metadata in the SIP creation and METS packaging process.
- Methods for adding and retrieving technical, source, and rights metadata at both the package and representation levels.
- New constants and folder structure for technical, source, and rights metadata.

### Tests

- Added comprehensive unit tests for metadata creation, addition, and retrieval, including technical, source, and rights metadata.

## [1.0.4] - 2025-12-04

### Fixed

- Fixed issue with handling of metadata `otherType` in `MetadataType` to ensure correct assignment and retrieval when type is `OTHER`.
- Fixed CLI issue where the metadata version was not handled correctly in options.

### Added

- Added a unit test to ensure `MetadataType` correctly handles the 'OTHER' type and `otherType` assignment, preventing regressions in future iterations.

## [1.0.3] - 2025-11-17

### Fixed

- Updated the default values for the E-ARK SIP structural map and division labels from `dotnet-eark-sip` to `RODA` to ensure correct labeling in generated METS files. This affects the constants `EARK_SIP_STRUCTURAL_MAP` and `EARK_SIP_DIV_LABEL`.

## [1.0.2] - 2025-10-13

### Fixed

- Corrected the handling of the `note` and `noteType` fields in the `IPAgent` and `MetsTypeMetsHdrAgentNote` classes to ensure proper serialization and assignment of agent notes in METS headers.
- Improved the `ToString()` method in `IPAgent` for more accurate string representation of agent attributes.
- Ensured that the `Notetype` property in `MetsTypeMetsHdrAgentNote` is set and checked correctly, preventing issues with unset or default values.


[1.0.2]: https://github.com/igfej-justica-gov-pt/dotnet-eark-sip/compare/1.0.1...1.0.2
[1.0.3]: https://github.com/igfej-justica-gov-pt/dotnet-eark-sip/compare/1.0.2...1.0.3
[1.0.4]: https://github.com/igfej-justica-gov-pt/dotnet-eark-sip/compare/1.0.3...1.0.4
[1.0.5]: https://github.com/igfej-justica-gov-pt/dotnet-eark-sip/compare/1.0.4...1.0.5
