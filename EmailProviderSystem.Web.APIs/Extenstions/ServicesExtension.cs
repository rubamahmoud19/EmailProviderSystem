using Castle.Windsor.Installer;
using EmailProviderSystem.Services;
using EmailProviderSystem.Services.Interfaces.IRepositories;
using EmailProviderSystem.Services.Interfaces.IServices;
using EmailProviderSystem.Services.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Text;

namespace EmailProviderSystem.Web.APIs.Extenstions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {

            string StoringDataType = configuration["StoringDataType"] ?? "Filebase";

            if (StoringDataType == "Filebase")
            {
                services.AddSingleton<IDataRepository, FilebaseRepository>();
            }
            else
            {
                services.AddScoped<IDataRepository, DatabaseRepository>();
            }

            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }

        public static IServiceCollection AddSwagerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token as 'Bearer {token}'",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,
        },
        new List<string>()
    }
});
            });

            return services;
        }

        public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration conf)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["Jwt:Key"] ?? string.Empty)),
            ValidIssuer = conf["Jwt:Issuer"],
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
        });
            return services;
        }
    }
}
