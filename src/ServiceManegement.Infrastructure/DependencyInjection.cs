﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ServiceManagement.Application.Interfaces;
using ServiceManagement.Domain.Interfaces;
using ServiceManagement.Infrastructure.Authentication;
using ServiceManagement.Infrastructure.Data.Repository;
using ServiceManagement.Infrastructure.Implementations;
using ServiceManagement.Infrastructure.Interceptors;
using ServiceManagement.Infrastructure.Persistence;
using System.Text;

namespace ServiceManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
    {

        services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IUserPasswordHasher, UserPasswordHasher>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICustomEmailSender, EmailSender>();
        services.AddScoped<ISaveChangesInterceptor, BaseEntityInterceptor>();
        services.AddMemoryCache();


        //DbContext configuration
        services.AddDbContext<AppDbContext>((sp,options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            options.AddInterceptors(sp.GetService<ISaveChangesInterceptor>());
        });

        //JWT configurations
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
                ClockSkew = TimeSpan.Zero,
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

        return services;
    }
}
