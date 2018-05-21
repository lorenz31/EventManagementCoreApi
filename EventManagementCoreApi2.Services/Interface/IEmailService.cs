using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementCoreApi2.Services.Interface
{
    public interface IEmailService
    {
        void SendEmail(string from, string to, string subject, string body);
    }
}
