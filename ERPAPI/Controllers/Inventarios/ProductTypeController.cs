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
    [Route("api/ProductType")]
    //[ApiController]
    public class ProductTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ProductTypeController(ILogger<ProductTypeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Obtiene el Listado de ProductType paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductTypePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<ProductType> Items = new List<ProductType>();
            try
            {
                var query = _context.ProductType.AsQueryable();
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

        // GET: api/ProductType
        /// <summary>
        /// Obtiene el listado de tipos de producto.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductType()
        {
            List<ProductType> Items = new List<ProductType>();
            try
            {
                Items = await _context.ProductType.ToListAsync();
                //int Count = Items.Count();

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
        /// <param name="ProductTypeId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ProductTypeId}")]
        public async Task<IActionResult> GetProductTypeById(Int64 ProductTypeId)
        {
            ProductType Items = new ProductType();
            try
            {
                Items = await _context.ProductType.Where(q => q.ProductTypeId == ProductTypeId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta un tipo de producto
        /// </summary>
        /// <param name="_ProductType"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]ProductType _ProductType)
        {
            ProductType productType = new ProductType();
            try
            {
                productType = _ProductType;
                _context.ProductType.Add(productType);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(productType));
        }

        /// <summary>
        /// Actualiza un producto
        /// </summary>
        /// <param name="_ProductType"></param>
        /// <returns></returns>

        [HttpPost("[action]")]
        public async Task<ActionResult<ProductType>> Update([FromBody]ProductType _ProductType)
        {

            try
            {

                ProductType subproductq = (from c in _context.ProductType
                                   .Where(q => q.ProductTypeId == _ProductType.ProductTypeId)
                                          select c
                                    ).FirstOrDefault();

                _context.Entry(subproductq).CurrentValues.SetValues((_ProductType));
                //                _context.SubProduct.Update(_subproduct);
                await _context.SaveChangesAsync();
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(_ProductType));
            //   return Ok(subproduct);
        }

        /// <summary>
        /// Elimina un tipo de producto.
        /// </summary>
        /// <param name="_ProductType"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ProductType _ProductType)
        {
            ProductType productType = new ProductType();
            try
            {
                productType = _context.ProductType
                  .Where(x => x.ProductTypeId == _ProductType.ProductTypeId)
                  .FirstOrDefault();
                  _context.ProductType.Remove(productType);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(productType));

        }


    }
}