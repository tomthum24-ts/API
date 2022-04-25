using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> otp) : base(otp)
        {

        }
        public DbSet<UserViewModel> Users { get; set; }
    }
}
