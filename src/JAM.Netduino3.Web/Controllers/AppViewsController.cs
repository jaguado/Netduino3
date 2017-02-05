using JAM.Netduino3.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Inspinia_MVC5.Controllers
{
    public class AppViewsController : Controller
    {

        public ActionResult Contacts()
        {
            return View();
        }
     
        public ActionResult Profile()
        {
            return View();
        }
        
        public ActionResult Management()
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                DummyData();

            ViewBag.Devices = DevicesController.Devices;
            return View();
        }


        private static void DummyData()
        {
            //Dummy Devices
            if (DevicesController.Devices.Count == 0)
            {
                DevicesController.Devices.Add(new JAM.Netduino3.Web.Models.Device
                {
                    IP = "192.168.0.13",
                    MAC = "00:00:00:00:00",
                    RelaysState=new bool[9]
                });
            }

        }

        public ActionResult ProjectDetail()
        {
            return View();
        }
        
        public ActionResult FileManager()
        {
            return View();
        }
        
        public ActionResult Calendar()
        {
            return View();
        }
        
        public ActionResult FAQ()
        {
            return View();
        }
        
        public ActionResult Timeline()
        {
            return View();
        }
        
        public ActionResult PinBoard()
        {
            return View();
        }

        public ActionResult TeamsBoard()
        {
            return View();
        }

        public ActionResult Clients()
        {
            return View();
        }

        public ActionResult OutlookView()
        {
            return View();
        }

        public ActionResult IssueTracker()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult Article()
        {
            return View();
        }

	}
}