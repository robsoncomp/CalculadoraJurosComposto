﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace CalculaJuros.API.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            try
            {
                var uri = new Uri("/v1/swagger/", UriKind.Relative);
                return Redirect(uri.ToString());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}