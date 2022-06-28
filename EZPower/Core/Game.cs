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
            CLI.Print("------------------------ EZ Tools ------------------------", System.ConsoleColor.DarkYellow);
            ShowAvailableFeatures();

            string inputText = "";

            while (inputText != "q")
            {
                inputText = CLI.ReadLine();
                string rawName = CLIParser.ParseFeatureName(inputText);
                string asmName = Reflect.GetClassFullName(rawName);

                try
                {
                    dynamic program = CLIParser.ParseCreateFeature(asmName);

                    try
                    {
                        CLIParser.ParseFeatureArgs(inputText, program);
                    }
                    catch
                    {
                        Debug.Error("Error: " + inputText);
                    }
                }
                catch
                {
                    Debug.Error("Error: Program failed " + rawName);
                }
            }
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

        void ShowAvailableFeatures(string featureName)
        {
            ProgramFeatureInfo[] features = Reflect.GetAllProgramFeatures();

            foreach (ProgramFeatureInfo info in features)
            {
                if (info.ProgramName.ToLower().Contains(featureName.ToLower()))
                {
                    CLI.Print(info.ProgramName, System.ConsoleColor.White, false);
                    CLI.Print(" - " + info.ProgramDescription, System.ConsoleColor.DarkGray, true);
                    for (int i = 0; i < info.HelpArgs.Length; i++)
                    {
                        CLI.Print("  ", false);
                        CLI.Print("-" + info.HelpArgs[i].Key, System.ConsoleColor.Cyan, false);

                        if (info.HelpArgs[i].Parameters.Length != 0)
                        {
                            for (int j = 0; j < info.HelpArgs[i].Parameters.Length; j++)
                            {
                                CLI.Print(" " + info.HelpArgs[i].Parameters[j], System.ConsoleColor.Cyan, false);
                            }
                        }
                        CLI.Print(" : " + info.HelpArgs[i].Description, System.ConsoleColor.DarkCyan, true);
                    }
                }
            }
        }
    }
}
