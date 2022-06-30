using EZPower.Core.CLIParser;
using EZPower.Core.NetPower;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
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
    [ProgramFeature("PortScan", "Scan ports on source destination.")]
    public class PortScan : ProgramFeature, IProgramFeature
    {
        PortScanData _portScanData;

        public PortScan()
        {
            _portScanData = InitFeatureData<PortScanData>(new PortScanData("127.0.0.1","1337"));
        }

        [ProgramFeatureArgs("ShowHelpText", 'h', "Show a list of commands")]
        public void ShowHelpText()
        {
            base.GetHelpText();
        }

        [ProgramFeatureArgs("SetIp", 'i', "Ip address (0.0.0.0 format)", "ipaddress")]
        public string SetIp(string ip)
        {
            _portScanData.Ip = ip;
            SaveGameData(_portScanData);
            return "IP set: "+ip;
        }

        [ProgramFeatureArgs("SetPort", 'p', "Specific port of the ip to scan", "port")]
        public string SetPort(string port)
        {
            _portScanData.Port = port;
            SaveGameData(_portScanData);
            return "Port set: " + port;
        }

        [ProgramFeatureArgs("TestMeDaddy", 'g', "asdasdasd", "works[]")]
        public string TestMeDaddy(params string[] lol)
        {
            string result = "";
            if (lol == null ||  lol.Length==0)
            {
                return result;
            }
            foreach(string s in lol)
            {
                result+=("Wow: "+s);
            }
            return result;
        }

        [ProgramFeatureArgs("ScanPort", 's', "Scan ip", "[ip:port]")]
        public string ScanPort(string ipport)
        {
            PortResult result;
            string ip;
            string port;
            
            if (ipport != null && ipport != "" && ipport.Split(":", StringSplitOptions.RemoveEmptyEntries).Length >1)
            {
                ip = ipport.Split(":", StringSplitOptions.RemoveEmptyEntries)[0];
                port = ipport.Split(":", StringSplitOptions.RemoveEmptyEntries)[1];
                result = ScanPortResult(ip, port);
            }
            else
            {
                port = _portScanData.Port;
                ip = _portScanData.Ip;
                result = ScanPortResult(_portScanData.Ip, _portScanData.Port);
            }
            return ip+":"+port+" ~ "+result.IsOpened+" Result: " + result.Result;
        }

        [ProgramFeatureArgs("PingIp", 'n', "Ping ip", "ipaddress")]
        public string PingIp(string ipaddress)
        {
            string ip = ipaddress;
            if (ipaddress == "")
            {
                ip = _portScanData.Ip;
            }
            string result = "";
            return "Ping " + result;
        }


        PortResult ScanPortResult(string ip, string port)
        {
            PortResult result = new PortResult { IsOpened = false, Result = "none" };
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress;
            int realPort;
            try
            {
                int.TryParse(port, out realPort);
                IPAddress.TryParse(ip, out ipAddress);
                try
                {
                    socket?.Connect(ipAddress, realPort);
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                    {
                        Debug.Error("Socket: failed to connect to "+ip+":"+port);
                    }
                    result.IsOpened = false;
                    result.Result = ip+":"+port+" - " +ex.Message;
                }
                finally
                {
                    if (socket?.Connected ?? false)
                    {
                        Debug.Error("Disconnecting socket from " + ip + ":" + port);
                        socket?.Disconnect(false);
                    }
                    socket?.Close();
                }
            }
            catch { Debug.Error("Ip/Port: Failed to parse port/ip"); }

            return result;
        }

        public void Dispose()
        {
            //Debug.Warn("Disposted of "+GetType().Name);
            GC.Collect();
        }
    }
}
