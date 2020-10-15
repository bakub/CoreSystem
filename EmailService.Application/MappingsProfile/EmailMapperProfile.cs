using AutoMapper;
using EmailService.Application.Email.Dtos;
using EmailService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService.Application.MappingsProfile
{
    public class EmailMapperProfile : Profile
    {
        public EmailMapperProfile()
        {
            MapEmailDtoToEmail();
            MapEmailToEmailDto();
        }

        private void MapEmailDtoToEmail()
        {
            //CreateMap<EmailDto, EmailInfo>()
            //    .ForMember(d => d.EmailDetails, opt => opt.MapFrom(x => x));
            //CreateMap<EmailDto, EmailDetails>();
        }

        private void MapEmailToEmailDto()
        {
            CreateMap<EmailDetails, EmailDto>();
            CreateMap<EmailInfo, EmailDto>()
                .ForMember(d => d.EmailDetails, opt => opt.MapFrom(x => x.EmailDetails));
            CreateMap<EmailInfo, EmailStatusDto>()
                .ForMember(d => d.Emails, opt => opt.MapFrom(x => x.EmailDetails));
            CreateMap<EmailDetails, EmailDetailDto>();
            CreateMap<EmailDetails, EmailStatusInfoDto>();
        }
    }
}
