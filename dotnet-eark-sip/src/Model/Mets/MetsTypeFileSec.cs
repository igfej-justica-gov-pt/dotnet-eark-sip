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
    /// Represents the file section of a METS (Metadata Encoding and Transmission Standard) document.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1174.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("MetsTypeFileSec", Namespace = "http://www.loc.gov/METS/", AnonymousType = true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MetsTypeFileSec
    {

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<MetsTypeFileSecFileGrp> _fileGrp;

        /// <summary>
        /// <para xml:lang="en">A sequence of file group elements &lt;fileGrp&gt; can be used group the digital files comprising the content of a METS object either into a flat arrangement or, because each file group element can itself contain one or more  file group elements,  into a nested (hierarchical) arrangement. In the case where the content files are images of different formats and resolutions, for example, one could group the image content files by format and create a separate &lt;fileGrp&gt; for each image format/resolution such as:
        ///-- one &lt;fileGrp&gt; for the thumbnails of the images
        ///-- one &lt;fileGrp&gt; for the higher resolution JPEGs of the image 
        ///-- one &lt;fileGrp&gt; for the master archival TIFFs of the images 
        ///For a text resource with a variety of content file types one might group the content files at the highest level by type,  and then use the &lt;fileGrp&gt; element’s nesting capabilities to subdivide a &lt;fileGrp&gt; by format within the type, such as:
        ///-- one &lt;fileGrp&gt; for all of the page images with nested &lt;fileGrp&gt; elements for each image format/resolution (tiff, jpeg, gif)
        ///-- one &lt;fileGrp&gt; for a PDF version of all the pages of the document 
        ///-- one &lt;fileGrp&gt; for  a TEI encoded XML version of the entire document or each of its pages.
        ///A &lt;fileGrp&gt; may contain zero or more &lt;fileGrp&gt; elements and or &lt;file&gt; elements.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute(@"A sequence of file group elements &lt;fileGrp&gt; can be used group the digital files comprising the content of a METS object either into a flat arrangement or, because each file group element can itself contain one or more file group elements, into a nested (hierarchical) arrangement. In the case where the content files are images of different formats and resolutions, for example, one could group the image content files by format and create a separate &lt;fileGrp&gt; for each image format/resolution such as: -- one &lt;fileGrp&gt; for the thumbnails of the images -- one &lt;fileGrp&gt; for the higher resolution JPEGs of the image -- one &lt;fileGrp&gt; for the master archival TIFFs of the images For a text resource with a variety of content file types one might group the content files at the highest level by type, and then use the &lt;fileGrp&gt; element’s nesting capabilities to subdivide a &lt;fileGrp&gt; by format within the type, such as: -- one &lt;fileGrp&gt; for all of the page images with nested &lt;fileGrp&gt; elements for each image format/resolution (tiff, jpeg, gif) -- one &lt;fileGrp&gt; for a PDF version of all the pages of the document -- one &lt;fileGrp&gt; for a TEI encoded XML version of the entire document or each of its pages. A &lt;fileGrp&gt; may contain zero or more &lt;fileGrp&gt; elements and or &lt;file&gt; elements.")]
        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        [System.Xml.Serialization.XmlElementAttribute("fileGrp")]
        public System.Collections.ObjectModel.Collection<MetsTypeFileSecFileGrp> FileGrp
        {
            get
            {
                return _fileGrp;
            }
            private set
            {
                _fileGrp = value;
            }
        }

        /// <summary>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="MetsTypeFileSec" /> class.</para>
        /// </summary>
        public MetsTypeFileSec()
        {
            this._fileGrp = new System.Collections.ObjectModel.Collection<MetsTypeFileSecFileGrp>();
            this._anyAttribute = new System.Collections.ObjectModel.Collection<System.Xml.XmlAttribute>();
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
