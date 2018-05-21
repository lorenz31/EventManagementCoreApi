using EventManagementCoreApi2.Services.Interface;
using EventManagementCoreApi2.Core.Identity;
using EventManagementCoreApi2.Security.JWT;
using EventManagementCoreApi2.Core.Response;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace EventManagementCoreApi2.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/v1/Token")]
    public class TokenController : Controller
    {
        private IAccountService _accSvc;
        private IConfiguration _config;

        public TokenController(
            IAccountService accSvc,
            IConfiguration config)
        {
            _accSvc = accSvc;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostGenerateToken([FromBody] ApplicationUser obj)
        {
            if (!ModelState.IsValid)
                return Unauthorized();

            var userVerified = await _accSvc.VerifyUserAsync(obj);

            if (userVerified != null)
            {
                var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create(_config.GetSection("JwtSettings:SecurityKey").Value))
                                .AddSubject(obj.Email)
                                .AddIssuer(_config.GetSection("AppConfiguration:Issuer").Value)
                                .AddAudience(_config.GetSection("AppConfiguration:Issuer").Value)
                                //.AddClaim("SellerId", userVerified.Id.ToString())
                                .AddExpiry(10)
                                .Build();

                JwtTokenModel tokenModel = new JwtTokenModel
                {
                    AccessToken = token.Value,
                    UserId = userVerified.Id.ToString()
                };

                return Ok(tokenModel);
            }

            return BadRequest(new ResponseModel { Status = false, Message = "Error obtaining token." });
        }
    }
}