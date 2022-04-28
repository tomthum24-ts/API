using API.Common;
using API.Data;
using API.Domain;
using API.DomainObjects;
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
        private readonly User _user;

        public CreateUserCommandHandler(AppDbContext db, IUnitOfWork unitOfWork, User user)
        {
            _db = db;
            _unitOfWork = unitOfWork;
            _user = user;
        }

        //private readonly IUnitOfWork _unitOfWork;
        //private readonly User _user
        //public CreateUserCommandHandler(AppDbContext db, IUnitOfWork unitOfWork)
        //{
        //    _db = db;
        //    _unitOfWork = unitOfWork;
        //}

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
            //await _db.Users.AddAsync(entity, cancellationToken);
            
            //await _unitOfWork.Users.Add(entity);
            return methodResult;
        }


    }
}
