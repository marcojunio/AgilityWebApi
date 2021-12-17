using System;
using AgilityWeb.Domain.Model.Authentication;
using AgilityWeb.Domain.Model.User;
using AgilityWeb.Infra.Base.Factory;
using AgilityWeb.Infra.Model.Authentication;

namespace AgilityWeb.Infra.Model.User
{
    public class UserFactoryConverter : FactoryConverterModelToDto<UserEntity, UserDto>
    {
        public static UserFactoryConverter Factory() => new UserFactoryConverter();

        public override UserEntity Set(UserEntity target, UserDto source)
        {
            if (target.Id != source.Id && source.Id != null)
                target.Id = source.Id;

            target.Id = source.Id ?? Guid.NewGuid().ToString();
            target.Document = source.Document;
            target.Email = source.Email;
            target.FirstName = source.FirstName;
            target.LastName = source.LastName;
            target.DateInsert = source.DateInsert;
            target.DateEdition = source.DateEdition;

            var authEntity = new AuthEntity()
            {
                Id = source.AuthDto.Id,
                Login = source.AuthDto.Login,
                Password = source.AuthDto.Password,
                CodeVerification = source.AuthDto.CodeVerification,
                IdUser = source.AuthDto.IdUser,
                DateEdition = source.AuthDto?.DateEdition,
                DateInsert = source.AuthDto?.DateInsert,
            };

            target.AuthEntity = authEntity;

            return target;
        }

        public override UserDto Set(UserDto target, UserEntity source)
        {
            if (target.Id != source.Id && source.Id != null)
                target.Id = source.Id;

            target.Document = source.Document;
            target.Email = source.Email;
            target.FirstName = source.FirstName;
            target.LastName = source.LastName;
            target.DateInsert = source.DateInsert;
            target.DateEdition = source.DateEdition;

            var authDto = new AuthDto
            {
                Id = source.AuthEntity.Id,
                Login = source.AuthEntity.Login,
                Password = source.AuthEntity.Password,
                CodeVerification = source.AuthEntity?.CodeVerification,
                IdUser = source.AuthEntity.IdUser,
                DateEdition = source.AuthEntity?.DateEdition,
                DateInsert = source.AuthEntity?.DateInsert,
            };

            target.AuthDto = authDto;
            
            return target;
        }
    }
}