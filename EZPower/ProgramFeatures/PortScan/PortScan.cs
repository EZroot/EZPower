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
            _portScanData = InitFeatureData(new PortScanData("127.0.0.1","1337"));
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
            if (lol == null || lol.Length == 0)
            {
                return result;
            }
            foreach (string s in lol)
            {
                result += ("Wow: " + s+" ");
            }
            return result;
        }

        [ProgramFeatureArgs("ScanPort", 's', "Scan default ip (if empty) or specific ip:port or portA-portB", "value")]
        public string ScanPort(string ipport)
        {
            //we dont need this
            //but maybe we will later..?
            List<PortResult> multipleResult = new List<PortResult>();

            if (ipport != null && ipport != "" && ipport.Contains("~"))
            {
                string[] portDelta = ipport.Split("~", StringSplitOptions.RemoveEmptyEntries);

                int portA = int.Parse(portDelta[0]);
                int portB = int.Parse(portDelta[1]);

                for (int i = portA; i <= portB; i++)
                {
                    _portScanData.Port = i.ToString();

                    PortResult res = ScanPortResult(_portScanData.Ip, _portScanData.Port);
                    if (res.IsOpened)
                    {
                        CLI.Print("["+_portScanData.Ip + ":" + _portScanData.Port + "] " + res.Result, ConsoleColor.Green);
                    }
                    else
                    {
                        CLI.Print("[" + _portScanData.Ip + ":" + _portScanData.Port + "] " + res.Result, ConsoleColor.Red);
                    }
                    multipleResult.Add(res);
                }
            }
            else if (ipport != null && ipport != "" && ipport.Contains(":"))
            {
                _portScanData.Ip = ipport.Split(":", StringSplitOptions.RemoveEmptyEntries)[0];
                _portScanData.Port = ipport.Split(":", StringSplitOptions.RemoveEmptyEntries)[1];
                SaveGameData(_portScanData);

                PortResult res = ScanPortResult(_portScanData.Ip, _portScanData.Port);
                if (res.IsOpened)
                {
                    CLI.Print("[" + _portScanData.Ip + ":" + _portScanData.Port + "] " + res.Result, ConsoleColor.Green);
                }
                else
                {
                    CLI.Print("[" + _portScanData.Ip + ":" + _portScanData.Port + "] " + res.Result, ConsoleColor.Red);
                }
                multipleResult.Add(res);
            }
            else if(ipport != null && ipport != "")
            {
                _portScanData.Port = ipport;
                SaveGameData(_portScanData);
                PortResult res = ScanPortResult(_portScanData.Ip, _portScanData.Port);
                if (res.IsOpened)
                {
                    CLI.Print("[" + _portScanData.Ip + ":" + _portScanData.Port + "] " + res.Result, ConsoleColor.Green);
                }
                else
                {
                    CLI.Print("[" + _portScanData.Ip + ":" + _portScanData.Port + "] " + res.Result, ConsoleColor.Red);
                }
                multipleResult.Add(res);
            }
            else
            {
                PortResult res = ScanPortResult(_portScanData.Ip, _portScanData.Port);
                if (res.IsOpened)
                {
                    CLI.Print("[" + _portScanData.Ip + ":" + _portScanData.Port + "] " + res.Result, ConsoleColor.Green);
                }
                else
                {
                    CLI.Print("[" + _portScanData.Ip + ":" + _portScanData.Port + "] " + res.Result, ConsoleColor.Red);
                }
                multipleResult.Add(res);
            }

            return "Finished scanning "+ _portScanData.Ip+":"+_portScanData.Port;
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

        [ProgramFeatureArgs("ShowCurrentData", 'g', "Show current ip:port")]
        public string ShowCurrentData()
        {
            string result = _portScanData.Ip +":"+_portScanData.Port;
            return "Data -> " + result;
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

                _portScanData.Port = realPort.ToString();
                _portScanData.Ip = ipAddress.ToString();
                SaveGameData(_portScanData);
                try
                {

                    var rr = socket.BeginConnect(ipAddress, realPort, null,null);
                    bool success = rr.AsyncWaitHandle.WaitOne(3000, true);
                    if (success)
                    {
                        result.IsOpened = true;
                        result.Result = "Opened -> " + _portScanData.Ip + ":" + realPort;
                    }
                    else
                    {
                        result.IsOpened = false;
                        result.Result = "Closed -> " + _portScanData.Ip + ":" + realPort;
                    }
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                    {
                        Debug.Error("Socket: failed to connect to "+ip+":"+port);
                    }
                    result.IsOpened = false;
                    result.Result = "Closed -> "+_portScanData.Ip+":"+realPort;
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
            Debug.Warn("Disposed of "+GetType().Name);
            GC.Collect();
        }
    }
}
