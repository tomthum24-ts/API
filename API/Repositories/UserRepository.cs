using API.Data;
using API.Domain;
using API.DTOs;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class UserRepository : GenericRepository<UserDTO>, IUserRepository
    {
        public UserRepository(AppDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<UserDTO>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(UserRepository));
                return new List<UserDTO>();
            }
        }

        public override async Task<bool> Upsert(UserDTO entity)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.ID == entity.ID)
                                                    .FirstOrDefaultAsync();

                if (existingUser == null)
                    return await Add(entity);

                existingUser.Name = entity.Name;
                existingUser.UserName = entity.UserName;
                existingUser.Address = entity.Address;
                existingUser.Phone=entity.Phone;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(UserRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.GuiID == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(UserRepository));
                return false;
            }
        }
    }
}
