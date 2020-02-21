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
    public class HorarioController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public HorarioController(ApplicationDbContext context, ILogger<HorarioController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Guardar(Horario horario)
        {
            try
            {
                if (horario.Id == 0)
                {
                    context.Horarios.Add(horario);
                    await context.SaveChangesAsync();
                    return Ok(horario);
                }
                else
                {
                    Horario horarioExistente = await context.Horarios.FirstOrDefaultAsync(f => f.Id == horario.Id);
                    if (horarioExistente == null)
                    {
                        throw new Exception("Registro de horario a actualizar no existe");
                    }

                    context.Entry(horarioExistente).CurrentValues.SetValues(horario);
                    await context.SaveChangesAsync();
                    return Ok(horarioExistente);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar el registro del horario");
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetHorarios()
        {
            try
            {
                var horarios = await context.Horarios.Include(e => e.Estado).ToListAsync();
                return Ok(horarios);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cargar horarios");
                return BadRequest(ex);
            }
        }
    }
}