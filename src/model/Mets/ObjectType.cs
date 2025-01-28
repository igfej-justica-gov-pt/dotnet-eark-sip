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
    /// <para xml:lang="en">objectType: complexType for interfaceDef and mechanism elements
    ///				The mechanism and behavior elements point to external objects--an interface definition object or an executable code object respectively--which together constitute a behavior that can be applied to one or more &lt;div&gt; elements in a &lt;structMap&gt;.</para>
    /// </summary>
    [System.ComponentModel.DescriptionAttribute(@"objectType: complexType for interfaceDef and mechanism elements The mechanism and behavior elements point to external objects--an interface definition object or an executable code object respectively--which together constitute a behavior that can be applied to one or more &lt;div&gt; elements in a &lt;structMap&gt;.")]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1174.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("objectType", Namespace="http://www.loc.gov/METS/")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ObjectType : ILocation, global::Xlink.ISimpleLink
    {
        
        /// <summary>
        /// <para xml:lang="en">ID (ID/O): This attribute uniquely identifies the element within the METS document, and would allow the element to be referenced unambiguously from another element or document via an IDREF or an XPTR. For more information on using ID attributes for internal and external linking see Chapter 4 of the METS Primer.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"ID (ID/O): This attribute uniquely identifies the element within the METS document, and would allow the element to be referenced unambiguously from another element or document via an IDREF or an XPTR. For more information on using ID attributes for internal and external linking see Chapter 4 of the METS Primer.")]
        [System.Xml.Serialization.XmlAttributeAttribute("ID")]
        public string Id { get; set; }
        
        /// <summary>
        /// <para xml:lang="en">LABEL (string/O): A text description of the entity represented.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("LABEL (string/O): A text description of the entity represented.")]
        [System.Xml.Serialization.XmlAttributeAttribute("LABEL")]
        public string Label { get; set; }
        
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
    }
}
