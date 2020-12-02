using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaPlanillaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public CategoriaPlanillaController(ApplicationDbContext context, ILogger<CategoriaPlanillaController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetCategorias()
        {
            try
            {
                var categorias = await context.CategoriasPlanillas.ToListAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cargar las categorias de planillas.");
                return BadRequest(ex.Message);
            }
        }
    }
}