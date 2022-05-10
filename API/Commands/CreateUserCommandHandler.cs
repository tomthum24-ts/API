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

            var createUser = new User(
                   
                 request.UserName,
                 request.Name,
                 request.Address,
                 request.Phone,
                 request.Status
                );

            await _db.User.AddAsync(createUser, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            methodResult.Result = _mapper.Map<CreateUserCommandResponse>(createUser);
            return methodResult;
        }


    }
}
