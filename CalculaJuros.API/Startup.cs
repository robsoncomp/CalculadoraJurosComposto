using CalculoJuros.Configuration.InitialsConfig;
using CalculoJuros.Configuration.SimpleInjectorConfig;
using CalculoJuros.Configuration.SwaggerConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;


namespace CalculaJuros.API
{
    public class Startup
    {
        private readonly Container _container = new Container();
        public IConfigurationRoot Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            Configuration = env.ConfigurationEnvironmentConfig();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConfigurationServices(Configuration);
            services.AddConfigurationSwaggerGen();
            services.AddConfigurationSimpleInjector(_container);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            _container.InitializeComponents(Configuration, app);

            app.AddConfigurationSwaggerUI();

            app.AddConfigurationApp(env, loggerFactory, Configuration);
        }
    }
}
