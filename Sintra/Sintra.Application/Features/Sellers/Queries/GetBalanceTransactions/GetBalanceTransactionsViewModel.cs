using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.Sellers.Queries.GetBalanceTransactions
{
    public class GetBalanceTransactionsViewModel:EmployeeBalanceTransaction
    {
        public string RecieveDatee
        {
            get
            {
                return RecieveDate.ToString("dd-MM-yyyy HH:mm");
            }
        }
    }
}
