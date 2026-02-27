using System.Text;
using EntityLayer.Concrete;
using Hotelier.DAL.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HotelWebAPI.Infrastructure.Extensions
{
    public static class RegisterExtension
    {
        public static void AddDatabaseService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApiContext>(opt =>
                {
                    opt.UseSqlServer(configuration.GetConnectionString("ConnectionString"),
                        t => t.MigrationsAssembly("HotelWebAPI"));
                });
        }
        public static void AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, Role>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 0;
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<ApiContext>()
            .AddDefaultTokenProviders();

        }

        public static void AddCorsService(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("HotelApi", opts =>
                {
                    opts.WithOrigins("https://localhost:7191")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        public static void AddJWTService(this IServiceCollection services, IConfiguration configuration)
        {

            var jwt = configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwt["Key"]!);



            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwt["Issuer"],

                    ValidateAudience = true,
                    ValidAudience = jwt["Audience"],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(30)
                };
            });

        }
    }
}