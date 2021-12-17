using System;
using AgilityWeb.Domain.Base;
using AgilityWeb.Domain.Model.Authentication;

namespace AgilityWeb.Domain.Model.User
{
    public class UserDto : EntityDtoBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateInsert { get; set; }
        public DateTime? DateEdition { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public AuthDto AuthDto { get; set; }
    }
}