using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EZPower.ProgramFeatures.ProcessProf
{
    [ProgramFeature("ProcessProf", "Everything you need to know about processes")]
    public class ProcessProf : ProgramFeature, IProgramFeature
    {
        ProcessProfData _data;

        public ProcessProf()
        {
            _data = InitFeatureData(new ProcessProfData(ProcessPower.GetCurrentProcessName()));
        }

        [ProgramFeatureArgs("CreateProcessTest", 'c', "Create new process", "args0")]
        public string CreateProcessTest(params string[] args0)
        {
            string input = "";
            Process process;

            if (args0 == null || args0.Length == 0)
            {
                process = ProcessPower.CreateProcess("Portscan -h", false);
                return "Started: " + process.ProcessName + " [" + process.Id + "]";
            }
            foreach (string s in args0)
            {
                input += s + " ";
            }

            process = ProcessPower.CreateProcess(input, false);
            return "Started: " + process.ProcessName + " [" + process.Id + "]";
        }

        [ProgramFeatureArgs("GetCurrentProcessName", 'i', "Show process name")]
        public string GetCurrentProcessName()
        {
            return ProcessPower.GetCurrentProcessName();
        }

        [ProgramFeatureArgs("GetCurrentMachineName", 'm', "Show machine name")]
        public string GetCurrentMachineName()
        {
            return ProcessPower.GetCurrentMachineName();
        }

        [ProgramFeatureArgs("ShowHelpText", 'h', "Show a list of commands")]
        public void ShowHelpText()
        {
            base.GetHelpText();
        }

        public void Dispose()
        {
            Debug.Warn("Disposed of " + GetType().Name);
            GC.Collect();
        }
    }
}
