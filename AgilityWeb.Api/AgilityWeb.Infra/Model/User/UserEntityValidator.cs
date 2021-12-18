using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AgilityWeb.Common.Exceptions;
using AgilityWeb.Infra.Base.Validator;

namespace AgilityWeb.Infra.Model.User
{
    public class UserEntityValidator : AbstractValidator<UserEntity>
    {
        public static UserEntityValidator Factory() => new UserEntityValidator();

        public override void Validation(UserEntity user)
        {
            if (!string.IsNullOrEmpty(user.Document))
                Errors.Add("Document is required");

            if (!string.IsNullOrEmpty(user.Email))
                Errors.Add("E-mail is required.");

            if (!string.IsNullOrEmpty(user.FirstName))
                Errors.Add("First name is required.");

            if (!string.IsNullOrEmpty(user.LastName))
                Errors.Add("Last name is required.");

            if (!string.IsNullOrEmpty(user.AuthEntity.Login))
                Errors.Add("Login is required.");

            if (!string.IsNullOrEmpty(user.AuthEntity.Password))
                Errors.Add("Password is required.");


            if (Errors.Any())
            {
                string errors = "";
                Errors.ForEach(s => { errors += $"{s}\t"; });

                IsValid = false;
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, errors);
            }
        }
    }
}