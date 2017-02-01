using System.Collections.Generic;

namespace JAM.Netduino3.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Device
    {
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
        public int Port { set; get; }

        internal Helpers.GatewayHelper gateway { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public List<Messages> Queue { set; get; }
        
        internal bool Deleted { set; get; }
    }
}
