﻿using System;
using System.Net;
using System.Net.Sockets;

namespace Thresh.Unity.Utility
{
    public static class NetUtil
    {
        public static byte[] IntToBytes(int i)
        {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(i));
        }

        public static int NetBytesToInt(byte[] bytes)
        {
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bytes, 0));
        }

        public static string Resolve(string ipOrHost)
        {
            if (string.IsNullOrEmpty(ipOrHost))
            {
                throw new ArgumentException("Supplied string must not be empty", "ipOrHost");
            }

            ipOrHost = ipOrHost.Trim();

            IPAddress ipAddress = null;
            if (!IPAddress.TryParse(ipOrHost,out ipAddress))
            {
                try
                {
                    IPAddress[] addresses = Dns.GetHostAddresses(ipOrHost);
                    if (addresses == null || addresses.Length == 0)
                    {
                        return ipOrHost;
                    }

                    int index = Core.Utility.MathUtil.GetRandom(0, addresses.Length - 1);
                    return addresses[index].ToString();
                }
                catch (SocketException e)
                {
                    if (e.SocketErrorCode == SocketError.HostNotFound)
                    {
                        return ipOrHost;
                    }
                    else
                    {
                        throw e;
                    }
                }
            }

            return ipOrHost;
        }
    }
}