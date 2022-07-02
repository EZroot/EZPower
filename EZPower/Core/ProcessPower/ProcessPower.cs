using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EZPower
{
    public static class ProcessPower
    {
        public static Process CreateProcess(string args, bool waitForExit = false)
        {
            Process process = new Process();
            // Configure the process using the StartInfo properties.
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.FileName = GetCurrentProcessName();
            process.StartInfo.Arguments = args;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.Start();

            if (waitForExit)
            {
                process.WaitForExit();// Waits here for the process to exit.
            }

            return process;
        }

        public static Process GetCurrentProcess()
        {
            return Process.GetCurrentProcess();
        }

        public static Process GetProcessByID(int id)
        {
            return Process.GetProcessById(id);
        }
        public static Process[] GetProcesses(int id)
        {
            return Process.GetProcesses();
        }
        public static Process[] GetProcessesByName(string name)
        {
            return Process.GetProcessesByName(name);
        }

        public static string GetCurrentProcessName()
        {
            return Process.GetCurrentProcess().ProcessName;
        }

        public static string GetCurrentMachineName()
        {
            return Process.GetCurrentProcess().MachineName;
        }
    }
}
