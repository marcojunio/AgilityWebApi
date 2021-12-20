using System;
using System.Threading.Tasks;
using AgilityWeb.Domain.Model.User;
using AgilityWeb.Domain.Model.User.Authentication;
using AgilityWeb.Infra.Model.Authentication;
using AgilityWeb.Infra.Model.User;
using Microsoft.AspNetCore.Mvc;

namespace AgilityWeb.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestControlller : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> teste()
        {
            var teste = new UserDto();
            teste.DateEdition = DateTime.Now;
            teste.DateInsert = DateTime.Now;
            teste.Document = "";
            teste.Email = "fdgdfg";
            teste.FirstName = "dfgdfg";
            teste.LastName = "dfgdfg";
            teste.AuthDto = new AuthDto()
            {
                Login = "dfgfdg",
                Password = "dfgdfgdfg"
            };

            var instance = UserEntityValidator.Factory();
            instance.Validation(teste);
            var result = instance.IsValid;
            
            return Ok(teste);
        }
    }
}