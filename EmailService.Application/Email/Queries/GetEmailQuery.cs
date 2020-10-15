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
    public class GetEmailQuery : IRequest<EmailDto>
    {
        public int Id { get; set; }
    }

    public class GetEmailQueryHandler : IRequestHandler<GetEmailQuery, EmailDto>
    {
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public GetEmailQueryHandler(IEmailService emailService, IMapper mapper)
        {
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<EmailDto> Handle(GetEmailQuery request, CancellationToken cancellationToken)
        {
            var email = await _emailService.GetEmailById(request.Id);
            return _mapper.Map<EmailDto>(email);
        }
    }
}
