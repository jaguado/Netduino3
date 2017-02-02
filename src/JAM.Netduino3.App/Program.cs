using System.Threading;
using System;
using Microsoft.SPOT;
using System.Net;
using JAM.Netduino3.App.Helpers;
using Microsoft.SPOT.Net.NetworkInformation;

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
                var NI = NetworkInterface.GetAllNetworkInterfaces()[0];
                NetHelper.WaitForWifi();
                
                //Update date and time
                var timeUpdated = Ntp.UpdateTimeFromNtpServer(config[ConfigConstants.NtpServer], int.Parse(config[ConfigConstants.TimeZone]));
                Debug.Print("Fecha y hora: " + DateTime.Now);

                //Update DDNS
                //Helpers.DDNS.ActualizarDNS(config[ConfigConstants.DdnsUpdateUrl]);
                //Debug.Print("DDNS actualizado");
                                
                //WebServer
                var web = new Web(int.Parse(config[ConfigConstants.WebServerPort]));
                Debug.Print("WebServer iniciado en http://" + web.Ip + ":" + web.Port);

                //IoT Registration
                Register(NI);

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

        private static void Register(NetworkInterface NI)
        {
            try
            {
                var res = Iot.Register(NI.IPAddress, NI.PhysicalAddress, ApiServer);
                Debug.Print("Registered: " + res);
            }
            catch (Exception ex)
            {
                Debug.Print("Error registering: " + ex.ToString() + Enviroment.NewLine + ex.StackTrace);
            }
        }
    }
}
    