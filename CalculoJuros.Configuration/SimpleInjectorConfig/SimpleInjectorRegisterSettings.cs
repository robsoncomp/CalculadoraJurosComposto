using System.Linq;
using CalculoJuros.Application.Interfaces.IServices;
using CalculoJuros.Application.Services;
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
            //Application Services -------
            container.Register<ICalculoJurosService, CalculoJurosService>(Lifestyle.Scoped);
        }
    }
}
