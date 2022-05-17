using API.HRM.DOMAIN.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE.Services.User
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);
    }
}
