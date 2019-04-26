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
        /// <summary>
        ///   Obtiene el listado de productos.        
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProduct()
        {
            List<Product> Items = new List<Product>();
            try
            {
                Items = await _context.Product.ToListAsync();               
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return Ok(Items);
        }

        /// <summary>
        /// Obtiene el Producto mediante el Id Enviado
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductById(Int64 ProductId)
        {
            Product Items = new Product();
            try
            {
                Items = await _context.Product.Where(q=>q.ProductId== ProductId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return Ok(Items);
        }


        /// <summary>
        /// Inserta un producto , y retorna el id generado.
        /// </summary>
        /// <param name="_product"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]Product _product)
        {
            Product product = new Product();
            try
            {
                product = _product;
                _context.Product.Add(product);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return Ok(product);
        }

        /// <summary>
        /// Actualiza un producto
        /// </summary>
        /// <param name="_product"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async  Task<IActionResult> Update([FromBody]Product _product)
        {
            Product product = new Product();
            try
            {
                product = _product;
                _context.Product.Update(product);
               await  _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return Ok(product);
        }

        /// <summary>
        /// Elimina un producto
        /// </summary>
        /// <param name="_product"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async  Task<IActionResult> Delete([FromBody]Product _product)
        {
            Product product = new Product();
            try
            {
                product = _context.Product
                   .Where(x => x.ProductId == (int)_product.ProductId)
                   .FirstOrDefault();
                _context.Product.Remove(product);
               await _context.SaveChangesAsync();
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