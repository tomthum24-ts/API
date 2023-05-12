using API.APPLICATION.Queries;
using API.DOMAIN.DTOs.Project;
using API.INFRASTRUCTURE.DataConnect;

namespace API.APPLICATION
{
    public interface IProjectServices : IDanhMucQueries<ProjectDTO>
    {
    }
    public class ProjectServices : DanhMucQueries<ProjectDTO>, IProjectServices
    {
        public ProjectServices(DapperContext connectionFactory) : base(connectionFactory)
        {
        }
    }
}
