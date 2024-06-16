using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Text;

namespace BaseCommon.Extension
{
    public static class SendEmailExtension
    {
        private static readonly Random _random = new Random();
        public static string RandomString(int size, bool lowerCase = false)
        {
            var passwordBuilder = new StringBuilder();
            passwordBuilder.Append(RandomNumber(100000, 999999));

            return passwordBuilder.ToString().Trim();
        }
        public static int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        public static void SendMail(string receiveMail,string content, string tille, string smtpConnect,int port)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("VietColdChain", "vietcoldchainapp@gmail.com"));
            email.To.Add(new MailboxAddress(receiveMail, receiveMail));

            email.Subject = tille;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = content
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(smtpConnect, port, false);

                // Note: only needed if the SMTP server requires authentication
                smtp.Authenticate("vietcoldchainapp@gmail.com", "rqco kbbo dais fpvq");
                try
                {
                    smtp.Send(email);
                }
                catch (Exception ex)
                {
                    throw;
                }

                smtp.Disconnect(true);
            }
        }
    }
}