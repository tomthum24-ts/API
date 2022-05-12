using API.Common;
using API.Data;
using API.InterFace;
using API.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, MethodResult<DeleteUserCommandResponse>>
    {
        private readonly AppDbContext _db;


        public DeleteUserCommandHandler(AppDbContext db)
        {
            _db = db;

        }

        public async Task<MethodResult<DeleteUserCommandResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<DeleteUserCommandResponse>();
            var editEntity = await _db.User.Where(x=>request.Ids.Contains(x.id)).ToListAsync(cancellationToken);
            if (editEntity == null)
            {
                return null;
            }
             _db.User.RemoveRange(editEntity);
            await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return methodResult;
        }
    }
}
