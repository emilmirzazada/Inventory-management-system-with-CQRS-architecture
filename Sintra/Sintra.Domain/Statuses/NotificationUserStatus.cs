using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Domain.Statuses
{
    public static class NotificationUserStatus
    {
        public static readonly List<Status> NotificationUserStatuses;
        static NotificationUserStatus()
        {
            NotViewed = new Status
            {
                Id = 1,
                Value = "Not Viewed"
            };
            Viewed = new Status
            {
                Id = 2,
                Value = "Viewed"
            };

            NotificationUserStatuses = new List<Status>
            {
                NotViewed,Viewed
            };

        }

        public static Status NotViewed;
        public static Status Viewed;

    }
}
