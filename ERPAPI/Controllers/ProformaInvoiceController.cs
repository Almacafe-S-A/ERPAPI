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
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/ProformaInvoice")]
    [ApiController]
    public class ProformaInvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ProformaInvoiceController(ILogger<ProformaInvoiceController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el Listado de ProformaInvoice paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProformaInvoicePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<ProformaInvoice> Items = new List<ProformaInvoice>();
            try
            {
                var query = _context.ProformaInvoice.AsQueryable();
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
        /// Obtiene el Listado de ProformaInvoicees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProformaInvoice()
        {
            List<ProformaInvoice> Items = new List<ProformaInvoice>();
            try
            {
                Items = await _context.ProformaInvoice.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetProformaInvoiceByCustomer(Int64 CustomerId)
        {
            List<ProformaInvoice> Items = new List<ProformaInvoice>();
            try
            {
                Items = await _context.ProformaInvoice.Where(q=>q.CustomerId==CustomerId).ToListAsync();
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
        /// Obtiene los Datos de la ProformaInvoice por medio del Id enviado.
        /// </summary>
        /// <param name="ProformaInvoiceId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ProformaInvoiceId}")]
        public async Task<IActionResult> GetProformaInvoiceById(Int64 ProformaInvoiceId)
        {
            ProformaInvoice Items = new ProformaInvoice();
            try
            {
                Items = await _context.ProformaInvoice.Where(q => q.ProformaId == ProformaInvoiceId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }



        [HttpPost("[action]")]
        public async Task<ActionResult<ProformaInvoice>> InsertWithInventory([FromBody]ProformaInvoiceDTO _ProformaInvoice)
        {
            ProformaInvoice _ProformaInvoiceq = new ProformaInvoice();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.ProformaInvoice.Add(_ProformaInvoice);
                        //await _context.SaveChangesAsync();

                        foreach (var item in _ProformaInvoice.ProformaInvoiceLine)
                        {
                            item.ProformaInvoiceId = _ProformaInvoice.ProformaId;
                            _context.ProformaInvoiceLine.Add(item);

                            Kardex _kardexmax = await (from c in _context.Kardex
                                                 .OrderByDescending(q => q.DocumentDate)
                                                           // .Take(1)
                                                       join d in _context.KardexLine on c.KardexId equals d.KardexId
                                                       where   d.ProducId == item.SubProductId
                                                       select c
                                                )
                                                .FirstOrDefaultAsync();

                            if (_kardexmax == null) { _kardexmax = new Kardex(); }
                            KardexLine _KardexLine = await _context.KardexLine
                                                                         .Where(q => q.KardexId == _kardexmax.KardexId)
                                                                         .Where(q => q.SubProducId == item.SubProductId)
                                                                         //.Where(q => q.WareHouseId == item.WareHouseId)
                                                                          //.Where(q => q.BranchId == _GoodsDeliveredq.BranchId)
                                                                         .OrderByDescending(q => q.KardexLineId)
                                                                         .Take(1)
                                                                        .FirstOrDefaultAsync();

                            Product _subproduct = await (from c in _context.Product
                                                      .Where(q => q.ProductId == item.SubProductId)
                                                            select c
                                                      ).FirstOrDefaultAsync();

                            if (_KardexLine.Total > item.Quantity)
                            {
                                item.Total = _KardexLine.Total - item.Quantity;
                            }
                            else
                            {
                                return BadRequest("Inventario insuficiente!");
                            }


                            _ProformaInvoice.Kardex._KardexLine.Add(new KardexLine
                            {
                                DocumentDate = _ProformaInvoice.OrderDate,
                                ProducId = item.ProductId,
                                ProductName = item.ProductName,
                                SubProducId = item.SubProductId,
                                SubProductName = item.SubProductName,
                                QuantityEntry = 0,
                                QuantityOut = item.Quantity,
                                BranchId = _ProformaInvoice.BranchId,
                                BranchName = _ProformaInvoice.BranchName,
                                WareHouseId = item.WareHouseId,
                                WareHouseName = item.WareHouseName,
                                UnitOfMeasureId = item.UnitOfMeasureId,
                                UnitOfMeasureName = item.UnitOfMeasureName,
                                TypeOperationId = 1,
                                TypeOperationName = "Salida",
                                Total = item.Total,
                               // TotalBags = item.QuantitySacos - _KardexLine.TotalBags,
                                //QuantityOutCD = item.Quantity - (item.Quantity * _subproduct.Merma),
                                //TotalCD = _KardexLine.TotalCD - (item.Quantity - (item.Quantity * _subproduct.Merma)),
                            });


                        }
                        await _context.SaveChangesAsync();

                        _ProformaInvoice.Kardex.DocType = 0;
                        _ProformaInvoice.Kardex.DocName = "FacturaProforma/ProformaInvoice";
                        _ProformaInvoice.Kardex.DocumentDate = _ProformaInvoice.OrderDate;
                        _ProformaInvoice.Kardex.FechaCreacion = DateTime.Now;
                        _ProformaInvoice.Kardex.FechaModificacion = DateTime.Now;
                        _ProformaInvoice.Kardex.TypeOperationId = 1;
                        _ProformaInvoice.Kardex.TypeOperationName = "Salida";
                        _ProformaInvoice.Kardex.KardexDate = DateTime.Now;

                        _ProformaInvoice.Kardex.DocumentName = "FacturaProforma";

                        _ProformaInvoice.Kardex.CustomerId = _ProformaInvoice.CustomerId;
                        _ProformaInvoice.Kardex.CustomerName = _ProformaInvoice.CustomerName;
                        _ProformaInvoice.Kardex.CurrencyId = _ProformaInvoice.CurrencyId;
                        _ProformaInvoice.Kardex.CurrencyName = _ProformaInvoice.CurrencyName;
                        _ProformaInvoice.Kardex.DocumentId = _ProformaInvoice.ProformaId;
                        _ProformaInvoice.Kardex.UsuarioCreacion = _ProformaInvoice.UsuarioCreacion;
                        _ProformaInvoice.Kardex.UsuarioModificacion = _ProformaInvoice.UsuarioModificacion;
                        _context.Kardex.Add(_ProformaInvoice.Kardex);

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ProformaInvoice.CustomerId,
                            DocType = "ProformaInvoice",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_ProformaInvoice, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_ProformaInvoice, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ProformaInvoice.UsuarioCreacion,
                            UsuarioModificacion = _ProformaInvoice.UsuarioModificacion,
                            UsuarioEjecucion = _ProformaInvoice.UsuarioModificacion,

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
                //_ProformaInvoiceq = _ProformaInvoice;
                //_context.ProformaInvoice.Add(_ProformaInvoiceq);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ProformaInvoiceq));
        }




        /// <summary>
        /// Inserta una nueva ProformaInvoice
        /// </summary>
        /// <param name="_ProformaInvoice"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ProformaInvoice>> Insert([FromBody]ProformaInvoice _ProformaInvoice)
        {
            ProformaInvoice _ProformaInvoiceq = new ProformaInvoice();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.ProformaInvoice.Add(_ProformaInvoice);
                        //await _context.SaveChangesAsync();

                        foreach (var item in _ProformaInvoice.ProformaInvoiceLine)
                        {
                            item.ProformaInvoiceId = _ProformaInvoice.ProformaId;
                            _context.ProformaInvoiceLine.Add(item);
                        }
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ProformaInvoice.CustomerId,
                            DocType = "ProformaInvoice",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_ProformaInvoice, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_ProformaInvoice, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ProformaInvoice.UsuarioCreacion,
                            UsuarioModificacion = _ProformaInvoice.UsuarioModificacion,
                            UsuarioEjecucion = _ProformaInvoice.UsuarioModificacion,

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
                //_ProformaInvoiceq = _ProformaInvoice;
                //_context.ProformaInvoice.Add(_ProformaInvoiceq);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ProformaInvoiceq));
        }

        /// <summary>
        /// Actualiza la ProformaInvoice
        /// </summary>
        /// <param name="_ProformaInvoice"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ProformaInvoice>> Update([FromBody]ProformaInvoice _ProformaInvoice)
        {
            ProformaInvoice _ProformaInvoiceq = _ProformaInvoice;
            try
            {
                _ProformaInvoiceq = await (from c in _context.ProformaInvoice
                                 .Where(q => q.ProformaId == _ProformaInvoice.ProformaId)
                                           select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_ProformaInvoiceq).CurrentValues.SetValues((_ProformaInvoice));

                //_context.ProformaInvoice.Update(_ProformaInvoiceq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ProformaInvoiceq));
        }

        /// <summary>
        /// Elimina una ProformaInvoice       
        /// </summary>
        /// <param name="_ProformaInvoice"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ProformaInvoice _ProformaInvoice)
        {
            ProformaInvoice _ProformaInvoiceq = new ProformaInvoice();
            try
            {
                _ProformaInvoiceq = _context.ProformaInvoice
                .Where(x => x.ProformaId == (Int64)_ProformaInvoice.ProformaId)
                .FirstOrDefault();

                _context.ProformaInvoice.Remove(_ProformaInvoiceq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ProformaInvoiceq));

        }







    }
}