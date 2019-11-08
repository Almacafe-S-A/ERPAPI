using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/ControlAsistencias")]
    //[ApiController]
    public class ControlAsistenciasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ControlAsistenciasController(ILogger<ControlAsistenciasController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de ControlAsistencias paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetControlAsistenciasPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<ControlAsistencias> Items = new List<ControlAsistencias>();
            try
            {
                var query = _context.ControlAsistencias.AsQueryable();
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
        /// Obtiene el Listado de ControlAsistencias.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetControlAsistencias()
        {
            List<ControlAsistencias> Items = new List<ControlAsistencias>();
            try
            {
                Items = await _context.ControlAsistencias.ToListAsync();
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
        /// Obtiene la sucursal mediante el Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetControlAsistenciasById(int Id)
        {
            ControlAsistencias Items = new ControlAsistencias();
            try
            {
                Items = await _context.ControlAsistencias.Where(q => q.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una sucursal
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]ControlAsistencias payload)
        {
            ControlAsistencias ControlAsistencias = new ControlAsistencias();
            try
            {
                ControlAsistencias = payload;
                _context.ControlAsistencias.Add(ControlAsistencias);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(ControlAsistencias));
        }

        /// <summary>
        /// Actualiza una sucursal
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody]ControlAsistencias payload)
        {
            ControlAsistencias ControlAsistencias = payload;
            try
            {
                ControlAsistencias = (from c in _context.ControlAsistencias
                                    .Where(q => q.Id == payload.Id)
                          select c
                                    ).FirstOrDefault();

                payload.FechaCreacion = ControlAsistencias.FechaCreacion;
                payload.UsuarioCreacion = ControlAsistencias.UsuarioCreacion;

                _context.Entry(ControlAsistencias).CurrentValues.SetValues(payload);
                // _context.ControlAsistencias.Update(payload);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(ControlAsistencias));
        }


        /// <summary>
        /// Elimina una sucursal
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ControlAsistencias payload)
        {
            ControlAsistencias ControlAsistencias = new ControlAsistencias();
            try
            {
                ControlAsistencias = _context.ControlAsistencias
               .Where(x => x.Id == (int)payload.Id)
               .FirstOrDefault();
                _context.ControlAsistencias.Remove(ControlAsistencias);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(ControlAsistencias));

        }


    }
}
