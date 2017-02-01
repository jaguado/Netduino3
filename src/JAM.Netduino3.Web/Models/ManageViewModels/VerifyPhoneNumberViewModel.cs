using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JAM.Netduino3.Web.Models.ManageViewModels
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class VerifyPhoneNumberViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        [Required]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Code { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Required]
        [Phone]
        [Display(Name = "Phone number")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string PhoneNumber { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
