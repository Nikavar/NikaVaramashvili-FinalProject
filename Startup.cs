using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersistenceDb.MVC.Context;
using System;
using PersistenceDb.MVC.Seeds;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using MovieWebApi.Domain.POCO;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MovieMVC.Admin
{
    public class Startup
    {        
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            #region OldCode
            //services.AddControllersWithViews();
            services.AddDbContext<MVCDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<MVCDbContext>();

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            });

            //services.AddScoped<DbContext, MVCDbContext>();


            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<MVCDbContext>()
                .AddEntityFrameworkStores<MVCDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

            services.AddMemoryCache();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            services.AddControllersWithViews();

            services.AddHttpClient("config", c =>
            {
                c.BaseAddress = new Uri(_configuration["AdminApiSettings:BaseAddress"]);
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddDbContext<MVCDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnectionString")));

        //    services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<MVCDbContext>()
        //        .AddEntityFrameworkStores<MVCDbContext>()
        //         .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);

        //    services.AddMemoryCache();
        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //    });

        //    services.AddControllersWithViews();
        //}

       
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
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}
