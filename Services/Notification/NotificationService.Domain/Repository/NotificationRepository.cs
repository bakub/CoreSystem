using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Context;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Domain.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationsDbContext _dbContext;

        public NotificationRepository(NotificationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<NotificationHistory> GetNotificationById(int id)
        {
            return await _dbContext.NotificationHistories.FirstOrDefaultAsync(z => z.Id == id);
        }

        public async Task<ICollection<NotificationHistory>> GetPendingNotifications()
        {
            return await _dbContext.NotificationHistories
                .Where(z => z.SendDate == null && z.ErrorCount < 3)
                .OrderBy(z => z.CreateDate)
                .ToListAsync();
        }

        public async Task<long> CreateNotification(string email, NotificationType type)
        {
            var notification = new NotificationHistory { Email = email, Type = type, CreateDate = DateTime.Now };

            await _dbContext.NotificationHistories.AddAsync(notification);
            await _dbContext.SaveChangesAsync();
            return notification.Id;
        }

        public async Task MarkNotificationAsSent(NotificationHistory notification)
        {
            notification.SendDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();
        }

        public async Task IncreaseNotificationErrorCount(NotificationHistory notification)
        {
            notification.ErrorCount = +1;
            await _dbContext.SaveChangesAsync();
        }
    }
}
