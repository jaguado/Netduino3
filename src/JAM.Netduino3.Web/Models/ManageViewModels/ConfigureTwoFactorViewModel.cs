using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JAM.Netduino3.Web.Models.ManageViewModels
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ConfigureTwoFactorViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string SelectedProvider { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public ICollection<SelectListItem> Providers { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
