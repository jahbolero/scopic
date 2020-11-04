using System.Collections.Generic;
using System.Threading.Tasks;
using MimeKit;

namespace scopic_test_server.Services
{
    public interface IEmailService
    {
        void SendEmails(List<MimeMessage> Msgs);
        Task SendEmail(MimeMessage Msg);
        MimeMessage NewMail(string recepient, string subject, string message);
    }
}