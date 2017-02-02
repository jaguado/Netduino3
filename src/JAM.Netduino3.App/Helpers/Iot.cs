using System;
using Microsoft.SPOT;

namespace JAM.Netduino3.App.Helpers
{
    public static class Iot
    {
        public static string Register(string ip, byte[] mac, string ApiServer)
        {
            return NetHelper.HttpPost(ApiServer + "/api/Devices", "{\"mac\": \"" + mac.ToFormattedString() + "\",  \"ip\": \"" + ip + "\"}");
        }

        public static string GetQuehue(string mac, string ApiServer)
        {
            throw new NotImplementedException();
        }
    }
}
