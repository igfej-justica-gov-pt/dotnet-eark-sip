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
    /// <para xml:lang="en">divType: Complex Type for Divisions
    ///					The METS standard represents a document structurally as a series of nested div elements, that is, as a hierarchy (e.g., a book, which is composed of chapters, which are composed of subchapters, which are composed of text).  Every div node in the structural map hierarchy may be connected (via subsidiary mptr or fptr elements) to content files which represent that div's portion of the whole document.
    ///
    ///SPECIAL NOTE REGARDING DIV ATTRIBUTE VALUES:
    ///to clarify the differences between the ORDER, ORDERLABEL, and LABEL attributes for the &lt;div&gt; element, imagine a text with 10 roman numbered pages followed by 10 arabic numbered pages. Page iii would have an ORDER of "3", an ORDERLABEL of "iii" and a LABEL of "Page iii", while page 3 would have an ORDER of "13", an ORDERLABEL of "3" and a LABEL of "Page 3".</para>
    /// </summary>
    [System.ComponentModel.DescriptionAttribute(@"divType: Complex Type for Divisions The METS standard represents a document structurally as a series of nested div elements, that is, as a hierarchy (e.g., a book, which is composed of chapters, which are composed of subchapters, which are composed of text). Every div node in the structural map hierarchy may be connected (via subsidiary mptr or fptr elements) to content files which represent that div's portion of the whole document. SPECIAL NOTE REGARDING DIV ATTRIBUTE VALUES: to clarify the differences between the ORDER, ORDERLABEL, and LABEL attributes for the &lt;div&gt; element, imagine a text with 10 roman numbered pages followed by 10 arabic numbered pages. Page iii would have an ORDER of ""3"", an ORDERLABEL of ""iii"" and a LABEL of ""Page iii"", while page 3 would have an ORDER of ""13"", an ORDERLABEL of ""3"" and a LABEL of ""Page 3"".")]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1174.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("divType", Namespace = "http://www.loc.gov/METS/")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DivType : IOrderlabels
    {

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<DivTypeMptr> _mptr;

        /// <summary>
        /// <para xml:lang="en">Like the &lt;fptr&gt; element, the METS pointer element &lt;mptr&gt; represents digital content that manifests its parent &lt;div&gt; element. Unlike the &lt;fptr&gt;, which either directly or indirectly points to content represented in the &lt;fileSec&gt; of the parent METS document, the &lt;mptr&gt; element points to content represented by an external METS document. Thus, this element allows multiple discrete and separate METS documents to be organized at a higher level by a separate METS document. For example, METS documents representing the individual issues in the series of a journal could be grouped together and organized by a higher level METS document that represents the entire journal series. Each of the &lt;div&gt; elements in the &lt;structMap&gt; of the METS document representing the journal series would point to a METS document representing an issue.  It would do so via a child &lt;mptr&gt; element. Thus the &lt;mptr&gt; element gives METS users considerable flexibility in managing the depth of the &lt;structMap&gt; hierarchy of individual METS documents. The &lt;mptr&gt; element points to an external METS document by means of an xlink:href attribute and associated XLink attributes.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"Like the &lt;fptr&gt; element, the METS pointer element &lt;mptr&gt; represents digital content that manifests its parent &lt;div&gt; element. Unlike the &lt;fptr&gt;, which either directly or indirectly points to content represented in the &lt;fileSec&gt; of the parent METS document, the &lt;mptr&gt; element points to content represented by an external METS document. Thus, this element allows multiple discrete and separate METS documents to be organized at a higher level by a separate METS document. For example, METS documents representing the individual issues in the series of a journal could be grouped together and organized by a higher level METS document that represents the entire journal series. Each of the &lt;div&gt; elements in the &lt;structMap&gt; of the METS document representing the journal series would point to a METS document representing an issue. It would do so via a child &lt;mptr&gt; element. Thus the &lt;mptr&gt; element gives METS users considerable flexibility in managing the depth of the &lt;structMap&gt; hierarchy of individual METS documents. The &lt;mptr&gt; element points to an external METS document by means of an xlink:href attribute and associated XLink attributes.")]
        [System.Xml.Serialization.XmlElementAttribute("mptr")]
        public System.Collections.ObjectModel.Collection<DivTypeMptr> Mptr
        {
            get
            {
                return _mptr;
            }
            private set
            {
                _mptr = value;
            }
        }

        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the Mptr collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MptrSpecified
        {
            get
            {
                return (this.Mptr.Count != 0);
            }
        }

        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="DivType" /> class.</para>
        /// </summary>
        public DivType()
        {
            this._mptr = new System.Collections.ObjectModel.Collection<DivTypeMptr>();
            this._fptr = new System.Collections.ObjectModel.Collection<DivTypeFptr>();
            this._div = new System.Collections.ObjectModel.Collection<DivType>();
            this._dmdid = new System.Collections.ObjectModel.Collection<string>();
            this._admid = new System.Collections.ObjectModel.Collection<string>();
            this._contentids = new System.Collections.ObjectModel.Collection<string>();
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<DivTypeFptr> _fptr;

        /// <summary>
        /// <para xml:lang="en">The &lt;fptr&gt; or file pointer element represents digital content that manifests its parent &lt;div&gt; element. The content represented by an &lt;fptr&gt; element must consist of integral files or parts of files that are represented by &lt;file&gt; elements in the &lt;fileSec&gt;. Via its FILEID attribute,  an &lt;fptr&gt; may point directly to a single integral &lt;file&gt; element that manifests a structural division. However, an &lt;fptr&gt; element may also govern an &lt;area&gt; element,  a &lt;par&gt;, or  a &lt;seq&gt;  which in turn would point to the relevant file or files. A child &lt;area&gt; element can point to part of a &lt;file&gt; that manifests a division, while the &lt;par&gt; and &lt;seq&gt; elements can point to multiple files or parts of files that together manifest a division. More than one &lt;fptr&gt; element can be associated with a &lt;div&gt; element. Typically sibling &lt;fptr&gt; elements represent alternative versions, or manifestations, of the same content</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"The &lt;fptr&gt; or file pointer element represents digital content that manifests its parent &lt;div&gt; element. The content represented by an &lt;fptr&gt; element must consist of integral files or parts of files that are represented by &lt;file&gt; elements in the &lt;fileSec&gt;. Via its FILEID attribute, an &lt;fptr&gt; may point directly to a single integral &lt;file&gt; element that manifests a structural division. However, an &lt;fptr&gt; element may also govern an &lt;area&gt; element, a &lt;par&gt;, or a &lt;seq&gt; which in turn would point to the relevant file or files. A child &lt;area&gt; element can point to part of a &lt;file&gt; that manifests a division, while the &lt;par&gt; and &lt;seq&gt; elements can point to multiple files or parts of files that together manifest a division. More than one &lt;fptr&gt; element can be associated with a &lt;div&gt; element. Typically sibling &lt;fptr&gt; elements represent alternative versions, or manifestations, of the same content")]
        [System.Xml.Serialization.XmlElementAttribute("fptr")]
        public System.Collections.ObjectModel.Collection<DivTypeFptr> Fptr
        {
            get
            {
                return _fptr;
            }
            private set
            {
                _fptr = value;
            }
        }

        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the Fptr collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FptrSpecified
        {
            get
            {
                return (this.Fptr.Count != 0);
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<DivType> _div;

        [System.Xml.Serialization.XmlElementAttribute("div")]
        public System.Collections.ObjectModel.Collection<DivType> Div
        {
            get
            {
                return _div;
            }
            private set
            {
                _div = value;
            }
        }

        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the Div collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DivSpecified
        {
            get
            {
                return (this.Div.Count != 0);
            }
        }

        /// <summary>
        /// <para xml:lang="en">ID (ID/O): This attribute uniquely identifies the element within the METS document, and would allow the element to be referenced unambiguously from another element or document via an IDREF or an XPTR. For more information on using ID attributes for internal and external linking see Chapter 4 of the METS Primer.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"ID (ID/O): This attribute uniquely identifies the element within the METS document, and would allow the element to be referenced unambiguously from another element or document via an IDREF or an XPTR. For more information on using ID attributes for internal and external linking see Chapter 4 of the METS Primer.")]
        [System.Xml.Serialization.XmlAttributeAttribute("ID")]
        public string Id { get; set; }

        /// <summary>
        /// <para xml:lang="en">ORDER (integer/O): A representation of the element's order among its siblings (e.g., its absolute, numeric sequence). For an example, and clarification of the distinction between ORDER and ORDERLABEL, see the description of the ORDERLABEL attribute.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("ORDER (integer/O): A representation of the element\'s order among its siblings (e." +
            "g., its absolute, numeric sequence). For an example, and clarification of the di" +
            "stinction between ORDER and ORDERLABEL, see the description of the ORDERLABEL at" +
            "tribute.")]
        [System.Xml.Serialization.XmlAttributeAttribute("ORDER")]
        public string Order { get; set; }

        /// <summary>
        /// <para xml:lang="en">ORDERLABEL (string/O): A representation of the element's order among its siblings (e.g., “xii”), or of any non-integer native numbering system. It is presumed that this value will still be machine actionable (e.g., it would support ‘go to page ___’ function), and it should not be used as a replacement/substitute for the LABEL attribute. To understand the differences between ORDER, ORDERLABEL and LABEL, imagine a text with 10 roman numbered pages followed by 10 arabic numbered pages. Page iii would have an ORDER of “3”, an ORDERLABEL of “iii” and a LABEL of “Page iii”, while page 3 would have an ORDER of “13”, an ORDERLABEL of “3” and a LABEL of “Page 3”.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"ORDERLABEL (string/O): A representation of the element's order among its siblings (e.g., “xii”), or of any non-integer native numbering system. It is presumed that this value will still be machine actionable (e.g., it would support ‘go to page ___’ function), and it should not be used as a replacement/substitute for the LABEL attribute. To understand the differences between ORDER, ORDERLABEL and LABEL, imagine a text with 10 roman numbered pages followed by 10 arabic numbered pages. Page iii would have an ORDER of “3”, an ORDERLABEL of “iii” and a LABEL of “Page iii”, while page 3 would have an ORDER of “13”, an ORDERLABEL of “3” and a LABEL of “Page 3”.")]
        [System.Xml.Serialization.XmlAttributeAttribute("ORDERLABEL")]
        public string Orderlabel { get; set; }

        /// <summary>
        /// <para xml:lang="en">LABEL (string/O): An attribute used, for example, to identify a &lt;div&gt; to an end user viewing the document. Thus a hierarchical arrangement of the &lt;div&gt; LABEL values could provide a table of contents to the digital content represented by a METS document and facilitate the users’ navigation of the digital object. Note that a &lt;div&gt; LABEL should be specific to its level in the structural map. In the case of a book with chapters, the book &lt;div&gt; LABEL should have the book title and the chapter &lt;div&gt;; LABELs should have the individual chapter titles, rather than having the chapter &lt;div&gt; LABELs combine both book title and chapter title . For further of the distinction between LABEL and ORDERLABEL see the description of the ORDERLABEL attribute.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"LABEL (string/O): An attribute used, for example, to identify a &lt;div&gt; to an end user viewing the document. Thus a hierarchical arrangement of the &lt;div&gt; LABEL values could provide a table of contents to the digital content represented by a METS document and facilitate the users’ navigation of the digital object. Note that a &lt;div&gt; LABEL should be specific to its level in the structural map. In the case of a book with chapters, the book &lt;div&gt; LABEL should have the book title and the chapter &lt;div&gt;; LABELs should have the individual chapter titles, rather than having the chapter &lt;div&gt; LABELs combine both book title and chapter title . For further of the distinction between LABEL and ORDERLABEL see the description of the ORDERLABEL attribute.")]
        [System.Xml.Serialization.XmlAttributeAttribute("LABEL")]
        public string Label { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _dmdid;

        /// <summary>
        /// <para xml:lang="en">DMDID (IDREFS/O): Contains the ID attribute values identifying the &lt;dmdSec&gt;, elements in the METS document that contain or link to descriptive metadata pertaining to the structural division represented by the current &lt;div&gt; element.  For more information on using METS IDREFS and IDREF type attributes for internal linking, see Chapter 4 of the METS Primer.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"DMDID (IDREFS/O): Contains the ID attribute values identifying the &lt;dmdSec&gt;, elements in the METS document that contain or link to descriptive metadata pertaining to the structural division represented by the current &lt;div&gt; element. For more information on using METS IDREFS and IDREF type attributes for internal linking, see Chapter 4 of the METS Primer.")]
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

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _admid;

        /// <summary>
        /// <para xml:lang="en">ADMID (IDREFS/O): Contains the ID attribute values identifying the &lt;rightsMD&gt;, &lt;sourceMD&gt;, &lt;techMD&gt; and/or &lt;digiprovMD&gt; elements within the &lt;amdSec&gt; of the METS document that contain or link to administrative metadata pertaining to the structural division represented by the &lt;div&gt; element. Typically the &lt;div&gt; ADMID attribute would be used to identify the &lt;rightsMD&gt; element or elements that pertain to the &lt;div&gt;, but it could be used anytime there was a need to link a &lt;div&gt; with pertinent administrative metadata. For more information on using METS IDREFS and IDREF type attributes for internal linking, see Chapter 4 of the METS Primer.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"ADMID (IDREFS/O): Contains the ID attribute values identifying the &lt;rightsMD&gt;, &lt;sourceMD&gt;, &lt;techMD&gt; and/or &lt;digiprovMD&gt; elements within the &lt;amdSec&gt; of the METS document that contain or link to administrative metadata pertaining to the structural division represented by the &lt;div&gt; element. Typically the &lt;div&gt; ADMID attribute would be used to identify the &lt;rightsMD&gt; element or elements that pertain to the &lt;div&gt;, but it could be used anytime there was a need to link a &lt;div&gt; with pertinent administrative metadata. For more information on using METS IDREFS and IDREF type attributes for internal linking, see Chapter 4 of the METS Primer.")]
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

        /// <summary>
        /// <para xml:lang="en">TYPE (string/O): An attribute that specifies the type of structural division that the &lt;div&gt; element represents. Possible &lt;div&gt; TYPE attribute values include: chapter, article, page, track, segment, section etc. METS places no constraints on the possible TYPE values. Suggestions for controlled vocabularies for TYPE may be found on the METS website.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"TYPE (string/O): An attribute that specifies the type of structural division that the &lt;div&gt; element represents. Possible &lt;div&gt; TYPE attribute values include: chapter, article, page, track, segment, section etc. METS places no constraints on the possible TYPE values. Suggestions for controlled vocabularies for TYPE may be found on the METS website.")]
        [System.Xml.Serialization.XmlAttributeAttribute("TYPE")]
        public string Type { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _contentids;

        /// <summary>
        /// <para xml:lang="en">CONTENTIDS (URI/O): Content IDs for the content represented by the &lt;div&gt; (equivalent to DIDL DII or Digital Item Identifier, a unique external ID).</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("CONTENTIDS (URI/O): Content IDs for the content represented by the &lt;div&gt; (e" +
            "quivalent to DIDL DII or Digital Item Identifier, a unique external ID).")]
        [System.Xml.Serialization.XmlAttributeAttribute("CONTENTIDS")]
        public System.Collections.ObjectModel.Collection<string> Contentids
        {
            get
            {
                return _contentids;
            }
            private set
            {
                _contentids = value;
            }
        }

        /// <summary>
        /// <para xml:lang="en">Gets a value indicating whether the Contentids collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ContentidsSpecified
        {
            get
            {
                return (this.Contentids.Count != 0);
            }
        }

        /// <summary>
        /// <para xml:lang="en">xlink:label - an xlink label to be referred to by an smLink element</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("xlink:label - an xlink label to be referred to by an smLink element")]
        [System.Xml.Serialization.XmlAttributeAttribute("label", Namespace = "http://www.w3.org/1999/xlink", Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string Label1 { get; set; }
    }
}
