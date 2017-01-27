using Microsoft.SPOT.Net.NetworkInformation;

namespace JAM.Netduino3.Helpers
{
    public class WebServerHelper
    {
        public int Port { set; get; }
        public string Ip { set; get; }
        public WebServerHelper(int port){
            Port = port;

            var ws = new WebServer();
            ws.StartListening();
            
            //Get Ip
            var nis = NetworkInterface.GetAllNetworkInterfaces();
            if (nis != null && nis.Length>0)
            {
                Ip = nis[0].IPAddress;
            }
        }
    }
}
