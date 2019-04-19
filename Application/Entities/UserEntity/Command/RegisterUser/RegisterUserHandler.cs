using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Entities.UserEntity.Command.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserDto>
    {
        private readonly IAuthRepository _auth;

        public RegisterUserHandler(IAuthRepository auth)
        {
            _auth = auth;
        }
        public async Task<RegisterUserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new User() {
                Email = request.Email,
                UserName = request.Username,
                UserDetail = new UserDetail() {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                }
            };

            var user = await _auth.RegisterUser(newUser, request.Password);

            if (user == null)
                throw new CreationFailureException(nameof(User), "");
            
            return RegisterUserDto.Create(newUser);
        }
    }
}