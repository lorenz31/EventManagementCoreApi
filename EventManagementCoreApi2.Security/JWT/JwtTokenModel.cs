using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagementCoreApi2.Security.JWT
{
    public class JwtTokenModel
    {
        public string UserId { get; set; }
        public string AccessToken { get; set; }
    }
}
