﻿
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
               var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
               int count = user.Count();
               List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
               if (branchlist.Count > 0)
               {
                   Items = await _context.GoodsDelivered.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.GoodsDeliveredId).ToListAsync();
               }
               else
               {
                   Items = await _context.GoodsDelivered.OrderByDescending(b => b.GoodsDeliveredId).ToListAsync();
               }
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
                List<int> listayaprocesada = _context.BoletaDeSalida
                                              .Where(q => q.DocumentoId > 0 && q.Cargadoname == "Cargado")
                                              .Select(q => q.DocumentoId).ToList();

                Items = await _context.GoodsDelivered.Where(q => !listayaprocesada.Contains((int)q.GoodsDeliveredId)).ToListAsync();
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

        private BoletaDeSalida InsertBoletaSalida( GoodsDelivered _GoodsDelivered) {
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
                Motorista = _GoodsDelivered.Motorista,
                Quantity = _GoodsDelivered._GoodsDeliveredLine.Select(q => q.QuantitySacos).Sum(),
                SubProductId = _GoodsDelivered.SubProductId,
                SubProductName = _GoodsDelivered.SubProductName,
                Vigilante = _GoodsDelivered.VigilanteName,
                ARNo = (int)_GoodsDelivered._GoodsDeliveredLine.FirstOrDefault().NoAR,
                DocumentoTipo = "Entrega de Mercaderias",
                DocumentoId = (int)_GoodsDelivered.GoodsDeliveredId,
                CargadoId = 13,
                Cargadoname = "Cargado",
                UsuarioCreacion = _GoodsDelivered.UsuarioCreacion,
                UsuarioModificacion = _GoodsDelivered.UsuarioModificacion,
                UnitOfMeasureId = _GoodsDelivered._GoodsDeliveredLine[0].UnitOfMeasureId,
                UnitOfMeasureName = _GoodsDelivered._GoodsDeliveredLine[0].UnitOfMeasureName,
                WeightBallot = _GoodsDelivered.BoletaPesoId,
                BoletaDeSalidaLines = new List<BoletaDeSalidaLine>(),
                ProductName = _GoodsDelivered.ProductName,
                VigilanteId = _GoodsDelivered.VigilanteId,
                Producto = _GoodsDelivered.ProductId,

            };


            foreach (var item in _GoodsDelivered._GoodsDeliveredLine)
            {
                if (item.Quantity == 0)
                {
                    _GoodsDelivered._GoodsDeliveredLine.Remove(item);
                    continue;
                }
                _boletadesalida.BoletaDeSalidaLines.Add(new BoletaDeSalidaLine()
                {
                    SubProductId = item.SubProductId,
                    SubProductName = item.SubProductName,
                    Quantity = item.QuantitySacos == 0? item.Quantity : item.QuantitySacos,
                    UnitOfMeasureId = (int)item.UnitOfMeasureId,
                    UnitOfMeasureName = item.QuantitySacos == 0? item.UnitOfMeasureName:"Sacos",
                    Warehouseid = (int)item.WareHouseId,
                    WarehouseName = item.WareHouseName,
                });
                if (item.QuantitySacos > 0)
                {
                    break;
                }
            }
            _context.BoletaDeSalida.Add(_boletadesalida);

            return _boletadesalida;


        }

        private async Task<ActionResult<Kardex>> KardexEntrega(GoodsDelivered  _GoodsDeliveredq) {
            foreach (var item in _GoodsDeliveredq._GoodsDeliveredLine)
            {
                if (item.Quantity == 0) { continue; }
                GoodsDeliveryAuthorizationLine ARL = _context.GoodsDeliveryAuthorizationLine
                    .Where(q => q.GoodsDeliveryAuthorizationLineId == item.NoARLineId)
                    .FirstOrDefault();
                if (ARL == null)
                {
                    return BadRequest("No se encontro detalle en autorizacion de retiro");
                }

                /// Sale de la AR
                _context.Kardex.Add(new Kardex
                {
                    DocumentDate = _GoodsDeliveredq.DocumentDate,
                    KardexDate = DateTime.Now,
                    ProducId = _GoodsDeliveredq.ProductId,
                    ProductName = _GoodsDeliveredq.ProductName,
                    SubProducId = Convert.ToInt32(item.SubProductId),
                    SubProductName = item.SubProductName,
                    QuantityEntry = 0,
                    QuantityOut = item.Quantity,
                    QuantityOutBags = item.QuantitySacos,
                    Precio = (decimal)item.Price,
                    BranchId = _GoodsDeliveredq.BranchId,
                    BranchName = _GoodsDeliveredq.BranchName,
                    WareHouseId = Convert.ToInt32(item.WareHouseId),
                    WareHouseName = item.WareHouseName,
                    UnitOfMeasureId = item.UnitOfMeasureId,
                    UnitOfMeasureName = item.UnitOfMeasureName,
                    TypeOperationId = TipoOperacion.Salida,
                    TypeOperationName = "Salida",
                    Total = (decimal)item.QuantityAuthorized - item.Quantity,
                    DocumentLine = (int)ARL.GoodsDeliveryAuthorizationLineId,
                    DocumentName = "Autorizacion de Retiro",
                    DocumentId = ARL.GoodsDeliveryAuthorizationId,
                    DocType = 3,
                    CustomerName = _GoodsDeliveredq.CustomerName,
                    CustomerId = _GoodsDeliveredq.CustomerId,
                    ValorTotal = ((decimal)item.QuantityAuthorized - item.Quantity) * Convert.ToDecimal(item.Price),
                    ValorMovimiento = item.Quantity * Convert.ToDecimal(item.Price),
                    SourceDocumentId = (int)_GoodsDeliveredq.GoodsDeliveredId,
                    SourceDocumentName = "Entrega de Mercaderias",
                    SourceDocumentLine = (int)item.GoodsDeliveredLinedId,
                    PdaNo = item.Pda,
                    Estiba = item.ControlPalletsId,


                });
                ARL.Saldo = ARL.Saldo - item.Quantity;

                List<CertificadoLine> cdls = _context.CertificadoLine.Where(q => q.PdaNo == ARL.Pda &&q.IdCD == ARL.NoCertificadoDeposito&&q.CantidadDisponible>0)
                    .ToList();
                decimal cantrebajar = item.Quantity;
                foreach (var cdl in cdls)
                {
                    
                    if (cantrebajar == 0 )
                    {
                        break;
                    }
                    if (cdl != null)
                    {

                        decimal cantrebajarlinea = cantrebajar <= (decimal)cdl.CantidadDisponible ?
                            //(decimal)cdl.CantidadDisponible - 
                            cantrebajar : (decimal)cdl.CantidadDisponible;
                        ////Sale del Cerrtificado
                        _context.Kardex.Add(new Kardex
                        {
                            DocumentDate = _GoodsDeliveredq.DocumentDate,
                            KardexDate = DateTime.Now,
                            ProducId = _GoodsDeliveredq.ProductId,
                            ProductName = _GoodsDeliveredq.ProductName,
                            SubProducId = Convert.ToInt32(item.SubProductId),
                            SubProductName = item.SubProductName,
                            QuantityEntry = 0,
                            QuantityOut = cantrebajarlinea,
                            QuantityOutBags = item.QuantitySacos,
                            QuantityEntryBags = 0,
                            BranchId = _GoodsDeliveredq.BranchId,
                            BranchName = _GoodsDeliveredq.BranchName,
                            WareHouseId = Convert.ToInt32(item.WareHouseId),
                            WareHouseName = item.WareHouseName,
                            UnitOfMeasureId = item.UnitOfMeasureId,
                            UnitOfMeasureName = item.UnitOfMeasureName,
                            TypeOperationId = TipoOperacion.Salida,
                            TypeOperationName = "Salida",
                            Total = Math.Abs((decimal)cdls.Sum(s => s.CantidadDisponible) - cantrebajarlinea),
                            DocumentLine = cdl.PdaNo,
                            DocumentName = "Certficado de Depósito",
                            DocumentId = cdl.IdCD,
                            DocType = 2,
                            SourceDocumentId = (int) _GoodsDeliveredq.GoodsDeliveredId,
                            SourceDocumentName = "Entrega de Mercaderias",  
                            SourceDocumentLine = (int)item.GoodsDeliveredLinedId,
                            CustomerName = _GoodsDeliveredq.CustomerName,
                            CustomerId = _GoodsDeliveredq.CustomerId,
                            PdaNo = cdl.PdaNo,
                            Precio = cdl.Price,

                            GoodsAuthorizationId= ARL.GoodsDeliveryAuthorizationId,
                            ValorTotal = Math.Abs((decimal) cdls.Sum(s => s.CantidadDisponible) - cantrebajarlinea) * Convert.ToDecimal(cdl.Price),
                            ValorMovimiento = cantrebajarlinea * Convert.ToDecimal(cdl.Price),
                            Estiba = item.ControlPalletsId,


                        });

                        

                        cdl.CantidadDisponible = cdl.CantidadDisponible- cantrebajarlinea;
                        cantrebajar = cantrebajar - cantrebajarlinea;
                    }
                }

            }
            return Ok();
        }


        /// <summary>
        /// Inserta una nueva GoodsDelivered
        /// </summary>
        /// <param name="_GoodsDelivered"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<GoodsDelivered>> Insert([FromBody]GoodsDelivered _GoodsDelivered)
        {
            GoodsDelivered _GoodsDeliveredq = new GoodsDelivered();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _GoodsDeliveredq = _GoodsDelivered;

                        _GoodsDelivered._GoodsDeliveredLine = _GoodsDelivered._GoodsDeliveredLine.Where(q => q.Quantity > 0).ToList();


                        _GoodsDeliveredq.SubProductName =String.Join(',' ,_GoodsDeliveredq._GoodsDeliveredLine.Select(s => s.SubProductName).Distinct());
                        _GoodsDeliveredq.Certificados = String.Join(',', _GoodsDeliveredq._GoodsDeliveredLine.Select(s => s.NoCD).Distinct());
                        _GoodsDeliveredq.Autorizaciones = String.Join(',', _GoodsDeliveredq._GoodsDeliveredLine.Select(s => s.NoAR).Distinct());
                        _GoodsDeliveredq.Estado = "Emitido";
                        ControlPallets _ControlPallets = _context.ControlPallets
                            .Include(i => i._ControlPalletsLine)
                            .Where(q => q.ControlPalletsId == _GoodsDeliveredq.ControlId).FirstOrDefault();
                        _ControlPallets.Estado = "Entregado";

                        Boleto_Ent _Boleto_Ent = _context.Boleto_Ent
                            .Where(q => q.clave_e == _ControlPallets.WeightBallot)
                            .Include(i => i.Boleto_Sal).FirstOrDefault();

                        if (_Boleto_Ent != null)
                        {

                            double taracamion= Convert.ToDouble((_Boleto_Ent.peso_e)) ;
                            _GoodsDeliveredq.PesoBruto =(decimal) Math.Round(Convert.ToDouble(_Boleto_Ent.Boleto_Sal.peso_s), 2, MidpointRounding.AwayFromZero);
                            _GoodsDeliveredq.PesoNeto =(decimal) Math.Round(Convert.ToDouble(_GoodsDeliveredq.PesoBruto) - taracamion, 2, MidpointRounding.AwayFromZero);
                            

                            double yute = Math.Round((double)_ControlPallets.TotalSacosYute * 1, 2, MidpointRounding.AwayFromZero);
                            double polietileno = Math.Round(Convert.ToDouble((_ControlPallets.TotalSacosPolietileno * 0.5)), 2, MidpointRounding.AwayFromZero);
                            double tarasaco = (Math.Round(Math.Round(yute, 2) + Math.Round(polietileno, 2), 2, MidpointRounding.AwayFromZero));
                            _GoodsDeliveredq.TaraUnidadMedida = (decimal) tarasaco;
                            
                            _GoodsDeliveredq.PesoNeto2 = _GoodsDeliveredq.PesoNeto - (decimal)tarasaco;

                            _GoodsDeliveredq.TaraUnidadMedida =_Boleto_Ent.Convercion(tarasaco, _Boleto_Ent.UnidadPreferidaId);
                            _GoodsDeliveredq.PesoNeto2 = _Boleto_Ent.Convercion((double)_GoodsDeliveredq.PesoNeto2, _Boleto_Ent.UnidadPreferidaId);
                            _GoodsDeliveredq.PesoBruto = _Boleto_Ent.Convercion((double)_GoodsDeliveredq.PesoBruto, _Boleto_Ent.UnidadPreferidaId);
                            _GoodsDeliveredq.PesoNeto = _Boleto_Ent.Convercion((double)_GoodsDeliveredq.PesoNeto, _Boleto_Ent.UnidadPreferidaId);
                            _GoodsDeliveredq.TaraTransporte = _Boleto_Ent.Convercion(taracamion, _Boleto_Ent.UnidadPreferidaId);

                            


                        }
                        else
                        {
                            _GoodsDeliveredq.PesoNeto2 = (decimal)_ControlPallets._ControlPalletsLine.Sum(s => s.Qty);
                        }

                        if (_GoodsDeliveredq.PesoNeto2 != _GoodsDeliveredq._GoodsDeliveredLine.Sum(x => x.Quantity))
                        {
                            return BadRequest("Los el peso del detalle no coincide con el peso de la boleta");
                        }


                        _context.GoodsDelivered.Add(_GoodsDeliveredq);
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();
                        await _context.SaveChangesAsync();

                        BoletaDeSalida _boletadesalida =  InsertBoletaSalida(_GoodsDeliveredq);
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();                        
                        await _context.SaveChangesAsync();

                        await KardexEntrega(_GoodsDelivered);
                        _GoodsDeliveredq.ExitTicket = _boletadesalida.BoletaDeSalidaId;
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();
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

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

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

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

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