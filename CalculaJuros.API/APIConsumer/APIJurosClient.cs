using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace CalculaJuros.API.APIConsumer
{
    public class APIJurosClient
    {
        private HttpClient _client;
        private JsonSerializerOptions _jsonOptions;

        public APIJurosClient(HttpClient client)
        {
            _client = client;        
        }

        public double ObterTaxaJuros()
        {
            string resultado = string.Empty;
            HttpResponseMessage response = _client.GetAsync("taxajuros").Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {               
                string conteudo = response.Content.ReadAsStringAsync().Result;
                resultado = JsonSerializer.Deserialize<string>(conteudo, _jsonOptions);
            }
            else
                Console.WriteLine("Ocorreu erro na consulta a API");

            if (!string.IsNullOrEmpty(resultado))
                return Convert.ToDouble(resultado);

            return 0;
        }
    }
}

