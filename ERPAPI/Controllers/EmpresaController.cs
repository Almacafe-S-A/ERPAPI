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
    [Route("api/Empresa")]
    [ApiController]
    public class EmpresaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EmpresaController(ILogger<EmpresaController> logger, ApplicationDbContext context)
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
            var Empresa = await _context.Empresa.FindAsync(id);

            if (Empresa == null)
            {
                return NotFound();
            }

            return await Task.Run(() => Empresa);
        }

        [HttpGet("[action]/{IdEmpresa}")]
        public async Task<IActionResult> GetEmpresaById(Int64 IdEmpresa)
        {
            Empresa Items = new Empresa();
            try
            {
                Items = await _context.Empresa.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }

        // PUT: api/Empresa/5
        //[HttpPut("[action]")]
        //public async Task<IActionResult> PutEmpresa(long id, Empresa Empresa)
        //{
        //    if (id != Empresa.IdEmpresa)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(Empresa).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmpresaExists(id))
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

        // POST: api/Empresa
        [HttpPost("[action]")]
        public async Task<ActionResult<Empresa>> PostEmpresa(Empresa Empresa)
        {
            _context.Empresa.Add(Empresa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpresa", new { id = Empresa.IdEmpresa }, Empresa);
        }


        /// <summary>
        /// Inserta un Empresa , y retorna el id generado.
        /// </summary>
        /// <param name="_Empresa"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]Empresa _Empresa)
        {
            Empresa Empresa = new Empresa();
            try
            {
                Empresa = _Empresa;
                _context.Empresa.Add(Empresa);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Empresa));
        }

        /// <summary>
        /// Elimina un producto
        /// </summary>
        /// <param name="_Empresa"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Empresa _Empresa)
        {
            Empresa Empresa = new Empresa();
            try
            {
                Empresa = _context.Empresa
                   .Where(x => x.IdEmpresa == (int)_Empresa.IdEmpresa)
                   .FirstOrDefault();
                _context.Empresa.Remove(Empresa);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Empresa));

        }


        /// <summary>
        /// Actualiza un producto
        /// </summary>
        /// <param name="_Empresa"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Empresa>> Update([FromBody]Empresa _Empresa)
        {
            Empresa _Empresap = _Empresa;
            try
            {
                _Empresap = await (from c in _context.Empresa
                                 .Where(q => q.IdEmpresa == _Empresa.IdEmpresa)
                                  select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Empresap).CurrentValues.SetValues((_Empresa));

                //_context.Escala.Update(_Escalaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Empresap));
        }
    }
}
