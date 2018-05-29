using EventManagementCoreApi2.Core.Models;
using EventManagementCoreApi2.DAL.Repository;
using EventManagementCoreApi2.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementCoreApi2.Services.Service
{
    public class EventService : IEventService
    {
        private IEventRepository _eventRepo;

        public EventService(IEventRepository eventRepo)
        {
            _eventRepo = eventRepo;
        }

        public async Task<bool> AddEventAsync(Event obj)
        {
            if (string.IsNullOrEmpty(obj.Name))
                return false;

            if (string.IsNullOrEmpty(obj.Location))
                return false;

            if (string.IsNullOrEmpty(obj.Type))
                return false;

            var evt = new Event
            {
                Id = Guid.NewGuid(),
                Name = obj.Name,
                Location = obj.Location,
                Type = obj.Type,
                Status = 1,
                UserId = obj.UserId
            };

            var isAdded = await _eventRepo.AddEventAsync(evt);

            return isAdded ? true : false;
        }

        public async Task<bool> AddEventDetailAsync(EventDetail obj)
        {
            var detail = new EventDetail
            {
                Id = Guid.NewGuid(),
                Time = DateTime.UtcNow,
                Detail = obj.Detail,
                EventId = Guid.Parse(obj.EventId.ToString())
            };

            var isAdded = await _eventRepo.AddEventDetailAsync(detail);

            return isAdded ? true : false;
        }
    }
}
