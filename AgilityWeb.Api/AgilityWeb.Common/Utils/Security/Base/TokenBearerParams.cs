using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilityWeb.Common.Utils.Security.Base
{
    /// <summary>
    /// Params with generate token jwt
    /// </summary>
    public class TokenBearerParams
    {
        public string UniqueName { get; set; }
        public string SecretKey { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string[] Roles { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public Dictionary<string, string> ExtraClaims { get; set; }
    }
}
