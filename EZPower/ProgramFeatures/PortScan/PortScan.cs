using EZPower.Core.CLIParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZPower.ProgramFeatures
{

    [ProgramFeature("PortScanner", "Scan ports on source destination.")]
    public class PortScan : ProgramFeature, IProgramFeature
    {
        [ProgramFeatureArgs("HelpText", 'h', "Show a list of commands")]
        public void ShowHelpText()
        {
            base.GetHelpText();
        }

        public void Derp()
        {
            CLI.Print("YASDUASJID");
        }
    }
}
