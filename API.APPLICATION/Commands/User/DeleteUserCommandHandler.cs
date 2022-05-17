using API.APPLICATION.Commands.User;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.MethodResult;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION
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
            var editEntity = await _db.User.Where(x => request.Ids.Contains(x.id)).ToListAsync(cancellationToken);
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
