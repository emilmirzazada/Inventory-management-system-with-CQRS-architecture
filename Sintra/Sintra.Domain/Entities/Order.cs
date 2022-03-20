using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        public string EmployeeId { get; set; }
        public int? ClientId { get; set; }
        public int? CreditId { get; set; }
        public Product Product { get; set; }
        public ApplicationUser Employee { get; set; }
        public Client Client { get; set; }
        public Credit Credit { get; set; }
        public bool IsPaid { get; set; }
        public bool Deleted { get; set; }
    }
}
