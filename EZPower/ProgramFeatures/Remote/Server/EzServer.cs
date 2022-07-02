using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EZPower.ProgramFeatures.Remote
{
    [ProgramFeature("EzServer", "Host a server locally.")]
    public class EzServer : ProgramFeature, IProgramFeature
    {
        EzServerData _data;

        public EzServer()
        {
            _data = InitFeatureData(new EzServerData("127.0.0.1", "1337"));
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

        [ProgramFeatureArgs("StartServer", 's', "Start socket server on local ip", "port", "[bool keepLocal = false]")]
        public string StartServer(params string[] args)
        {
            if (args.Length > 0)
            {
                _data.Port = args[0];
            }

            SaveGameData(_data);

            Process proc;
            if (args.Length > 1 && (args[1].ToLower().Contains("n") || args[1].ToLower().Contains("false")))
            {
                StartLocalServer(_data.Port);
            }
            else
            {
                proc = ProcessPower.CreateProcess("EzServer -s " + _data.Port + " false");
            }

            return "Server started on ["+_data.Ip+":"+_data.Port+"]";
        }

        void StartLocalServer(string port)
        {

            int intPort = int.Parse(_data.Port);
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(_data.Ip), intPort);
            try
            {
                Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                listener.Bind(localEndPoint);
                Debug.Log("Binded server.");
                listener.Listen(10);
                CLI.Print("Server Started on "+localEndPoint.Address+":"+localEndPoint.Port, ConsoleColor.Green);
                CLI.Print("Waiting for connection...", ConsoleColor.Yellow);
                Socket handler = listener.Accept();

                // Incoming data from the client.
                string data = null;
                byte[] bytes = null;

                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                Console.WriteLine("Text received : {0}", data);

                byte[] msg = Encoding.ASCII.GetBytes(data);
                handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Debug.Error(e.ToString());
            }
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
