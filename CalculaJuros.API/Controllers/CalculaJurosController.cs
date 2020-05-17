using CalculoJuros.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CalculaJuros.API.Controllers
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class CalculaJurosController : ControllerBase
    {
        private const string mensagemErro = "Erro Interno: Não foi possível se comunicar com a API da taxa de juros";
        private readonly ICalculoJurosService _calculoJurosService;

        public CalculaJurosController(ICalculoJurosService calculoJurosService)
        {
            _calculoJurosService = calculoJurosService;
        }

        /// <summary>
        /// Método Calcula Juros Composto
        /// </summary>
        /// <param name="valorInicial">Valor inicial</param>
        /// <param name="meses">Número de meses utilizado para o calculo</param>   
        /// <response code="200">Resultado</response>
        [HttpGet]
        public async Task<string> GetAsync(double valorInicial = 0, int meses = 0)
        {
            double taxa = await _calculoJurosService.ObterTaxaJurosAsync();
            if (taxa == 0)
                return mensagemErro;

            double resultado = _calculoJurosService.CalcularJurosCompostos(valorInicial, meses, taxa);
            return _calculoJurosService.FormatarValorDoubleParaDuasCasasDecimais(resultado);
        }

       
    }
}