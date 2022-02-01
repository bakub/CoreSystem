namespace UserService.Models
{
    public class UsersDto
    {
        UsersDto(List<UserDto> users)
        {
            Users = users;
        }

        public List<UserDto> Users { get; }

        public static UsersDto Create(List<UserDto> users) => new(users);
    }
}
