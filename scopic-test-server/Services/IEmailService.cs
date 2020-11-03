using System.Collections.Generic;
using MimeKit;

namespace scopic_test_server.Services
{
    public interface IEmailService
    {
        void SendEmails(List<MimeMessage> Msgs);
        MimeMessage NewMail(string recepient, string subject, string message);
    }
}