using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CreditId { get; set; }
        public string EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public decimal PreviousDebt { get; set; }
        public decimal PresentDebt { get; set; }
        public DateTime Date { get; set; }
        public bool IsPaid { get; set; }
        public Credit Credit { get; set; }
        public ApplicationUser Employee { get; set; }
    }
}
