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
    [Route("api/PurchaseOrder")]
    [ApiController]
    public class PurchaseOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PurchaseOrdersController(ILogger<PurchaseOrdersController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el Listado de PurchaseOrder paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPurchaseOrderPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<PurchaseOrder> Items = new List<PurchaseOrder>();
            try
            {
                var query = _context.PurchaseOrder.AsQueryable();
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
        /// Obtiene el Listado de PurchaseOrderes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPurchaseOrder()
        {
            List<PurchaseOrder> Items = new List<PurchaseOrder>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.PurchaseOrder.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.Id).ToListAsync();
                }
                else
                {
                    Items = await _context.PurchaseOrder.OrderByDescending(b => b.Id).ToListAsync();
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{VendorId}")]
        public async Task<IActionResult> GetPurchaseOrderByVendor(Int64 VendorId)
        {
            List<PurchaseOrder> Items = new List<PurchaseOrder>();
            try
            {
                Items = await _context.PurchaseOrder.Where(q => q.VendorId == VendorId).ToListAsync();
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
        /// Obtiene los Datos de la PurchaseOrder por medio del Id enviado.
        /// </summary>
        /// <param name="PurchaseOrderId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PurchaseOrderId}")]
        public async Task<IActionResult> GetPurchaseOrderById(Int64 PurchaseOrderId)
        {
            PurchaseOrder Items = new PurchaseOrder();
            try
            {
                Items = await _context.PurchaseOrder.Where(q => q.Id == PurchaseOrderId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<PurchaseOrder>> InsertWithInventory([FromBody]PurchaseOrderDTO _PurchaseOrders)
        {
            PurchaseOrder _PurchaseOrdersq = new PurchaseOrder();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.PurchaseOrder.Add(_PurchaseOrders);
                       // await _context.SaveChangesAsync();

                        foreach (var item in _PurchaseOrders.PurchaseOrderLines)
                        {
                            item.Id = _PurchaseOrders.Id;
                            _context.PurchaseOrderLine.Add(item);

                            Kardex _kardexmax = await (from c in _context.Kardex
                                                 .OrderByDescending(q => q.DocumentDate)
                                                           // .Take(1)
                                                       join d in _context.KardexLine on c.KardexId equals d.KardexId
                                                       where d.ProducId == item.ProductId
                                                       select c
                                                )
                                                .FirstOrDefaultAsync();

                            if (_kardexmax == null) { _kardexmax = new Kardex(); }
                            KardexLine _KardexLine = await _context.KardexLine
                                                                         .Where(q => q.KardexId == _kardexmax.KardexId)
                                                                         .Where(q => q.ProducId == item.ProductId)
                                                                         //.Where(q => q.WareHouseId == item.WareHouseId)
                                                                         //.Where(q => q.BranchId == _GoodsDeliveredq.BranchId)
                                                                         .OrderByDescending(q => q.KardexLineId)
                                                                         .Take(1)
                                                                        .FirstOrDefaultAsync();

                            Product _subproduct = await (from c in _context.Product
                                                      .Where(q => q.ProductId == item.ProductId)
                                                         select c
                                                      ).FirstOrDefaultAsync();


                            item.QtyReceivedToDate = _KardexLine.Total + item.QtyReceived;
                            


                            _PurchaseOrders.Kardex._KardexLine.Add(new KardexLine
                            {
                                DocumentDate = _PurchaseOrders.DatePlaced,
                                ProducId = Convert.ToInt64(item.ProductId),
                                ProductName = item.ProductDescription,
                                SubProducId = 0,
                                SubProductName = "N/A",
                                QuantityEntry = Convert.ToDecimal(item.QtyReceived),
                                QuantityOut = 0,
                                BranchId = Convert.ToInt64(_PurchaseOrders.BranchId),
                                //BranchName = _PurchaseOrders.Branch.BranchName,
                                WareHouseId = 0,
                                WareHouseName = "N/A",
                                UnitOfMeasureId = item.UnitOfMeasureId,
                                UnitOfMeasureName = item.UnitOfMeasureName,
                                TypeOperationId = 1,
                                TypeOperationName = "Orden de Compra",
                                Total = Convert.ToDecimal(item.QtyReceivedToDate),
                                // TotalBags = item.QuantitySacos - _KardexLine.TotalBags,
                                //QuantityOutCD = item.Quantity - (item.Quantity * _subproduct.Merma),
                                //TotalCD = _KardexLine.TotalCD - (item.Quantity - (item.Quantity * _subproduct.Merma)),
                            });


                        }
                        await _context.SaveChangesAsync();

                        _PurchaseOrders.Kardex.DocType = 0;
                        _PurchaseOrders.Kardex.DocumentName = "FacturaProforma/PurchaseOrders";
                        _PurchaseOrders.Kardex.DocumentDate = _PurchaseOrders.DatePlaced;
                        _PurchaseOrders.Kardex.FechaCreacion = DateTime.Now;
                        _PurchaseOrders.Kardex.FechaModificacion = DateTime.Now;
                        _PurchaseOrders.Kardex.TypeOperationId = TipoOperacion.Salida;
                        _PurchaseOrders.Kardex.TypeOperationName = "Salida";
                        _PurchaseOrders.Kardex.KardexDate = DateTime.Now;

                        //_PurchaseOrders.Kardex.DocumentName = "FacturaProforma";

                        _PurchaseOrders.Kardex.CustomerId = 0;
                        _PurchaseOrders.Kardex.CustomerName = "N/A";
                        _PurchaseOrders.Kardex.CurrencyId = Convert.ToInt32(_PurchaseOrders.CurrencyId);
                        _PurchaseOrders.Kardex.CurrencyName = _PurchaseOrders.CurrencyName;
                        _PurchaseOrders.Kardex.DocumentId = _PurchaseOrders.Id;
                        _PurchaseOrders.Kardex.UsuarioCreacion = _PurchaseOrders.UsuarioCreacion;
                        _PurchaseOrders.Kardex.UsuarioModificacion = _PurchaseOrders.UsuarioModificacion;
                        _context.Kardex.Add(_PurchaseOrders.Kardex);

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _PurchaseOrders.VendorId,
                            DocType = "PurchaseOrders",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_PurchaseOrders, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_PurchaseOrders, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _PurchaseOrders.UsuarioCreacion,
                            UsuarioModificacion = _PurchaseOrders.UsuarioModificacion,
                            UsuarioEjecucion = _PurchaseOrders.UsuarioModificacion,

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
                //_PurchaseOrdersq = _PurchaseOrders;
                //_context.PurchaseOrders.Add(_PurchaseOrdersq);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PurchaseOrdersq));
        }


        /// <summary>
        /// Inserta una nueva PurchaseOrder
        /// </summary>
        /// <param name="_PurchaseOrder"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<PurchaseOrder>> Insert([FromBody]PurchaseOrder _PurchaseOrder)
        {
            PurchaseOrder _PurchaseOrderq = new PurchaseOrder();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.PurchaseOrder.Add(_PurchaseOrder);
                        //await _context.SaveChangesAsync();

                        foreach (var item in _PurchaseOrder.PurchaseOrderLines)
                        {
                            item.PurchaseOrderId = _PurchaseOrder.Id;
                            _context.PurchaseOrderLine.Add(item);
                        }
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _PurchaseOrder.VendorId,
                            DocType = "PurchaseOrder",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_PurchaseOrder, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_PurchaseOrder, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _PurchaseOrder.UsuarioCreacion,
                            UsuarioModificacion = _PurchaseOrder.UsuarioModificacion,
                            UsuarioEjecucion = _PurchaseOrder.UsuarioModificacion,

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
                //_PurchaseOrderq = _PurchaseOrder;
                //_context.PurchaseOrder.Add(_PurchaseOrderq);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PurchaseOrderq));
        }

        /// <summary>
        /// Actualiza la PurchaseOrder
        /// </summary>
        /// <param name="_PurchaseOrder"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<PurchaseOrder>> Update([FromBody]PurchaseOrder _PurchaseOrder)
        {
            PurchaseOrder _PurchaseOrderq = _PurchaseOrder;
            try
            {
                _PurchaseOrderq = await (from c in _context.PurchaseOrder
                                 .Where(q => q.Id == _PurchaseOrder.Id)
                                           select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_PurchaseOrderq).CurrentValues.SetValues((_PurchaseOrder));

                //_context.PurchaseOrder.Update(_PurchaseOrderq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PurchaseOrderq));
        }

        /// <summary>
        /// Elimina una PurchaseOrder       
        /// </summary>
        /// <param name="_PurchaseOrder"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]PurchaseOrder _PurchaseOrder)
        {
            PurchaseOrder _PurchaseOrderq = new PurchaseOrder();
            try
            {
                _PurchaseOrderq = _context.PurchaseOrder
                .Where(x => x.Id == (Int64)_PurchaseOrder.Id)
                .FirstOrDefault();

                _context.PurchaseOrder.Remove(_PurchaseOrderq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PurchaseOrderq));

        }







    }
}
