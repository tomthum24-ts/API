using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE.Repositories.UnitOfWork
{
    public class UnitOfWork //: IUnitOfWork
    {
        ////private readonly ApplicationContext _context;
        //private readonly AppDbContext _context;
        //public UnitOfWork(AppContext context)
        //{
        //    _context = context;
        //    Users = new UserRepository(_context);
        //}
        //public IUserRepository Users { get; private set; }
        //public int Complete()
        //{
        //    return _context.SaveChanges();
        //}
        //public void Dispose()
        //{
        //    _context.Dispose();
        //}
    }
}
