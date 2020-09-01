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
    public class TipoBonificacionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public TipoBonificacionController(ApplicationDbContext context, ILogger<TipoBonificacionController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetTiposBonificacion()
        {
            try
            {
                var registros = await context.TiposBonificaciones.Include(r=>r.Estado).ToListAsync();
                return Ok(registros);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,"Error al cargar los tipos de bonificaciones");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Guardar(TipoBonificacion registro)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (string.IsNullOrEmpty(registro.Nombre))
                        {
                            transaction.Rollback();
                            return BadRequest("Nombre de tipo de bonificación no puede estar en blanco");
                        }

                        if (string.IsNullOrEmpty(registro.UsuarioCreacion) ||
                            string.IsNullOrEmpty(registro.UsuarioModificacion))
                        {
                            transaction.Rollback();
                            return BadRequest("Usuario de creación o modificación o pueden estar vacios");
                        }

                        if (registro.Id == 0)
                        {
                            await context.TiposBonificaciones.AddAsync(registro);
                            await context.SaveChangesAsync();
                            transaction.Commit();
                            return Ok(registro);
                        }
                        
                        var registroExistente =
                            await context.TiposBonificaciones.FirstOrDefaultAsync(r => r.Id == registro.Id);
                        if (registroExistente == null)
                        {
                            throw new Exception("Registro a modificar no existe");
                        }

                        context.Entry(registroExistente).CurrentValues.SetValues(registro);
                        await context.SaveChangesAsync();
                        transaction.Commit();
                        return Ok(registroExistente);
                        
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ocurrio un error al momento de guardar el tipo deducción");
                return BadRequest(ex.Message);
            }
        }
    }
}