using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using ERPAPI.Data;
using ERPAPI.Models;
//using ERPAPI.Models.SyncfusionViewModels;
using Microsoft.AspNetCore.Authorization;
using ERP.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers.Api
{
     [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
   // [Produces("application/json")]
    [ApiController]
    [Route("api/CustomerType")]
    public class CustomerTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CustomerTypeController(ILogger<CustomerController> logger,ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/CustomerType
        [HttpGet]
        public async Task<IActionResult> GetCustomerType()
        {
            try
            {
                List<CustomerType> Items = await _context.CustomerType.ToListAsync();
                //  int Count = Items.Count();
                return Ok(Items);
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
                return Ok(customerType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error: { ex.Message }");
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody]CustomerType payload)
        {
            try
            {
                CustomerType customerType = payload;
                _context.CustomerType.Update(customerType);
                await _context.SaveChangesAsync();
                return Ok(customerType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error: { ex.Message }");
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Remove([FromBody]CustomerType payload)
        {
            try
            {
                CustomerType customerType = _context.CustomerType
               .Where(x => x.CustomerTypeId == (Int64)payload.CustomerTypeId)
               .FirstOrDefault();
                _context.CustomerType.Remove(customerType);
                await _context.SaveChangesAsync();
                return Ok(customerType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error: { ex.Message }");
            }

        }
    }
}