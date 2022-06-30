using System;
using System.Collections.Generic;
using System.Text;
using EZPower.Core.CLIParser;

namespace EZPower.ProgramFeatures.WindowsCmd
{
    [ProgramFeature("CMD", "Access to cmd.")]
    public class CMD : ProgramFeature, IProgramFeature
    {
        [ProgramFeatureArgs("ShowHelpText", 'h', "Show available help commands.")]
        public void ShowHelpText()
        {
            base.GetHelpText();
        }

        [ProgramFeatureArgs("StartCmd", 'c', "Try cmd", "args0")]
        public string StartCmd(string args0)
        {
            System.Diagnostics.Process cmd = new System.Diagnostics.Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(args0);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            return (cmd.StandardOutput.ReadToEnd());
        }

        public void Dispose()
        {
            Debug.Warn("Disposted of " + GetType().Name);
            GC.Collect();
        }
    }
}
