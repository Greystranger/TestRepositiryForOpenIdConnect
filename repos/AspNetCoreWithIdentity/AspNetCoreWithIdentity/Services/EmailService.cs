using System;
using System.Collections.Generic;
using System.Linq;
using MailKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using MailKit.Security;
using MimeKit;

namespace AspNetCoreWithIdentity.Services
{
    public class EmailService
    {
        public static async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage
            {
                From = {new MailboxAddress("Service administration", "yatsenkolawyer@ukr.net")},
                To = {new MailboxAddress("", email)},
                Subject = subject,
                Body = new TextPart(MimeKit.Text.TextFormat.Html) {Text = message}
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.ukr.net", 2525, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync("yatsenkolawyer@ukr.net", "SLAYDER777");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
