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
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.

            // char is a single Unicode character
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        public static void SendMail(string receiveMail,string content, string tille, string smtpConnect,int port)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("vietcoldchainapp@gmail.com", "vietcoldchainapp@gmail.com"));
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
                smtp.Authenticate("son.tienson@gmail.com", "qxki hrip zuvk denz");
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