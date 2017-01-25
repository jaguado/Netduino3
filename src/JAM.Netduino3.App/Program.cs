using System.Threading;
using Rinsen.WebServer;

namespace JAM.Netduino3.App
{
    public class Program
    {
        public static void Main()
        {
            var webServer = new WebServerHelper();
            webServer.StartServer(80);
            webServer.RouteTable.DefaultControllerName = "MyFirst";
            webServer.RouteTable.DefaultMethodName = "Index";
            Thread.Sleep(Timeout.Infinite);
        }
    }

    public class MyFirstController : Controller
    {
        public void Index()
        {
            var res = new Result
            {
                Name="TestName", Value="TestValue"
            };

            SetJsonResult(res);
        }
    }

    public class Result
    {
        public string Name { set; get; }
        public string Value { set; get; }
    }
}
    