using EmailService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Application.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmail(EmailDetails email);
    }
}
