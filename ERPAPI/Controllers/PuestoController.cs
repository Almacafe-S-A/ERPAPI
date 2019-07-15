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

        // PUT: api/Puesto/5
        [HttpPut("[action]")]
        public async Task<IActionResult> PutPuesto(long id, Puesto puesto)
        {
            if (id != puesto.IdPuesto)
            {
                return BadRequest();
            }
             
            _context.Entry(puesto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuestoExists(id))
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

        // POST: api/Puesto
        [HttpPost("[action]")]
        public async Task<ActionResult<Puesto>> PostPuesto(Puesto puesto)
        {
            _context.Puesto.Add(puesto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPuesto", new { id = puesto.IdPuesto }, puesto);
        }

        // DELETE: api/Puesto/5
        [HttpPost("[action]")]
        public async Task<ActionResult<Puesto>> DeletePuesto(long id)
        {
            var puesto = await _context.Puesto.FindAsync(id);
            if (puesto == null)
            {
                return await Task.Run(() => NotFound());
            }

            _context.Puesto.Remove(puesto);
            await _context.SaveChangesAsync();

            return await Task.Run(() => puesto);
        }

        private bool PuestoExists(long id)
        {
            return _context.Puesto.Any(e => e.IdPuesto == id);
        }
    }
}
