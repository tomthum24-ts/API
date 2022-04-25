using API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace API.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope=app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }
        private static void SeedData(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                //Console.WriteLine("-->  Seeding ....");
                //context.Users.AddRange(
                //    new UserDTO()
                //    {
                //        Id=1,
                //        UserName="Son1",
                //        HoDem="Tran1",
                //        Ten="Son1"
                //    },
                //    new UserDTO()
                //    {
                //        Id = 2,
                //        UserName = "Son2",
                //        HoDem = "Tran2",
                //        Ten = "Son2"
                //    }
                //    );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("-->  Have data");
            }
        }
    }
}
