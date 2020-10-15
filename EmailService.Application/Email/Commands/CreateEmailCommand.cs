using AutoMapper;
using EmailService.Application.Interfaces;
using EmailService.Domain.Context;
using EmailService.Domain.Entities;
using EmailService.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Application.Email.Commands
{
    public class CreateEmailCommand : IRequest<int>
    {
        public List<string> Recipients { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
    }

    public class CreateEmailCommandHandler : IRequestHandler<CreateEmailCommand, int>
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public CreateEmailCommandHandler(IEmailService emailService, IConfiguration configuration)
        {
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<int> Handle(CreateEmailCommand request, CancellationToken cancellationToken)
        {
            var sender = _configuration.GetValue<string>("Sender");

            var newEmail = new EmailInfo
            {
                Sender = sender,
                Content = request.Content,
                Subject = request.Subject,
                CreatedDate = DateTime.Now,
                EmailDetails = request.Recipients.Select(recipient => new EmailDetails
                {
                    Recipient = recipient,
                }).ToList()
            };
            return await _emailService.CreateEmail(newEmail);
        }
    }
    
}
