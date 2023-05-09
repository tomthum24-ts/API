using API.DOMAIN;
using API.DOMAIN.DomainObjects;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE.Repositories
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(IDbContext db) : base(db)
        {
        }
    }
}
