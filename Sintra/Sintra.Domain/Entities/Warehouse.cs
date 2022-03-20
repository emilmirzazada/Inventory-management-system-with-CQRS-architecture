using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Sintra.Domain.Entities
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        [JsonIgnore]
        public bool Deleted { get; set; }

    }
}
