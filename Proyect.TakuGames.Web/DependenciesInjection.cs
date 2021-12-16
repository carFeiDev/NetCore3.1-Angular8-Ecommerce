
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Project.TakuGames.Business;
using Project.TakuGames.Dal;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Dal;
using Project.TakuGames.Model.Domain;
using System;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;

namespace Proyect.TakuGames.Web
{
    /// <summary>
    /// DependenciesInjection
    /// </summary> 
    public static class DependenciesInjection
    {
        /// <summary>
        /// Configura los servicios propios de la app
        /// </summary>
        /// <param name="services">referencia al los servicios</param>
        /// <param name="configuration">referencia al archivo de configuracion</param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("TakuGamesDatabase")));                                 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGamesBusiness, GamesBusiness>();
            services.AddTransient<IGameBusiness, GameBusiness>();
            services.AddTransient<ICartBusiness, CartBusiness>();
            services.AddTransient<IOrderBusiness, OrderBusiness>();
            services.AddTransient<IUserBusiness, UserBusiness>();
            services.AddTransient<IFavoritelistBusiness, FavoritelistBusiness>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                 options.RequireHttpsMetadata = false;
                 options.SaveToken = true;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = configuration["Jwt:Issuer"],
                     ValidAudience = configuration["Jwt:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                     ClockSkew = System.TimeSpan.Zero // override the default clock skew of 5 min
                    };
                 services.AddCors();
            });

            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";
                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"TakuGames {groupName}",
                    Version = groupName,
                    Description = "Takugames API",
                    Contact = new OpenApiContact
                    {
                        Name = "Takugames Project",
                        Email = string.Empty,
                        Url = new Uri("https://google.com/"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);             
            });          

            services.AddAuthorization(config =>
            {
                config.AddPolicy(UserRoles.Admin, Policies.AdminPolicy());
                config.AddPolicy(UserRoles.User, Policies.UserPolicy());
            });
        }
    }
}
