using Microsoft.SPOT;
using System.IO;
using System.Net;
using Toolbox.NETMF.NET;

namespace JAM.Netduino3.App.Helpers
{
    public class DDNS
    {
        public static string ActualizarDNS(string User, string Pwd, string Host, string IP)
        {
            //GET /nic/update?hostname=mytest.testdomain.com&myip=1.2.3.4 HTTP/1.0 
            //Host: dynupdate.no-ip.com 
            //Authorization: Basic base64-encoded-auth-string 
            //User-Agent: Bobs Update Client WindowsXP/1.2 bob@somedomain.com

            string strHost = string.Concat("dynupdate.no-ip.com");
            string strReq = string.Concat("/nic/update?hostname=", Host, "&myip=", IP);
            HTTP_Client http = new HTTP_Client(new IntegratedSocket(strHost, 80));
            http.UserAgent = "Netduino browser 1.0 - JAMSoluciones.cl";
            http.Authenticate(User, Pwd);

            // Requests the latest source
            HTTP_Client.HTTP_Response Response = http.Get(strReq);
            
            // Did we get the expected response? (a "200 OK")
            if (Response.ResponseCode != 200)
                return Response.ResponseBody;

            return string.Empty;
        }

        public static void ActualizarDNS(string url)
        {
            using (var wr = HttpWebRequest.Create(url))
            {
                using(var res = wr.GetResponse())
                {
                    var sr=new StreamReader(res.GetResponseStream());
                    Debug.Print("DDNS update status: " + sr.ReadToEnd().Trim());
                }
            }
        }
    }
}
