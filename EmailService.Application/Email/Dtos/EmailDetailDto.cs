using EmailService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService.Application.Email.Dtos
{
    public class EmailDetailDto
    {
        public string Recipient { get; set; }
        public EmailDetailStatusEnum? Status { get; set; }
        public DateTime? SentDate { get; set; }
    }
}
