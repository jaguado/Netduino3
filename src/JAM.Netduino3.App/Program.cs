using System;
using Microsoft.SPOT;
using System.Threading;

namespace JAM.Netduino3.App
{
    public class Program
    {
        public static void Main()
        {
            var webServer = new WebServerHelper();
            webServer.StartServer(80);
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
