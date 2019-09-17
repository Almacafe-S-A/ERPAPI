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
    [Route("api/PayrollEmployee")]
    [ApiController]
    public class PayrollEmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PayrollEmployeeController(ILogger<PayrollController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de PayrollEmployee paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPayrollEmployeePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<PayrollEmployee> Items = new List<PayrollEmployee>();
            try
            {
                var query = _context.PayrollEmployee.AsQueryable();
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
        /// Listado Planillas/Empleado.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPayrollEmployee()
        {
            List<PayrollEmployee> Items = new List<PayrollEmployee>();
            try
            {
                Items = await _context.PayrollEmployee.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene una relación específica.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult<PayrollEmployee>> GetPayrollEmployee(Int64 Id)
        {
            List<PayrollEmployee> Items = new List<PayrollEmployee>();
            try
            {
                Items = await _context.PayrollEmployee.Where(q => q.IdPlanilla == Id).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Actualizar relación.
        /// </summary>
        /// <param name="_payrollEmployee"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<PayrollEmployee>> Update([FromBody]PayrollEmployee _payrollEmployee)
        {
            PayrollEmployee _payrollEmployeeq = _payrollEmployee;
            try
            {
                _payrollEmployeeq = await (from c in _context.PayrollEmployee
                                 .Where(q => q.IdPlanillaempleado == _payrollEmployee.IdPlanillaempleado)
                                   select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_payrollEmployeeq).CurrentValues.SetValues((_payrollEmployee));

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_payrollEmployeeq));
        }

        /// <summary>
        /// Crear relación.
        /// </summary>
        /// <param name="_payrollEmployee"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<PayrollEmployee>> Insert([FromBody]PayrollEmployee _payrollEmployee)
        {
            PayrollEmployee _payrollEmployeeq = new PayrollEmployee();
            try
            {
                _payrollEmployeeq = _payrollEmployee;
                _context.PayrollEmployee.Add(_payrollEmployeeq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_payrollEmployeeq));
        }

        /// <summary>
        /// Elimina relación.
        /// </summary>
        /// <param name="_payrollEmployee"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<PayrollEmployee>> Delete([FromBody]PayrollEmployee _payrollEmployee)
        {
            PayrollEmployee _payrollEmployeeq = new PayrollEmployee();
            try
            {
                _payrollEmployeeq = _context.PayrollEmployee
                .Where(x => x.IdPlanillaempleado == (Int64)_payrollEmployee.IdPlanillaempleado)
                .FirstOrDefault();

                _context.PayrollEmployee.Remove(_payrollEmployeeq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_payrollEmployeeq));
        }
        
    }
}
