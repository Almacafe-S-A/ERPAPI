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
    [Route("api/GoodsDelivered")]
    [ApiController]
    public class GoodsDeliveredController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GoodsDeliveredController(ILogger<GoodsDeliveredController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de GoodsDelivered paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveredPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<GoodsDelivered> Items = new List<GoodsDelivered>();
            try
            {
                var query = _context.GoodsDelivered.AsQueryable();
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
        /// Obtiene el Listado de GoodsDeliveredes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDelivered()
        {
            List<GoodsDelivered> Items = new List<GoodsDelivered>();
            try
            {
                Items = await _context.GoodsDelivered.ToListAsync();
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
        public async Task<IActionResult> GetGoodsDeliveredNoSelected()
        {
            List<GoodsDelivered> Items = new List<GoodsDelivered>();
            try
            {
                List<Int64> listayaprocesada = _context.BoletaDeSalida
                                              .Where(q => q.GoodsDeliveredId > 0)
                                              .Select(q => q.GoodsDeliveredId).ToList();

                Items = await _context.GoodsDelivered.Where(q => !listayaprocesada.Contains(q.GoodsDeliveredId)).ToListAsync();
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
        /// Obtiene los Datos de la GoodsDelivered por medio del Id enviado.
        /// </summary>
        /// <param name="GoodsDeliveredId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{GoodsDeliveredId}")]
        public async Task<IActionResult> GetGoodsDeliveredById(Int64 GoodsDeliveredId)
        {
            GoodsDelivered Items = new GoodsDelivered();
            try
            {
                Items = await _context.GoodsDelivered.Include(q=>q._GoodsDeliveredLine).Where(q => q.GoodsDeliveredId == GoodsDeliveredId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva GoodsDelivered
        /// </summary>
        /// <param name="_GoodsDelivered"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<GoodsDelivered>> Insert([FromBody]GoodsDeliveredDTO _GoodsDelivered)
        {
            GoodsDelivered _GoodsDeliveredq = new GoodsDelivered();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _GoodsDeliveredq = _GoodsDelivered;

                        BoletaDeSalida _boletadesalida = new BoletaDeSalida
                        {
                            BranchId = _GoodsDelivered.BranchId,
                            BranchName = _GoodsDelivered.BranchName,
                            CustomerId = _GoodsDelivered.CustomerId,
                            CustomerName = _GoodsDelivered.CustomerName,
                            DocumentDate = _GoodsDelivered.DocumentDate,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Marca = _GoodsDelivered.Marca,
                            Placa = _GoodsDelivered.Placa,
                            Motorista = _GoodsDelivered.Name,
                            Quantity = _GoodsDelivered._GoodsDeliveredLine.Select(q => q.QuantitySacos).Sum(),
                            SubProductId = _GoodsDelivered.SubProductId,
                            SubProductName = _GoodsDelivered.SubProductName,
                            GoodsDeliveryAuthorizationId = _GoodsDelivered.GoodsDeliveryAuthorizationId,
                            GoodsDeliveredId = _GoodsDeliveredq.GoodsDeliveredId,
                            CargadoId = 13,
                            Cargadoname = "Cargado",
                            UsuarioCreacion = _GoodsDelivered.UsuarioCreacion,
                            UsuarioModificacion = _GoodsDelivered.UsuarioModificacion,
                            UnitOfMeasureId = _GoodsDelivered._GoodsDeliveredLine[0].UnitOfMeasureId,
                            UnitOfMeasureName = _GoodsDelivered._GoodsDeliveredLine[0].UnitOfMeasureName,
                            WeightBallot = _GoodsDelivered.WeightBallot,
                        };

                        _context.BoletaDeSalida.Add(_boletadesalida);
                        await _context.SaveChangesAsync();

                        _GoodsDeliveredq.ExitTicket = _boletadesalida.BoletaDeSalidaId;

                        _context.GoodsDelivered.Add(_GoodsDeliveredq);

                        foreach (var item in _GoodsDeliveredq._GoodsDeliveredLine)
                        {
                            item.GoodsDeliveredId = _GoodsDeliveredq.GoodsDeliveredId;
                            _context.GoodsDeliveredLine.Add(item);

                            Kardex _kardexmax = await ( from c in _context.Kardex
                                                        .OrderByDescending(q => q.DocumentDate)
                                                        // .Take(1)
                                                        join d in _context.KardexLine on c.KardexId equals d.KardexId
                                                        where c.CustomerId == _GoodsDeliveredq.CustomerId && d.SubProducId == item.SubProductId
                                                        && c.DocumentName !="CD"  && d.WareHouseId == item.WareHouseId
                                                        select c
                                                      )
                                                      .FirstOrDefaultAsync();

                            if (_kardexmax == null) { _kardexmax = new Kardex(); }
                            KardexLine _KardexLine = await _context.KardexLine
                                                                         .Where(q => q.KardexId == _kardexmax.KardexId)
                                                                         .Where(q => q.SubProducId == item.SubProductId)
                                                                          .Where(q => q.WareHouseId == item.WareHouseId)
                                                                         .Where(q => q.BranchId == _GoodsDeliveredq.BranchId)
                                                                         .OrderByDescending(q => q.KardexLineId)
                                                                         .Take(1)
                                                                        .FirstOrDefaultAsync();

                            SubProduct _subproduct = await (from c in _context.SubProduct
                                                      .Where(q=>q.SubproductId==item.SubProductId)
                                                      select c
                                                      ).FirstOrDefaultAsync();

                            if (_KardexLine.Total > item.Quantity)
                            {
                                item.Total = _KardexLine.TotalCD - item.Quantity;
                            }
                            else
                            {
                                return await Task.Run(() => BadRequest("Inventario insuficiente!"));
                            }

                            _GoodsDelivered.Kardex._KardexLine.Add(new KardexLine
                            {
                                DocumentDate = _GoodsDeliveredq.DocumentDate,
                                ProducId = _GoodsDeliveredq.ProductId,
                                ProductName = _GoodsDeliveredq.ProductName,
                                SubProducId = item.SubProductId,
                                SubProductName = item.SubProductName,
                                QuantityEntry = 0,
                                QuantityOut = item.Quantity,
                                BranchId = _GoodsDeliveredq.BranchId,
                                BranchName = _GoodsDeliveredq.BranchName,
                                WareHouseId = item.WareHouseId,
                                WareHouseName = item.WareHouseName,
                                UnitOfMeasureId = item.UnitOfMeasureId,
                                UnitOfMeasureName = item.UnitOfMeasureName,
                                TypeOperationId = 1,
                                TypeOperationName = "Salida",
                                Total = item.Total,
                                TotalBags = _KardexLine.TotalBags-item.QuantitySacos  ,
                                QuantityOutCD = item.Quantity - (item.Quantity * _subproduct.Merma),
                                TotalCD = _KardexLine.TotalCD - (item.Quantity - (item.Quantity * _subproduct.Merma)),
                            });
                        }

                        await _context.SaveChangesAsync();
                        _GoodsDelivered.Kardex.DocType = 0;
                        _GoodsDelivered.Kardex.DocName = "EntregaMercaderia/GoodsDelivered";
                        _GoodsDelivered.Kardex.DocumentDate = _GoodsDeliveredq.DocumentDate;
                        _GoodsDelivered.Kardex.FechaCreacion = DateTime.Now;
                        _GoodsDelivered.Kardex.FechaModificacion = DateTime.Now;
                        _GoodsDelivered.Kardex.TypeOperationId = 1;
                        _GoodsDelivered.Kardex.TypeOperationName = "Salida";
                        _GoodsDelivered.Kardex.KardexDate = DateTime.Now;

                        _GoodsDelivered.Kardex.DocumentName = "CE";

                        _GoodsDelivered.Kardex.CustomerId = _GoodsDeliveredq.CustomerId;
                        _GoodsDelivered.Kardex.CustomerName = _GoodsDeliveredq.CustomerName;
                        _GoodsDelivered.Kardex.CurrencyId = _GoodsDeliveredq.CurrencyId;
                        _GoodsDelivered.Kardex.CurrencyName = _GoodsDeliveredq.CurrencyName;
                        _GoodsDelivered.Kardex.DocumentId = _GoodsDeliveredq.GoodsDeliveredId;
                        _GoodsDelivered.Kardex.UsuarioCreacion = _GoodsDeliveredq.UsuarioCreacion;
                        _GoodsDelivered.Kardex.UsuarioModificacion = _GoodsDeliveredq.UsuarioModificacion;
                        _context.Kardex.Add(_GoodsDelivered.Kardex);

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _GoodsDelivered.GoodsDeliveredId,
                            DocType = "GoodsDelivered",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsDelivered, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsDelivered, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _GoodsDelivered.UsuarioCreacion,
                            UsuarioModificacion = _GoodsDelivered.UsuarioModificacion,
                            UsuarioEjecucion = _GoodsDelivered.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();

                      

                        _boletadesalida.GoodsDeliveredId = _GoodsDeliveredq.GoodsDeliveredId;
                        _context.Entry(_boletadesalida).CurrentValues.SetValues((_boletadesalida));

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

            return await Task.Run(() => Ok(_GoodsDeliveredq));
        }

        /// <summary>
        /// Actualiza la GoodsDelivered
        /// </summary>
        /// <param name="_GoodsDelivered"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<GoodsDelivered>> Update([FromBody]GoodsDelivered _GoodsDelivered)
        {
            GoodsDelivered _GoodsDeliveredq = _GoodsDelivered;
            try
            {
                _GoodsDeliveredq = await (from c in _context.GoodsDelivered
                                 .Where(q => q.GoodsDeliveredId == _GoodsDelivered.GoodsDeliveredId)
                                          select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_GoodsDeliveredq).CurrentValues.SetValues((_GoodsDelivered));

                //_context.GoodsDelivered.Update(_GoodsDeliveredq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveredq));
        }

        /// <summary>
        /// Elimina una GoodsDelivered       
        /// </summary>
        /// <param name="_GoodsDelivered"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]GoodsDelivered _GoodsDelivered)
        {
            GoodsDelivered _GoodsDeliveredq = new GoodsDelivered();
            try
            {
                _GoodsDeliveredq = _context.GoodsDelivered
                .Where(x => x.GoodsDeliveredId == (Int64)_GoodsDelivered.GoodsDeliveredId)
                .FirstOrDefault();

                _context.GoodsDelivered.Remove(_GoodsDeliveredq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveredq));

        }







    }
}