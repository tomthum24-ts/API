﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DOMAIN.DTOs.User
{
    public class Tokens
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? ExpiresIn { get; set; }

    }
}