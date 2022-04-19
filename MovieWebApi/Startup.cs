using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoviesManagement.Service.Abstractions;
using MoviesManagement.Service.Implementations;
using MovieWebApi.Infrastructure.Extensions;
using MovieWebApi.PersistenceDB.Context;
using MovieWebApi.PersistenceDB.Seed;
using MovieWebApi.Service.Abstractions;
using MovieWebApi.Service.Implementations;
using MovieWebApi.Service.Models.JWT;
using MovieWebApi.Web.Infrastructure.Extensions;
using MovieWebApi.Web.Mappings;
using MovieWebApi.Web.SwaggerConfigs;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Configuration;
using System.Text;

namespace MoviesManagement.Web
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
            services.AddControllers();
            services.AddServices();
            services.RegisterMaps();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            // ConnectionString
            services.AddDbContext<MovieDbContext>
                    (options => options.UseSqlServer
                    (_configuration.GetConnectionString("DefaultConnection")));

            //JWT Token Configuration
            var JWTConfig = _configuration.GetSection("JWTConfiguration");
            services.Configure<AppSettings>(JWTConfig);

            //JWT Token Authentification
            var appSettings = JWTConfig.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Key);
            services.AddAuthentication(au =>
            {
                au.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                au.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {                    
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddScoped<DbContext, MovieDbContext>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IOrdersServiceAPI, OrdersServiceAPI>();
            services.AddScoped<IMovieService, MovieService>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfig>();
            services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());

            //ToDo

            //services.AddHealthChecks()
            //        .AddDbContextCheck<MovieManagementContext>();

            services.AddControllers().AddFluentValidation(Configuration =>
            {
                Configuration.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(
            options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.AddErrorHandling(); 
            }

            //ToDo
            //app.UseMiddleware<MyExceptionHandlerMiddleware>(); დასამატებელი მაქვს

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseRequestResponse();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHealthChecks("/health"); unckecked later
                endpoints.MapControllers();
            });

            MovieManagementSeed.SeedInitialData(app);
        }
    }
}
