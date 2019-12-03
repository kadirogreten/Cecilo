using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Cecilo.Models.Mail
{
    public class Mail
    {
        public static void MailSender(string body)
        {
            //var fromAddress = new MailAddress("iletisim@isbiroptik.net");
           // var toAddress = new MailAddress("iletisim@isbiroptik.net");
            var fromAddress = new MailAddress("kadir@weezstudio.com");
            var toAddress = new MailAddress("kadir@weezstudio.com");
            const string subject = "İşbir Optik | Sitenizden Gelen Mesaj";
            using (var smtp = new SmtpClient
            {
               // Host = "mail.isbiroptik.net",
                Host = "mail.weezstudio.com",
                Port = 587,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                //Credentials = new NetworkCredential(fromAddress.Address, "3JXV1DZAh9ic")
                Credentials = new NetworkCredential(fromAddress.Address, "89892dbC")
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}