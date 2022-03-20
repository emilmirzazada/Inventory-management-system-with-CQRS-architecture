using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.OrderBonuses.Queries.GetAllOrderBonuses
{
    public class GetAllOrderBonusesViewModel:OrderBonus
    {
        public string PayBonus
        {
            get
            {
                return $@"
                    <button id='payBonus' data-employeeId='{EmployeeId}' data-amount='{Bonus}'
                                class='btn btn-default waves-effect waves-light'>
                            Ödə
                        </button>
                        ";
            }
        }
    }
}
