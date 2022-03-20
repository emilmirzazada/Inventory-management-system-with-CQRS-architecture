using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Domain.Statuses
{
    public static class NotificationStatus
    {
        public static readonly List<Status> NotificationStatuses;
        static NotificationStatus()
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

            NotificationStatuses = new List<Status>
            {
                NotViewed,Viewed
            };

        }

        public static Status NotViewed;
        public static Status Viewed;
    }
    
}
