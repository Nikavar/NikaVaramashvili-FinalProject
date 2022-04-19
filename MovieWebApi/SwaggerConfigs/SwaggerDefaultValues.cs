using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace MovieWebApi.Web.SwaggerConfigs
{
    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
			var apiDescription = context.ApiDescription;
			operation.Deprecated |= apiDescription.IsDeprecated();

			if (operation.Parameters == null)
				return;

			foreach (var param in operation.Parameters)
			{
				var description = apiDescription.ParameterDescriptions.First(p => p.Name == param.Name);
				if (param.Description == null)
				{
					param.Description = description.ModelMetadata?.Description;
				}

				if (param.Schema.Default == null && description.DefaultValue != null)
				{
					param.Schema.Default = new OpenApiString(description.DefaultValue.ToString());
				}

				param.Required |= description.IsRequired;
			}
		}
    }
}
