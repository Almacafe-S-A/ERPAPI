﻿using System;
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


        /// <summary>
        /// Obtienne los productos de los recibos de mercaderias que han sido liquidados 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRecibosPendientes([FromQuery(Name = "Recibos")] int[] recibos, [FromQuery(Name = "preciocafe")] int preciocafe)
        {
            
            List<CertificadoLine> recibospendientes = new List<CertificadoLine>();
            List<GoodsReceivedLine> detallerecibos = _context.GoodsReceivedLine.Where(q => recibos.Any(a => a == q.GoodsReceivedId))
                .Include(i => i.SubProduct)
                .ToList();
            PrecioCafe preciodelcafe = _context.PrecioCafe.Where(q => q.Id == preciocafe).FirstOrDefault();
            if (preciodelcafe == null)
            {
                return BadRequest("Selecccione el valor del precio del cafe para este cliente ");
            }


            //Obtiene los productos desde la liquidacion
            List<LiquidacionLine> liquidacionLines = _context.LiquidacionLine.Where(q => q.GoodsReceivedLine != null && detallerecibos.Any(a => a.GoodsReceivedId ==q.GoodsReceivedLine.GoodsReceivedId))                                                 
                                                .Include(i => i.Liqudacion)
                                                .Include(i => i.GoodsReceivedLine)
                                                .ToList();
            try
            {
                recibospendientes =  (from lineasrecibo in liquidacionLines
                                           select new CertificadoLine()
                                           {                                               
                                               CertificadoLineId = 0,
                                               UnitMeasurName = lineasrecibo.UOM,
                                               UnitMeasureId = (long)lineasrecibo.GoodsReceivedLine.UnitOfMeasureId,
                                               Quantity = (decimal)lineasrecibo.CantidadRecibida,
                                               SubProductId = (long)lineasrecibo.SubProductId,
                                               SubProductName = lineasrecibo.SubProductName,
                                               ReciboId =  (int)lineasrecibo.GoodsReceivedLine.GoodsReceivedId,
                                               Price = (decimal)lineasrecibo.PrecioUnitarioCIF
                                               ,WarehouseId = (int)lineasrecibo.GoodsReceivedLine.WareHouseId
                                               ,WarehouseName = lineasrecibo.GoodsReceivedLine.WareHouseName
                                               ,Amount = (decimal)lineasrecibo.CantidadRecibida * (decimal)lineasrecibo.PrecioUnitarioCIF
                                               ,CantidadDisponible = lineasrecibo.GoodsReceivedLine.SaldoporCertificar == null ? 
                                                        lineasrecibo.GoodsReceivedLine.Quantity : lineasrecibo.GoodsReceivedLine.SaldoporCertificar                                               
                                               ,ValorUnitarioDerechos = (decimal)lineasrecibo.ValorUnitarioDerechos                                               
                                               ,DerechosFiscales = lineasrecibo.ValorUnitarioDerechos * lineasrecibo.CantidadRecibida
                                           }).ToList();

                ////Busca los productos tipo cafe
                List<GoodsReceivedLine> detallereciboscafe = detallerecibos
                    .Where(q => q.SubProduct.TipoCafe != TipoCaFe.NoesCafe).ToList();

                recibospendientes.AddRange(
                    (from detalle in detallereciboscafe
                     select new CertificadoLine()
                     {
                         CertificadoLineId = 0,
                         UnitMeasurName = detalle.UnitOfMeasureName ,
                         UnitMeasureId = (long)detalle.UnitOfMeasureId,
                         Quantity = (decimal)detalle.Quantity,
                         SubProductId = (long)detalle.SubProductId,
                         SubProductName = detalle.SubProductName,
                         ReciboId = (int)detalle.GoodsReceivedId,
                         Price = ObtenerPrecioCafe(detalle.SubProduct, preciodelcafe),
                         WarehouseId = (int)detalle.WareHouseId,
                         WarehouseName = detalle.WareHouseName,
                         Amount = (decimal)detalle.Quantity * ObtenerPrecioCafe(detalle.SubProduct,preciodelcafe) ,
                         CantidadDisponible = detalle.SaldoporCertificar==null?(decimal)detalle.Quantity: detalle.SaldoporCertificar,
                         ValorUnitarioDerechos = 0,
                         DerechosFiscales = 0
                     }
                     )

                    );

                int pdano = 1;
                foreach (var item in recibospendientes)
                {
                    item.PdaNo = pdano;
                    pdano++;
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