using HCM.Data;
using Microsoft.EntityFrameworkCore;
using HCM.Data.Interceptors;

namespace HCM.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            //Uncomment to apply migrations

            //builder.Services.AddDbContext<HcmDbContext>((sp, options) =>
            //options.UseSqlServer(connectionString)
            //.AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>()));
            //builder.Services.AddSingleton<SoftDeleteInterceptor>();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
