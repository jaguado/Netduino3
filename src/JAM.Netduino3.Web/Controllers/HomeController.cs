using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JAM.Netduino3.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {

            ViewData["SubTitle"] = "Welcome to Grow Control ";
            ViewData["Message"] = "By JAM Tech";

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Manage()
        {

            ViewData["SubTitle"] = "Available Devices";
            ViewData["Message"] = "List of all the devices registered and with activity since 10 minutes at this time";


            return View();
        }

    }
}