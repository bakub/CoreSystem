using UserService.Domain.Entities;

namespace UserService.Models
{
    public class UserDto
    {
        UserDto(string firstName, string lastName, string emailAddress, long id)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Id = id;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }
        public long Id { get; }

        public static UserDto Create(User user) => new(user.FirstName, user.LastName, user.EmailAddress, user.Id);
    }
}
