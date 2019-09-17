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
    [Route("api/CustomerPhones")]
    [ApiController]
    public class CustomerPhonesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CustomerPhonesController(ILogger<CustomerPhonesController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CustomerPhones paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerPhonesPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CustomerPhones> Items = new List<CustomerPhones>();
            try
            {
                var query = _context.CustomerPhones.AsQueryable();
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
        /// Obtiene el Listado de CustomerPhoneses 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerPhones()
        {
            List<CustomerPhones> Items = new List<CustomerPhones>();
            try
            {
                Items = await _context.CustomerPhones.ToListAsync();
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
        public async Task<IActionResult> GetCustomerPhonesByCustomerId(Int64 CustomerId)
        {
            List<CustomerPhones> Items = new List<CustomerPhones>();
            try
            {
                Items = await _context.CustomerPhones.Where(q => q.CustomerId == CustomerId).ToListAsync();
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
        /// Obtiene los Datos de la CustomerPhones por medio del Id enviado.
        /// </summary>
        /// <param name="CustomerPhoneId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerPhoneId}")]
        public async Task<IActionResult> GetCustomerPhonesById(Int64 CustomerPhoneId)
        {
            CustomerPhones Items = new CustomerPhones();
            try
            {
                Items = await _context.CustomerPhones.Where(q => q.CustomerPhoneId == CustomerPhoneId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva CustomerPhones
        /// </summary>
        /// <param name="_CustomerPhones"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerPhones>> Insert([FromBody]CustomerPhones _CustomerPhones)
        {
            CustomerPhones _CustomerPhonesq = new CustomerPhones();
            try
            {
                _CustomerPhonesq = _CustomerPhones;
                _context.CustomerPhones.Add(_CustomerPhonesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CustomerPhonesq));
        }

        /// <summary>
        /// Actualiza la CustomerPhones
        /// </summary>
        /// <param name="_CustomerPhones"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CustomerPhones>> Update([FromBody]CustomerPhones _CustomerPhones)
        {
            CustomerPhones _CustomerPhonesq = _CustomerPhones;
            try
            {
                _CustomerPhonesq = await (from c in _context.CustomerPhones
                                 .Where(q => q.CustomerPhoneId == _CustomerPhones.CustomerPhoneId)
                                          select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CustomerPhonesq).CurrentValues.SetValues((_CustomerPhones));

                //_context.CustomerPhones.Update(_CustomerPhonesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerPhonesq));
        }

        /// <summary>
        /// Elimina una CustomerPhones       
        /// </summary>
        /// <param name="_CustomerPhones"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CustomerPhones _CustomerPhones)
        {
            CustomerPhones _CustomerPhonesq = new CustomerPhones();
            try
            {
                _CustomerPhonesq = _context.CustomerPhones
                .Where(x => x.CustomerPhoneId == (Int64)_CustomerPhones.CustomerPhoneId)
                .FirstOrDefault();

                _context.CustomerPhones.Remove(_CustomerPhonesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerPhonesq));

        }







    }
}