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
    /// <para xml:lang="en">mdSecType: Complex Type for Metadata Sections
    ///			A generic framework for pointing to/including metadata within a METS document, a la Warwick Framework.</para>
    /// </summary>
    [System.ComponentModel.DescriptionAttribute("mdSecType: Complex Type for Metadata Sections A generic framework for pointing to" +
        "/including metadata within a METS document, a la Warwick Framework.")]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1174.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("mdSecType", Namespace="http://www.loc.gov/METS/")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MdSecType
    {
        
        /// <summary>
        /// <para xml:lang="en">The metadata reference element &lt;mdRef&gt; element is a generic element used throughout the METS schema to provide a pointer to metadata which resides outside the METS document.  NB: &lt;mdRef&gt; is an empty element.  The location of the metadata must be recorded in the xlink:href attribute, supplemented by the XPTR attribute as needed.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"The metadata reference element &lt;mdRef&gt; element is a generic element used throughout the METS schema to provide a pointer to metadata which resides outside the METS document. NB: &lt;mdRef&gt; is an empty element. The location of the metadata must be recorded in the xlink:href attribute, supplemented by the XPTR attribute as needed.")]
        [System.Xml.Serialization.XmlElementAttribute("mdRef")]
        public MdSecTypeMdRef MdRef { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">A metadata wrapper element &lt;mdWrap&gt; provides a wrapper around metadata embedded within a METS document. The element is repeatable. Such metadata can be in one of two forms: 1) XML-encoded metadata, with the XML-encoding identifying itself as belonging to a namespace other than the METS document namespace. 2) Any arbitrary binary or textual form, PROVIDED that the metadata is Base64 encoded and wrapped in a &lt;binData&gt; element within the internal descriptive metadata element.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"A metadata wrapper element &lt;mdWrap&gt; provides a wrapper around metadata embedded within a METS document. The element is repeatable. Such metadata can be in one of two forms: 1) XML-encoded metadata, with the XML-encoding identifying itself as belonging to a namespace other than the METS document namespace. 2) Any arbitrary binary or textual form, PROVIDED that the metadata is Base64 encoded and wrapped in a &lt;binData&gt; element within the internal descriptive metadata element.")]
        [System.Xml.Serialization.XmlElementAttribute("mdWrap")]
        public MdSecTypeMdWrap MdWrap { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">ID (ID/R): This attribute uniquely identifies the element within the METS document, and would allow the element to be referenced unambiguously from another element or document via an IDREF or an XPTR. The ID attribute on the &lt;dmdSec&gt;, &lt;techMD&gt;, &lt;sourceMD&gt;, &lt;rightsMD&gt; and &lt;digiprovMD&gt; elements (which are all of mdSecType) is required, and its value should be referenced from one or more DMDID attributes (when the ID identifies a &lt;dmdSec&gt; element) or ADMID attributes (when the ID identifies a &lt;techMD&gt;, &lt;sourceMD&gt;, &lt;rightsMD&gt; or &lt;digiprovMD&gt; element) that are associated with other elements in the METS document. The following elements support references to a &lt;dmdSec&gt; via a DMDID attribute: &lt;file&gt;, &lt;stream&gt;, &lt;div&gt;.  The following elements support references to &lt;techMD&gt;, &lt;sourceMD&gt;, &lt;rightsMD&gt; and &lt;digiprovMD&gt; elements via an ADMID attribute: &lt;metsHdr&gt;, &lt;dmdSec&gt;, &lt;techMD&gt;, &lt;sourceMD&gt;, &lt;rightsMD&gt;, &lt;digiprovMD&gt;, &lt;fileGrp&gt;, &lt;file&gt;, &lt;stream&gt;, &lt;div&gt;, &lt;area&gt;, &lt;behavior&gt;. For more information on using ID attributes for internal and external linking see Chapter 4 of the METS Primer.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"ID (ID/R): This attribute uniquely identifies the element within the METS document, and would allow the element to be referenced unambiguously from another element or document via an IDREF or an XPTR. The ID attribute on the &lt;dmdSec&gt;, &lt;techMD&gt;, &lt;sourceMD&gt;, &lt;rightsMD&gt; and &lt;digiprovMD&gt; elements (which are all of mdSecType) is required, and its value should be referenced from one or more DMDID attributes (when the ID identifies a &lt;dmdSec&gt; element) or ADMID attributes (when the ID identifies a &lt;techMD&gt;, &lt;sourceMD&gt;, &lt;rightsMD&gt; or &lt;digiprovMD&gt; element) that are associated with other elements in the METS document. The following elements support references to a &lt;dmdSec&gt; via a DMDID attribute: &lt;file&gt;, &lt;stream&gt;, &lt;div&gt;. The following elements support references to &lt;techMD&gt;, &lt;sourceMD&gt;, &lt;rightsMD&gt; and &lt;digiprovMD&gt; elements via an ADMID attribute: &lt;metsHdr&gt;, &lt;dmdSec&gt;, &lt;techMD&gt;, &lt;sourceMD&gt;, &lt;rightsMD&gt;, &lt;digiprovMD&gt;, &lt;fileGrp&gt;, &lt;file&gt;, &lt;stream&gt;, &lt;div&gt;, &lt;area&gt;, &lt;behavior&gt;. For more information on using ID attributes for internal and external linking see Chapter 4 of the METS Primer.")]
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings=true)]
        [System.Xml.Serialization.XmlAttributeAttribute("ID")]
        public string Id { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">GROUPID (string/O): This identifier is used to indicate that different metadata sections may be considered as part of a group. Two metadata sections with the same GROUPID value are to be considered part of the same group. For example this facility might be used to group changed versions of the same metadata if previous versions are maintained in a file for tracking purposes.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"GROUPID (string/O): This identifier is used to indicate that different metadata sections may be considered as part of a group. Two metadata sections with the same GROUPID value are to be considered part of the same group. For example this facility might be used to group changed versions of the same metadata if previous versions are maintained in a file for tracking purposes.")]
        [System.Xml.Serialization.XmlAttributeAttribute("GROUPID")]
        public string Groupid { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<string> _admid;
        
        /// <summary>
        /// <para xml:lang="en">ADMID (IDREFS/O): Contains the ID attribute values of the &lt;digiprovMD&gt;, &lt;techMD&gt;, &lt;sourceMD&gt; and/or &lt;rightsMD&gt; elements within the &lt;amdSec&gt; of the METS document that contain administrative metadata pertaining to the current mdSecType element. Typically used in this context to reference preservation metadata (digiprovMD) which applies to the current metadata. For more information on using METS IDREFS and IDREF type attributes for internal linking, see Chapter 4 of the METS Primer.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"ADMID (IDREFS/O): Contains the ID attribute values of the &lt;digiprovMD&gt;, &lt;techMD&gt;, &lt;sourceMD&gt; and/or &lt;rightsMD&gt; elements within the &lt;amdSec&gt; of the METS document that contain administrative metadata pertaining to the current mdSecType element. Typically used in this context to reference preservation metadata (digiprovMD) which applies to the current metadata. For more information on using METS IDREFS and IDREF type attributes for internal linking, see Chapter 4 of the METS Primer.")]
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
        /// <para xml:lang="en">Initializes a new instance of the <see cref="MdSecType" /> class.</para>
        /// </summary>
        public MdSecType()
        {
            this._admid = new System.Collections.ObjectModel.Collection<string>();
            this._anyAttribute = new System.Collections.ObjectModel.Collection<System.Xml.XmlAttribute>();
        }
        
        /// <summary>
        /// <para xml:lang="en">CREATED (dateTime/O): Specifies the date and time of creation for the metadata.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("CREATED (dateTime/O): Specifies the date and time of creation for the metadata.")]
        [System.Xml.Serialization.XmlAttributeAttribute("CREATED", DataType="dateTime")]
        public System.DateTime Created { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">Gets or sets a value indicating whether the Created property is specified.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CreatedSpecified { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">STATUS (string/O): Indicates the status of this metadata (e.g., superseded, current, etc.).</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("STATUS (string/O): Indicates the status of this metadata (e.g., superseded, curre" +
            "nt, etc.).")]
        [System.Xml.Serialization.XmlAttributeAttribute("STATUS")]
        public string Status { get; set; }
        
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
