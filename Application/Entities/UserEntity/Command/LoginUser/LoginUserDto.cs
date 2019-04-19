using System;
using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Entities.UserEntity.Command.LoginUser
{
    public class LoginUserDto
    {
        public Guid Id { get; set; }

        public static Expression<Func<User, LoginUserDto>> Projection
        {
            get
            {
                return User => new LoginUserDto
                {
                    Id = User.Id
                };
            }
        }

        public static LoginUserDto Create(User user)
        {
            return Projection.Compile().Invoke(user);
        }
    }
}