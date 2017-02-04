using EmbeddedWebserver.Core;
using EmbeddedWebserver.Core.Handlers.Abstract;
using System.IO;
using EmbeddedWebserver.Core.Helpers;
using System.Collections;
using JAM.Netduino.dto;

namespace EmbeddedWebserver.Test
{
    public class LogHandler: HandlerBase
    {
        #region Non-public members

        protected override void ProcessRequestWorker(HttpContext pContext)
        {
            StringBuilder responseBuilder = new StringBuilder(HandlerBase.HtmlDoctype, 1700);
            responseBuilder.Append("<html><head><title>Netduino Log</title></head><body><table>");
            //Encabezado
            _serializeToRow(responseBuilder, new Log()
            {
                Origen = "Origen",
                Descripcion = "Descripción",
                Contenido = "Contenido",
                Fecha = "Fecha"
            });
            using(JAM.Netduino.DB.Raw.Datos oDatos = new JAM.Netduino.DB.Raw.Datos("test")){
                foreach(Log oLog in oDatos.Listar(typeof(Log))){
                    _serializeToRow(responseBuilder,oLog);
                }
            }
            responseBuilder.Append("</table></body></html>");
   
            pContext.Response.ResponseBody = responseBuilder.ToString();
        }

        private static void _serializeToRow(StringBuilder pResponseBuilder, Log oLog)
        {
            pResponseBuilder.Append("<tr>");
            pResponseBuilder.Append("<td>");
            pResponseBuilder.Append(oLog.Origen);
            pResponseBuilder.Append("</td><td>");
            pResponseBuilder.Append(oLog.Descripcion);
            pResponseBuilder.Append("</td>");
            pResponseBuilder.Append("<td>");
            pResponseBuilder.Append(oLog.Contenido);
            pResponseBuilder.Append("</td><td>");
            pResponseBuilder.Append(oLog.Fecha);
            pResponseBuilder.Append("</td>");
            pResponseBuilder.Append("</tr>");
        }


        #endregion

        #region Constructors

        public LogHandler() : base(HttpMethods.GET) { }

        #endregion
    }
}
