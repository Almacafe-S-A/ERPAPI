using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
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
    public class BiometricoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public BiometricoController(ApplicationDbContext context, ILogger<BiometricoController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Guardar(Biometrico biometrico)
        {
            try
            {
                if (biometrico.Detalle == null)
                {
                    throw new Exception("Archivo Biometrico sin Detalle");
                }

                if (biometrico.Id == 0)
                {
                    context.Biometricos.Add(biometrico);
                    await context.SaveChangesAsync();
                    return Ok(biometrico);
                }
                Biometrico registroExistente = await context.Biometricos.Include(d => d.Detalle)
                    .FirstOrDefaultAsync(r => r.Id == biometrico.Id);
                context.Entry(registroExistente).CurrentValues.SetValues(biometrico);
                await context.SaveChangesAsync();
                return Ok(biometrico);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar los registros del biometrico");
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetBiometricos()
        {
            try
            {
                var registros = await context.Biometricos
                    .Include(d => d.Detalle)
                    .Include(e => e.Estado).ToListAsync();
                return Ok(registros);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cargar los registros del biometrico");
                return BadRequest(ex);
            }
        }
    }
}