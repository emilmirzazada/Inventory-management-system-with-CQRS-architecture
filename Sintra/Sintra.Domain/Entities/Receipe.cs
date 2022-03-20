using System;
using Sintra.Domain.Common;

namespace Sintra.Domain.Entities
{
    public class Receipe : BaseEntity
    {
        public int OrderId { get; set; }

        public string WorkerId { get; set; }
        
        public DateTimeOffset Date { get; set; }

        public string Comment { get; set; }

        public Order Order { get; set; }

        public ApplicationUser Worker { get; set; }
    }
}