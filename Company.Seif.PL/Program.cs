using Company.Seif.BLL;
using Company.Seif.BLL.Interfaces;
using Company.Seif.BLL.Repositories;
using Company.Seif.DAL.Data.Contexts;
using Company.Seif.DAL.Models;
using Company.Seif.PL.Mapping;
using Company.Seif.PL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.Seif.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(); //Register Built_in MVC Services
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); //Allow DI of departmentRepository
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();
            builder.Services.AddDbContext<CompanyDbContext>(
                options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                }
                );

            // builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<CompanyDbContext>()
                            .AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie(config =>

            {
                config.LoginPath = "/Acount/SignIn";
            }
            );


            //Allow DI of CompanyDbContext
            builder.Services.AddScoped<IScopedServices, ScopedServices>();
            builder.Services.AddScoped<ITransientServices, TransientServices>();
            builder.Services.AddSingleton<ISingletonServices, SingletonServices>();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
