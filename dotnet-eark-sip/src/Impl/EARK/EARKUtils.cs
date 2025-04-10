using IP;
using Mets;
using Microsoft.Extensions.Logging;

/// <summary>
/// Utility class for handling E-ARK SIP and METS-related operations.
/// </summary>
public class EARKUtils
{
    private readonly EARKMETSCreator metsCreator;

    /// <summary>
    /// Initializes a new instance of the <see cref="EARKUtils"/> class with the specified METS creator.
    /// </summary>
    /// <param name="metsCreator">The METS creator used for generating METS files.</param>
    public EARKUtils(EARKMETSCreator metsCreator)
    {
        this.metsCreator = metsCreator;
    }

    private void AddMetadataToZipAndMETS(
      Dictionary<string, IZipEntryInfo> zipEntries,
      MetsWrapper metsWrapper,
      IEnumerable<IPMetadata> metadataList,
      string? representationId,
      string folder,
      Func<MetsWrapper, IPMetadata, string, MdSecTypeMdRef> addToMetsFunc
    )
    {
        if (metadataList == null) return;

        foreach (IPMetadata metadata in metadataList)
        {
            IIPFile file = metadata.GetMetadata();

            string filePath = folder + ModelUtils.GetFoldersFromList(file.GetRelativeFolders()) + file.GetFileName();
            MdSecTypeMdRef mdRef = addToMetsFunc(metsWrapper, metadata, filePath);

            if (representationId != null)
            {
                filePath = IPConstants.REPRESENTATIONS_FOLDER + representationId + IPConstants.ZIP_PATH_SEPARATOR + filePath;
            }

            ZIPUtils.AddMdRefFileToZip(zipEntries, file.GetPath(), filePath, mdRef);
        }
    }

    /// <summary>
    /// Adds descriptive metadata to the ZIP archive and METS document.
    /// </summary>
    /// <param name="zipEntries">The dictionary of ZIP entries.</param>
    /// <param name="metsWrapper">The METS wrapper to update.</param>
    /// <param name="descriptiveMetadata">The list of descriptive metadata to add.</param>
    /// <param name="representationId">The optional representation ID.</param>
    public void AddDescriptiveMetadataToZipAndMETS(
      Dictionary<string, IZipEntryInfo> zipEntries,
      MetsWrapper metsWrapper,
      List<IPDescriptiveMetadata> descriptiveMetadata,
      string? representationId
    )
    {
        AddMetadataToZipAndMETS(
          zipEntries,
          metsWrapper,
          descriptiveMetadata,
          representationId,
          IPConstants.DESCRIPTIVE_FOLDER,
          (fMetsWrapper, fMetadata, fFilePath) => metsCreator.AddDescriptiveMetadataToMETS(fMetsWrapper, (IPDescriptiveMetadata)fMetadata, fFilePath)
        );
    }

    /// <summary>
    /// Adds preservation metadata to the ZIP archive and METS document.
    /// </summary>
    /// <param name="zipEntries">The dictionary of ZIP entries.</param>
    /// <param name="metsWrapper">The METS wrapper to update.</param>
    /// <param name="preservationMetadata">The list of preservation metadata to add.</param>
    /// <param name="representationId">The optional representation ID.</param>
    public void AddPreservationMetadataToZipAndMETS(
      Dictionary<string, IZipEntryInfo> zipEntries,
      MetsWrapper metsWrapper,
      List<IPMetadata> preservationMetadata,
      string? representationId
    )
    {
        AddMetadataToZipAndMETS(
          zipEntries,
          metsWrapper,
          preservationMetadata,
          representationId,
          IPConstants.PRESERVATION_FOLDER,
          metsCreator.AddPreservationMetadataToMETS
        );
    }

    /// <summary>
    /// Adds other metadata to the ZIP archive and METS document.
    /// </summary>
    /// <param name="zipEntries">The dictionary of ZIP entries.</param>
    /// <param name="metsWrapper">The METS wrapper to update.</param>
    /// <param name="otherMetadata">The list of other metadata to add.</param>
    /// <param name="representationId">The optional representation ID.</param>
    public void AddOtherMetadataToZipAndMETS(
      Dictionary<string, IZipEntryInfo> zipEntries,
      MetsWrapper metsWrapper,
      List<IPMetadata> otherMetadata,
      string? representationId
    )
    {
        AddMetadataToZipAndMETS(
          zipEntries,
          metsWrapper,
          otherMetadata,
          representationId,
          IPConstants.OTHER_FOLDER,
          metsCreator.AddOtherMetadataToMETS
        );
    }

    /// <summary>
    /// Adds representations to the ZIP archive and METS document.
    /// </summary>
    /// <param name="ip">The information package (IP) instance.</param>
    /// <param name="representations">The list of representations to add.</param>
    /// <param name="zipEntries">The dictionary of ZIP entries.</param>
    /// <param name="mainMetsWrapper">The main METS wrapper to update.</param>
    /// <param name="buildDir">The directory where the build is taking place.</param>
    /// <param name="sipType">The type of SIP (Submission Information Package).</param>
    public void AddRepresentationsToZipAndMETS(
      IIP ip,
      List<IPRepresentation> representations,
      Dictionary<string, IZipEntryInfo> zipEntries,
      MetsWrapper mainMetsWrapper,
      string buildDir,
      IPEnums.SIPType sipType
    )
    {
        if (representations == null || representations.Count == 0) return;

        if (ip is SIP sip)
        {
            sip.NotifySipBuildRepresentationsProcessingStarted(representations.Count);
        }

        foreach (IPRepresentation representation in representations)
        {
            string representationId = representation.GetObjectID();

            bool isRepresentationMetadataOther = representation.OtherMetadata.Count > 0;
            bool isRepresentationMetadata = (representation.DescriptiveMetadata.Count > 0) || (representation.PreservationMetadata.Count > 0);
            bool isRepresentationDocumentation = representation.Documentation.Count > 0;
            bool isRepresentationSchemas = representation.Schemas.Count > 0;
            bool isRepresentationsData = representation.Data.Count > 0;

            IPHeader header = new IPHeader(IPEnums.IPStatus.NEW).SetAgents(representation.Agents);
            MetsWrapper representationMetsWrapper;

            if (IPEnums.SIPType.ERMS == sipType)
            {
                representationMetsWrapper = metsCreator.GenerateMets(
                  representationId,
                  representation.GetDescription(),
                  ip.GetProfile(),
                  false,
                  null,
                  null,
                  header,
                  representation.GetContentType(),
                  representation.ContentInformationType,
                  isRepresentationMetadata,
                  isRepresentationMetadataOther,
                  isRepresentationSchemas,
                  isRepresentationDocumentation,
                  false,
                  isRepresentationsData
                );
            }
            else if (IPEnums.SIPType.SIARD == sipType)
            {
                representationMetsWrapper = metsCreator.GenerateMets(
                  representationId,
                  representation.GetDescription(),
                  "https://citssiard.dilcis.eu/profile/E-ARK-SIARD-REPRESENTATION.xml",
                  false,
                  null,
                  null,
                  header,
                  representation.GetContentType(),
                  representation.ContentInformationType,
                  isRepresentationMetadata,
                  isRepresentationMetadataOther,
                  isRepresentationSchemas,
                  isRepresentationDocumentation,
                  false,
                  isRepresentationsData
                );
            }
            else if (IPEnums.SIPType.EARK2S != sipType)
            {
                representationMetsWrapper = metsCreator.GenerateMets(
                  representationId,
                  representation.GetDescription(),
                  ip.GetProfile(),
                  false,
                  null,
                  null,
                  header,
                  representation.GetContentType(),
                  representation.ContentInformationType,
                  isRepresentationMetadata,
                  isRepresentationMetadataOther,
                  isRepresentationSchemas,
                  isRepresentationDocumentation,
                  false,
                  isRepresentationsData
                );
            }
            else
            {
                representationMetsWrapper = metsCreator.GenerateMetsShallow(
                  representation,
                  ip.GetProfile(),
                  false,
                  null,
                  null,
                  header,
                  isRepresentationMetadata,
                  isRepresentationMetadataOther,
                  isRepresentationSchemas,
                  isRepresentationDocumentation,
                  false,
                  isRepresentationsData
                );
            }

            representationMetsWrapper.MainDiv.Type = representation.GetStatus();

            // representation data
            if (sipType == IPEnums.SIPType.ERMS)
            {
                AddRepresentationDataFilesToZipErmsAndMETS(ip, zipEntries, representationMetsWrapper, representation, representationId);
            }
            else if (sipType == IPEnums.SIPType.SIARD)
            {
                AddRepresentationDataFilesToZipSiardAndMETS(ip, zipEntries, representationMetsWrapper, representation, representationId);
            }
            else
            {
                AddRepresentationDataFilesToZipAndMETS(ip, zipEntries, representationMetsWrapper, representation, representationId);
            }

            AddDescriptiveMetadataToZipAndMETS(zipEntries, representationMetsWrapper, representation.DescriptiveMetadata, representationId);
            AddPreservationMetadataToZipAndMETS(zipEntries, representationMetsWrapper, representation.PreservationMetadata, representationId);
            AddOtherMetadataToZipAndMETS(zipEntries, representationMetsWrapper, representation.OtherMetadata, representationId);
            AddSchemasToZipAndMETS(zipEntries, representationMetsWrapper, representation.Schemas, representationId);
            AddDocumentationToZipAndMETS(zipEntries, representationMetsWrapper, representation.Documentation, representationId);

            string path = IPConstants.REPRESENTATIONS_FOLDER + representationId + IPConstants.ZIP_PATH_SEPARATOR;
            if (sipType == IPEnums.SIPType.ERMS)
            {
                metsCreator.AddRepresentationMetsToZipAndToMainMets(
                  zipEntries,
                  mainMetsWrapper,
                  representationId,
                  representationMetsWrapper,
                  path + IPConstants.DATA + IPConstants.ZIP_PATH_SEPARATOR + IPConstants.METS_FILE,
                  buildDir
                );
            }
            else if (sipType == IPEnums.SIPType.SIARD)
            {
                metsCreator.AddRepresentationSiardMetsToZipAndToMainMets(
                  zipEntries,
                  mainMetsWrapper,
                  representationId,
                  representationMetsWrapper,
                  path + IPConstants.DATA + IPConstants.ZIP_PATH_SEPARATOR + IPConstants.METS_FILE,
                  buildDir
                );
            }
            else
            {
                metsCreator.AddRepresentationMetsToZipAndToMainMets(
                  zipEntries,
                  mainMetsWrapper,
                  representationId,
                  representationMetsWrapper,
                  path + IPConstants.METS_FILE,
                  buildDir
                );
            }

            metsCreator.CleanFileGrpStructure();
        }

        if (ip is SIP sip1)
        {
            sip1.NotifySipBuildRepresentationsProcessingEnded();
        }
    }

    private void AddRepresentationDataFilesToZipErmsAndMETS(
      IIP ip,
      Dictionary<string,
      IZipEntryInfo> zipEntries,
      MetsWrapper representationMetsWrapper,
      IPRepresentation representation,
      string representationId
    )
    {
        if (representation.Data == null || representation.Data.Count == 0) return;

        if (ip is SIP sip)
        {
            sip.NotifySipBuildRepresentationProcessingStarted(representation.Data.Count);
        }

        int i = 1;
        foreach (IIPFile file in representation.Data)
        {
            if (file is IPFile)
            {
                string dataFilePath = ModelUtils.GetFoldersFromList(file.GetRelativeFolders()) + file.GetFileName();
                FileType fileType = metsCreator.AddDataFileToMets(representationMetsWrapper, dataFilePath, file.GetPath());

                dataFilePath = IPConstants.DATA_FOLDER + dataFilePath;
                dataFilePath = IPConstants.REPRESENTATIONS_FOLDER + representationId + IPConstants.ZIP_PATH_SEPARATOR + dataFilePath;

                ZIPUtils.AddFileTypeFileToZip(zipEntries, file.GetPath(), dataFilePath, fileType);
            }
            else if (file is IPFileShallow shallow && (shallow.FileLocation != null))
            {
                metsCreator.AddDataFileToMets(representationMetsWrapper, shallow);
            }

            if (ip is SIP sip1)
            {
                sip1.NotifySipBuildRepresentationProcessingCurrentStatus(i++);
            }
        }

        if (ip is SIP sip2)
        {
            sip2.NotifySipBuildRepresentationProcessingEnded();
        }
    }

    private void AddRepresentationDataFilesToZipSiardAndMETS(
      IIP ip,
      Dictionary<string,
      IZipEntryInfo> zipEntries,
      MetsWrapper representationMetsWrapper,
      IPRepresentation representation,
      string representationId
    )
    {
        if (representation.Data == null || representation.Data.Count == 0) return;

        if (ip is SIP sip)
        {
            sip.NotifySipBuildRepresentationProcessingStarted(representation.Data.Count);
        }

        int i = 1;
        foreach (IIPFile file in representation.Data)
        {
            if (file is IPFile)
            {
                string dataFilePath = ModelUtils.GetFoldersFromList(file.GetRelativeFolders()) + file.GetFileName();
                FileType fileType = metsCreator.AddDataFileToMets(representationMetsWrapper, dataFilePath, file.GetPath());

                if (representation.ContentInformationType.GetOtherType() != null)
                {
                    fileType.AddCustomAttribute("csip:OTHERCONTENTINFORMATIONTYPE", representation.ContentInformationType.GetOtherType());
                }

                dataFilePath = IPConstants.DATA_FOLDER + dataFilePath;
                dataFilePath = IPConstants.REPRESENTATIONS_FOLDER + representationId + IPConstants.ZIP_PATH_SEPARATOR + dataFilePath;

                ZIPUtils.AddFileTypeFileToZip(zipEntries, file.GetPath(), dataFilePath, fileType);
            }
            else if (file is IPFileShallow shallow && (shallow.FileLocation != null))
            {
                metsCreator.AddDataFileToMets(representationMetsWrapper, shallow);
            }

            if (ip is SIP sip1)
            {
                sip1.NotifySipBuildRepresentationProcessingCurrentStatus(i++);
            }
        }

        if (ip is SIP sip2)
        {
            sip2.NotifySipBuildRepresentationProcessingEnded();
        }
    }

    /// <summary>
    /// Adds representation data files to the ZIP archive and METS document.
    /// </summary>
    /// <param name="ip">The information package (IP) instance.</param>
    /// <param name="zipEntries">The dictionary of ZIP entries.</param>
    /// <param name="representationMetsWrapper">The METS wrapper for the representation.</param>
    /// <param name="representation">The representation containing the data files.</param>
    /// <param name="representationId">The ID of the representation.</param>
    public void AddRepresentationDataFilesToZipAndMETS(
      IIP ip,
      Dictionary<string,
      IZipEntryInfo> zipEntries,
      MetsWrapper representationMetsWrapper,
      IPRepresentation representation,
      string representationId
    )
    {
        if (representation.Data == null || representation.Data.Count == 0) return;

        if (ip is SIP sip)
        {
            sip.NotifySipBuildRepresentationProcessingStarted(representation.Data.Count);
        }

        int i = 1;
        foreach (IIPFile file in representation.Data)
        {
            if (file is IPFile)
            {
                string dataFilePath = IPConstants.DATA_FOLDER + ModelUtils.GetFoldersFromList(file.GetRelativeFolders()) + file.GetFileName();
                FileType fileType = metsCreator.AddDataFileToMets(representationMetsWrapper, dataFilePath, file.GetPath());

                dataFilePath = IPConstants.REPRESENTATIONS_FOLDER + representationId + IPConstants.ZIP_PATH_SEPARATOR + dataFilePath;

                ZIPUtils.AddFileTypeFileToZip(zipEntries, file.GetPath(), dataFilePath, fileType);
            }
            else if (file is IPFileShallow shallow && (shallow.FileLocation != null))
            {
                metsCreator.AddDataFileToMets(representationMetsWrapper, shallow);
            }

            if (ip is SIP sip1)
            {
                sip1.NotifySipBuildRepresentationProcessingCurrentStatus(i++);
            }
        }

        if (ip is SIP sip2)
        {
            sip2.NotifySipBuildRepresentationProcessingEnded();
        }
    }

    private void AddFileToZipAndMETS(
      Dictionary<string, IZipEntryInfo> zipEntries,
      MetsWrapper metsWrapper,
      List<IIPFile> files,
      string? representationId,
      string folder,
      Func<MetsWrapper, string, string, FileType> addToMetsFunc
    )
    {
        if (files == null) return;

        foreach (IIPFile file in files)
        {
            string filePath = folder + ModelUtils.GetFoldersFromList(file.GetRelativeFolders()) + file.GetFileName();
            FileType fileType = addToMetsFunc(metsWrapper, filePath, file.GetPath());

            if (representationId != null)
            {
                filePath = IPConstants.REPRESENTATIONS_FOLDER + representationId + IPConstants.ZIP_PATH_SEPARATOR + filePath;
            }

            ZIPUtils.AddFileTypeFileToZip(zipEntries, file.GetPath(), filePath, fileType);
        }
    }

    /// <summary>
    /// Adds schemas to the ZIP archive and METS document.
    /// </summary>
    /// <param name="zipEntries">The dictionary of ZIP entries.</param>
    /// <param name="metsWrapper">The METS wrapper to update.</param>
    /// <param name="schemas">The list of schemas to add.</param>
    /// <param name="representationId">The optional representation ID.</param>
    public void AddSchemasToZipAndMETS(Dictionary<string, IZipEntryInfo> zipEntries, MetsWrapper metsWrapper, List<IIPFile> schemas, string? representationId)
    {
        AddFileToZipAndMETS(zipEntries, metsWrapper, schemas, representationId, IPConstants.SCHEMAS_FOLDER, metsCreator.AddSchemaFileToMETS);
    }

    /// <summary>
    /// Adds documentation to the ZIP archive and METS document.
    /// </summary>
    /// <param name="zipEntries">The dictionary of ZIP entries.</param>
    /// <param name="metsWrapper">The METS wrapper to update.</param>
    /// <param name="documentation">The list of documentation to add.</param>
    /// <param name="representationId">The optional representation ID.</param>
    public void AddDocumentationToZipAndMETS(Dictionary<string, IZipEntryInfo> zipEntries, MetsWrapper metsWrapper, List<IIPFile> documentation, string? representationId)
    {
        AddFileToZipAndMETS(zipEntries, metsWrapper, documentation, representationId, IPConstants.DOCUMENTATION_FOLDER, metsCreator.AddDocumentationFileToMETS);
    }

    /// <summary>
    /// Adds default schemas to the provided list of schemas, copying them from resources if necessary.
    /// </summary>
    /// <param name="logger">The logger instance for logging errors or information.</param>
    /// <param name="schemas">The list of schemas to which default schemas will be added.</param>
    /// <param name="buildDir">The directory where the schemas will be copied.</param>
    /// <param name="_override">Indicates whether to override existing schemas in the list.</param>
    public void AddDefaultSchemas(ILogger logger, List<IIPFile> schemas, string buildDir, bool _override)
    {
        try
        {
            string tempSchema = "";
            if (schemas.Count > 0)
            {
                tempSchema = schemas.First().GetFileName() ?? "";

                if (!_override)
                {
                    List<string> defaultSchemas = new List<string> {
            IPConstants.SCHEMA_EARK_CSIP_FILENAME,
            IPConstants.SCHEMA_EARK_SIP_FILENAME,
            IPConstants.SCHEMA_METS_FILENAME_WITH_VERSION,
            IPConstants.SCHEMA_XLINK_FILENAME,
          };

                    if (defaultSchemas.Contains(tempSchema))
                    {
                        schemas.RemoveAt(0);
                        tempSchema = "";
                    }
                }
            }

            string earkCsipSchema = Utils.CopyResourceFromClasspathToDir(
              typeof(EARKSIP), buildDir, IPConstants.SCHEMA_EARK_CSIP_FILENAME, IPConstants.SCHEMA_EARK_CSIP_RELATIVE_PATH_FROM_RESOURCES
            );
            if (tempSchema != IPConstants.SCHEMA_EARK_CSIP_FILENAME)
            {
                schemas.Add(new IPFile(earkCsipSchema, IPConstants.SCHEMA_EARK_CSIP_FILENAME));
            }

            string earkSipSchema = Utils.CopyResourceFromClasspathToDir(
              typeof(EARKSIP), buildDir, IPConstants.SCHEMA_EARK_SIP_FILENAME, IPConstants.SCHEMA_EARK_SIP_RELATIVE_PATH_FROM_RESOURCES
            );
            if (tempSchema != IPConstants.SCHEMA_EARK_SIP_FILENAME)
            {
                schemas.Add(new IPFile(earkSipSchema, IPConstants.SCHEMA_EARK_SIP_FILENAME));
            }

            string metsSchema = Utils.CopyResourceFromClasspathToDir(
              typeof(EARKSIP), buildDir, IPConstants.SCHEMA_METS_FILENAME_WITH_VERSION, IPConstants.SCHEMA_METS_RELATIVE_PATH_FROM_RESOURCES
            );
            if (tempSchema != IPConstants.SCHEMA_METS_FILENAME_WITH_VERSION)
            {
                schemas.Add(new IPFile(metsSchema, IPConstants.SCHEMA_METS_FILENAME_WITH_VERSION));
            }

            string xlinkSchema = Utils.CopyResourceFromClasspathToDir(
              typeof(EARKSIP), buildDir, IPConstants.SCHEMA_XLINK_FILENAME, IPConstants.SCHEMA_XLINK_RELATIVE_PATH_FROM_RESOURCES
            );
            if (tempSchema != IPConstants.SCHEMA_XLINK_FILENAME)
            {
                schemas.Add(new IPFile(xlinkSchema, IPConstants.SCHEMA_XLINK_FILENAME));
            }
        }
        catch (IOException e)
        {
            logger.LogError(e, "Error while trying to add default schemas");
        }
    }
}