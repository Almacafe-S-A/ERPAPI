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

namespace ERPAPI.Controllers
{
     [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
   // [Produces("application/json")]
    [ApiController]
    [Route("api/CustomerType")]
    public class CustomerTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CustomerType
        [HttpGet]
        public async Task<IActionResult> GetCustomerType()
        {
            List<CustomerType> Items = await _context.CustomerType.ToListAsync();
          //  int Count = Items.Count();
            return Ok(Items);
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]CustomerType payload)
        {
            CustomerType customerType = payload;
            _context.CustomerType.Add(customerType);
            await _context.SaveChangesAsync();
            return Ok(customerType);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody]CustomerType payload)
        {
            CustomerType customerType = payload;
            _context.CustomerType.Update(customerType);
             await _context.SaveChangesAsync();
            return Ok(customerType);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Remove([FromBody]CustomerType payload)
        {
            CustomerType customerType = _context.CustomerType
                .Where(x => x.CustomerTypeId == (Int64)payload.CustomerTypeId)
                .FirstOrDefault();
            _context.CustomerType.Remove(customerType);
            await _context.SaveChangesAsync();
            return Ok(customerType);

        }
    }
}