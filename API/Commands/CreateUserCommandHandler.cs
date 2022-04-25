using API.Common;
using API.Data;
using API.Domain;
using API.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, MethodResult<CreateUserCommandResponse>>
    {    
        private readonly AppDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserCommandHandler(AppDbContext db, IUnitOfWork unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }

        public async Task<MethodResult<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateUserCommandResponse>();
            var entity = new UserDTO
            {
                UserName = request.UserName,
                HoDem = request.HoDem,
                Ten = request.Ten,
            };
            await _db.Users.AddAsync(entity, cancellationToken);
            //await _unitOfWork.SaveChangesAsync(cancellationToken);
            return methodResult;
        }


    }
}
