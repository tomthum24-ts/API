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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using static API.Enums.ErrorCodesEnum;

namespace API.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, MethodResult<UpdateUserCommandResponse>>
    {
        private readonly AppDbContext _db;
        private IRepositoryWrapper _user;
        private readonly IMapper _mapper;
        //public HttpRequestMessage Request { get; set; }

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
                //HttpRequestMessage mess = new HttpRequestMessage();
                //var message = String.Format("your id: {0} was not found", request.id);
                //var errorResponse = mess.CreateErrorResponse(HttpStatusCode.NotFound, message);
                //throw new HttpResponseException(errorResponse);

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
