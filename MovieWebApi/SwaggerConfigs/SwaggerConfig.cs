using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebApi.Web.SwaggerConfigs
{
	public class SwaggerConfig : IConfigureOptions<SwaggerGenOptions>
	{
		private readonly IApiVersionDescriptionProvider _provider;

		public SwaggerConfig(IApiVersionDescriptionProvider provider)
		{
			_provider = provider;
		}

		public void Configure(SwaggerGenOptions options)
		{

			foreach (var description in _provider.ApiVersionDescriptions)
			{
				options.SwaggerDoc($"{description.GroupName}", new OpenApiInfo
				{
					Version = description.ApiVersion.ToString(),
					Title = "Movies.ItAcademy.Ge API",
					Description = "Movie Management Project_ ASP.NET Core Web API",
					Contact = new OpenApiContact
					{
						Name = "Nikoloz Varamashvili",
						Email = "varnikoloz@gmail.com"
					}
				}); ;
			}
		}

		private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
		{
			var info = new OpenApiInfo()
			{
				Title = "Movies Management Web API",
				Version = description.ApiVersion.ToString(),
			};

			return info;
		}
	}
}
