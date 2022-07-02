using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace EZPower
{
    public static class NetPower
    {
        public static IPAddress GetLocalIpAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            return null;
        }

        public static IPAddress[] GetLocalIpAddresses()
        {
            List<IPAddress> addresses = new List<IPAddress>();
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    addresses.Add(ip);
                }
            }
            return addresses.ToArray();
        }

        //todo: get public ip
        //download file?
    }
}