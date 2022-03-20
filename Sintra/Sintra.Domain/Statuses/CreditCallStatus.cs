using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Domain.Statuses
{
    public static class CreditCallStatus
    {
        public static readonly List<Status> CreditCallStatuses;
        static CreditCallStatus()
        {
            NotAnswered = new Status
            {
                Id = 1,
                Value = "Cavab vermədi"
            };
            HangedUp = new Status
            {
                Id = 2,
                Value = "Söndürdü"
            };
            AnsweredAndSaidNextDate = new Status
            {
                Id = 3,
                Value = "Cavab verdi və yeni tarix dedi"
            };
            Rejected = new Status
            {
                Id = 4,
                Value = "Rədd elədi"
            };
            CreditCallStatuses = new List<Status>
            {
                NotAnswered,HangedUp,AnsweredAndSaidNextDate,Rejected
            };
        }

        public static Status NotAnswered;
        public static Status HangedUp;
        public static Status AnsweredAndSaidNextDate;
        public static Status Rejected;
    }
}
