﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
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
    [Route("api/CertificadoLine")]
    [ApiController]
    public class CertificadoLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CertificadoLineController(ILogger<CertificadoLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CertificadoLinees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCertificadoLine()
        {
            List<CertificadoLine> Items = new List<CertificadoLine>();
            try
            {
                Items = await _context.CertificadoLine.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{IdCD}")]
        public async Task<IActionResult> GetCertificadoLineByIdCD(Int64 IdCD)
        {
            List<CertificadoLine> Items = new List<CertificadoLine>();
            try
            {
                Items = await _context.CertificadoLine
                             .Where(q => q.IdCD == IdCD).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        private List<CertificadoLine> ObtenerDetalleCertificarLiquidado(List<GoodsReceivedLine> detallerecibos)
        {


            //Obtiene los productos desde la liquidacion
            List<LiquidacionLine> liquidacionLines = _context.LiquidacionLine.Where(q => q.GoodsReceivedLine != null
                               && detallerecibos.Any(a => a.GoodsReceiveLinedId == q.GoodsReceivedLineId))
                                                .Include(i => i.Liqudacion)
                                                .Include(i => i.GoodsReceivedLine)
                                                .ToList();            
            List<CertificadoLine> detalleaCertificar = new List<CertificadoLine>();
             detalleaCertificar =  (from lineasrecibo in liquidacionLines
             select new CertificadoLine()
             {
                 CertificadoLineId = 0,
                 UnitMeasurName = lineasrecibo.UOM,
                 UnitMeasureId = (long)lineasrecibo.GoodsReceivedLine.UnitOfMeasureId,
                 Quantity = Decimal.Round( lineasrecibo.GoodsReceivedLine.SaldoporCertificar == null|| lineasrecibo.GoodsReceivedLine.SaldoporCertificar == lineasrecibo.GoodsReceivedLine.Quantity ?
                      (decimal)lineasrecibo.CantidadRecibida -  ((decimal)lineasrecibo.CantidadRecibida * (lineasrecibo.SubProduct.Merma / 100)) :
                      (decimal)lineasrecibo.GoodsReceivedLine.SaldoporCertificar,2,MidpointRounding.AwayFromZero),
                 SubProductId = (long)lineasrecibo.SubProductId,
                 SubProductName = lineasrecibo.SubProductName,
                 ReciboId = (int)lineasrecibo.GoodsReceivedLine.GoodsReceivedId,
                 Price = (decimal)lineasrecibo.PrecioUnitarioCIF,
                 WarehouseId = (int)lineasrecibo.GoodsReceivedLine.WareHouseId,
                 WarehouseName = lineasrecibo.GoodsReceivedLine.WareHouseName,
                 Amount = decimal.Round(Decimal.Round(lineasrecibo.GoodsReceivedLine.SaldoporCertificar == null || lineasrecibo.GoodsReceivedLine.SaldoporCertificar == lineasrecibo.GoodsReceivedLine.Quantity ?
                      (decimal)lineasrecibo.CantidadRecibida - ((decimal)lineasrecibo.CantidadRecibida * (lineasrecibo.SubProduct.Merma / 100)) :
                      (decimal)lineasrecibo.GoodsReceivedLine.SaldoporCertificar, 2, MidpointRounding.AwayFromZero) * (decimal)lineasrecibo.PrecioUnitarioCIF,2, MidpointRounding.AwayFromZero),
                 CantidadDisponible = lineasrecibo.GoodsReceivedLine.SaldoporCertificar == null || lineasrecibo.GoodsReceivedLine.Quantity== lineasrecibo.GoodsReceivedLine.SaldoporCertificar ?
                         Decimal.Round((decimal)lineasrecibo.CantidadRecibida -
                              ((decimal)lineasrecibo.CantidadRecibida * (lineasrecibo.SubProduct.Merma / 100)),2, MidpointRounding.AwayFromZero)
                          :Decimal.Round((decimal) lineasrecibo.GoodsReceivedLine.SaldoporCertificar, 2, MidpointRounding.AwayFromZero),
                 ValorUnitarioDerechos = (decimal)lineasrecibo.ValorUnitarioDerechos,
                 DerechosFiscales = lineasrecibo.ValorUnitarioDerechos * lineasrecibo.CantidadRecibida,
                 GoodsReceivedLineId = lineasrecibo.GoodsReceivedLineId
             }).ToList();

            return detalleaCertificar;

        }

        private List<CertificadoLine> detalleRecibosCafe(List<GoodsReceivedLine> detalleRecibo,PrecioCafe preciodelcafe) {
            List<CertificadoLine> detallependienteCertificarcafe = new List<CertificadoLine>();

            detallependienteCertificarcafe = (from detalle in detalleRecibo
                                              select new CertificadoLine()
                                              {
                                                  CertificadoLineId = 0,
                                                  UnitMeasurName = detalle.UnitOfMeasureName,
                                                  UnitMeasureId = (long)detalle.UnitOfMeasureId,
                                                  Quantity =Decimal.Round( detalle.SaldoporCertificar == null || detalle.SaldoporCertificar==detalle.Quantity ?
                                                     detalle.Quantity - (detalle.Quantity * (detalle.SubProduct.Merma / 100)) :
                                                     (decimal)detalle.SaldoporCertificar,2),
                                                  SubProductId = (long)detalle.SubProductId,
                                                  SubProductName = detalle.SubProductName,
                                                  ReciboId = (int)detalle.GoodsReceivedId,
                                                  Price = ObtenerPrecioCafe(detalle.SubProduct, preciodelcafe),
                                                  WarehouseId = (int)detalle.WareHouseId,
                                                  WarehouseName = detalle.WareHouseName,
                                                  Amount = detalle.Quantity * ObtenerPrecioCafe(detalle.SubProduct, preciodelcafe),
                                                  CantidadDisponible = detalle.SaldoporCertificar == null || detalle.SaldoporCertificar == detalle.Quantity ?
                                                     Decimal.Round(detalle.Quantity - (detalle.Quantity * (detalle.SubProduct.Merma / 100)) ,2):
                                                     Decimal.Round((decimal)detalle.SaldoporCertificar,2),
                                                  Merma = Decimal.Round(detalle.Quantity * (detalle.SubProduct.Merma / 100),2),
                                                  ValorUnitarioDerechos = 0,
                                                  DerechosFiscales = 0,
                                                  GoodsReceivedLineId = detalle.GoodsReceiveLinedId
                                              }
                         ).ToList();
            return detallependienteCertificarcafe;
        }


        private List<CertificadoLine> detalleInventarioCafe(List<InventarioBodegaHabilitada> detalleRecibo, PrecioCafe preciodelcafe)
        {
            List<CertificadoLine> detallependienteCertificarcafe = new List<CertificadoLine>();

            detallependienteCertificarcafe = (from detalle in detalleRecibo
                                              select new CertificadoLine()
                                              {
                                                  CertificadoLineId = 0,
                                                  UnitMeasurName = detalle.UnitOfMeasureName,
                                                  UnitMeasureId = (long)detalle.UnitOfMeasureId,
                                                  Quantity =Decimal.Round( (decimal)detalle.ValorPergamino - ((decimal)detalle.ValorPergamino * (detalle.Product.Merma / 100)) 
                                                  - ((decimal)detalle.ValorPergamino * (7 / 100)),2),
                                                  SubProductId = (long)detalle.ProductoId,
                                                  SubProductName = detalle.ProductoNombre,
                                                  ReciboId = (int)detalle.InventarioFisicoId,
                                                  Price = ObtenerPrecioCafe(detalle.Product, preciodelcafe),
                                                  WarehouseId = (int)detalle.WarehouseId,
                                                  WarehouseName = detalle.WarehouseName,
                                                  Amount = Decimal.Round((decimal)detalle.ValorPergamino - ((decimal)detalle.ValorPergamino * (detalle.Product.Merma / 100))
                                                  - ((decimal)detalle.ValorPergamino * (7 / 100)), 2) * (decimal)detalle.Cantidad * ObtenerPrecioCafe(detalle.Product, preciodelcafe),
                                                  CantidadDisponible = Decimal.Round((decimal)detalle.ValorPergamino 
                                                  - ((decimal)detalle.ValorPergamino * (detalle.Product.Merma / 100)),2),
                                                  ValorUnitarioDerechos = 0,
                                                  Merma = Decimal.Round(detalle.Cantidad * (detalle.Product.Merma / 100), 2),
                                                  DerechosFiscales = 0,
                                                  //GoodsReceivedLineId = detalle.GoodsReceiveLinedId
                                              }
                         ).ToList();

            List<CertificadoLine> detalleAgrupado = new List<CertificadoLine>();
            detalleAgrupado.Add(detallependienteCertificarcafe.FirstOrDefault());
            detalleAgrupado.FirstOrDefault().Quantity = detallependienteCertificarcafe.Sum(s => s.Quantity);
            detalleAgrupado.FirstOrDefault().CantidadDisponible = detallependienteCertificarcafe.Sum(s => s.CantidadDisponible);
            detalleAgrupado.FirstOrDefault().Amount = detalleAgrupado.FirstOrDefault().Quantity * detalleAgrupado.FirstOrDefault().Price;
            

            return detallependienteCertificarcafe;




        }

        private List<CertificadoLine> ObtenerDetalleCertificarInvetario(List<InventarioBodegaHabilitada> inventarioFisicoLines)
        {           
            List<CertificadoLine> detalleaCertificar = new List<CertificadoLine>();
            detalleaCertificar = (from lineasrecibo in inventarioFisicoLines
                                  select new CertificadoLine()
                                  {
                                      CertificadoLineId = 0,
                                      UnitMeasurName = lineasrecibo.UnitOfMeasureName,
                                      UnitMeasureId = (long)lineasrecibo.UnitOfMeasureId,
                                      Quantity = lineasrecibo.SaldoPendienteCertificar == 0 || 
                                            lineasrecibo.SaldoPendienteCertificar == lineasrecibo.Cantidad ?
                                         Decimal.Round(  (decimal)lineasrecibo.Cantidad - ((decimal)lineasrecibo.Cantidad * (lineasrecibo.Product.Merma / 100)) - ((decimal)lineasrecibo.Cantidad * (7/ 100)) ,2):
                                         Decimal.Round( (decimal)lineasrecibo.SaldoPendienteCertificar,2),
                                      SubProductId = (long)lineasrecibo.ProductoId,
                                      SubProductName = lineasrecibo.ProductoNombre,
                                      ReciboId = (int)lineasrecibo.Id,
                                      Price = 1,
                                      WarehouseId = (int)lineasrecibo.WarehouseId,
                                      WarehouseName = lineasrecibo.WarehouseName,
                                      Amount = (decimal)lineasrecibo.Cantidad ,
                                      CantidadDisponible = lineasrecibo.SaldoPendienteCertificar == 0 || lineasrecibo.SaldoPendienteCertificar == lineasrecibo.Cantidad ?
                                          Decimal.Round( (decimal)lineasrecibo.Cantidad - ((decimal)lineasrecibo.Cantidad * (lineasrecibo.Product.Merma / 100))- ((decimal)lineasrecibo.Cantidad * (7 / 100)) ):
                                           Decimal.Round((decimal)lineasrecibo.SaldoPendienteCertificar, 2),
                                      Merma = Decimal.Round(lineasrecibo.Cantidad * (lineasrecibo.Product.Merma / 100), 2),
                                      ValorUnitarioDerechos = 0,
                                      DerechosFiscales = 0,
                                  }).ToList();

            return detalleaCertificar;

        }




        /// <summary>
        /// Obtienne los productos de los recibos de mercaderias que han sido liquidados 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{inventario}/{preciocafe}")]
        public async Task<IActionResult> GetInventarioPendiente(int inventario, int preciocafe)
        {            
            
            List<InventarioBodegaHabilitada> inventarioBodegas = new List<InventarioBodegaHabilitada>();
            inventarioBodegas = _context.InventarioBodegaHabilitada
                .Include(p => p.Product)
                .Where(q => q.InventarioFisicoId == inventario).ToList();

            List<CertificadoLine> recibospendientes = new List<CertificadoLine>();
            PrecioCafe preciodelcafe = _context.PrecioCafe.Where(q => q.Id == preciocafe).FirstOrDefault();
            try
            {
                //Obtiene el detalle de los recibos liquidados
                if (preciodelcafe != null)
                {
                    List<InventarioBodegaHabilitada> detallereciboscafe = inventarioBodegas
                    .Where(q => q.Product.TipoCafe != TipoCaFe.NoesCafe).ToList();
                    recibospendientes.AddRange(detalleInventarioCafe(detallereciboscafe, preciodelcafe));
                }
                else
                {
                    recibospendientes.AddRange(ObtenerDetalleCertificarInvetario(inventarioBodegas));
                }
                recibospendientes = recibospendientes.OrderBy(q => q.SubProductId).OrderBy(q => q.UnitMeasureId).ToList();

                ///numerar partidas
                int pdano = 1;
                Int64 codproducto = 0;
                Int64 coduom = 0;
                decimal precio = 0;
                int vuelta = 1;
                foreach (var item in recibospendientes)
                {

                    if (vuelta != 1 && (codproducto != item.SubProductId || coduom != item.UnitMeasureId || precio != item.Price))
                    {
                        pdano++;

                    }
                    vuelta++;
                    item.PdaNo = pdano;
                    codproducto = item.SubProductId;
                    coduom = item.UnitMeasureId;
                    precio = item.Price;
                }

                return Ok(recibospendientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Ocurrio un error:" + ex.Message);
            }
        }

        /// <summary>
        /// Obtienne los productos de los recibos de mercaderias que han sido liquidados 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRecibosPendientes([FromQuery(Name = "Recibos")] int[] recibos, [FromQuery(Name = "preciocafe")] int preciocafe)
        {
            
            List<CertificadoLine> recibospendientes = new List<CertificadoLine>();
            List<GoodsReceivedLine> detallerecibos = _context.GoodsReceivedLine
                .Where(q => recibos.Any(a => a == q.GoodsReceivedId)
                                                && (q.SaldoporCertificar == null || q.SaldoporCertificar>0))
                .Include(i => i.SubProduct)
                .ToList();
            PrecioCafe preciodelcafe = _context.PrecioCafe.Where(q => q.Id == preciocafe).FirstOrDefault();
             try
            {
                //Obtiene el detalle de los recibos liquidados
                if (preciodelcafe != null)
                {
                    List<GoodsReceivedLine> detallereciboscafe = detallerecibos
                    .Where(q => q.SubProduct.TipoCafe != TipoCaFe.NoesCafe).ToList();

                    recibospendientes.AddRange(detalleRecibosCafe(detallereciboscafe,preciodelcafe));

                }
                else
                {
                    recibospendientes.AddRange(ObtenerDetalleCertificarLiquidado(detallerecibos));
                }
                recibospendientes = recibospendientes.OrderBy(q => q.SubProductId).OrderBy(q => q.UnitMeasureId).ToList();

                ///numerar partidas
                int pdano = 1;
                Int64 codproducto = 0;
                Int64 coduom = 0;
                decimal precio = 0;
                int vuelta = 1;
                foreach (var item in recibospendientes)
                {
                    
                    if (vuelta != 1 && (codproducto != item.SubProductId||coduom != item.UnitMeasureId || precio != item.Price) )
                    {
                        pdano++;
                        
                    }
                    vuelta++;
                    item.PdaNo = pdano;
                    codproducto = item.SubProductId;
                    coduom = item.UnitMeasureId;
                    precio = item.Price;
                }

                return Ok(recibospendientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Ocurrio un error:" + ex.Message);
            }
        }


        private decimal ObtenerPrecioCafe(SubProduct product, PrecioCafe precioCafe)
        {
            if (precioCafe == null)
            {
                return 0;
            }
            if (product.TipoCafe == TipoCaFe.Oro)
            {
                return (decimal)precioCafe.PrecioQQOro;
            }
            if (product.TipoCafe == TipoCaFe.Pergamino)
            {
                return (decimal)precioCafe.PercioQQPergamino;
            }
            if (product.TipoCafe == TipoCaFe.OtrasCalidades)
            {
                return (decimal)precioCafe.PrecioQQCalidadesInferiores;
            }
            return 0;
        }

        /// <summary>
        /// Obtiene los Datos de la CertificadoLine por medio del Id enviado.
        /// </summary>
        /// <param name="CertificadoLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CertificadoLineId}")]
        public async Task<IActionResult> GetCertificadoLineById(Int64 CertificadoLineId)
        {
            CertificadoLine Items = new CertificadoLine();
            try
            {
                Items = await _context.CertificadoLine.Where(q => q.CertificadoLineId == CertificadoLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva CertificadoLine
        /// </summary>
        /// <param name="_CertificadoLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CertificadoLine>> Insert([FromBody]CertificadoLine _CertificadoLine)
        {
            CertificadoLine _CertificadoLineq = new CertificadoLine();
            try
            {
                _CertificadoLineq = _CertificadoLine;
                _context.CertificadoLine.Add(_CertificadoLineq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CertificadoLineq));
        }

        /// <summary>
        /// Actualiza la CertificadoLine
        /// </summary>
        /// <param name="_CertificadoLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CertificadoLine>> Update([FromBody]CertificadoLine _CertificadoLine)
        {
            CertificadoLine _CertificadoLineq = _CertificadoLine;
            try
            {
                _CertificadoLineq = await (from c in _context.CertificadoLine
                                 .Where(q => q.CertificadoLineId == _CertificadoLine.CertificadoLineId)
                                           select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CertificadoLineq).CurrentValues.SetValues((_CertificadoLine));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                //_context.CertificadoLine.Update(_CertificadoLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CertificadoLineq));
        }

        /// <summary>
        /// Elimina una CertificadoLine       
        /// </summary>
        /// <param name="_CertificadoLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CertificadoLine _CertificadoLine)
        {
            CertificadoLine _CertificadoLineq = new CertificadoLine();
            try
            {
                _CertificadoLineq = _context.CertificadoLine
                .Where(x => x.CertificadoLineId == (Int64)_CertificadoLine.CertificadoLineId)
                .FirstOrDefault();

                _context.CertificadoLine.Remove(_CertificadoLineq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CertificadoLineq));

        }







    }
}