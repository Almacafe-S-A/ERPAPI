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
    [Route("api/GoodsDeliveredLine")]
    [ApiController]
    public class GoodsDeliveredLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GoodsDeliveredLineController(ILogger<GoodsDeliveredLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de GoodsDeliveredLinees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveredLine()
        {
            List<GoodsDeliveredLine> Items = new List<GoodsDeliveredLine>();
            try
            {
                Items = await _context.GoodsDeliveredLine.ToListAsync();
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
        /// Obtiene los Datos de la GoodsDeliveredLine por medio del Id enviado.
        /// </summary>
        /// <param name="GoodsDeliveredLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{GoodsDeliveredLineId}")]
        public async Task<IActionResult> GetGoodsDeliveredLineById(Int64 GoodsDeliveredLineId)
        {
            GoodsDeliveredLine Items = new GoodsDeliveredLine();
            try
            {
                Items = await _context.GoodsDeliveredLine.Where(q => q.GoodsDeliveredLinedId == GoodsDeliveredLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{GoodsDeliveredId}")]
        public async Task<IActionResult> GetGoodsDeliveredLineByGoodsDeliveredId(Int64 GoodsDeliveredId)
        {
            List<GoodsDeliveredLine> Items = new List<GoodsDeliveredLine>();
            try
            {
                Items = await _context.GoodsDeliveredLine
                             .Where(q => q.GoodsDeliveredId == GoodsDeliveredId).ToListAsync();
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
        /// Obtienne los de los controles de salidas pendientes y le resta el saldo autorizado 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveredLinePendientes([FromQuery(Name = "ARs")] int[] ARs, [FromQuery(Name = "controlid")] int controlid)
        {
            List<ControlPalletsLine> controlPalletsLines = new List<ControlPalletsLine>();
            List<GoodsDeliveryAuthorizationLine> goodsDeliveryAuthorizationsLines = new List<GoodsDeliveryAuthorizationLine>();
            List<GoodsDeliveredLine> goodsDeliveredLines = new List<GoodsDeliveredLine>();
            List<GoodsDeliveryAuthorizationLine> arNoCD = new List<GoodsDeliveryAuthorizationLine>();

            goodsDeliveryAuthorizationsLines = await _context.GoodsDeliveryAuthorizationLine
                .Where(q => ARs.Any(a => a == q.GoodsDeliveryAuthorizationId))
                .OrderBy(o => o.SaldoProducto)
                .ToListAsync();

            arNoCD = goodsDeliveryAuthorizationsLines
                .Where(q => q.NoCertificadoDeposito == 0).ToList();

            goodsDeliveryAuthorizationsLines = goodsDeliveryAuthorizationsLines
                .Where(q => q.NoCertificadoDeposito != 0).ToList();



            try
            {
                //Obtiene el detalle de los recibos liquidados
                goodsDeliveredLines = (from line in goodsDeliveryAuthorizationsLines
                                       .GroupBy(g => new {
                                                g.SubProductId,
                                                g.WarehouseName,
                                                g.UnitOfMeasureId,
                                                g.UnitOfMeasureName,
                                                g.WarehouseId,
                                                g.SubProductName,
                                                g.Pda,
                                                g.NoCertificadoDeposito,
                                                g.GoodsDeliveryAuthorizationId,
                                                g.GoodsDeliveryAuthorizationLineId
                                                }
                                              )
                                       select
                                    new GoodsDeliveredLine
                                    {
                                        SubProductId = (long)line.Key.SubProductId,
                                        QuantityAuthorized = line.Sum(s => s.Saldo),                 
                                        Quantity = 0,
                                        Description = line.Key.SubProductName,
                                        SubProductName = line.Key.SubProductName,
                                        UnitOfMeasureId = (long)line.Key.UnitOfMeasureId,
                                        UnitOfMeasureName = line.Key.UnitOfMeasureName,
                                        WareHouseId =(long)line.Key.WarehouseId,
                                        WareHouseName = line.Key.WarehouseName,
                                        NoCD = line.Key.NoCertificadoDeposito,
                                        NoAR= line.Key.GoodsDeliveryAuthorizationId,
                                        Pda = (int)line.Key.Pda,
                                        NoARLineId = (int)line.Key.GoodsDeliveryAuthorizationLineId,
                                    }).OrderBy(o => o.QuantityAuthorized).ToList();

                if (arNoCD.Count>0)
                {
                    goodsDeliveredLines.AddRange((from c in arNoCD
                                                  select new GoodsDeliveredLine {
                                                    SubProductId = c.SubProductId,
                                                    SubProductName= c.SubProductName,
                                                    Quantity= 0,
                                                    QuantityAuthorized = c.Saldo,
                                                    Description =c.SubProductName,
                                                    UnitOfMeasureId = c.UnitOfMeasureId,
                                                    UnitOfMeasureName= c.UnitOfMeasureName,
                                                    WareHouseId=(long)c.WarehouseId,
                                                    WareHouseName = c.WarehouseName,
                                                    NoCD = 0,
                                                    NoAR= c.GoodsDeliveryAuthorizationId,
                                                    Pda= 0,
                                                    NoARLineId = c.GoodsDeliveryAuthorizationLineId
                                                  
                                                  
                                                  }).ToList());
                    goodsDeliveryAuthorizationsLines.AddRange(arNoCD);
                }
                ControlPallets _ControlPallets = _context.ControlPallets
                    .Include(i => i._ControlPalletsLine)
                    .Where(q => q.ControlPalletsId == controlid).FirstOrDefault();

                controlPalletsLines = await _context.ControlPalletsLine
                    .Where(q => q.ControlPalletsId == controlid)
                    .ToListAsync();
                Boleto_Ent _Boleto_Ent = _context.Boleto_Ent.Where(q => q.clave_e == _ControlPallets.WeightBallot).Include(i => i.Boleto_Sal).FirstOrDefault();
                if (_Boleto_Ent != null && _Boleto_Ent.Boleto_Sal != null)
                {
                    _ControlPallets.taracamion = Convert.ToDouble((_Boleto_Ent.peso_e)) ;
                    _ControlPallets.pesobruto = Math.Round(Convert.ToDouble(_Boleto_Ent.Boleto_Sal.peso_s), 2, MidpointRounding.AwayFromZero);
                    _ControlPallets.pesoneto = Math.Round(Convert.ToDouble(_ControlPallets.pesobruto) - Convert.ToDouble(_ControlPallets.taracamion), 2, MidpointRounding.AwayFromZero);
                    _ControlPallets.boleto_Ent = _Boleto_Ent;

                    double yute = Math.Round((double)_ControlPallets.TotalSacosYute * 1, 2, MidpointRounding.AwayFromZero);
                    double polietileno = Math.Round(Convert.ToDouble((_ControlPallets.TotalSacosPolietileno * 0.5)), 2, MidpointRounding.AwayFromZero);
                    double tarasaco = (Math.Round(Math.Round(yute, 2) + Math.Round(polietileno, 2), 2, MidpointRounding.AwayFromZero));
                    _ControlPallets.Tara = tarasaco;
                    _ControlPallets.pesoneto2 = Convert.ToDouble(_ControlPallets.pesoneto) - Convert.ToDouble(tarasaco);

                    _ControlPallets.Tara = Convert.ToDouble(_Boleto_Ent.Convercion(_ControlPallets.Tara, _Boleto_Ent.UnidadPreferidaId));
                    _ControlPallets.pesoneto2 = Convert.ToDouble(_Boleto_Ent.Convercion(_ControlPallets.pesoneto2, _Boleto_Ent.UnidadPreferidaId));
                    _ControlPallets.pesobruto = Convert.ToDouble(_Boleto_Ent.Convercion(_ControlPallets.pesobruto, _Boleto_Ent.UnidadPreferidaId));
                    _ControlPallets.pesoneto = Convert.ToDouble(_Boleto_Ent.Convercion(_ControlPallets.pesoneto, _Boleto_Ent.UnidadPreferidaId));
                    _ControlPallets.taracamion = Convert.ToDouble(_Boleto_Ent.Convercion(_ControlPallets.taracamion, _Boleto_Ent.UnidadPreferidaId));
                    _ControlPallets.UnitOfMeasureId = _Boleto_Ent.UnidadPreferidaId;


                    goodsDeliveredLines = EntregaPesada(_ControlPallets,goodsDeliveredLines, goodsDeliveryAuthorizationsLines);
                    goodsDeliveredLines = goodsDeliveredLines.Where(q => q.QuantitySacos > 0).OrderByDescending(d => d.Quantity).ToList();
                    foreach (var item in goodsDeliveredLines)
                    {
                        if (item != goodsDeliveredLines.First())
                        {
                            item.QuantitySacos = 0;
                            //item.UnitOfMeasureName = "";

                        }
                        item.WareHouseId = (long)_ControlPallets.WarehouseId;
                        item.WareHouseName = _ControlPallets.WarehouseName;
                    }

                }
                else
                {

                    goodsDeliveredLines = EntregaNoPesada(_ControlPallets, goodsDeliveredLines, goodsDeliveryAuthorizationsLines, controlPalletsLines);
                    foreach (var item in goodsDeliveredLines)
                    {
                        if (item != goodsDeliveredLines.First())
                        {
                            item.QuantitySacos = 0;


                        }
                    }

                }
                

                //goodsDeliveredLines = goodsDeliveredLines.Where(q => q.Quantity>0).ToList();


                
                return Ok(goodsDeliveredLines.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Ocurrio un error:" + ex.Message);
            }
        }


        private List<GoodsDeliveredLine> EntregaNoPesada(
            ControlPallets _ControlPallets,
            List<GoodsDeliveredLine> goodsDeliveredLines,
            List<GoodsDeliveryAuthorizationLine> goodsDeliveryAuthorizationsLines,
            List<ControlPalletsLine> controlPalletsLines

            )

        {
            foreach (var controllinea in _ControlPallets._ControlPalletsLine)
            {
                decimal pesoentregar = (decimal)controllinea.Qty;
                int cantsacos = (int)_ControlPallets.TotalSacos;


                foreach (var item in goodsDeliveredLines)
                {
                    
                    //item.ControlPalletsId = (long)_ControlPallets.PalletId;

                    List<GoodsDeliveryAuthorizationLine> authorizationLines = goodsDeliveryAuthorizationsLines
                        .Where(q => q.SubProductId == item.SubProductId).ToList();

                    
                    decimal pesoentregalinea = 0;
                    if (!(item.SubProductId == controllinea.SubProductId 
                        && item.UnitOfMeasureId == controllinea.UnitofMeasureId))
                    {
                        continue;
                    }
                    if (item.QuantityAuthorized >= pesoentregar)
                    {
                        pesoentregalinea = pesoentregar;
                    }
                    else
                    {
                        pesoentregalinea = (decimal)item.QuantityAuthorized;

                    }
                    if (pesoentregar == 0)
                    {
                        pesoentregalinea = 0;
                    }

                    pesoentregar = pesoentregar - pesoentregalinea;

                    item.WareHouseId = (long)controllinea.WarehouseId;
                    item.WareHouseName = controllinea.WarehouseName;
                    item.Quantity = pesoentregalinea;
                    item.Total = pesoentregar;
                    item.QuantitySacos = 0;
                    item.ControlPalletsId = _ControlPallets.PalletId;

                }
            }
            

            return goodsDeliveredLines;

        }

        private List<GoodsDeliveredLine> EntregaPesada(
            ControlPallets _ControlPallets, 
            List<GoodsDeliveredLine> goodsDeliveredLines,
            List<GoodsDeliveryAuthorizationLine> goodsDeliveryAuthorizationsLines
            ) 
        
        {

            
            decimal pesoentregar = (decimal)_ControlPallets.pesoneto2;
            int cantsacos = (int)_ControlPallets.TotalSacos;


            foreach (var item in goodsDeliveredLines)
            {
                if (item.SubProductId!=null && item.SubProductId!= _ControlPallets.SubProductId)
                {
                    continue;
                }
                item.WareHouseId = (long)_ControlPallets.WarehouseId;
                item.WareHouseName = _ControlPallets.WarehouseName;
                item.ControlPalletsId = (long)_ControlPallets.PalletId;

                List<GoodsDeliveryAuthorizationLine> authorizationLines = goodsDeliveryAuthorizationsLines.Where(q => q.SubProductId == item.SubProductId).ToList();
                decimal pesoentregalinea = 0;

                if (item.QuantityAuthorized >= pesoentregar)
                {
                    pesoentregalinea = pesoentregar;
                }
                else
                {
                    pesoentregalinea = (decimal)item.QuantityAuthorized;

                }
                if (pesoentregar == 0)
                {
                    pesoentregalinea = 0;
                }

                pesoentregar = pesoentregar - pesoentregalinea;


                item.Quantity = pesoentregalinea;
                item.Total = pesoentregar;
                item.QuantitySacos = cantsacos;
                item.ControlPalletsId = _ControlPallets.PalletId;

            }

            return goodsDeliveredLines;

        }



        /// <summary>
        /// Inserta una nueva GoodsDeliveredLine
        /// </summary>
        /// <param name="_GoodsDeliveredLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<GoodsDeliveredLine>> Insert([FromBody]GoodsDeliveredLine _GoodsDeliveredLine)
        {
            GoodsDeliveredLine _GoodsDeliveredLineq = new GoodsDeliveredLine();
            try
            {
                _GoodsDeliveredLineq = _GoodsDeliveredLine;
                _context.GoodsDeliveredLine.Add(_GoodsDeliveredLineq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveredLineq));
        }

        /// <summary>
        /// Actualiza la GoodsDeliveredLine
        /// </summary>
        /// <param name="_GoodsDeliveredLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<GoodsDeliveredLine>> Update([FromBody]GoodsDeliveredLine _GoodsDeliveredLine)
        {
            GoodsDeliveredLine _GoodsDeliveredLineq = _GoodsDeliveredLine;
            try
            {
                _GoodsDeliveredLineq = await (from c in _context.GoodsDeliveredLine
                                 .Where(q => q.GoodsDeliveredLinedId == _GoodsDeliveredLine.GoodsDeliveredLinedId)
                                              select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_GoodsDeliveredLineq).CurrentValues.SetValues((_GoodsDeliveredLine));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                //_context.GoodsDeliveredLine.Update(_GoodsDeliveredLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveredLineq));
        }

        /// <summary>
        /// Elimina una GoodsDeliveredLine       
        /// </summary>
        /// <param name="_GoodsDeliveredLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]GoodsDeliveredLine _GoodsDeliveredLine)
        {
            GoodsDeliveredLine _GoodsDeliveredLineq = new GoodsDeliveredLine();
            try
            {
                _GoodsDeliveredLineq = _context.GoodsDeliveredLine
                .Where(x => x.GoodsDeliveredLinedId == (Int64)_GoodsDeliveredLine.GoodsDeliveredLinedId)
                .FirstOrDefault();

                _context.GoodsDeliveredLine.Remove(_GoodsDeliveredLineq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveredLineq));

        }







    }
}