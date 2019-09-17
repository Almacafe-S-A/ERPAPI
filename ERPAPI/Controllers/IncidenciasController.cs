using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;

using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Incidencias")]
    [ApiController]
    public class IncidenciasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public IncidenciasController(ILogger<IncidenciasController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Incidencias paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetIncidenciasPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Incidencias> Items = new List<Incidencias>();
            try
            {
                var query = _context.Incidencias.AsQueryable();
                var totalRegistro = query.Count();

                Items = await query
                   .Skip(cantidadDeRegistros * (numeroDePagina - 1))
                   .Take(cantidadDeRegistros)
                    .ToListAsync();

                Response.Headers["X-Total-Registros"] = totalRegistro.ToString();
                Response.Headers["X-Cantidad-Paginas"] = ((Int64)Math.Ceiling((double)totalRegistro / cantidadDeRegistros)).ToString();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

           
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Listado de Incidencias.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetIncidencias()
        {
            List<Incidencias> Items = new List<Incidencias>();
            try
            {
                Items = await _context.Incidencias.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene incidencia por ID.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult<Incidencias>> GetIncidencias(Int64 Id)
        {
            Incidencias Items = new Incidencias();
            try
            {
                Items = await _context.Incidencias.Where(q => q.IdIncidencia == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Actualizar incidencia.
        /// </summary>
        /// <param name="_incidencias"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Incidencias>> Update([FromBody]Incidencias _incidencias)
        {
            Incidencias _incidenciasq = _incidencias;
            try
            {
                _incidenciasq = await (from c in _context.Incidencias
                                 .Where(q => q.IdIncidencia == _incidencias.IdIncidencia)
                                   select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_incidenciasq).CurrentValues.SetValues((_incidencias));

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_incidenciasq));
        }

        /// <summary>
        /// Inserta una incidencia.
        /// </summary>
        /// <param name="_incidencias"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Incidencias>> Insert([FromBody]Incidencias _incidencias)
        {
            Incidencias _incidenciasq = new Incidencias();
            try
            {
                _incidenciasq = _incidencias;
                _context.Incidencias.Add(_incidenciasq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_incidenciasq));
        }

        /// <summary>
        /// Eliminar incidencia.
        /// </summary>
        /// <param name="_incidencias"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Incidencias>> DeleteIncidencias([FromBody]Incidencias _incidencias)
        {
            Incidencias _incidenciasq = new Incidencias();
            try
            {
                _incidenciasq = _context.Incidencias
                .Where(x => x.IdIncidencia == (Int64)_incidencias.IdIncidencia)
                .FirstOrDefault();

                _context.Incidencias.Remove(_incidenciasq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_incidenciasq));
        }

        //-----------------------------------------------------------------------------------------//
        
        /// <summary>
        /// Listado de vacaciones por empleado.
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EmpId}")]
        public async Task<IActionResult> GetVacaciones(Int64 EmpId)
        {
            List<Incidencias> Items = new List<Incidencias>();
            try
            {
                Items = await (from i in _context.Incidencias where i.IdEmpleado == EmpId && i.IdTipoIncidencia == 1 select i).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Listado de vacaciones por empleado.
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetIncapacidades(Int64 EmpId)
        {
            List<Incidencias> Items = new List<Incidencias>();
            try
            {
                Items = await (from i in _context.Incidencias where i.IdEmpleado == EmpId && i.IdTipoIncidencia == 2 select i).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

    }
}
