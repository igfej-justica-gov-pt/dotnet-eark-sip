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
    /// <para xml:lang="en">amdSecType: Complex Type for Administrative Metadata Sections
    ///			The administrative metadata section consists of four possible subsidiary sections: techMD (technical metadata for text/image/audio/video files), rightsMD (intellectual property rights metadata), sourceMD (analog/digital source metadata), and digiprovMD (digital provenance metadata, that is, the history of migrations/translations performed on a digital library object from it's original digital capture/encoding).</para>
    /// </summary>
    [System.ComponentModel.DescriptionAttribute(@"amdSecType: Complex Type for Administrative Metadata Sections The administrative metadata section consists of four possible subsidiary sections: techMD (technical metadata for text/image/audio/video files), rightsMD (intellectual property rights metadata), sourceMD (analog/digital source metadata), and digiprovMD (digital provenance metadata, that is, the history of migrations/translations performed on a digital library object from it's original digital capture/encoding).")]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1174.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("amdSecType", Namespace = "http://www.loc.gov/METS/")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AmdSecType
    {

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<MdSecType> _techMd;

        /// <summary>
        /// <para xml:lang="en">A technical metadata element &lt;techMD&gt; records technical metadata about a component of the METS object, such as a digital content file. The &lt;techMD&gt; element conforms to same generic datatype as the &lt;dmdSec&gt;, &lt;rightsMD&gt;, &lt;sourceMD&gt; and &lt;digiprovMD&gt; elements, and supports the same sub-elements and attributes.  A technical metadata element can either wrap the metadata  (mdWrap) or reference it in an external location (mdRef) or both.  METS allows multiple &lt;techMD&gt; elements; and technical metadata can be associated with any METS element that supports an ADMID attribute. Technical metadata can be expressed according to many current technical description standards (such as MIX and textMD) or a locally produced XML schema.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"A technical metadata element &lt;techMD&gt; records technical metadata about a component of the METS object, such as a digital content file. The &lt;techMD&gt; element conforms to same generic datatype as the &lt;dmdSec&gt;, &lt;rightsMD&gt;, &lt;sourceMD&gt; and &lt;digiprovMD&gt; elements, and supports the same sub-elements and attributes. A technical metadata element can either wrap the metadata (mdWrap) or reference it in an external location (mdRef) or both. METS allows multiple &lt;techMD&gt; elements; and technical metadata can be associated with any METS element that supports an ADMID attribute. Technical metadata can be expressed according to many current technical description standards (such as MIX and textMD) or a locally produced XML schema.")]
        [System.Xml.Serialization.XmlElementAttribute("techMD")]
        public System.Collections.ObjectModel.Collection<MdSecType> TechMd
        {
            get
            {
                return _techMd;
            }
            private set
            {
                _techMd = value;
            }
        }

        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the TechMd collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TechMdSpecified
        {
            get
            {
                return (this.TechMd.Count != 0);
            }
        }

        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="AmdSecType" /> class.</para>
        /// </summary>
        public AmdSecType()
        {
            this._techMd = new System.Collections.ObjectModel.Collection<MdSecType>();
            this._rightsMd = new System.Collections.ObjectModel.Collection<MdSecType>();
            this._sourceMd = new System.Collections.ObjectModel.Collection<MdSecType>();
            this._digiprovMd = new System.Collections.ObjectModel.Collection<MdSecType>();
            this._anyAttribute = new System.Collections.ObjectModel.Collection<System.Xml.XmlAttribute>();
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<MdSecType> _rightsMd;

        /// <summary>
        /// <para xml:lang="en">An intellectual property rights metadata element &lt;rightsMD&gt; records information about copyright and licensing pertaining to a component of the METS object. The &lt;rightsMD&gt; element conforms to same generic datatype as the &lt;dmdSec&gt;, &lt;techMD&gt;, &lt;sourceMD&gt; and &lt;digiprovMD&gt; elements, and supports the same sub-elements and attributes. A rights metadata element can either wrap the metadata  (mdWrap) or reference it in an external location (mdRef) or both.  METS allows multiple &lt;rightsMD&gt; elements; and rights metadata can be associated with any METS element that supports an ADMID attribute. Rights metadata can be expressed according current rights description standards (such as CopyrightMD and rightsDeclarationMD) or a locally produced XML schema.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"An intellectual property rights metadata element &lt;rightsMD&gt; records information about copyright and licensing pertaining to a component of the METS object. The &lt;rightsMD&gt; element conforms to same generic datatype as the &lt;dmdSec&gt;, &lt;techMD&gt;, &lt;sourceMD&gt; and &lt;digiprovMD&gt; elements, and supports the same sub-elements and attributes. A rights metadata element can either wrap the metadata (mdWrap) or reference it in an external location (mdRef) or both. METS allows multiple &lt;rightsMD&gt; elements; and rights metadata can be associated with any METS element that supports an ADMID attribute. Rights metadata can be expressed according current rights description standards (such as CopyrightMD and rightsDeclarationMD) or a locally produced XML schema.")]
        [System.Xml.Serialization.XmlElementAttribute("rightsMD")]
        public System.Collections.ObjectModel.Collection<MdSecType> RightsMd
        {
            get
            {
                return _rightsMd;
            }
            private set
            {
                _rightsMd = value;
            }
        }

        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the RightsMd collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RightsMdSpecified
        {
            get
            {
                return (this.RightsMd.Count != 0);
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<MdSecType> _sourceMd;

        /// <summary>
        /// <para xml:lang="en">A source metadata element &lt;sourceMD&gt; records descriptive and administrative metadata about the source format or media of a component of the METS object such as a digital content file. It is often used for discovery, data administration or preservation of the digital object. The &lt;sourceMD&gt; element conforms to same generic datatype as the &lt;dmdSec&gt;, &lt;techMD&gt;, &lt;rightsMD&gt;,  and &lt;digiprovMD&gt; elements, and supports the same sub-elements and attributes.  A source metadata element can either wrap the metadata  (mdWrap) or reference it in an external location (mdRef) or both.  METS allows multiple &lt;sourceMD&gt; elements; and source metadata can be associated with any METS element that supports an ADMID attribute. Source metadata can be expressed according to current source description standards (such as PREMIS) or a locally produced XML schema.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"A source metadata element &lt;sourceMD&gt; records descriptive and administrative metadata about the source format or media of a component of the METS object such as a digital content file. It is often used for discovery, data administration or preservation of the digital object. The &lt;sourceMD&gt; element conforms to same generic datatype as the &lt;dmdSec&gt;, &lt;techMD&gt;, &lt;rightsMD&gt;, and &lt;digiprovMD&gt; elements, and supports the same sub-elements and attributes. A source metadata element can either wrap the metadata (mdWrap) or reference it in an external location (mdRef) or both. METS allows multiple &lt;sourceMD&gt; elements; and source metadata can be associated with any METS element that supports an ADMID attribute. Source metadata can be expressed according to current source description standards (such as PREMIS) or a locally produced XML schema.")]
        [System.Xml.Serialization.XmlElementAttribute("sourceMD")]
        public System.Collections.ObjectModel.Collection<MdSecType> SourceMd
        {
            get
            {
                return _sourceMd;
            }
            private set
            {
                _sourceMd = value;
            }
        }

        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the SourceMd collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SourceMdSpecified
        {
            get
            {
                return (this.SourceMd.Count != 0);
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<MdSecType> _digiprovMd;

        /// <summary>
        /// <para xml:lang="en">A digital provenance metadata element &lt;digiprovMD&gt; can be used to record any preservation-related actions taken on the various files which comprise a digital object (e.g., those subsequent to the initial digitization of the files such as transformation or migrations) or, in the case of born digital materials, the files’ creation. In short, digital provenance should be used to record information that allows both archival/library staff and scholars to understand what modifications have been made to a digital object and/or its constituent parts during its life cycle. This information can then be used to judge how those processes might have altered or corrupted the object’s ability to accurately represent the original item. One might, for example, record master derivative relationships and the process by which those derivations have been created. Or the &lt;digiprovMD&gt; element could contain information regarding the migration/transformation of a file from its original digitization (e.g., OCR, TEI, etc.,)to its current incarnation as a digital object (e.g., JPEG2000). The &lt;digiprovMD&gt; element conforms to same generic datatype as the &lt;dmdSec&gt;,  &lt;techMD&gt;, &lt;rightsMD&gt;, and &lt;sourceMD&gt; elements, and supports the same sub-elements and attributes. A digital provenance metadata element can either wrap the metadata  (mdWrap) or reference it in an external location (mdRef) or both.  METS allows multiple &lt;digiprovMD&gt; elements; and digital provenance metadata can be associated with any METS element that supports an ADMID attribute. Digital provenance metadata can be expressed according to current digital provenance description standards (such as PREMIS) or a locally produced XML schema.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("A digital provenance metadata element &lt;digiprovMD&gt; can be used to record an" +
            "y preservation-related actions taken on the various files which comprise a digit" +
            "al object (e.g., those subsequent to the initial digitization of the files such " +
            "as transformation or migrations) or, in the case of born digital materials, the " +
            "files’ creation. In short, digital provenance should be used to record informati" +
            "on that allows both archival/library staff and scholars to understand what modif" +
            "ications have been made to a digital object and/or its constituent parts during " +
            "its life cycle. This information can then be used to judge how those processes m" +
            "ight have altered or corrupted the object’s ability to accurately represent the " +
            "original item. One might, for example, record master derivative relationships an" +
            "d the process by which those derivations have been created. Or the &lt;digiprovM" +
            "D&gt; element could contain information regarding the migration/transformation o" +
            "f a file from its original digitization (e.g., OCR, TEI, etc.,)to its current in" +
            "carnation as a digital object (e.g., JPEG2000). The &lt;digiprovMD&gt; element c" +
            "onforms to same generic datatype as the &lt;dmdSec&gt;, &lt;techMD&gt;, &lt;righ" +
            "tsMD&gt;, and &lt;sourceMD&gt; elements, and supports the same sub-elements and " +
            "attributes. A digital provenance metadata element can either wrap the metadata (" +
            "mdWrap) or reference it in an external location (mdRef) or both. METS allows mul" +
            "tiple &lt;digiprovMD&gt; elements; and digital provenance metadata can be associ" +
            "ated with any METS element that supports an ADMID attribute. Digital provenance " +
            "metadata can be expressed according to current digital provenance description st" +
            "andards (such as PREMIS) or a locally produced XML schema.")]
        [System.Xml.Serialization.XmlElementAttribute("digiprovMD")]
        public System.Collections.ObjectModel.Collection<MdSecType> DigiprovMd
        {
            get
            {
                return _digiprovMd;
            }
            private set
            {
                _digiprovMd = value;
            }
        }

        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the DigiprovMd collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DigiprovMdSpecified
        {
            get
            {
                return (this.DigiprovMd.Count != 0);
            }
        }

        /// <summary>
        /// <para xml:lang="en">ID (ID/O): This attribute uniquely identifies the element within the METS document, and would allow the element to be referenced unambiguously from another element or document via an IDREF or an XPTR. For more information on using ID attributes for internal and external linking see Chapter 4 of the METS Primer.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"ID (ID/O): This attribute uniquely identifies the element within the METS document, and would allow the element to be referenced unambiguously from another element or document via an IDREF or an XPTR. For more information on using ID attributes for internal and external linking see Chapter 4 of the METS Primer.")]
        [System.Xml.Serialization.XmlAttributeAttribute("ID")]
        public string Id { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<System.Xml.XmlAttribute> _anyAttribute;

        /// <summary>
        /// Gets a collection of XML attributes that are not explicitly defined in the schema.
        /// </summary>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Collections.ObjectModel.Collection<System.Xml.XmlAttribute> AnyAttribute
        {
            get
            {
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
    }
}
