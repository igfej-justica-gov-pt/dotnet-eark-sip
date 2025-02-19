using CommandLine;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace dotnet_eark_sip_cli;

public class Create {
  private static readonly ILogger logger = DefaultLogger.Create<Create>();

  public static void Main(string[] args) {
    try {
      Parser.Default.ParseArguments(args, LoadVerbs())
        .WithParsed(options => {
          try {
            int exitCode = Run(options);
            Environment.Exit(exitCode);
          } catch (InvalidPathException e) {
            logger.LogError(e, e.Message);
            Environment.Exit(ExitCodes.EXIT_CODE_CREATE_INVALID_PATHS);
          } catch (InvalidArgumentsException e) {
            logger.LogError(e, e.Message);
            Environment.Exit(ExitCodes.EXIT_CODE_CREATE_INVALID_ARGS);
          } catch (Exception e) {
            logger.LogError(e, "An unexpected error occurred");
            Environment.Exit(ExitCodes.EXIT_CODE_ERROR);
          }
        })
        .WithNotParsed(_ => {
          // Discard errors because they are already logged by the parser
          Environment.Exit(ExitCodes.EXIT_CODE_CREATE_MISSING_ARGS);
        });
    } catch (Exception e) {
      logger.LogError(e, "An error occurred");
      Environment.Exit(ExitCodes.EXIT_CODE_ERROR);
    }
  }

  private static Type[] LoadVerbs() {
    return Assembly.GetExecutingAssembly()
      .GetTypes()
      .Where(t => t.GetCustomAttribute<VerbAttribute>() != null)
      .ToArray();
  }

  private static int Run(object args) {
    if (args is Options options) {
      return Run(options);
    }

    throw new InvalidArgumentsException("Invalid arguments");
  }

  private static int Run(Options args) {
    if (CommandUtils.ValidateRepresentationDataPaths(args.RepresentationListArgs)) {
      throw new InvalidPathException("Make sure all the representation data paths exist");
    }

    if (CommandUtils.ValidateDocumentationPaths(args.Documentation)) {
      throw new InvalidPathException("Make sure all the documentation paths exist");
    }

    if (CommandUtils.ValidateMetadataPaths(args.MetadataListArgs)) {
      throw new InvalidPathException("Make sure all the descriptive metadata paths exist");
    }

    if (CommandUtils.ValidateMetadataSchemaPaths(args.MetadataListArgs)) {
      throw new InvalidPathException("Make sure all the descriptive metadata schema paths exist");
    }

    LogSystem.LogOperatingSystemInfo();

    try {
      string sipPath = new SIPBuilder()
        .SetMetadataArgs(args.MetadataListArgs?.ToList())
        .SetOverride(args.OverrideSchema)
        .SetRepresentationArgs(args.RepresentationListArgs?.ToList())
        .SetTargetOnly(args.TargetOnly)
        .SetSipId(args.SIPId)
        .SetAncestors(args.Ancestors?.ToList())
        .SetDocumentation(args.Documentation?.ToList())
        .SetSoftwareVersion(VersionProvider.GetVersion())
        .SetPath(args.Path)
        .SetSubmitterAgentId(args.SubmitterAgentId)
        .SetSubmitterAgentName(args.SubmitterAgentName)
        .SetChecksumAlgorithm(args.ChecksumAlgorithm)
        .SetVersion(args.Version)
        .SetWriteStrategy(args.Strategy)
        .Build();

      logger.LogInformation("E-ARK SIP created at {sipPath}", sipPath.Normalize());

      return ExitCodes.EXIT_CODE_OK;
    } catch (Exception e) {
      logger.LogError(e, "Failed to create the SIP");
      return ExitCodes.EXIT_CODE_CREATE_CANNOT_SIP;
    }
  }
}