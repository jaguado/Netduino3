using EmbeddedWebserver.Core.Configuration;
using EmbeddedWebserver.Core.Handlers.Interfaces;
using System;

namespace JAM.Netduino3.App.Helpers
{
    internal class WebServer:IDisposable
    {
        EmbeddedWebserver.Core.Webserver server;
        public WebServer()
        {
            EmbeddedWebapplicationConfiguration config = new EmbeddedWebapplicationConfiguration(@"SD\webroot");
            config.ReadConfiguration();
            server = new EmbeddedWebserver.Core.Webserver(config); 
        }
        public void RegisterHandler(string pAccessUri, Type pHandlerType)
        {
            server.RegisterHandler(pAccessUri, pHandlerType);
        }
        public void RegisterThreadSafeHandler(string pAccessUri, IHandler pHandlerInstance)
        {
            server.RegisterThreadSafeHandler(pAccessUri, pHandlerInstance);
        }
        public string UrlDecode(string pSourceUrl)
        {
            return server.UrlDecode(pSourceUrl);
        }
        public string UrlEncode(string pSourceUrl)
        {
            return server.UrlEncode(pSourceUrl);
        }
        public void StartListening()
        {
            server.StartListening();
        }
        public void StopListening()
        {
            server.StopListening();
        }


        void IDisposable.Dispose()
        {
            if(server!=null){
                if (server.IsListening)
                {
                    server.StopListening();
                }
                server.Dispose();
            }
        }
    }
}
