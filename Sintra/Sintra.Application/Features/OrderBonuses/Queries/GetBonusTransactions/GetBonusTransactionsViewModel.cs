using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.OrderBonuses.Queries.GetBonusTransactions
{
    public class GetBonusTransactionsViewModel: EmployeeBonusTransaction
    {
        public string PayDatee
        {
            get
            {
                return PayDate.ToString("dd-MM-yyyy HH:mm");
            }
        }
    }
}
