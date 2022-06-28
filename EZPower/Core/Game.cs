using EZPower.Core.CLIParser;
using EZPower.Core.NetPower;
using EZPower.ProgramFeatures;
using EZPower.ProgramFeatures.Shipateer;
using System;
using System.IO;

namespace EZPower
{
    /*
     * 
     * create state machine and have diff states
     * that way if we open cmd then we can use that as a input stream instead of regular
     * 
     */
    public class Game
    {
        public Game()
        {
        }

        UserProfile _profile;

        public void Start()
        {
            InitUserProfile();
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
            CLIParser.ParseImmediateCommand("help", cliText, ShowHelpText);

            CLIParser.ParseImmediateCommand("features", cliText, ShowAvailableFeatures);

            CLIParser.ParseImmediateCommand("ip", cliText, ShowIPAddress);

            CLIParser.ParseImmediateCommand("newuser", cliText, CreateUserProfile);
            CLIParser.ParseImmediateCommand("user", cliText, ShowUserProfile);

            CLIParser.ParseImmediateCommand("clear", cliText, ClearCLI);
            CLIParser.ParseImmediateCommand("cls", cliText, ClearCLI);

            CLIParser.ParseImmediateCommand("test", cliText, TestJson);
        }

        void ShowIPAddress()
        {
            CLI.Print("Local IP: "+NetPower.GetLocalIpAddress());
        }

        void ShowHelpText()
        {
            CLI.Print("help", System.ConsoleColor.Green, false);
            CLI.Print(" - Shows this text", System.ConsoleColor.Yellow);

            CLI.Print("features", System.ConsoleColor.Green, false);
            CLI.Print(" - Show all available features", System.ConsoleColor.Yellow);

            CLI.Print("ip", System.ConsoleColor.Green, false);
            CLI.Print(" - Show current ip address", System.ConsoleColor.Yellow);

            CLI.Print("newuser", System.ConsoleColor.Green, false);
            CLI.Print(" - Reset your user profile", System.ConsoleColor.Yellow);

            CLI.Print("user", System.ConsoleColor.Green, false);
            CLI.Print(" -  Show your user profile", System.ConsoleColor.Yellow);

            CLI.Print("clear", System.ConsoleColor.Green, false);
            CLI.Print(" - Clears console screen", System.ConsoleColor.Yellow);

            CLI.Print("test", System.ConsoleColor.Green, false);
            CLI.Print(" - Try out the latest crazy", System.ConsoleColor.Yellow);
        }

        void InitUserProfile()
        {
            _profile = (UserProfile)CLIParser.LoadJsonObject<UserProfile>(typeof(UserProfile).Name+".ezdat");
            if (_profile == null)
            {
                CreateUserProfile();
            }
            CLI.DefaultPrefix = _profile.ConsolePrefix;
        }

        void CreateUserProfile()
        {
            CLI.Print("New User: ", false);
            string user = Console.ReadLine();

            CLI.Print("Prefix: ", false);
            string prefix = Console.ReadLine();

            _profile = new UserProfile(user, Directory.GetCurrentDirectory(), prefix, NetPower.GetLocalIpAddress().ToString());
            _profile.SaveGameData<UserProfile>(_profile, typeof(UserProfile).Name + ".ezdat");
            CLI.Print("User profile [" + user + "] created!");
            CLI.DefaultPrefix = _profile.ConsolePrefix;
        }

        void ShowUserProfile()
        {
            CLI.Print(_profile.UserName);
            CLI.Print(_profile.ConsolePrefix);
            CLI.Print(_profile.CurrentPath);
            CLI.Print(_profile.CurrentIP);
        }

        void TestJson()
        {
            //PlayerData data = new PlayerData(new PlayerProfileStats("dillon", 100, 100), new PlayerCombatStats(1, 1, 1, 1), new PlayerHistoryStats(0, "", 0, 0, 0, 0), new PlayerMoneyStats(0, 0));
            ShipateerData data = new ShipateerData();

            CLI.Print(CLIParser.ToJson(data));
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

        void ClearCLI()
        {
            CLI.Clear();
            PrintMotto();
        }

        void PrintMotto()
        {
            CLI.Print("[------------------------< EZ Tools >------------------------]", System.ConsoleColor.DarkYellow);
        }
    }
}
