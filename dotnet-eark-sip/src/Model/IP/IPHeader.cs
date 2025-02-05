

using IPEnums;

namespace IP {
  [Serializable]
  public class IPHeader {
    private DateTime? createDate;
    private DateTime? modificationDate;
    private IPStatus status;
    private List<IPAgent> agents;
    private readonly List<IPAltRecordID> altRecordIDs;

    public IPHeader() {
      createDate = DateTime.Now;
      modificationDate = null;
      status = IPStatus.NEW;
      agents = new List<IPAgent>();
      altRecordIDs = new List<IPAltRecordID>();
    }

    public IPHeader(IPStatus status) {
      this.status = status;
      createDate = DateTime.Now;
      modificationDate = null;
      agents = new List<IPAgent>();
      altRecordIDs = new List<IPAltRecordID>();
    }

    public DateTime? GetCreateDate() {
      return createDate;
    }

    public IPHeader SetCreateDate(DateTime? createDate) {
      this.createDate = createDate;
      return this;
    }

    public DateTime? GetModificationDate() {
      return modificationDate;
    }

    public IPHeader SetModificationDate(DateTime? modificationDate) {
      this.modificationDate = modificationDate;
      return this;
    }

    public IPStatus GetStatus() {
      return status;
    }

    public IPHeader SetStatus(IPStatus status) {
      this.status = status;
      return this;
    }

    public List<IPAltRecordID> GetAltRecordIDs() {
      return altRecordIDs;
    }

    public IPHeader addAltRecordID(IPAltRecordID altRecordID) {
      altRecordIDs.Add(altRecordID);
      return this;
    }

    public List<IPAgent> GetAgents() {
      return agents;
    }

    public IPHeader SetAgents(List<IPAgent> agents) {
      this.agents = agents;
      return this;
    }

    public IPHeader AddAgent(IPAgent ipAgent) {
      agents.Add(ipAgent);
      return this;
    }

    public override string ToString()
    {
      return "IPHeader [createDate=" + createDate +
        ", modificationDate=" + modificationDate +
        ", status=" + status +
        ", altRecordIDs=[" + string.Join(", ", altRecordIDs) + "]" +
        ", agents=[" + string.Join(", ", agents) + "]" +
      "]";
    }
  }
}