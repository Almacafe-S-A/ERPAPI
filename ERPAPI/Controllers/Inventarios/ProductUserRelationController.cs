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
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/ProductUserRelation")]
    [ApiController]
    public class ProductUserRelationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ProductUserRelationController(ILogger<ProductUserRelationController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de ProductUserRelation paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductUserRelationPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<ProductUserRelation> Items = new List<ProductUserRelation>();
            try
            {
                var query = _context.ProductUserRelation.AsQueryable();
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

        /// <summary>
        /// Obtiene el Listado de ProductUserRelationes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductUserRelation()
        {
            List<ProductUserRelation> Items = new List<ProductUserRelation>();
            try
            {
                Items = await _context.ProductUserRelation.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la ProductUserRelation por medio del Id enviado.
        /// </summary>
        /// <param name="ProductUserRelationId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ProductUserRelationId}")]
        public async Task<IActionResult> GetProductUserRelationById(Int64 ProductUserRelationId)
        {
            ProductUserRelation Items = new ProductUserRelation();
            try
            {
                Items = await _context.ProductUserRelation.Where(q => q.ProductUserRelationId == ProductUserRelationId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva ProductUserRelation
        /// </summary>
        /// <param name="_ProductUserRelation"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ProductUserRelation>> Insert([FromBody]ProductUserRelation _ProductUserRelation)
        {
            ProductUserRelation _ProductUserRelationq = new ProductUserRelation();
            try
            {
                _ProductUserRelationq = _ProductUserRelation;
                _context.ProductUserRelation.Add(_ProductUserRelationq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ProductUserRelationq));
        }

        /// <summary>
        /// Actualiza la ProductUserRelation
        /// </summary>
        /// <param name="_ProductUserRelation"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ProductUserRelation>> Update([FromBody]ProductUserRelation _ProductUserRelation)
        {
            ProductUserRelation _ProductUserRelationq = _ProductUserRelation;
            try
            {
                _ProductUserRelationq = await (from c in _context.ProductUserRelation
                                 .Where(q => q.ProductUserRelationId == _ProductUserRelation.ProductUserRelationId)
                                               select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_ProductUserRelationq).CurrentValues.SetValues((_ProductUserRelation));

                //_context.ProductUserRelation.Update(_ProductUserRelationq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_ProductUserRelationq));
        }

        /// <summary>
        /// Elimina una ProductUserRelation       
        /// </summary>
        /// <param name="_ProductUserRelation"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ProductUserRelation _ProductUserRelation)
        {
            ProductUserRelation _ProductUserRelationq = new ProductUserRelation();
            try
            {
                _ProductUserRelationq = _context.ProductUserRelation
                .Where(x => x.ProductUserRelationId == (Int64)_ProductUserRelation.ProductUserRelationId)
                .FirstOrDefault();

                _context.ProductUserRelation.Remove(_ProductUserRelationq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_ProductUserRelationq));

        }







    }
}