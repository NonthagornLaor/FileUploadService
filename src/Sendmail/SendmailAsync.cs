using MailKit.Net.Smtp;
using MimeKit;
using Sendmail.Interface;
using Sendmail.Models;

namespace Sendmail
{
    public class SendmailAsync : ISendmailAsync
    {
        private readonly EmailConfig _emailConfig;

        public SendmailAsync(EmailConfig emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);
            await SendAsync(mailMessage);
        }
        private MimeMessage CreateEmailMessage(Message message)
        {
            try
            {
                foreach (var value in message.Content)
                {
                }

                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(String.Empty, _emailConfig.From));
                emailMessage.To.AddRange(message.To);
                emailMessage.Subject = message.Subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
                return emailMessage;
            }
            catch (Exception ex)
            {
                return null;
            }                      
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(mailMessage);
                }
                catch(Exception ex)
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }        
    }
}
