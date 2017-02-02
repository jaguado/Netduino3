using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System;
using System.Linq;
using System.Reflection;

namespace JAM.Netduino3.Web.Controllers
{
    /// <summary>
    /// Here you can register your device and list all the registered devices
    /// </summary>
    [Route("api/[controller]")]
    public class DevicesController : Controller
    {
        private static ConcurrentBag<Models.Device> Devices = new ConcurrentBag<Models.Device>();
        // GET: api/devices
        /// <summary>
        /// List all registered devices
        /// </summary>
        /// <returns>Registered devices</returns>
        [HttpGet]
        public IEnumerable<Models.Device> ListRegistered()
        {
            return Devices.Where(d=>!d.Deleted);
        }

        /// <summary>
        /// Get message quehue for specified device
        /// </summary>
        /// <param name="mac">Device MAC address</param>
        /// <param name="activeOnly">Show only active messages?</param>
        /// <returns>List of messages</returns>
        [Route("Quehue")]
        [HttpGet]
        public IEnumerable<Models.Messages> GetQuehue([FromQuery] string mac, [FromQuery] bool activeOnly)
        {
            var tmpDevice = Devices.FirstOrDefault(d => d.MAC == mac);
            if (tmpDevice != null && tmpDevice.MAC != string.Empty)
                return activeOnly ? tmpDevice.Queue.Where(m => m.Active) : tmpDevice.Queue;
            return null;
        }

        // POST: api/devices
        /// <summary>
        /// Register a device
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Models.Device RegisterDevice([FromBody] Models.Device device)
        {
            var tempDevice = device;
            tempDevice.RegistrationDate = DateTime.Now;

            //Asign IP
            if (string.IsNullOrEmpty(tempDevice.IP))
                tempDevice.IP = HttpContext.Connection.RemoteIpAddress.ToString();

            //Asign port
            var rnd = new Random();
            tempDevice.Port = rnd.Next(10000, 50000);
            while(Devices.Any(d=>d.Port == tempDevice.Port))
            {
                tempDevice.Port = rnd.Next(10000, 50000);
            }

            //Create Thread listening on the port
            //tempDevice.gateway = new Helpers.GatewayHelper(tempDevice.Port);

            var existingDevice = Devices.SingleOrDefault(s => s.MAC == device.MAC);
            if(existingDevice != null && existingDevice.MAC != "")
            {
                existingDevice.IP = device.IP;
                existingDevice.RegistrationDate = DateTime.Now;
                existingDevice.Deleted = false;
            }
            else
                Devices.Add(tempDevice);

            Ok();
            return tempDevice;
        }

        // DELETE: api/devices
        /// <summary>
        /// Unregister a device
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public void UnRegisterDevice([FromBody] Models.Device device)
        {
            Devices.Single(d => d.MAC == device.MAC).Deleted = true;
        }

        /// <summary>
        /// Modify device and message quehue for specified device
        /// </summary>
        /// <param name="device">Device</param>
        [HttpPut]
        public void ModifyDeviceQuehue([FromBody] Models.Device device)
        {
            var tmpDevice = Devices.FirstOrDefault(d => d == device);
            if (tmpDevice != null && tmpDevice.MAC == device.MAC && device.Queue != null && device.Queue.Any(q => q.New))
            {
                //Add new ones
                tmpDevice.Queue.AddRange(device.Queue.Where(q => q.New));

                //Update old ones
                tmpDevice.IP = device.IP;
                foreach(var msg in device.Queue.Where(m=>!m.New))
                {
                    var m = tmpDevice.Queue.Single(q => q.UniqueId == msg.UniqueId);
                    m.Active = msg.Active;
                    m.Content = msg.Content;
                    //TODO Implement message properties rewrite
                }
            }
            Ok();
        }
    }
}       