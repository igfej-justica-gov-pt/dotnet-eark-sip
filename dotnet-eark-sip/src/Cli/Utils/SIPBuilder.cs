using IP;
using Mets;

public class SIPBuilder {
  private List<MetadataGroup> metadataArgs = new();
  private List<RepresentationGroup> representationArgs = new();
  private bool targetOnly;
  private CSIPVersion version = CSIPVersion.V220;
  private string path;
  private string submitterAgentName;
  private string submitterAgentId;
  private string sipId;
  private List<string> ancestors;
  private IFilecoreChecksumtype checksumAlgorithm = IFilecoreChecksumtype.SHA256;
  private List<string> documentation = new();

  private string softwareVersion;
  private bool overrideSchema;

  private WriteStrategyEnum writeStrategyEnum;

  public SIPBuilder() {}

  public SIPBuilder SetMetadataArgs(List<MetadataGroup> metadataArgs) {
    this.metadataArgs = metadataArgs;
    return this;
  }

  public SIPBuilder SetRepresentationArgs(List<RepresentationGroup> representationArgs) {
    this.representationArgs = representationArgs;
    return this;
  }

  public SIPBuilder SetTargetOnly(bool targetOnly) {
    this.targetOnly = targetOnly;
    return this;
  }

  public SIPBuilder SetVersion(CSIPVersion version) {
    this.version = version;
    return this;
  }

  public SIPBuilder SetPath(string path) {
    this.path = path;
    return this;
  }

  public SIPBuilder SetSubmitterAgentName(string submitterAgentName) {
    this.submitterAgentName = submitterAgentName;
    return this;
  }

  public SIPBuilder SetSubmitterAgentId(string submitterAgentId) {
    this.submitterAgentId = submitterAgentId;
    return this;
  }

  public SIPBuilder SetSipId(string sipId) {
    this.sipId = sipId;
    return this;
  }

  public SIPBuilder SetAncestors(List<string> ancestors) {
    this.ancestors = ancestors;
    return this;
  }

  public SIPBuilder SetChecksumAlgorithm(IFilecoreChecksumtype checksumAlgorithm) {
    this.checksumAlgorithm = checksumAlgorithm;
    return this;
  }

  public SIPBuilder SetDocumentation(List<string> documentation) {
    this.documentation = documentation;
    return this;
  }

  public SIPBuilder SetSoftwareVersion(string softwareVersion) {
    this.softwareVersion = softwareVersion;
    return this;
  }

  public SIPBuilder SetOverride(bool overrideSchema) {
    this.overrideSchema = overrideSchema;
    return this;
  }

  public SIPBuilder SetWriteStrategy(WriteStrategyEnum writeStrategyEnum) {
    this.writeStrategyEnum = writeStrategyEnum;
    return this;
  }

  public string Build() {
    SIP sip = new EARKSIP(SIPBuilderUtils.GetOrGenerateID(sipId), IPContentType.GetMIXED(), IPContentInformationType.GetMIXED(), version.ToString());

    string softVersion = "DEVELOPMENT-VERSION";
    if (softwareVersion != null) softVersion = softwareVersion;

    sip.AddCreatorSoftwareAgent("KEEPS dotnet-eark-sip", softVersion);
    sip.AddSubmitterAgent(submitterAgentName, submitterAgentId);
    sip.SetDescription("SIP created by dotnet-eark-sip cli");

    sip.SetChecksumAlgorithm(checksumAlgorithm);

    if (overrideSchema) sip.SetOverride();

    try {
      SIPBuilderUtils.AddMetadataGroupsToSIP(sip, metadataArgs);
    } catch (IPException e) {
      // Logger.Debug("Cannot add metadata to the SIP.", e);
      throw new SIPBuilderException("Cannot add metadata to the SIP.", e);
    }

    try {
      SIPBuilderUtils.AddRepresentationGroupsToSIP(sip, representationArgs, targetOnly);
    } catch (IPException e) {
      // Logger.Debug("Cannot add representation to the SIP.", e);
      throw new SIPBuilderException("Cannot add representation to the SIP.", e);
    }

    if (documentation != null) {
      try {
        SIPBuilderUtils.AddDocumentationToSIP(sip, documentation);
      } catch (IPException e) {
        // Logger.Debug("Cannot add documentation to the SIP.", e);
        throw new SIPBuilderException("Cannot add documentation to the SIP.", e);
      }
    }

    if (ancestors != null) sip.SetAncestors(ancestors);

    string buildPath;
    if (!string.IsNullOrEmpty(path) && Directory.Exists(path)) buildPath = path;
    else buildPath = Directory.GetCurrentDirectory();

    try {
      IWriteStrategy writeStrategy = SIPBuilderUtils.GetWriteStrategy(writeStrategyEnum, buildPath);
      return sip.Build(writeStrategy);
    } catch (IPException e) {
      // Logger.Debug("Unable to create the E-ARK SIP", e);
      throw new SIPBuilderException("Unable to create the E-ARK SIP", e);
    }
  }
}