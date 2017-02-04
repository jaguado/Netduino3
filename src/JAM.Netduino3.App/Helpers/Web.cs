using EmbeddedWebserver.Core.Handlers.Interfaces;
using Microsoft.SPOT.Net.NetworkInformation;

namespace JAM.Netduino3.App.Helpers
{
    public class Web
    {
        public int Port { set; get; }
        public string Ip { set; get; }
        private WebServer _web { set; get; }
        public Web(int port){
            Port = port;

            _web = new WebServer();
            _web.StartListening();
           
            //Get Ip
            var nis = NetworkInterface.GetAllNetworkInterfaces();
            if (nis != null && nis.Length>0)
            {
                Ip = nis[0].IPAddress;
            }
        }

        public void RegisterHandler(string pAccessUri, IHandler pHandlerInstance)
        {
            if (_web != null)
            {
                _web.RegisterThreadSafeHandler(pAccessUri, pHandlerInstance);
            }
        }

        public int getRequestErrors()
        {
            throw new System.Exception("Not implemented");
        }
    }
}
