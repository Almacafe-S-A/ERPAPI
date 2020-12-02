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

        /// <summary>
        /// Obtiene el Listado de Product paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Product> Items = new List<Product>();
            try
            {
                var query = _context.Product.AsQueryable();
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
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.Product
                            .Where(p => branchlist.Any(b => p.BranchId == b.BranchId))
                             .Include(c => c.Branch)
                              //.OrderByDescending(b => b.ProductId)
                              .ToListAsync();
                }
                else
                {
                    Items = await _context.Product
                             .Include(c => c.Branch)
                              //.OrderByDescending(b => b.ProductId)
                              .ToListAsync();
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el Producto mediante el Id Enviado
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ProductId}")]
        public async Task<IActionResult> GetProductById(Int64 ProductId)
        {
            Product Items = new Product();
            try
            {
                Items = await _context.Product.Include(c => c.Branch)
                                              .Where(q => q.ProductId == ProductId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene los datos del producto con el id enviado
        /// </summary>
        /// <param name="ProductName"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ProductName}")]
        public async Task<ActionResult<Product>> GetProductName(string ProductName)
        {
            Product Items = new Product();
            try
            {
                Items = await _context.Product.Where(q => q.ProductName == ProductName).FirstOrDefaultAsync();
                // int Count = Items.Count();
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
            return await Task.Run(() => Ok(product));
        }

        /// <summary>
        /// Actualiza un producto
        /// </summary>
        /// <param name="_Product"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Product>> Update([FromBody]Product _Product)
        {
            Product _Productq = _Product;
            try
            {
                _Productq = await (from c in _context.Product
                                 .Where(q => q.ProductId == _Product.ProductId)
                                  select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Productq).CurrentValues.SetValues((_Product));

                //_context.Escala.Update(_Escalaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Productq));
        }


        [HttpGet("[action]/{ProductCode}")]
        public async Task<IActionResult> GetProductValidarProductCode(String ProductCode)
        {
            Product Items = new Product();
            try
            {
                Items = await _context.Product.Where(q => q.ProductCode == ProductCode).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }


        /// Elimina un producto
        /// <summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Product>> Delete([FromBody]Product _Product)
        {
            Product product = new Product();
            try
            {
                bool flag = false;
                var VariableVendor = _context.ProformaInvoice.Where(a => a.ProductId == _Product.ProductId)
                                    .FirstOrDefault();
                if (VariableVendor == null)
                {
                    flag = true;
                }

                if (flag)
                {
                    product = _context.Product
                   .Where(x => x.ProductId == (int)_Product.ProductId)
                   .FirstOrDefault();
                    _context.Product.Remove(product);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            
            return await Task.Run(() => Ok(product));

        }



        /// <summary>
        /// Elimina un producto
        /// </summary>
        /// <param name="_product"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async  Task<IActionResult> DeleteProduct([FromBody]Product _product)
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
            return await Task.Run(() => Ok(product));

        }

        // GET: api/Product/GetProductVendorsByProductID
        /// <summary>
        ///   Obtiene el listado de Proveedores por Producto.        
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{ProductId}")]
        public async Task<IActionResult> GetProductVendorsByProductID(Int64 ProductId)
        {
            
            try
            {
                //Items = await _context.ProductRelation.Include(q=>q.Product).Include(q=>q.SubProduct).ToListAsync();
                var Items = await (from c in _context.VendorProduct
                               join d in _context.Vendor on c.VendorId equals d.VendorId where c.ProductId == ProductId
                                   //join e in _context.Product on c.ProductId equals e.ProductId 
                                   select new Vendor
                               {                  
                                   VendorId = d.VendorId,
                                   VendorName = d.VendorName,
                                   Phone = d.Phone,
                                   Address = d.Address


                               }
                               ).ToListAsync();

                return await Task.Run(() => Ok(Items));
                // Items = await _context.ProductRelation.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            
        }
    }
}