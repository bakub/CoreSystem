using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserService.Consumers.CreateUser;
using UserService.Consumers.GetUsers;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api")]
    public class UserController : ApiControllerBase
    {
        [HttpGet("users")]
        public async Task<ActionResult<ICollection<UsersDto>>> GetUsers()
        {
            var user = await MediatorClient<GetUsersQuery>()
                .GetResponse<UsersDto>(new GetUsersQuery());

            return Ok(user.Message);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var user =  await MediatorClient<GetUserByIdQuery>()
                .GetResponse<UserDto>(new GetUserByIdQuery(id));

            return Ok(user.Message);
        }

        [HttpPost("user")]
        public async Task<ActionResult<UserIdDto>> Create([FromBody] CreateUserCommand command)
        {
            var user = await MediatorClient<CreateUserCommand>()
                .GetResponse<UserIdDto>(command);

            return Ok(user.Message);
        }
    }
}