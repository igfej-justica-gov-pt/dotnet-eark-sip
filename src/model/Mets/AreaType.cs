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
    /// <para xml:lang="en">areaType: Complex Type for Area Linking
    ///				The area element provides for more sophisticated linking between a div element and content files representing that div, be they text, image, audio, or video files.  An area element can link a div to a point within a file, to a one-dimension segment of a file (e.g., text segment, image line, audio/video clip), or a two-dimensional section of a file 	(e.g, subsection of an image, or a subsection of the  video display of a video file.  The area element has no content; all information is recorded within its various attributes.</para>
    /// </summary>
    [System.ComponentModel.DescriptionAttribute(@"areaType: Complex Type for Area Linking The area element provides for more sophisticated linking between a div element and content files representing that div, be they text, image, audio, or video files. An area element can link a div to a point within a file, to a one-dimension segment of a file (e.g., text segment, image line, audio/video clip), or a two-dimensional section of a file (e.g, subsection of an image, or a subsection of the video display of a video file. The area element has no content; all information is recorded within its various attributes.")]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1174.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("areaType", Namespace="http://www.loc.gov/METS/")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AreaType : IOrderlabels
    {
        
        /// <summary>
        /// <para xml:lang="en">ID (ID/O): This attribute uniquely identifies the element within the METS document, and would allow the element to be referenced unambiguously from another element or document via an IDREF or an XPTR. For more information on using ID attributes for internal and external linking see Chapter 4 of the METS Primer.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"ID (ID/O): This attribute uniquely identifies the element within the METS document, and would allow the element to be referenced unambiguously from another element or document via an IDREF or an XPTR. For more information on using ID attributes for internal and external linking see Chapter 4 of the METS Primer.")]
        [System.Xml.Serialization.XmlAttributeAttribute("ID")]
        public string Id { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">FILEID (IDREF/R): An attribute which provides the XML ID value that identifies the &lt;file&gt; element in the &lt;fileSec&gt; that then points to and/or contains the digital content represented by the &lt;area&gt; element. It must contain an ID value represented in an ID attribute associated with a &lt;file&gt; element in the &lt;fileSec&gt; element in the same METS document.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"FILEID (IDREF/R): An attribute which provides the XML ID value that identifies the &lt;file&gt; element in the &lt;fileSec&gt; that then points to and/or contains the digital content represented by the &lt;area&gt; element. It must contain an ID value represented in an ID attribute associated with a &lt;file&gt; element in the &lt;fileSec&gt; element in the same METS document.")]
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlAttributeAttribute("FILEID")]
        public string Fileid { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">SHAPE (string/O): An attribute that can be used as in HTML to define the shape of the relevant area within the content file pointed to by the &lt;area&gt; element. Typically this would be used with image content (still image or video frame) when only a portion of an integal image map pertains. If SHAPE is specified then COORDS must also be present. SHAPE should be used in conjunction with COORDS in the manner defined for the shape and coords attributes on an HTML4 &lt;area&gt; element. SHAPE must contain one of the following values: 
        ///RECT 
        ///CIRCLE
        ///POLY</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"SHAPE (string/O): An attribute that can be used as in HTML to define the shape of the relevant area within the content file pointed to by the &lt;area&gt; element. Typically this would be used with image content (still image or video frame) when only a portion of an integal image map pertains. If SHAPE is specified then COORDS must also be present. SHAPE should be used in conjunction with COORDS in the manner defined for the shape and coords attributes on an HTML4 &lt;area&gt; element. SHAPE must contain one of the following values: RECT CIRCLE POLY")]
        [System.Xml.Serialization.XmlAttributeAttribute("SHAPE")]
        public AreaTypeShape Shape { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">Gets or sets a value indicating whether the Shape property is specified.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ShapeSpecified { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">COORDS (string/O): Specifies the coordinates in an image map for the shape of the pertinent area as specified in the SHAPE attribute. While technically optional, SHAPE and COORDS must both appear together to define the relevant area of image content. COORDS should be used in conjunction with SHAPE in the manner defined for the COORDs and SHAPE attributes on an HTML4 &lt;area&gt; element. COORDS must be a comma delimited string of integer value pairs representing coordinates (plus radius in the case of CIRCLE) within an image map. Number of coordinates pairs depends on shape: RECT: x1, y1, x2, y2; CIRC: x1, y1; POLY: x1, y1, x2, y2, x3, y3 . . .</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"COORDS (string/O): Specifies the coordinates in an image map for the shape of the pertinent area as specified in the SHAPE attribute. While technically optional, SHAPE and COORDS must both appear together to define the relevant area of image content. COORDS should be used in conjunction with SHAPE in the manner defined for the COORDs and SHAPE attributes on an HTML4 &lt;area&gt; element. COORDS must be a comma delimited string of integer value pairs representing coordinates (plus radius in the case of CIRCLE) within an image map. Number of coordinates pairs depends on shape: RECT: x1, y1, x2, y2; CIRC: x1, y1; POLY: x1, y1, x2, y2, x3, y3 . . .")]
        [System.Xml.Serialization.XmlAttributeAttribute("COORDS")]
        public string Coords { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">BEGIN (string/O): An attribute that specifies the point in the content file where the relevant section of content begins. It can be used in conjunction with either the END attribute or the EXTENT attribute as a means of defining the relevant portion of the referenced file precisely. It can only be interpreted meaningfully in conjunction with the BETYPE or EXTTYPE, which specify the kind of beginning/ending point values or beginning/extent values that are being used. The BEGIN attribute can be used with or without a companion END or EXTENT element. In this case, the end of the content file is assumed to be the end point.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"BEGIN (string/O): An attribute that specifies the point in the content file where the relevant section of content begins. It can be used in conjunction with either the END attribute or the EXTENT attribute as a means of defining the relevant portion of the referenced file precisely. It can only be interpreted meaningfully in conjunction with the BETYPE or EXTTYPE, which specify the kind of beginning/ending point values or beginning/extent values that are being used. The BEGIN attribute can be used with or without a companion END or EXTENT element. In this case, the end of the content file is assumed to be the end point.")]
        [System.Xml.Serialization.XmlAttributeAttribute("BEGIN")]
        public string Begin { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">END (string/O): An attribute that specifies the point in the content file where the relevant section of content ends. It can only be interpreted meaningfully in conjunction with the BETYPE, which specifies the kind of ending point values being used. Typically the END attribute would only appear in conjunction with a BEGIN element.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"END (string/O): An attribute that specifies the point in the content file where the relevant section of content ends. It can only be interpreted meaningfully in conjunction with the BETYPE, which specifies the kind of ending point values being used. Typically the END attribute would only appear in conjunction with a BEGIN element.")]
        [System.Xml.Serialization.XmlAttributeAttribute("END")]
        public string End { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">BETYPE: Begin/End Type.
        ///					BETYPE (string/O): An attribute that specifies the kind of BEGIN and/or END values that are being used. For example, if BYTE is specified, then the BEGIN and END point values represent the byte offsets into a file. If IDREF is specified, then the BEGIN element specifies the ID value that identifies the element in a structured text file where the relevant section of the file begins; and the END value (if present) would specify the ID value that identifies the element with which the relevant section of the file ends. Must be one of the following values: 
        ///BYTE
        ///IDREF
        ///SMIL
        ///MIDI
        ///SMPTE-25
        ///SMPTE-24
        ///SMPTE-DF30
        ///SMPTE-NDF30
        ///SMPTE-DF29.97
        ///SMPTE-NDF29.97
        ///TIME
        ///TCF
        ///XPTR</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"BETYPE: Begin/End Type. BETYPE (string/O): An attribute that specifies the kind of BEGIN and/or END values that are being used. For example, if BYTE is specified, then the BEGIN and END point values represent the byte offsets into a file. If IDREF is specified, then the BEGIN element specifies the ID value that identifies the element in a structured text file where the relevant section of the file begins; and the END value (if present) would specify the ID value that identifies the element with which the relevant section of the file ends. Must be one of the following values: BYTE IDREF SMIL MIDI SMPTE-25 SMPTE-24 SMPTE-DF30 SMPTE-NDF30 SMPTE-DF29.97 SMPTE-NDF29.97 TIME TCF XPTR")]
        [System.Xml.Serialization.XmlAttributeAttribute("BETYPE")]
        public AreaTypeBetype Betype { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">Gets or sets a value indicating whether the Betype property is specified.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BetypeSpecified { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">EXTENT (string/O): An attribute that specifies the extent of the relevant section of the content file. Can only be interpreted meaningfully in conjunction with the EXTTYPE which specifies the kind of value that is being used. Typically the EXTENT attribute would only appear in conjunction with a BEGIN element and would not be used if the BEGIN point represents an IDREF.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"EXTENT (string/O): An attribute that specifies the extent of the relevant section of the content file. Can only be interpreted meaningfully in conjunction with the EXTTYPE which specifies the kind of value that is being used. Typically the EXTENT attribute would only appear in conjunction with a BEGIN element and would not be used if the BEGIN point represents an IDREF.")]
        [System.Xml.Serialization.XmlAttributeAttribute("EXTENT")]
        public string Extent { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">EXTTYPE (string/O): An attribute that specifies the kind of EXTENT values that are being used. For example if BYTE is specified then EXTENT would represent a byte count. If TIME is specified the EXTENT would represent a duration of time. EXTTYPE must be one of the following values: 
        ///BYTE
        ///SMIL
        ///MIDI
        ///SMPTE-25
        ///SMPTE-24
        ///SMPTE-DF30
        ///SMPTE-NDF30
        ///SMPTE-DF29.97
        ///SMPTE-NDF29.97
        ///TIME
        ///TCF.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"EXTTYPE (string/O): An attribute that specifies the kind of EXTENT values that are being used. For example if BYTE is specified then EXTENT would represent a byte count. If TIME is specified the EXTENT would represent a duration of time. EXTTYPE must be one of the following values: BYTE SMIL MIDI SMPTE-25 SMPTE-24 SMPTE-DF30 SMPTE-NDF30 SMPTE-DF29.97 SMPTE-NDF29.97 TIME TCF.")]
        [System.Xml.Serialization.XmlAttributeAttribute("EXTTYPE")]
        public AreaTypeExttype Exttype { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">Gets or sets a value indicating whether the Exttype property is specified.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ExttypeSpecified { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _admid;
        
        /// <summary>
        /// <para xml:lang="en">ADMID (IDREFS/O): Contains the ID attribute values identifying the &lt;rightsMD&gt;, &lt;sourceMD&gt;, &lt;techMD&gt; and/or &lt;digiprovMD&gt; elements within the &lt;amdSec&gt; of the METS document that contain or link to administrative metadata pertaining to the content represented by the &lt;area&gt; element. Typically the &lt;area&gt; ADMID attribute would be used to identify the &lt;rightsMD&gt; element or elements that pertain to the &lt;area&gt;, but it could be used anytime there was a need to link an &lt;area&gt; with pertinent administrative metadata. For more information on using METS IDREFS and IDREF type attributes for internal linking, see Chapter 4 of the METS Primer</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"ADMID (IDREFS/O): Contains the ID attribute values identifying the &lt;rightsMD&gt;, &lt;sourceMD&gt;, &lt;techMD&gt; and/or &lt;digiprovMD&gt; elements within the &lt;amdSec&gt; of the METS document that contain or link to administrative metadata pertaining to the content represented by the &lt;area&gt; element. Typically the &lt;area&gt; ADMID attribute would be used to identify the &lt;rightsMD&gt; element or elements that pertain to the &lt;area&gt;, but it could be used anytime there was a need to link an &lt;area&gt; with pertinent administrative metadata. For more information on using METS IDREFS and IDREF type attributes for internal linking, see Chapter 4 of the METS Primer")]
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
        /// <para xml:lang="en">Initializes a new instance of the <see cref="AreaType" /> class.</para>
        /// </summary>
        public AreaType()
        {
            this._admid = new System.Collections.ObjectModel.Collection<string>();
            this._contentids = new System.Collections.ObjectModel.Collection<string>();
            this._anyAttribute = new System.Collections.ObjectModel.Collection<System.Xml.XmlAttribute>();
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _contentids;
        
        /// <summary>
        /// <para xml:lang="en">CONTENTIDS (URI/O): Content IDs for the content represented by the &lt;area&gt; (equivalent to DIDL DII or Digital Item Identifier, a unique external ID).</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("CONTENTIDS (URI/O): Content IDs for the content represented by the &lt;area&gt; (" +
            "equivalent to DIDL DII or Digital Item Identifier, a unique external ID).")]
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
        private System.Collections.ObjectModel.Collection<System.Xml.XmlAttribute> _anyAttribute;
        
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
