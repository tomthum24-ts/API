using API.INFRASTRUCTURE.Interface.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.RefreshTooken
{
    public class UpdateRefreshTookenCommandHandler : IRequestHandler<UpdateRefreshTookenCommand, MethodResult<UpdateRefreshTookenCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRefreshTookenCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<MethodResult<UpdateRefreshTookenCommandResponse>> Handle(UpdateRefreshTookenCommand request, CancellationToken cancellationToken)
        {

            throw new System.NotImplementedException();
        }
    }
}
