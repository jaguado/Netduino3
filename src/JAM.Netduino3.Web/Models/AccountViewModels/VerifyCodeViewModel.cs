using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JAM.Netduino3.Web.Models.AccountViewModels
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class VerifyCodeViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        [Required]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Provider { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Required]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Code { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string ReturnUrl { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Display(Name = "Remember this browser?")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public bool RememberBrowser { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Display(Name = "Remember me?")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public bool RememberMe { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
