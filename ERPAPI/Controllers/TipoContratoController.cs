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

        // PUT: api/TipoContrato/5
        [HttpPut("[action]")]
        public async Task<IActionResult> PutTipoContrato(long id, TipoContrato tipoContrato)
        {
            if (id != tipoContrato.IdTipoContrato)
            {
                return BadRequest();
            }

            _context.Entry(tipoContrato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoContratoExists(id))
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

        // POST: api/TipoContrato
        [HttpPost("[action]")]
        public async Task<ActionResult<TipoContrato>> PostTipoContrato(TipoContrato tipoContrato)
        {
            _context.TipoContrato.Add(tipoContrato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoContrato", new { id = tipoContrato.IdTipoContrato }, tipoContrato);
        }

        // DELETE: api/TipoContrato/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoContrato>> DeleteTipoContrato(long id)
        {
            var tipoContrato = await _context.TipoContrato.FindAsync(id);
            if (tipoContrato == null)
            {
                return NotFound();
            }

            _context.TipoContrato.Remove(tipoContrato);
            await _context.SaveChangesAsync();

            return await Task.Run(() => tipoContrato);
        }

        private bool TipoContratoExists(long id)
        {
            return _context.TipoContrato.Any(e => e.IdTipoContrato == id);
        }
    }
}
