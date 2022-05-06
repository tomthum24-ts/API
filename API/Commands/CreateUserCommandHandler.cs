using API.Common;
using API.Data;
using API.Domain;
using API.DomainObjects;
using API.Models;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, MethodResult<CreateUserCommandResponse>>
    {    
        private readonly AppDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public CreateUserCommandHandler(AppDbContext db, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _db = db;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<MethodResult<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateUserCommandResponse>();
            var entity = new UserViewModel
            {
                UserName = request.UserName,
                Name = request.Name,
                Address = request.Address,
                Phone= request.Phone,
                Status=request.Status,
            };
            await _db.User.AddAsync(entity, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            methodResult.Result = _mapper.Map<CreateUserCommandResponse>(entity);
            return methodResult;
        }


    }
}
