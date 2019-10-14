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
    [Route("api/GoodsDeliveryAuthorization")]
    [ApiController]
    public class GoodsDeliveryAuthorizationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GoodsDeliveryAuthorizationController(ILogger<GoodsDeliveryAuthorizationController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de GoodsDeliveryAuthorization paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveryAuthorizationPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<GoodsDeliveryAuthorization> Items = new List<GoodsDeliveryAuthorization>();
            try
            {
                var query = _context.GoodsDeliveryAuthorization.AsQueryable();
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
        /// Obtiene el Listado de GoodsDeliveryAuthorizationes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveryAuthorization()
        {
            List<GoodsDeliveryAuthorization> Items = new List<GoodsDeliveryAuthorization>();
            try
            {
                Items = await _context.GoodsDeliveryAuthorization.ToListAsync();
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
        /// Obtiene los Datos de la GoodsDeliveryAuthorization por medio del Id enviado.
        /// </summary>
        /// <param name="GoodsDeliveryAuthorizationId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{GoodsDeliveryAuthorizationId}")]
        public async Task<IActionResult> GetGoodsDeliveryAuthorizationById(Int64 GoodsDeliveryAuthorizationId)
        {
            GoodsDeliveryAuthorization Items = new GoodsDeliveryAuthorization();
            try
            {
                Items = await _context.GoodsDeliveryAuthorization.Include(q=>q.GoodsDeliveryAuthorizationLine)
                       .Where(q => q.GoodsDeliveryAuthorizationId == GoodsDeliveryAuthorizationId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveryAuthorizationNoSelected()
        {
            List<GoodsDeliveryAuthorization> Items = new List<GoodsDeliveryAuthorization>();
            try
            {
                List<Int64> listayaprocesada = _context.GoodsDeliveredLine
                                              .Where(q => q.NoAR > 0)
                                              .Select(q => q.NoAR).ToList();

                Items = await _context.GoodsDeliveryAuthorization.Where(q => !listayaprocesada.Contains(q.GoodsDeliveryAuthorizationId)).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveryAuthorizationNoSelectedBoletaSalida()
        {
            List<GoodsDeliveryAuthorization> Items = new List<GoodsDeliveryAuthorization>();
            try
            {
                List<Int64> listayaprocesada = _context.BoletaDeSalida
                                              .Where(q => q.GoodsDeliveryAuthorizationId > 0)
                                              .Select(q => q.GoodsDeliveryAuthorizationId).ToList();

                Items = await _context.GoodsDeliveryAuthorization.Where(q => !listayaprocesada.Contains(q.GoodsDeliveryAuthorizationId)).ToListAsync();
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
        /// Inserta una nueva GoodsDeliveryAuthorization
        /// </summary>
        /// <param name="_GoodsDeliveryAuthorization"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<GoodsDeliveryAuthorization>> Insert([FromBody]GoodsDeliveryAuthorizationDTO _GoodsDeliveryAuthorization)
        {
            GoodsDeliveryAuthorization _GoodsDeliveryAuthorizationq = new GoodsDeliveryAuthorization();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _GoodsDeliveryAuthorizationq = _GoodsDeliveryAuthorization;
                        _context.GoodsDeliveryAuthorization.Add(_GoodsDeliveryAuthorizationq);

                        foreach (var item in _GoodsDeliveryAuthorizationq.GoodsDeliveryAuthorizationLine)
                        {
                            item.GoodsDeliveryAuthorizationId = _GoodsDeliveryAuthorizationq.GoodsDeliveryAuthorizationId;
                            _context.GoodsDeliveryAuthorizationLine.Add(item);

                            Int64 IdCD = await _context.CertificadoDeposito.Where(q => q.NoCD == item.NoCertificadoDeposito).Select(q => q.IdCD).FirstOrDefaultAsync();
                            //Kardex _kardexmax = await (from kdx in _context.Kardex
                            //      .Where(q => q.CustomerId == _GoodsDeliveryAuthorization.CustomerId)
                            //                           from kdxline in _context.KardexLine
                            //                             .Where(q => q.KardexId == kdx.KardexId)
                            //                               .Where(o => o.SubProducId == item.SubProductId)
                            //                               .OrderByDescending(o => o.DocumentDate).Take(1)
                            //                           select kdx).FirstOrDefaultAsync();

                            Kardex _kardexmax = await (from c in _context.Kardex
                                                             .OrderByDescending(q => q.DocumentDate)
                                                           // .Take(1)
                                                       join d in _context.KardexLine on c.KardexId equals d.KardexId
                                                       where c.CustomerId == _GoodsDeliveryAuthorization.CustomerId && d.SubProducId == item.SubProductId
                                                             && c.DocumentId == IdCD && c.DocumentName =="CD"
                                                       select c
                                                          )
                                                          .FirstOrDefaultAsync();

                            if (_kardexmax == null) { _kardexmax = new Kardex(); }

                          

                            KardexLine _KardexLine = await _context.KardexLine
                                                                         .Where(q => q.KardexId == _kardexmax.KardexId)
                                                                         .Where(q => q.SubProducId == item.SubProductId)
                                                                        // .Where(q => q.WareHouseId == item.WarehouseId)
                                                                        // .Where(q => q.BranchId == _GoodsDeliveryAuthorizationq.BranchId)
                                                                         .OrderByDescending(q => q.KardexLineId)
                                                                         .Take(1)
                                                                        .FirstOrDefaultAsync();

                            SubProduct _subproduct = await (from c in _context.SubProduct
                                                     .Where(q => q.SubproductId == item.SubProductId)
                                                            select c
                                                     ).FirstOrDefaultAsync();

                            if(_KardexLine.TotalCD< item.Quantity)
                            {
                                return await Task.Run(() => BadRequest($"La cantidad a retirar no puede ser superior al total del ciertificado"));
                            }

                            //  _context.GoodsReceivedLine.Add(item);
                            //item. = item.Quantity + _KardexLine.Total;

                            //Por cada linea de certificado , se agrega un Kardex de salida del tipo CD
                            _GoodsDeliveryAuthorization.Kardex._KardexLine.Add(new KardexLine
                            {
                                DocumentDate = _GoodsDeliveryAuthorization.DocumentDate,
                                // ProducId = _CertificadoDeposito.,
                                // ProductName = _GoodsReceivedq.ProductName,
                                SubProducId = item.SubProductId,
                                SubProductName = item.SubProductName,
                                QuantityEntry = 0,
                                QuantityOut = item.Quantity,
                                QuantityEntryBags = 0,
                                BranchId = _GoodsDeliveryAuthorization.BranchId,
                                BranchName = _GoodsDeliveryAuthorization.BranchName,
                                WareHouseId = item.WarehouseId,
                                WareHouseName = item.WarehouseName,
                                UnitOfMeasureId = item.UnitOfMeasureId,
                                UnitOfMeasureName = item.UnitOfMeasureName,
                                TypeOperationId = 1,
                                TypeOperationName = "Salida",
                                //Total = item.valorcertificado,
                                //TotalBags = item.QuantitySacos + _KardexLine.TotalBags,
                                //QuantityEntryCD = item.Quantity / (1 + _subproduct.Merma),
                                QuantityOutCD = item.Quantity,
                                TotalCD = _KardexLine.TotalCD - (item.Quantity),
                            });


                            _GoodsDeliveryAuthorization.Kardex.DocType = 0;
                            _GoodsDeliveryAuthorization.Kardex.DocName = "SolicitudAutorizacion/GoodsDeliveryAuthorization";
                            _GoodsDeliveryAuthorization.Kardex.DocumentDate = _GoodsDeliveryAuthorization.DocumentDate;
                            _GoodsDeliveryAuthorization.Kardex.FechaCreacion = DateTime.Now;
                            _GoodsDeliveryAuthorization.Kardex.FechaModificacion = DateTime.Now;
                            _GoodsDeliveryAuthorization.Kardex.TypeOperationId = 1;
                            _GoodsDeliveryAuthorization.Kardex.TypeOperationName = "Salida";
                            _GoodsDeliveryAuthorization.Kardex.KardexDate = DateTime.Now;
                            _GoodsDeliveryAuthorization.Kardex.DocumentName = "CD";

                            _GoodsDeliveryAuthorization.Kardex.CustomerId = _GoodsDeliveryAuthorization.CustomerId;
                            _GoodsDeliveryAuthorization.Kardex.CustomerName = _GoodsDeliveryAuthorization.CustomerName;
                            //_CertificadoDeposito.Kardex.CurrencyId = _CertificadoDeposito.CurrencyId;
                            _GoodsDeliveryAuthorization.Kardex.CurrencyName = _GoodsDeliveryAuthorization.CurrencyName;
                            _GoodsDeliveryAuthorization.Kardex.DocumentId = IdCD;
                            _GoodsDeliveryAuthorization.Kardex.UsuarioCreacion = _GoodsDeliveryAuthorization.UsuarioCreacion;
                            _GoodsDeliveryAuthorization.Kardex.UsuarioModificacion = _GoodsDeliveryAuthorization.UsuarioModificacion;

                            _context.Kardex.Add(_GoodsDeliveryAuthorization.Kardex);


                        }




                        //await _context.SaveChangesAsync();
                        //foreach (var item in _GoodsDeliveryAuthorization.CertificadosAsociados)
                        //{
                        //    CDGoodsDeliveryAuthorization _certificadoauthorization =
                        //        new CDGoodsDeliveryAuthorization
                        //        {
                        //            CD = item,
                        //            GoodsDeliveryAuthorizationId = _GoodsDeliveryAuthorizationq.GoodsDeliveryAuthorizationId,
                        //        };

                        //    _context.CDGoodsDeliveryAuthorization.Add(_certificadoauthorization);
                        //}


                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _GoodsDeliveryAuthorization.GoodsDeliveryAuthorizationId,
                            DocType = "GoodsDeliveryAuthorization",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsDeliveryAuthorization, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsDeliveryAuthorization, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _GoodsDeliveryAuthorization.UsuarioCreacion,
                            UsuarioModificacion = _GoodsDeliveryAuthorization.UsuarioModificacion,
                            UsuarioEjecucion = _GoodsDeliveryAuthorization.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();


                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                   
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(()=> BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return Ok(_GoodsDeliveryAuthorizationq);
        }

        /// <summary>
        /// Actualiza la GoodsDeliveryAuthorization
        /// </summary>
        /// <param name="_GoodsDeliveryAuthorization"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<GoodsDeliveryAuthorization>> Update([FromBody]GoodsDeliveryAuthorization _GoodsDeliveryAuthorization)
        {
            GoodsDeliveryAuthorization _GoodsDeliveryAuthorizationq = _GoodsDeliveryAuthorization;
            try
            {
                _GoodsDeliveryAuthorizationq = await (from c in _context.GoodsDeliveryAuthorization
                                 .Where(q => q.GoodsDeliveryAuthorizationId == _GoodsDeliveryAuthorization.GoodsDeliveryAuthorizationId)
                                                      select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_GoodsDeliveryAuthorizationq).CurrentValues.SetValues((_GoodsDeliveryAuthorization));

                //_context.GoodsDeliveryAuthorization.Update(_GoodsDeliveryAuthorizationq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveryAuthorizationq));
        }

        /// <summary>
        /// Elimina una GoodsDeliveryAuthorization       
        /// </summary>
        /// <param name="_GoodsDeliveryAuthorization"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]GoodsDeliveryAuthorization _GoodsDeliveryAuthorization)
        {
            GoodsDeliveryAuthorization _GoodsDeliveryAuthorizationq = new GoodsDeliveryAuthorization();
            try
            {
                _GoodsDeliveryAuthorizationq = _context.GoodsDeliveryAuthorization
                .Where(x => x.GoodsDeliveryAuthorizationId == (Int64)_GoodsDeliveryAuthorization.GoodsDeliveryAuthorizationId)
                .FirstOrDefault();

                _context.GoodsDeliveryAuthorization.Remove(_GoodsDeliveryAuthorizationq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveryAuthorizationq));

        }







    }
}