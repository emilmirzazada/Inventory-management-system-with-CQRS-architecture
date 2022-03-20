using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Domain.Entities
{
    public class NotificationUser
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int NotificationId { get; set; }
        public string UserId { get; set; }
        public Notification Notification { get; set; }
        public ApplicationUser User { get; set; }
    }
}
