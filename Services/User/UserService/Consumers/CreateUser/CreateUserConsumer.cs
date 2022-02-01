using Integrations.Interfaces;
using MassTransit;
using UserService.Models;
using UserService.Repository;

namespace UserService.Consumers.CreateUser
{
    public class CreateUserConsumer : IConsumer<CreateUserCommand>
    {
        public CreateUserConsumer(
            IBus eventBus, 
            IUserRepository userRepository, 
            IRequestClient<ValidateUserEmailAddressMessage> validateEmailAddressClient)
        {
            _eventBus = eventBus;
            _userRepository = userRepository;
            _validateEmailAddressClient = validateEmailAddressClient;
        }

        private IBus _eventBus { get; set; }
        private IUserRepository _userRepository { get; set; }
        private IRequestClient<ValidateUserEmailAddressMessage> _validateEmailAddressClient { get; set; }


        public async Task Consume(ConsumeContext<CreateUserCommand> context)
        {
            //var validation = await _validateEmailAddressClient.GetResponse<ValidationResult>
            //    (new ValidateUserEmailAddressMessage(context.Message.EmailAdress));
            //if (!validation.Message.IsValid)
            //    return;

            var userCommand = context.Message;

            var userId = await _userRepository.CreateUserAsync(userCommand.FirstName, userCommand.LastName, userCommand.EmailAdress);

            var newUserEvent = new CreateUserEvent(userCommand.EmailAdress);

            await _eventBus.Publish(newUserEvent);

            await context.RespondAsync(new UserIdDto(userId));
        }
    }
}
