using Sintra.Domain.Entities;
using Sintra.Domain.Statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sintra.Application.Features.ProductTransfers.Queries.GetAllProductTransfers
{
    public class GetAllProductTransfersViewModel : ProductTransfer
    {
        public string Status
        {
            get
            {
                return TransferStatus.TransferStatuses.Where(x => x.Id == TransferStatusId).FirstOrDefault().Value;
            }
        }
        public string CreateDatee
        {
            get
            {
                return CreateDate.ToString("dd-MM-yyyy HH:mm");
            }
        }
        public string ApproveDatee
        {
            get
            {
                return ApproveDate.ToString("dd-MM-yyyy") == "01-01-0001" ? "" : ApproveDate.ToString("dd-MM-yyyy HH:mm");
            }
        }
        public string transferProducts
        {
            get
            {
                return $@"<a href = '/ProductTransfer/TransferProducts?transferId={Id}' id='{Id}' style='margin-left: 5px;' class='on-default edit-row'><i class='fa fa-info'></i></a>";
            }
        }
    }
}
