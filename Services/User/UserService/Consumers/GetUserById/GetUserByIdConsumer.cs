using MassTransit;
using UserService.Models;
using UserService.Repository;

namespace UserService.Consumers.GetUsers
{
    public class GetUserByIdConsumer : IConsumer<GetUserByIdQuery>
    {
        public GetUserByIdConsumer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private IUserRepository _userRepository { get; set; }

        public async Task Consume(ConsumeContext<GetUserByIdQuery> context)
        {
            var user = await _userRepository.GetUserByIdAsync(context.Message.Id);

            var response = UserDto.Create(user);

            await context.RespondAsync(response);
        }
    }
}
