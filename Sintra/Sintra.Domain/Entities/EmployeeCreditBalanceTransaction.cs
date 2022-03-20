using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Domain.Entities
{
    public class EmployeeCreditBalanceTransaction
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string RecieverEmployeeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime RecieveDate { get; set; }
        public ApplicationUser Employee { get; set; }

        public ApplicationUser RecieverEmployee { get; set; }
    }
}
