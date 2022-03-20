using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.Transactions.Queries.GetAllTransactions
{
    public class GetAllTransactionsViewModel:Transaction
    {
        public string Datee
        {
            get
            {
                return Date.ToString("dd-MM-yyyy HH:mm");
            }
        }
        public string EmployeeName
        {
            get
            {
                return Employee.FirstName + " " + Employee.LastName;
            }
        }
    }
}
