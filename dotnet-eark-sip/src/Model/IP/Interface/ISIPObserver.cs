public interface ISIPObserver {
  void SipBuildRepresentationsProcessingStarted(int totalNumberOfRepresentations);

  void SipBuildRepresentationProcessingStarted(int totalNumberOfFiles);

  void SipBuildRepresentationProcessingCurrentStatus(int numberOfFilesAlreadyProcessed);

  void SipBuildRepresentationProcessingEnded();

  void SipBuildRepresentationsProcessingEnded();

  void SipBuildPackagingStarted(int totalNumberOfFiles);

  void SipBuildPackagingCurrentStatus(int numberOfFilesAlreadyProcessed);

  void SipBuildPackagingEnded();
}