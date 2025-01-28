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
    public partial interface IFilecore
    {
        
        /// <summary>
        /// <para xml:lang="en">MIMETYPE (string/O): The IANA MIME media type for the associated file or wrapped content. Some values for this attribute can be found on the IANA website.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("MIMETYPE (string/O): The IANA MIME media type for the associated file or wrapped " +
            "content. Some values for this attribute can be found on the IANA website.")]
        string Mimetype
        {
            get;
            set;
        }
        
        /// <summary>
        /// <para xml:lang="en">SIZE (long/O): Specifies the size in bytes of the associated file or wrapped content.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("SIZE (long/O): Specifies the size in bytes of the associated file or wrapped cont" +
            "ent.")]
        long Size
        {
            get;
            set;
        }
        
        /// <summary>
        /// <para xml:lang="en">CREATED (dateTime/O): Specifies the date and time of creation for the associated file or wrapped content.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("CREATED (dateTime/O): Specifies the date and time of creation for the associated " +
            "file or wrapped content.")]
        System.DateTime Created
        {
            get;
            set;
        }
        
        /// <summary>
        /// <para xml:lang="en">CHECKSUM (string/O): Provides a checksum value for the associated file or wrapped content.</para>
        /// </summary>
        [System.ComponentModel.DescriptionAttribute("CHECKSUM (string/O): Provides a checksum value for the associated file or wrapped" +
            " content.")]
        string Checksum
        {
            get;
            set;
        }
        
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
        IFilecoreChecksumtype Checksumtype
        {
            get;
            set;
        }
    }
}
