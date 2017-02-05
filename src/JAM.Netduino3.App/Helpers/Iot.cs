using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Net.NetworkInformation;

namespace JAM.Netduino3.App.Helpers
{
    public static class Iot
    {
        public static void Register(NetworkInterface NI,string ApiServer, bool[] RelaysState)
        {
            try
            {
                var content = string.Empty;
                if (RelaysState != null)
                {
                    var stateJson = "\"relaysState\": [";
                    for(int i = 1; i < RelaysState.Length; i++)
                    {
                        //TODO create relays status json
                        if (i == RelaysState.Length - 1)
                        {
                            //last one 
                            stateJson += (RelaysState[i] ? 1:0) + "]";
                        }
                        else
                            stateJson += (RelaysState[i] ? 1 : 0) + ",";
                    }
                    content = "{\"mac\": \"" + NI.PhysicalAddress.ToFormattedString() + "\",  \"ip\": \"" + NI.IPAddress + "\"," + stateJson;
                }
                else
                {
                    content = "{\"mac\": \"" + NI.PhysicalAddress.ToFormattedString() + "\",  \"ip\": \"" + NI.IPAddress + "\"";
                }

                //content += ",\"campo1\":" + "\"valor1\"";
                //content += ",\"campo1\":" + "\"valor1\"";
                //content += ",\"campo1\":" + "\"valor1\"";

                //Fin
                content = "}";
                NetHelper.HttpPost(ApiServer + "/api/Devices",content, "application/json", false);
                Debug.Print("Registered");
            }
            catch (Exception ex)
            {
                Debug.Print("Error registering: " + ex.ToString() + Enviroment.NewLine + ex.StackTrace);
            }
        }

        public static string GetQuehue(string mac, string ApiServer)
        {
            throw new NotImplementedException();
        }
    }
}
