using AgilityWeb.Api.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AgilityWeb.Api
{
    public static class StartupAuthentication
    {
        public static void ConfigureAuthentication(IServiceCollection services,TokenConfiguration tokenConfiguration)
        {
            var key = Encoding.ASCII.GetBytes(tokenConfiguration.Key);
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var paramsOptions = options.TokenValidationParameters;
                paramsOptions.ValidAudience = tokenConfiguration.Audience;
                paramsOptions.ValidIssuer = tokenConfiguration.Issuer;
                paramsOptions.ClockSkew = TimeSpan.Zero;
                paramsOptions.IssuerSigningKey = new SymmetricSecurityKey(key);
                paramsOptions.ValidateLifetime = true;
                paramsOptions.ValidateIssuerSigningKey = true;
                paramsOptions.ValidateAudience = true;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }
    }
}
