using NotificationService.Domain.Entities;
using NotificationService.Interfaces;
using System.Net;
using System.Net.Mail;

namespace NotificationService.Implementation
{
    public class EmailService : IEmailService
    {
        static string smtpAddress = "smtp.gmail.com";
        static int portNumber = 587;
        static bool enableSSL = true;
        static string emailFromAddress = "tramanalyzer@gmail.com"; //Sender Email Address  
        static string password = "Zlewozmywak1"; //Sender Password  
        static string emailToAddress = "bar.jakub@hotmail.com"; //Receiver Email Address  
        static string subject = "Amazonka";

        public async Task SendEmail(NotificationHistory notification, string subject, string content)
        {
            using (MailMessage mail = new MailMessage())
            using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))

            {
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(notification.Email);
                mail.Subject = subject;
                mail.Body = content;
                mail.IsBodyHtml = true;

                {
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }
    }
}
