using HCM.Data;
using Microsoft.EntityFrameworkCore;
using HCM.Data.Interceptors;
using HCM.Api.Auth.Utilities;
using HCM.Services.Contracts;
using HCM.Services;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using HCM.Services.Mapping;

namespace HCM.Api.Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddSingleton<SoftDeleteInterceptor>();

            builder.Services.AddDbContext<HcmDbContext>((sp, options) =>
            options.UseSqlServer(connectionString)
            .AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>()));

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!))
                    };
                });

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<HcmProfile>();
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
