using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.Notiifications.Queries.GetUserNotifications
{
    public class UserNotificationsViewModel:Notification
    {
        public string Name { get; set; }
    }
}
