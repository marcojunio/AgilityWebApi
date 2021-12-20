using System.Linq;
using System.Net;
using AgilityWeb.Common.Exceptions;
using AgilityWeb.Domain.Base.Validator;
using Newtonsoft.Json;

namespace AgilityWeb.Domain.Model.User
{
    public class UserEntityValidator : AbstractValidator<UserDto>
    {
        public static UserEntityValidator Factory() => new();

        public override void Validation(UserDto user)
        {
            if (string.IsNullOrEmpty(user.Document))
                Errors.Add("document","Document is required");

            if (string.IsNullOrEmpty(user.Email))
                Errors.Add("email","E-mail is required.");

            if (string.IsNullOrEmpty(user.FirstName))
                Errors.Add("firstName","First name is required.");

            if (string.IsNullOrEmpty(user.LastName))
                Errors.Add("lastName","Last name is required.");

            if (string.IsNullOrEmpty(user.AuthDto.Login))
                Errors.Add("login","Login is required.");

            if (string.IsNullOrEmpty(user.AuthDto.Password))
                Errors.Add("password","Password is required.");


            if (Errors.Any())
            {
                IsValid = false;
                var errors = JsonConvert.SerializeObject(Errors);
                throw new HttpStatusCodeException("Mandatory fields not informed.",HttpStatusCode.BadRequest, errors);
            }
        }
    }
}