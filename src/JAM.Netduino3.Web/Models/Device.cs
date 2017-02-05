using System;
using System.Collections.Generic;
using System.Linq;

namespace JAM.Netduino3.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Device
    {
        public Device()
        {
            LastUpdate = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        public string MAC { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string IP { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string PublicIP { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int Port { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime LastUpdate { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int RelaysCount
        {
            get
            {
                return RelaysState.Length;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int RelaysInUse
        {
            get
            {
                return RelaysState.Count(c => c);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool[] RelaysState { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int PoweredOnPercentage
        {
            get
            {
                if (RelaysCount == 0) return 0;
                return (100 / RelaysCount) * RelaysInUse;
            }
        }


        internal Helpers.GatewayHelper gateway { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public List<Messages> Queue { set; get; }
        
        internal bool Deleted { set; get; }

        internal DateTime RegistrationDate { set; get; }
    }
}
