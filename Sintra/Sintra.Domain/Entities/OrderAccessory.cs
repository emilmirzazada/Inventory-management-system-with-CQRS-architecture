using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sintra.Domain.Entities
{
    public class OrderAccessory
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int AccessoryId { get; set; }
        public DateTime RepeatDate { get; set; }
        public DateTime LastRepeatDate { get; set; }

        public Order Order { get; set; }
        public Product Accessory { get; set; }
        [NotMapped]
        public string RepeatDatee
        {
            get
            {
                return RepeatDate.ToString("dd-MM-yyyy HH:mm");
            }
        }
        [NotMapped]
        public string LastRepeatDatee
        {
            get
            {
                if (LastRepeatDate != null)
                    return LastRepeatDate.ToString("dd-MM-yyyy") == "01-01-0001" ? "" : LastRepeatDate.ToString("dd-MM-yyyy HH:mm");
                return null;
            }
        }
    }
}
