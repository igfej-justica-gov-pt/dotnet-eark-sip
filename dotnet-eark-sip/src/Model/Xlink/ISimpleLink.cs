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
namespace Xlink
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.1.1174.0")]
    public partial interface ISimpleLink
    {
        
        [System.ComponentModel.DefaultValueAttribute("simple")]
        string Type
        {
            get;
            set;
        }
        
        string Href
        {
            get;
            set;
        }
        
        string Role
        {
            get;
            set;
        }
        
        string Arcrole
        {
            get;
            set;
        }
        
        string Title
        {
            get;
            set;
        }
        
        Show Show
        {
            get;
            set;
        }
        
        Actuate Actuate
        {
            get;
            set;
        }
    }
}
