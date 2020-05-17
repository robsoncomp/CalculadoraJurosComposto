using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TaxaJuros.TestIntegration
{
    public class IntegrationTestC
    {
        [Fact]
        public async Task TesteEndPointCalculoJuros()
        {
            // Arrange
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
            // Add TestServer
            webHost.UseTestServer();
                    webHost.UseStartup<WebApplication41.Startup>();
                });

            // Create and start up the host
            var host = await hostBuilder.StartAsync();

            // Create an HttpClient which is setup for the test host
            var client = host.GetTestClient();

            // Act
            var response = await client.GetAsync("/Home/Test");

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Be("This is a test");

        }
    }
}
