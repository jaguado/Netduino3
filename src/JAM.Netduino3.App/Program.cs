using System;
using Microsoft.SPOT;
using System.Threading;

namespace JAM.Netduino3.App
{
    public class Program
    {
        public static void Main()
        {
            Debug.Print(Resources.GetString(Resources.StringResources.String1));
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
