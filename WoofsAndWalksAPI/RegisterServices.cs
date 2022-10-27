﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WoofsAndWalksAPI.Data;
using WoofsAndWalksAPI.Data.Repositories;
using WoofsAndWalksAPI.Helpers;
using WoofsAndWalksAPI.Interfaces;
using WoofsAndWalksAPI.Services;

namespace WoofsAndWalksAPI;

public static class RegisterServices
{
    public static void ConfigureInjection(this WebApplicationBuilder builder)
    {
        /* Database Context Dependency Injection */
        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors();
        builder.Services.AddScoped<LogUserActivity>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ILikesRepository, LikesRepository>();
        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

        // auth and JWT
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])), 
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

        // Cloudinary
        builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
        builder.Services.AddScoped<IPhotoService, PhotoService>();

    }
}
