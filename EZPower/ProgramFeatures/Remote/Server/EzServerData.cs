using System;
using System.Collections.Generic;
using System.Text;

namespace EZPower.ProgramFeatures.Remote
{
    [Serializable]
    public class EzServerData
    {
        string _ip;
        string _port;

        public EzServerData(string ip, string port)
        {
            _ip = ip;
            _port = port;
        }

        public string Ip { get => _ip; set => _ip = value; }
        public string Port { get => _port; set => _port = value; }
    }
}
