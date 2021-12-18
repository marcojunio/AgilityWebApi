using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AgilityWeb.Common.Utils.Security.Base;
using Microsoft.IdentityModel.Tokens;

namespace AgilityWeb.Common.Utils.Security
{
    /// <summary>
    /// helpers in cryptography and decrypt
    /// </summary>
    public class SecurityMethods
    {
        /// <summary>
        /// Generate token JWT with method Sha256
        /// </summary>
        /// <param name="tokenBearerParams"></param>
        /// <returns></returns>
        public string GenerateBearerToken(TokenBearerParams tokenBearerParams)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenBearerParams.SecretKey);

            var subject = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Name, tokenBearerParams.UniqueName)
                });

            foreach (var role in tokenBearerParams.Roles ?? new string[] { })
            {
                subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var claimsDictionary = (tokenBearerParams.ExtraClaims ?? new Dictionary<string, string>());
            foreach (var claimKey in claimsDictionary.Keys)
            {
                subject.AddClaim(new Claim(claimKey, claimsDictionary[claimKey]));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                IssuedAt = tokenBearerParams.CreatedDate,
                NotBefore = tokenBearerParams.CreatedDate?.AddHours(-1),
                Issuer = tokenBearerParams.Issuer,
                Audience = tokenBearerParams.Audience,
                Subject = subject,
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        /// <summary>
        /// Decrypt token jwt
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public JwtSecurityToken DecryptTokenJwt(string token)
        {
            var jwtSecurityToken = new JwtSecurityTokenHandler();
            var tokenDecrypt = jwtSecurityToken.ReadToken(token);
            var infosToken = tokenDecrypt as JwtSecurityToken;

            return infosToken;
        }


        //
        // public string HashPassword(string pass)
        // {
        //     byte[] buffer = Encoding.ASCII.GetBytes(pass);
        //
        //     var sh1 = new SHA1CryptoServiceProvider();
        //     var hash = sh1.ComputeHash(buffer);
        //
        //     var teste=  KeyDeri
        //     return hash.ToString();
        // }
    }
}