using System.Collections;
using System.IO;
using System.Net;
using System.Text;

namespace JAM.Netduino3.App.Helpers
{
    public static class NetHelper
    {
        public static void WaitForWifi()
        {
            while (IPAddress.GetDefaultLocalAddress() == IPAddress.Any) ; // wait for DHCP-allocated IP address
        }

        public static string HttpGet(string url)
        {
            using (var wr = HttpWebRequest.Create(url))
            {
                using (var res = wr.GetResponse())
                {
                    var sr = new StreamReader(res.GetResponseStream());
                    return sr.ReadToEnd().Trim();
                }
            }
        }

        public static string HttpPost(string url, ArrayList pars=null, string contentType = "application/x-www-form-urlencoded")
        {
            using (var wr = HttpWebRequest.Create(url))
            {
                wr.Method = "POST";
                wr.ContentType = contentType;
               
                if (pars != null)
                {
                    var postData = "";
                    foreach (DictionaryEntry par in pars) {
                        postData += par.Key + "=" + par.Value + "&";
                    }
                    var data = Encoding.UTF8.GetBytes(postData);
                    wr.ContentLength = data.Length;
                    using (var stream = wr.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                

                var response = (HttpWebResponse)wr.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                using (var res = wr.GetResponse())
                {
                    var sr = new StreamReader(res.GetResponseStream());
                    return sr.ReadToEnd().Trim();
                }
            }
        }


        public static string HttpPost(string url, string content, string contentType = "application/json")
        {
            using (var wr = HttpWebRequest.Create(url))
            {
                wr.Method = "POST";
                wr.ContentType = contentType;
                //wr.ContentType = "application/x-www-form-urlencoded";

                var data = Encoding.UTF8.GetBytes(content);
                wr.ContentLength = content.Length;
                using (var stream = wr.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)wr.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                using (var res = wr.GetResponse())
                {
                    var sr = new StreamReader(res.GetResponseStream());
                    return sr.ReadToEnd().Trim();
                }
            }
        }



        public static string HttpPut(string url, ArrayList pars)
        {
            using (var wr = HttpWebRequest.Create(url))
            {
                wr.Method = "PUT";
                wr.ContentType = "application/x-www-form-urlencoded";

                if (pars != null)
                {
                    var postData = "";
                    foreach (DictionaryEntry par in pars)
                    {
                        postData += par.Key + "=" + par.Value + "&";
                    }
                    var data = Encoding.UTF8.GetBytes(postData);
                    wr.ContentLength = data.Length;
                    using (var stream = wr.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }


                var response = (HttpWebResponse)wr.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                using (var res = wr.GetResponse())
                {
                    var sr = new StreamReader(res.GetResponseStream());
                    return sr.ReadToEnd().Trim();
                }
            }
        }

        public static string ToFormattedString(this byte[] address)
        {
            var mac = "";
            for (int i = 0; i < address.Length; i++)
            {
                // Display the physical address in hexadecimal.
                mac += address[i].ToString("X2");               
            }
            return mac;
        }
    }
}
