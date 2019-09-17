using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/CostListItem")]
    [ApiController]
    public class CostListItemController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        /*public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public CostListItemController(ILogger<CostListItemController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }



        /// <summary>
        /// Obtiene el Listado de CostListItem paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCostListItemPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CostListItem> Items = new List<CostListItem>();
            try
            {
                var query = _context.CostListItem.AsQueryable();
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
        /// Obtiene los Datos de la Tasa de Cambio en una lista.
        /// </summary>

        // GET: api/CostListItem
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCostListItem()

        {
            List<CostListItem> Items = new List<CostListItem>();
            try
            {
                Items = await _context.CostListItem.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
          
            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Obtiene los Datos de la CostListItem por medio del Id enviado.
        /// </summary>
        /// <param name="CostListItemId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CostListItemId}")]
        public async Task<IActionResult> GetCostListItemById(Int64 CostListItemId)
        {
            CostListItem Items = new CostListItem();
            try
            {
                Items = await _context.CostListItem.Where(q => q.CostListItemId == CostListItemId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva CostListItem
        /// /// </summary>
        /// <param name="_CostListItem"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CostListItem>> Insert([FromBody]CostListItem _CostListItem)
        {
            CostListItem _CostListItemq = new CostListItem();
            try
            {
                _CostListItemq = _CostListItem;
                _context.CostListItem.Add(_CostListItemq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CostListItemq));
        }

        /// <summary>
        /// Actualiza la CostListItem
        /// </summary>
        /// <param name="_CostListItem"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CostListItem>> Update([FromBody]CostListItem _CostListItem)
        {
            CostListItem _CostListItemq = _CostListItem;
            try
            {
                _CostListItemq = await (from c in _context.CostListItem
                                 .Where(q => q.CostListItemId == _CostListItem.CostListItemId)
                                        select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CostListItemq).CurrentValues.SetValues((_CostListItem));

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CostListItemq));
        }

        /// <summary>
        /// Elimina una CostListItem       
        /// </summary>
        /// <param name="_CostListItem"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CostListItem _CostListItem)
        {
            CostListItem _CostListItemq = new CostListItem();
            try
            {
                _CostListItemq = _context.CostListItem
                .Where(x => x.CostListItemId == (Int64)_CostListItem.CostListItemId)
                .FirstOrDefault();

                _context.CostListItem.Remove(_CostListItemq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CostListItemq));

        }
    }
}