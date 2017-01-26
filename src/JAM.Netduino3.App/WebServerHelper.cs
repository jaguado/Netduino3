using System;
using Microsoft.SPOT;
using MicroWebServer.Mvc;
using System.Reflection;
using MicroWebServer;
using System.Net;
using Microsoft.SPOT.Net.NetworkInformation;

namespace JAM.Netduino3.App
{
    public class WebServerHelper
    {
        public int Port { set; get; }
        public string Ip { set; get; }
        public WebServerHelper(int port){
            Port = port;
            var server = new MvcHttpServer("http", port);
            var routes = server.Routes;

            routes.MapApiRoute("api", Assembly.GetAssembly(typeof(Program)));
           
            routes.MapResourceRoute("/test.html", new Resource(Resources.ResourceManager, Resources.StringResources.TestHtml) { ContentType = "text/html" });
            routes.MapResourceRoute("/scripts/jquery.js", new Resource(Resources.ResourceManager, Resources.StringResources.ScriptsJqueryJs) { ContentType = "text/javascript" });
            routes.MapResourceRoute("/scripts/knockout.js", new Resource(Resources.ResourceManager, Resources.StringResources.ScriptsKnockoutJs) { ContentType = "text/javascript" });


            //Get Ip
            var nis = NetworkInterface.GetAllNetworkInterfaces();
            if (nis != null && nis.Length>0)
            {
                Ip = nis[0].IPAddress;
            }
        }
    }
}
