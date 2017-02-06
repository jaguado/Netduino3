using EmbeddedWebserver.Core;
using EmbeddedWebserver.Core.Handlers.Abstract;
using JAM.Netduino3.App.Helpers;
using Microsoft.SPOT.Hardware;

namespace JAM.Netduino3.App.Handlers
{
    public class RelaysReadHandler : HandlerBase
    {
        #region Non-public members

        private readonly GrowControl _growControl;
        protected override void ProcessRequestWorker(HttpContext pContext)
        {
            if (_growControl == null) return;

            var dict = pContext.Request.QueryString;
            if (dict.ContainsKey("relay"))
            {
                var index = int.Parse(dict["relay"].Trim());
                var state = _growControl.GetRelayState(index);
                pContext.Response.ResponseBody = (!state).ToString();
            }

            //TODO Add Response body!!
        }

        #endregion

        #region Constructors

        public RelaysReadHandler(ref GrowControl growControl) : base(HttpMethods.GET)
        {
            _growControl = growControl;
        }

        #endregion
    }
}
