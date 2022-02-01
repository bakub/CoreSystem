using MassTransit;
using UserService.Models;
using UserService.Repository;

namespace UserService.Consumers.GetUsers
{
    public class GetUsersConsumer : IConsumer<GetUsersQuery>
    {
        public GetUsersConsumer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private IUserRepository _userRepository { get; set; }

        public async Task Consume(ConsumeContext<GetUsersQuery> context)
        {
            var users = await _userRepository.GetUsersAsync();

            var usersDto = users
                .Select(user=>UserDto.Create(user))
                .ToList();

            await context.RespondAsync(UsersDto.Create(usersDto));
        }
    }
}
