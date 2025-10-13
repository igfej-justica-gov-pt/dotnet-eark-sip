# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.2] - 2025-10-13

### Fixed

- Corrected the handling of the `note` and `noteType` fields in the `IPAgent` and `MetsTypeMetsHdrAgentNote` classes to ensure proper serialization and assignment of agent notes in METS headers.
- Improved the `ToString()` method in `IPAgent` for more accurate string representation of agent attributes.
- Ensured that the `Notetype` property in `MetsTypeMetsHdrAgentNote` is set and checked correctly, preventing issues with unset or default values.


[1.0.2]: https://github.com/igfej-justica-gov-pt/dotnet-eark-sip/compare/1.0.1...1.0.2
