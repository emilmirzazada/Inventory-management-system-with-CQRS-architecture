using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sintra.Domain.Entities
{
    public class Credit
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Term { get; set; }
        public decimal Amount { get; set; }
        public decimal Debt { get; set; }
        public decimal InitialPayment { get; set; }
        [NotMapped]
        public decimal Monthly
        {
            get
            {
                return (Amount - InitialPayment) / Term;
            }
        }
        [NotMapped]
        public string Datee
        {
            get
            {
                return Date.ToString("dd-MM-yyyy HH:mm");
            }
        }

        public IEnumerable<CreditCall> CreditCalls { get; set; }
    }
}
