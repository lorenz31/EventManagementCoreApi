using EventManagementCoreApi2.Core.Identity;
using EventManagementCoreApi2.Core.Response;
using EventManagementCoreApi2.Services.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EventManagementCoreApi2.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/v1/Account")]
    public class AccountController : Controller
    {
        private IAccountService _accSvc;

        public AccountController(IAccountService accSvc)
        {
            _accSvc = accSvc;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> PostRegisterUserAsync([FromBody] ApplicationUser obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseModel { Status = false, Message = "Error adding user." });

            var isRegistered = await _accSvc.RegisterUserAsync(obj);

            if (isRegistered)
                return Ok(new ResponseModel { Status = true, Message = "New user registered." });
            else
                return BadRequest(new ResponseModel { Status = false, Message = "Error registering new user." });
        }
    }
}