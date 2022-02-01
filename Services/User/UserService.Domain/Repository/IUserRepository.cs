using UserService.Domain.Entities;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<ICollection<User>> GetUsersAsync();
        Task<int> CreateUserAsync(string firstName, string lastName, string emailAddress);
    }
}
