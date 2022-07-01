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

        [ProgramFeatureArgs("ScanPort", 's', "Scan default ip (if empty) or specific ip:port or portA-portB", "value")]
        public string ScanPort(string ipport)
        {
            //we dont need this
            //but maybe we will later..?
            List<PortResult> multipleResult = new List<PortResult>();
            string ip = "";
            string port = "";
            if (ipport != null && ipport != "" && ipport.Split("~",StringSplitOptions.RemoveEmptyEntries).Length>0)
            {
                string[] portDelta = ipport.Split("~", StringSplitOptions.RemoveEmptyEntries);

                int portA = int.Parse(portDelta[0]);
                int portB = int.Parse(portDelta[1]);

                ip = _portScanData.Ip;
                for (int i = portA; i <= portB; i++)
                {
                    port = i.ToString();
                    PortResult res = ScanPortResult(ip, port);
                    if (res.IsOpened)
                    {
                        CLI.Print("Port " + port + ":>" + res.Result, ConsoleColor.Green);
                    }
                    else
                    {
                        CLI.Print("Port " + port + " : " + res.Result, ConsoleColor.Red);
                    }
                    multipleResult.Add(res);
                }
            }
            else if (ipport != null && ipport != "" && ipport.Split(":", StringSplitOptions.RemoveEmptyEntries).Length >1)
            {
                ip = ipport.Split(":", StringSplitOptions.RemoveEmptyEntries)[0];
                port = ipport.Split(":", StringSplitOptions.RemoveEmptyEntries)[1];
                PortResult res = ScanPortResult(ip, port);
                if (res.IsOpened)
                {
                    CLI.Print("Port " + port + ":>" + res.Result, ConsoleColor.Green);
                }
                else
                {
                    CLI.Print("Port " + port + " : " + res.Result, ConsoleColor.Red);
                }
                multipleResult.Add(res);
            }
            else
            {
                port = _portScanData.Port;
                ip = _portScanData.Ip;
                PortResult res = ScanPortResult(ip, port);
                if (res.IsOpened)
                {
                    CLI.Print("Port " + port + ":>" + res.Result, ConsoleColor.Green);
                }
                else
                {
                    CLI.Print("Port " + port + " : " + res.Result, ConsoleColor.Red);
                }
                multipleResult.Add(res);
            }

            return "Scanned "+ip+":"+port;
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
                    result.Result = ex.Message;
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
