using System;
using System.Net.Sockets;
using EmbeddedWebserver.Core;
using EmbeddedWebserver.Core.Handlers.Abstract;
using Toolbox.NETMF.NET;

namespace JAM.Netduino.App.Handlers
{
    class TestHandler: HandlerBase
    {
        #region Non-public members

        protected override void ProcessRequestWorker(HttpContext pContext)
        {
            var data= pContext.Request.QueryString;

            var host = "www.google.cl";
            var port = 80;
            if (data != null && data["host"] != null)
                host = data["host"];
            if (data != null && data["port"] != null)
                port = int.Parse(data["port"]);
            try
            {
                var socket = new IntegratedSocket(host, (ushort) port);
                var timespan = DateTime.Now;
                socket.Connect();
                if (socket.IsConnected)
                    socket.Close();

                pContext.Response.ResponseBody = DateTime.Now.Subtract(timespan).Milliseconds.ToString();
            }
            catch (Exception ex)
            {
                pContext.Response.StatusCode = HttpStatusCodes.InternalServerError;
            }                    
        }

        #endregion

        #region Constructors

        public TestHandler() : base(HttpMethods.GET) { }

        #endregion
    }
}
