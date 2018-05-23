using EventManagementCoreApi2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementCoreApi2.DAL.Repository
{
    public interface IEventRepository
    {
        Task<bool> AddEventAsync(Event obj);
    }
}
