using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/InventoryTransfer")]
    [ApiController]
    public class InventoryTransfersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InventoryTransfersController(ILogger<InventoryTransfersController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el Listado de InventoryTransfer paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInventoryTransferPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<InventoryTransfer> Items = new List<InventoryTransfer>();
            try
            {
                var query = _context.InventoryTransfer.AsQueryable();
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


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el Listado de InventoryTransferes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInventoryTransfer()
        {
            List<InventoryTransfer> Items = new List<InventoryTransfer>();
            try
            {
                Items = await _context.InventoryTransfer.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{SourceBranchId}")]
        public async Task<IActionResult> GetInventoryTransferBySourceBranch(Int64 SourceBranchId)
        {
            List<InventoryTransfer> Items = new List<InventoryTransfer>();
            try
            {
                Items = await _context.InventoryTransfer.Where(q => q.SourceBranchId == SourceBranchId).ToListAsync();
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
        /// Obtiene los Datos de la InventoryTransfer por medio del Id enviado.
        /// </summary>
        /// <param name="InventoryTransferId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InventoryTransferId}")]
        public async Task<IActionResult> GetInventoryTransferById(Int64 InventoryTransferId)
        {
            InventoryTransfer Items = new InventoryTransfer();
            try
            {
                Items = await _context.InventoryTransfer.Where(q => q.Id == InventoryTransferId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<InventoryTransfer>> InsertBranchTransfer([FromBody]InventoryTransferDTO _InventoryTransfers)
        {
            InventoryTransfer _InventoryTransfersq = new InventoryTransfer();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.InventoryTransfer.Add(_InventoryTransfers);
                        // await _context.SaveChangesAsync();

                        foreach (var item in _InventoryTransfers.InventoryTransferLines)
                        {
                            item.Id = _InventoryTransfers.Id;
                            _context.InventoryTransferLine.Add(item);

                            Kardex _kardexmax = await (from c in _context.Kardex
                                                 .OrderByDescending(q => q.DocumentDate)
                                                           // .Take(1)
                                                       join d in _context.KardexLine on c.KardexId equals d.KardexId
                                                       where d.ProducId == item.ProductId
                                                       select c
                                                )
                                                .FirstOrDefaultAsync();

                            if (_kardexmax == null) { _kardexmax = new Kardex(); }
                            KardexLine _KardexLineSource = await _context.KardexLine
                                                                         .Where(q => q.KardexId == _kardexmax.KardexId)
                                                                         .Where(q => q.ProducId == item.ProductId)
                                                                         //.Where(q => q.WareHouseId == item.WareHouseId)
                                                                         .Where(q => q.BranchId == _InventoryTransfers.SourceBranchId)
                                                                         .OrderByDescending(q => q.KardexLineId)
                                                                         .Take(1)
                                                                        .FirstOrDefaultAsync();

                            KardexLine _KardexLineTarget = await _context.KardexLine
                                                                         .Where(q => q.KardexId == _kardexmax.KardexId)
                                                                         .Where(q => q.ProducId == item.ProductId)
                                                                         //.Where(q => q.WareHouseId == item.WareHouseId)
                                                                         .Where(q => q.BranchId == _InventoryTransfers.TargetBranchId)
                                                                         .OrderByDescending(q => q.KardexLineId)
                                                                         .Take(1)
                                                                        .FirstOrDefaultAsync();

                            Product _subproduct = await (from c in _context.Product
                                                      .Where(q => q.ProductId == item.ProductId)
                                                         select c
                                                      ).FirstOrDefaultAsync();


                            //item. = _KardexLine.Total + item.QtyReceived;

                            

                            //////Entrada de Inventario///////////
                            _InventoryTransfers.Kardex._KardexLine.Add(new KardexLine
                            {
                                DocumentDate = _InventoryTransfers.DateGenerated,
                                ProducId = Convert.ToInt64(item.ProductId),
                                ProductName = item.ProductName,
                                SubProducId = 0,
                                SubProductName = "N/A",
                                QuantityEntry = Convert.ToDouble(item.QtyIn),
                                QuantityOut = 0,
                                BranchId = Convert.ToInt64(_InventoryTransfers.TargetBranchId),
                                //BranchName = _InventoryTransfers.Branch.BranchName,
                                WareHouseId = 0,
                                WareHouseName = "N/A",
                                UnitOfMeasureId = item.UnitOfMeasureId,
                                UnitOfMeasureName = item.UnitOfMeasureName,
                                TypeOperationId = 7,
                                TypeOperationName = "Orden de Compra",
                                Total = _KardexLineTarget.Total + Convert.ToDouble(item.QtyIn),
                                // TotalBags = item.QuantitySacos - _KardexLine.TotalBags,
                                //QuantityOutCD = item.Quantity - (item.Quantity * _subproduct.Merma),
                                //TotalCD = _KardexLine.TotalCD - (item.Quantity - (item.Quantity * _subproduct.Merma)),
                            });

                            //////Salida de Inventario///////////
                            _InventoryTransfers.Kardex._KardexLine.Add(new KardexLine
                            {
                                DocumentDate = _InventoryTransfers.DateGenerated,
                                ProducId = Convert.ToInt64(item.ProductId),
                                ProductName = item.ProductName,
                                SubProducId = 0,
                                SubProductName = "N/A",
                                QuantityEntry = 0,
                                QuantityOut = Convert.ToDouble(item.QtyIn),
                                BranchId = Convert.ToInt64(_InventoryTransfers.SourceBranch),
                                //BranchName = _InventoryTransfers.Branch.BranchName,
                                WareHouseId = 0,
                                WareHouseName = "N/A",
                                UnitOfMeasureId = item.UnitOfMeasureId,
                                UnitOfMeasureName = item.UnitOfMeasureName,
                                TypeOperationId = 7,
                                TypeOperationName = "Orden de Compra",
                                Total = _KardexLineSource.Total - Convert.ToDouble(item.QtyIn),
                                // TotalBags = item.QuantitySacos - _KardexLine.TotalBags,
                                //QuantityOutCD = item.Quantity - (item.Quantity * _subproduct.Merma),
                                //TotalCD = _KardexLine.TotalCD - (item.Quantity - (item.Quantity * _subproduct.Merma)),
                            });


                        }
                        await _context.SaveChangesAsync();

                        _InventoryTransfers.Kardex.DocType = 0;
                        _InventoryTransfers.Kardex.DocName = "FacturaProforma/InventoryTransfers";
                        _InventoryTransfers.Kardex.DocumentDate = _InventoryTransfers.DateGenerated;
                        _InventoryTransfers.Kardex.FechaCreacion = DateTime.Now;
                        _InventoryTransfers.Kardex.FechaModificacion = DateTime.Now;
                        _InventoryTransfers.Kardex.TypeOperationId = 1;
                        _InventoryTransfers.Kardex.TypeOperationName = "Salida";
                        _InventoryTransfers.Kardex.KardexDate = DateTime.Now;

                        _InventoryTransfers.Kardex.DocumentName = "FacturaProforma";

                        _InventoryTransfers.Kardex.CustomerId = 0;
                        _InventoryTransfers.Kardex.CustomerName = "N/A";
                        _InventoryTransfers.Kardex.CurrencyId = Convert.ToInt32(_InventoryTransfers.CurrencyId);
                        _InventoryTransfers.Kardex.CurrencyName = _InventoryTransfers.CurrencyName;
                        _InventoryTransfers.Kardex.DocumentId = _InventoryTransfers.Id;
                        _InventoryTransfers.Kardex.UsuarioCreacion = _InventoryTransfers.UsuarioCreacion;
                        _InventoryTransfers.Kardex.UsuarioModificacion = _InventoryTransfers.UsuarioModificacion;
                        _context.Kardex.Add(_InventoryTransfers.Kardex);

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _InventoryTransfers.Id,
                            DocType = "InventoryTransfers",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_InventoryTransfers, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_InventoryTransfers, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _InventoryTransfers.UsuarioCreacion,
                            UsuarioModificacion = _InventoryTransfers.UsuarioModificacion,
                            UsuarioEjecucion = _InventoryTransfers.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }
                //_InventoryTransfersq = _InventoryTransfers;
                //_context.InventoryTransfers.Add(_InventoryTransfersq);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_InventoryTransfersq));
        }


        /// <summary>
        /// Inserta una nueva InventoryTransfer
        /// </summary>
        /// <param name="_InventoryTransfer"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InventoryTransfer>> Insert([FromBody]InventoryTransfer _InventoryTransfer)
        {
            InventoryTransfer _InventoryTransferq = new InventoryTransfer();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.InventoryTransfer.Add(_InventoryTransfer);
                        //await _context.SaveChangesAsync();

                        foreach (var item in _InventoryTransfer.InventoryTransferLines)
                        {
                            item.InventoryTransferId = _InventoryTransfer.Id;
                            _context.InventoryTransferLine.Add(item);
                        }
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _InventoryTransfer.Id,
                            DocType = "InventoryTransfer",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_InventoryTransfer, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_InventoryTransfer, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _InventoryTransfer.UsuarioCreacion,
                            UsuarioModificacion = _InventoryTransfer.UsuarioModificacion,
                            UsuarioEjecucion = _InventoryTransfer.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }
                //_InventoryTransferq = _InventoryTransfer;
                //_context.InventoryTransfer.Add(_InventoryTransferq);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_InventoryTransferq));
        }

        /// <summary>
        /// Actualiza la InventoryTransfer
        /// </summary>
        /// <param name="_InventoryTransfer"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<InventoryTransfer>> Update([FromBody]InventoryTransfer _InventoryTransfer)
        {
            InventoryTransfer _InventoryTransferq = _InventoryTransfer;
            try
            {
                _InventoryTransferq = await (from c in _context.InventoryTransfer
                                 .Where(q => q.Id == _InventoryTransfer.Id)
                                         select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_InventoryTransferq).CurrentValues.SetValues((_InventoryTransfer));

                //_context.InventoryTransfer.Update(_InventoryTransferq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_InventoryTransferq));
        }

        /// <summary>
        /// Elimina una InventoryTransfer       
        /// </summary>
        /// <param name="_InventoryTransfer"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]InventoryTransfer _InventoryTransfer)
        {
            InventoryTransfer _InventoryTransferq = new InventoryTransfer();
            try
            {
                _InventoryTransferq = _context.InventoryTransfer
                .Where(x => x.Id == (Int64)_InventoryTransfer.Id)
                .FirstOrDefault();

                _context.InventoryTransfer.Remove(_InventoryTransferq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_InventoryTransferq));

        }







    }
}
