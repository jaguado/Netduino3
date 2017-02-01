using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;

namespace JAM.Netduino3.Web.Models.ManageViewModels
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ManageLoginsViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IList<UserLoginInfo> CurrentLogins { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IList<AuthenticationDescription> OtherLogins { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
