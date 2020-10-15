using EmailService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmailService.Domain.Entities
{
    public class EmailDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Recipient { get; set; }
        public EmailDetailStatusEnum? Status { get; set; }
        public DateTime? SentDate { get; set; }
        public EmailInfo EmailInfo { get; set; }

    }
}
