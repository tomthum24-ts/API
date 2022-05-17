using API.HRM.DOMAIN;
using API.INFRASTRUCTURE.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE
{
    public interface IUserRepository : IRepositoryBase<User>
    {
    }
}
