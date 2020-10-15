using EmailService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService.Domain.Context
{
    public class EmailDbContext : DbContext
    {

        public EmailDbContext(DbContextOptions<EmailDbContext> options)
            : base(options)
        {
        }

        public DbSet<EmailInfo> EmailInfo { get; set; }
        public DbSet<EmailDetails> EmailDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}