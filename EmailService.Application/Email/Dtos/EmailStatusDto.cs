using EmailService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService.Application.Email.Dtos
{
    public class EmailStatusInfoDto
    {
        public EmailDetailStatusEnum? Status { get; set; }
        public string Recipient { get; set; }
    } 
    
    public class EmailStatusDto
    {
        public EmailStatusEnum? Status { get; set; }
        public ICollection<EmailStatusInfoDto> Emails { get; set; }
    }
}
