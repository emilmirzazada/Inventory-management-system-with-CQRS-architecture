using Sintra.Application.Features.Notiifications.Queries.GetUserNotifications;
using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Interfaces.Repositories
{
    public interface INotificationRepository : IGenericRepositoryAsync<Notification>
    {
        List<UserNotificationsViewModel> GetUserNotifications(string userId);
    }
}
