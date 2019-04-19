using FluentValidation;

namespace Application.Entities.UserEntity.Command.LoginUser
{
    public class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.UserNameEmail).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}