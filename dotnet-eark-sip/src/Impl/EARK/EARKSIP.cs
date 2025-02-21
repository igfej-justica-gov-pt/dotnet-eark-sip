using System.Reflection;
using IP;
using IPEnums;
using Mets;
using Microsoft.Extensions.Logging;

public class EARKSIP : SIP {
  private static readonly ILogger<EARKSIP> logger = DefaultLogger.Create<EARKSIP>();
  private static readonly string SIP_TEMP_DIR = "EARKSIP";
  private static readonly string DEFAULT_SIP_VERSION = "2.2.0";

  private static readonly string SOFTWARE_VERSION = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0";

  private readonly EARKMETSCreator metsCreator;

  public EARKSIP() : base() {
    SetProfile(IPConstants.SIP_SPEC_PROFILE);
    metsCreator = new METSGeneratorFactory().GetGenerator(DEFAULT_SIP_VERSION);
  }

  public EARKSIP(string id) : base(id) {
    SetProfile(IPConstants.SIP_SPEC_PROFILE);
    metsCreator = new METSGeneratorFactory().GetGenerator(DEFAULT_SIP_VERSION);
  }

  public EARKSIP(string id, IPContentType contentType, IPContentInformationType contentInformationType, string version) : base(id, contentType, contentInformationType) {
    if (version == "2.2.0" || version == "2.1.0") {
      SetProfile(IPConstants.SIP_SPEC_PROFILE.Replace(".xml", "-v" + version.Replace(".", "-") + ".xml"));
    } else {
      SetProfile(IPConstants.SIP_SPEC_PROFILE);
    }

    metsCreator = new METSGeneratorFactory().GetGenerator(version);
  }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
  public EARKSIP(string id, IPContentType contentType, IPContentInformationType contentInformationType) {
    new EARKSIP(id, contentType, contentInformationType, DEFAULT_SIP_VERSION);
  }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

  public override string Build(IWriteStrategy writeStrategy) {
    return Build(writeStrategy, false);
  }

  public override string Build(IWriteStrategy writeStrategy, bool onlyManifest) {
    return Build(writeStrategy, null, onlyManifest);
  }

  public override string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension) {
    return Build(writeStrategy, fileNameWithoutExtension, false, SIPType.EARK2);
  }

  public override string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension, SIPType sipType) {
    return Build(writeStrategy, fileNameWithoutExtension, false, sipType);
  }

  public override string Build(IWriteStrategy writeStrategy, string? fileNameWithoutExtension, bool onlyManifest) {
    return Build(writeStrategy, fileNameWithoutExtension, onlyManifest, SIPType.EARK2);
  }

  public override string Build(IWriteStrategy writeStrategy, string? fileNameWithoutExtension, bool onlyManifest, SIPType sipType) {
    IPConstants.METS_ENCODE_AND_DECODE_HREF = true;
    string buildDir = ModelUtils.CreateBuildDir(SIP_TEMP_DIR).FullName;

    EARKUtils earkUtils = new EARKUtils(metsCreator);

    try {
      AddCreatorSoftwareAgent("dotnet-eark-sip", SOFTWARE_VERSION);

      Dictionary<string, IZipEntryInfo> zipEntries = GetZipEntries();
      earkUtils.AddDefaultSchemas(logger, GetSchemas(), buildDir, GetOverride());

      bool isMetadataOther = GetOtherMetadata() != null && GetOtherMetadata().Count > 0;
      bool isMetadata = (GetDescriptiveMetadata() != null && GetDescriptiveMetadata().Count > 0) || (GetPreservationMetadata() != null && GetPreservationMetadata().Count > 0);
      bool isDocumentation = GetDocumentation() != null && GetDocumentation().Count > 0;
      bool isSchemas = GetSchemas() != null && GetSchemas().Count > 0;
      bool isRepresentations = GetRepresentations() != null && GetRepresentations().Count > 0;

      MetsWrapper mainMETSWrapper = metsCreator.GenerateMets(
        string.Join(" ", GetIds()),
        GetDescription(),
        GetProfile(),
        true,
        GetAncestors(),
        null,
        GetHeader(),
        GetContentType(),
        GetContentInformationType(),
        isMetadata,
        isMetadataOther,
        isSchemas,
        isDocumentation,
        isRepresentations,
        false
      );

      earkUtils.AddDescriptiveMetadataToZipAndMETS(zipEntries, mainMETSWrapper, GetDescriptiveMetadata(), null);
      earkUtils.AddPreservationMetadataToZipAndMETS(zipEntries, mainMETSWrapper, GetPreservationMetadata(), null);
      earkUtils.AddOtherMetadataToZipAndMETS(zipEntries, mainMETSWrapper, GetOtherMetadata(), null);
      earkUtils.AddRepresentationsToZipAndMETS(this, GetRepresentations(), zipEntries, mainMETSWrapper, buildDir, sipType);
      earkUtils.AddSchemasToZipAndMETS(zipEntries, mainMETSWrapper, GetSchemas(), null);
      earkUtils.AddDocumentationToZipAndMETS(zipEntries, mainMETSWrapper, GetDocumentation(), null);

      METSUtils.AddMainMETSToZip(zipEntries, mainMETSWrapper, buildDir);
      NotifySipBuildPackagingStarted(zipEntries.Count);
      return writeStrategy.Write(zipEntries, this, fileNameWithoutExtension, GetId(), true);
    } catch (Exception e) {
      ModelUtils.CleanUpUponInterrupt(logger, writeStrategy.DestinationPath);
      throw e;
    } finally {
      ModelUtils.DeleteBuildDir(buildDir);
    }
  }

  public override HashSet<IFilecoreChecksumtype> GetExtraChecksumAlgorithms() {
    return new HashSet<IFilecoreChecksumtype>();
  }
}