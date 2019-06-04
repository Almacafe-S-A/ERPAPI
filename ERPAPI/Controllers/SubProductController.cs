using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/SubProduct")]
    public class SubProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public SubProductController(ILogger<SubProductController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Currency
        [HttpGet("[action]")]
        public async Task<ActionResult<SubProduct>> GetSubProduct()
        {
            List<SubProduct> Items = new List<SubProduct>();
            try
            {
                Items = await _context.SubProduct.ToListAsync();
               
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
            //  return Ok(Items);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<SubProduct>> GetSubProductbByProductTypeId(Int64 ProductTypeId)
        {
            List<SubProduct> Items = new List<SubProduct>();
            try
            {
                Items = await _context.SubProduct.Where(q=>q.ProductTypeId== ProductTypeId).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
            //  return Ok(Items);
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<SubProduct>> GetSubProductoByTipoByCustomer([FromBody]CustomerTypeSubProduct _CustomerTypeSubProduct)
        {
            List<SubProduct> Items = new List<SubProduct>();
            try
            {
                List<Int64> SubProductsCustomer = (from c in _context.CustomerProduct
                                                    .Where(q=>q.CustomerId == _CustomerTypeSubProduct.CustomerId)
                                                    select c.SubProductId
                                                    ).ToList();

                Items = await _context.SubProduct
                              .Where(q=> SubProductsCustomer.Contains(q.SubproductId))
                              .Where(q => q.ProductTypeId == _CustomerTypeSubProduct.ProductTypeId).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
            //  
        }




        [HttpPost("[action]")]
        public async Task<ActionResult<SubProduct>> Insert([FromBody]SubProduct _Currency)
        {
            SubProduct subProduct = _Currency;
            try
            {
                _context.SubProduct.Add(subProduct);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(subProduct));
            //  return Ok(currency);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SubProduct>> Update([FromBody]SubProduct _subproduct)
        {
            
            try
            {

                SubProduct subproductq = (from c in _context.SubProduct
                                   .Where(q => q.SubproductId == _subproduct.SubproductId)
                                          select c
                                    ).FirstOrDefault();

                _subproduct.FechaCreacion = subproductq.FechaCreacion;
                _subproduct.UsuarioCreacion = subproductq.UsuarioCreacion;

                _context.SubProduct.Update(_subproduct);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(_subproduct));
            //   return Ok(subproduct);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SubProduct>> Delete([FromBody]SubProduct _Currency)
        {
            SubProduct subproduct = new SubProduct();
            try
            {
                subproduct = _context.SubProduct
               .Where(x => x.SubproductId == _Currency.SubproductId)
               .FirstOrDefault();
                _context.SubProduct.Remove(subproduct);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            // return Ok(currency);
            return await Task.Run(() => Ok(subproduct));
        }


    }
}