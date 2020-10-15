using EmailService.Application.Interfaces;
using EmailService.Domain.Context;
using EmailService.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Application.Email.Commands
{
    public class SendPendingEmailsCommand : IRequest<Unit>
    {

    }

    public class SendPendingEmailsCommandHandler : IRequestHandler<SendPendingEmailsCommand, Unit>
    {
        private readonly IEmailService _emailService;
        public SendPendingEmailsCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<Unit> Handle(SendPendingEmailsCommand request, CancellationToken cancellationToken)
        {
            await _emailService.SendPendingEmails();
            return Unit.Value;
        }
    }
}
