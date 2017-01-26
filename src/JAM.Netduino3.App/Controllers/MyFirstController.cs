using System;
using Microsoft.SPOT;
using Rinsen.WebServer;

namespace JAM.Netduino3.App
{
    public class MyFirstController : Controller
    {
        public void Index()
        {
            var res = new Result
            {
                Name = "TestName",
                Value = "TestValue"
            };

            SetJsonResult(res);
        }

        public class Result
        {
            public string Name { set; get; }
            public string Value { set; get; }
        }
    }
}
