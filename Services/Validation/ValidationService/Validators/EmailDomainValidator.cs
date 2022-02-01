using Integrations.Interfaces;

namespace ValidationService.Validators
{
    public class EmailDomainValidator : IEmailDomainValidator
    {
        private IReadOnlyCollection<string> ForbiddenDomains = new List<string>{ "wp.pl" };

        public async Task<ValidationResult> IsAllowedDomain(string email)
        {
            return ValidationResult.Success();
        }
    }
}
