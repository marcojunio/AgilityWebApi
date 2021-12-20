using AgilityWeb.Domain.Base;

namespace AgilityWeb.Domain.Model.User.Authentication
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