using System;
using System.Collections.Generic;
using System.Text;

namespace EZPower.ProgramFeatures
{
    public interface IProgramFeature : IDisposable
    {
        void ShowHelpText();
    }
}
