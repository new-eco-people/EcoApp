using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using MediatR;

namespace Application.Entities.UserEntity.Command.LoginUser
{
    public class LoginUserCommand : IRequest<LoginUserDto>
    {
        public string UserNameEmail { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserDto>
    {
        private readonly IAuthRepository _auth;

        public LoginUserCommandHandler(IAuthRepository auth)
        {
            _auth = auth;
        }
        public async Task<LoginUserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user  = await _auth.LoginUser(request.UserNameEmail, request.Password);

            if (user == null)
                throw new NotFoundException(nameof(User), request.UserNameEmail);

            return LoginUserDto.Create(user);
        }
    }
}