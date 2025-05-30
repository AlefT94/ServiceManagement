using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ServiceManagement.Domain.Enums;
using ServiceManegement.Infrastructure.Authentication;
using ServiceManegement.Infrastructure.Authorization;
using System.Text;

namespace ServiceManegement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
    {
        // Configuração do JWT
        var jwtSettings = new JwtSettings();
        configuration.GetSection("JwtSettings").Bind(jwtSettings);
        services.AddSingleton(jwtSettings);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            };
        });

        ///TODO Add authorization
        /*services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicies.AdminOnly, policy =>
                policy.RequireRole(UserRole.Administrator.ToString()));

            options.AddPolicy(AuthorizationPolicies.EmployeeAccess, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(UserRole.Administrator.ToString()) ||
                    context.User.IsInRole(UserRole.Employee.ToString())));

            options.AddPolicy(AuthorizationPolicies.CompanyAccess, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(UserRole.Administrator.ToString()) ||
                    context.User.IsInRole(UserRole.Company.ToString())));

            options.AddPolicy(AuthorizationPolicies.AllUsers, policy =>
                policy.RequireAuthenticatedUser());
        });*/

        services.AddScoped<ITokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
