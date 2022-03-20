using System;
using System.Collections.Generic;
using System.Text;

namespace Sintra.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
    }
}
