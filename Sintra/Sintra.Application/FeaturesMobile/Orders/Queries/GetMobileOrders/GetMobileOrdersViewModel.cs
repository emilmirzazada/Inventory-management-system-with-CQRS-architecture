using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.FeaturesMobile.Orders.Queries.GetMobileOrders
{
    public class GetMobileOrdersViewModel : Order
    {
        public string CreateDatee
        {
            get
            {
                return CreateDate.ToString("dd-MM-yyyy HH:mm");
            }
        }
        public string ClientName
        {
            get
            {
                return Client.FirstName + " " + Client.LastName;
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
