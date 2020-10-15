using AutoMapper;
using EmailService.Application.Email.Dtos;
using EmailService.Application.Interfaces;
using EmailService.Domain.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Application.Email.Queries
{
    public class GetAllEmailsQuery : IRequest<ICollection<EmailDto>>
    {

    }

    public class GetAllEmailsQueryHandler : IRequestHandler<GetAllEmailsQuery, ICollection<EmailDto>>
    {
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public GetAllEmailsQueryHandler(IEmailService emailService, IMapper mapper)
        {
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<ICollection<EmailDto>> Handle(GetAllEmailsQuery request, CancellationToken cancellationToken)
        {
            var emails = await _emailService.GetAllEmails();
            return _mapper.Map<ICollection<EmailDto>>(emails);
        }
    }
}
