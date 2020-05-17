using CalculaJuros.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace TaxaJuros.TestIntegration
{
    public class IntegrationTestC
    {
        [Fact]
        public async Task TesteEndPointCalculoJuros()
        {

            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.UseStartup<Startup>();
                });

            var host = await hostBuilder.StartAsync();

            var client = host.GetTestClient();

            var response = await client.GetAsync("v1/CalculaJuros");

            var conteudo = await response.Content.ReadAsStringAsync();

            string responseString = JsonSerializer.Deserialize<string>(conteudo);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Erro Interno: Não foi possível se comunicar com a API da taxa de juros", responseString);
        }
    }
}
