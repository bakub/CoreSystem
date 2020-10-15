using EmailService.Application.Interfaces;
using EmailService.Domain.Context;
using EmailService.Domain.Entities;
using EmailService.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Application.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailDbContext _dbContext;
        private readonly IEmailSenderService _emailSenderService;
        private readonly ILogger<EmailService> _logger;

        public EmailService(EmailDbContext dbContext, IEmailSenderService emailSenderService, ILogger<EmailService> logger)
        {
            _dbContext = dbContext;
            _emailSenderService = emailSenderService;
            _logger = logger;
        }

        public async Task<EmailInfo> GetEmailById(int id)
        {
            return await _dbContext.EmailInfo.Include(z => z.EmailDetails).FirstOrDefaultAsync(z => z.Id == id);
        }   
        
        public async Task<ICollection<EmailInfo>> GetAllEmails()
        {
            return await _dbContext.EmailInfo.Include(z=>z.EmailDetails).ToListAsync();
        }     

        public async Task<int> CreateEmail(EmailInfo email)
        {
            await _dbContext.EmailInfo.AddAsync(email);
            await _dbContext.SaveChangesAsync();
            return email.Id;
        }

        public async Task<ICollection<EmailInfo>> GetEmailsByStatus(EmailStatusEnum status)
        {
            return await _dbContext.EmailInfo.Include(z => z.EmailDetails).Where(z => z.Status == status).ToListAsync();
        }

        public async Task SendPendingEmails()
        {
            var emailRecipients = await GetPendingEmails();
            foreach (var email in emailRecipients)
            {
                try
                {
                    await _emailSenderService.SendEmail(email);
                    email.SentDate = DateTime.UtcNow;
                    email.Status = EmailDetailStatusEnum.Delivered;
                }
                catch (Exception ex)
                {
                    email.Status = EmailDetailStatusEnum.Undelivered;
                    _logger.LogError(ex, "Coudnt send email with address: " + email.Recipient);
                }
            }
            await _dbContext.SaveChangesAsync();
            await MarkEmailsAsSent(emailRecipients);
        }

        #region private

        private async Task<ICollection<EmailDetails>> GetPendingEmails()
        {
            var emails = await GetEmailsByStatus(EmailStatusEnum.Pending);
            return emails.SelectMany(q=>q.EmailDetails).OrderBy(z => z.EmailInfo.CreatedDate).ToList();
        }
        
        private async Task MarkEmailsAsSent(ICollection<EmailDetails> emailDetails)
        {
            var emails = emailDetails.GroupBy(q => q.EmailInfo).ToList();
            foreach (var email in emails)
                email.Key.Status = EmailStatusEnum.Sent;

            await _dbContext.SaveChangesAsync();
        }

        #endregion
    }
}
