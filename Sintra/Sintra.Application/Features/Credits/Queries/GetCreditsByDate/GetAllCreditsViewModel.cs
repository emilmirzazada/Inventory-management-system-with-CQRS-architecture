using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.Credits.Queries.GetCreditsByDate
{
    public class GetAllCreditsViewModel:Credit
    {
        
        public Order Order { get; set; }
        public string AddCall
        {
            get
            {
                return $@"<a href = '#' id='{Id}' data-toggle='modal' data-target='#editmodal' style='margin-left: 5px;' class='on-default edit-row editlink'><i class='fa fa-pencil'></i></a>";
            }
        }
        public string EmployeeName
        {
            get
            {
                return Order?.Employee?.FirstName + " " + Order?.Employee?.LastName;
            }
        }
        public string ClientName
        {
            get
            {
                return Order?.Client?.FirstName + " " + Order?.Client?.LastName;
            }
        }
        public string Debtt
        {
            get
            {
                return Debt.ToString("F");
            }
        }
        public string Monthlyy
        {
            get
            {
                return Monthly.ToString("F");
            }
        }
    }
}
