using System.Threading;
using System;
using Microsoft.SPOT;
using System.Net;
using JAM.Netduino3.App.Helpers;

namespace JAM.Netduino3.App
{
    public class Program
    {
        private const string ApiServer = "iot.jamtech.cl:5000";
        public static void Main()
        {
            try
            {
                Debug.EnableGCMessages(true);

                //Load configuration
                var config = new Config();
                Debug.Print(config.Count + " parametros cargados");

                //Wifi
                Debug.Print("Esperando Wifi");
                Microsoft.SPOT.Net.NetworkInformation.NetworkInterface NI = Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0];
                NetHelper.WaitForWifi();
                
                //Update date and time
                var timeUpdated = Ntp.UpdateTimeFromNtpServer(config[ConfigConstants.NtpServer], int.Parse(config[ConfigConstants.TimeZone]));
                Debug.Print("Fecha y hora: " + DateTime.Now);

                //Update DDNS
                //Helpers.DDNS.ActualizarDNS(config[ConfigConstants.DdnsUpdateUrl]);
                //Debug.Print("DDNS actualizado");

                //Register on server
                var res = Helpers.Iot.Register(NI.IPAddress, NI.PhysicalAddress, ApiServer);
                

                //WebServer
                var web = new Web(int.Parse(config[ConfigConstants.WebServerPort]));
                Debug.Print("WebServer iniciado en http://" + web.Ip + ":" + web.Port);

                Debug.Print("Memoria disponible: " + Debug.GC(false).ToString());
                Debug.Print("Memoria disponible: " + Debug.GC(true).ToString());
            }
            catch(Exception ex)
            {
                Debug.Print("WebServer error: " + ex.Message + Enviroment.NewLine + "Stacktrace: " + ex.StackTrace);
            }

            Debug.Print("Going to sleep");
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
    