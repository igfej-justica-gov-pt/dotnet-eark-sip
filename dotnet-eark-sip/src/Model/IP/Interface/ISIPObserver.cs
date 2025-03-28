/// <summary>
/// Defines methods to observe the SIP building process, including representations and packaging.
/// </summary>
public interface ISIPObserver
{
  /// <summary>
  /// Notifies that the processing of SIP representations has started.
  /// </summary>
  /// <param name="totalNumberOfRepresentations">The total number of representations to process.</param>
  void SipBuildRepresentationsProcessingStarted(int totalNumberOfRepresentations);

  /// <summary>
  /// Notifies that the processing of a single SIP representation has started.
  /// </summary>
  /// <param name="totalNumberOfFiles">The total number of files in the representation.</param>
  void SipBuildRepresentationProcessingStarted(int totalNumberOfFiles);

  /// <summary>
  /// Provides the current status of the SIP representation processing.
  /// </summary>
  /// <param name="numberOfFilesAlreadyProcessed">The number of files already processed.</param>
  void SipBuildRepresentationProcessingCurrentStatus(int numberOfFilesAlreadyProcessed);

  /// <summary>
  /// Notifies that the processing of a single SIP representation has ended.
  /// </summary>
  void SipBuildRepresentationProcessingEnded();

  /// <summary>
  /// Notifies that the processing of all SIP representations has ended.
  /// </summary>
  void SipBuildRepresentationsProcessingEnded();

  /// <summary>
  /// Notifies that the SIP packaging process has started.
  /// </summary>
  /// <param name="totalNumberOfFiles">The total number of files to package.</param>
  void SipBuildPackagingStarted(int totalNumberOfFiles);

  /// <summary>
  /// Provides the current status of the SIP packaging process.
  /// </summary>
  /// <param name="numberOfFilesAlreadyProcessed">The number of files already processed during packaging.</param>
  void SipBuildPackagingCurrentStatus(int numberOfFilesAlreadyProcessed);

  /// <summary>
  /// Notifies that the SIP packaging process has ended.
  /// </summary>
  void SipBuildPackagingEnded();
}