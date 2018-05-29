using EventManagementCoreApi2.Core.Models;
using EventManagementCoreApi2.DAL.DataContext;
using EventManagementCoreApi2.DAL.Repository;
using EventManagementCoreApi2.Helpers;
using EventManagementCoreApi2.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementCoreApi2.UnitTest
{
    [TestClass]
    public class EventServiceUnitTest
    {
        EventRepository eventRepo;
        GenericRepository<EventDetail> eventDetailRepo;
        DatabaseContext db;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>();
            options.UseSqlServer(DbConnectionStringHelper.CONNECTIONSTRING);

            db = new DatabaseContext(options.Options);
            eventRepo = new EventRepository(db);
            eventDetailRepo = new GenericRepository<EventDetail>(db);
        }

        [TestMethod]
        public async Task Event_AddEventAsync_Test()
        {
            var evt = new Event
            {
                Id = Guid.NewGuid(),
                Name = "Code Camp",
                Location = "DevHub Davao City",
                Type = "Workshop",
                Status = 1,
                UserId = Guid.Parse("D591E665-54F2-4D39-8E66-08D5BF33FD98")
            };

            if (string.IsNullOrEmpty(evt.Name))
                Assert.Fail();

            if (string.IsNullOrEmpty(evt.Location))
                Assert.Fail();

            if (string.IsNullOrEmpty(evt.Type))
                Assert.Fail();

            var isAdded = await eventRepo.AddEventAsync(evt);

            Assert.IsTrue(isAdded);
        }

        [TestMethod]
        public async Task Event_AddEventDetailAsync_Test()
        {
            string eventId = "7F356397-938B-4A2F-8DB3-5A60FDF6CEC7";

            var detail = new EventDetail
            {
                Id = Guid.NewGuid(),
                Time = DateTime.UtcNow,
                Detail = "Angular 5 workshop",
                EventId = Guid.Parse(eventId)
            };

            var isAdded = await eventRepo.AddEventDetailAsync(detail);

            Assert.IsTrue(isAdded);
        }
    }
}
