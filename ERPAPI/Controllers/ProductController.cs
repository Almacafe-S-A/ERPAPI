using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ProductController(ILogger<ProductController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/Product
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProduct()
        {
            List<Product> Items = new List<Product>();
            try
            {
                Items = await _context.Product.ToListAsync();
                //int Count = Items.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return Ok(Items);
        }



        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]Product _product)
        {
            Product product = new Product();
            try
            {
                product = _product;
                _context.Product.Add(product);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return Ok(product);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]Product _product)
        {
            Product product = new Product();
            try
            {
                product = _product;
                _context.Product.Update(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return Ok(product);
        }

        [HttpPost("[action]")]
        public IActionResult Delete([FromBody]Product _product)
        {
            Product product = new Product();
            try
            {
                product = _context.Product
                   .Where(x => x.ProductId == (int)_product.ProductId)
                   .FirstOrDefault();
                _context.Product.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return Ok(product);

        }
    }
}