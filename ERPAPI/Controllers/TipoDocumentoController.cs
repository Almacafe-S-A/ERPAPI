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
    [Route("api/TipoDocumento")]
    [ApiController]
    public class TipoDocumentoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public TipoDocumentoController(ILogger<CountryController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el Listado de TipoDocumento paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTipoDocumentoPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<TipoDocumento> Items = new List<TipoDocumento>();
            try
            {
                var query = _context.TipoDocumento.AsQueryable();
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

        // GET: api/TipoDocumento
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<TipoDocumento>>> GetTipoDocumento()
        {
            List<TipoDocumento> _tipodocumento = await _context.TipoDocumento.ToListAsync();
            _tipodocumento = (from c in _tipodocumento
                              select new TipoDocumento {
                                   IdTipoDocumento = c.IdTipoDocumento,
                                   IdEstado = c.IdEstado,
                                   NombreTipoDocumento = c.NombreTipoDocumento,
                                    FechaCreacion = c.FechaCreacion,
                              }).ToList();
            return await Task.Run(() => Ok(_tipodocumento));
        }

        // GET: api/TipoDocumento/5
        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult<TipoDocumento>> GetTipoDocumento(long id)
        {
            var tipoDocumento = await _context.TipoDocumento.FindAsync(id);

            if (tipoDocumento == null)
            {
                return NotFound();
            }

            return await Task.Run(() => tipoDocumento);
        }

        // PUT: api/TipoDocumento/5
        [HttpPut("[action]")]
        public async Task<IActionResult> PutTipoDocumento(long id, TipoDocumento tipoDocumento)
        {
            if (id != tipoDocumento.IdTipoDocumento)
            {
                return BadRequest();
            }

            _context.Entry(tipoDocumento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoDocumentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return await Task.Run(() => NoContent());
        }

        // POST: api/TipoDocumento
        [HttpPost("[action]")]
        public async Task<ActionResult<TipoDocumento>> PostTipoDocumento(TipoDocumento tipoDocumento)
        {
            _context.TipoDocumento.Add(tipoDocumento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoDocumento", new { id = tipoDocumento.IdTipoDocumento }, tipoDocumento);
        }

        // DELETE: api/TipoDocumento/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoDocumento>> DeleteTipoDocumento(long id)
        {
            var tipoDocumento = await _context.TipoDocumento.FindAsync(id);
            if (tipoDocumento == null)
            {
                return NotFound();
            }

            _context.TipoDocumento.Remove(tipoDocumento);
            await _context.SaveChangesAsync();

            return await Task.Run(() => tipoDocumento);
        }

        private bool TipoDocumentoExists(long id)
        {
            return _context.TipoDocumento.Any(e => e.IdTipoDocumento == id);
        }
    }
}
