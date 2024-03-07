using System;

namespace API.DOMAIN.DTOs.User
{
    public class Tokens
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public long ExpiresIn { get; set; }

    }
}
