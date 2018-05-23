using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace EventManagementCoreApi2.UnitTest
{
    [TestClass]
    public class EmailServiceUnitTest
    {
        [TestMethod]
        public void SendEmail()
        {
            string recipient = "lorenz53192@";
            string sender = "developer.lorenz@gmail.com";
            string subject = "Event Management Confirmation";
            string body = "You have successfully registered on our site. Please click the link to activate your account.";

            try
            {
                MailMessage mail = new MailMessage(sender, recipient)
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

                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
