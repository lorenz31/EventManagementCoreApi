using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagementCoreApi2.Core.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
        public EventDetail Detail { get; set; }
        public ICollection<EventAttendee> Attendees { get; set; }
    }
}
