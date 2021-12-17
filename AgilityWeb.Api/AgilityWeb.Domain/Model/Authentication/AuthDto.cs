using AgilityWeb.Domain.Base;
using AgilityWeb.Domain.Model.User;

namespace AgilityWeb.Domain.Model.Authentication
{
    public class AuthDto : EntityDtoBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int? CodeVerification { get; set; }
        public string IdUser { get; set; }
        public UserDto UserEntity { get; set; }
    }
}