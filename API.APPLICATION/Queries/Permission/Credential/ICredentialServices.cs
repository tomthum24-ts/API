using API.DOMAIN.DTOs.Permission;
using API.INFRASTRUCTURE.DataConnect;

namespace API.APPLICATION.Queries
{
    public interface ICredentialServices : IDanhMucQueries<CredentialDTO>
    {
    }

    public class CredentialServices : DanhMucQueries<CredentialDTO>, ICredentialServices
    {
        public CredentialServices(DapperContext connectionFactory) : base(connectionFactory)
        {
        }
    }
}