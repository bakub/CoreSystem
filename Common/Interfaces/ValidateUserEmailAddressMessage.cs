namespace Integrations.Interfaces
{
    public class ValidateUserEmailAddressMessage
    {
        public ValidateUserEmailAddressMessage(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public string EmailAddress { get; set; }
    }
}
