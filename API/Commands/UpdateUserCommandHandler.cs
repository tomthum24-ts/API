using API.Common;
using API.Data;
using API.Domain;
using API.DomainObjects;
using API.Extension;
using API.InterFace;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using static API.Enums.ErrorCodesEnum;

namespace API.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, MethodResult<UpdateUserCommandResponse>>
    {
        private readonly AppDbContext _db;
        private IRepositoryWrapper _user;
        private readonly IMapper _mapper;
        public UpdateUserCommandHandler(AppDbContext db, IRepositoryWrapper user, IMapper mapper)
        {
            _db = db;
            _user = user;
            _mapper = mapper;
        }

        public async Task<MethodResult<UpdateUserCommandResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateUserCommandResponse>();
            var editEntity = await _db.User.FindAsync(request.id);
            if (editEntity == null || editEntity.id<0)
            {
                //methodResult.AddAPIErrorMessage(nameof(EBaseErrorCode.EB02), new[]
                //{
                //    ErrorHelpers.GenerateErrorResult(nameof(request.id), request.id)
                //});
                //throw new CommandHandlerException(methodResult.ErrorMessages);
                //var message = string.Format("Product with id = {0} not found", request.id);
                //HttpError err = new HttpError(message);
                //return Request.CreateResponse(HttpStatusCode.NotFound, err);

            }
            editEntity.SetName(request.Name);
            editEntity.SetUserName(request.UserName);
            editEntity.SetAddress(request.Address);
            editEntity.SetPhone(request.Phone);
            editEntity.SetStatus(request.Status);
            _db.User.Update(editEntity);
            await _db.SaveChangesAsync(cancellationToken);
            return methodResult;
        }
    }
}
