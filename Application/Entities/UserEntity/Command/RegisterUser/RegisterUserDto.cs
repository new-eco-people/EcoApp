using System;
using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Entities.UserEntity.Command.RegisterUser
{
    public class RegisterUserDto
    {
        public Guid Id { get; set; }

        public static Expression<Func<User, RegisterUserDto>> Projection
        {
            get
            {
                return User => new RegisterUserDto
                {
                    Id = User.Id
                };
            }
        }

        public static RegisterUserDto Create(User user)
        {
            return Projection.Compile().Invoke(user);
        }
    }
}