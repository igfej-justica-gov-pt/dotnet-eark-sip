using IP;
using Mets;
using Microsoft.Extensions.Logging;
using System.Xml;
using Xml.Mets.CsipExtensionMets;

/// <summary>
/// Abstract class responsible for creating and managing EARK METS files.
/// </summary>
public abstract class EARKMETSCreator
{
    private static readonly ILogger<EARKMETSCreator> logger = DefaultLogger.Create<EARKMETSCreator>();

    private readonly Dictionary<string, MetsTypeFileSecFileGrp> dataFileGrp = new Dictionary<string, MetsTypeFileSecFileGrp>();

    /// <summary>
    /// Escapes invalid characters in a name to make it a valid.
    /// </summary>
    /// <param name="id">The input string to be escaped.</param>
    /// <returns>A string with invalid characters replaced by underscores.</returns>
    protected string EscapeNCName(string id)
    {
        return id.Replace("[:@$%&/+,;\\s]", "_");
    }

    /// <summary>
    /// Generates a METS (Metadata Encoding and Transmission Standard) wrapper with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier for the METS object.</param>
    /// <param name="label">The label for the METS object.</param>
    /// <param name="profile">The profile to be used for the METS object.</param>
    /// <param name="mainMets">Indicates whether this is the main METS file.</param>
    /// <param name="ancestors">A list of ancestor identifiers, if any.</param>
    /// <param name="metsPath">The file path for the METS file.</param>
    /// <param name="header">The IPHeader containing metadata for the METS header.</param>
    /// <param name="contentType">The content type of the METS object.</param>
    /// <param name="contentInformationType">The content information type of the METS object.</param>
    /// <param name="isMetadata">Indicates if metadata is included.</param>
    /// <param name="isMetadataOther">Indicates if other metadata is included.</param>
    /// <param name="isSchemas">Indicates if schemas are included.</param>
    /// <param name="isDocumentation">Indicates if documentation is included.</param>
    /// <param name="isRepresentations">Indicates if representations are included.</param>
    /// <param name="isRepresentationsData">Indicates if representation data is included.</param>
    /// <param name="type">The type of the OAIS package, default is SIP.</param>
    /// <returns>A MetsWrapper object containing the generated METS structure.</returns>
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
    )
    {
        Mets.Mets mets = new Mets.Mets();
        MetsWrapper metsWrapper = new MetsWrapper(mets, metsPath);

        AddBasicAttributesToMets(mets, id, label, profile, contentType, contentInformationType);

        AddHeaderToMets(mets, header, type);

        AddAmdSecToMets(mets);

        MetsTypeFileSec fileSec = CreateFileSec();

        AddDataFileGrpToMets(metsWrapper, fileSec, mainMets, isRepresentationsData);

        AddCommonFileGrpToMets(metsWrapper, fileSec, isSchemas, isDocumentation);

        if ((mainMets && isRepresentations) || fileSec.FileGrp.Count > 0)
        {
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

    /// Generates Shallow SIP Representation METS with folders represented.
    /// <param name="representation">The IPRepresentation object to generate the Shallow METS from.</param>
    /// <param name="profile">The profile to be used for the METS object.</param>
    /// <param name="mainMets">Indicates whether this is the main METS file.</param>
    /// <param name="ancestors">A list of ancestor identifiers, if any.</param>
    /// <param name="metsPath">The file path for the METS file.</param>
    /// <param name="header">The IPHeader containing metadata for the METS header.</param>
    /// <param name="isMetadata">Indicates if metadata is included.</param>
    /// <param name="isMetadataOther">Indicates if other metadata is included.</param>
    /// <param name="isSchemas">Indicates if schemas are included.</param>
    /// <param name="isDocumentation">Indicates if documentation is included.</param>
    /// <param name="isRepresentations">Indicates if representations are included.</param>
    /// <param name="isRepresentationsData">Indicates if representation data is included.</param>
    /// <param name="type">The type of the OAIS package, default is SIP.</param>
    /// <returns>A MetsWrapper object containing the generated METS structure.</returns>
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
    )
    {
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

        if ((mainMets && isRepresentations) || fileSec.FileGrp.Count > 0)
        {
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

    /// <summary>
    /// Creates a new file group with a specified use attribute.
    /// </summary>
    /// <param name="use">The use attribute for the file group.</param>
    /// <returns>A new instance of MetsTypeFileSecFileGrp with the specified use attribute.</returns>
    protected MetsTypeFileSecFileGrp CreateFileGroup(string use)
    {
        return new MetsTypeFileSecFileGrp
        {
            Id = Utils.GenerateRandomAndPrefixedUUID(),
            Use = use
        };
    }

    /// <summary>
    /// Creates a new div for a struct map with a specified label attribute.
    /// </summary>
    /// <param name="label">The label attribute for the div.</param>
    /// <returns>A new instance of DivType with the specified label attribute.</returns>
    protected DivType CreateDivForStructMap(string label)
    {
        return new DivType
        {
            Id = Utils.GenerateRandomAndPrefixedUUID(),
            Label = label,
        };
    }

    /// <summary>
    /// Creates a new representation div for a struct map with a specified label attribute and a mptr to add to it.
    /// </summary>
    /// <param name="representationId">The ID of the representation, to generate the label attribute.</param>
    /// <param name="mptr">The DivTypeMptr to add to the div.</param>
    /// <returns>A new instance of DivType.</returns>
    protected DivType CreateRepresentationDivForStructMap(string representationId, DivTypeMptr mptr)
    {
        DivType div = new DivType
        {
            Id = Utils.GenerateRandomAndPrefixedUUID(),
            Label = IPConstants.REPRESENTATIONS_WITH_FIRST_LETTER_CAPITAL + "/" + representationId,
        };

        div.Mptr.Add(mptr);
        return div;
    }

    /// <summary>
    /// Adds a representation METS file to the ZIP and updates the main METS file.
    /// </summary>
    /// <param name="zipEntries">The dictionary of ZIP entries to update.</param>
    /// <param name="mainMetsWrapper">The main METS wrapper to update.</param>
    /// <param name="representationId">The ID of the representation.</param>
    /// <param name="representationMetsWrapper">The METS wrapper for the representation.</param>
    /// <param name="representationMetsPath">The file path of the representation METS file.</param>
    /// <param name="buildDir">The build directory for temporary files.</param>
    public void AddRepresentationMetsToZipAndToMainMets(
      Dictionary<string, IZipEntryInfo> zipEntries,
      MetsWrapper mainMetsWrapper,
      string representationId,
      MetsWrapper representationMetsWrapper,
      string representationMetsPath,
      string buildDir
    )
    {
        try
        {
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
        }
        catch (Exception e)
        {
            if (e is XmlException || e is IOException)
            {
                throw new IPException("Error saving representation METS", e);
            }
            else
            {
                throw e;
            }
        }
    }

    /// <summary>
    /// Adds a SIARD representation METS file to the ZIP and updates the main METS file.
    /// </summary>
    /// <param name="zipEntries">The dictionary of ZIP entries to update.</param>
    /// <param name="mainMetsWrapper">The main METS wrapper to update.</param>
    /// <param name="representationId">The ID of the representation.</param>
    /// <param name="representationMetsWrapper">The METS wrapper for the representation.</param>
    /// <param name="representationMetsPath">The file path of the representation METS file.</param>
    /// <param name="buildDir">The build directory for temporary files.</param>
    public void AddRepresentationSiardMetsToZipAndToMainMets(
      Dictionary<string, IZipEntryInfo> zipEntries,
      MetsWrapper mainMetsWrapper,
      string representationId,
      MetsWrapper representationMetsWrapper,
      string representationMetsPath,
      string buildDir
    )
    {
        try
        {
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
        }
        catch (Exception e)
        {
            if (e is XmlException || e is IOException)
            {
                throw new IPException("Error saving representation METS");
            }
            else
            {
                throw e;
            }
        }
    }

    /// <summary>
    /// Adds a METS file to the ZIP archive and updates the file type with basic information.
    /// </summary>
    /// <param name="zipEntries">The dictionary of ZIP entries to update.</param>
    /// <param name="metsWrapper">The METS wrapper containing the METS structure.</param>
    /// <param name="metsPath">The file path for the METS file.</param>
    /// <param name="buildDir">The build directory for temporary files.</param>
    /// <param name="mainMets">Indicates whether this is the main METS file.</param>
    /// <param name="fileType">The file type to update with basic information.</param>
    protected void AddMetsToZip(Dictionary<string, IZipEntryInfo> zipEntries, MetsWrapper metsWrapper, string metsPath, string buildDir, bool mainMets, FileType fileType)
    {
        string temp = Path.Combine(buildDir, Path.GetRandomFileName() + IPConstants.METS_FILE_NAME + IPConstants.METS_FILE_EXTENSION);
        File.Create(temp).Dispose();

        ZIPUtils.AddMETSFileToZip(zipEntries, temp, metsPath, metsWrapper.Mets, mainMets, fileType);
    }

    /// <summary>
    /// Creates a METS agent based on the provided IPAgent.
    /// </summary>
    /// <param name="ipAgent">The IPAgent containing agent details.</param>
    /// <returns>A MetsTypeMetsHdrAgent object populated with the agent's details.</returns>
    protected MetsTypeMetsHdrAgent CreateMETSAgent(IPAgent ipAgent)
    {
        MetsTypeMetsHdrAgent agent = new MetsTypeMetsHdrAgent
        {
            Name = ipAgent.GetName(),
            Role = ipAgent.GetRole(),
            Otherrole = ipAgent.GetOtherRole(),
            Type = ipAgent._GetType(),
            Othertype = ipAgent.GetOtherType(),
        };

        if (!string.IsNullOrEmpty(ipAgent.GetNote()))
        {
            var note = new MetsTypeMetsHdrAgentNote
            {
                Value = ipAgent.GetNote(),
                Notetype = ipAgent.GetNoteType()
            };
            agent.Note.Add(note);
        }

        return agent;
    }

    /// <summary>
    /// Adds descriptive metadata to the METS wrapper.
    /// </summary>
    /// <param name="metsWrapper">The METS wrapper to which the metadata will be added.</param>
    /// <param name="descriptiveMetadata">The descriptive metadata to add.</param>
    /// <param name="descriptiveMetadataPath">The file path of the descriptive metadata.</param>
    /// <returns>An MdSecTypeMdRef object representing the added metadata reference.</returns>
    public MdSecTypeMdRef AddDescriptiveMetadataToMETS(MetsWrapper metsWrapper, IPDescriptiveMetadata descriptiveMetadata, string descriptiveMetadataPath)
    {
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

    /// <summary>
    /// Adds other metadata to the METS wrapper.
    /// </summary>
    /// <param name="metsWrapper">The METS wrapper to which the metadata will be added.</param>
    /// <param name="otherMetadata">The other metadata to add.</param>
    /// <param name="otherMetadataPath">The file path of the other metadata.</param>
    /// <returns>An MdSecTypeMdRef object representing the added metadata reference.</returns>
    public MdSecTypeMdRef AddOtherMetadataToMETS(MetsWrapper metsWrapper, IPMetadata otherMetadata, string otherMetadataPath)
    {
        return AddMetadataToMETS(metsWrapper, otherMetadata, otherMetadataPath, IMetadataMdtype.OTHER, otherMetadata.GetID(), null);
    }

    /// <summary>
    /// Adds metadata to the METS wrapper.
    /// </summary>
    /// <param name="metsWrapper">The METS wrapper to which the metadata will be added.</param>
    /// <param name="metadata">The metadata to add.</param>
    /// <param name="metadataPath">The file path of the metadata.</param>
    /// <param name="mdType">The type of the metadata.</param>
    /// <param name="mdOtherType">The other type of the metadata, if applicable.</param>
    /// <param name="mdTypeVersion">The version of the metadata type, if applicable.</param>
    /// <param name="isDescriptive">Indicates whether the metadata is descriptive.</param>
    /// <returns>An MdSecTypeMdRef object representing the added metadata reference.</returns>
    protected MdSecTypeMdRef AddMetadataToMETS(
      MetsWrapper metsWrapper,
      IPMetadata metadata,
      string metadataPath,
      IMetadataMdtype mdType,
      string mdOtherType,
      string? mdTypeVersion,
      bool isDescriptive = false
    )
    {
        MdSecType dmdSec = new MdSecType
        {
            Id = Utils.GenerateRandomAndPrefixedUUID(),
            Status = metadata.GetMetadataStatus(),
        };

        MdSecTypeMdRef mdRef = CreateMdRef(metadata.GetID(), metadataPath);
        mdRef.Mdtype = mdType;
        if (!string.IsNullOrWhiteSpace(mdOtherType))
        {
            mdRef.Othermdtype = mdOtherType;
        }
        mdRef.Mdtypeversion = mdTypeVersion;

        // set mimetype, date creation, etc.
        METSUtils.SetFileBasicInformation(metadata.GetMetadata().GetPath(), mdRef);
        // also set date created in dmdSec elem
        dmdSec.Created = mdRef.Created;
        dmdSec.CreatedSpecified = true;

        // structural map info.
        if (isDescriptive)
        {
            metsWrapper.MetadataDiv.Dmdid.Add(dmdSec.Id);
        }
        else
        {
            metsWrapper.OtherMetadataDiv.Dmdid.Add(dmdSec.Id);
        }

        dmdSec.MdRef = mdRef;
        metsWrapper.Mets.DmdSec.Add(dmdSec);

        return mdRef;
    }

    /// <summary>
    /// Adds preservation metadata to the METS wrapper.
    /// </summary>
    /// <param name="metsWrapper">The METS wrapper to which the metadata will be added.</param>
    /// <param name="preservationMetadata">The preservation metadata to add.</param>
    /// <param name="preservationMetadataPath">The file path of the preservation metadata.</param>
    /// <returns>An MdSecTypeMdRef object representing the added metadata reference.</returns>
    public MdSecTypeMdRef AddPreservationMetadataToMETS(MetsWrapper metsWrapper, IPMetadata preservationMetadata, string preservationMetadataPath)
    {
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

    /// <summary>
    /// Adds technical metadata to the METS wrapper.
    /// </summary>
    /// <param name="metsWrapper">The METS wrapper to which the metadata will be added.</param>
    /// <param name="technicalMetadata">The technical metadata to add.</param>
    /// <param name="technicalMetadataPath">The file path of the technical metadata.</param>
    /// <returns>An MdSecTypeMdRef object representing the added metadata reference.</returns>
    public MdSecTypeMdRef AddTechnicalMetadataToMETS(MetsWrapper metsWrapper, IPMetadata technicalMetadata, string technicalMetadataPath)
    {
        MdSecType techMD = new()
        {
            Id = Utils.GenerateRandomAndPrefixedUUID(),
            Status = technicalMetadata.GetMetadataStatus().ToString(),
        };

        MdSecTypeMdRef mdRef = CreateMdRef(technicalMetadata.GetID(), technicalMetadataPath);
        mdRef.Mdtype = technicalMetadata.GetMetadataType()._GetType();
        if (!string.IsNullOrWhiteSpace(technicalMetadata.GetMetadataType().GetOtherType()))
        {
            mdRef.Othermdtype = technicalMetadata.GetMetadataType().GetOtherType();
        }

        // set mimetype, date creation, etc.
        METSUtils.SetFileBasicInformation(technicalMetadata.GetMetadata().GetPath(), mdRef);

        // structural map info.
        metsWrapper.MetadataDiv?.Admid.Add(techMD.Id);

        techMD.MdRef = mdRef;
        metsWrapper.Mets.AmdSec[0].TechMd.Add(techMD);

        return mdRef;
    }

    /// <summary>
    /// Adds source metadata to the METS wrapper.
    /// </summary>
    /// <param name="metsWrapper">The METS wrapper to which the metadata will be added.</param>
    /// <param name="sourceMetadata">The source metadata to add.</param>
    /// <param name="sourceMetadataPath">The file path of the source metadata.</param>
    /// <returns>An MdSecTypeMdRef object representing the added metadata reference.</returns>
    public MdSecTypeMdRef AddSourceMetadataToMETS(MetsWrapper metsWrapper, IPMetadata sourceMetadata, string sourceMetadataPath)
    {
        MdSecType sourceMD = new()
        {
            Id = Utils.GenerateRandomAndPrefixedUUID(),
            Status = sourceMetadata.GetMetadataStatus().ToString(),
        };

        MdSecTypeMdRef mdRef = CreateMdRef(sourceMetadata.GetID(), sourceMetadataPath);
        mdRef.Mdtype = sourceMetadata.GetMetadataType()._GetType();
        if (!string.IsNullOrWhiteSpace(sourceMetadata.GetMetadataType().GetOtherType()))
        {
            mdRef.Othermdtype = sourceMetadata.GetMetadataType().GetOtherType();
        }

        // set mimetype, date creation, etc.
        METSUtils.SetFileBasicInformation(sourceMetadata.GetMetadata().GetPath(), mdRef);

        // structural map info.
        metsWrapper.MetadataDiv?.Admid.Add(sourceMD.Id);

        sourceMD.MdRef = mdRef;
        metsWrapper.Mets.AmdSec[0].SourceMd.Add(sourceMD);

        return mdRef;
    }

    /// <summary>
    /// Adds rights metadata to the METS wrapper.
    /// </summary>
    /// <param name="metsWrapper">The METS wrapper to which the metadata will be added.</param>
    /// <param name="rightsMetadata">The rights metadata to add.</param>
    /// <param name="rightsMetadataPath">The file path of the rights metadata.</param>
    /// <returns>An MdSecTypeMdRef object representing the added metadata reference.</returns>
    public MdSecTypeMdRef AddRightsMetadataToMETS(MetsWrapper metsWrapper, IPMetadata rightsMetadata, string rightsMetadataPath)
    {
        MdSecType rightsMD = new()
        {
            Id = Utils.GenerateRandomAndPrefixedUUID(),
            Status = rightsMetadata.GetMetadataStatus().ToString(),
        };

        MdSecTypeMdRef mdRef = CreateMdRef(rightsMetadata.GetID(), rightsMetadataPath);
        mdRef.Mdtype = rightsMetadata.GetMetadataType()._GetType();
        if (!string.IsNullOrWhiteSpace(rightsMetadata.GetMetadataType().GetOtherType()))
        {
            mdRef.Othermdtype = rightsMetadata.GetMetadataType().GetOtherType();
        }

        // set mimetype, date creation, etc.
        METSUtils.SetFileBasicInformation(rightsMetadata.GetMetadata().GetPath(), mdRef);

        // structural map info.
        metsWrapper.MetadataDiv?.Admid.Add(rightsMD.Id);

        rightsMD.MdRef = mdRef;
        metsWrapper.Mets.AmdSec[0].RightsMd.Add(rightsMD);

        return mdRef;
    }

    /// <summary>
    /// Creates a metadata reference (MdRef) for a given ID and metadata path.
    /// </summary>
    /// <param name="id">The unique identifier for the metadata.</param>
    /// <param name="metadataPath">The file path of the metadata.</param>
    /// <returns>An instance of MdSecTypeMdRef representing the metadata reference.</returns>
    protected MdSecTypeMdRef CreateMdRef(string id, string metadataPath)
    {
        MdSecTypeMdRef mdRef = new MdSecTypeMdRef
        {
            Id = METSEnums.FILE_ID_PREFIX + EscapeNCName(id),
            Type = IPConstants.METS_TYPE_SIMPLE,
            Loctype = ILocationLoctype.URL,
            Href = METSUtils.EncodeHref(metadataPath)
        };

        return mdRef;
    }

    /// <summary>
    /// Adds a data file to the METS wrapper based on the provided shallow file information.
    /// </summary>
    /// <param name="representationMets">The METS wrapper to which the data file will be added.</param>
    /// <param name="shallow">The shallow file information containing details about the data file.</param>
    public void AddDataFileToMets(MetsWrapper representationMets, IPFileShallow shallow)
    {
        FileType file = shallow.FileType;
        file.Id = Utils.GenerateRandomAndPrefixedFileID();

        // add to file section
        FileTypeFLocat fileLocation = METSUtils.CreateShallowFileLocation(shallow.FileLocation.ToString());
        file.FLocat.Add(fileLocation);

        AddDataFileFromShallow(representationMets.DataFileGroup.FileGrp, shallow, file);
    }

    /// <summary>
    /// Adds a data file to the METS wrapper.
    /// </summary>
    /// <param name="representationMets">The METS wrapper to which the data file will be added.</param>
    /// <param name="dataFilePath">The file path of the data file.</param>
    /// <param name="dataFile">The data file to add.</param>
    /// <returns>A FileType object representing the added data file.</returns>
    public FileType AddDataFileToMets(MetsWrapper representationMets, string dataFilePath, string dataFile)
    {
        FileType file = new FileType
        {
            Id = Utils.GenerateRandomAndPrefixedFileID()
        };

        METSUtils.SetFileBasicInformation(logger, dataFile, file);

        FileTypeFLocat fileLocation = METSUtils.CreateFileLocation(dataFilePath);
        file.FLocat.Add(fileLocation);
        representationMets.DataFileGroup.File.Add(file);

        if (representationMets.DataDiv.Fptr.Count == 0)
        {
            DivTypeFptr fptr = new DivTypeFptr
            {
                Fileid = representationMets.DataFileGroup.Id
            };

            representationMets.DataDiv.Fptr.Add(fptr);
        }

        return file;
    }

    /// <summary>
    /// Adds a schema file to the METS wrapper.
    /// </summary>
    /// <param name="metsWrapper">The METS wrapper to which the schema file will be added.</param>
    /// <param name="filePath">The file path of the schema file.</param>
    /// <param name="schemaFile">The schema file to add.</param>
    /// <returns>A FileType object representing the added schema file.</returns>
    public FileType AddSchemaFileToMETS(MetsWrapper metsWrapper, string filePath, string schemaFile)
    {
        FileType file = new FileType { Id = Utils.GenerateRandomAndPrefixedFileID() };

        METSUtils.SetFileBasicInformation(logger, schemaFile, file);

        FileTypeFLocat fileLocation = METSUtils.CreateFileLocation(filePath);
        file.FLocat.Add(fileLocation);

        metsWrapper.SchemasFileGroup.File.Add(file);
        if (metsWrapper.SchemasDiv != null && metsWrapper.SchemasDiv.Fptr.Count == 0)
        {
            DivTypeFptr fptr = new DivTypeFptr
            {
                Fileid = metsWrapper.SchemasFileGroup.Id
            };

            metsWrapper.SchemasDiv.Fptr.Add(fptr);
        }

        return file;
    }

    /// <summary>
    /// Adds a documentation file to the METS wrapper.
    /// </summary>
    /// <param name="metsWrapper">The METS wrapper to which the documentation file will be added.</param>
    /// <param name="filePath">The file path of the documentation file.</param>
    /// <param name="documentationFile">The documentation file to add.</param>
    /// <returns>A FileType object representing the added documentation file.</returns>
    public FileType AddDocumentationFileToMETS(MetsWrapper metsWrapper, string filePath, string documentationFile)
    {
        FileType file = new FileType { Id = Utils.GenerateRandomAndPrefixedFileID() };

        METSUtils.SetFileBasicInformation(logger, documentationFile, file);

        FileTypeFLocat fileLocation = METSUtils.CreateFileLocation(filePath);
        file.FLocat.Add(fileLocation);

        metsWrapper.DocumentationFileGroup.File.Add(file);
        if (metsWrapper.DocumentationDiv != null && metsWrapper.DocumentationDiv.Fptr.Count == 0)
        {
            DivTypeFptr fptr = new DivTypeFptr
            {
                Fileid = metsWrapper.DocumentationFileGroup.Id
            };

            metsWrapper.DocumentationDiv.Fptr.Add(fptr);
        }

        return file;
    }

    /// <summary>
    /// Generates a structural map for ancestor references.
    /// </summary>
    /// <param name="ancestors">A list of ancestor identifiers.</param>
    /// <returns>A StructMapType object representing the ancestor structural map.</returns>
    protected StructMapType GenerateAncestorStructMap(List<string> ancestors)
    {
        StructMapType structMap = new StructMapType
        {
            Id = Utils.GenerateRandomAndPrefixedUUID(),
            Label = IPConstants.EARK_SIP_STRUCTURAL_MAP
        };

        DivType mainDiv = CreateDivForStructMap(IPConstants.EARK_SIP_DIV_LABEL);
        DivType ancestorsDiv = CreateDivForStructMap(IPConstants.EARK_SIP_ANCESTORS_DIV_LABEL);

        foreach (string ancestor in ancestors)
        {
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

    /// <summary>
    /// Adds basic attributes to the provided METS object.
    /// </summary>
    /// <param name="mets">The METS object to which attributes will be added.</param>
    /// <param name="id">The unique identifier for the METS object.</param>
    /// <param name="label">The label for the METS object.</param>
    /// <param name="profile">The profile to be used for the METS object.</param>
    /// <param name="contentType">The content type of the METS object.</param>
    /// <param name="contentInformationType">The content information type of the METS object.</param>
    protected void AddBasicAttributesToMets(Mets.Mets mets, string id, string label, string profile, IPContentType contentType, IPContentInformationType contentInformationType)
    {
        mets.Objid = id;
        mets.Profile = profile;
        mets.Label = label;

        mets.Type = EnumUtils.GetXmlEnumName(contentType.GetContentType());
        if (contentType.IsOtherAndOtherTypeIsDefined())
        {
            mets.Othertype = contentType.GetOtherType();
        }

        if (contentInformationType != null)
        {
            mets.Contentinformationtype = contentInformationType.GetContentInformationType();
            mets.ContentinformationtypeSpecified = true;
            string otherContentInformationType = contentInformationType.GetOtherType();
            if (!string.IsNullOrEmpty(otherContentInformationType))
            {
                mets.Othercontentinformationtype = otherContentInformationType;
            }
        }
    }

    /// <summary>
    /// Adds a header to the provided METS object based on the given IPHeader and OAIS package type.
    /// </summary>
    /// <param name="mets">The METS object to which the header will be added.</param>
    /// <param name="ipHeader">The IPHeader containing metadata for the METS header.</param>
    /// <param name="type">The type of the OAIS package.</param>
    protected void AddHeaderToMets(Mets.Mets mets, IPHeader ipHeader, Oaispackagetype type)
    {
        MetsTypeMetsHdr header = new MetsTypeMetsHdr();

        try
        {
            DateTime currentDate = DateTime.Now;
            header.Createdate = currentDate;
            header.CreatedateSpecified = true;
            header.Lastmoddate = currentDate;
            header.LastmoddateSpecified = true;
            header.Recordstatus = EnumUtils.GetXmlEnumName(ipHeader.GetStatus());
            header.Oaispackagetype = type;
        }
        catch (Exception e)
        {
            throw new IPException("Error adding header to METS", e);
        }

        // header/agent
        foreach (IPAgent agent in ipHeader.GetAgents())
        {
            header.Agent.Add(CreateMETSAgent(agent));
        }

        // records
        foreach (IPAltRecordID record in ipHeader.GetAltRecordIDs())
        {
            MetsTypeMetsHdrAltRecordId recordId = new MetsTypeMetsHdrAltRecordId
            {
                Type = record._GetType(),
                Value = record.GetValue()
            };

            header.AltRecordId.Add(recordId);
        }

        mets.MetsHdr = header;
    }

    /// <summary>
    /// Adds an administrative metadata section (AmdSec) to the provided METS object.
    /// </summary>
    /// <param name="mets">The METS object to which the administrative metadata section will be added.</param>
    protected void AddAmdSecToMets(Mets.Mets mets)
    {
        AmdSecType amdSec = new AmdSecType
        {
            Id = Utils.GenerateRandomAndPrefixedUUID()
        };

        mets.AmdSec.Add(amdSec);
    }

    /// <summary>
    /// Adds common file groups (schemas and documentation) to the METS wrapper and file section.
    /// </summary>
    /// <param name="metsWrapper">The METS wrapper to which the file groups will be added.</param>
    /// <param name="fileSec">The file section to which the file groups will be added.</param>
    /// <param name="isSchemas">Indicates whether schema files should be added.</param>
    /// <param name="isDocumentation">Indicates whether documentation files should be added.</param>
    protected void AddCommonFileGrpToMets(MetsWrapper metsWrapper, MetsTypeFileSec fileSec, bool isSchemas, bool isDocumentation)
    {
        if (isSchemas)
        {
            MetsTypeFileSecFileGrp schemasFileGroup = CreateFileGroup(IPConstants.SCHEMAS_WITH_FIRST_LETTER_CAPITAL);
            fileSec.FileGrp.Add(schemasFileGroup);
            metsWrapper.SchemasFileGroup = schemasFileGroup;
        }

        if (isDocumentation)
        {
            MetsTypeFileSecFileGrp documentationFileGroup = CreateFileGroup(IPConstants.DOCUMENTATION_WITH_FIRST_LETTER_CAPITAL);
            fileSec.FileGrp.Add(documentationFileGroup);
            metsWrapper.DocumentationFileGroup = documentationFileGroup;
        }
    }

    /// <summary>
    /// Creates a new file section for the METS object.
    /// </summary>
    /// <returns>A new instance of MetsTypeFileSec with a unique ID.</returns>
    protected MetsTypeFileSec CreateFileSec()
    {
        return new MetsTypeFileSec { Id = Utils.GenerateRandomAndPrefixedUUID() };
    }

    /// <summary>
    /// Adds a data file group to the METS wrapper and file section.
    /// </summary>
    /// <param name="metsWrapper">The METS wrapper to which the file group will be added.</param>
    /// <param name="fileSec">The file section to which the file group will be added.</param>
    /// <param name="mainMets">Indicates whether this is the main METS file.</param>
    /// <param name="isRepresentationsData">Indicates if representation data is included.</param>
    protected void AddDataFileGrpToMets(MetsWrapper metsWrapper, MetsTypeFileSec fileSec, bool mainMets, bool isRepresentationsData)
    {
        if (!mainMets && isRepresentationsData)
        {
            MetsTypeFileSecFileGrp dataFileGroup = CreateFileGroup(IPConstants.DATA_WITH_FIRST_LETTER_CAPITAL);
            fileSec.FileGrp.Add(dataFileGroup);
            metsWrapper.DataFileGroup = dataFileGroup;
        }
    }

    /// <summary>
    /// Creates a new structural map for the METS object.
    /// </summary>
    /// <returns>A new instance of StructMapType with a unique ID, type, and label.</returns>
    protected StructMapType CreateStructMap()
    {
        return new StructMapType
        {
            Id = Utils.GenerateRandomAndPrefixedUUID(),
            Type = IPConstants.METS_TYPE_PHYSICAL,
            Label = IPConstants.COMMON_SPEC_STRUCTURAL_MAP
        };
    }

    /// <summary>
    /// Adds common divisions (metadata, schemas, documentation) to the main division of the METS wrapper.
    /// </summary>
    /// <param name="metsWrapper">The METS wrapper to which the divisions will be added.</param>
    /// <param name="id">The unique identifier for the main division.</param>
    /// <param name="isMetadata">Indicates whether metadata should be added.</param>
    /// <param name="isMetadataOther">Indicates whether other metadata should be added.</param>
    /// <param name="isSchemas">Indicates whether schemas should be added.</param>
    /// <param name="isDocumentation">Indicates whether documentation should be added.</param>
    /// <returns>The main division with the added common divisions.</returns>
    protected DivType AddCommonDivsToMainDiv(MetsWrapper metsWrapper, string id, bool isMetadata, bool isMetadataOther, bool isSchemas, bool isDocumentation)
    {
        DivType mainDiv = CreateDivForStructMap(id);
        metsWrapper.MainDiv = mainDiv;

        // metadata
        if (isMetadata)
        {
            DivType metadataDiv = CreateDivForStructMap(IPConstants.METADATA_WITH_FIRST_LETTER_CAPITAL);
            metsWrapper.MetadataDiv = metadataDiv;
            mainDiv.Div.Add(metadataDiv);
        }

        if (isMetadataOther)
        {
            DivType otherMetadataDiv = CreateDivForStructMap(
              IPConstants.METADATA_WITH_FIRST_LETTER_CAPITAL +
              IPConstants.ZIP_PATH_SEPARATOR +
              IPConstants.OTHER_WITH_FIRST_LETTER_CAPITAL
            );
            metsWrapper.OtherMetadataDiv = otherMetadataDiv;
            mainDiv.Div.Add(otherMetadataDiv);
        }

        if (isSchemas)
        {
            DivType schemasDiv = CreateDivForStructMap(IPConstants.SCHEMAS_WITH_FIRST_LETTER_CAPITAL);
            metsWrapper.SchemasDiv = schemasDiv;
            mainDiv.Div.Add(schemasDiv);
        }

        if (isDocumentation)
        {
            DivType documentationDiv = CreateDivForStructMap(IPConstants.DOCUMENTATION_WITH_FIRST_LETTER_CAPITAL);
            metsWrapper.DocumentationDiv = documentationDiv;
            mainDiv.Div.Add(documentationDiv);
        }

        return mainDiv;
    }

    /// <summary>
    /// Adds a data division to the METS wrapper if it is not the main METS and representation data is included.
    /// </summary>
    /// <param name="metsWrapper">The METS wrapper to which the data division will be added.</param>
    /// <param name="mainDiv">The main division to which the data division will be added.</param>
    /// <param name="mainMets">Indicates whether this is the main METS file.</param>
    /// <param name="isRepresentationsData">Indicates if representation data is included.</param>
    protected void AddDataDivToMets(MetsWrapper metsWrapper, DivType mainDiv, bool mainMets, bool isRepresentationsData)
    {
        if (!mainMets && isRepresentationsData)
        {
            DivType dataDiv = CreateDivForStructMap(IPConstants.DATA_WITH_FIRST_LETTER_CAPITAL);
            metsWrapper.DataDiv = dataDiv;
            mainDiv.Div.Add(dataDiv);
        }
    }

    /// <summary>
    /// Adds ancestor references to the METS object as a structural map.
    /// </summary>
    /// <param name="mets">The METS object to which the ancestor references will be added.</param>
    /// <param name="ancestors">A list of ancestor identifiers, if any.</param>
    protected void AddAncestorsToMets(Mets.Mets mets, List<string>? ancestors)
    {
        if (ancestors != null && ancestors.Count > 0)
        {
            StructMapType structMapParent = GenerateAncestorStructMap(ancestors);
            mets.StructMap.Add(structMapParent);
        }
    }

    /// <summary>
    /// Adds file groups to the internal data structure based on the provided representation.
    /// </summary>
    /// <param name="representation">The representation containing data files to be grouped.</param>
    protected void AddFileGrps(IPRepresentation representation)
    {
        foreach (IIPFile file in representation.Data)
        {
            string dataFilePath;
            if (file.GetRelativeFolders() == null || file.GetRelativeFolders().Count == 0)
            {
                dataFilePath = IPConstants.DATA_WITH_FIRST_LETTER_CAPITAL;
            }
            else
            {
                dataFilePath = IPConstants.DATA_FOLDER + ModelUtils.GetFoldersFromList(file.GetRelativeFolders());
            }

            if (!dataFileGrp.ContainsKey(dataFilePath) && ((IPFileShallow)file).FileLocation != null)
            {
                MetsTypeFileSecFileGrp dataFileGroup = CreateFileGroup(dataFilePath);
                dataFileGrp.Add(dataFilePath, dataFileGroup);
            }
        }
    }

    /// <summary>
    /// Creates shallow file groups for the given representation and adds them to the METS wrapper and file section.
    /// </summary>
    /// <param name="metsWrapper">The METS wrapper to which the file groups will be added.</param>
    /// <param name="fileSec">The file section to which the file groups will be added.</param>
    /// <param name="mainMets">Indicates whether this is the main METS file.</param>
    /// <param name="isRepresentationsData">Indicates if representation data is included.</param>
    /// <param name="representation">The representation containing data files to be grouped.</param>
    protected void CreateShallowFileGrps(MetsWrapper metsWrapper, MetsTypeFileSec fileSec, bool mainMets, bool isRepresentationsData, IPRepresentation representation)
    {
        if (!mainMets && isRepresentationsData)
        {
            AddFileGrps(representation);

            foreach (string key in dataFileGrp.Keys)
            {
                MetsTypeFileSecFileGrp value = dataFileGrp[key];
                fileSec.FileGrp.Add(value);

                if (metsWrapper.DataFileGroup == null)
                {
                    metsWrapper.DataFileGroup = new FileGrpType();
                }

                metsWrapper.DataFileGroup.FileGrp.Add(value);
            }
        }
    }

    /// <summary>
    /// Creates and adds a shallow data division to the METS wrapper based on the provided representation.
    /// </summary>
    /// <param name="metsWrapper">The METS wrapper to which the data division will be added.</param>
    /// <param name="representation">The representation containing data files to be added to the division.</param>
    /// <param name="mainDiv">The main division to which the data division will be added.</param>
    /// <param name="mainMets">Indicates whether this is the main METS file.</param>
    /// <param name="isRepresentationsData">Indicates if representation data is included.</param>
    protected void CreateAndAddShallowDataDiv(MetsWrapper metsWrapper, IPRepresentation representation, DivType mainDiv, bool mainMets, bool isRepresentationsData)
    {
        if (!mainMets && isRepresentationsData)
        {
            Tree<StructMapDiv> dataDivsTree = CreateTree(representation);
            DivType dataDiv = CreateDivForStructMap(dataDivsTree.Root.Label);

            if (dataDiv.Fptr.Count == 0 && dataFileGrp[dataDiv.Label] != null)
            {
                DivTypeFptr fptr = new DivTypeFptr { Fileid = dataFileGrp[dataDiv.Label].Id };
                dataDiv.Fptr.Add(fptr);
            }

            CreateDataDiv(dataDivsTree, dataDiv);
            metsWrapper.DataDiv = dataDiv;
            mainDiv.Div.Add(dataDiv);
        }
    }

    /// <summary>
    /// Creates a tree structure of structural map divisions based on the provided representation.
    /// </summary>
    /// <param name="representation">The representation containing data files to be organized into a tree structure.</param>
    /// <returns>A tree structure of structural map divisions.</returns>
    protected Tree<StructMapDiv> CreateTree(IPRepresentation representation)
    {
        Tree<StructMapDiv> divsTree = new Tree<StructMapDiv>(new StructMapDiv(IPConstants.DATA_WITH_FIRST_LETTER_CAPITAL));
        foreach (IIPFile file in representation.Data)
        {
            IPFileShallow shallow = (IPFileShallow)file;
            string dataFilePath;

            if (shallow.GetRelativeFolders() == null || shallow.GetRelativeFolders().Count == 0)
            {
                dataFilePath = IPConstants.DATA_WITH_FIRST_LETTER_CAPITAL;
            }
            else
            {
                dataFilePath = IPConstants.DATA_FOLDER + ModelUtils.GetFoldersFromList(shallow.GetRelativeFolders());
            }

            AddNodes(divsTree, dataFilePath, file.GetRelativeFolders());
        }

        return divsTree;
    }

    /// <summary>
    /// Adds nodes to the tree structure based on the file location and relative folder hierarchy.
    /// </summary>
    /// <param name="divTree">The tree structure to which nodes will be added.</param>
    /// <param name="fileLocation">The file location associated with the node.</param>
    /// <param name="fileRelativeFolders">The list of relative folders representing the hierarchy.</param>
    protected void AddNodes(Tree<StructMapDiv> divTree, string fileLocation, List<string> fileRelativeFolders)
    {
        if (fileRelativeFolders == null || fileRelativeFolders.Count == 0)
        {
            if (fileLocation != null) divTree.Root.FileLocation = fileLocation;
        }
        else
        {
            Tree<StructMapDiv> childNode = divTree.AddChild(new StructMapDiv(fileRelativeFolders.First()), divTree.Root);
            AddNodes(childNode, fileLocation, fileRelativeFolders.Skip(1).ToList());
        }
    }

    /// <summary>
    /// Recursively creates data divisions in the METS structure based on the provided tree structure.
    /// </summary>
    /// <param name="dataDivsTree">The tree structure containing data divisions.</param>
    /// <param name="dataDiv">The parent division to which child divisions will be added.</param>
    protected void CreateDataDiv(Tree<StructMapDiv> dataDivsTree, DivType dataDiv)
    {
        if (dataDivsTree.Children.Count > 0)
        {
            foreach (Tree<StructMapDiv> child in dataDivsTree.Children)
            {
                DivType div = CreateDivForStructMap(child.Root.Label);
                if (div.Fptr.Count == 0 && !string.IsNullOrEmpty(child.Root.FileLocation) && dataFileGrp.ContainsKey(child.Root.FileLocation))
                {
                    DivTypeFptr fptr = new DivTypeFptr { Fileid = dataFileGrp[child.Root.FileLocation].Id };
                    div.Fptr.Add(fptr);
                }

                dataDiv.Div.Add(div);
                CreateDataDiv(child, div);
            }
        }
    }

    /// <summary>
    /// Adds a data file to the appropriate file group based on the shallow file's relative folder structure.
    /// </summary>
    /// <param name="fileGrpTypes">The collection of file groups to search for a matching group.</param>
    /// <param name="shallow">The shallow file containing relative folder information.</param>
    /// <param name="file">The file to be added to the matching file group.</param>
    protected void AddDataFileFromShallow(IEnumerable<FileGrpType> fileGrpTypes, IPFileShallow shallow, FileType file)
    {
        foreach (FileGrpType fileGrpType in fileGrpTypes)
        {
            string dataFilePath;
            if (shallow.GetRelativeFolders() == null || shallow.GetRelativeFolders().Count == 0)
            {
                dataFilePath = IPConstants.DATA_WITH_FIRST_LETTER_CAPITAL;
            }
            else
            {
                dataFilePath = IPConstants.DATA_FOLDER + ModelUtils.GetFoldersFromList(shallow.GetRelativeFolders());
            }

            if (fileGrpType.Use == dataFilePath)
            {
                fileGrpType.File.Add(file);
            }
        }
    }

    /// <summary>
    /// Clears the internal data file group structure.
    /// </summary>
    public void CleanFileGrpStructure()
    {
        dataFileGrp.Clear();
    }
}