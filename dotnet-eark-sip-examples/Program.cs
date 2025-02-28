// 8885 files, 5657 folders, 3.77 GB
// Tempo, memória, CPU

// Exemplos a criar, para testar
//  - todos os ficheiros num SIP
//  - um SIP para cada ficheiro
//    - tentar correr em paralelo?

namespace dotnet_eark_sip_examples;

public class Program {
  public static void Main(string[] args) {
    string exampleToRun = args.FirstOrDefault("1");
    switch (exampleToRun) {
      case "1":
        Example1.Run();
        break;

      case "2":
        Example2.Run();
        break;

      case "3":
        Example3.Run();
        break;

      case "4":
        Example4.Run();
        break;

      default:
        Console.WriteLine("Example {0} not found", exampleToRun);
        break;
    }
  }
}