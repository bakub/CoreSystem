namespace Integrations.Interfaces
{
    public class CreateUserEvent //: ICreateUserEvent
    {
        public CreateUserEvent(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public string EmailAddress { get; set; }
    }
}
