using System;
using System.Security.Principal;
using Sintra.Domain.Common;

namespace Sintra.Domain.Entities
{
    public class Service : BaseEntity
    {
        public int OrderAccessoryId { get; set; }

        public string WorkerId { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }
        
        public ApplicationUser Worker { get; set; }
        
        public OrderAccessory OrderAccessory { get; set; }
    }
}