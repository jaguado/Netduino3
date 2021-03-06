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
        private static string ApiServer = "http://iot.growcontrol.cl";
        public static void Main()
        {
            try
            {
                #if DEBUG
                    //ApiServer = "http://iot.jamtech.cl:8080";
                #endif

                Debug.EnableGCMessages(true);

                //Load configuration
                var config = new Config();
                Debug.Print(config.Count + " parametros cargados");

                //Wifi
                Debug.Print("Esperando Wifi");
                var NI = NetworkInterface.GetAllNetworkInterfaces()[0];
                NetHelper.WaitForWifi();
                Debug.Print("Wifi Ok. Ip: " + NI.IPAddress);

                //Update date and time
                var timeUpdated = Ntp.UpdateTimeFromNtpServer(config[ConfigConstants.NtpServer], int.Parse(config[ConfigConstants.TimeZone]));
                Log.Print("Fecha y hora: " + DateTime.Now);

                //Update DDNS
                //Helpers.DDNS.ActualizarDNS(config[ConfigConstants.DdnsUpdateUrl]);
                //Debug.Print("DDNS actualizado");

                //Growcontrol init
                var growControl = new GrowControl(NI, ApiServer);
                growControl.RunOnSeparateThread();
                
                //WebServer
                var web = new Web(int.Parse(config[ConfigConstants.WebServerPort]), false);
                Log.Print("WebServer iniciado en http://" + web.Ip + ":" + web.Port);

                //Relays control handler
                web.RegisterHandler("relayChange", new Handlers.RelaysChangeHandler(ref growControl));
                web.RegisterHandler("relayRead", new Handlers.RelaysReadHandler(ref growControl));
                web.RegisterHandler("Register", new Handlers.RegisterHandler(ref growControl));
                web.Start();


                Log.Print("Memoria disponible: " + Debug.GC(false).ToString());
                Blink(false);
            }
            catch(Exception ex)
            {
                var httpEx = ex as EmbeddedWebserver.Core.HttpException;
                if (httpEx!=null)
                    Log.Print("Web server error: " + httpEx.ErrorCode.ToString());
                else
                    Log.Print("Init error: " + ex.Message + Enviroment.NewLine + "Stacktrace: " + ex.StackTrace);
                Blink(true);
            }

            Log.Print("Memoria disponible: " + Debug.GC(true).ToString());
            Log.Print("Going to sleep");
            Thread.Sleep(Timeout.Infinite);
        }


        private static OutputPort onboardLED;
        private static void Blink(bool error)
        {
            int waitTime = 300;
            if(onboardLED==null)
                onboardLED = new OutputPort(Pins.ONBOARD_LED, true);
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
            Log.Print("LED state: " + onboardLED.Read());
        }
    }
}
    