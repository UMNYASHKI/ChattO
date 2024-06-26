﻿using Infrastructure.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Extensions;

public static class JwtExtension
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtIssuer = configuration.GetSection("JwtSettings:Issuer").Get<string>();
        var jwtAudience = configuration.GetSection("JwtSettings:Audience").Get<string>();
        var jwtKey = configuration.GetSection("JwtSettings:Key").Get<string>();

        services.AddScoped<UserSecurityValidation>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
         .AddJwtBearer(options =>
         {
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 ValidIssuer = jwtIssuer,
                 ValidAudience = jwtAudience,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
             };
             options.EventsType = typeof(UserSecurityValidation);
         });

        return services;
    }
}
