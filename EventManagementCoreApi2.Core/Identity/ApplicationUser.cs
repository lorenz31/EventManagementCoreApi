using EventManagementCoreApi2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagementCoreApi2.Core.Identity
{
    public class ApplicationUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
