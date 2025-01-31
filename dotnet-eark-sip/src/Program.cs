using System.Xml.Serialization;
using Mets;

class Program {
  static void Main() {
    string xmlPath = ".\\Test\\Resources\\METS_example.xml";

    try {
      // Load the XML
      string xmlContent = File.ReadAllText(xmlPath);

      // Deserialize
      XmlSerializer serializer = new XmlSerializer(typeof(Mets.Mets));
      using (StringReader reader = new StringReader(xmlContent)) {
        Mets.Mets metsObject = (Mets.Mets)serializer.Deserialize(reader);
        Console.WriteLine("Deserialization successful");

        // Access data
        Console.WriteLine($"Created Date: {metsObject.MetsHdr.Createdate}");
        Console.WriteLine($"FileSec Use: {metsObject.FileSec.FileGrp[0].Use}");
      }
    } catch (Exception ex) {
      Console.WriteLine($"Error: {ex}");
    }
  }
}