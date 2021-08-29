using CourseProj.Data;
using CourseProj.Data.interfaces;
using CourseProj.Data.Models;
using CourseProj.Data.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;

namespace CourseProj
{
    public class Startup
    {
        private IConfigurationRoot _confstring;

        public Startup(Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment)
        {
            _confstring = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath).AddJsonFile("DBsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DBContent>(options => options.UseSqlServer(_confstring.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/SignIn");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/SignIn");
                });

            AddAllTransient(services);
            services.AddMvc();
            services.AddRazorPages();
        }

        public void AddAllTransient(IServiceCollection services)
        {
            services.AddTransient<IUsers, UserRepository>();
            services.AddTransient<ICollections, CollectionRepository>();
            services.AddTransient<IItems, ItemRepository>();
            services.AddTransient<ILikes, LikesRepository>();
            services.AddTransient<IComments, CommentRepository>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute( name: "default", pattern: "{controller=Users}/{action=MainPage}/{id?}");
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                DBContent content = scope.ServiceProvider.GetRequiredService<DBContent>();
                DBObject.Initial(content);
            }
        }
    }
}
