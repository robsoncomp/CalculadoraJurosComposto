using Microsoft.AspNetCore.Mvc;

namespace TaxaJuros.API.Controllers
{
    [ApiController]
    [Route("taxaJuros")]
    public class TaxaJurosController : ControllerBase
    {
        private const string taxaJuros = "0,01";

        [HttpGet]
        public string Get()
        {
            return taxaJuros;
        }
    }
}