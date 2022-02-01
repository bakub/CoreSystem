using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationService.Domain.Context
{
    public interface IContextProvider
    {
        DbContext GetContext();
    }
    public class EmailDbContextProvider : IContextProvider
    {
        private DbContext _context;
        public EmailDbContextProvider(NotificationsDbContext context)
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
