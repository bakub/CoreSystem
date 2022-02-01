using NotificationService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NotificationService.Domain.Entities
{
    public class NotificationHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public bool Deleted { get; set; }

        [Required]
        public long TimeStamp { get; set; }

        public string Email { get; set; }

        public NotificationType Type { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? SendDate { get; set; }
        public int ErrorCount { get; set; }
    }
}
