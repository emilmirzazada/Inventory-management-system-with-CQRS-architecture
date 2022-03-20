using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersViewModel : Order
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
        public string OrderAccessories
        {
            get
            {
                return $@"<a href = '/Order/OrderAccessories?orderId={Id}' id='{Id}' style='margin-left: 5px;' class='on-default edit-row'><i class='fa fa-info'></i></a>";
            }
        }
        public string IsCredit
        {
            get
            {
                if (CreditId != 0)
                    return $@"<a href = '#' id='{CreditId}' data-toggle='modal' onclick='emptydata()' data-target='#creditmodal' style ='margin-left: 5px;' class='on-default edit-row creditlink'><i class='fa fa-info'></i></a>";
                else
                    return "";
            }
        }
        public string actions
        {
            get
            {
                /*return $@"<a href = '#' id='{Id}' style='margin-left: 5px;' class='editlink on-default edit-row' data-toggle='modal' data-target='#editmodal'><i class='fa fa-pencil'></i></a>
	                      <a href = '#' id='{Id}' style='margin-left: 10px;' class='deletelink on-default remove-row'><i class='fa fa-trash-o'></i></a>";*/
                return $"<a href = '#' id = '{Id}' style = 'margin-left: 10px;' class='deletelink on-default remove-row'><i class='fa fa-trash-o'></i></a>";
            }
        }
    }
}
