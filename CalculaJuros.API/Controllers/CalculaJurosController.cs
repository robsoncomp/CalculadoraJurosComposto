using System;
using System.Net.Http;
using System.Net.Http.Headers;
using CalculaJuros.API.APIConsumer;
using Microsoft.AspNetCore.Mvc;

namespace CalculaJuros.API.Controllers
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class CalculaJurosController : ControllerBase
    {
        /// <summary>
        /// Método Calcula Juros Composto
        /// </summary>
        /// <param name="valorInicial">Valor inicial</param>
        /// <param name="meses">Número de meses utilizado para o calculo</param>   
        /// <response code="200">Resultado</response>
        [HttpGet]
        public string Get(double valorInicial = 0, int meses = 0)
        {
            double taxa = ObterTaxaJuros();
            double resultado = CalcularJurosCompostos(valorInicial, meses, taxa);
            return FormatarValorDoubleParaDuasCasasDecimais(resultado);
        }

        private static double ObterTaxaJuros()
        {
            double resultado;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50401/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var APIJurosClient = new APIJurosClient(client);
                resultado = APIJurosClient.ObterTaxaJuros();
            }
            return resultado;
        }

        private static double CalcularJurosCompostos(double valorInicial, int meses, double taxa) => valorInicial * Math.Pow(1 + taxa, meses);

        private static string FormatarValorDoubleParaDuasCasasDecimais(double num) => (num).ToString("N2");
    }
}