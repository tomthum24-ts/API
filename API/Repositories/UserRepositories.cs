using API.Data;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class UserRepositories 
    {
        private readonly AppDbContext _context;
        public UserRepositories(AppDbContext context)
        {
            _context = context;
        }
        public void CreateUser(UserViewModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Add(user);
        }

        public  IEnumerable<UserViewModel> GetAllUser()
        {
            var data=  _context.Users;
            return data;
        }

        public UserViewModel GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.ID == id);
        }

        public bool SaveChange()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
