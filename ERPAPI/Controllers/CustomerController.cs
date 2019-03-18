using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using coderush.Data;
using ERPAPI.Models;
//using ERPAPI.Models.SyncfusionViewModels;
using Microsoft.AspNetCore.Authorization;
using ERP.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERPAPI.Controllers.Api
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
   // [Produces("application/json")]
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult> GetCustomer()
        {
            List<Customer> Items = await _context.Customer.ToListAsync(); 
            //  int Count = Items.Count();
            //return Ok(new { Items, Count });
            return Ok(Items);
        }



        [HttpGet("GetCustomerById/{CustomerId}")]
        public async Task<ActionResult> GetCustomerById(Int64 CustomerId)
        {
            Customer Items = await _context.Customer.Where(q=>q.CustomerId== CustomerId).FirstOrDefaultAsync();           
            return Ok(Items);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Customer>> Insert([FromBody]Customer payload)
        {
            Customer customer = payload;
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            return (customer);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<Customer>> Update([FromBody]Customer payload)
        {
            Customer customer = payload;
            _context.Customer.Update(customer);
            await _context.SaveChangesAsync();
            return (customer);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Customer>> Remove([FromBody]Customer payload)
        {
            Customer customer = _context.Customer
                .Where(x => x.CustomerId == (Int64)payload.CustomerId)
                .FirstOrDefault();
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return (customer);

        }
    }
}