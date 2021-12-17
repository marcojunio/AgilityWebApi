using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilityWeb.Infra.Base.Validator;

namespace AgilityWeb.Infra.Model.User
{
    public class UserEntityValidator : AbstractValidator<UserEntity>
    {
        public static UserEntityValidator Factory() => new UserEntityValidator();

        public override List<string> Validation(UserEntity user)
        {
            if (!string.IsNullOrEmpty(user.Document))
                user.ErrorReason.Add("Document is required.");

            if (!string.IsNullOrEmpty(user.Email))
                user.ErrorReason.Add("E-mail is required.");

            if (!string.IsNullOrEmpty(user.FirstName))
                user.ErrorReason.Add("First name is required.");

            if (!string.IsNullOrEmpty(user.LastName))
                user.ErrorReason.Add("Last name is required.");

            if (!string.IsNullOrEmpty(user.AuthEntity.Login))
                user.ErrorReason.Add("Login is required.");

            if (!string.IsNullOrEmpty(user.AuthEntity.Password))
                user.ErrorReason.Add("Password is required.");

            
            if (user.ErrorReason.Any())
                IsValid = false;

            return user.ErrorReason;
        }
    }
}