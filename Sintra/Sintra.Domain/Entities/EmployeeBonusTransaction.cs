using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Domain.Entities
{
    public class EmployeeBonusTransaction
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string PayerEmployeeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PayDate { get; set; }
        public ApplicationUser Employee { get; set; }

        public ApplicationUser PayerEmployee { get; set; }
    }
}
