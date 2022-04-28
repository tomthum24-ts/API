using API.Domain;
using API.DTOs;
using API.Models;
using API.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace API.InterFace.User
{
    public interface IUserService 
    {
        Task<UserDTO> GetInfoUserByID(int id);
    }
}
