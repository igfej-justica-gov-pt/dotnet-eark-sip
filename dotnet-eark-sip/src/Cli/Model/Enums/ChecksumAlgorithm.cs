using Mets;

public class ChecksumAlgorithm {
  public static readonly ChecksumAlgorithm ADLER32 = new(IFilecoreChecksumtype.ADLER32);
  public static readonly ChecksumAlgorithm CRC32 = new(IFilecoreChecksumtype.CRC32);
  public static readonly ChecksumAlgorithm HAVAL = new(IFilecoreChecksumtype.HAVAL);
  public static readonly ChecksumAlgorithm MD5 = new(IFilecoreChecksumtype.MD5);
  public static readonly ChecksumAlgorithm MNP = new(IFilecoreChecksumtype.MNP);
  public static readonly ChecksumAlgorithm SHA1 = new(IFilecoreChecksumtype.SHA1);
  public static readonly ChecksumAlgorithm SHA256 = new(IFilecoreChecksumtype.SHA256);
  public static readonly ChecksumAlgorithm SHA384 = new(IFilecoreChecksumtype.SHA384);
  public static readonly ChecksumAlgorithm SHA512 = new(IFilecoreChecksumtype.SHA512);
  public static readonly ChecksumAlgorithm TIGER = new(IFilecoreChecksumtype.TIGER);
  public static readonly ChecksumAlgorithm WHIRLPOOL = new(IFilecoreChecksumtype.WHIRLPOOL);

  public IFilecoreChecksumtype Algorithm { get; }

  public ChecksumAlgorithm(string algorithm) {
    Algorithm = (IFilecoreChecksumtype)Enum.Parse(typeof(IFilecoreChecksumtype), algorithm);
  }

  private ChecksumAlgorithm(IFilecoreChecksumtype algorithm) {
    Algorithm = algorithm;
  }

  public override string ToString() => Algorithm.ToString();
}