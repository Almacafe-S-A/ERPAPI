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
    [Route("api/CustomerProduct")]
    [ApiController]
    public class CustomerProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CustomerProductController(ILogger<CustomerProductController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CustomerProductes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerProduct()
        {
            List<CustomerProduct> Items = new List<CustomerProduct>();
            try
            {
                Items = await _context.CustomerProduct.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }

        /// <summary>
        /// Obtiene los Datos de la CustomerProduct por medio del Id enviado.
        /// </summary>
        /// <param name="CustomerProductId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerProductId}")]
        public async Task<IActionResult> GetCustomerProductById(Int64 CustomerProductId)
        {
            CustomerProduct Items = new CustomerProduct();
            try
            {
                Items = await _context.CustomerProduct.Where(q => q.CustomerProductId == CustomerProductId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva CustomerProduct
        /// </summary>
        /// <param name="_CustomerProduct"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerProduct>> Insert([FromBody]CustomerProduct _CustomerProduct)
        {
            CustomerProduct _CustomerProductq = new CustomerProduct();
            try
            {
                _CustomerProductq = _CustomerProduct;
                _context.CustomerProduct.Add(_CustomerProductq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_CustomerProductq);
        }

        /// <summary>
        /// Actualiza la CustomerProduct
        /// </summary>
        /// <param name="_CustomerProduct"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CustomerProduct>> Update([FromBody]CustomerProduct _CustomerProduct)
        {
            CustomerProduct _CustomerProductq = _CustomerProduct;
            try
            {
                _CustomerProductq = await (from c in _context.CustomerProduct
                                 .Where(q => q.CustomerProductId == _CustomerProduct.CustomerProductId)
                                           select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CustomerProductq).CurrentValues.SetValues((_CustomerProduct));

                //_context.CustomerProduct.Update(_CustomerProductq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_CustomerProductq);
        }

        /// <summary>
        /// Elimina una CustomerProduct       
        /// </summary>
        /// <param name="_CustomerProduct"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CustomerProduct _CustomerProduct)
        {
            CustomerProduct _CustomerProductq = new CustomerProduct();
            try
            {
                _CustomerProductq = _context.CustomerProduct
                .Where(x => x.CustomerProductId == (Int64)_CustomerProduct.CustomerProductId)
                .FirstOrDefault();

                _context.CustomerProduct.Remove(_CustomerProductq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_CustomerProductq);

        }







    }
}