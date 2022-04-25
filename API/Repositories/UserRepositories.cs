using API.Data;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class UserRepositories : IUserRepositories
    {
        private readonly AppDbContext _context;
        public UserRepositories(AppDbContext context)
        {
            _context = context;
        }
        public void CreateUser(UserDTO user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Add(user);
        }

        public  IEnumerable<UserDTO> GetAllUser()
        {
            var data=  _context.Users;
            return data;
        }

        public UserDTO GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChange()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
