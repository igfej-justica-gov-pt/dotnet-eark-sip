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
    /// <para xml:lang="en">structMapType: Complex Type for Structural Maps
    ///			The structural map (structMap) outlines a hierarchical structure for the original object being encoded, using a series of nested div elements.</para>
    /// </summary>
    [System.ComponentModel.DescriptionAttribute("structMapType: Complex Type for Structural Maps The structural map (structMap) ou" +
        "tlines a hierarchical structure for the original object being encoded, using a s" +
        "eries of nested div elements.")]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1174.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("structMapType", Namespace = "http://www.loc.gov/METS/")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class StructMapType
    {

        /// <summary>
        /// <para xml:lang="en">The structural divisions of the hierarchical organization provided by a &lt;structMap&gt; are represented by division &lt;div&gt; elements, which can be nested to any depth. Each &lt;div&gt; element can represent either an intellectual (logical) division or a physical division. Every &lt;div&gt; node in the structural map hierarchy may be connected (via subsidiary &lt;mptr&gt; or &lt;fptr&gt; elements) to content files which represent that div's portion of the whole document.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"The structural divisions of the hierarchical organization provided by a &lt;structMap&gt; are represented by division &lt;div&gt; elements, which can be nested to any depth. Each &lt;div&gt; element can represent either an intellectual (logical) division or a physical division. Every &lt;div&gt; node in the structural map hierarchy may be connected (via subsidiary &lt;mptr&gt; or &lt;fptr&gt; elements) to content files which represent that div's portion of the whole document.")]
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        [System.Xml.Serialization.XmlElementAttribute("div")]
        public DivType Div { get; set; }

        /// <summary>
        /// <para xml:lang="en">ID (ID/O): This attribute uniquely identifies the element within the METS document, and would allow the element to be referenced unambiguously from another element or document via an IDREF or an XPTR. For more information on using ID attributes for internal and external linking see Chapter 4 of the METS Primer.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"ID (ID/O): This attribute uniquely identifies the element within the METS document, and would allow the element to be referenced unambiguously from another element or document via an IDREF or an XPTR. For more information on using ID attributes for internal and external linking see Chapter 4 of the METS Primer.")]
        [System.Xml.Serialization.XmlAttributeAttribute("ID")]
        public string Id { get; set; }

        /// <summary>
        /// <para xml:lang="en">TYPE (string/O): Identifies the type of structure represented by the &lt;structMap&gt;. For example, a &lt;structMap&gt; that represented a purely logical or intellectual structure could be assigned a TYPE value of “logical” whereas a &lt;structMap&gt; that represented a purely physical structure could be assigned a TYPE value of “physical”. However, the METS schema neither defines nor requires a common vocabulary for this attribute. A METS profile, however, may well constrain the values for the &lt;structMap&gt; TYPE.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"TYPE (string/O): Identifies the type of structure represented by the &lt;structMap&gt;. For example, a &lt;structMap&gt; that represented a purely logical or intellectual structure could be assigned a TYPE value of “logical” whereas a &lt;structMap&gt; that represented a purely physical structure could be assigned a TYPE value of “physical”. However, the METS schema neither defines nor requires a common vocabulary for this attribute. A METS profile, however, may well constrain the values for the &lt;structMap&gt; TYPE.")]
        [System.Xml.Serialization.XmlAttributeAttribute("TYPE")]
        public string Type { get; set; }

        /// <summary>
        /// <para xml:lang="en">LABEL (string/O): Describes the &lt;structMap&gt; to viewers of the METS document. This would be useful primarily where more than one &lt;structMap&gt; is provided for a single object. A descriptive LABEL value, in that case, could clarify to users the purpose of each of the available structMaps.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"LABEL (string/O): Describes the &lt;structMap&gt; to viewers of the METS document. This would be useful primarily where more than one &lt;structMap&gt; is provided for a single object. A descriptive LABEL value, in that case, could clarify to users the purpose of each of the available structMaps.")]
        [System.Xml.Serialization.XmlAttributeAttribute("LABEL")]
        public string Label { get; set; }

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

        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="StructMapType" /> class.</para>
        /// </summary>
        public StructMapType()
        {
            this._anyAttribute = new System.Collections.ObjectModel.Collection<System.Xml.XmlAttribute>();
        }
    }
}
