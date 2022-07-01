using EZPower.Core.CLIParser;
using EZPower.Core.NetPower;
using EZPower.ProgramFeatures;
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
                    using (dynamic program = CLIParser.ParseCreateFeature(Reflect.GetClassFullName(CLIParser.ParseFeatureName(inputText))))
                    {
                        //key command and parameters
                        try
                        {
                            CLI.Print(CLIParser.ParseFeatureName(inputText) + ":> ", ConsoleColor.Yellow, false);
                            CLI.Print(CLIParser.ParseFeatureArgs(inputText, program), ConsoleColor.Green);
                        }
                        catch(Exception e)
                        {
                            Debug.Error("[Error] " + inputText + " : "+e.Message);
                        }

                    }
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
                    CLI.Print("-"+info.HelpArgs[i].Key, System.ConsoleColor.Yellow, false);

                    if (info.HelpArgs[i].Parameters.Length != 0)
                    {
                        for (int j = 0; j < info.HelpArgs[i].Parameters.Length; j++)
                        {
                            CLI.Print(" "+ info.HelpArgs[i].Parameters[j], System.ConsoleColor.DarkYellow, false);
                        }
                    }
                    CLI.Print(" : " + info.HelpArgs[i].Description, System.ConsoleColor.White, true);
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

        void InitUserProfile()
        {
            _profile = (UserProfile)CLIParser.LoadJsonObject<UserProfile>(typeof(UserProfile).Name);
            if (_profile == null)
            {
                CreateUserProfile();
            }
            CLI.DefaultPrefix = _profile.UserName + "@" + NetPower.GetLocalIpAddress() + _profile.ConsolePrefix;
        }

        void CreateUserProfile()
        {
            CLI.Print("New User: ", false);
            string user = Console.ReadLine();

            CLI.Print("Prefix: ", false);
            string prefix = Console.ReadLine();

            _profile = new UserProfile(user, Directory.GetCurrentDirectory(), prefix, NetPower.GetLocalIpAddress().ToString());
            _profile.SaveGameData<UserProfile>(_profile, typeof(UserProfile).Name);
            CLI.Print("User profile [" + user + "] created!");
        }

        void ShowUserProfile()
        {
            CLI.Print(_profile.UserName);
            CLI.Print(_profile.ConsolePrefix);
            CLI.Print(_profile.CurrentPath);
            CLI.Print(_profile.CurrentIP);
        }

    }
}
