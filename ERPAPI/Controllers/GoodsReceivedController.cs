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
    [Route("api/GoodsReceived")]
    [ApiController]
    public class GoodsReceivedController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GoodsReceivedController(ILogger<GoodsReceivedController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de GoodsReceivedes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsReceived()
        {
            List<GoodsReceived> Items = new List<GoodsReceived>();
            try
            {
                Items = await _context.GoodsReceived.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la GoodsReceived por medio del Id enviado.
        /// </summary>
        /// <param name="GoodsReceivedId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{GoodsReceivedId}")]
        public async Task<IActionResult> GetGoodsReceivedById(Int64 GoodsReceivedId)
        {
            GoodsReceived Items = new GoodsReceived();
            try
            {
                Items = await _context.GoodsReceived.Where(q => q.GoodsReceivedId == GoodsReceivedId).Include(q => q._GoodsReceivedLine).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return Ok(Items);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsReceivedNoSelected()
        {
            List<GoodsReceived> Items = new List<GoodsReceived>();
            try
            {
                List<Int64> listayaprocesada = _context.RecibosCertificado
                                              .Where(q => q.IdRecibo > 0)
                                              .Select(q => q.IdRecibo).ToList();
                Items = await _context.GoodsReceived.Where(q => !listayaprocesada.Contains(q.GoodsReceivedId)).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva GoodsReceived
        /// </summary>
        /// <param name="_GoodsReceived"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<GoodsReceived>> Insert([FromBody]GoodsReceivedDTO _GoodsReceived)
        {
            GoodsReceived _GoodsReceivedq = new GoodsReceived();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _GoodsReceivedq = _GoodsReceived;

                        _context.GoodsReceived.Add(_GoodsReceivedq);
                        // await _context.SaveChangesAsync();

                        foreach (var item in _GoodsReceivedq._GoodsReceivedLine)
                        {
                            item.GoodsReceivedId = _GoodsReceivedq.GoodsReceivedId;
                            _context.GoodsReceivedLine.Add(item);

                            _GoodsReceived.Kardex._KardexLine.Add(new KardexLine
                            {
                                DocumentDate = _GoodsReceivedq.DocumentDate,
                                ProducId = _GoodsReceivedq.ProductId,
                                ProductName = _GoodsReceivedq.ProductName,
                                SubProducId = _GoodsReceivedq.SubProductId,
                                SubProductName = _GoodsReceivedq.SubProductName,
                                QuantityEntry = item.Quantity,
                                QuantityOut =0,
                                BranchId = _GoodsReceivedq.BranchId,
                                BranchName = _GoodsReceivedq.BranchName,
                                WareHouseId = item.WareHouseId,
                                WareHouseName = item.WareHouseName,
                                UnitOfMeasureId = item.UnitOfMeasureId,
                                UnitOfMeasureName = item.UnitOfMeasureName,
                                TypeOperationId = 1,
                                TypeOperationName = "Entrada",
                        });
                        }

                        await _context.SaveChangesAsync();
                        _GoodsReceived.Kardex.DocType = 0;                      
                        _GoodsReceived.Kardex.DocName = "ReciboMercaderia/GoodsReceived";
                        _GoodsReceived.Kardex.DocumentDate = _GoodsReceivedq.DocumentDate;
                        _GoodsReceived.Kardex.FechaCreacion = DateTime.Now;
                        _GoodsReceived.Kardex.FechaModificacion = DateTime.Now;
                        _GoodsReceived.Kardex.TypeOperationId = 1;
                        _GoodsReceived.Kardex.TypeOperationName = "Entrada";
                        _GoodsReceived.Kardex.KardexDate = DateTime.Now;
                        _GoodsReceived.Kardex.DocumentName = "GoodsReceived";

                        _GoodsReceived.Kardex.CustomerId = _GoodsReceivedq.CustomerId;
                        _GoodsReceived.Kardex.CustomerName = _GoodsReceivedq.CustomerName;
                        _GoodsReceived.Kardex.CurrencyId = _GoodsReceivedq.CurrencyId;
                        _GoodsReceived.Kardex.CurrencyName = _GoodsReceivedq.CurrencyName;
                        _GoodsReceived.Kardex.DocumentId = _GoodsReceivedq.GoodsReceivedId;
                        _context.Kardex.Add(_GoodsReceived.Kardex);

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
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsReceivedq));
        }

        /// <summary>
        /// Actualiza la GoodsReceived
        /// </summary>
        /// <param name="_GoodsReceived"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<GoodsReceived>> Update([FromBody]GoodsReceived _GoodsReceived)
        {
            GoodsReceived _GoodsReceivedq = _GoodsReceived;
            try
            {
                _GoodsReceivedq = await (from c in _context.GoodsReceived
                                 .Where(q => q.GoodsReceivedId == _GoodsReceived.GoodsReceivedId)
                                         select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_GoodsReceivedq).CurrentValues.SetValues((_GoodsReceived));

                //_context.GoodsReceived.Update(_GoodsReceivedq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsReceivedq));
        }

        /// <summary>
        /// Elimina una GoodsReceived       
        /// </summary>
        /// <param name="_GoodsReceived"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]GoodsReceived _GoodsReceived)
        {
            GoodsReceived _GoodsReceivedq = new GoodsReceived();
            try
            {
                _GoodsReceivedq = _context.GoodsReceived
                .Where(x => x.GoodsReceivedId == (Int64)_GoodsReceived.GoodsReceivedId)
                .FirstOrDefault();

                _context.GoodsReceived.Remove(_GoodsReceivedq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsReceivedq));

        }


        [HttpPost("[action]")]
        public async Task<ActionResult<GoodsReceived>> AgruparRecibos([FromBody]List<Int64> listarecibos)
        {
            GoodsReceived _goodsreceivedlis = new GoodsReceived();
            try
            {
                string inparams = "";
                foreach (var item in listarecibos)
                {
                    inparams += item +",";
                }

                inparams = inparams.Substring(0,inparams.Length-1);
                // string[] ids = listarecibos.Split(',');
                _goodsreceivedlis = await _context.GoodsReceived.Where(q => q.GoodsReceivedId == Convert.ToInt64(listarecibos[0])).FirstOrDefaultAsync();
                //List<GoodsReceivedLineDTO> d = _context.Query<GoodsReceivedLineDTO>().FromSql (
                //("  SELECT  grl.SubProductId, grl.SubProductName, grl.UnitOfMeasureName         "
                // + " , SUM(Quantity) AS Cantidad, SUM(grl.QuantitySacos) AS CantidadSacos         "
                // + "  , SUM(grl.Price) Precio, SUM(grl.Total) AS Total                            "
                // + $"  FROM GoodsReceivedLine grl                 where  GoodsReceivedId in ({inparams})                                "
                // + "  GROUP BY grl.SubProductId, grl.SubProductName, grl.UnitOfMeasureName        "
                // )
                //    ).AsNoTracking().ToList();
                //.Where(q => listarecibos.Contains(q.GoodsReceivedId)).ToList();

              //  List<GoodsReceivedLineDTO> d = new List<GoodsReceivedLineDTO>();
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = ("  SELECT  grl.SubProductId,grl.UnitOfMeasureId, grl.SubProductName, grl.UnitOfMeasureName         "
                 + " , SUM(Quantity) AS Cantidad, SUM(grl.QuantitySacos) AS CantidadSacos         "
                  //+ "  , SUM(grl.Price) Precio, SUM(grl.Total) AS Total                            "
                  + "  , grl.Price as Precio, SUM(grl.Quantity) * (grl.Price)  AS Total                            "
                 + $"  FROM GoodsReceivedLine grl                 where  GoodsReceivedId in ({inparams})                                "
                 + "  GROUP BY grl.SubProductId,grl.UnitOfMeasureId, grl.SubProductName, grl.UnitOfMeasureName,grl.Price        "
                 );

                   _context.Database.OpenConnection();
                    using (var result = command.ExecuteReader())
                    {
                        // do something with result
                        while (await result.ReadAsync())
                        {
                            _goodsreceivedlis._GoodsReceivedLine.Add(new GoodsReceivedLine {
                                SubProductId = Convert.ToInt64(result["SubProductId"]),
                                SubProductName = result["SubProductName"].ToString(),
                                UnitOfMeasureId = Convert.ToInt64(result["UnitOfMeasureId"]),
                                UnitOfMeasureName = result["UnitOfMeasureName"].ToString(),
                                Quantity = Convert.ToInt32(result["Cantidad"]),
                                QuantitySacos = Convert.ToInt32(result["CantidadSacos"]),
                                Price = Convert.ToDouble(result["Precio"]),
                                Total = Convert.ToDouble(result["Total"]),
                                
                            });
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                throw ex;
            }

            return await Task.Run(() => Ok(_goodsreceivedlis));
        }





    }
}