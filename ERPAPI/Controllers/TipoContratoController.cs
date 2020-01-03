/********************************************************************************************************
-- NAME   :  CRUDComision
-- PROPOSE:  show record Comision
REVISIONS:
version              Date                Author                        Description
----------           -------------       ---------------               -------------------------------
6.0                  31/12/2019          Marvin.Guillen                        Changes of validation to delete records
5.0                  26/11/2019          Brayan.Interiano                      Changes of Duplicate
4.0                  16/09/2019          Freddy.Chinchilla                     Changes to Pagination of controller
3.0                  22/07/2019          Cristopher.Arias                      Changes of Tipo de contrato
2.0                  15/07/2019          Freddy.Chinchilla                     Changes of Task return
1.0                  18/09/2019          Mario.Rodrigez                        Creation of Controller
********************************************************************************************************/

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
    [Route("api/TipoContrato")]
    [ApiController]
    public class TipoContratoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public TipoContratoController(ILogger<CountryController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el Listado de TipoContrato paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTipoContratoPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<TipoContrato> Items = new List<TipoContrato>();
            try
            {
                var query = _context.TipoContrato.AsQueryable();
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


        // GET: api/TipoContrato
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<TipoContrato>>> GetTipoContrato()
        {
            return await _context.TipoContrato.ToListAsync();
        }

        // GET: api/TipoContrato/5
        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult<TipoContrato>> GetTipoContrato(long id)
        {
            var tipoContrato = await _context.TipoContrato.FindAsync(id);

            if (tipoContrato == null)
            {
                return NotFound();
            }

            return await Task.Run(() => tipoContrato);
        }
        [HttpGet("[action]/{IdTipoContrato}")]
        public async Task<ActionResult<Int32>> ValidationDelete(Int64 IdTipoContrato)
        {
            try
            {
                //var Items = await _context.Product.CountAsync();
                Int32 Items = await _context.Employees.Where(a => a.IdTipoContrato == IdTipoContrato)
                                    .CountAsync();
                return await Task.Run(() => Ok(Items));


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }
        [HttpGet("[action]/{IdTipoContrato}")]
        public async Task<ActionResult<TipoContrato>> GetTipoContratoById(Int64 IdTipoContrato)
        {
            TipoContrato Items = new TipoContrato();
            try
            {
                Items = await _context.TipoContrato.Where(q => q.IdTipoContrato == IdTipoContrato).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{NombreTipoContrato}")]
        public async Task<ActionResult<TipoContrato>> GetTipoContratoByName(String NombreTipoContrato)
        {
            TipoContrato Items = new TipoContrato();
            try
            {
                Items = await _context.TipoContrato.Where(q => q.NombreTipoContrato == NombreTipoContrato).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Inserta un puesto , y retorna el id generado.
        /// </summary>
        /// <param name="_TipoContrato"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]TipoContrato _TipoContrato)
        {
            TipoContrato tipocontrato = new TipoContrato();
            try
            {
                tipocontrato = _TipoContrato;
                _context.TipoContrato.Add(tipocontrato);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(tipocontrato));
        }

        // POST: api/TipoContrato
        [HttpPost("[action]")]
        public async Task<ActionResult<TipoContrato>> PostTipoContrato(TipoContrato tipoContrato)
        {
            _context.TipoContrato.Add(tipoContrato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoContrato", new { id = tipoContrato.IdTipoContrato }, tipoContrato);
        }

        // DELETE: api/TipoContrato/5
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteTipoContrato([FromBody]TipoContrato _TipoContrato)
        {
            TipoContrato tipocontrato = new TipoContrato();
            try
            {
                tipocontrato = _context.TipoContrato
                   .Where(x => x.IdTipoContrato == (int)_TipoContrato.IdTipoContrato)
                   .FirstOrDefault();
                _context.TipoContrato.Remove(tipocontrato);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(tipocontrato));

        }

        /// <summary>
        /// Actualiza un producto
        /// </summary>
        /// <param name="_TipoContrato"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<TipoContrato>> Update([FromBody]TipoContrato _TipoContrato)
        {
            TipoContrato _Tipocontratoq = _TipoContrato;
            try
            {
                _Tipocontratoq = await (from c in _context.TipoContrato
                                 .Where(q => q.IdTipoContrato == _TipoContrato.IdTipoContrato)
                                   select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Tipocontratoq).CurrentValues.SetValues((_TipoContrato));

                //_context.Escala.Update(_Escalaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Tipocontratoq));
        }
    }
}
