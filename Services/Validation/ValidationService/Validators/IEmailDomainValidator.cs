using Integrations.Interfaces;

namespace ValidationService.Validators
{
    public interface IEmailDomainValidator
    {
        Task<ValidationResult> IsAllowedDomain(string email);
    }
}
