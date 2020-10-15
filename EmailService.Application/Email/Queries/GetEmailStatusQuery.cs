using AutoMapper;
using EmailService.Application.Email.Dtos;
using EmailService.Application.Interfaces;
using EmailService.Domain.Context;
using EmailService.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Application.Email.Queries
{
    public class GetEmailStatusQuery : IRequest<EmailStatusDto>
    {
        public int Id { get; set; }
    }

    public class GetEmailStatusQueryHandler : IRequestHandler<GetEmailStatusQuery, EmailStatusDto>
    {
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public GetEmailStatusQueryHandler(IEmailService emailService, IMapper mapper)
        {
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<EmailStatusDto> Handle(GetEmailStatusQuery request, CancellationToken cancellationToken)
        {
            var email = await _emailService.GetEmailById(request.Id);
            return _mapper.Map<EmailStatusDto>(email);
        }
    }
}
