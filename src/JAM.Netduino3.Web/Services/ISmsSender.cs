using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAM.Netduino3.Web.Services
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface ISmsSender
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Task SendSmsAsync(string number, string message);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
