using Castle.Windsor.Installer;
using EmailProviderSystem.Services.DatabaseServices;
using EmailProviderSystem.Services.FilebaseServices;
using EmailProviderSystem.Services.Interfaces;
using EmailProviderSystem.Services.MutualServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
                services.AddSingleton<IEmailService, FilebaseEmailService>();
                services.AddSingleton<IAuthService, FilebaseAuthService>();
                services.AddSingleton<IFileService, FilebaseFileService>();
            }
            else
            {
                services.AddScoped<IEmailService, DatabaseEmailService>();
                services.AddScoped<IAuthService, DatabaseAuthService>();
                services.AddScoped<IFileService, FilebaseFileService>();
            }

            
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
