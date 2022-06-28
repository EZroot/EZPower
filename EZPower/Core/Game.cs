using EZPower.Core.CLIParser;
using EZPower.ProgramFeatures;

namespace EZPower
{
    public class Game
    {
        public Game()
        {
        }

        public void Start()
        {
            PrintMotto();
            ShowAvailableFeatures();

            string inputText = "";

            while (inputText != "q")
            {
                inputText = CLI.ReadLine();

                CheckGameCommands(inputText);

                if (Reflect.DoesFeatureExist(CLIParser.ParseFeatureName(inputText)))
                {
                    dynamic program = CLIParser.ParseCreateFeature(Reflect.GetClassFullName(CLIParser.ParseFeatureName(inputText)));
                    //key command and parameters
                    try { CLIParser.ParseFeatureArgs(inputText, program); } catch { Debug.Error("Error: " + inputText); }
                }
            }
        }

        void CheckGameCommands(string cliText)
        {
            CLIParser.ParseImmediateCommand("clear", cliText, ClearCLI);
            CLIParser.ParseImmediateCommand("cls", cliText, ClearCLI);
            CLIParser.ParseImmediateCommand("help", cliText, ShowHelpText);
            CLIParser.ParseImmediateCommand("features", cliText, ShowAvailableFeatures);

        }

        void ShowHelpText()
        {
            CLI.Print("help", System.ConsoleColor.Green, false);
            CLI.Print(" - Shows this text", System.ConsoleColor.Yellow);

            CLI.Print("features",System.ConsoleColor.Green, false);
            CLI.Print(" - Show all available features", System.ConsoleColor.Yellow);

            CLI.Print("clear", System.ConsoleColor.Green, false);
            CLI.Print(" - Clears console screen", System.ConsoleColor.Yellow);
        }

        void ClearCLI()
        {
            CLI.Clear();
            PrintMotto();
        }

        void PrintMotto()
        {
            CLI.Print("[------------------------< EZ Tools >------------------------]", System.ConsoleColor.DarkYellow);
        }

        void ShowAvailableFeatures()
        {
            ProgramFeatureInfo[] features = Reflect.GetAllProgramFeatures();

            foreach (ProgramFeatureInfo info in features)
            {
                CLI.Print(  info.ProgramName , System.ConsoleColor.White, false);
                CLI.Print(" - " + info.ProgramDescription, System.ConsoleColor.DarkGray, true);
                for(int i =0; i < info.HelpArgs.Length;i++)
                {
                    CLI.Print("  ", false);
                    CLI.Print("-"+info.HelpArgs[i].Key, System.ConsoleColor.Cyan, false);

                    if (info.HelpArgs[i].Parameters.Length != 0)
                    {
                        for (int j = 0; j < info.HelpArgs[i].Parameters.Length; j++)
                        {
                            CLI.Print(" "+ info.HelpArgs[i].Parameters[j], System.ConsoleColor.Cyan, false);
                        }
                    }
                    CLI.Print(" : " + info.HelpArgs[i].Description, System.ConsoleColor.DarkCyan, true);
                }
            }
        }
    }
}
