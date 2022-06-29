using System;
using System.Collections.Generic;
using System.Text;

namespace EZPower.ProgramFeatures.EZFTP
{
    [ProgramFeature("FTP", "File transfer protocol.")]
    public class FTP : ProgramFeature, IProgramFeature
    {
        [ProgramFeatureArgs("HelpText", 'h', "Show a list of commands")]
        public void ShowHelpText()
        {
            base.GetHelpText();
        }

        [ProgramFeatureArgs("StringTest", 's', "Show a list of commands", "echo")]
        public string ShowTest(string derp)
        {
            return derp;
        }
    }
}
