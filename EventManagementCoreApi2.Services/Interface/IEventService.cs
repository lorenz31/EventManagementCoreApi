using EventManagementCoreApi2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementCoreApi2.Services.Interface
{
    public interface IEventService
    {
        Task<bool> AddEventAsync(Event obj);
    }
}
