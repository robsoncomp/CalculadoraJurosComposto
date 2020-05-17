using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CalculoJuros.Application.APIConsumer
{
    public class APIJurosClient
    {
        private HttpClient _client;
        private JsonSerializerOptions _jsonOptions;

        public APIJurosClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<double> ObterTaxaJurosAsync()
        {
            try
            {
                string resultado = string.Empty;
                HttpResponseMessage response = await _client.GetAsync("taxajuros");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string conteudo = response.Content.ReadAsStringAsync().Result;
                    resultado = JsonSerializer.Deserialize<string>(conteudo, _jsonOptions);
                }
                if (!string.IsNullOrEmpty(resultado))
                    return Convert.ToDouble(resultado);
                return 0;

            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}

