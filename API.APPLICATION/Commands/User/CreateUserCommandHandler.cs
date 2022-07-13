using API.APPLICATION.Commands.User;
using API.HRM.DOMAIN;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.DataConnect;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, MethodResult<CreateUserCommandResponse>>
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly IUserService _user;
        private readonly UserRepository _userRepository;
        public CreateUserCommandHandler(AppDbContext db, IMapper mapper, IUserService user, UserRepository userRepository)
        {
            _db = db;
            _mapper = mapper;
            _user = user;
            _userRepository = userRepository;
        }
        public async Task<MethodResult<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateUserCommandResponse>();
            var existingUser = await _db.User.Where(x => request.UserName.Contains(x.UserName)).ToListAsync(cancellationToken);
            if (existingUser.Count>0)
            {
                methodResult.AddAPIErrorMessage(nameof(EBaseErrorCode.EB01), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.UserName), request.UserName)
                    });
                return methodResult;
            }
            var createUser = new User(
                 request.UserName,
                 request.Name,
                 request.Address,
                 request.Phone,
                 request.Status
                );
            _userRepository.Add(createUser);
            //await _db.User.AddAsync(createUser, cancellationToken);
            //await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateUserCommandResponse>(createUser);
            return methodResult;
        }


    }
}
