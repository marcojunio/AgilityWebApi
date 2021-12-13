
using AgilityWeb.Domain.User;
using System;

namespace AgilityWeb.Infra.Model.User
{
    public class UserFactoryConverter : FactoryConverterModelToDto<UserEntity, UserDto>
    {
        public static UserFactoryConverter Factory() => new UserFactoryConverter();

        public override UserEntity Set(UserEntity target, UserDto source)
        {
            target.Id = Guid.NewGuid().ToString();
            target.Document = source.Document;
            target.Email = source.Email;
            target.Login = source.Login;
            target.Password = source.Password;

            return target;
        }

        public override UserDto Set(UserDto target, UserEntity source)
        {
            target.Id = source.Id;
            target.Document = source.Document;
            target.Email = source.Email;
            target.Login = source.Login;
            target.Password = source.Password;

            return target;
        }
    }
}
