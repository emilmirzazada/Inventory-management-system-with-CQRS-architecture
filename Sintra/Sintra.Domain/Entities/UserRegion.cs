using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sintra.Domain.Entities
{
    public class UserRegion
    {
        public string UserId { get; set; }
        public int RegionId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
       
        public Region Region { get; set; }
    }
}
