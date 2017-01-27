using System.Net;

namespace JAM.Netduino3.App
{
    public static class NetHelper
    {
        public static void WaitForWifi()
        {
            while (IPAddress.GetDefaultLocalAddress() == IPAddress.Any) ; // wait for DHCP-allocated IP address
        }
    }
}
