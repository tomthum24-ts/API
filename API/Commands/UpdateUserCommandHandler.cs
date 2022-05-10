using API.Common;
using API.Data;
using API.Domain;
using API.DomainObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<CreateUserCommand, MethodResult<CreateUserCommandResponse>>
    {
        private readonly AppDbContext _db;
        protected readonly IEntityRepository<User, int> _user;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(AppDbContext db, IEntityRepository<User, int> user, IUnitOfWork unitOfWork)
        {
            _db = db;
            //_user = _unitOfWork.GetEntityRepository<User, int>();
            _unitOfWork = unitOfWork;
        }


        public async Task<MethodResult<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
