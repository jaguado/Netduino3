using System.Threading;
using System;
using Microsoft.SPOT;

namespace JAM.Netduino3.App
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                Debug.EnableGCMessages(true);

                //Load configuration
                var config = new Helpers.Config();
                Debug.Print(config.Count + " parametros cargados");

                //Wifi
                Debug.Print("Esperando Wifi");
                NetHelper.WaitForWifi();
                
                //Update date and time
                var timeUpdated = Helpers.Ntp.UpdateTimeFromNtpServer(config[ConfigConstants.NtpServer], int.Parse(config[ConfigConstants.TimeZone]));
                Debug.Print("Fecha y hora: " + DateTime.Now);

                //Update DDNS
                Helpers.DDNS.ActualizarDNS(config[ConfigConstants.DdnsUpdateUrl]);
                Debug.Print("DDNS actualizado");

                //WebServer
                var web = new WebServerHelper(int.Parse(config[ConfigConstants.WebServerPort]));
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
    