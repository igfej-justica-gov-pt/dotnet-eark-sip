public class StructMapDiv {
  public string Label { get; private set; }
  public string? FileLocation { get; set; }

  public StructMapDiv(string label) {
    Label = label;
  }

  public override int GetHashCode() {
    return base.GetHashCode();
  }

  public override bool Equals(object obj) {
    if (this == obj) return true;
    if (obj == null || obj.GetType() != this.GetType()) return false;

    StructMapDiv structMapDiv = (StructMapDiv)obj;
    return Label.Equals(structMapDiv.Label);
  }
}