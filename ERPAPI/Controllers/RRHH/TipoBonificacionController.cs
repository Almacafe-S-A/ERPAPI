using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

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
                        // Verifica si ya existe un registro con el mismo nombre en la base de datos
                        bool existeRegistroConMismoNombre = await context.TiposBonificaciones.AnyAsync(t => t.Nombre == registro.Nombre);

                        if (existeRegistroConMismoNombre)
                        {
                            throw new Exception("Ya existe un Tipo de Bonificación registrado con ese nombre.");
                        }

                        if (string.IsNullOrEmpty(registro.Nombre))
                        {
                            transaction.Rollback();
                            return BadRequest("Nombre de tipo de bonificación no puede estar en blanco");
                        }
                        if (registro.Valor <= 0)
                        {
                            transaction.Rollback();
                            return BadRequest("Valor de tipo de bonificación no puede ser menor o igual a 0");
                        }
                        if (string.IsNullOrEmpty(registro.UsuarioCreacion) || string.IsNullOrEmpty(registro.UsuarioModificacion))
                        {
                            transaction.Rollback();
                            return BadRequest("Usuario de creación o modificación no pueden estar vacíos");
                        }

                        if (registro.Id == 0)
                        {
                            await context.TiposBonificaciones.AddAsync(registro);

                            // YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                            new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                            await context.SaveChangesAsync();
                            transaction.Commit();
                            return Ok(registro);
                        }

                        var registroExistente = await context.TiposBonificaciones.FirstOrDefaultAsync(r => r.Id == registro.Id);
                        if (registroExistente == null)
                        {
                            return BadRequest("Registro a modificar no existe");
                        }

                        context.Entry(registroExistente).CurrentValues.SetValues(registro);

                        // YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                        await context.SaveChangesAsync();
                        transaction.Commit();
                        return Ok(registroExistente);

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        logger.LogError(ex, "Ocurrió un error al momento de guardar el tipo de bonificación");
                        return BadRequest(new { errorMessage = ex.Message });
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ocurrió un error al momento de guardar el tipo de bonificación");
                return BadRequest(ex.Message);
            }
        }

    }
}