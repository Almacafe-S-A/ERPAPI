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
    [Route("api/Dependientes")]
    [ApiController]
    public class DependientesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DependientesController(ILogger<CountryController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Dependientes
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Dependientes>>> GetDependientes()
        {
            return await _context.Dependientes.ToListAsync();
        }

        // GET: api/Dependientes/5
        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult<Dependientes>> GetDependientes(long id)
        {
            var dependientes = await _context.Dependientes.FindAsync(id);

            if (dependientes == null)
            {
                return await Task.Run(() => NotFound());
            }

            return await Task.Run(() => dependientes);
        }

        // PUT: api/Dependientes/5
        [HttpPut("[action]")]
        public async Task<IActionResult> PutDependientes(long id, Dependientes dependientes)
        {
            if (id != dependientes.IdDependientes)
            {
                return BadRequest();
            }

            _context.Entry(dependientes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DependientesExists(id))
                {
                    return await Task.Run(() => NotFound());
                }
                else
                {
                    throw;
                }
            }

            return await Task.Run(() => NoContent());
        }

        // POST: api/Dependientes
        [HttpPost("[action]")]
        public async Task<ActionResult<Dependientes>> PostDependientes(Dependientes dependientes)
        {
            _context.Dependientes.Add(dependientes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDependientes", new { id = dependientes.IdDependientes }, dependientes);
        }

        // DELETE: api/Dependientes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dependientes>> DeleteDependientes(long id)
        {
            var dependientes = await _context.Dependientes.FindAsync(id);
            if (dependientes == null)
            {
                return await Task.Run(() => NotFound());
            }

            _context.Dependientes.Remove(dependientes);
            await _context.SaveChangesAsync();

            return await Task.Run(() => dependientes);
        }

        private bool DependientesExists(long id)
        {
            return _context.Dependientes.Any(e => e.IdDependientes == id);
        }
    }
}
