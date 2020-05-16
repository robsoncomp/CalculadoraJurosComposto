using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace CalculoJuros.Configuration.SwaggerConfig
{
    public static class SwaggerSettings
    {
        public static IServiceCollection AddConfigurationSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var sb = new StringBuilder();
                sb.AppendLine("<strong>Cálculo de Juros Compostos</strong>");
                sb.AppendLine("  Aplicação desenvolvida na plataforna .Net utilizando ASP.NET Core 3.1");

                c.SwaggerDoc("v1",
                            new OpenApiInfo
                            {
                                Title = "API - Calculo de Juros Compostos",
                                Version = "v1",
                                Description = sb.ToString(),
                                Contact = new OpenApiContact
                                {
                                    Name = "Robson Silvestri",
                                    Url = new Uri("https://www.linkedin.com/in/robson-vanderlei-silvestri-5507013a/")
                                }
                            });

                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == docName);
                });

                c.OperationFilter<SwaggerOperationFilter>();
                c.DocumentFilter<SwaggerDocumentFilter>();
                c.SchemaFilter<SwaggerSchemaFilter>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Autorização JWT no cabeçalho usando esquema Bearer. Exemplo: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                 });

                c.DescribeAllParametersInCamelCase();

                var rootApp = new DirectoryInfo(AppContext.BaseDirectory);
                var Files = rootApp.GetFiles("*.xml");

                foreach (FileInfo file in Files)
                {
                    c.IncludeXmlComments(file.FullName, true);
                }
            });

            return services;
        }

        public static IApplicationBuilder AddConfigurationSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "/{documentName}/swagger.json";
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "v1/swagger";
                c.SwaggerEndpoint("/v1/swagger.json", "eSaniagro Confirmação Movimentação V1");
            });

            return app;
        }
    }
}
