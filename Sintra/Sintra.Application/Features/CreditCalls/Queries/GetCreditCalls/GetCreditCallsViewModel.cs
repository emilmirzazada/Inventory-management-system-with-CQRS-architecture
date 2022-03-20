using Sintra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Application.Features.CreditCalls.Queries.GetCreditCalls
{
    public class GetCreditCallsViewModel : CreditCall
    {
        public Order Order { get; set; }

        public string commentSpan
        {
            get
            {
                if(Comment!=null && Comment.Length>7)
                    return $@"<span title='{Comment}'>{Comment.Substring(0,7)}</span>";
                return $@"<span title='{Comment}'>{Comment}</span>";
            }
        }
    }
}
