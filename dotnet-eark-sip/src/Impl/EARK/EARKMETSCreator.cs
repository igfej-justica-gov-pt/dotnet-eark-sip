using System.Xml;
using IP;
using Mets;
using Microsoft.Extensions.Logging;
using Xml.Mets.CsipExtensionMets;

public abstract class EARKMETSCreator {
  private static readonly ILogger<EARKMETSCreator> logger = DefaultLogger.Create<EARKMETSCreator>();

  private readonly Dictionary<string, MetsTypeFileSecFileGrp> dataFileGrp = new Dictionary<string, MetsTypeFileSecFileGrp>();

  protected string EscapeNCName(string id) {
    return id.Replace("[:@$%&/+,;\\s]", "_");
  }

  public MetsWrapper GenerateMets(
    string id,
    string label,
    string profile,
    bool mainMets,
    List<string>? ancestors,
    string? metsPath,
    IPHeader header,
    IPContentType contentType,
    IPContentInformationType contentInformationType,
    bool isMetadata,
    bool isMetadataOther,
    bool isSchemas,
    bool isDocumentation,
    bool isRepresentations,
    bool isRepresentationsData,
    Oaispackagetype type = Oaispackagetype.SIP
  ) {
    Mets.Mets mets = new Mets.Mets();
    MetsWrapper metsWrapper = new MetsWrapper(mets, metsPath);

    AddBasicAttributesToMets(mets, id, label, profile, contentType, contentInformationType);

    AddHeaderToMets(mets, header, type);

    AddAmdSecToMets(mets);

    MetsTypeFileSec fileSec = CreateFileSec();

    AddDataFileGrpToMets(metsWrapper, fileSec, mainMets, isRepresentationsData);

    AddCommonFileGrpToMets(metsWrapper, fileSec, isSchemas, isDocumentation);

    if ((mainMets && isRepresentations) || fileSec.FileGrp.Count > 0) {
      mets.FileSec = fileSec;
    }

    StructMapType structMap = CreateStructMap();

    DivType mainDiv = AddCommonDivsToMainDiv(metsWrapper, id, isMetadata, isMetadataOther, isSchemas, isDocumentation);

    AddDataDivToMets(metsWrapper, mainDiv, mainMets, isRepresentationsData);

    structMap.Div = mainDiv;
    mets.StructMap.Add(structMap);

    AddAncestorsToMets(mets, ancestors);

    return metsWrapper;
  }

  /**
   * Generates Shallow SIP Representation METS with folders represented.
   *
   * @param representation        IPRepresentation
   * @param profile               string
   * @param mainMets              boolean If is main METS file or not.
   * @param ancestors             List<string> Nullable
   * @param metsPath              string Path to the Mets
   * @param ipHeader              IPHeader
   * @param type                  string
   * @param isMetadata            boolean If have metadata or not.
   * @param isMetadataOther       boolean If have other metadata or not.
   * @param isSchemas             boolean If have schemas or not.
   * @param isDocumentation       boolean If have documentation or not.
   * @param isSubmission          boolean If have submission or not.
   * @param isRepresentations     boolean If have representations or not.
   * @param isRepresentationsData boolean If have data in representations or not.
   *
   * @return MetsWrapper.
   */
  public MetsWrapper GenerateMetsShallow(
    IPRepresentation representation,
    string profile,
    bool mainMets,
    List<string>? ancestors,
    string? metsPath,
    IPHeader header,
    bool isMetadata,
    bool isMetadataOther,
    bool isSchemas,
    bool isDocumentation,
    bool isRepresentations,
    bool isRepresentationsData,
    Oaispackagetype type = Oaispackagetype.SIP
  ) {
    Mets.Mets mets = new Mets.Mets();
    MetsWrapper metsWrapper = new MetsWrapper(mets, metsPath);

    // basic attributes
    AddBasicAttributesToMets(
      mets,
      representation.GetObjectID(),
      representation.GetDescription(),
      profile,
      representation.GetContentType(),
      representation.ContentInformationType
    );

    // header
    AddHeaderToMets(mets, header, type);

    // administrative section
    AddAmdSecToMets(mets);

    // file section
    MetsTypeFileSec fileSec = CreateFileSec();

    CreateShallowFileGrps(metsWrapper, fileSec, mainMets, isRepresentationsData, representation);

    AddCommonFileGrpToMets(metsWrapper, fileSec, isSchemas, isDocumentation);

    if ((mainMets && isRepresentations) || fileSec.FileGrp.Count > 0) {
      mets.FileSec = fileSec;
    }

    StructMapType structMap = CreateStructMap();

    DivType mainDiv = AddCommonDivsToMainDiv(metsWrapper, representation.GetObjectID(), isMetadata, isMetadataOther, isSchemas, isDocumentation);

    CreateAndAddShallowDataDiv(metsWrapper, representation, mainDiv, mainMets, isRepresentationsData);

    structMap.Div = mainDiv;
    mets.StructMap.Add(structMap);

    AddAncestorsToMets(mets, ancestors);

    return metsWrapper;
  }

  private IPAgent GetCreatorAgent() {
    return new IPAgent(
      "KEEPS' .NET E-ARK SIP",
      MetsTypeMetsHdrAgentRole.CREATOR,
      null,
      MetsTypeMetsHdrAgentType.OTHER,
      "SOFTWARE",
      "1.0.0",
      Notetype.SOFTWARE_VERSION
    );
  }

  protected MetsTypeFileSecFileGrp CreateFileGroup(string use) {
    return new MetsTypeFileSecFileGrp
    {
      Id = Utils.GenerateRandomAndPrefixedUUID(),
      Use = use
    };
  }

  protected DivType CreateDivForStructMap(string label) {
    return new DivType
    {
      Id = Utils.GenerateRandomAndPrefixedUUID(),
      Label = label,
    };
  }

  protected DivType CreateRepresentationDivForStructMap(string representationId, DivTypeMptr mptr) {
    DivType div = new DivType
    {
      Id = Utils.GenerateRandomAndPrefixedUUID(),
      Label = IPConstants.REPRESENTATIONS_WITH_FIRST_LETTER_CAPITAL + "/" + representationId,
    };

    div.Mptr.Add(mptr);
    return div;
  }

  public void AddRepresentationMetsToZipAndToMainMets(
    Dictionary<string, IZipEntryInfo> zipEntries,
    MetsWrapper mainMetsWrapper,
    string representationId,
    MetsWrapper representationMetsWrapper,
    string representationMetsPath,
    string buildDir
  ) {
    try {
      // create mets pointer
      DivTypeMptr mptr = new DivTypeMptr
      {
        Loctype = ILocationLoctype.URL,
        Type = IPConstants.METS_TYPE_SIMPLE,
        Href = METSUtils.EncodeHref(representationMetsPath)
      };

      // create file
      FileType fileType = new FileType { Id = Utils.GenerateRandomAndPrefixedFileID() };

      AddMetsToZip(zipEntries, representationMetsWrapper, representationMetsPath, buildDir, false, fileType);

      // add to file group and then to file section
      MetsTypeFileSecFileGrp fileGrp = CreateFileGroup(IPConstants.REPRESENTATIONS_WITH_FIRST_LETTER_CAPITAL + "/" + representationId);
      FileTypeFLocat fileLocation = METSUtils.CreateFileLocation(representationMetsPath);
      fileType.FLocat.Add(fileLocation);
      fileGrp.File.Add(fileType);
      mainMetsWrapper.Mets.FileSec.FileGrp.Add(fileGrp);

      // set mets pointer
      DivType representationDiv = CreateRepresentationDivForStructMap(representationId, mptr);
      mptr.Title = fileGrp.Id;
      mainMetsWrapper.MainDiv.Div.Add(representationDiv);
    } catch (Exception e) {
      if (e is XmlException || e is IOException) {
        throw new IPException("Error saving representation METS", e);
      } else {
        throw e;
      }
    }
  }

  public void AddRepresentationSiardMetsToZipAndToMainMets(
    Dictionary<string, IZipEntryInfo> zipEntries,
    MetsWrapper mainMetsWrapper,
    string representationId,
    MetsWrapper representationMetsWrapper,
    string representationMetsPath,
    string buildDir
  ) {
    try {
      // create mets pointer
      DivTypeMptr mptr = new DivTypeMptr
      {
        Loctype = ILocationLoctype.URL,
        Type = IPConstants.METS_TYPE_SIMPLE,
        Href = METSUtils.EncodeHref(representationMetsPath)
      };

      // create file
      FileType fileType = new FileType { Id = Utils.GenerateRandomAndPrefixedFileID() };

      AddMetsToZip(zipEntries, representationMetsWrapper, representationMetsPath, buildDir, false, fileType);

      // add to file group and then to file section
      FileTypeFLocat fileLocation = METSUtils.CreateFileLocation(representationMetsPath);
      fileType.FLocat.Add(fileLocation);

      MetsTypeFileSecFileGrp fileGrp = CreateFileGroup(IPConstants.REPRESENTATIONS_WITH_FIRST_LETTER_CAPITAL + "/" + representationId);
      fileGrp.File.Add(fileType);
      fileGrp.AddCustomAttribute("csip:CONTENTINFORMATIONTYPE", "citssiard_v1_0");
      fileGrp.AddCustomAttribute("csip:OTHERCONTENTINFORMATIONTYPE", mainMetsWrapper.Mets.Othercontentinformationtype);

      mainMetsWrapper.Mets.FileSec.FileGrp.Add(fileGrp);

      // set mets pointer
      DivType representationDiv = CreateRepresentationDivForStructMap(representationId, mptr);
      mptr.Title = fileGrp.Id;
      mainMetsWrapper.MainDiv.Div.Add(representationDiv);
    } catch (Exception e) {
      if (e is XmlException || e is IOException) {
        throw new IPException("Error saving representation METS");
      } else {
        throw e;
      }
    }
  }

  protected void AddMetsToZip(Dictionary<string, IZipEntryInfo> zipEntries, MetsWrapper metsWrapper, string metsPath, string buildDir, bool mainMets, FileType fileType) {
    string temp = Path.Combine(buildDir, Path.GetRandomFileName() + IPConstants.METS_FILE_NAME + IPConstants.METS_FILE_EXTENSION);
    File.Create(temp).Dispose();

    ZIPUtils.AddMETSFileToZip(zipEntries, temp, metsPath, metsWrapper.Mets, mainMets, fileType);
  }

  protected MetsTypeMetsHdrAgent CreateMETSAgent(IPAgent ipAgent) {
    MetsTypeMetsHdrAgent agent = new MetsTypeMetsHdrAgent
    {
      Name = ipAgent.GetName(),
      Role = ipAgent.GetRole(),
      Otherrole = ipAgent.GetOtherRole(),
      Type = ipAgent._GetType(),
      Othertype = ipAgent.GetOtherType(),
    };

    MetsTypeMetsHdrAgentNote note = new MetsTypeMetsHdrAgentNote
    {
      Value = ipAgent.GetNote(),
      Notetype = ipAgent.GetNoteType() ?? Notetype.NOT_SET
    };

    agent.Note.Add(note);
    return agent;
  }

  public MdSecTypeMdRef AddDescriptiveMetadataToMETS(MetsWrapper metsWrapper, IPDescriptiveMetadata descriptiveMetadata, string descriptiveMetadataPath) {
    return AddMetadataToMETS(
      metsWrapper,
      descriptiveMetadata,
      descriptiveMetadataPath,
      descriptiveMetadata.GetMetadataType()._GetType(),
      descriptiveMetadata.GetMetadataType().GetOtherType(),
      descriptiveMetadata.MetadataVersion,
      true
    );
  }

  public MdSecTypeMdRef AddOtherMetadataToMETS(MetsWrapper metsWrapper, IPMetadata otherMetadata, string otherMetadataPath) {
    return AddMetadataToMETS(metsWrapper, otherMetadata, otherMetadataPath, IMetadataMdtype.OTHER, otherMetadata.GetID(), null);
  }

  protected MdSecTypeMdRef AddMetadataToMETS(
    MetsWrapper metsWrapper,
    IPMetadata metadata,
    string metadataPath,
    IMetadataMdtype mdType,
    string mdOtherType,
    string? mdTypeVersion,
    bool isDescriptive = false
  ) {
    MdSecType dmdSec = new MdSecType
    {
      Id = Utils.GenerateRandomAndPrefixedUUID(),
      Status = metadata.GetMetadataStatus(),
    };

    MdSecTypeMdRef mdRef = CreateMdRef(metadata.GetID(), metadataPath);
    mdRef.Mdtype = mdType;
    if (!string.IsNullOrEmpty(mdOtherType)) {
      mdRef.Othermdtype = mdOtherType;
    }
    mdRef.Mdtypeversion = mdTypeVersion;

    // set mimetype, date creation, etc.
    METSUtils.SetFileBasicInformation(metadata.GetMetadata().GetPath(), mdRef);
    // also set date created in dmdSec elem
    dmdSec.Created = mdRef.Created;
    dmdSec.CreatedSpecified = true;

    // structural map info.
    if (isDescriptive) {
      metsWrapper.MetadataDiv.Dmdid.Add(dmdSec.Id);
    } else {
      metsWrapper.OtherMetadataDiv.Dmdid.Add(dmdSec.Id);
    }

    dmdSec.MdRef = mdRef;
    metsWrapper.Mets.DmdSec.Add(dmdSec);

    return mdRef;
  }

  public MdSecTypeMdRef AddPreservationMetadataToMETS(MetsWrapper metsWrapper, IPMetadata preservationMetadata, string preservationMetadataPath) {
    MdSecType digiprovMD = new MdSecType
    {
      Id = Utils.GenerateRandomAndPrefixedUUID(),
      Status = preservationMetadata.GetMetadataStatus(),
    };

    MdSecTypeMdRef mdRef = CreateMdRef(preservationMetadata.GetID(), preservationMetadataPath);
    mdRef.Mdtype = preservationMetadata.GetMetadataType()._GetType();

    // set mimetype, date creation, etc.
    METSUtils.SetFileBasicInformation(preservationMetadata.GetMetadata().GetPath(), mdRef);

    // structural map info.
    metsWrapper.MetadataDiv.Admid.Add(digiprovMD.Id);

    digiprovMD.MdRef = mdRef;
    metsWrapper.Mets.AmdSec[0].DigiprovMd.Add(digiprovMD);

    return mdRef;
  }

  protected MdSecTypeMdRef CreateMdRef(string id, string metadataPath) {
    MdSecTypeMdRef mdRef = new MdSecTypeMdRef
    {
      Id = METSEnums.FILE_ID_PREFIX + EscapeNCName(id),
      Type = IPConstants.METS_TYPE_SIMPLE,
      Loctype = ILocationLoctype.URL,
      Href = METSUtils.EncodeHref(metadataPath)
    };

    return mdRef;
  }

  public void AddDataFileToMets(MetsWrapper representationMets, IPFileShallow shallow) {
    FileType file = shallow.FileType;
    file.Id = Utils.GenerateRandomAndPrefixedFileID();

    // add to file section
    FileTypeFLocat fileLocation = METSUtils.CreateShallowFileLocation(shallow.FileLocation.ToString());
    file.FLocat.Add(fileLocation);

    AddDataFileFromShallow(representationMets.DataFileGroup.FileGrp, shallow, file);
  }

  public FileType AddDataFileToMets(MetsWrapper representationMets, string dataFilePath, string dataFile) {
    FileType file = new FileType
    {
      Id = Utils.GenerateRandomAndPrefixedFileID()
    };

    METSUtils.SetFileBasicInformation(logger, dataFile, file);

    FileTypeFLocat fileLocation = METSUtils.CreateFileLocation(dataFilePath);
    file.FLocat.Add(fileLocation);
    representationMets.DataFileGroup.File.Add(file);

    if (representationMets.DataDiv.Fptr.Count == 0) {
      DivTypeFptr fptr = new DivTypeFptr
      {
        Fileid = representationMets.DataFileGroup.Id
      };

      representationMets.DataDiv.Fptr.Add(fptr);
    }

    return file;
  }

  public FileType AddSchemaFileToMETS(MetsWrapper metsWrapper, string filePath, string schemaFile) {
    FileType file = new FileType { Id = Utils.GenerateRandomAndPrefixedFileID() };

    METSUtils.SetFileBasicInformation(logger, schemaFile, file);

    FileTypeFLocat fileLocation = METSUtils.CreateFileLocation(filePath);
    file.FLocat.Add(fileLocation);

    metsWrapper.SchemasFileGroup.File.Add(file);
    if (metsWrapper.SchemasDiv != null && metsWrapper.SchemasDiv.Fptr.Count == 0) {
      DivTypeFptr fptr = new DivTypeFptr
      {
        Fileid = metsWrapper.SchemasFileGroup.Id
      };

      metsWrapper.SchemasDiv.Fptr.Add(fptr);
    }

    return file;
  }

  public FileType AddDocumentationFileToMETS(MetsWrapper metsWrapper, string filePath, string documentationFile) {
    FileType file = new FileType { Id = Utils.GenerateRandomAndPrefixedFileID() };

    METSUtils.SetFileBasicInformation(logger, documentationFile, file);

    FileTypeFLocat fileLocation = METSUtils.CreateFileLocation(filePath);
    file.FLocat.Add(fileLocation);

    metsWrapper.DocumentationFileGroup.File.Add(file);
    if (metsWrapper.DocumentationDiv != null && metsWrapper.DocumentationDiv.Fptr.Count == 0) {
      DivTypeFptr fptr = new DivTypeFptr
      {
        Fileid = metsWrapper.DocumentationFileGroup.Id
      };

      metsWrapper.DocumentationDiv.Fptr.Add(fptr);
    }

    return file;
  }

  protected StructMapType GenerateAncestorStructMap(List<string> ancestors) {
    StructMapType structMap = new StructMapType
    {
      Id = Utils.GenerateRandomAndPrefixedUUID(),
      Label = IPConstants.EARK_SIP_STRUCTURAL_MAP
    };

    DivType mainDiv = CreateDivForStructMap(IPConstants.EARK_SIP_DIV_LABEL);
    DivType ancestorsDiv = CreateDivForStructMap(IPConstants.EARK_SIP_ANCESTORS_DIV_LABEL);

    foreach (string ancestor in ancestors) {
      DivTypeMptr mptr = new DivTypeMptr
      {
        Type = IPConstants.METS_TYPE_SIMPLE,
        Href = METSUtils.EncodeHref(ancestor),
        Loctype = ILocationLoctype.HANDLE
      };

      ancestorsDiv.Mptr.Add(mptr);
    }

    mainDiv.Div.Add(ancestorsDiv);
    structMap.Div = mainDiv;

    return structMap;
  }

  protected void AddBasicAttributesToMets(Mets.Mets mets, string id, string label, string profile, IPContentType contentType, IPContentInformationType contentInformationType) {
    mets.Objid = id;
    mets.Profile = profile;
    mets.Label = label;

    mets.Type = EnumUtils.GetXmlEnumName(contentType.GetContentType());
    if (contentType.IsOtherAndOtherTypeIsDefined()) {
      mets.Othertype = contentType.GetOtherType();
    }

    if (contentInformationType != null) {
      mets.Contentinformationtype = contentInformationType.GetContentInformationType();
      string otherContentInformationType = contentInformationType.GetOtherType();
      if (!string.IsNullOrEmpty(otherContentInformationType)) {
        mets.Othercontentinformationtype = otherContentInformationType;
      }
    }
  }

  protected void AddHeaderToMets(Mets.Mets mets, IPHeader ipHeader, Oaispackagetype type) {
    MetsTypeMetsHdr header = new MetsTypeMetsHdr();

    try {
      DateTime currentDate = DateTime.Now;
      header.Createdate = currentDate;
      header.CreatedateSpecified = true;
      header.Lastmoddate = currentDate;
      header.LastmoddateSpecified = true;
      header.Recordstatus = EnumUtils.GetXmlEnumName(ipHeader.GetStatus());
      header.Oaispackagetype = type;
    } catch (Exception e) {
      throw new IPException("Error adding header to METS", e);
    }

    // header/agent
    foreach (IPAgent agent in ipHeader.GetAgents()) {
      header.Agent.Add(CreateMETSAgent(agent));
    }

    // records
    foreach (IPAltRecordID record in ipHeader.GetAltRecordIDs()) {
      MetsTypeMetsHdrAltRecordId recordId = new MetsTypeMetsHdrAltRecordId
      {
        Type = record._GetType(),
        Value = record.GetValue()
      };

      header.AltRecordId.Add(recordId);
    }

    mets.MetsHdr = header;
  }

  protected void AddAmdSecToMets(Mets.Mets mets) {
    AmdSecType amdSec = new AmdSecType
    {
      Id = Utils.GenerateRandomAndPrefixedUUID()
    };

    mets.AmdSec.Add(amdSec);
  }

  protected void AddCommonFileGrpToMets(MetsWrapper metsWrapper, MetsTypeFileSec fileSec, bool isSchemas, bool isDocumentation) {
    if (isSchemas) {
      MetsTypeFileSecFileGrp schemasFileGroup = CreateFileGroup(IPConstants.SCHEMAS_WITH_FIRST_LETTER_CAPITAL);
      fileSec.FileGrp.Add(schemasFileGroup);
      metsWrapper.SchemasFileGroup = schemasFileGroup;
    }

    if (isDocumentation) {
      MetsTypeFileSecFileGrp documentationFileGroup = CreateFileGroup(IPConstants.DOCUMENTATION_WITH_FIRST_LETTER_CAPITAL);
      fileSec.FileGrp.Add(documentationFileGroup);
      metsWrapper.DocumentationFileGroup = documentationFileGroup;
    }
  }

  protected MetsTypeFileSec CreateFileSec() {
    return new MetsTypeFileSec { Id = Utils.GenerateRandomAndPrefixedUUID() };
  }

  protected void AddDataFileGrpToMets(MetsWrapper metsWrapper, MetsTypeFileSec fileSec, bool mainMets, bool isRepresentationsData) {
    if (!mainMets && isRepresentationsData) {
      MetsTypeFileSecFileGrp dataFileGroup = CreateFileGroup(IPConstants.DATA_WITH_FIRST_LETTER_CAPITAL);
      fileSec.FileGrp.Add(dataFileGroup);
      metsWrapper.DataFileGroup = dataFileGroup;
    }
  }

  protected StructMapType CreateStructMap() {
    return new StructMapType
    {
      Id = Utils.GenerateRandomAndPrefixedUUID(),
      Type = IPConstants.METS_TYPE_PHYSICAL,
      Label = IPConstants.COMMON_SPEC_STRUCTURAL_MAP
    };
  }

  protected DivType AddCommonDivsToMainDiv(MetsWrapper metsWrapper, string id, bool isMetadata, bool isMetadataOther, bool isSchemas, bool isDocumentation) {
    DivType mainDiv = CreateDivForStructMap(id);
    metsWrapper.MainDiv = mainDiv;

    // metadata
    if (isMetadata) {
      DivType metadataDiv = CreateDivForStructMap(IPConstants.METADATA_WITH_FIRST_LETTER_CAPITAL);
      metsWrapper.MetadataDiv = metadataDiv;
      mainDiv.Div.Add(metadataDiv);
    }

    if (isMetadataOther) {
      DivType otherMetadataDiv = CreateDivForStructMap(
        IPConstants.METADATA_WITH_FIRST_LETTER_CAPITAL +
        IPConstants.ZIP_PATH_SEPARATOR +
        IPConstants.OTHER_WITH_FIRST_LETTER_CAPITAL
      );
      metsWrapper.OtherMetadataDiv = otherMetadataDiv;
      mainDiv.Div.Add(otherMetadataDiv);
    }

    if (isSchemas) {
      DivType schemasDiv = CreateDivForStructMap(IPConstants.SCHEMAS_WITH_FIRST_LETTER_CAPITAL);
      metsWrapper.SchemasDiv = schemasDiv;
      mainDiv.Div.Add(schemasDiv);
    }

    if (isDocumentation) {
      DivType documentationDiv = CreateDivForStructMap(IPConstants.DOCUMENTATION_WITH_FIRST_LETTER_CAPITAL);
      metsWrapper.DocumentationDiv = documentationDiv;
      mainDiv.Div.Add(documentationDiv);
    }

    return mainDiv;
  }

  protected void AddDataDivToMets(MetsWrapper metsWrapper, DivType mainDiv, bool mainMets, bool isRepresentationsData) {
    if (!mainMets && isRepresentationsData) {
      DivType dataDiv = CreateDivForStructMap(IPConstants.DATA_WITH_FIRST_LETTER_CAPITAL);
      metsWrapper.DataDiv = dataDiv;
      mainDiv.Div.Add(dataDiv);
    }
  }

  protected void AddAncestorsToMets(Mets.Mets mets, List<string>? ancestors) {
    if (ancestors != null && ancestors.Count > 0) {
      StructMapType structMapParent = GenerateAncestorStructMap(ancestors);
      mets.StructMap.Add(structMapParent);
    }
  }

  protected void AddFileGrps(IPRepresentation representation) {
    foreach (IIPFile file in representation.Data) {
      string dataFilePath;
      if (file.GetRelativeFolders() == null || file.GetRelativeFolders().Count == 0) {
        dataFilePath = IPConstants.DATA_WITH_FIRST_LETTER_CAPITAL;
      } else {
        dataFilePath = IPConstants.DATA_FOLDER + ModelUtils.GetFoldersFromList(file.GetRelativeFolders());
      }

      if (!dataFileGrp.ContainsKey(dataFilePath) && ((IPFileShallow)file).FileLocation != null) {
        MetsTypeFileSecFileGrp dataFileGroup = CreateFileGroup(dataFilePath);
        dataFileGrp.Add(dataFilePath, dataFileGroup);
      }
    }
  }

  protected void CreateShallowFileGrps(MetsWrapper metsWrapper, MetsTypeFileSec fileSec, bool mainMets, bool isRepresentationsData, IPRepresentation representation) {
    if (!mainMets && isRepresentationsData) {
      AddFileGrps(representation);

      foreach (string key in dataFileGrp.Keys) {
        MetsTypeFileSecFileGrp value = dataFileGrp[key];
        fileSec.FileGrp.Add(value);

        if (metsWrapper.DataFileGroup == null) {
          metsWrapper.DataFileGroup = new FileGrpType();
        }

        metsWrapper.DataFileGroup.FileGrp.Add(value);
      }
    }
  }

  protected void CreateAndAddShallowDataDiv(MetsWrapper metsWrapper, IPRepresentation representation, DivType mainDiv, bool mainMets, bool isRepresentationsData) {
    if (!mainMets && isRepresentationsData) {
      Tree<StructMapDiv> dataDivsTree = CreateTree(representation);
      DivType dataDiv = CreateDivForStructMap(dataDivsTree.Root.Label);

      if (dataDiv.Fptr.Count == 0 && dataFileGrp[dataDiv.Label] != null) {
        DivTypeFptr fptr = new DivTypeFptr { Fileid = dataFileGrp[dataDiv.Label].Id };
        dataDiv.Fptr.Add(fptr);
      }

      CreateDataDiv(dataDivsTree, dataDiv);
      metsWrapper.DataDiv = dataDiv;
      mainDiv.Div.Add(dataDiv);
    }
  }

  protected Tree<StructMapDiv> CreateTree(IPRepresentation representation) {
    Tree<StructMapDiv> divsTree = new Tree<StructMapDiv>(new StructMapDiv(IPConstants.DATA_WITH_FIRST_LETTER_CAPITAL));
    foreach (IIPFile file in representation.Data) {
      IPFileShallow shallow = (IPFileShallow)file;
      string dataFilePath;

      if (shallow.GetRelativeFolders() == null || shallow.GetRelativeFolders().Count == 0) {
        dataFilePath = IPConstants.DATA_WITH_FIRST_LETTER_CAPITAL;
      } else {
        dataFilePath = IPConstants.DATA_FOLDER + ModelUtils.GetFoldersFromList(shallow.GetRelativeFolders());
      }

      AddNodes(divsTree, dataFilePath, file.GetRelativeFolders());
    }

    return divsTree;
  }

  protected void AddNodes(Tree<StructMapDiv> divTree, string fileLocation, List<string> fileRelativeFolders) {
    if (fileRelativeFolders == null || fileRelativeFolders.Count == 0) {
      if (fileLocation != null) divTree.Root.FileLocation = fileLocation;
    } else {
      Tree<StructMapDiv> childNode = divTree.AddChild(new StructMapDiv(fileRelativeFolders.First()), divTree.Root);
      AddNodes(childNode, fileLocation, fileRelativeFolders.Skip(1).ToList());
    }
  }  

  protected void CreateDataDiv(Tree<StructMapDiv> dataDivsTree, DivType dataDiv) {
    if (dataDivsTree.Children.Count > 0) {
      foreach (Tree<StructMapDiv> child in dataDivsTree.Children) {
        DivType div = CreateDivForStructMap(child.Root.Label);
        if (div.Fptr.Count == 0 && !string.IsNullOrEmpty(child.Root.FileLocation) && dataFileGrp.ContainsKey(child.Root.FileLocation)) {
          DivTypeFptr fptr = new DivTypeFptr { Fileid = dataFileGrp[child.Root.FileLocation].Id };
          div.Fptr.Add(fptr);
        }

        dataDiv.Div.Add(div);
        CreateDataDiv(child, div);
      }
    }
  }

  // Adds Data File to the respective MetsTypeFileSecFileGrp.
  protected void AddDataFileFromShallow(IEnumerable<FileGrpType> fileGrpTypes, IPFileShallow shallow, FileType file) {
    foreach (FileGrpType fileGrpType in fileGrpTypes) {
      string dataFilePath;
      if (shallow.GetRelativeFolders() == null || shallow.GetRelativeFolders().Count == 0) {
        dataFilePath = IPConstants.DATA_WITH_FIRST_LETTER_CAPITAL;
      } else {
        dataFilePath = IPConstants.DATA_FOLDER + ModelUtils.GetFoldersFromList(shallow.GetRelativeFolders());
      }

      if (fileGrpType.Use == dataFilePath) {
        fileGrpType.File.Add(file);
      }
    }
  }

  /**
   * Clean the Dictionary data.
   */
  public void CleanFileGrpStructure() {
    dataFileGrp.Clear();
  }
}