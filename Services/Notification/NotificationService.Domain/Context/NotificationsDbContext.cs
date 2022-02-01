using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationService.Domain.Context
{
    public class NotificationsDbContext : DbContext
    {

        public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options)
            : base(options)
        {
        }

        public DbSet<NotificationHistory> NotificationHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}