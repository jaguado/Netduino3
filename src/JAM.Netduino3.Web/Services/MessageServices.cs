using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAM.Netduino3.Web.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AuthMessageSender : IEmailSender, ISmsSender
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public Task SendEmailAsync(string email, string subject, string message)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public Task SendSmsAsync(string number, string message)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
