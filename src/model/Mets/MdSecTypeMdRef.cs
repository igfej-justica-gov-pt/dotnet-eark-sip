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
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1174.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("MdSecTypeMdRef", Namespace="http://www.loc.gov/METS/", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MdSecTypeMdRef : ILocation, global::Xlink.ISimpleLink, IMetadata, IFilecore
    {
        
        /// <summary>
        /// <para xml:lang="en">ID (ID/O): This attribute uniquely identifies the element within the METS document, and would allow the element to be referenced unambiguously from another element or document via an IDREF or an XPTR. For more information on using ID attributes for internal and external linking see Chapter 4 of the METS Primer.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"ID (ID/O): This attribute uniquely identifies the element within the METS document, and would allow the element to be referenced unambiguously from another element or document via an IDREF or an XPTR. For more information on using ID attributes for internal and external linking see Chapter 4 of the METS Primer.")]
        [System.Xml.Serialization.XmlAttributeAttribute("ID")]
        public string Id { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">LOCTYPE (string/R): Specifies the locator type used in the xlink:href attribute. Valid values for LOCTYPE are: 
        ///					ARK
        ///					URN
        ///					URL
        ///					PURL
        ///					HANDLE
        ///					DOI
        ///					OTHER</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("LOCTYPE (string/R): Specifies the locator type used in the xlink:href attribute. " +
            "Valid values for LOCTYPE are: ARK URN URL PURL HANDLE DOI OTHER")]
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlAttributeAttribute("LOCTYPE")]
        public ILocationLoctype Loctype { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">OTHERLOCTYPE (string/O): Specifies the locator type when the value OTHER is used in the LOCTYPE attribute. Although optional, it is strongly recommended when OTHER is used.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("OTHERLOCTYPE (string/O): Specifies the locator type when the value OTHER is used " +
            "in the LOCTYPE attribute. Although optional, it is strongly recommended when OTH" +
            "ER is used.")]
        [System.Xml.Serialization.XmlAttributeAttribute("OTHERLOCTYPE")]
        public string Otherloctype { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private string _type = "simple";
        
        [System.ComponentModel.DefaultValueAttribute("simple")]
        [System.Xml.Serialization.XmlAttributeAttribute("type", Namespace="http://www.w3.org/1999/xlink", Form=System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }
        
        [System.Xml.Serialization.XmlAttributeAttribute("href", Namespace="http://www.w3.org/1999/xlink", Form=System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string Href { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute("role", Namespace="http://www.w3.org/1999/xlink", Form=System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string Role { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute("arcrole", Namespace="http://www.w3.org/1999/xlink", Form=System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string Arcrole { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute("title", Namespace="http://www.w3.org/1999/xlink", Form=System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string Title { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute("show", Namespace="http://www.w3.org/1999/xlink", Form=System.Xml.Schema.XmlSchemaForm.Qualified)]
        public global::Xlink.Show Show { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">Gets or sets a value indicating whether the Show property is specified.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ShowSpecified { get; set; }
        
        [System.Xml.Serialization.XmlAttributeAttribute("actuate", Namespace="http://www.w3.org/1999/xlink", Form=System.Xml.Schema.XmlSchemaForm.Qualified)]
        public global::Xlink.Actuate Actuate { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">Gets or sets a value indicating whether the Actuate property is specified.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActuateSpecified { get; set; }
        
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
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlAttributeAttribute("MDTYPE")]
        public IMetadataMdtype Mdtype { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">OTHERMDTYPE (string/O): Specifies the form of metadata in use when the value OTHER is indicated in the MDTYPE attribute.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("OTHERMDTYPE (string/O): Specifies the form of metadata in use when the value OTHE" +
            "R is indicated in the MDTYPE attribute.")]
        [System.Xml.Serialization.XmlAttributeAttribute("OTHERMDTYPE")]
        public string Othermdtype { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">MDTYPEVERSION(string/O): Provides a means for recording the version of the type of metadata (as recorded in the MDTYPE or OTHERMDTYPE attribute) that is being used.  This may represent the version of the underlying data dictionary or metadata model rather than a schema version.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"MDTYPEVERSION(string/O): Provides a means for recording the version of the type of metadata (as recorded in the MDTYPE or OTHERMDTYPE attribute) that is being used. This may represent the version of the underlying data dictionary or metadata model rather than a schema version.")]
        [System.Xml.Serialization.XmlAttributeAttribute("MDTYPEVERSION")]
        public string Mdtypeversion { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">MIMETYPE (string/O): The IANA MIME media type for the associated file or wrapped content. Some values for this attribute can be found on the IANA website.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("MIMETYPE (string/O): The IANA MIME media type for the associated file or wrapped " +
            "content. Some values for this attribute can be found on the IANA website.")]
        [System.Xml.Serialization.XmlAttributeAttribute("MIMETYPE")]
        public string Mimetype { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">SIZE (long/O): Specifies the size in bytes of the associated file or wrapped content.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("SIZE (long/O): Specifies the size in bytes of the associated file or wrapped cont" +
            "ent.")]
        [System.Xml.Serialization.XmlAttributeAttribute("SIZE")]
        public long Size { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">Gets or sets a value indicating whether the Size property is specified.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SizeSpecified { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">CREATED (dateTime/O): Specifies the date and time of creation for the associated file or wrapped content.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("CREATED (dateTime/O): Specifies the date and time of creation for the associated " +
            "file or wrapped content.")]
        [System.Xml.Serialization.XmlAttributeAttribute("CREATED", DataType="dateTime")]
        public System.DateTime Created { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">Gets or sets a value indicating whether the Created property is specified.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CreatedSpecified { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">CHECKSUM (string/O): Provides a checksum value for the associated file or wrapped content.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("CHECKSUM (string/O): Provides a checksum value for the associated file or wrapped" +
            " content.")]
        [System.Xml.Serialization.XmlAttributeAttribute("CHECKSUM")]
        public string Checksum { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">CHECKSUMTYPE (enumerated string/O): Specifies the checksum algorithm used to produce the value contained in the CHECKSUM attribute.  CHECKSUMTYPE must contain one of the following values:
        ///					Adler-32
        ///					CRC32
        ///					HAVAL
        ///					MD5
        ///					MNP
        ///					SHA-1
        ///					SHA-256
        ///					SHA-384
        ///					SHA-512
        ///					TIGER
        ///					WHIRLPOOL</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"CHECKSUMTYPE (enumerated string/O): Specifies the checksum algorithm used to produce the value contained in the CHECKSUM attribute. CHECKSUMTYPE must contain one of the following values: Adler-32 CRC32 HAVAL MD5 MNP SHA-1 SHA-256 SHA-384 SHA-512 TIGER WHIRLPOOL")]
        [System.Xml.Serialization.XmlAttributeAttribute("CHECKSUMTYPE")]
        public IFilecoreChecksumtype Checksumtype { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">Gets or sets a value indicating whether the Checksumtype property is specified.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ChecksumtypeSpecified { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">LABEL (string/O): Provides a label to display to the viewer of the METS document that identifies the associated metadata.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("LABEL (string/O): Provides a label to display to the viewer of the METS document " +
            "that identifies the associated metadata.")]
        [System.Xml.Serialization.XmlAttributeAttribute("LABEL")]
        public string Label { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">XPTR (string/O): Locates the point within a file to which the &lt;mdRef&gt; element refers, if applicable.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("XPTR (string/O): Locates the point within a file to which the &lt;mdRef&gt; eleme" +
            "nt refers, if applicable.")]
        [System.Xml.Serialization.XmlAttributeAttribute("XPTR")]
        public string Xptr { get; set; }
    }
}
