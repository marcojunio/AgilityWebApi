using AgilityWeb.Domain.Model.Authentication;
using AgilityWeb.Domain.Model.User;
using AgilityWeb.Infra.Base.Factory;
using AgilityWeb.Infra.Model.User;

namespace AgilityWeb.Infra.Model.Authentication
{
    public class AuthFactoryConverter : FactoryConverterModelToDto<AuthEntity,AuthDto>
    {
        public override AuthDto Set(AuthDto target, AuthEntity source)
        {
            if (target.Id != source.Id && source.Id != null)
                target.Id = source.Id;
            
            target.Login = source.Login;
            target.Password = source.Password;
            target.CodeVerification = source.CodeVerification;
            target.IdUser = source.IdUser;
            target.DateEdition = source.DateEdition;
            target.DateInsert = source.DateInsert;


            target.UserEntity = new UserDto()
            {
                Document = source.UserEntity.Document,
                Email = source.UserEntity.Email,
                Id = source.UserEntity.Id,
                DateEdition = source.UserEntity.DateEdition,
                DateInsert = source.UserEntity.DateInsert,
                FirstName = source.UserEntity.FirstName,
                LastName = source.UserEntity.LastName,
            };

            return target;
        }

        public override AuthEntity Set(AuthEntity target, AuthDto source)
        {
            if (target.Id != source.Id && source.Id != null)
                target.Id = source.Id;
            
            target.Login = source.Login;
            target.Password = source.Password;
            target.CodeVerification = source.CodeVerification;
            target.IdUser = source.IdUser;
            target.DateEdition = source.DateEdition;
            target.DateInsert = source.DateInsert;


            target.UserEntity = new UserEntity()
            {
                Document = source.UserEntity.Document,
                Email = source.UserEntity.Email,
                Id = source.UserEntity.Id,
                DateEdition = source.UserEntity.DateEdition,
                DateInsert = source.UserEntity.DateInsert,
                FirstName = source.UserEntity.FirstName,
                LastName = source.UserEntity.LastName,
            };

            return target;
        }
    }
}