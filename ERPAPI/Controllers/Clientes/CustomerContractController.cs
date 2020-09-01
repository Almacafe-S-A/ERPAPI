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
    [Route("api/CustomerContract")]
    [ApiController]
    public class CustomerContractController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CustomerContractController(ILogger<CustomerContractController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CustomerContract paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerContractPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CustomerContract> Items = new List<CustomerContract>();
            try
            {
                var query = _context.CustomerContract.AsQueryable();
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
        /// Obtiene el Listado de CustomerContractes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerContract()
        {
            List<CustomerContract> Items = new List<CustomerContract>();
            try
            {
                Items = await _context.CustomerContract.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        
        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetCustomerContractByCustomerId(Int64 CustomerId)
        {
            List<CustomerContract> Items = new List<CustomerContract>();
            try
            {
                Items = await _context.CustomerContract.Where(q=>q.CustomerId== CustomerId).ToListAsync();
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
        /// Obtiene los Datos de la CustomerContract por medio del Id enviado.
        /// </summary>
        /// <param name="CustomerContractId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerContractId}")]
        public async Task<IActionResult> GetCustomerContractById(Int64 CustomerContractId)
        {
            CustomerContract Items = new CustomerContract();
            try
            {
                Items = await _context.CustomerContract.Where(q => q.CustomerContractId == CustomerContractId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva CustomerContract
        /// </summary>
        /// <param name="_CustomerContract"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerContract>> Insert([FromBody]CustomerContract _CustomerContract)
        {
            CustomerContract _CustomerContractq = new CustomerContract();
            try
            {
                _CustomerContractq = _CustomerContract;
                _context.CustomerContract.Add(_CustomerContractq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerContractq));
        }

        /// <summary>
        /// Actualiza la CustomerContract
        /// </summary>
        /// <param name="_CustomerContract"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CustomerContract>> Update([FromBody]CustomerContract _CustomerContract)
        {
            CustomerContract _CustomerContractq = _CustomerContract;
            try
            {
                _CustomerContractq = await (from c in _context.CustomerContract
                                 .Where(q => q.CustomerContractId == _CustomerContract.CustomerContractId)
                                            select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CustomerContractq).CurrentValues.SetValues((_CustomerContract));

                //_context.CustomerContract.Update(_CustomerContractq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerContractq));
        }

        /// <summary>
        /// Elimina una CustomerContract       
        /// </summary>
        /// <param name="_CustomerContract"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CustomerContract _CustomerContract)
        {
            CustomerContract _CustomerContractq = new CustomerContract();
            try
            {
                _CustomerContractq = _context.CustomerContract
                .Where(x => x.CustomerContractId == (Int64)_CustomerContract.CustomerContractId)
                .FirstOrDefault();

                _context.CustomerContract.Remove(_CustomerContractq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerContractq));

        }







    }
}