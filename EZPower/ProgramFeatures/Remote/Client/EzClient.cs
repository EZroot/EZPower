using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EZPower.ProgramFeatures.Remote
{
    [ProgramFeature("EzClient", "Connect to a remote server/host.")]
    public class EzClient : ProgramFeature, IProgramFeature
    {
        EzClientData _data;

        public EzClient()
        {
            _data = InitFeatureData(new EzClientData("127.0.0.1","1337"));
        }

        [ProgramFeatureArgs("Connect", 'c', "Connect to server ip port", "ip","port")]
        public string Connect(params string[] ipPort)
        {
            string result = "";

            if (ipPort.Length == 1)
            {
                Debug.Error("FUCK");
                _data.Ip = ipPort[0];
            }
            else if (ipPort.Length > 1)
            {
                Debug.Error("FUCKasdasd");

                _data.Ip = ipPort[0];
                _data.Port = ipPort[1];
            }
                SaveGameData(_data);

            Socket socket = ConnectToSocket(_data.Ip, _data.Port);
            result += _data.Ip + ":" + _data.Port + " Connected: " + socket.Connected;
            return result;
        }

        Socket ConnectToSocket(string ip, string port)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress;
            int realPort;
            try
            {
                int.TryParse(port, out realPort);
                IPAddress.TryParse(ip, out ipAddress);
                try
                {
                    IPEndPoint localEndPoint = new IPEndPoint(ipAddress, realPort);
                    CLI.Print("Trying to connect to [" + localEndPoint.Address + ":" + localEndPoint.Port + "]...", ConsoleColor.White);
                    socket?.Connect(localEndPoint);
                    if(socket?.Connected ?? true)
                    {

                        CLI.Print("Succesfully connected to " + localEndPoint.Address + ":" + localEndPoint.Port + "!", ConsoleColor.Green);
                    }
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                    {
                        Debug.Error("Socket: failed to connect to " + ip + ":" + port);
                    }
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
            return socket;
        }


        [ProgramFeatureArgs("SetIp", 'i', "Ip address (0.0.0.0 format)", "ipaddress")]
        public string SetIp(string ip)
        {
            _data.Ip = ip;
            SaveGameData(_data);
            return "IP set: " + ip;
        }

        [ProgramFeatureArgs("SetPort", 'p', "Specific port of the ip to connect", "port")]
        public string SetPort(string port)
        {
            _data.Port = port;
            SaveGameData(_data);
            return "Port set: " + port;
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
