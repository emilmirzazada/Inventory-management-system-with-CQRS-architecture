using Sintra.Domain.Statuses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sintra.Domain.Entities
{
    public class CreditCall
    {
        public int Id { get; set; }
        public int CreditId { get; set; }
        public Credit Credit { get; set; }
        public DateTime CallDate { get; set; }
        public DateTime NextCallDate { get; set; }
        public string Comment { get; set; }
        public int StatusId { get; set; }
        [NotMapped]
        public string Status
        {
            get
            {
                return CreditCallStatus.CreditCallStatuses.Where(x => x.Id == StatusId).FirstOrDefault().Value;
            }
        }
        public string EmployeeId { get; set; }
        public ApplicationUser Employee { get; set; }
        [NotMapped]
        public string CallDatee
        {
            get
            {
                return CallDate.ToString("dd-MM-yyyy HH:mm");
            }
        }
        [NotMapped]
        public string NextCallDatee
        {
            get
            {

                return NextCallDate.ToString("dd-MM-yyyy") == "01-01-0001" ? "" : NextCallDate.ToString("dd-MM-yyyy");
            }
        }

    }
}

