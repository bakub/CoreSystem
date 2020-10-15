using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService.Application.Email.Commands;
using EmailService.Application.Email.Dtos;
using EmailService.Application.Email.Queries;
using EmailService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreSystem.Controllers
{
    [Route("api")]
    public class EmailController : EmailControllerBase
    {
        [HttpGet("emails")]
        public async Task<ICollection<EmailDto>> GetEmails()
        {
            return await Mediator.Send(new GetAllEmailsQuery {  });
        }

        [HttpGet("email/{id}")]
        public async Task<ActionResult<EmailDto>> Get(int id)
        {
            var email = await Mediator.Send(new GetEmailQuery {  Id = id });
            if (email == null)
                return NotFound();

            return Ok(email);
        }      
        
        [HttpGet("email/{id}/status")]
        public async Task<ActionResult<EmailStatusDto>> GetStatus(int id)
        {
            var email = await Mediator.Send(new GetEmailStatusQuery { Id = id });
            if (email == null)
                return NotFound();

            return Ok(email);
        }

        [HttpPost("email")]
        public async Task<ActionResult<int>> Create([FromBody]CreateEmailCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("emails/send")]
        public async Task<ActionResult> SendPendingEmails()
        {
            await Mediator.Send(new SendPendingEmailsCommand());
            return Ok();
        }
    }
}
