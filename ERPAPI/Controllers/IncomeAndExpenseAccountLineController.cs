using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/IncomeAndExpenseAccountLine")]
    [ApiController]
    public class IncomeAndExpenseAccountLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public IncomeAndExpenseAccountLineController(ILogger<IncomeAndExpenseAccountLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de IncomeAndExpenseAccountLine paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetIncomeAndExpenseAccountLinePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<IncomeAndExpenseAccountLine> Items = new List<IncomeAndExpenseAccountLine>();
            try
            {
                var query = _context.IncomeAndExpenseAccountLine.AsQueryable();
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


        /// <summary>
        /// Obtiene el Listado de IncomeAndExpenseAccountLinees 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetIncomeAndExpenseAccountLine()
        {
            List<IncomeAndExpenseAccountLine> Items = new List<IncomeAndExpenseAccountLine>();
            try
            {
                Items = await _context.IncomeAndExpenseAccountLine.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la IncomeAndExpenseAccountLine por medio del Id enviado.
        /// </summary>
        /// <param name="IncomeAndExpenseAccountLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IncomeAndExpenseAccountLineId}")]
        public async Task<IActionResult> GetIncomeAndExpenseAccountLineById(Int64 IncomeAndExpenseAccountLineId)
        {
            IncomeAndExpenseAccountLine Items = new IncomeAndExpenseAccountLine();
            try
            {
                Items = await _context.IncomeAndExpenseAccountLine.Where(q => q.IncomeAndExpenseAccountLineId == IncomeAndExpenseAccountLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{IncomeAndExpensesAccountId}")]
        public async Task<IActionResult> GetIncomeAndExpensesAccountIdByIncomeAndExpenseAccountLineId(Int64 IncomeAndExpensesAccountId)
        {
            List<IncomeAndExpenseAccountLine> Items = new List<IncomeAndExpenseAccountLine>();
            try
            {
                Items = await _context.IncomeAndExpenseAccountLine.Where(q => q.IncomeAndExpensesAccountId == IncomeAndExpensesAccountId).ToListAsync();

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva IncomeAndExpenseAccountLine
        /// </summary>
        /// <param name="_IncomeAndExpenseAccountLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<IncomeAndExpenseAccountLine>> Insert([FromBody]IncomeAndExpenseAccountLine _IncomeAndExpenseAccountLine)
        {
            IncomeAndExpenseAccountLine _IncomeAndExpenseAccountLineq = new IncomeAndExpenseAccountLine();
            try
            {
                _IncomeAndExpenseAccountLineq = _IncomeAndExpenseAccountLine;
                _context.IncomeAndExpenseAccountLine.Add(_IncomeAndExpenseAccountLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_IncomeAndExpenseAccountLineq));
        }

        /// <summary>
        /// Actualiza la IncomeAndExpenseAccountLine
        /// </summary>
        /// <param name="_IncomeAndExpenseAccountLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<IncomeAndExpenseAccountLine>> Update([FromBody]IncomeAndExpenseAccountLine _IncomeAndExpenseAccountLine)
        {
            IncomeAndExpenseAccountLine _IncomeAndExpenseAccountLineq = _IncomeAndExpenseAccountLine;
            try
            {
                _IncomeAndExpenseAccountLineq = await (from c in _context.IncomeAndExpenseAccountLine
                                 .Where(q => q.IncomeAndExpenseAccountLineId == _IncomeAndExpenseAccountLine.IncomeAndExpenseAccountLineId)
                                                       select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_IncomeAndExpenseAccountLineq).CurrentValues.SetValues((_IncomeAndExpenseAccountLine));

                //_context.IncomeAndExpenseAccountLine.Update(_IncomeAndExpenseAccountLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_IncomeAndExpenseAccountLineq));
        }

        /// <summary>
        /// Elimina una IncomeAndExpenseAccountLine       
        /// </summary>
        /// <param name="_IncomeAndExpenseAccountLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]IncomeAndExpenseAccountLine _IncomeAndExpenseAccountLine)
        {
            IncomeAndExpenseAccountLine _IncomeAndExpenseAccountLineq = new IncomeAndExpenseAccountLine();
            try
            {
                _IncomeAndExpenseAccountLineq = _context.IncomeAndExpenseAccountLine
                .Where(x => x.IncomeAndExpenseAccountLineId == (Int64)_IncomeAndExpenseAccountLine.IncomeAndExpenseAccountLineId)
                .FirstOrDefault();

                _context.IncomeAndExpenseAccountLine.Remove(_IncomeAndExpenseAccountLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_IncomeAndExpenseAccountLineq));

        }







    }
}