using EmbeddedWebserver.Core;
using EmbeddedWebserver.Core.Handlers.Abstract;
using JAM.Netduino3.App.Helpers;
using Microsoft.SPOT.Hardware;

namespace JAM.Netduino3.App.Handlers
{
    public class RelaysChangeHandler : HandlerBase
    {
        #region Non-public members

        private readonly GrowControl _growControl;
        protected override void ProcessRequestWorker(HttpContext pContext)
        {
            if (_growControl == null) return;

            var dict = pContext.Request.GetPostRequestParameters();
            if (dict.ContainsKey("relay"))
            {
                var index = int.Parse(dict["relay"].Trim());
                if (dict.ContainsKey("value"))
                {
                    _growControl.ChangeRelayState(index, dict["value"].Trim().ToLower().Equals("true"));
                }
            }

            //TODO Add Response body!!
        }

        #endregion

        #region Constructors

        public RelaysChangeHandler(ref GrowControl growControl) : base(HttpMethods.POST)
        {
            _growControl = growControl;
        }

        #endregion
    }
}
