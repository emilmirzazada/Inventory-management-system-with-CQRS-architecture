using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.Sellers.Queries.GetAllSellers
{
    public class GetAllSellersViewModel:ApplicationUser
    {
        public string ReceiveBalance
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
