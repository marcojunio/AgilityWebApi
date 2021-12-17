using AgilityWeb.Api.Settings;
using AgilityWeb.Common.Utils.Security;
using AgilityWeb.Common.Utils.Security.Base;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace AgilityWeb.Tests.ProjectCommon.Utils.Security
{
    public class SecurityMethodsTest
    {
        [Fact]
        public void ShouldBeGenerateTokenJwtSucessfull()
        {
            var token = securityMethods.GenerateBearerToken(tokenBearerParams);
            Assert.NotNull(token);
        }

        [Fact]
        public void ShouldBeDecryptJwtTokenSucessfull()
        {
            var token = securityMethods.GenerateBearerToken(tokenBearerParams);
            var infos = securityMethods.DecryptTokenJwt(token);

            var claims = infos.Payload;

            foreach (var claimsKeyGetValue in claims.Keys)
            {
                Assert.NotNull(claims[claimsKeyGetValue]);
            }
        }
        
        
        private static TokenConfiguration tokenConfiguration = new()
        {
            Key = "TOKJSDHFOJKSDHFHSDFDFSDI",
            Audience = "LFDJFPKDFLSÇGSD5",
            Issuer = "dijfsdoifjsdijfsd"
        };

        private static TokenBearerParams tokenBearerParams = new()
        {
            Audience = "344235544515",
            Issuer = "5643242345",
            CreatedDate = DateTime.Now,
            ExpirationDate = DateTime.UtcNow.AddHours(5),
            Roles = new [] {"Administrator"},
            SecretKey = "dskfmopkfdgjo´sdfsdfgdft54fgh541dsfg51ertyh51ery51sdg51$%%$",
            UniqueName = "user",
            ExtraClaims = new Dictionary<string, string> {{"user", "user"}, {"email", "email"}}
        };
        
        private SecurityMethods securityMethods = new(tokenConfiguration);

    }
}