using IP;

/// <summary>
/// Represents the base class for SIP (Submission Information Package).
/// </summary>
public abstract class SIP : IP.IP
{
    private readonly List<ISIPObserver> observers;

    /// <summary>
    /// Initializes a new instance of the <see cref="SIP"/> class.
    /// </summary>
    public SIP() : base()
    {
        observers = new List<ISIPObserver>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SIP"/> class with the specified ID.
    /// </summary>
    /// <param name="id">The unique identifier for the SIP.</param>
    public SIP(string id) : base()
    {
        SetId(id);
        SetType(IPEnums.IPType.SIP);
        observers = new List<ISIPObserver>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SIP"/> class with the specified ID, content type, and content information type.
    /// </summary>
    /// <param name="id">The unique identifier for the SIP.</param>
    /// <param name="contentType">The content type of the SIP.</param>
    /// <param name="contentInformationType">The content information type of the SIP.</param>
    public SIP(string id, IPContentType contentType, IPContentInformationType contentInformationType) : base()
    {
        SetId(id);
        SetType(IPEnums.IPType.SIP);
        SetContentType(contentType);
        SetContentInformationType(contentInformationType);
        observers = new List<ISIPObserver>();
    }

    /// <summary>
    /// Adds an observer to the SIP.
    /// </summary>
    /// <param name="observer">The observer to be added.</param>
    public void AddObserver(ISIPObserver observer)
    {
        observers.Add(observer);
    }

    /// <summary>
    /// Removes an observer from the SIP.
    /// </summary>
    /// <param name="observer">The observer to be removed.</param>
    public void RemoveObserver(ISIPObserver observer)
    {
        observers.Remove(observer);
    }

    /// <summary>
    /// Notifies observers that the processing of SIP representations has started.
    /// </summary>
    /// <param name="totalNumberOfRepresentations">The total number of representations to process.</param>
    public void NotifySipBuildRepresentationsProcessingStarted(int totalNumberOfRepresentations)
    {
        foreach (ISIPObserver sipObserver in observers)
        {
            sipObserver.SipBuildRepresentationsProcessingStarted(totalNumberOfRepresentations);
        }
    }

    /// <summary>
    /// Notifies observers that the processing of a SIP representation has started.
    /// </summary>
    /// <param name="totalNumberOfFiles">The total number of files in the representation.</param>
    public void NotifySipBuildRepresentationProcessingStarted(int totalNumberOfFiles)
    {
        foreach (ISIPObserver sipObserver in observers)
        {
            sipObserver.SipBuildRepresentationProcessingStarted(totalNumberOfFiles);
        }
    }

    /// <summary>
    /// Notifies observers of the current status of SIP representation processing.
    /// </summary>
    /// <param name="numberOfFilesAlreadyProcessed">The number of files already processed.</param>
    public void NotifySipBuildRepresentationProcessingCurrentStatus(int numberOfFilesAlreadyProcessed)
    {
        foreach (ISIPObserver sipObserver in observers)
        {
            sipObserver.SipBuildRepresentationProcessingCurrentStatus(numberOfFilesAlreadyProcessed);
        }
    }

    /// <summary>
    /// Notifies observers that the processing of a SIP representation has ended.
    /// </summary>
    public void NotifySipBuildRepresentationProcessingEnded()
    {
        foreach (ISIPObserver sipObserver in observers)
        {
            sipObserver.SipBuildRepresentationProcessingEnded();
        }
    }

    /// <summary>
    /// Notifies observers that the processing of all SIP representations has ended.
    /// </summary>
    public void NotifySipBuildRepresentationsProcessingEnded()
    {
        foreach (ISIPObserver sipObserver in observers)
        {
            sipObserver.SipBuildRepresentationsProcessingEnded();
        }
    }

    /// <summary>
    /// Notifies observers that the packaging of the SIP has started.
    /// </summary>
    /// <param name="totalNumberOfFiles">The total number of files to be packaged.</param>
    public void NotifySipBuildPackagingStarted(int totalNumberOfFiles)
    {
        foreach (ISIPObserver sipObserver in observers)
        {
            sipObserver.SipBuildPackagingStarted(totalNumberOfFiles);
        }
    }

    /// <summary>
    /// Notifies observers of the current status of SIP packaging.
    /// </summary>
    /// <param name="numberOfFilesAlreadyProcessed">The number of files already processed during packaging.</param>
    public void NotifySipBuildPackagingCurrentStatus(int numberOfFilesAlreadyProcessed)
    {
        foreach (ISIPObserver sipObserver in observers)
        {
            sipObserver.SipBuildPackagingCurrentStatus(numberOfFilesAlreadyProcessed);
        }
    }

    /// <summary>
    /// Notifies observers that the packaging of the SIP has ended.
    /// </summary>
    public void NotifySipBuildPackagingEnded()
    {
        foreach (ISIPObserver sipObserver in observers)
        {
            sipObserver.SipBuildPackagingEnded();
        }
    }
}