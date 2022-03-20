using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Domain.Entities
{
    public class OrderBonus
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string EmployeeId { get; set; }
        public ApplicationUser Employee { get; set; }
        public decimal Bonus { get; set; }
        public bool BonusPaid { get; set; }
        public decimal Percentage { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
    }
}
