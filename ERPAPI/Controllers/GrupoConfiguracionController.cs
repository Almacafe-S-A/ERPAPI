using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/GrupoConfiguracion")]
    [ApiController]
    public class GrupoConfiguracionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GrupoConfiguracionController(ILogger<GrupoConfiguracionController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de GrupoConfiguracion paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGrupoConfiguracionPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<GrupoConfiguracion> Items = new List<GrupoConfiguracion>();
            try
            {
                var query = _context.GrupoConfiguracion.AsQueryable();
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

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el Listado de GrupoConfiguraciones 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGrupoConfiguracion()
        {
            List<GrupoConfiguracion> Items = new List<GrupoConfiguracion>();
            try
            {
                Items = await _context.GrupoConfiguracion.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la GrupoConfiguracion por medio del Id enviado.
        /// </summary>
        /// <param name="GrupoConfiguracionId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{GrupoConfiguracionId}")]
        public async Task<IActionResult> GetGrupoConfiguracionById(Int64 GrupoConfiguracionId)
        {
            GrupoConfiguracion Items = new GrupoConfiguracion();
            try
            {
                Items = await _context.GrupoConfiguracion.Where(q => q.IdConfiguracion == GrupoConfiguracionId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Obtiene un rol , filtrado por su Nombre.
        /// </summary>
        /// <param name="ConfiguracionName"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ConfiguracionName}")]
        public async Task<ActionResult> GetConfiguracionByName(String ConfiguracionName)
        {
            try
            {
                GrupoConfiguracion Items = await _context.GrupoConfiguracion.Where(q => q.Nombreconfiguracion == ConfiguracionName).FirstOrDefaultAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        

        }

        /// <summary>
        /// Inserta una nueva GrupoConfiguracion
        /// </summary>
        /// <param name="_GrupoConfiguracion"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<GrupoConfiguracion>> Insert([FromBody]GrupoConfiguracion _GrupoConfiguracion)
        {
            GrupoConfiguracion _GrupoConfiguracionq = new GrupoConfiguracion();
            try
            {
                _GrupoConfiguracionq = _GrupoConfiguracion;
                _context.GrupoConfiguracion.Add(_GrupoConfiguracionq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GrupoConfiguracionq));
        }

        /// <summary>
        /// Actualiza la GrupoConfiguracion
        /// </summary>
        /// <param name="_GrupoConfiguracion"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<GrupoConfiguracion>> Update([FromBody]GrupoConfiguracion _GrupoConfiguracion)
        {
            GrupoConfiguracion _GrupoConfiguracionq = _GrupoConfiguracion;
            try
            {
                _GrupoConfiguracionq = await (from c in _context.GrupoConfiguracion
                                 .Where(q => q.IdConfiguracion == _GrupoConfiguracion.IdConfiguracion)
                                        select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_GrupoConfiguracionq).CurrentValues.SetValues((_GrupoConfiguracion));

                //_context.GrupoConfiguracion.Update(_GrupoConfiguracionq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GrupoConfiguracionq));
        }

        /// <summary>
        /// Elimina una GrupoConfiguracion       
        /// </summary>
        /// <param name="_GrupoConfiguracion"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]GrupoConfiguracion _GrupoConfiguracion)
        {
            GrupoConfiguracion _GrupoConfiguracionq = new GrupoConfiguracion();
            try
            {
                _GrupoConfiguracionq = _context.GrupoConfiguracion
                .Where(x => x.IdConfiguracion == (Int64)_GrupoConfiguracion.IdConfiguracion)
                .FirstOrDefault();

                _context.GrupoConfiguracion.Remove(_GrupoConfiguracionq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_GrupoConfiguracionq));

        }







    }
}