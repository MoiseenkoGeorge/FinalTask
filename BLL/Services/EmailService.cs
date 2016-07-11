using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using BLL.Interface.Services;
namespace BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient smtpClient = new SmtpClient("smtp.mail.ru", 25) {
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new System.Net.NetworkCredential("FinalTask@mail.ru", "5501mk123"),
            EnableSsl = true,
            Timeout = 5000
        };
        public void SendToEmail(MailMessage message)
        {
            smtpClient.Send(message);
        }

        public void SendConfirmationToEmail(string receiverEmail,string body)
        {
            MailAddress receiver = new MailAddress(receiverEmail);
            MailAddress sender = new MailAddress("FinalTask@mail.ru", "FinalTask");
            MailMessage message = new MailMessage(sender, receiver)
            {
                Subject = "Email confirmation",
                Body = body,
                IsBodyHtml = true
            };
            SendToEmail(message);
        }

        public void SendInviteToEmails(string[] receiverEmails,string managerName, string body)
        {
            MailAddress sender = new MailAddress(managerName, "FinalTask");
            foreach(var email in receiverEmails)
            {
                MailMessage message = new MailMessage(sender, new MailAddress(email))
                {
                    Subject = "Invitation to Job",
                    Body = string.Format("Dear,{0},manager " + managerName + "invites you to work", email),
                    IsBodyHtml = false
                };
                SendToEmail(message);
            }
            
        }
    }
}
