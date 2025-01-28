using IP;

public abstract class SIP : IP.IP {
  private readonly List<ISIPObserver> observers;

  public SIP() : base() {
    observers = new List<ISIPObserver>();
  }

  public SIP(string id) : base() {
    SetId(id);
    SetType(IPEnums.IPType.SIP);
    observers = new List<ISIPObserver>();
  }

  public SIP(string id, IPContentType contentType, IPContentInformationType contentInformationType) : base() {
    SetId(id);
    SetType(IPEnums.IPType.SIP);
    SetContentType(contentType);
    SetContentInformationType(contentInformationType);
    observers = new List<ISIPObserver>();
  }

  public void AddObserver(ISIPObserver observer) {
    observers.Add(observer);
  }

  public void RemoveObserver(ISIPObserver observer) {
    observers.Remove(observer);
  }

  public void NotifySipBuildRepresentationsProcessingStarted(int totalNumberOfRepresentations) {
    foreach (ISIPObserver sipObserver in observers) {
      sipObserver.SipBuildRepresentationsProcessingStarted(totalNumberOfRepresentations);
    }
  }

  public void NotifySipBuildRepresentationProcessingStarted(int totalNumberOfFiles) {
    foreach (ISIPObserver sipObserver in observers) {
      sipObserver.SipBuildRepresentationProcessingStarted(totalNumberOfFiles);
    }
  }

  public void NotifySipBuildRepresentationProcessingCurrentStatus(int numberOfFilesAlreadyProcessed) {
    foreach (ISIPObserver sipObserver in observers) {
      sipObserver.SipBuildRepresentationProcessingCurrentStatus(numberOfFilesAlreadyProcessed);
    }
  }

  public void NotifySipBuildRepresentationProcessingEnded() {
    foreach (ISIPObserver sipObserver in observers) {
      sipObserver.SipBuildRepresentationProcessingEnded();
    }
  }

  public void NotifySipBuildRepresentationsProcessingEnded() {
    foreach (ISIPObserver sipObserver in observers) {
      sipObserver.SipBuildRepresentationsProcessingEnded();
    }
  }

  public void NotifySipBuildPackagingStarted(int totalNumberOfFiles) {
    foreach (ISIPObserver sipObserver in observers) {
      sipObserver.SipBuildPackagingStarted(totalNumberOfFiles);
    }
  }

  public void NotifySipBuildPackagingCurrentStatus(int numberOfFilesAlreadyProcessed) {
    foreach (ISIPObserver sipObserver in observers) {
      sipObserver.SipBuildPackagingCurrentStatus(numberOfFilesAlreadyProcessed);
    }
  }

  public void NotifySipBuildPackagingEnded() {
    foreach (ISIPObserver sipObserver in observers) {
      sipObserver.SipBuildPackagingEnded();
    }
  }
}