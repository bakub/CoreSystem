using EmailService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService.Application.Email.Dtos
{
    public class EmailDto
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
        public EmailStatusEnum Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public ICollection<EmailDetailDto> EmailDetails { get; set; }
    }
}
