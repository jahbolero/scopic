using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using scopic_test_server.Helper;

namespace scopic_test_server.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;
        public EmailService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public MimeMessage NewMail(string recepient, string subject, string message)
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse($"{_appSettings.MailSender}"));
            mail.To.Add(MailboxAddress.Parse($"{recepient}"));
            mail.Subject = subject;
            mail.Body = new TextPart(TextFormat.Html)
            {
                Text = message
            };
            return mail;
        }

        public async Task SendEmails(List<MimeMessage> Msgs)
        {
            using var smtp = new SmtpClient();
            smtp.Connect(_appSettings.SmtpServer, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_appSettings.MailSender, _appSettings.MailPassword);
            foreach (var msg in Msgs)
            {
                await smtp.SendAsync(msg);
            }
            smtp.Disconnect(true);
        }
        public async void SendEmail(MimeMessage Msg)
        {
            try
            {
                using var smtp = new SmtpClient();
                smtp.Connect(_appSettings.SmtpServer, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_appSettings.MailSender, _appSettings.MailPassword);
                await smtp.SendAsync(Msg);
                smtp.Disconnect(true);
            }
            catch (Exception e)
            {

            }

        }
    }


}