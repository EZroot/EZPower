using System;
using System.Collections.Generic;
using System.Text;

namespace EZPower.ProgramFeatures.EZFTP
{
    [ProgramFeature("EZFTP", "File transfer protocol.")]
    public class EZFTP : ProgramFeature, IProgramFeature
    {
        [ProgramFeatureArgs("HelpText", 'h', "Show a list of commands")]
        public void ShowHelpText()
        {
            base.GetHelpText();
        }

    }
}
