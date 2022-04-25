using API.Models;
using System.Collections.Generic;

namespace API.Repositories
{
    public interface IUserRepositories
    {
        bool SaveChange();
        IEnumerable<UserDTO> GetAllUser();
        UserDTO GetById(int id);
        void CreateUser(UserDTO user);
    }
}
