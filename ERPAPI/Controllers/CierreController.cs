using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;

namespace ERPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CierreController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CierreController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cierres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CierresAccounting>>> GetCierresAccounting_1()
        {
            return await _context.CierresAccounting_1.ToListAsync();
        }

        // GET: api/Cierres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CierresAccounting>> GetCierresAccounting(DateTime id)
        {
            var cierresAccounting = await _context.CierresAccounting_1.FindAsync(id);

            if (cierresAccounting == null)
            {
                return NotFound();
            }

            return cierresAccounting;
        }

        // PUT: api/Cierres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCierresAccounting(DateTime id, CierresAccounting cierresAccounting)
        {
            if (id != cierresAccounting.FechaCierre)
            {
                return BadRequest();
            }

            _context.Entry(cierresAccounting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CierresAccountingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cierres
        [HttpPost]
        public async Task<ActionResult<CierresAccounting>> PostCierresAccounting(CierresAccounting cierresAccounting)
        {
            _context.CierresAccounting_1.Add(cierresAccounting);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCierresAccounting", new { id = cierresAccounting.FechaCierre }, cierresAccounting);
        }

        // DELETE: api/Cierres/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CierresAccounting>> DeleteCierresAccounting(DateTime id)
        {
            var cierresAccounting = await _context.CierresAccounting_1.FindAsync(id);
            if (cierresAccounting == null)
            {
                return NotFound();
            }

            _context.CierresAccounting_1.Remove(cierresAccounting);
            await _context.SaveChangesAsync();

            return cierresAccounting;
        }

        private bool CierresAccountingExists(DateTime id)
        {
            return _context.CierresAccounting_1.Any(e => e.FechaCierre == id);
        }
    }
}
