using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalculaJuros.API.Controllers
{
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/showmethecode")]
    [Produces("application/json")]
    [ApiController]
    public class ShowCodeController : ControllerBase
    {
        /// <summary>
        /// Método retorna url de onde encontra-se o código no github
        /// </summary>         
        /// <response code="200">URL Github</response>
        [HttpGet]
        public string Get()
        {          
            return "https://github.com/robsoncomp/";
        }
    }
}