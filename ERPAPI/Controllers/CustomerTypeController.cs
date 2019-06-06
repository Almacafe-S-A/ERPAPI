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
                return BadRequest($"Ocurrio un error: { ex.Message }");
            }
          
        }

        [HttpGet("[action]")]
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
    }
}