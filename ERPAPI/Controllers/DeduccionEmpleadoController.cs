using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class DeduccionEmpleadoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DeduccionEmpleadoController(ILogger<DeduccionEmpleadoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ActionResult<DeduccionEmpleado>> Guardar([FromBody] DeduccionEmpleado deduccion)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        if (deduccion.Id != 0)
                        {
                            DeduccionEmpleado deduccionExistente =
                                await _context.DeduccionesEmpleados.FirstOrDefaultAsync(d => d.Id == deduccion.Id);
                            if (deduccionExistente == null)
                            {
                                throw new Exception("No existe la deducción del empleado a modificar");
                            }

                            _context.Entry(deduccionExistente).CurrentValues.SetValues(deduccion);
                            await _context.SaveChangesAsync();
                            BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                               {
                                   IdOperacion = deduccionExistente.Id,
                                   DocType = "DeduccionEmpleado",
                                   ClaseInicial =
                                       Newtonsoft.Json.JsonConvert.SerializeObject(deduccionExistente, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                                   Accion = "Update",
                                   FechaCreacion = DateTime.Now,
                                   FechaModificacion = DateTime.Now,
                                   UsuarioCreacion = deduccionExistente.UsuarioCreacion,
                                   UsuarioModificacion = deduccionExistente.UsuarioModificacion,
                                   UsuarioEjecucion = deduccionExistente.UsuarioModificacion,

                               });

                            await _context.SaveChangesAsync();
                            transaction.Commit();
                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError(ex, "Error Guardar Deducción Empleado");
                        return BadRequest(ex);
                    }
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error Guardar Deducción Empleado");
                return BadRequest(ex);
            }
        }
    }
}