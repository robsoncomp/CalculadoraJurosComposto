using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CalculoJuros.Configuration.SwaggerConfig
{
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var rpath = new OpenApiPaths();
            foreach (var path in swaggerDoc.Paths)
            {
                rpath.Add(path.Key.Replace("v{version}", swaggerDoc.Info.Version), path.Value);
            }

            swaggerDoc.Paths = rpath;
        }
    }
}
