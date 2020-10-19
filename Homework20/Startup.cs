using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Homework20.AuthModel;
using Homework20.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using PersLib.Data;
using PersLib.Interfaces;
using PersLib.Model;

namespace Homework20
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IPers, PersApi>();

            //services.AddHttpClient<IPers, PersApi>();

            // Добавление использования архетектуры MVC
            services.AddMvc(
                mvcOptions =>
                {
                    mvcOptions.EnableEndpointRouting = false;
                });

            services.AddDbContext<DataContext>();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.AllowedForNewUsers = true;
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.HttpOnly = true;
                //opt.Cookie.Expiration = TimeSpan.FromMinutes(1);
                opt.LoginPath = "/Login/Login";
                opt.LogoutPath = "/Login/LogOut";
                opt.SlidingExpiration = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Добавление возможности использования статических файлов
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            // Настройка маршрутизации
            app.UseMvc(
                route =>
                {
                    route.MapRoute("default", "{Controller=Index}/{Action=Index}");
                });
        }
    }
}
