using System.Threading;
using System;
using Microsoft.SPOT;
using MicroWebServer.Mvc;
using System.Reflection;
using MicroWebServer;
using System.IO;

namespace JAM.Netduino3.App
{
    public class Program
    {
        public static void Main()
        {
            
            try
            {
                Debug.EnableGCMessages(true);

                NetHelper.WaitForWifi();

                //Update date and time
                var timeUpdated = Helpers.Ntp.UpdateTimeFromNtpServer(ConfigHelper.NtpServer, ConfigHelper.TimeZone);
                Debug.Print("Fecha y hora: " + DateTime.Now);

                //Update DDNS
                Helpers.DDNS.ActualizarDNS(ConfigHelper.DdnsUpdateUrl);
                Debug.Print("DDNS actualizado");


                //Rinsen.WebServer
                //var webServer = new WebServerHelper();
                //webServer.StartServer(80);
                //webServer.RouteTable.DefaultControllerName = "MyFirst";
                //webServer.RouteTable.DefaultMethodName = "Index

                //MicroWebServer
                var server = new MvcHttpServer("http", 80);
                var routes = server.Routes;

                routes.MapApiRoute("api", Assembly.GetAssembly(typeof(Program)));
                routes.MapResourceRoute("/test.html", new Resource(Resources.ResourceManager, Resources.StringResources.TestHtml) { ContentType = "text/html" });
                routes.MapResourceRoute("/scripts/jquery.js", new Resource(Resources.ResourceManager, Resources.StringResources.ScriptsJqueryJs) { ContentType = "text/javascript" });
                routes.MapResourceRoute("/scripts/knockout.js", new Resource(Resources.ResourceManager, Resources.StringResources.ScriptsKnockoutJs) { ContentType = "text/javascript" });

                Debug.Print("Memory: " + Microsoft.SPOT.Debug.GC(false).ToString());
            }
            catch(Exception ex)
            {
                Debug.Print("WebServer error: " + ex.Message + Enviroment.NewLine + "Stacktrace: " + ex.StackTrace);
            }

            Debug.Print("Going to sleep");
            Thread.Sleep(Timeout.Infinite);
        }

        /// <summary>
        /// Returns a list of folders. 
        /// This function expects the SD card to be mounted.
        /// </summary>
        /// <param name="currentFolder">Current folder to enumerate from</param>
        /// <returns>List of folders</returns>
        
    }
}
    