using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Domain.Statuses
{
    public class NotificationType
    {
        public static readonly List<Status> NotificationTypes;
        static NotificationType()
        {
            Transfer = new Status
            {
                Id = 1,
                Value = "Transfer"
            };
            NotificationTypes = new List<Status>
            {
                Transfer
            };

        }
        public static Status Transfer;
    }
}
