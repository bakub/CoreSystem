using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected IRequestClient<T> MediatorClient<T>() where T : class
        {
            return Mediator.CreateRequestClient<T>();
        }
    }
}