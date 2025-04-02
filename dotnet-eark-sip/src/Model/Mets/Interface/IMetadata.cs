//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// This code was generated by XmlSchemaClassGenerator version 2.1.1174.0 using the following command:
// xscgen --namespaceHierarchy -o .\src\model .\schemas\*.xsd
namespace Mets
{


    /// <summary>
    /// Represents metadata information with properties for metadata type, other metadata type, and metadata version.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1174.0")]
    public partial interface IMetadata
    {

        /// <summary>
        /// <para xml:lang="en">MDTYPE (string/R): Is used to indicate the type of the associated metadata. It must have one of the following values:
        ///MARC: any form of MARC record
        ///MODS: metadata in the Library of Congress MODS format
        ///EAD: Encoded Archival Description finding aid
        ///DC: Dublin Core
        ///NISOIMG: NISO Technical Metadata for Digital Still Images
        ///LC-AV: technical metadata specified in the Library of Congress A/V prototyping project
        ///VRA: Visual Resources Association Core
        ///TEIHDR: Text Encoding Initiative Header
        ///DDI: Data Documentation Initiative
        ///FGDC: Federal Geographic Data Committee metadata
        ///LOM: Learning Object Model
        ///PREMIS:  PREservation Metadata: Implementation Strategies
        ///PREMIS:OBJECT: PREMIS Object entiry
        ///PREMIS:AGENT: PREMIS Agent entity
        ///PREMIS:RIGHTS: PREMIS Rights entity
        ///PREMIS:EVENT: PREMIS Event entity
        ///TEXTMD: textMD Technical metadata for text
        ///METSRIGHTS: Rights Declaration Schema
        ///ISO 19115:2003 NAP: North American Profile of ISO 19115:2003 descriptive metadata
        ///EAC-CPF: Encoded Archival Context - Corporate Bodies, Persons, and Families
        ///LIDO: Lightweight Information Describing Objects
        ///OTHER: metadata in a format not specified above</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"MDTYPE (string/R): Is used to indicate the type of the associated metadata. It must have one of the following values: MARC: any form of MARC record MODS: metadata in the Library of Congress MODS format EAD: Encoded Archival Description finding aid DC: Dublin Core NISOIMG: NISO Technical Metadata for Digital Still Images LC-AV: technical metadata specified in the Library of Congress A/V prototyping project VRA: Visual Resources Association Core TEIHDR: Text Encoding Initiative Header DDI: Data Documentation Initiative FGDC: Federal Geographic Data Committee metadata LOM: Learning Object Model PREMIS: PREservation Metadata: Implementation Strategies PREMIS:OBJECT: PREMIS Object entiry PREMIS:AGENT: PREMIS Agent entity PREMIS:RIGHTS: PREMIS Rights entity PREMIS:EVENT: PREMIS Event entity TEXTMD: textMD Technical metadata for text METSRIGHTS: Rights Declaration Schema ISO 19115:2003 NAP: North American Profile of ISO 19115:2003 descriptive metadata EAC-CPF: Encoded Archival Context - Corporate Bodies, Persons, and Families LIDO: Lightweight Information Describing Objects OTHER: metadata in a format not specified above")]
        IMetadataMdtype Mdtype
        {
            get;
            set;
        }

        /// <summary>
        /// <para xml:lang="en">OTHERMDTYPE (string/O): Specifies the form of metadata in use when the value OTHER is indicated in the MDTYPE attribute.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("OTHERMDTYPE (string/O): Specifies the form of metadata in use when the value OTHE" +
            "R is indicated in the MDTYPE attribute.")]
        string Othermdtype
        {
            get;
            set;
        }

        /// <summary>
        /// <para xml:lang="en">MDTYPEVERSION(string/O): Provides a means for recording the version of the type of metadata (as recorded in the MDTYPE or OTHERMDTYPE attribute) that is being used.  This may represent the version of the underlying data dictionary or metadata model rather than a schema version.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"MDTYPEVERSION(string/O): Provides a means for recording the version of the type of metadata (as recorded in the MDTYPE or OTHERMDTYPE attribute) that is being used. This may represent the version of the underlying data dictionary or metadata model rather than a schema version.")]
        string Mdtypeversion
        {
            get;
            set;
        }
    }
}
