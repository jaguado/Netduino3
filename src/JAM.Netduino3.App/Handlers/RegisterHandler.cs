using EmbeddedWebserver.Core;
using EmbeddedWebserver.Core.Handlers.Abstract;
using JAM.Netduino3.App.Helpers;
using Microsoft.SPOT.Hardware;

namespace JAM.Netduino3.App.Handlers
{
    public class RegisterHandler : HandlerBase
    {
        #region Non-public members

        private readonly GrowControl _growControl;
        protected override void ProcessRequestWorker(HttpContext pContext)
        {
            if (_growControl == null) return;

            _growControl.IotRegistration();
            pContext.Response.ResponseBody = Iot.GetJsonContent(_growControl._ni, _growControl._apiServer, _growControl.GetRelaysState());
            //TODO Add Response body!!
        }

        #endregion

        #region Constructors

        public RegisterHandler(ref GrowControl growControl) : base(HttpMethods.POST)
        {
            _growControl = growControl;
        }

        #endregion
    }
}
