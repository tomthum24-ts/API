using API.Common;
using API.Data;
using API.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, MethodResult<DeleteUserCommandResponse>>
    {
        private readonly AppDbContext _db;
        private readonly IUserService _userService;

        public DeleteUserCommandHandler(AppDbContext db, IUserService userService)
        {
            _db = db;
            this._userService = userService;
        }

        public async Task<MethodResult<DeleteUserCommandResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<DeleteUserCommandResponse>();
            var user = await _userService.GetAll().ConfigureAwait(false);
            
            
            return methodResult;
        }
    }
}
