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
    [Route("api/EndososCertificadosLine")]
    [ApiController]
    public class EndososCertificadosLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EndososCertificadosLineController(ILogger<EndososCertificadosLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de EndososCertificadosLinees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososCertificadosLine()
        {
            List<EndososCertificadosLine> Items = new List<EndososCertificadosLine>();
            try
            {
                Items = await _context.EndososCertificadosLine.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }

        /// <summary>
        /// Obtiene los Datos de la EndososCertificadosLine por medio del Id enviado.
        /// </summary>
        /// <param name="EndososCertificadosLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EndososCertificadosLineId}")]
        public async Task<IActionResult> GetEndososCertificadosLineById(Int64 EndososCertificadosLineId)
        {
            EndososCertificadosLine Items = new EndososCertificadosLine();
            try
            {
                Items = await _context.EndososCertificadosLine.Where(q => q.EndososCertificadosLineId == EndososCertificadosLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        [HttpGet("[action]/{IdCD}")]
        public async Task<IActionResult> GetDetalleCertificadoDisponibleparaEndoso(Int64 IdCD)
        {

            List<EndososCertificadosLine> pendientes = new List<EndososCertificadosLine>();



            if (_context.EndososCertificados.Where(q => q.IdCD == IdCD) == null)
            {
                return BadRequest("El Certificado se encuentra endosado, no se puede endosar");
            }



            try
            {
                List<CertificadoLine> certificadoLines = await _context.CertificadoLine.Where(q => IdCD == q.IdCD).ToListAsync();
                certificadoLines = (from cd in certificadoLines
                                    select new CertificadoLine()
                                    {
                                        Amount = cd.Amount,
                                        CantidadDisponible = cd.CantidadDisponible,
                                        CantidadDisponibleAutorizar = cd.CantidadDisponibleAutorizar.HasValue ? cd.CantidadDisponibleAutorizar : cd.Quantity,
                                        CertificadoLineId = cd.CertificadoLineId,
                                        Observaciones = cd.Observaciones,
                                        Merma = cd.Merma,
                                        PdaNo = cd.PdaNo,
                                        Quantity = cd.Quantity,
                                        Description = cd.Description,
                                        IdCD = cd.IdCD,
                                        DerechosFiscales = cd.DerechosFiscales,
                                        GoodsReceivedLine = cd.GoodsReceivedLine,
                                        GoodsReceivedLineId = cd.GoodsReceivedLineId,
                                        Price = cd.Price,
                                        ReciboId = cd.ReciboId,
                                        Saldo = cd.Saldo,
                                        SaldoEndoso = cd.SaldoEndoso,
                                        //SubProduct = cd.SubProduct,
                                        SubProductId = cd.SubProductId,
                                        SubProductName = cd.SubProductName,
                                        TotalCantidad = cd.TotalCantidad,
                                        UnitMeasureId = cd.UnitMeasureId,
                                        UnitMeasurName = cd.UnitMeasurName,
                                        ValorUnitarioDerechos = cd.ValorUnitarioDerechos,
                                        WarehouseId = cd.WarehouseId,
                                        WarehouseName = cd.WarehouseName,


                                    }).ToList();

                pendientes = (from cd in certificadoLines.Where(q => q.CantidadDisponibleAutorizar > 0 || q.CantidadDisponibleAutorizar == null)
                                          .GroupBy(g => new
                                          {
                                              g.IdCD,
                                              g.PdaNo,
                                              g.SubProductId,
                                              g.WarehouseName,
                                              g.UnitMeasurName,
                                              g.UnitMeasureId,
                                              g.WarehouseId,
                                              g.SubProductName,
                                              g.Price,
                                              g.ValorUnitarioDerechos,
                                          })
                              select new EndososCertificadosLine()
                              {
                                  EndososCertificadosLineId = 0,
                                  UnitOfMeasureName = cd.Key.UnitMeasurName,
                                  UnitOfMeasureId = (long)cd.Key.UnitMeasureId,
                                  Quantity = (decimal)cd.Sum(s => s.CantidadDisponibleAutorizar),
                                  SubProductId = (long)cd.Key.SubProductId,
                                  SubProductName = cd.Key.SubProductName,
                                  // = (int)cd.Key.IdCD,
                                  Price = (decimal)cd.Key.Price,
                                  ValorEndoso = (decimal)cd.Sum(s => s.CantidadDisponibleAutorizar) * cd.Key.Price,
                                  ValorUnitarioDerechos = cd.Key.ValorUnitarioDerechos,
                                  CertificadoLineId = 0,
                                  Saldo = (decimal)cd.Sum(s => s.CantidadDisponibleAutorizar),
                                  SaldoPrev = (decimal)cd.Sum(s => s.CantidadDisponibleAutorizar),
                                  DerechosFiscales = cd.Sum(s => s.DerechosFiscales),
                                  Pda = (int)cd.Key.PdaNo,
                              }).ToList();
                if (pendientes.Count > 7)
                {
                    return BadRequest("La autorizacion solo puede contener 7 lineas de detalle");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(pendientes);
        }

        [HttpGet("[action]/{EndososCertificadosId}")]
        public async Task<IActionResult> GetEndososCertificadosLineByEndososCertificadosId(Int64 EndososCertificadosId)
        {
            List<EndososCertificadosLine> Items = new List<EndososCertificadosLine>();
            try
            {
                Items = await _context.EndososCertificadosLine
                             .Include(d => d.EndososLiberacion)
                             .Where(q => q.EndososCertificadosId == EndososCertificadosId).ToListAsync();
                Items = (from i in Items
                         select new EndososCertificadosLine {
                             CantidadLiberacion = i.EndososLiberacion.Sum(s => s.Quantity),
                             ValorLiberado = (i.EndososLiberacion.Sum(s => s.Quantity) * i.Price),
                             EndososCertificadosLineId = i.EndososCertificadosLineId
                            ,EndososCertificadosId = i.EndososCertificadosId
                            ,UnitOfMeasureId = i.UnitOfMeasureId
                            ,UnitOfMeasureName = i.UnitOfMeasureName
                            ,CertificadoLineId = i.CertificadoLineId
                            ,SubProductId = i.SubProductId
                            ,SubProductName = i.SubProductName
                            ,Quantity = i.Quantity
                            ,Price = i.Price
                            ,ValorEndoso = i.ValorEndoso
                            ,Saldo = i.Saldo
                            ,DerechosFiscales = i.DerechosFiscales
                            ,Pda = i.Pda
                            ,ValorUnitarioDerechos = i.ValorUnitarioDerechos

                         })

                    .ToList();
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
        /// Inserta una nueva EndososCertificadosLine
        /// </summary>
        /// <param name="_EndososCertificadosLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<EndososCertificadosLine>> Insert([FromBody]EndososCertificadosLine _EndososCertificadosLine)
        {
            EndososCertificadosLine _EndososCertificadosLineq = new EndososCertificadosLine();
            try
            {
                _EndososCertificadosLineq = _EndososCertificadosLine;
                _context.EndososCertificadosLine.Add(_EndososCertificadosLineq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososCertificadosLineq);
        }

        /// <summary>
        /// Actualiza la EndososCertificadosLine
        /// </summary>
        /// <param name="_EndososCertificadosLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<EndososCertificadosLine>> Update([FromBody]EndososCertificadosLine _EndososCertificadosLine)
        {
            EndososCertificadosLine _EndososCertificadosLineq = _EndososCertificadosLine;
            try
            {
                _EndososCertificadosLineq = await (from c in _context.EndososCertificadosLine
                                 .Where(q => q.EndososCertificadosLineId == _EndososCertificadosLine.EndososCertificadosLineId)
                                                   select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_EndososCertificadosLineq).CurrentValues.SetValues((_EndososCertificadosLine));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                //_context.EndososCertificadosLine.Update(_EndososCertificadosLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososCertificadosLineq);
        }

        /// <summary>
        /// Elimina una EndososCertificadosLine       
        /// </summary>
        /// <param name="_EndososCertificadosLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]EndososCertificadosLine _EndososCertificadosLine)
        {
            EndososCertificadosLine _EndososCertificadosLineq = new EndososCertificadosLine();
            try
            {
                _EndososCertificadosLineq = _context.EndososCertificadosLine
                .Where(x => x.EndososCertificadosLineId == (Int64)_EndososCertificadosLine.EndososCertificadosLineId)
                .FirstOrDefault();

                _context.EndososCertificadosLine.Remove(_EndososCertificadosLineq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososCertificadosLineq);

        }







    }
}