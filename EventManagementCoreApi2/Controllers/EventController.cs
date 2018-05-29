using EventManagementCoreApi2.Services.Interface;
using EventManagementCoreApi2.Core.Models;
using EventManagementCoreApi2.Core.Response;
using EventManagementCoreApi2.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementCoreApi2.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/v1/Event/{userid:guid}")]
    public class EventController : Controller
    {
        private IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> PostAddEvent(string userid, [FromBody] Event obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseModel { Status = false, Message = "Invalid payload." });

            var isValidId = GuidParserHelper.ParseStringToGuid(userid);

            if (isValidId)
            {
                var isAdded = await _eventService.AddEventAsync(obj);

                if (isAdded)
                    return Ok(new ResponseModel { Status = true, Message = "Event successfully added." });
                else
                    return BadRequest(new ResponseModel { Status = false, Message = "Error adding event." });
            }

            return BadRequest(new ResponseModel { Status = false, Message = "Error adding event." });
        }

        [HttpPost]
        [Route("detail")]
        public async Task<IActionResult> PostEventDetail(string userid, [FromBody] EventDetail obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseModel { Status = false, Message = "Invalid payload." });

            var isValidId = GuidParserHelper.ParseStringToGuid(userid);

            if (isValidId)
            {
                var isAdded = await _eventService.AddEventDetailAsync(obj);

                if (isAdded)
                    return Ok(new ResponseModel { Status = true, Message = "Event detail successfully added." });
                else
                    return BadRequest(new ResponseModel { Status = false, Message = "Error adding event detail." });
            }

            return BadRequest(new ResponseModel { Status = false, Message = "Error adding event detail." });
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetUserEvents(string userid)
        {
            var isValidId = GuidParserHelper.ParseStringToGuid(userid);

            if (isValidId)
            {
                var userId = GuidParserHelper.StringToGuid(userid);

                var events = await _eventService.GetEventsAsync(userId);

                return Ok(events);
            }

            return BadRequest(new ResponseModel { Status = false, Message = "Invalid user id." });
        }
    }
}