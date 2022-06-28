using EZPower.Core.CLIParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZPower.ProgramFeatures
{
    public class ProgramFeature 
    {

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void LateUpdate()
        {

        }

        public void GetHelpText()
        {
            ProgramFeatureInfo[] features = Reflect.GetAllProgramFeatures();

            foreach (ProgramFeatureInfo info in features)
            {
                if (info.ProgramName.ToLower().Contains(this.GetType().Name.ToLower()))
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
