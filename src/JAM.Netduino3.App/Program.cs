using System.Threading;
using System;
using Microsoft.SPOT;
using System.Net;
using JAM.Netduino3.App.Helpers;
using Microsoft.SPOT.Net.NetworkInformation;
using SecretLabs.NETMF.Hardware.Netduino;
using Microsoft.SPOT.Hardware;

namespace JAM.Netduino3.App
{
    public class Program
    {
        private static string ApiServer = "https://growcontrol.herokuapp.com";
        public static void Main()
        {
            try
            {
                #if DEBUG
                    //ApiServer = "http://iot.jamtech.cl:5000";
                #endif

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


                //Growcontrol init
                var growControl = new GrowControl();
                growControl.RunOnSeparateThread();
                web.RegisterHandler("relay", new Handlers.RelaysHandler(ref growControl));


                Debug.Print("Memoria disponible: " + Debug.GC(false).ToString());
                Debug.Print("Memoria disponible: " + Debug.GC(true).ToString());
                Blink(false);
            }
            catch(Exception ex)
            {
                Debug.Print("Init error: " + ex.Message + Enviroment.NewLine + "Stacktrace: " + ex.StackTrace);
                Blink(true);
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
        private static void Blink(bool error)
        {
            int waitTime = 300;
            var onboardLED = new OutputPort(Pins.ONBOARD_LED, true);
            Thread.Sleep(waitTime);
            if (!error)
            {
                for (int i = 0; i < 3; i++)
                {
                    onboardLED.Write(false);
                    Thread.Sleep(waitTime);
                    onboardLED.Write(true);
                    Thread.Sleep(waitTime);
                }
                onboardLED.Write(false);
            }
            else
            {
                for(int i = 0; i < 10; i++)
                {
                    onboardLED.Write(false);
                    Thread.Sleep(waitTime);
                    onboardLED.Write(true);
                    Thread.Sleep(waitTime);
                }
            }
        }
    }
}
    