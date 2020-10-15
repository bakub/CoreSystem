using EmailService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmailService.Domain.Entities
{
    public class EmailInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
        public EmailStatusEnum Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public ICollection<EmailDetails> EmailDetails { get; set; }
    }
}
