using API.Common;
using API.Data;
using API.DomainObjects;
using API.Extension;
using MediatR;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using static API.Enums.ErrorCodesEnum;

namespace API.Commands
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, MethodResult<ChangePasswordCommandResponse>>
    {
        private readonly AppDbContext _db;

        public ChangePasswordCommandHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<MethodResult<ChangePasswordCommandResponse>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<ChangePasswordCommandResponse>();
            var editEntity = await _db.User.FindAsync(request.id);
            string errorMessage = "";
            if (editEntity == null || editEntity.id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EBaseErrorCode.EB01), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), null)
                    });
               // throw new CommandHandlerException(methodResult.ErrorMessages);
                return null;

            }
            editEntity.SetPassWord(CommonBase.ToMD5(request.Password));

            var a = CommonBase.IsStrongPassword(request.Password, out errorMessage);

            _db.User.Update(editEntity);
            await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false); ;
            return methodResult;
        }
    }
}
