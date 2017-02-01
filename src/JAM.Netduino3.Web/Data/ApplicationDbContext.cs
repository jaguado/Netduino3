using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JAM.Netduino3.Web.Models;

namespace JAM.Netduino3.Web.Data
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
            : base(options)
        {
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected override void OnModelCreating(ModelBuilder builder)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
