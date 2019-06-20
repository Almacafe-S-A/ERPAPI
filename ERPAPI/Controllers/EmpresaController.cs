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
    [Route("api/Empresa")]
    [ApiController]
    public class EmpresaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EmpresaController(ILogger<CountryController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Empresa
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresa()
        {
            return await _context.Empresa.ToListAsync();
        }

        // GET: api/Empresa/5
        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult<Empresa>> GetEmpresa(long id)
        {
            var empresa = await _context.Empresa.FindAsync(id);

            if (empresa == null)
            {
                return await Task.Run(() => NotFound());
            }

            return await Task.Run(() => empresa);
        }

        // PUT: api/Empresa/5
        [HttpPut("[action]")]
        public async Task<IActionResult> PutEmpresa(long id, Empresa empresa)
        {
            if (id != empresa.IdEmpresa)
            {
                return await Task.Run(() => BadRequest());
            }

            _context.Entry(empresa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaExists(id))
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

        // POST: api/Empresa
        [HttpPost("[action]")]
        public async Task<ActionResult<Empresa>> PostEmpresa(Empresa empresa)
        {
            _context.Empresa.Add(empresa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpresa", new { id = empresa.IdEmpresa }, empresa);
        }

        // DELETE: api/Empresa/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Empresa>> DeleteEmpresa(long id)
        {
            var empresa = await _context.Empresa.FindAsync(id);
            if (empresa == null)
            {
                return await Task.Run(() => NotFound());
            }

            _context.Empresa.Remove(empresa);
            await _context.SaveChangesAsync();

            return await Task.Run(() => empresa);
        }

        private bool EmpresaExists(long id)
        {
            return _context.Empresa.Any(e => e.IdEmpresa == id);
        }
    }
}
