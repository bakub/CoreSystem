using EmailService.Application.Interfaces;
using EmailService.Domain.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmailService.Application.Implementation
{
    public class EmailSenderService : IEmailSenderService
    {

        public async Task SendEmail(EmailDetails email)
        {
            var emailMessage = new MimeMessage();
            emailMessage.Sender = MailboxAddress.Parse(email.EmailInfo.Sender);
            emailMessage.To.Add(MailboxAddress.Parse(email.Recipient));
            emailMessage.Subject = email.EmailInfo.Subject;
            emailMessage.Body = new TextPart(TextFormat.Html) { Text = email.EmailInfo.Content };

            //// send email
            //var smtp = new SmtpClient();
            //smtp.Connect("smtp.xx", 587, SecureSocketOptions.StartTls);
            //smtp.Authenticate("[USERNAME]", "[PASSWORD]");
            //smtp.Send(emailMessage);
            //smtp.Disconnect(true);
        }
    }
}
