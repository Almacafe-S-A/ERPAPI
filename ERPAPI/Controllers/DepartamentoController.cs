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

        /// <summary>
        /// Obtiene el Listado de Departamento paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Departamento>>> GetDepartamentoPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Departamento> Items = new List<Departamento>();
            try
            {
                var query = _context.Departamento.AsQueryable();
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
        //[HttpPut("[action]")]
        //public async Task<IActionResult> PutDepartamento(long id, Departamento departamento)

        [HttpPut("[action]")]
        public async Task<ActionResult<Departamento>> Update([FromBody]Departamento _Departamento)
        {
            Departamento _Departamentoq = _Departamento;
            try
            {
                _Departamentoq = await (from c in _context.Departamento
                                 .Where(q => q.IdDepartamento == _Departamentoq.IdDepartamento)
                                  select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Departamentoq).CurrentValues.SetValues((_Departamento));

                //_context.Escala.Update(_Escalaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Departamentoq);
        }

        // POST: api/Departamento
        [HttpPost("[action]")]
        public async Task<ActionResult<Escala>> Insert([FromBody]Departamento _Departamento)
        {
            Departamento _Departamentoq = new Departamento();
            try
            {
                _Departamentoq = _Departamento;
                _context.Departamento.Add(_Departamentoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Departamentoq);
        }

        // DELETE: api/Departamento/5
        
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Departamento _Departamento)
        {
            Departamento _Departamentoq = new Departamento();
            try
            {
                _Departamentoq = _context.Departamento
                .Where(x => x.IdDepartamento == (Int64)_Departamento.IdDepartamento)
                .FirstOrDefault();

                _context.Departamento.Remove(_Departamentoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Departamentoq);

        }

        private bool DepartamentoExists(long id)
        {
            return _context.Departamento.Any(e => e.IdDepartamento == id);
        }
    }
}
