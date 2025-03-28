/// <summary>
/// Represents a structure map division with a label and file location.
/// </summary>
public class StructMapDiv
{
  /// <summary>
  /// Gets the label of the structure map division.
  /// </summary>

  public string Label { get; private set; }
  /// <summary>
  /// Gets or sets the file location associated with the structure map division.
  /// </summary>
  public string FileLocation { get; set; }

  /// <summary>
  /// Initializes a new instance of the <see cref="StructMapDiv"/> class with the specified label.
  /// </summary>
  /// <param name="label">The label of the structure map division.</param>
  public StructMapDiv(string label)
  {
    Label = label;
    FileLocation = "";
  }

  /// <summary>
  /// Returns the hash code for this instance.
  /// </summary>
  /// <returns>The hash code for this instance.</returns>
  public override int GetHashCode()
  {
    return base.GetHashCode();
  }

  /// <summary>
  /// Determines whether the specified object is equal to the current object.
  /// </summary>
  /// <param name="obj">The object to compare with the current object.</param>
  /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
  public override bool Equals(object obj)
  {
    if (this == obj) return true;
    if (obj == null || obj.GetType() != this.GetType()) return false;

    StructMapDiv structMapDiv = (StructMapDiv)obj;
    return Label.Equals(structMapDiv.Label);
  }
}