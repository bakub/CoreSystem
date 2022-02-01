namespace UserService.Models
{
    public class UserIdDto
    {
        public UserIdDto(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}
