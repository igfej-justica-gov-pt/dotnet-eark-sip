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
    /// <para xml:lang="en">fileType: Complex Type for Files
    ///				The file element provides access to content files for a METS object.  A file element may contain one or more FLocat elements, which provide pointers to a content file, and/or an FContent element, which wraps an encoded version of the file. Note that ALL FLocat and FContent elements underneath a single file element should identify/contain identical copies of a single file.</para>
    /// </summary>
    [System.ComponentModel.DescriptionAttribute(@"fileType: Complex Type for Files The file element provides access to content files for a METS object. A file element may contain one or more FLocat elements, which provide pointers to a content file, and/or an FContent element, which wraps an encoded version of the file. Note that ALL FLocat and FContent elements underneath a single file element should identify/contain identical copies of a single file.")]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1174.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("fileType", Namespace = "http://www.loc.gov/METS/")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FileType : IFilecore
    {

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<FileTypeFLocat> _fLocat;

        /// <summary>
        /// <para xml:lang="en">The file location element &lt;FLocat&gt; provides a pointer to the location of a content file. It uses the XLink reference syntax to provide linking information indicating the actual location of the content file, along with other attributes specifying additional linking information. NOTE: &lt;FLocat&gt; is an empty element. The location of the resource pointed to MUST be stored in the xlink:href attribute.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"The file location element &lt;FLocat&gt; provides a pointer to the location of a content file. It uses the XLink reference syntax to provide linking information indicating the actual location of the content file, along with other attributes specifying additional linking information. NOTE: &lt;FLocat&gt; is an empty element. The location of the resource pointed to MUST be stored in the xlink:href attribute.")]
        [System.Xml.Serialization.XmlElementAttribute("FLocat")]
        public System.Collections.ObjectModel.Collection<FileTypeFLocat> FLocat
        {
            get
            {
                return _fLocat;
            }
            private set
            {
                _fLocat = value;
            }
        }

        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the FLocat collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FLocatSpecified
        {
            get
            {
                return (this.FLocat.Count != 0);
            }
        }

        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="FileType" /> class.</para>
        /// </summary>
        public FileType()
        {
            this._fLocat = new System.Collections.ObjectModel.Collection<FileTypeFLocat>();
            this._stream = new System.Collections.ObjectModel.Collection<FileTypeStream>();
            this._transformFile = new System.Collections.ObjectModel.Collection<FileTypeTransformFile>();
            this._file = new System.Collections.ObjectModel.Collection<FileType>();
            this._admid = new System.Collections.ObjectModel.Collection<string>();
            this._dmdid = new System.Collections.ObjectModel.Collection<string>();
            this._anyAttribute = new System.Collections.ObjectModel.Collection<System.Xml.XmlAttribute>();
        }

        /// <summary>
        /// <para xml:lang="en">The file content element &lt;FContent&gt; is used to identify a content file contained internally within a METS document. The content file must be either Base64 encoded and contained within the subsidiary &lt;binData&gt; wrapper element, or consist of XML information and be contained within the subsidiary &lt;xmlData&gt; wrapper element.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"The file content element &lt;FContent&gt; is used to identify a content file contained internally within a METS document. The content file must be either Base64 encoded and contained within the subsidiary &lt;binData&gt; wrapper element, or consist of XML information and be contained within the subsidiary &lt;xmlData&gt; wrapper element.")]
        [System.Xml.Serialization.XmlElementAttribute("FContent")]
        public FileTypeFContent FContent { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<FileTypeStream> _stream;

        /// <summary>
        /// <para xml:lang="en">A component byte stream element &lt;stream&gt; may be composed of one or more subsidiary streams. An MPEG4 file, for example, might contain separate audio and video streams, each of which is associated with technical metadata. The repeatable &lt;stream&gt; element provides a mechanism to record the existence of separate data streams within a particular file, and the opportunity to associate &lt;dmdSec&gt; and &lt;amdSec&gt; with those subsidiary data streams if desired.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"A component byte stream element &lt;stream&gt; may be composed of one or more subsidiary streams. An MPEG4 file, for example, might contain separate audio and video streams, each of which is associated with technical metadata. The repeatable &lt;stream&gt; element provides a mechanism to record the existence of separate data streams within a particular file, and the opportunity to associate &lt;dmdSec&gt; and &lt;amdSec&gt; with those subsidiary data streams if desired.")]
        [System.Xml.Serialization.XmlElementAttribute("stream")]
        public System.Collections.ObjectModel.Collection<FileTypeStream> Stream
        {
            get
            {
                return _stream;
            }
            private set
            {
                _stream = value;
            }
        }

        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the Stream collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool StreamSpecified
        {
            get
            {
                return (this.Stream.Count != 0);
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<FileTypeTransformFile> _transformFile;

        /// <summary>
        /// <para xml:lang="en">The transform file element &lt;transformFile&gt; provides a means to access any subsidiary files listed below a &lt;file&gt; element by indicating the steps required to "unpack" or transform the subsidiary files. This element is repeatable and might provide a link to a &lt;behavior&gt; in the &lt;behaviorSec&gt; that performs the transformation.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"The transform file element &lt;transformFile&gt; provides a means to access any subsidiary files listed below a &lt;file&gt; element by indicating the steps required to ""unpack"" or transform the subsidiary files. This element is repeatable and might provide a link to a &lt;behavior&gt; in the &lt;behaviorSec&gt; that performs the transformation.")]
        [System.Xml.Serialization.XmlElementAttribute("transformFile")]
        public System.Collections.ObjectModel.Collection<FileTypeTransformFile> TransformFile
        {
            get
            {
                return _transformFile;
            }
            private set
            {
                _transformFile = value;
            }
        }

        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the TransformFile collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TransformFileSpecified
        {
            get
            {
                return (this.TransformFile.Count != 0);
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<FileType> _file;

        /// <summary>
        /// Gets or sets a collection of nested <see cref="FileType"/> objects.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("file")]
        public System.Collections.ObjectModel.Collection<FileType> File
        {
            get
            {
                return _file;
            }
            private set
            {
                _file = value;
            }
        }

        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the File collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FileSpecified
        {
            get
            {
                return (this.File.Count != 0);
            }
        }

        /// <summary>
        /// <para xml:lang="en">ID (ID/R): This attribute uniquely identifies the element within the METS document, and would allow the element to be referenced unambiguously from another element or document via an IDREF or an XPTR. Typically, the ID attribute value on a &lt;file&gt; element would be referenced from one or more FILEID attributes (which are of type IDREF) on &lt;fptr&gt;and/or &lt;area&gt; elements within the &lt;structMap&gt;.  Such references establish links between  structural divisions (&lt;div&gt; elements) and the specific content files or parts of content files that manifest them. For more information on using ID attributes for internal and external linking see Chapter 4 of the METS Primer.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"ID (ID/R): This attribute uniquely identifies the element within the METS document, and would allow the element to be referenced unambiguously from another element or document via an IDREF or an XPTR. Typically, the ID attribute value on a &lt;file&gt; element would be referenced from one or more FILEID attributes (which are of type IDREF) on &lt;fptr&gt;and/or &lt;area&gt; elements within the &lt;structMap&gt;. Such references establish links between structural divisions (&lt;div&gt; elements) and the specific content files or parts of content files that manifest them. For more information on using ID attributes for internal and external linking see Chapter 4 of the METS Primer.")]
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        [System.Xml.Serialization.XmlAttributeAttribute("ID")]
        public string Id { get; set; }

        /// <summary>
        /// <para xml:lang="en">SEQ (integer/O): Indicates the sequence of this &lt;file&gt; relative to the others in its &lt;fileGrp&gt;.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("SEQ (integer/O): Indicates the sequence of this &lt;file&gt; relative to the othe" +
            "rs in its &lt;fileGrp&gt;.")]
        [System.Xml.Serialization.XmlAttributeAttribute("SEQ")]
        public int Seq { get; set; }

        /// <summary>
        /// <para xml:lang="en">Gets or sets a value indicating whether the Seq property is specified.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SeqSpecified { get; set; }

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
        [System.Xml.Serialization.XmlAttributeAttribute("CREATED", DataType = "dateTime")]
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
        /// <para xml:lang="en">OWNERID (string/O): A unique identifier assigned to the file by its owner.  This may be a URI which differs from the URI used to retrieve the file.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("OWNERID (string/O): A unique identifier assigned to the file by its owner. This m" +
            "ay be a URI which differs from the URI used to retrieve the file.")]
        [System.Xml.Serialization.XmlAttributeAttribute("OWNERID")]
        public string Ownerid { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _admid;

        /// <summary>
        /// <para xml:lang="en">ADMID (IDREFS/O): Contains the ID attribute values of the &lt;techMD&gt;, &lt;sourceMD&gt;, &lt;rightsMD&gt; and/or &lt;digiprovMD&gt; elements within the &lt;amdSec&gt; of the METS document that contain administrative metadata pertaining to the file. For more information on using METS IDREFS and IDREF type attributes for internal linking, see Chapter 4 of the METS Primer.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"ADMID (IDREFS/O): Contains the ID attribute values of the &lt;techMD&gt;, &lt;sourceMD&gt;, &lt;rightsMD&gt; and/or &lt;digiprovMD&gt; elements within the &lt;amdSec&gt; of the METS document that contain administrative metadata pertaining to the file. For more information on using METS IDREFS and IDREF type attributes for internal linking, see Chapter 4 of the METS Primer.")]
        [System.Xml.Serialization.XmlAttributeAttribute("ADMID")]
        public System.Collections.ObjectModel.Collection<string> Admid
        {
            get
            {
                return _admid;
            }
            private set
            {
                _admid = value;
            }
        }

        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the Admid collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AdmidSpecified
        {
            get
            {
                return (this.Admid.Count != 0);
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _dmdid;

        /// <summary>
        /// <para xml:lang="en">DMDID (IDREFS/O): Contains the ID attribute values identifying the &lt;dmdSec&gt;, elements in the METS document that contain or link to descriptive metadata pertaining to the content file represented by the current &lt;file&gt; element.  For more information on using METS IDREFS and IDREF type attributes for internal linking, see Chapter 4 of the METS Primer.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"DMDID (IDREFS/O): Contains the ID attribute values identifying the &lt;dmdSec&gt;, elements in the METS document that contain or link to descriptive metadata pertaining to the content file represented by the current &lt;file&gt; element. For more information on using METS IDREFS and IDREF type attributes for internal linking, see Chapter 4 of the METS Primer.")]
        [System.Xml.Serialization.XmlAttributeAttribute("DMDID")]
        public System.Collections.ObjectModel.Collection<string> Dmdid
        {
            get
            {
                return _dmdid;
            }
            private set
            {
                _dmdid = value;
            }
        }

        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the Dmdid collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DmdidSpecified
        {
            get
            {
                return (this.Dmdid.Count != 0);
            }
        }

        /// <summary>
        /// <para xml:lang="en">GROUPID (string/O): An identifier that establishes a correspondence between this file and files in other file groups. Typically, this will be used to associate a master file in one file group with the derivative files made from it in other file groups.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("GROUPID (string/O): An identifier that establishes a correspondence between this " +
            "file and files in other file groups. Typically, this will be used to associate a" +
            " master file in one file group with the derivative files made from it in other f" +
            "ile groups.")]
        [System.Xml.Serialization.XmlAttributeAttribute("GROUPID")]
        public string Groupid { get; set; }

        /// <summary>
        /// <para xml:lang="en">USE (string/O): A tagging attribute to indicate the intended use of all copies of the file aggregated by the &lt;file&gt; element (e.g., master, reference, thumbnails for image files). A USE attribute can be expressed at the&lt;fileGrp&gt; level, the &lt;file&gt; level, the &lt;FLocat&gt; level and/or the &lt;FContent&gt; level.  A USE attribute value at the &lt;fileGrp&gt; level should pertain to all of the files in the &lt;fileGrp&gt;.  A USE attribute at the &lt;file&gt; level should pertain to all copies of the file as represented by subsidiary &lt;FLocat&gt; and/or &lt;FContent&gt; elements.  A USE attribute at the &lt;FLocat&gt; or &lt;FContent&gt; level pertains to the particular copy of the file that is either referenced (&lt;FLocat&gt;) or wrapped (&lt;FContent&gt;).</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"USE (string/O): A tagging attribute to indicate the intended use of all copies of the file aggregated by the &lt;file&gt; element (e.g., master, reference, thumbnails for image files). A USE attribute can be expressed at the&lt;fileGrp&gt; level, the &lt;file&gt; level, the &lt;FLocat&gt; level and/or the &lt;FContent&gt; level. A USE attribute value at the &lt;fileGrp&gt; level should pertain to all of the files in the &lt;fileGrp&gt;. A USE attribute at the &lt;file&gt; level should pertain to all copies of the file as represented by subsidiary &lt;FLocat&gt; and/or &lt;FContent&gt; elements. A USE attribute at the &lt;FLocat&gt; or &lt;FContent&gt; level pertains to the particular copy of the file that is either referenced (&lt;FLocat&gt;) or wrapped (&lt;FContent&gt;).")]
        [System.Xml.Serialization.XmlAttributeAttribute("USE")]
        public string Use { get; set; }

        /// <summary>
        /// <para xml:lang="en">BEGIN (string/O): An attribute that specifies the point in the parent &lt;file&gt; where the current &lt;file&gt; begins.  When used in conjunction with a &lt;file&gt; element, this attribute is only meaningful when this element is nested, and its parent &lt;file&gt; element represents a container file. It can be used in conjunction with the END attribute as a means of defining the location of the current file within its parent file. However, the BEGIN attribute can be used with or without a companion END attribute. When no END attribute is specified, the end of the parent file is assumed also to be the end point of the current file. The BEGIN and END attributes can only be interpreted meaningfully in conjunction with a BETYPE attribute, which specifies the kind of beginning/ending point values that are being used.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"BEGIN (string/O): An attribute that specifies the point in the parent &lt;file&gt; where the current &lt;file&gt; begins. When used in conjunction with a &lt;file&gt; element, this attribute is only meaningful when this element is nested, and its parent &lt;file&gt; element represents a container file. It can be used in conjunction with the END attribute as a means of defining the location of the current file within its parent file. However, the BEGIN attribute can be used with or without a companion END attribute. When no END attribute is specified, the end of the parent file is assumed also to be the end point of the current file. The BEGIN and END attributes can only be interpreted meaningfully in conjunction with a BETYPE attribute, which specifies the kind of beginning/ending point values that are being used.")]
        [System.Xml.Serialization.XmlAttributeAttribute("BEGIN")]
        public string Begin { get; set; }

        /// <summary>
        /// <para xml:lang="en">END (string/O): An attribute that specifies the point in the parent &lt;file&gt; where the current, nested &lt;file&gt; ends. It can only be interpreted meaningfully in conjunction with the BETYPE, which specifies the kind of ending point values being used. Typically the END attribute would only appear in conjunction with a BEGIN attribute.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"END (string/O): An attribute that specifies the point in the parent &lt;file&gt; where the current, nested &lt;file&gt; ends. It can only be interpreted meaningfully in conjunction with the BETYPE, which specifies the kind of ending point values being used. Typically the END attribute would only appear in conjunction with a BEGIN attribute.")]
        [System.Xml.Serialization.XmlAttributeAttribute("END")]
        public string End { get; set; }

        /// <summary>
        /// <para xml:lang="en">BETYPE: Begin/End Type.
        ///					BETYPE (string/O): An attribute that specifies the kind of BEGIN and/or END values that are being used. Currently BYTE is the only valid value that can be used in conjunction with nested &lt;file&gt; or &lt;stream&gt; elements.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("BETYPE: Begin/End Type. BETYPE (string/O): An attribute that specifies the kind o" +
            "f BEGIN and/or END values that are being used. Currently BYTE is the only valid " +
            "value that can be used in conjunction with nested &lt;file&gt; or &lt;stream&gt;" +
            " elements.")]
        [System.Xml.Serialization.XmlAttributeAttribute("BETYPE")]
        public FileTypeBetype Betype { get; set; }

        /// <summary>
        /// <para xml:lang="en">Gets or sets a value indicating whether the Betype property is specified.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BetypeSpecified { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute("FILEFORMATNAME", Namespace = "https://DILCIS.eu/XML/METS/SIPExtensionMETS", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string Fileformatname { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute("FILEFORMATVERSION", Namespace = "https://DILCIS.eu/XML/METS/SIPExtensionMETS", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string Fileformatversion { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute("FORMATREGISTRY", Namespace = "https://DILCIS.eu/XML/METS/SIPExtensionMETS", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string Formatregistry { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute("FORMATREGISTRYKEY", Namespace = "https://DILCIS.eu/XML/METS/SIPExtensionMETS", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string Formatregistrykey { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<System.Xml.XmlAttribute> _anyAttribute = null;

        /// <summary>
        /// Gets a collection of XML attributes that are not explicitly defined in the schema.
        /// </summary>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Collections.ObjectModel.Collection<System.Xml.XmlAttribute> AnyAttribute
        {
            get
            {
                if (_anyAttribute == null)
                {
                    _anyAttribute = new System.Collections.ObjectModel.Collection<System.Xml.XmlAttribute>();
                }

                return _anyAttribute;
            }
            private set
            {
                _anyAttribute = value;
            }
        }

        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the AnyAttribute collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AnyAttributeSpecified
        {
            get
            {
                return (this.AnyAttribute.Count != 0);
            }
        }

        /// <summary>
        /// Adds a custom XML attribute to the AnyAttribute collection.
        /// </summary>
        /// <param name="name">The name of the attribute to add.</param>
        /// <param name="value">The value of the attribute to add.</param>
        public void AddCustomAttribute(string name, string value)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlAttribute newAttribute = doc.CreateAttribute(name);
            newAttribute.Value = value;
            AnyAttribute.Add(newAttribute);
        }
    }
}
