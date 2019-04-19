using MediatR;

namespace Application.Entities.UserEntity.Command.RegisterUser
{
    public class RegisterUserCommand : IRequest<RegisterUserDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}