using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OnionArchitecture.Core.Domain.Entities.Auth;
using OnionArchitecture.Core.Services;
using OnionArchitecture.Core.Services.Contracts;
using OnionArchitecture.Infrastructure.Repositories;
using System.Text;

namespace OnionArchitecture.Api.Services.Configuration
{
    public static class ConfigureServices
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICreateAssignmentService, CreateAssignmentService>();
            services.AddScoped<IUpdateAssignmentService, UpdateAssigmentService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services
            .AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt => {
                var key = Encoding.ASCII.GetBytes(configuration["JwtConfig:Secret"]);

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = false
                };
            });
            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                                    .AddEntityFrameworkStores<OnionArchitectureContext>();
        }
    }
}
