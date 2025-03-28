namespace IP
{
  /// <summary>
  /// Represents an alternative record ID with a value and type.
  /// </summary>
  [Serializable]
  public class IPAltRecordID
  {
    private string value = "";
    private string type = "";

    public IPAltRecordID() { }

    public string GetValue()
    {
      return value;
    }

    public IPAltRecordID SetValue(string value)
    {
      this.value = value;
      return this;
    }

    public string _GetType()
    {
      return type;
    }

    public IPAltRecordID SetType(string type)
    {
      this.type = type;
      return this;
    }

    public override string ToString()
    {
      return "IPAltRecordID [value=" + value + ", type=" + type + "]";
    }
  }
}