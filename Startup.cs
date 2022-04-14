using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using MovieWebApi.Domain.POCO;
using MovieWebApi.PersistenceDB.Context;
using PersistenceDb.MVC.Context;
using PersistenceDb.MVC.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBC_Movies.Infrastructure.Extensions;

namespace MovieMVC.General
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Service&Repos
            services.AddServices();
            services.AddRepositories();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //DbContext
            services.AddDbContext<MVCDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnectionString")));

            //Auth
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<MVCDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
            
            services.AddMemoryCache();
            services.AddSession();

            services.Configure<IdentityOptions>(opts => {
                opts.User.RequireUniqueEmail = true;
                opts.SignIn.RequireConfirmedEmail = true;
            });
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            services.AddApiVersioning();
            services.AddControllersWithViews();

            services.AddHealthChecks()
            .AddCheck("Check Health Service", () => HealthCheckResult.Healthy("Healthy"))
            .AddDbContextCheck<MVCDbContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecksUI();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //Seeding
            MovieMVCSeed.SeedData(app);
            MovieMVCSeed.SeedUsersAndRolesAsync(app).Wait();
        }
    }
}
