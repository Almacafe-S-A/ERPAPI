using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authorization;
using ERP.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
     [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
   // [Produces("application/json")]
    [ApiController]
    [Route("api/CustomerType")]
    public class CustomerTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CustomerTypeController(ILogger<CustomerTypeController> logger,ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CustomerType paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<ActionResult<List<CustomerType>>> GetCustomerTypePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CustomerType> Items = new List<CustomerType>();
            try
            {
                var query = _context.CustomerType.AsQueryable();
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

        // GET: api/CustomerType
        [HttpGet("Get")]
        public async Task<IActionResult> GetCustomerType()
        {
            try
            {
                List<CustomerType> Items = await _context.CustomerType.ToListAsync();
                //  int Count = Items.Count();
                // return Ok(Items);
                return await Task.Run(() => Ok(Items));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error: { ex.Message }"));
            }
          
        }

        [HttpGet("[action]/{CustomerTypeId}")]
        public async Task<IActionResult> GetCustomerTypeById(Int64 CustomerTypeId)
        {
            try
            {
                CustomerType Items = await _context.CustomerType
                    .Where(q=>q.CustomerTypeId==CustomerTypeId).FirstOrDefaultAsync();
                //  int Count = Items.Count();
                // return Ok(Items);
                return await Task.Run(() => Ok(Items));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error: { ex.Message }");
            }

        }

        [HttpGet("[action]/{CustomerTypeName}")]
        public async Task<IActionResult> GetCustomerTypeByCustomerTypeName(string CustomerTypeName)
        {
            CustomerType Items = new CustomerType();
            try
            {
                Items = await _context.CustomerType.Where(q => q.CustomerTypeName == CustomerTypeName).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]CustomerType payload)
        {
            try
            {
                CustomerType customerType = payload;
                _context.CustomerType.Add(customerType);
                await _context.SaveChangesAsync();
                return await Task.Run(() => Ok(customerType));
                // return Ok(customerType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error: { ex.Message }");
            }
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody]CustomerType _customertype)
        {
            try
            {          

                CustomerType customerTypeq = (from c in _context.CustomerType
                                     .Where(q => q.CustomerTypeId == _customertype.CustomerTypeId)
                                      select c
                                    ).FirstOrDefault();

                _customertype.FechaCreacion = customerTypeq.FechaCreacion;
                _customertype.UsuarioCreacion = customerTypeq.UsuarioCreacion;

                _context.Entry(customerTypeq).CurrentValues.SetValues((_customertype));
              //  _context.CustomerType.Update(_customertype);
                await _context.SaveChangesAsync();
                return await Task.Run(() => Ok(_customertype));
                //return Ok(customerType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error: { ex.Message }");
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CustomerType payload)
        {
            try
            {
                CustomerType customerType = _context.CustomerType
               .Where(x => x.CustomerTypeId == (Int64)payload.CustomerTypeId)
               .FirstOrDefault();
                _context.CustomerType.Remove(customerType);
                await _context.SaveChangesAsync();
                return await Task.Run(() => Ok(customerType));
                //return Ok(customerType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error: { ex.Message }");
            }

        }

        /// <summary>
        /// Elimina la moneda
        /// </summary>
        /// <param name="_CustomerType"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerType>> DeleteCustomerType([FromBody]CustomerType _CustomerType)
        {
            CustomerType customertype = new CustomerType();
            try
            {
                bool flag = true;

                //Customer
                var VariableCustomer = _context.Customer.Where(a => a.CustomerTypeId == _CustomerType.CustomerTypeId)
                                                    .FirstOrDefault();
                if (VariableCustomer != null)
                {
                    flag = false;
                }
                //Customer
                var VariableCustomersOfCustomer = _context.CustomersOfCustomer.Where(a => a.CustomerTypeId == _CustomerType.CustomerTypeId)
                                                    .FirstOrDefault();
                if (VariableCustomersOfCustomer != null)
                {
                    flag = false;
                }

                if (flag)
                {
                    customertype = _context.CustomerType
                   .Where(x => x.CustomerTypeId == (int)_CustomerType.CustomerTypeId)
                   .FirstOrDefault();
                    _context.CustomerType.Remove(customertype);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(customertype));

        }
    }
}