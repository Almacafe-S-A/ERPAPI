using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ERPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReportesController : ControllerBase
    {
        private readonly IConfiguration Configuracion;

        public ReportesController(IConfiguration configuracion)
        {
            Configuracion = configuracion;
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<ActionResult<string>> CadenaConexionBD()
        {
            var cadena = Configuracion.GetConnectionString("DefaultConnection");
            return await Task.Run(() => Ok(cadena));
        }
    }
}