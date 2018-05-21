using EventManagementCoreApi2.Services.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementCoreApi2.Services.Service
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string from, string to, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                SmtpClient client = new SmtpClient
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential
                    {
                        UserName = "developer.lorenz@gmail.com",
                        Password = "coder53192"
                    },
                    EnableSsl = true,
                    Timeout = 20000
                };

                client.Send(mail);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
