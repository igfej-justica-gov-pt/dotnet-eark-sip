using Mets;
using Xml.Mets.CsipExtensionMets;

namespace IP {
  [Serializable]
  public class IPAgent {
    private string name;
    private MetsTypeMetsHdrAgentRole role;
    private string? otherRole;
    private MetsTypeMetsHdrAgentType type;
    private string? otherType;
    private string note;
    private Notetype? noteType;

    public IPAgent() {
      name = "";
      role = MetsTypeMetsHdrAgentRole.OTHER;
      type = MetsTypeMetsHdrAgentType.OTHER;
      note = "";
    }

    public IPAgent(string name, MetsTypeMetsHdrAgentRole role, string? otherRole, MetsTypeMetsHdrAgentType type, string? otherType, string note, Notetype noteType) {
      this.name = name;
      this.role = role;
      this.otherRole = otherRole;
      this.type = type;
      this.otherType = otherType;
      this.note = note;
      this.noteType = noteType;
    }

    public string GetName() {
      return name;
    }

    public IPAgent SetName(string name) {
      this.name = name;
      return this;
    }

    public MetsTypeMetsHdrAgentRole GetRole() {
      return role;
    }

    public IPAgent SetRole(MetsTypeMetsHdrAgentRole role) {
      this.role = role;
      return this;
    }

    public MetsTypeMetsHdrAgentType _GetType() {
      return type;
    }

    public IPAgent SetType(MetsTypeMetsHdrAgentType type) {
      this.type = type;
      return this;
    }

    public string? GetOtherRole() {
      return otherRole;
    }

    public IPAgent SetOtherRole(string otherRole) {
      this.otherRole = otherRole;
      return this;
    }

    public string? GetOtherType() {
      return otherType;
    }

    public IPAgent SetOtherType(string otherType) {
      this.otherType = otherType;
      return this;
    }

    public string GetNote() {
      return note;
    }

    public IPAgent SetNote(string note) {
      this.note = note;
      return this;
    }

    public Notetype? GetNoteType() {
      return noteType;
    }

    public IPAgent SetNoteType(Notetype? noteType) {
      this.noteType = noteType;
      return this;
    }

    private string GetNoteTypeString() {
      Notetype value = noteType ?? Notetype.IDENTIFICATIONCODE;

      if (noteType == null) return "";
      else return EnumUtils.GetXmlEnumName(value);
    }

    public override string ToString()
    {
      return "IPAgent [" + 
        "name=" + name + 
        ", role=" + EnumUtils.GetXmlEnumName(role) + 
        ", type=" + EnumUtils.GetXmlEnumName(type) + 
        ", otherRole=" + otherRole ?? " " + 
        ", otherType=" + otherType ?? " " + 
        ", note=" + note + 
        ", noteType=" + GetNoteTypeString() + 
      "]";
    }
  }
}