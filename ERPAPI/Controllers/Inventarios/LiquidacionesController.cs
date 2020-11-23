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
using EFCore.BulkExtensions;
//using ERPAPI.Migrations;
using Newtonsoft.Json;
namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Liquidaciones")]
    [ApiController]
    public class LiquidacionesController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public LiquidacionesController(ApplicationDbContext context, ILogger<Liquidacion> logger)
        {
            _context = context;
            _logger = logger;

        }

        /// <summary>
        /// Obtiene el listado de liquidaciones 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetLiquidaciones() {
            List<Liquidacion> liquidacions = new List<Liquidacion>();

            try
            {
                liquidacions = _context.Liquidacion.Include(i => i.Estados).ToList();
                return Ok(liquidacions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// Obtiene una liquidacionn por su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetLiquidacionById(int id) {
            try
            {
                var liquidacion = await _context.Liquidacion.Where(q => q.Id == id).FirstOrDefaultAsync();
                if (liquidacion!=null)
                {
                    return Ok(liquidacion);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Obtienne los productos de los recibos de mercaderias que no han sido liquidados 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLiquidacionesPendientesporCliente([FromQuery(Name = "Recibos")] int[] recibos) {

            List<LiquidacionLine> recibospendientes = new List<LiquidacionLine>();

            try
            {
                 recibospendientes = await  (from lineasrecibo in _context.GoodsReceivedLine.Include(i => i.GoodsReceived)
                                         where
                                         //lineasrecibo.GoodsReceived.CustomerId == customerid && 
                                         //lineasrecibo.GoodsReceived.ProductId == servicio &&
                                         recibos.Any(q=> q ==lineasrecibo.GoodsReceivedId) &&
                                         !_context.LiquidacionLine.Any(a => a.GoodsReceivedLineId == lineasrecibo.GoodsReceiveLinedId)
                                         select new LiquidacionLine() {
                                             Id = 0
                                             ,Cantidad = lineasrecibo.Quantity
                                             ,SubProductId = lineasrecibo.SubProductId                                             
                                             ,SubProductName = lineasrecibo.SubProductName 
                                             ,GoodsReceivedLineId = lineasrecibo.GoodsReceiveLinedId
                                             ,GoodsReceivedLine = lineasrecibo

                                         }).ToListAsync();
                       
                return Ok(recibospendientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Ocurrio un error:" + ex.Message);
            }
        }


        /// <summary>
        /// Obtienne los productos de los recibos de mercaderias que no han sido liquidados 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{liquidacionid}")]
        public async Task<IActionResult> LiquidacionDetalle(int liquidacionid)
        {

            List<LiquidacionLine> recibospendientes = new List<LiquidacionLine>();

            try
            {
                recibospendientes = await _context.LiquidacionLine
                    .Include(i => i.GoodsReceivedLine)
                    .Where(q => q.LiquidacionId == liquidacionid).ToListAsync();

                return Ok(recibospendientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Ocurrio un error:" + ex.Message);
            }
        }
        /// <summary>
        /// Inserta una liquidacion en la base de datos
        /// </summary>
        /// <param name="liquidacion"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Insert(Liquidacion liquidacion) {
            try
            {
               
                foreach (var item in liquidacion.detalleliquidacion)
                {
                    item.GoodsReceivedLine = null;
                }
                _context.Liquidacion.Add(liquidacion);
                BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                {
                    IdOperacion = liquidacion.Id,
                    DocType = "Liquidacion",
                    ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(liquidacion, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    Accion = "Insertar",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = liquidacion.UsuarioCreacion,
                    UsuarioModificacion = liquidacion.UsuarioModificacion,
                    UsuarioEjecucion = liquidacion.UsuarioModificacion,
                });
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Ocurrio un error:" + ex.Message);
            }


        }

        /// <summary>
        /// guarda los cambios efectuados a una liquidacion
        /// </summary>
        /// <param name="liquidacion"></param>
        /// <returns></returns>

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(Liquidacion liquidacion) {
            try
            {
                _context.Liquidacion.Update(liquidacion);
                BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                {
                    IdOperacion = liquidacion.Id,
                    DocType = "Liquidacion",
                    ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(liquidacion, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    Accion = "UPdate",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = liquidacion.UsuarioCreacion,
                    UsuarioModificacion = liquidacion.UsuarioModificacion,
                    UsuarioEjecucion = liquidacion.UsuarioModificacion,
                });
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Ocurrio un error:" + ex.Message);
            }

        }

    }
}
