using System;
using System.Collections.Generic;
using System.Text;

namespace DijiWalk.Entities
{
    public class JWTTokens
    {

        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
