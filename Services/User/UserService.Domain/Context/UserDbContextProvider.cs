using Microsoft.EntityFrameworkCore;

namespace UserService.Domain.Context
{
    public interface IContextProvider
    {
        DbContext GetContext();
    }
    public class UserDbContextProvider : IContextProvider
    {
        private readonly DbContext _context;
        public UserDbContextProvider(UserDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public DbContext GetContext()
        {
            return _context;
        }
    }
}
