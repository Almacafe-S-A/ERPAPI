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
using ERPAPI.Contexts;

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
                                             ,UOM = lineasrecibo.UnitOfMeasureName
                                             ,CantidadRecibida = lineasrecibo.Quantity
                                             ,SubProductId = lineasrecibo.SubProductId                                             
                                             ,SubProductName = lineasrecibo.SubProductName 
                                             ,GoodsReceivedLineId = lineasrecibo.GoodsReceiveLinedId
                                             ,GoodsReceivedLine = lineasrecibo
                                             ,TotalDerechos = 0
                                             ,ValorUnitarioDerechos = 0
                                             ,ValorTotalCIF = 0
                                             ,PrecioUnitarioCIF = 0
                                             ,ValorTotalDerechos = 0

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
                liquidacion.UsuarioCreacion = User.Identity.Name;
                liquidacion.FechaCreacion = DateTime.Now;

               
                foreach (var item in liquidacion.detalleliquidacion)
                {
                    item.GoodsReceivedLineId = item.GoodsReceivedLineId == 0 ? null : item.GoodsReceivedLineId;
                    item.SubProductId = item.SubProductId == 0 ? null : item.SubProductId;
                    item.GoodsReceivedLine = null;
                }
                _context.Liquidacion.Add(liquidacion);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

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
                liquidacion.EstadoId = liquidacion.EstadoId == 7 ? 5 : liquidacion.EstadoId;
                _context.Liquidacion.Update(liquidacion);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

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



        /// <summary>
        /// Actualiza el estado del Cheque a Impreso
        /// </summary>
        /// <param name="LiquidacionId"></param>
        /// <param name="Aprobado"></param>
        /// <returns></returns>
        [HttpGet("[action]/{LiquidacionId}/{Aprobado}")]
        public async Task<ActionResult<Liquidacion>> Aprobar(int LiquidacionId, bool Aprobado)
        {
            Liquidacion _Liquidacionq = new Liquidacion();
            //JournalEntry journalEntry = new JournalEntry();
            try
            {
                _Liquidacionq = await _context.Liquidacion.Where(q=>q.Id == LiquidacionId)
                    .Include(i => i.Estados)
                    .FirstOrDefaultAsync();
                //journalEntry = await _context.JournalEntry.Include(j => j.JournalEntryLines).Where(j => j.JournalEntryId == _Liquidacionq.JournalEntrId).FirstOrDefaultAsync();

                if (Aprobado)
                {
                    _Liquidacionq.EstadoId = 6;
                    //journalEntry.EstadoId = 6;
                    //journalEntry.EstadoName = "Aprobado";
                    //journalEntry.ApprovedBy = User.Identity.Name;
                    //journalEntry.ApprovedDate = DateTime.Now;
                    //////Actualiza el saldo de las cuentas contables del catalogo 
                    //Funciones.ActualizarSaldoCuentas(_context, journalEntry);
                }
                else
                {
                    _Liquidacionq.EstadoId = 7;

                }

                BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                {
                    IdOperacion = _Liquidacionq.Id,
                    DocType = "CheckAccount",
                    ClaseInicial =
                          Newtonsoft.Json.JsonConvert.SerializeObject(_Liquidacionq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    Accion = _Liquidacionq.Estados.DescripcionEstado + " Cheque",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = User.Identity.Name,
                    UsuarioModificacion = User.Identity.Name,
                    UsuarioEjecucion = User.Identity.Name,

                });



                //_context.Liquidacion.Update(_Liquidacionq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Liquidacionq));
        }


    }
}
