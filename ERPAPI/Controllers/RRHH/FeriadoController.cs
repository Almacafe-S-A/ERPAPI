using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Migrations;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class FeriadoController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public FeriadoController(ApplicationDbContext context, ILogger<FeriadoController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetFeriados()
        {
            try
            {
                var feriados = await context.Feriados.Include(e=>e.Estado).ToListAsync();
                return Ok(feriados);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cargar feriados");
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]/{fecha}")]
        public async Task<ActionResult> GetFeriadoFecha(DateTime fecha)
        {
            try
            {
                var feriados = await context.Feriados.Include(e => e.Estado).Where(f=> f.FechaInicio <= fecha && f.FechaFin >= fecha).ToListAsync();
                return Ok(feriados);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cargar feriados");
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Obtiene el Listado de Feriados 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{periodoId}")]
        public async Task<ActionResult<List<Feriado>>> GetFeriadosByPeriodo(int periodoId)
        {
            List<Feriado> Items = new List<Feriado>();
            try
            {
                Items = await context.Feriados.Where(q => q.PeriodoId == periodoId).ToListAsync();
            }
            catch (Exception ex)
            {

                logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetFeriadoById(Int64 Id)
        {
            Feriado Items = new Feriado();
            try
            {
                Items = await context.Feriados.Where(q => q.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{Name}")]
        public async Task<IActionResult> GetFeriadoByName(String Name)
        {
            Feriado Items = new Feriado();
            try
            {
                Items = await context.Feriados.Where(q => q.Nombre == Name).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta un nuevo Feriado
        /// </summary>
        /// <param name="_Feriado"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Feriado>> Insert([FromBody] Feriado _Feriado)
        {
            Feriado _Feriadoq = new Feriado();
            try
            {
                
                _Feriadoq = _Feriado;
                context.Feriados.Add(_Feriadoq);
                List<Employees> employees = context.Employees.Where(q => q.IdEstado == 1).ToList();

                foreach (var employee in employees)
                {
                    
                        int totalDays = (_Feriadoq.FechaFin - _Feriadoq.FechaInicio).Days;

                        for (int i = 0; i <= totalDays; i++)
                        {
                            DateTime currentDate = _Feriadoq.FechaInicio.AddDays(i);
                            context.ControlAsistencias.Add(new ControlAsistencias
                            {
                                Id = 0,
                                IdEmpleado = employee.IdEmpleado,
                                Fecha = currentDate,
                                TipoAsistencia = 79,
                                Dia = (int)currentDate.DayOfWeek,
                                FechaCreacion = DateTime.Now,
                                UsuarioCreacion = User.Identity.Name,
                                FechaModificacion = DateTime.Now,
                                UsuarioModificacion = User.Identity.Name
                            });
                        }
                }

                // Save changes to the database
                context.SaveChanges();


                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Feriadoq));
        }

        /// <summary>
        /// Update un Feriado
        /// </summary>
        /// <param name="_Feriado"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Feriado>> Update(Feriado _Feriado)
        {
            Feriado _Feriadoq = _Feriado;
            try
            {
                _Feriadoq = await (from c in context.Feriados
                                    .Where(q => q.Id == _Feriado.Id)
                                   select c
                                  ).FirstOrDefaultAsync();
                context.Entry(_Feriadoq).CurrentValues.SetValues((_Feriado));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(_Feriadoq));
        }

        /// <summary>
        /// Elimina un feriado       
        /// </summary>
        /// <param name="_Feriado"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody] Feriado _Feriado)
        {
            Feriado _Feriadoq = new Feriado();
            try
            {
                _Feriadoq = context.Feriados
                .Where(x => x.Id == (Int64)_Feriado.Id)
                .FirstOrDefault();

                context.Feriados.Remove(_Feriadoq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(context, logger, User.Identity.Name).SetAuditor();


                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Feriadoq));

        }
    }
}
