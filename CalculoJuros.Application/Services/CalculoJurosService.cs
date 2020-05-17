using CalculoJuros.Application.Interfaces.IServices;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using CalculoJuros.Application.APIConsumer;
using System.Threading.Tasks;

namespace CalculoJuros.Application.Services
{
    public class CalculoJurosService : ICalculoJurosService
    {

        public async Task<double> ObterTaxaJurosAsync()
        {
            double resultado;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50401/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var APIJurosClient = new APIJurosClient(client);
                resultado = await APIJurosClient.ObterTaxaJurosAsync();
            }
            return resultado;
        }

        public double CalcularJurosCompostos(double valorInicial, int meses, double taxa)
        {
            return valorInicial * Math.Pow(1 + taxa, meses);
        }

        public string FormatarValorDoubleParaDuasCasasDecimais(double num) => (num).ToString("N2");

    }
}
