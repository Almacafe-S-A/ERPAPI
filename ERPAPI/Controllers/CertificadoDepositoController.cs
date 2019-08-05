﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
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
    [Route("api/CertificadoDeposito")]
    [ApiController]
    public class CertificadoDepositoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper mapper;

        public CertificadoDepositoController(ILogger<CertificadoDepositoController> logger, ApplicationDbContext context
            , IMapper mapper
            )
        {
            this.mapper = mapper;
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CertificadoDepositoes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCertificadoDeposito()
        {
            List<CertificadoDeposito> Items = new List<CertificadoDeposito>();
            try
            {
                Items = await _context.CertificadoDeposito.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(()=> Ok(Items));
        }

        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetCertificadoDepositoByCustomer(Int64 CustomerId)
        {
            List<CertificadoDeposito> Items = new List<CertificadoDeposito>();
            try
            {
                Items = await _context.CertificadoDeposito.Where(q=>q.CustomerId==CustomerId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(()=> Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la CertificadoDeposito por medio del Id enviado.
        /// </summary>
        /// <param name="IdCD"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdCD}")]
        public async Task<IActionResult> GetCertificadoDepositoById(Int64 IdCD)
        {
            CertificadoDeposito Items = new CertificadoDeposito();
            try
            {
                Items = await _context.CertificadoDeposito.Include(q=>q._CertificadoLine).Where(q => q.IdCD == IdCD).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(()=> Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva CertificadoDeposito
        /// </summary>
        /// <param name="_CertificadoDeposito"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CertificadoDeposito>> Insert([FromBody]CertificadoDepositoDTO _CertificadoDeposito)
        {
            CertificadoDeposito _CertificadoDepositoq = new CertificadoDeposito();
            SolicitudCertificadoDeposito _SolicitudCertificado = new SolicitudCertificadoDeposito();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        //Solicitud de certificado
                        //  _SolicitudCertificado = mapper.Map<SolicitudCertificadoDeposito>(_CertificadoDeposito);
                        _SolicitudCertificado = new SolicitudCertificadoDeposito
                        {
                            CurrencyId = _CertificadoDeposito.CurrencyId,
                            CurrencyName = _CertificadoDeposito.CurrencyName,
                            BankName = _CertificadoDeposito.BankName,
                            BankId = _CertificadoDeposito.BankId,
                            Almacenaje = _CertificadoDeposito.Almacenaje,
                            CustomerId = _CertificadoDeposito.CustomerId,
                            CustomerName = _CertificadoDeposito.CustomerName,
                            Direccion =  _CertificadoDeposito.Direccion,
                            EmpresaSeguro = _CertificadoDeposito.EmpresaSeguro,
                            Estado = _CertificadoDeposito.Estado,
                            FechaCertificado = _CertificadoDeposito.FechaCertificado,
                            FechaFirma = _CertificadoDeposito.FechaFirma,
                            FechaInicioComputo = _CertificadoDeposito.FechaInicioComputo,
                            FechaVencimientoDeposito = _CertificadoDeposito.FechaVencimientoDeposito,
                            FechaVencimiento = _CertificadoDeposito.FechaVencimiento,
                            NoCD = _CertificadoDeposito.NoCD,
                            FechaPagoBanco = _CertificadoDeposito.FechaPagoBanco,
                            NombreEmpresa= _CertificadoDeposito.NombreEmpresa,
                            LugarFirma = _CertificadoDeposito.LugarFirma,
                            MontoGarantia = _CertificadoDeposito.MontoGarantia,
                            NoPoliza = _CertificadoDeposito.NoPoliza,
                            NombrePrestatario = _CertificadoDeposito.NombrePrestatario,
                            NoTraslado = _CertificadoDeposito.NoTraslado,
                            OtrosCargos = _CertificadoDeposito.OtrosCargos,
                            PorcentajeInteresesInsolutos = _CertificadoDeposito.PorcentajeInteresesInsolutos,
                            Seguro = _CertificadoDeposito.Seguro,
                            ServicioId = _CertificadoDeposito.ServicioId,
                            ServicioName = _CertificadoDeposito.ServicioName,
                            Quantitysum = _CertificadoDeposito.Quantitysum,
                            Total= _CertificadoDeposito.Total,
                            SujetasAPago= _CertificadoDeposito.SujetasAPago,
                            WarehouseId = _CertificadoDeposito.WarehouseId,
                            WarehouseName = _CertificadoDeposito.WarehouseName,
                             

                        };

                        _context.SolicitudCertificadoDeposito.Add(_SolicitudCertificado);
                        foreach (var item in _CertificadoDeposito._CertificadoLine)
                        {
                            SolicitudCertificadoLine _SolicitudCertificadoLine = new SolicitudCertificadoLine {

                                 Amount = item.Amount,
                                 Description = item.Description,
                               //  IdCD = item.IdCD,
                                 Price = item.Price,
                                 Quantity = item.Quantity,
                                 SubProductId = item.SubProductId,
                                 SubProductName = item.SubProductName,
                                 TotalCantidad = item.TotalCantidad,
                                 UnitMeasureId = item.UnitMeasureId,
                                 UnitMeasurName = item.UnitMeasurName,
                            };

                          //  _SolicitudCertificadoLine = mapper.Map<SolicitudCertificadoLine>(item);
                            _SolicitudCertificadoLine.IdSCD = _SolicitudCertificado.IdSCD;
                            _context.SolicitudCertificadoLine.Add(_SolicitudCertificadoLine);

                        }

                        await _context.SaveChangesAsync();

                        /////////////////////////////////////////////////////////////////////////
                        //////////////////Certificado////////////////////////////////////////////

                        _CertificadoDepositoq = _CertificadoDeposito;
                        _context.CertificadoDeposito.Add(_CertificadoDepositoq);
                        // await _context.SaveChangesAsync();

                        foreach (var item in _CertificadoDeposito._CertificadoLine)
                        {
                            item.IdCD = _CertificadoDepositoq.IdCD;
                            _context.CertificadoLine.Add(item);

                            Kardex _kardexmax = await (from kdx in _context.Kardex
                                      .Where(q => q.CustomerId == _CertificadoDepositoq.CustomerId)
                                                       from kdxline in _context.KardexLine
                                                         .Where(q => q.KardexId == kdx.KardexId)
                                                           .Where(o => o.SubProducId == item.SubProductId)
                                                           .OrderByDescending(o => o.DocumentDate).Take(1)
                                                       select kdx).FirstOrDefaultAsync();

                            if (_kardexmax == null) { _kardexmax = new Kardex(); }


                            KardexLine _KardexLine = await _context.KardexLine
                                                                         .Where(q => q.KardexId == _kardexmax.KardexId)
                                                                         .Where(q => q.SubProducId == item.SubProductId)
                                                                         .OrderByDescending(q => q.KardexLineId)
                                                                         .Take(1)
                                                                        .FirstOrDefaultAsync();

                            SubProduct _subproduct = await (from c in _context.SubProduct
                                                     .Where(q => q.SubproductId == item.SubProductId)
                                                            select c
                                                     ).FirstOrDefaultAsync();


                          //  _context.GoodsReceivedLine.Add(item);

                            item.Amount = item.Quantity + _KardexLine.Total;

                            _CertificadoDeposito.Kardex._KardexLine.Add(new KardexLine
                            {
                                DocumentDate = _CertificadoDeposito.FechaCertificado,
                               // ProducId = _CertificadoDeposito.,
                               // ProductName = _GoodsReceivedq.ProductName,
                                SubProducId = item.SubProductId,
                                SubProductName = item.SubProductName,
                                QuantityEntry = item.Quantity,
                                QuantityOut = 0,
                                QuantityEntryBags = item.TotalCantidad,
                                BranchId = _CertificadoDeposito.BranchId,
                                BranchName = _CertificadoDeposito.BranchName,
                                WareHouseId = _CertificadoDeposito.WarehouseId,
                                WareHouseName = _CertificadoDeposito.WarehouseName,
                                UnitOfMeasureId = item.UnitMeasureId,
                                UnitOfMeasureName = item.UnitMeasurName,
                                TypeOperationId = 1,
                                TypeOperationName = "Entrada",
                                Total = item.Amount,
                                //TotalBags = item.QuantitySacos + _KardexLine.TotalBags,
                                //QuantityEntryCD = item.Quantity / (1 + _subproduct.Merma),
                                QuantityEntryCD = item.Quantity,
                                TotalCD = _KardexLine.TotalCD + (item.Quantity),
                            });
                        }

                        await _context.SaveChangesAsync();
                        foreach (var item in _CertificadoDeposito.RecibosAsociados)
                        {
                            RecibosCertificado _recibocertificado =
                                new RecibosCertificado
                                {
                                    IdCD = _CertificadoDepositoq.IdCD,
                                    IdRecibo = item,
                                    productocantidadbultos = _CertificadoDeposito.Quantitysum,
                                    productorecibolempiras = _CertificadoDeposito.Total,
                                    // UnitMeasureId =_CertificadoDeposito.
                                };

                            _context.RecibosCertificado.Add(_recibocertificado);
                        }

                        _CertificadoDeposito.Kardex.DocType = 0;
                        _CertificadoDeposito.Kardex.DocName = "ReciboMercaderia/GoodsReceived";
                        _CertificadoDeposito.Kardex.DocumentDate = _CertificadoDeposito.FechaCertificado;
                        _CertificadoDeposito.Kardex.FechaCreacion = DateTime.Now;
                        _CertificadoDeposito.Kardex.FechaModificacion = DateTime.Now;
                        _CertificadoDeposito.Kardex.TypeOperationId = 1;
                        _CertificadoDeposito.Kardex.TypeOperationName = "Entrada";
                        _CertificadoDeposito.Kardex.KardexDate = DateTime.Now;
                        _CertificadoDeposito.Kardex.DocumentName = "CD";
                        
                        _CertificadoDeposito.Kardex.CustomerId = _CertificadoDeposito.CustomerId;
                        _CertificadoDeposito.Kardex.CustomerName = _CertificadoDeposito.CustomerName;
                        //_CertificadoDeposito.Kardex.CurrencyId = _CertificadoDeposito.CurrencyId;
                        _CertificadoDeposito.Kardex.CurrencyName = _CertificadoDeposito.CurrencyName;
                        _CertificadoDeposito.Kardex.DocumentId = _CertificadoDeposito.IdCD;
                        _CertificadoDeposito.Kardex.UsuarioCreacion = _CertificadoDeposito.UsuarioCreacion;
                        _CertificadoDeposito.Kardex.UsuarioModificacion = _CertificadoDeposito.UsuarioModificacion;

                        _context.Kardex.Add(_CertificadoDeposito.Kardex);



                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
                //_CertificadoDepositoq = _CertificadoDeposito;
                //_context.CertificadoDeposito.Add(_CertificadoDepositoq);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(()=> Ok(_CertificadoDepositoq));
        }

        /// <summary>
        /// Actualiza la CertificadoDeposito
        /// </summary>
        /// <param name="_CertificadoDeposito"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CertificadoDeposito>> Update([FromBody]CertificadoDeposito _CertificadoDeposito)
        {
            CertificadoDeposito _CertificadoDepositoq = _CertificadoDeposito;
            try
            {
                _CertificadoDepositoq = await (from c in _context.CertificadoDeposito
                                 .Where(q => q.IdCD == _CertificadoDeposito.IdCD)
                                               select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CertificadoDepositoq).CurrentValues.SetValues((_CertificadoDeposito));

                //_context.CertificadoDeposito.Update(_CertificadoDepositoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(()=> Ok(_CertificadoDepositoq));
        }

        /// <summary>
        /// Elimina una CertificadoDeposito       
        /// </summary>
        /// <param name="_CertificadoDeposito"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CertificadoDeposito _CertificadoDeposito)
        {
            CertificadoDeposito _CertificadoDepositoq = new CertificadoDeposito();
            try
            {
                _CertificadoDepositoq = _context.CertificadoDeposito
                .Where(x => x.IdCD == (Int64)_CertificadoDeposito.IdCD)
                .FirstOrDefault();

                _context.CertificadoDeposito.Remove(_CertificadoDepositoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(()=> Ok(_CertificadoDepositoq));

        }




        [HttpPost("[action]")]
        public async Task<ActionResult<GoodsReceived>> AgruparCertificados([FromBody]List<Int64> listacertificados)
        {
            List<CertificadoDeposito> _goodsreceivedlis = new List<CertificadoDeposito>();
            try
            {
                string inparams = "";
                foreach (var item in listacertificados)
                {
                    inparams += item + ",";
                }

                inparams = inparams.Substring(0, inparams.Length - 1);

                _goodsreceivedlis = await _context.CertificadoDeposito.Include(q=>q._CertificadoLine).Where(q => listacertificados.Contains(q.IdCD)).ToListAsync();

                //_goodsreceivedlis = await _context.CertificadoDeposito.Where(q => q.IdCD == Convert.ToInt64(listacertificados[0])).FirstOrDefault();



                //using (var command = _context.Database.GetDbConnection().CreateCommand())
                //{
                //    command.CommandText = ("  SELECT  grl.SubProductId,grl.UnitMeasureId, grl.SubProductName, grl.UnitMeasurName         "
                //   + " , SUM(Quantity) AS Cantidad, SUM(grl.IdCD) AS IdCD         "                    
                //   + " , SUM(grl.Quantity) * (grl.Price)  AS Total                            "
                //   + " ,Price "
                //   + $"  FROM CertificadoLine grl                 where  CertificadoLineId in ({inparams})                                "
                //   + "  GROUP BY grl.SubProductId,grl.UnitMeasureId, grl.SubProductName, grl.UnitMeasurName,grl.IdCD,grl.Price       "
                // );

                //    _context.Database.OpenConnection();
                //    using (var result = command.ExecuteReader())
                //    {
                //        // do something with result
                //        while (await result.ReadAsync())
                //        {
                //            _goodsreceivedlis._CertificadoLine.Add(new CertificadoLine
                //            {                                
                //                SubProductId = Convert.ToInt64(result["SubProductId"]),
                //                SubProductName = result["SubProductName"].ToString(),
                //                UnitMeasureId = Convert.ToInt64(result["UnitMeasureId"]),
                //                UnitMeasurName = result["UnitMeasurName"].ToString(),
                //                Quantity = Convert.ToInt32(result["Cantidad"]),
                //                IdCD = Convert.ToInt32(result["IdCD"]),
                //                Price = Convert.ToDouble(result["Price"]),

                //               // Total = Convert.ToDouble(result["Total"]),

                //            });
                //        }
                //    }
                //}


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