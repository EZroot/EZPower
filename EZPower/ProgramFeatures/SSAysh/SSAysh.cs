using System;
using System.Collections.Generic;
using System.Text;

namespace EZPower.ProgramFeatures.SSAysh
{
    [ProgramFeature("SSAysh", "SSH commands.")]
    public class SSAysh : ProgramFeature, IProgramFeature
    {
        [ProgramFeatureArgs("HelpText", 'h', "Show a list of commands")]
        public void ShowHelpText()
        {
            base.GetHelpText();
        }

    }
}
