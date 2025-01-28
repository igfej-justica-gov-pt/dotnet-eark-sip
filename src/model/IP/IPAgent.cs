using Mets;

namespace IP {
  [Serializable]
  public class IPAgent {
    private string name;
    private string role;
    private MetsTypeMetsHdrAgentType type;
    private string otherRole;
    private string otherType;
    private string note;
    private IPAgentNoteTypeEnum noteType;

    public IPAgent() {
      name = "";
      role = "";
      type = MetsTypeMetsHdrAgentType.Other;
      otherRole = "";
      otherType = "";
      note = "";
      noteType = IPAgentNoteTypeEnum.NOT_SET;
    }

    public IPAgent(string name, string role, string otherRole, MetsTypeMetsHdrAgentType type, string otherType, string note, IPAgentNoteTypeEnum noteType) {
      this.name = name;
      this.role = role;
      this.type = type;
      this.otherRole = otherRole;
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

    public string GetRole() {
      return role;
    }

    public IPAgent SetRole(string role) {
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

    public string GetOtherRole() {
      return otherRole;
    }

    public IPAgent SetOtherRole(string otherRole) {
      this.otherRole = otherRole;
      return this;
    }

    public string GetOtherType() {
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

    public IPAgentNoteTypeEnum GetNoteType() {
      return noteType;
    }

    public IPAgent SetNoteType(IPAgentNoteTypeEnum noteType) {
      this.noteType = noteType;
      return this;
    }

    public override string ToString()
    {
      return "IPAgent [name=" + name + ", role=" + role + ", type=" + type.ToString() + ", otherRole=" + otherRole + ", otherType=" + otherType + ", note=" + note + "]";
    }
  }
}