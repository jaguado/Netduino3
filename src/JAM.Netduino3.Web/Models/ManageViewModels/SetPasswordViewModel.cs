using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JAM.Netduino3.Web.Models.ManageViewModels
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class SetPasswordViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string NewPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string ConfirmPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
