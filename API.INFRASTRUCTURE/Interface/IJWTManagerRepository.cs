using API.HRM.DOMAIN.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE
{
    public interface IJWTManagerRepository
    {
        Tokens GenerateJWTTokens(Users users);
    }
}
