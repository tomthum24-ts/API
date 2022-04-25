using API.Data;
using API.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Queries
{

    //[TransientDependency(ServiceType = typeof(IUserQueries))]
    public class IUserQueries : BaseRequest, IRequest<IEnumerable<UserDTO>>
    {
        
    }
    public class GetAllUsersQueryHandle : IRequestHandler<IUserQueries, IEnumerable<UserDTO>>
        {
            public async Task<IEnumerable<UserDTO>> Handle(IUserQueries request, CancellationToken cancellationToken)
            {
                return new[]
                {
                    new UserDTO { Id=1,UserName="transon1",HoDem="son1",Ten="Son1"},
                    new UserDTO { Id=2,UserName="transon2",HoDem="son2",Ten="Son2"},
                };
            }
        }
}
