using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using SimpleInjector;

namespace CalculoJuros.Configuration.SimpleInjectorConfig
{
    public static class SimpleInjectorRegisterSettings
    {
        public static Container InitializeComponents(this Container container, IConfiguration Configuration, IApplicationBuilder app)
        {
            container.RegisterTypes();

            app.UseSimpleInjector(container);

            container.Verify();

            return container;
        }

        public static void RegisterTypes(this Container container)
        {
              }
    }
}
