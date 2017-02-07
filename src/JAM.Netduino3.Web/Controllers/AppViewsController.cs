using JAM.Netduino3.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

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
            {
                var task = LoadCloudDevices();
                task.Wait();
            }
            return View(DevicesController.Devices);
        }

        private static bool loadDevices = false;
        public async Task LoadCloudDevices()
        {
            var client = new RestClient("http://iot.growcontrol.cl/api/devices");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");

            loadDevices = true;
            var asyncHandler = client.ExecuteAsync<List<JAM.Netduino3.Web.Models.Device>>(request, r =>
            {
                DevicesController.Devices = JsonConvert.DeserializeObject<ConcurrentBag<JAM.Netduino3.Web.Models.Device>>(r.Content);
                loadDevices = false;
            });
            while (loadDevices)
            {

            }
        }

        public ActionResult Devices()
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                var task = LoadCloudDevices();
                task.Wait();
            }

            return PartialView("~/Views/AppViews/Devices.cshtml", DevicesController.Devices);
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