using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Permisos")]
    [ApiController]
    public class PermisosController : Controller
    {
        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> ListarPermisos()
        {
            try
            {
                var permisosText = System.IO.File.ReadAllText("PermisosSistema.txt");
                permisosText = permisosText.Replace("\r", "");
                var permisos = permisosText.Split("\n");
                return await Task.Run((() => Ok(permisos)));
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
        }
    }
}