using Microsoft.AspNetCore.Authentication.JwtBearer;
using AirbnbDiploma.Core.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AirbnbDiploma.Extensions.Service;

public static class AuthConfigurationExtension
{
    public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(opt =>
            {
                var secret = configuration.Get("Jwt:Secret");
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration.Get("Jwt:ValidIssuer"),
                    ValidAudience = configuration.Get("Jwt:ValidAudience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                };
            });
    }
}
