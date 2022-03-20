using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Domain.Statuses
{
    public class TransferStatus
    {
        public static readonly List<Status> TransferStatuses;
        static TransferStatus()
        {
            Waiting = new Status
            {
                Id = 1,
                Value = "Gözləyir"
            };
            Approved = new Status
            {
                Id = 2,
                Value = "Təsdiqlənib"
            };
            Rejected = new Status
            {
                Id = 3,
                Value = "Rədd edilib"
            };
            TransferStatuses = new List<Status>
            {
                Waiting,Approved,Rejected
            };

        }

        public static Status Waiting;
        public static Status Approved;
        public static Status Rejected;
    }
}
