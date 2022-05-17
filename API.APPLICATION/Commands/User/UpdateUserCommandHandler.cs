using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace API.APPLICATION.Commands.User
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, MethodResult<UpdateUserCommandResponse>>
    {
        private readonly AppDbContext _db;
        private readonly HttpClient _client;

        public UpdateUserCommandHandler(AppDbContext db)
        {
            _db = db;
        }
        public async Task<MethodResult<UpdateUserCommandResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateUserCommandResponse>();
            var editEntity = await _db.User.FindAsync(request.id);
            if (editEntity == null || editEntity.id < 0)
            {
                //_client.
                ////HttpRequestMessage mess = new HttpRequestMessage();
                ////var message = String.Format("your id: {0} was not found", request.id);
                ////var errorResponse = mess.CreateErrorResponse(HttpStatusCode.NotFound, message);
                //HttpResponseMessage errorResponse = new HttpResponseMessage(HttpStatusCode.NotFound);
                //throw new HttpResponseException(errorResponse, );

                return null;
            }
            editEntity.SetName(request.Name);
            editEntity.SetUserName(request.UserName);
            editEntity.SetAddress(request.Address);
            editEntity.SetPhone(request.Phone);
            editEntity.SetStatus(request.Status);
            _db.User.Update(editEntity);
            await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false); ;
            return methodResult;
        }
    }
}
