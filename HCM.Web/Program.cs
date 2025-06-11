using HCM.Data;
using Microsoft.EntityFrameworkCore;
using HCM.Data.Interceptors;
using Microsoft.AspNetCore.Authentication.Cookies;
using HCM.Web.Clients.Contracts;
using HCM.Web.Clients;

namespace HCM.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Uncomment to apply migrations
            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            //builder.Services.AddDbContext<HcmDbContext>((sp, options) =>
            //options.UseSqlServer(connectionString)
            //.AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>()));
            //builder.Services.AddSingleton<SoftDeleteInterceptor>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddHttpClient<IAuthApiClient, AuthApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7039");
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
