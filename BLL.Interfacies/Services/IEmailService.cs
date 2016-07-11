using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace BLL.Interface.Services
{
    public interface IEmailService
    {
        void SendToEmail(MailMessage message);
        void SendConfirmationToEmail(string receiverEmail,string body);
        void SendInviteToEmails(string[] receiverEmails,string managerName, string body);
    }
}
