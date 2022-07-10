using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using Sharp7;

namespace Assets.cs.Communication
{
    public static class CommHelper
    {
        public static string err = null;
        private static IPAddress IP_Address = new IPAddress(3232235521); //192.168.0.1

        public static int TryIPandConn(string address)
        {
            if (address.Split('.').Length != 4)
                return 1; //Wrong format

            if (!IPAddress.TryParse(address, out IP_Address))
                return 2; //Its not IP

            if (GetPing(IP_Address).Result < 0)
                return 3; //Ping problem

            if (TryConnect(IP_Address))
                return 0;

            return 4; //Problem with connection
        }

        public static Task<long> GetPing(IPAddress address)
        {
            Ping pinger = new Ping();

            string data = "Sending Ping by UnityChessPLC ok";
            byte[] buffer = Encoding.ASCII.GetBytes(data);

            // Wait 1.5 seconds for a reply.
            int timeout = 1500;

            // Set options for transmission:
            // The data can go through 64 gateways or routers
            // before it is destroyed, and the data packet
            // cannot be fragmented.
            PingOptions options = new PingOptions(64, true);

            Task<long> t = new Task<long>(() =>
           {
                // Send the request.
                PingReply reply = pinger.Send(address, timeout, buffer, options);

               if (reply.Status == IPStatus.Success)
               {
                   return reply.RoundtripTime;
               }
               else
               {
                   return (long)-1;
               }
           });
            t.Start();

            return t;
        }

        public static bool TryConnect(IPAddress address)
        {
            S7Client s7Client = new S7Client();
            int resultOfConnection = s7Client.ConnectTo(address.ToString(), 0, 1);
            s7Client.Disconnect();

            return resultOfConnection == 0 ? true : false;
        }

        public static void CPUinfo(IPAddress address = null)
        {
            if (address == null)
                address = IP_Address;

            S7Client s7Client = new S7Client();
            S7Client.S7CpuInfo s7CpuInfo = new S7Client.S7CpuInfo();

            s7Client.ConnectTo("192.168.0.1".ToString(), 0, 1);
            s7Client.GetCpuInfo(ref s7CpuInfo);
            s7Client.Disconnect();

            UnityEngine.MonoBehaviour.print(s7CpuInfo.ASName);
            UnityEngine.MonoBehaviour.print(s7CpuInfo.Copyright);
            UnityEngine.MonoBehaviour.print(s7CpuInfo.ModuleName);
            UnityEngine.MonoBehaviour.print(s7CpuInfo.ModuleTypeName);
            UnityEngine.MonoBehaviour.print(s7CpuInfo.SerialNumber);
        }
    }
}
