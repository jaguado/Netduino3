using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JAM.Netduino3.Web.Models.AccountViewModels
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class LoginViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        [Required]
        [EmailAddress]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Email { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Required]
        [DataType(DataType.Password)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Password { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Display(Name = "Remember me?")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public bool RememberMe { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
