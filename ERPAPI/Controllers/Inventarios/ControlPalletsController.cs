using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/ControlPallets")]
    [ApiController]
    public class ControlPalletsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ControlPalletsController(ILogger<ControlPalletsController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Control Estibas 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetControlPallets()
        {
            List<ControlPallets> Items = new List<ControlPallets>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.ControlPallets.Where(p => p.EsIngreso ==1 
                        && branchlist.Any(b => p.BranchId == b.BranchId))
                        .OrderByDescending(b => b.ControlPalletsId).ToListAsync();
                }
                else
                {
                    Items = await _context.ControlPallets.OrderByDescending(b => b.ControlPalletsId).ToListAsync();
                }
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
        /// Obtiene el Listado de Control Estibas 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetControlPalletsSalida()
        {
            List<ControlPallets> controlPalletsAvailable = new List<ControlPallets>();
            try
            {
                
                controlPalletsAvailable = await _context.ControlPallets
                    .Where(q => q.EsIngreso == 0 //&& q.Estado != "Entregado"
                        // 
                    // && q.BoletaPeso.Boleto_Sal !=  null
                    ).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(controlPalletsAvailable));
        }

        /// <summary>
        /// Controles de ingresos Dsiponibles para nuevos Recibos de Mercaderias 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetControlPalletsNoSelected()
        {
            List<ControlPallets> controlPalletsAvailable = new List<ControlPallets>();
            try
            {
               /////Selecciona todos los control de ingresos con boleta de peso asociada y completos
                controlPalletsAvailable = await _context.ControlPallets
                    .Where(q => q.EsIngreso == 1 && q.Estado!="Recibido" && q.Estado != "Entregado"
                         && !_context.GoodsDelivered.Any(a => a.ControlId == q.ControlPalletsId)
                         && !_context.GoodsReceived.Any(a => a.ControlId == q.ControlPalletsId) 
                        // && q.BoletaPeso.Boleto_Sal !=  null
                    ).ToListAsync();


            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(controlPalletsAvailable));
        }


        /// <summary>
        /// Controles de ingresos Dsiponibles para nuevos Recibos de Mercaderias 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetControlPalletsNoSelectedSalida()
        {
            List<ControlPallets> controlPalletsAvailable = new List<ControlPallets>();
            try
            {
                /////Selecciona todos los control de ingresos con boleta de peso asociada y completos
                controlPalletsAvailable = await _context.ControlPallets
                    .Include(i => i._ControlPalletsLine)
                    .Where(q => q.EsIngreso == 0 && q.Estado != "Entregado"
                         && !_context.GoodsDelivered.Any(a => a.ControlId == q.ControlPalletsId)
                    // && q.BoletaPeso.Boleto_Sal !=  null
                    ).ToListAsync();

                controlPalletsAvailable = (from c in controlPalletsAvailable
                                           select new ControlPallets {
                                            ControlPalletsId = c.ControlPalletsId,
                                            Motorista = c.Motorista,
                                            BranchId = c.BranchId,
                                            BranchName = c.BranchName,
                                            IdEstado = c.IdEstado,
                                            Estado = c.Estado,
                                            WarehouseId = c.WarehouseId,
                                            WarehouseName = c.WarehouseName,
                                            DocumentDate = c.DocumentDate,
                                            ProductId = c.ProductId,
                                            Product = c.Product,
                                            ProductName = c.ProductName,
                                            SubProductId = c.SubProductId,
                                            SubProduct = c.SubProduct,
                                            SubProductName = c.SubProductName,
                                            Observaciones = c.Observaciones,
                                            CustomerId = c.CustomerId,
                                            Customer = c.Customer,
                                            CustomerName = c.CustomerName,
                                            DescriptionProduct = c.DescriptionProduct,
                                            Placa = c.Placa,
                                            Marca = c.Marca,
                                            UnitOfMeasureId = c.UnitOfMeasureId,
                                            unitOfMeasure = c.unitOfMeasure,
                                            UnitOfMeasureName = c.UnitOfMeasureName,
                                            PalletId = c.PalletId,
                                            EsIngreso = c.EsIngreso,
                                            EsSalida = c.EsSalida,
                                            SubTotal = c.SubTotal,
                                            TotalSacos = c.TotalSacos,
                                            TotalSacosPolietileno = c.TotalSacosPolietileno,
                                            TotalSacosYute = c.TotalSacosYute,
                                            SacosDevueltos = c.SacosDevueltos,
                                            QQPesoBruto = c.QQPesoBruto,
                                            Tara = c.Tara,
                                            QQPesoNeto = c.QQPesoNeto,
                                            QQPesoFinal = c.QQPesoFinal,
                                            ProductoPesado = c.ProductoPesado,
                                            FechaCreacion = c.FechaCreacion,
                                            FechaModificacion = c.FechaModificacion,
                                            UsuarioCreacion = c.UsuarioCreacion,
                                            UsuarioModificacion = c.UsuarioModificacion,
                                            Impreso = c.Impreso,
                                            WeightBallot = c.WeightBallot,
                                            BoletaPeso = c.BoletaPeso,
                                            GoodsDeliveryAuthorizationId = c.GoodsDeliveryAuthorizationId,
                                            Total = (bool)c.ProductoPesado?(decimal)c.TotalSacos:(decimal)c._ControlPalletsLine.Sum(s =>s.Qty),

                                           }).ToList();


            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(controlPalletsAvailable));
        }


        /// <summary>
        /// Obtiene los Datos de la ControlPallets por medio del Id enviado.
        /// </summary>
        /// <param name="ControlPalletsId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ControlPalletsId}")]
        public async Task<IActionResult> GetControlPalletsById(Int64 ControlPalletsId)
        {
            ControlPallets Items = new ControlPallets();
            try
            {
                Items = await _context.ControlPallets
                    .Where(q => q.ControlPalletsId == ControlPalletsId)
                    .Include(q => q._ControlPalletsLine).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva ControlPallets
        /// </summary>
        /// <param name="_ControlPallets"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ControlPallets>> Insert([FromBody]ControlPallets _ControlPallets)
        {
            ControlPallets _ControlPalletsq = new ControlPallets();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ControlPalletsq = _ControlPallets;
                        _ControlPalletsq.SubProductId = _ControlPalletsq.SubProductId == 0 ? null : _ControlPalletsq.SubProductId;
                        _ControlPalletsq.WarehouseId = _ControlPalletsq.WarehouseId == 0 ? null : _ControlPalletsq.WarehouseId;
                        _ControlPalletsq.UnitOfMeasureId = _ControlPalletsq.UnitOfMeasureId == 0 ? null : _ControlPalletsq.UnitOfMeasureId;
                        string producto = _ControlPalletsq._ControlPalletsLine.First().SubProductName;
                        if (_ControlPalletsq._ControlPalletsLine.Count >1&& !(bool)_ControlPalletsq.ProductoPesado)
                        {
                            producto = "Productos Varios";
                        }
                        _ControlPalletsq.SubProductName = producto;
                        _context.ControlPallets.Add(_ControlPalletsq);

                      
                        foreach (var item in _ControlPalletsq._ControlPalletsLine)
                        {
                            item.ControlPalletsId = _ControlPalletsq.ControlPalletsId;
                            _context.ControlPalletsLine.Add(item);
                        }

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        // await _context.SaveChangesAsync();
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ControlPallets.ControlPalletsId,
                            DocType = "ControlPallets",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_ControlPallets, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ControlPallets.UsuarioCreacion,
                            UsuarioModificacion = _ControlPallets.UsuarioModificacion,
                            UsuarioEjecucion = _ControlPallets.UsuarioModificacion,

                        });
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
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }
            ControlPallets controlPallets = new ControlPallets();
            return await Task.Run(() => Ok(controlPallets));
        }

        /// <summary>
        /// Actualiza la ControlPallets
        /// </summary>
        /// <param name="_ControlPallets"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ControlPallets>> Update([FromBody]ControlPallets _ControlPallets)
        {
            ControlPallets _ControlPalletsq = _ControlPallets;
            try
            {
                _ControlPalletsq = await (from c in _context.ControlPallets
                                 .Where(q => q.ControlPalletsId == _ControlPallets.ControlPalletsId)
                                    select c
                                ).FirstOrDefaultAsync();
                _ControlPallets.SubProductName = _ControlPallets.SubProductId == 0 ? "Productos Varios" : _ControlPallets.SubProductName;
                _ControlPallets.SubProductId=_ControlPallets.SubProductId==0? null: _ControlPallets.SubProductId;
                _ControlPallets.UnitOfMeasureId = _ControlPallets.UnitOfMeasureId == 0 ? null : _ControlPallets.UnitOfMeasureId;
                

                _context.Entry(_ControlPalletsq).CurrentValues.SetValues((_ControlPallets));

                //_context.ControlPallets.Update(_ControlPalletsq);
                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_ControlPalletsq));
        }

        /// <summary>
        /// Elimina una ControlPallets       
        /// </summary>
        /// <param name="_ControlPallets"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ControlPallets _ControlPallets)
        {
            ControlPallets _ControlPalletsq = new ControlPallets();
            try
            {
                _ControlPalletsq = _context.ControlPallets
                .Where(x => x.ControlPalletsId == (Int64)_ControlPallets.ControlPalletsId)
                .FirstOrDefault();

                _context.ControlPallets.Remove(_ControlPalletsq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_ControlPalletsq));

        }







    }
}