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


        /// <summary>
        /// Obtiene el Listado de Dependientes paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDependientesPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Dependientes> Items = new List<Dependientes>();
            try
            {
                var query = _context.Dependientes.AsQueryable();
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

        /// <summary>
        /// Obtiene los Datos de la EmployeeDocument por medio del Id enviado.
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EmployeeId}")]
        public async Task<IActionResult> GetDependientesByEmployeeId(Int64 EmployeeId)
        {
            List<Dependientes> Items = new List<Dependientes>();
            try
            {
                Items = await _context.Dependientes.Where(q => q.IdEmpleado == EmployeeId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return Ok(Items);
        }

        // PUT: api/Dependientes/5
        [HttpPut("[action]")]
        public async Task<ActionResult<Dependientes>> Update([FromBody]Dependientes _Dependientes)

        {
            Dependientes _Dependientesq = _Dependientes;
            try
            {
                _Dependientesq = await (from c in _context.Dependientes
                                 .Where(q => q.IdDependientes == _Dependientesq.IdDependientes)
                                        select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Dependientesq).CurrentValues.SetValues((_Dependientes));

                //_context.Escala.Update(_Escalaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Dependientesq);
        }

        // POST: api/Dependientes
        [HttpPost("[action]")]
        public async Task<ActionResult<Dependientes>> Insert([FromBody]Dependientes _Dependientes)
        {
            Dependientes _Dependientesq = new Dependientes();
            try
            {
                _Dependientesq = _Dependientes;
                _context.Dependientes.Add(_Dependientesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Dependientesq);
        }


        // DELETE: api/Dependientes/5
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Dependientes _Dependientes)
        {
            Dependientes _Dependientesq = new Dependientes();
            try
            {
                _Dependientesq = _context.Dependientes
                .Where(x => x.IdDependientes == (Int64)_Dependientes.IdDependientes)
                .FirstOrDefault();

                _context.Dependientes.Remove(_Dependientesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Dependientesq);
        }

        private bool DependientesExists(long id)
        {
            return _context.Dependientes.Any(e => e.IdDependientes == id);
        }
    }
}
