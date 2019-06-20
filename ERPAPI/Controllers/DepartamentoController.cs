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
    [Route("api/Departamento")]
    [ApiController]
    public class DepartamentoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DepartamentoController(ILogger<CountryController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Departamento
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamento()
        {
            return await _context.Departamento.ToListAsync();
        }

        // GET: api/Departamento/5
        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult<Departamento>> GetDepartamento(long id)
        {
            var departamento = await _context.Departamento.FindAsync(id);

            if (departamento == null)
            {
                return NotFound();
            }

            return departamento;
        }

        // PUT: api/Departamento/5
        [HttpPut("[action]")]
        public async Task<IActionResult> PutDepartamento(long id, Departamento departamento)
        {
            if (id != departamento.IdDepartamento)
            {
                return BadRequest();
            }

            _context.Entry(departamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentoExists(id))
                {
                    return await Task.Run(() => NotFound());
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Departamento
        [HttpPost("[action]")]
        public async Task<ActionResult<Departamento>> PostDepartamento(Departamento departamento)
        {
            _context.Departamento.Add(departamento);
            await _context.SaveChangesAsync();

            return await Task.Run(() => CreatedAtAction("GetDepartamento", new { id = departamento.IdDepartamento }, departamento));
        }

        // DELETE: api/Departamento/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Departamento>> DeleteDepartamento(long id)
        {
            var departamento = await _context.Departamento.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }

            _context.Departamento.Remove(departamento);
            await _context.SaveChangesAsync();

            return departamento;
        }

        private bool DepartamentoExists(long id)
        {
            return _context.Departamento.Any(e => e.IdDepartamento == id);
        }
    }
}
