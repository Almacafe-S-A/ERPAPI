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
    [Route("api/Payroll")]
    [ApiController]
    public class PayrollController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PayrollController(ILogger<PayrollController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el Listado de Payroll paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPayrollPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Payroll> Items = new List<Payroll>();
            try
            {
                var query = _context.Payroll.AsQueryable();
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
           
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Listado de Planillas
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPayroll()
        {
            List<Payroll> Items = new List<Payroll>();
            try
            {
                Items = await _context.Payroll.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene una planilla por ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult<Payroll>> GetPayroll(Int64 Id)
        {
            Payroll Items = new Payroll();
            try
            {
                Items = await _context.Payroll.Where(q => q.IdPlanilla == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Actualizar planilla
        /// </summary>
        /// <param name="_payroll"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Payroll>> Update([FromBody]Payroll _payroll)
        {
            Payroll _payrollq = _payroll;
            try
            {
                _payrollq = await (from c in _context.Payroll
                                 .Where(q => q.IdPlanilla == _payroll.IdPlanilla)
                                              select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_payrollq).CurrentValues.SetValues((_payroll));
                
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_payrollq));
        }

        /// <summary>
        /// Inserta planilla
        /// </summary>
        /// <param name="_payroll"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Payroll>> Insert([FromBody]Payroll _payroll)
        {
            Payroll _payrollq = new Payroll();
            try
            {
                _payrollq = _payroll;
                _context.Payroll.Add(_payrollq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_payrollq));
        }

        /// <summary>
        /// Eliminar planilla
        /// </summary>
        /// <param name="_payroll"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Payroll>> Delete([FromBody]Payroll _payroll)
        {
            Payroll _payrollq = new Payroll();
            try
            {
                _payrollq = _context.Payroll
                .Where(x => x.IdPlanilla == (Int64)_payroll.IdPlanilla)
                .FirstOrDefault();

                _context.Payroll.Remove(_payrollq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_payrollq));
        }
        
    }
}
