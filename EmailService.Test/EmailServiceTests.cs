using AutoMapper;
using EmailService.Application.Interfaces;
using EmailService.Domain.Context;
using EmailService.Domain.Entities;
using EmailService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Test
{
    public class EmailServiceTests
    {

        private IEmailSenderService _emailSenderService;
        private Mock<ILogger<Application.Implementation.EmailService>> _logger;
        private IEmailService _emailService;
        private EmailDbContext _context;

        [SetUp]
        public void Setup()
        {
            _context = GetContextProvider().GetContext() as EmailDbContext;
            _emailSenderService = new EmailService.Application.Implementation.EmailSenderService();
            _logger = new Mock<ILogger<Application.Implementation.EmailService>>();
            _emailService = new EmailService.Application.Implementation.EmailService(_context,_emailSenderService,_logger.Object);
        }

        [Test]
        public async Task Emails_Pending_Should_Have_Status_Sent_After_Send()
        {
            //arrange
            var email = CreateRandomEmail();
            await _emailService.CreateEmail(email);

            // act
            await _emailService.SendPendingEmails();

            //assert
            var emailEntity = await _emailService.GetEmailById(email.Id);
            Assert.AreEqual(EmailStatusEnum.Sent, email.Status);
        }    
                 

        [Theory]
        [TestCase("a@wp.pl")]
        [TestCase("b@wp.pl")]
        public async Task Correct_Receipients_Should_Have_Status_Delivered_After_Send(string recipient)
        {
            //arrange
            var email = CreateRandomEmail(recipient);
            await _emailService.CreateEmail(email);

            // act
            await _emailService.SendPendingEmails();

            //assert
            var emailEntity = await _emailService.GetEmailById(email.Id);
            var emailDetailsStatus = emailEntity.EmailDetails.Select(z => z.Status).ToList();

            Assert.AreEqual(email.EmailDetails.Count, emailEntity.EmailDetails.Count);
            Assert.That(emailDetailsStatus, Is.All.EqualTo(EmailDetailStatusEnum.Delivered));
        }  
        
        
        [Theory]
        [TestCase("a@@@wp.pl")]
        [TestCase("b--/@@333wp/.pl")]
        public async Task Incorrect_Receipients_Should_Have_Status_Undelivered_After_Send(string recipient)
        {
            //arrange
            var email = CreateRandomEmail(recipient);
            await _emailService.CreateEmail(email);

            // act
            await _emailService.SendPendingEmails();

            //assert
            var emailEntity = await _emailService.GetEmailById(email.Id);
            var emailDetails = emailEntity.EmailDetails.ToList();

            Assert.AreEqual(email.EmailDetails.Count, emailEntity.EmailDetails.Count);
            Assert.That(emailDetails.Select(z => z.Status), Is.All.EqualTo(EmailDetailStatusEnum.Undelivered));
            Assert.That(emailDetails.Select(z => z.SentDate), Is.Not.Null);
        }

        #region helpers

        private IContextProvider GetContextProvider()
        {
            var options = new DbContextOptionsBuilder<EmailDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString())
                     .Options;
            var ctx = new EmailDbContext(options);
            return new EmailDbContextProvider(ctx);
        }

        private EmailInfo CreateRandomEmail(string recipient = null)
        {
            return new EmailInfo
            {
                Content = "Content",
                Subject = "Subject",
                Sender = "sender@wp.pl",
                Status = EmailStatusEnum.Pending,
                CreatedDate = DateTime.Now,
                EmailDetails =  new List<EmailDetails>
                {
                    new EmailDetails
                    {
                        Recipient = recipient ?? "test@test.pl"
                    }
                }
            };
        }

        #endregion
    }
}
