using EZPower.Core.CLIParser;
using EZPower.Core.NetPower;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZPower.ProgramFeatures
{

    /*
     * 
     * get target ip
     * set it as a var in this program
     * have data of subject computer
     * executing commands in right order should fill and select this data
     * good way to get ip or something and then port and then scan ip and that port if it changes
     *
     */
    [ProgramFeature("PortScanner", "Scan ports on source destination.")]
    public class PortScan : ProgramFeature, IProgramFeature
    {
        PortScanData _portScanData;

        public PortScan()
        {
            Debug.Log("trying to init portscan data");
            _portScanData = InitFeatureData<PortScanData>(new PortScanData("127.0.0.1","1337"));
            Debug.Log(_portScanData.Ip);
        }

        ~PortScan()
        {
            Debug.Error("Program disposed.");
        }

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
