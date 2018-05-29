using EventManagementCoreApi2.Core.Models;
using EventManagementCoreApi2.DAL.DataContext;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementCoreApi2.DAL.Repository
{
    public class EventRepository : IEventRepository
    {
        private DatabaseContext _dbContext;

        public EventRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddEventAsync(Event obj)
        {
            try
            {
                _dbContext.Events.Add(obj);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> AddEventDetailAsync(EventDetail obj)
        {
            try
            {
                var detail = new EventDetail
                {
                    Id = Guid.NewGuid(),
                    Time = DateTime.UtcNow,
                    Detail = obj.Detail,
                    EventId = Guid.Parse(obj.EventId.ToString())
                };

                _dbContext.EventDetail.Add(detail);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
