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
        private readonly SmtpClient _emailClient;
        public EmailService(AppSettings appSettings)
        {
            _emailClient = new SmtpClient();
            using var smtp = new SmtpClient();
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
            _emailClient.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            _emailClient.Authenticate(_appSettings.MailSender, _appSettings.MailPassword);
            foreach (var msg in Msgs)
            {
                await _emailClient.SendAsync(msg);
            }
            _emailClient.Disconnect(true);
        }
        public async void SendEmail(MimeMessage Msg)
        {
            try
            {
                _emailClient.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
                _emailClient.Authenticate(_appSettings.MailSender, _appSettings.MailPassword);
                await _emailClient.SendAsync(Msg);
                _emailClient.Disconnect(true);
            }
            catch (Exception e)
            {

            }

        }
    }


}