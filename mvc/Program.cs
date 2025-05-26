using mvc.Services;

namespace mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DbManager>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();
            builder.Services.AddSignalR();
            builder.Services.AddDistributedMemoryCache();

            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
            app.MapHub<ChatHub>("/chatHub");

            app.Run();
        }
    }
}
