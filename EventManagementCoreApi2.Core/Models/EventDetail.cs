using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagementCoreApi2.Core.Models
{
    public class EventDetail
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public string Detail { get; set; }
        public Event Event { get; set;  }
    }
}
