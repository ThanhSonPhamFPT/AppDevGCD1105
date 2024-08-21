using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Repository;
using WebApp.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebApp.Utility;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDBContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));
			builder.Services.ConfigureApplicationCookie(option =>
			{
				option.LoginPath = $"/Identity/Account/Login";
				option.LogoutPath = $"/Identity/Account/Logout";
				option.AccessDeniedPath = $"/Identity/Account/AccessDenied";
			});

			builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();
			builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
			builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IEmailSender,EmailSendercs>();
            builder.Services.AddRazorPages();
			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.MapRazorPages();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}