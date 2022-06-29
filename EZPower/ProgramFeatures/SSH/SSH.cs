using System;
using System.Collections.Generic;
using System.Text;

namespace EZPower.ProgramFeatures.SSAysh
{
    [ProgramFeature("SSH", "SSH commands.")]
    public class SSH : ProgramFeature, IProgramFeature
    {
        [ProgramFeatureArgs("HelpText", 'h', "Show a list of commands")]
        public void ShowHelpText()
        {
            base.GetHelpText();
        }

    }
}
