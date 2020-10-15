using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmailService.Domain.Entities;
using EmailService.Domain.Enums;
using MediatR;

namespace EmailService.Application.Interfaces
{
    public interface IEmailService
    {
        Task<EmailInfo> GetEmailById(int id);
        Task<ICollection<EmailInfo>> GetEmailsByStatus(EmailStatusEnum status);
        Task<ICollection<EmailInfo>> GetAllEmails();
        Task<int> CreateEmail(EmailInfo email);
        Task SendPendingEmails();

    }
}
