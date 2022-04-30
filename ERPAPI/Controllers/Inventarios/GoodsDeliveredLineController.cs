using System;
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



            ControlPallets _ControlPallets = _context.ControlPallets.Where(q => q.ControlPalletsId == controlid).FirstOrDefault();
            Boleto_Ent _Boleto_Ent = _context.Boleto_Ent.Where(q => q.clave_e == _ControlPallets.WeightBallot).Include(i => i.Boleto_Sal).FirstOrDefault();
            if (_Boleto_Ent != null && _Boleto_Ent.Boleto_Sal != null)
            {
                _ControlPallets.pesobruto = Math.Round(Convert.ToDouble(_Boleto_Ent.peso_e) / Convert.ToDouble(100), 2, MidpointRounding.AwayFromZero);
                _ControlPallets.pesoneto = Math.Round(Convert.ToDouble(_ControlPallets.pesobruto) - Convert.ToDouble(_ControlPallets.taracamion), 2, MidpointRounding.AwayFromZero);
                _ControlPallets.boleto_Ent = _Boleto_Ent;

                double yute = Math.Round((double)_ControlPallets.TotalSacosYute * 1 / 100, 2, MidpointRounding.AwayFromZero);
                double polietileno = Math.Round(Convert.ToDouble((_ControlPallets.TotalSacosPolietileno * 0.5)) / Convert.ToDouble(100), 2, MidpointRounding.AwayFromZero);
                double tarasaco = Math.Round(Math.Round(yute, 2) + Math.Round(polietileno, 2), 2, MidpointRounding.AwayFromZero);
                _ControlPallets.Tara = tarasaco;
                _ControlPallets.pesoneto2 = Convert.ToDouble(_ControlPallets.pesoneto) - Convert.ToDouble(tarasaco);
                
                if (_Boleto_Ent.peso_e > _Boleto_Ent.Boleto_Sal.peso_n)
                {
                    _ControlPallets.taracamion = Convert.ToDouble((_Boleto_Ent.peso_e - _Boleto_Ent.Boleto_Sal.peso_n) / Convert.ToDouble(100));
                }
                else if (_Boleto_Ent.peso_e < _Boleto_Ent.Boleto_Sal.peso_n)
                {
                    _ControlPallets.taracamion = Convert.ToDouble((_Boleto_Ent.peso_e)) / Convert.ToDouble(100);
                }
            }
            

           
            controlPalletsLines = await _context.ControlPalletsLine
                .Where(q => q.ControlPalletsId == controlid)
                
                .ToListAsync();
            goodsDeliveryAuthorizationsLines = await _context.GoodsDeliveryAuthorizationLine.Where(q => ARs.Any(a => a == q.GoodsDeliveryAuthorizationId)).ToListAsync();

            try
            {
                //Obtiene el detalle de los recibos liquidados
                goodsDeliveredLines = (from line in controlPalletsLines
                                       .GroupBy(g => new {
                                                g.SubProductId,
                                                g.ControlPalletsId,
                                                g.WarehouseName,
                                                g.UnitofMeasureName,
                                                g.UnitofMeasureId,
                                                g.WarehouseId,
                                                g.SubProductName,
                                                g.Totallinea,
                                                g.Estiba
                                                }
                                              )
                                       select
                                    new GoodsDeliveredLine
                                    {
                                        SubProductId = (long)line.Key.SubProductId,
                                        Quantity = line.First().Qty == null ? (decimal)_ControlPallets.pesoneto2 : (decimal)line.Sum(s => s.Qty),
                                        QuantityAuthorized = 0,
                                        Total = line.First().Qty == null ? (decimal)_ControlPallets.pesoneto2 : (decimal)line.Sum(s => s.Qty),
                                        Description = line.Key.SubProductName,
                                        SubProductName = line.Key.SubProductName,
                                        UnitOfMeasureId = (long)line.Key.UnitofMeasureId,
                                        UnitOfMeasureName = line.Key.UnitofMeasureName,
                                        WareHouseId =(long)line.Key.WarehouseId,
                                        WareHouseName = line.Key.WarehouseName,
                                        QuantitySacos =  line.Sum(s =>s.cantidadPoliEtileno) + line.Sum(s => s.cantidadYute),
                                        ControlPalletsId = line.Key.Estiba ,
                                    }).ToList();
                foreach (var item in goodsDeliveredLines)
                {
                    List<GoodsDeliveryAuthorizationLine> authorizationLines = goodsDeliveryAuthorizationsLines.Where(q => q.SubProductId == item.SubProductId).ToList();
                    if (authorizationLines.Count() == 0)
                    {
                        continue;
                    }
                    decimal saldo = authorizationLines.Sum(s => s.Quantity);
                    item.QuantityAuthorized = 0;
                    foreach (var autorizacion in authorizationLines)
                    {
                        if (autorizacion.Quantity>item.Quantity )
                        {
                            item.QuantityAuthorized = item.Quantity;
                            item.NoAR = autorizacion.GoodsDeliveryAuthorizationLineId;
                            item.NoCD = autorizacion.CertificadoLineId;
                            continue;
                        }
                        else
                        {
                            item.QuantityAuthorized += autorizacion.Quantity;
                            item.NoAR = autorizacion.GoodsDeliveryAuthorizationLineId;
                            item.NoCD = autorizacion.CertificadoLineId;
                        }
                    }
                    //item.QuantityAuthorized = authorizationLines.Sum(s => s.Quantity);
                    //item.Price = (double)authorizationLines.FirstOrDefault().Price;
                    

                }


                return Ok(goodsDeliveredLines);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Ocurrio un error:" + ex.Message);
            }
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