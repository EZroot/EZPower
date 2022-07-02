using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EZPower.ProgramFeatures.ProcessProf
{
    [Serializable]
    public class ProcessProfData
    {
        string _processName;

        public ProcessProfData(string processName)
        {
            _processName = processName;
        }

        public string ProcessName { get => _processName; set => _processName = value; }
    }
}
