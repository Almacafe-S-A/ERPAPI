using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    [Route("api/ProductRelation")]
    public class ProductRelationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ProductRelationController(ILogger<ProductRelationController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Currency
        [HttpGet("[action]")]
        public async Task<ActionResult<ProductRelation>> GetProductRelation()
        {
            List<ProductRelation> Items = new List<ProductRelation>();
            try
            {
                Items = await _context.ProductRelation.ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
            //  return Ok(Items);
        }

        [HttpGet("[action]/{ProductId}")]
        public async Task<ActionResult<ProductRelation>> GetSubProductByProductId(Int64 ProductId)
        {
            List<SubProduct> Items = new List<SubProduct>();
            try
            {
              List<Int64>  subproductItems = await _context.ProductRelation.Where(q=>q.ProductId== ProductId).Select(q=>q.SubProductId).ToListAsync();
                Items = await _context.SubProduct.Where(q => subproductItems.Contains(q.SubproductId)).ToListAsync();
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
        public async Task<ActionResult<ProductRelation>> Insert([FromBody]ProductRelation productrelation)
        {
            ProductRelation _productrelation = productrelation;
            try
            {
                _context.ProductRelation.Add(_productrelation);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_productrelation));
            //  return Ok(currency);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ProductRelation>> Update([FromBody]ProductRelation productrelation)
        {
            ProductRelation _productrelation = productrelation;
            try
            {
                _context.ProductRelation.Update(_productrelation);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(_productrelation));
            //   return Ok(subproduct);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ProductRelation>> Delete([FromBody]ProductRelation ProductRelation)
        {
            ProductRelation _ProductRelation = new ProductRelation();
            try
            {
                _ProductRelation = _context.ProductRelation
               .Where(x => x.RelationProductId == ProductRelation.RelationProductId)
               .FirstOrDefault();
                _context.ProductRelation.Remove(_ProductRelation);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            // return Ok(currency);
            return await Task.Run(() => Ok(_ProductRelation));
        }



    }
}