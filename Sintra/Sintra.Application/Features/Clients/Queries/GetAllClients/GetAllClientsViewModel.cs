using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.Clients.Queries.GetAllClients
{
    public class GetAllClientsViewModel:Client
    {
        public string Orders
        {
            get
            {
                return $@"<a href = '/Order/Orders?clientId={Id}' id='{Id}' style='margin-left: 5px;' class='on-default edit-row'><i class='fa fa-info'></i></a>";
            }
        }
    }
}
