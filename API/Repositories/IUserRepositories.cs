using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IUserRepositories
    {
        bool SaveChange();
        IEnumerable<UserViewModel> GetAllUser();
        Task< UserViewModel> GetById(int id);
        void CreateUser(UserViewModel user);
    }
}
