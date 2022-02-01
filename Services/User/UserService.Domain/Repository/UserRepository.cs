using Microsoft.EntityFrameworkCore;
using UserService.Domain.Context;
using UserService.Domain.Entities;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;

        public UserRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateUserAsync(string firstName, string lastName, string emailAddress)
        {
            var user = new User { 
                FirstName = firstName, 
                LastName = lastName,
                EmailAddress = emailAddress
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user.Id;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(z => z.Id == id);

        }

        public async Task<ICollection<User>> GetUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
    }
}
