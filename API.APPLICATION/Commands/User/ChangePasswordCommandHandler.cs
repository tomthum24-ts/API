using API.HRM.DOMAIN;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.EnCrypt;
using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static BaseCommon.Enums.ErrorCodesEnum;

namespace API.APPLICATION.Commands.User
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
                methodResult.AddAPIErrorMessage(nameof(EBaseErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), null)
                    });
                //throw new CommandHandlerException(methodResult.ErrorMessages);
                return methodResult;

            }
            editEntity.SetPassWord(CommonBase.ToMD5(request.Password));

            var a = CommonBase.IsStrongPassword(request.Password, out errorMessage);

            _db.User.Update(editEntity);
            await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false); ;
            return methodResult;
        }
    }
}
