using AgilityWeb.Api.Settings;
using AgilityWeb.Common.Utils.Security;
using AgilityWeb.Common.Utils.Security.Base;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace AgilityWeb.Tests.ProjectCommon.Utils.Security
{
    public class SecurityMethodsTest
    {
        [Fact]
        public void ShouldBeGenerateTokenJwtSucessfull()
        {
            var tokenConfiguration = new TokenConfiguration
            {
                Key = "TOKJSDHFOJKSDHFHSDFDFSDI",
                Audience = "LFDJFPKDFLSÇGSD5",
                Issuer = "dijfsdoifjsdijfsd"
            };

            var configure = new TokenBearerParams 
            { 
                Audience = "344235544515",
                Issuer = "5643242345",
                CreatedDate = DateTime.Now,
                ExpirationDate = DateTime.UtcNow.AddHours(5),
                Roles = new string[] {"Administrator"},
                SecretKey = "dskfmopkfdgjo´sdfsdfgdft54fgh541dsfg51ertyh51ery51sdg51$%%$",
                UniqueName = "user",
                ExtraClaims = new Dictionary<string,string> { {"user","user" },{"email","email" } }
            };

            var security = new SecurityMethods(tokenConfiguration);
            var token = security.GenerateBearerToken(configure);
            Assert.NotNull(token);
        }
    }
}
