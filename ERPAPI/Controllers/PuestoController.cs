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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Puesto")]
    [ApiController]
    public class PuestoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PuestoController(ILogger<PuestoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Puesto
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Puesto>>> GetPuesto()
        {
            return await _context.Puesto.ToListAsync();
        }

        // GET: api/Puesto/5
        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult<Puesto>> GetPuesto(long id)
        {
            var puesto = await _context.Puesto.FindAsync(id);

            if (puesto == null)
            {
                return NotFound();
            }

            return await Task.Run(() => puesto);
        }

        [HttpGet("[action]/{IdPuesto}")]
        public async Task<IActionResult> GetPuestoById(Int64 IdPuesto)
        {
            Puesto Items = new Puesto();
            try
            {
                Items = await _context.Puesto.Where(q => q.IdPuesto == IdPuesto).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }

        // PUT: api/Puesto/5
        //[HttpPut("[action]")]
        //public async Task<IActionResult> PutPuesto(long id, Puesto puesto)
        //{
        //    if (id != puesto.IdPuesto)
        //    {
        //        return BadRequest();
        //    }
             
        //    _context.Entry(puesto).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PuestoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return await Task.Run(() => NoContent());
        //}

        // POST: api/Puesto
        [HttpPost("[action]")]
        public async Task<ActionResult<Puesto>> PostPuesto(Puesto puesto)
        {
            _context.Puesto.Add(puesto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPuesto", new { id = puesto.IdPuesto }, puesto);
        }


        /// <summary>
        /// Inserta un puesto , y retorna el id generado.
        /// </summary>
        /// <param name="_Puesto"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]Puesto _Puesto)
        {
            Puesto puesto = new Puesto();
            try
            {
                puesto = _Puesto;
                _context.Puesto.Add(puesto);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(puesto));
        }

        /// <summary>
        /// Elimina un producto
        /// </summary>
        /// <param name="_Puesto"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Puesto _Puesto)
        {
            Puesto puesto = new Puesto();
            try
            {
                puesto = _context.Puesto
                   .Where(x => x.IdPuesto == (int)_Puesto.IdPuesto)
                   .FirstOrDefault();
                _context.Puesto.Remove(puesto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(puesto));

        }


        /// <summary>
        /// Actualiza un producto
        /// </summary>
        /// <param name="_Puesto"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Puesto>> Update([FromBody]Puesto _Puesto)
        {
            Puesto _Puestop = _Puesto;
            try
            {
                _Puestop = await (from c in _context.Puesto
                                 .Where(q => q.IdPuesto == _Puesto.IdPuesto)
                                  select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Puestop).CurrentValues.SetValues((_Puesto));

                //_context.Escala.Update(_Escalaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Puestop));
        }
    }
}
