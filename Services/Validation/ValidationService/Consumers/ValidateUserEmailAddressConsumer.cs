using Integrations.Interfaces;
using MassTransit;
using ValidationService.Validators;

namespace ValidationService.Consumers
{
    public class ValidateUserEmailAddressConsumer : IConsumer<ValidateUserEmailAddressMessage>
    {
        private readonly IEmailDomainValidator _emailDomainValidator;

        public ValidateUserEmailAddressConsumer(IEmailDomainValidator emailDomainValidator)
        {
            _emailDomainValidator = emailDomainValidator;
        }

        public async Task Consume(ConsumeContext<ValidateUserEmailAddressMessage> context)
        {
            var result = await _emailDomainValidator.IsAllowedDomain(context.Message.EmailAddress);
            await context.RespondAsync(result);
        }
    }
}
