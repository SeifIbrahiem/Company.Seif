using Company.Seif.BLL.Interfaces;
using Company.Seif.BLL.Repositories;
using Company.Seif.DAL.Data.Contexts;
using Company.Seif.PL.Services;
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
            builder.Services.AddDbContext<CompanyDbContext>(
                options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                }
                );  //Allow DI of CompanyDbContext
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


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
