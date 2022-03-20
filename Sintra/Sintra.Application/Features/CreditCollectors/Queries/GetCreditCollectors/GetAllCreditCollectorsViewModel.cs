using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.CreditCollectors.Queries.GetAllSellers
{
    public class GetAllCreditCollectorsViewModel:ApplicationUser
    {
        public string ReceiveCreditBalance
        {
            get
            {
                return $@"
                    <button id='receiveBalance' data-employeeId='{Id}' data-amount='{Balance}'
                                class='btn btn-default waves-effect waves-light'>
                            Qəbul et
                        </button>
                        ";
            }
        }
    }
}
