using CommandLine;
using System.Reflection;

namespace dotnet_eark_sip_cli;

public class Create {
  public static void Main(string[] args) {
    try {
      Parser.Default.ParseArguments(args, LoadVerbs())
        .WithParsed(_args => {
          try {
            int exitCode = Run(_args);
            Environment.Exit(exitCode);
          } catch (InvalidPathException e) {
            Console.WriteLine(e.Message + "\n" + e);
            Environment.Exit(ExitCodes.EXIT_CODE_CREATE_INVALID_PATHS);
          } catch (InvalidArgumentsException e) {
            Console.WriteLine(e.Message + "\n" + e);
            Environment.Exit(ExitCodes.EXIT_CODE_CREATE_INVALID_ARGS);
          } catch (Exception e) {
            Console.WriteLine("An unexpected error occurred\n" + e);
            Environment.Exit(ExitCodes.EXIT_CODE_ERROR);
          }
        })
        .WithNotParsed(_ => {
          // Discard errors because they are already logged by the parser
          Environment.Exit(ExitCodes.EXIT_CODE_CREATE_MISSING_ARGS);
        });
    } catch (Exception e) {
      Console.WriteLine("An error occurred parsing arguments\n" + e);
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
    List<Representation> representationListArgs = args.GetRepresentations();
    List<Metadata> metadataListArgs = args.GetMetadataFiles();

    if (representationListArgs.Count() == 0 && metadataListArgs.Count() == 0) {
      throw new InvalidArgumentsException("At least one of representation or metadata file(s) must be provided");
    }

    if (!CommandUtils.ValidateRepresentationDataPaths(representationListArgs)) {
      throw new InvalidPathException("Make sure all the representation data paths exist");
    }

    if (!CommandUtils.ValidateDocumentationPaths(args.Documentation ?? [])) {
      throw new InvalidPathException("Make sure all the documentation paths exist");
    }

    if (!CommandUtils.ValidateMetadataPaths(metadataListArgs)) {
      throw new InvalidPathException("Make sure all the descriptive metadata paths exist");
    }

    if (!CommandUtils.ValidateMetadataSchemaPaths(metadataListArgs)) {
      throw new InvalidPathException("Make sure all the descriptive metadata schema paths exist");
    }

    LogSystem.LogOperatingSystemInfo();

    try {
      string sipPath = new SIPBuilder()
        .SetMetadataArgs(metadataListArgs)
        .SetOverride(args.OverrideSchema)
        .SetRepresentationArgs(representationListArgs)
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

      Console.WriteLine("E-ARK SIP created at " + sipPath.Normalize());

      return ExitCodes.EXIT_CODE_OK;
    } catch (Exception e) {
      Console.WriteLine("Failed to create the SIP\n" + e);
      return ExitCodes.EXIT_CODE_CREATE_CANNOT_SIP;
    }
  }
}