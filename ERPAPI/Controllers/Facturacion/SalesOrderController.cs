﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
   [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    //[Produces("application/json")]
    [Route("api/SalesOrder")]
    [ApiController]
    public class SalesOrderController : Controller
    {
        private readonly ApplicationDbContext _context;
         private readonly ILogger _logger;
      //  private readonly INumberSequence _numberSequence;

        public SalesOrderController(ILogger<SalesOrderController> logger,ApplicationDbContext context)
                      //,  INumberSequence numberSequence)
        {
            _context = context;
           _logger= logger  ;
        }

        /// <summary>
        /// Obtiene el Listado de SalesOrder paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSalesOrderPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<SalesOrder> Items = new List<SalesOrder>();
            try
            {
                var query = _context.SalesOrder.AsQueryable();
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
        /// Obtiene una cotizacion 
        /// </summary>
        /// <returns></returns>
        // GET: api/SalesOrder
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSalesOrder()
        {
            List<SalesOrder> Items = new List<SalesOrder>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.SalesOrder.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.SalesOrderId).ToListAsync();
                }
                else
                {
                    Items = await _context.SalesOrder.OrderByDescending(b => b.SalesOrderId).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetSalesOrderByCustomerId(Int64 CustomerId)
        {
            List<SalesOrder> Items = new List<SalesOrder>();
            try
            {
                Items = await _context.SalesOrder.Where(q=>q.CustomerId== CustomerId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{SalesOrderId}")]
        public async Task<IActionResult> GetSalesOrderById(Int64 SalesOrderId)
        {
            SalesOrder Items = new SalesOrder();
            try
            {
                Items = await _context.SalesOrder.Include(q=>q.SalesOrderLines).Where(q => q.SalesOrderId == SalesOrderId).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

       


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotShippedYet()
        {
            List<SalesOrder> salesOrders = new List<SalesOrder>();
            try
            {
                List<Shipment> shipments = new List<Shipment>();
                shipments = await _context.Shipment.ToListAsync();
                List<int> ids = new List<int>();

                foreach (var item in shipments)
                {
                    ids.Add(item.SalesOrderId);
                }

                salesOrders = await _context.SalesOrder
                    .Where(x => !ids.Contains(x.SalesOrderId))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }
            return await Task.Run(() => Ok(salesOrders));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                SalesOrder result = await _context.SalesOrder
              .Where(x => x.SalesOrderId.Equals(id))
              .Include(x => x.SalesOrderLines)
              .FirstOrDefaultAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
          
        }

        private void UpdateSalesOrder(int salesOrderId)
        {
            try
            {
                SalesOrder salesOrder = new SalesOrder();
                salesOrder = _context.SalesOrder
                    .Where(x => x.SalesOrderId.Equals(salesOrderId))
                    .FirstOrDefault();

                if (salesOrder != null)
                {
                    List<SalesOrderLine> lines = new List<SalesOrderLine>();
                    lines = _context.SalesOrderLine.Where(x => x.SalesOrderId.Equals(salesOrderId)).ToList();

                   

                    _context.Update(salesOrder);

                    _context.SaveChanges();
                }
            }
            catch (Exception ex )
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");

            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]SalesOrder salesorder)
        {

            var validacotizacion = ValidSalesOrder(salesorder);

            if (validacotizacion is BadRequestObjectResult)
            {
                return validacotizacion;
            }
            SalesOrder salesOrder = salesorder;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    if (salesOrder.CustomerId == 0)
                    {
                        salesOrder.NameContract = "Cliente Potencial - " + salesOrder.CustomerName;
                    }
                    try
                    {
                        _context.SalesOrder.Add(salesOrder);
                        //await _context.SaveChangesAsync();

                        foreach (var item in salesorder.SalesOrderLines)
                        {
                            item.SalesOrderLineId = 0;
                            item.SalesOrderId = salesorder.SalesOrderId;
                            _context.SalesOrderLine.Add(item);
                        }
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = salesOrder.SalesOrderId,
                            DocType = "SalesOrder",

                            ClaseInicial =
                             Newtonsoft.Json.JsonConvert.SerializeObject(salesorder, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(salesOrder, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = salesOrder.UsuarioCreacion,
                            UsuarioModificacion = salesOrder.UsuarioModificacion,
                            UsuarioEjecucion = salesOrder.UsuarioModificacion,

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
               // this.UpdateSalesOrder(salesOrder.SalesOrderId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(salesOrder));
        }

        /// <summary>
        /// Valida las Cotizaciones
        /// </summary>
        /// <returns></returns>

         IActionResult ValidSalesOrder(SalesOrder salesOrder) {
            int? plazoPrecioFijo = salesOrder.PlazoMeses;
            int? plazoPrecioVariable = salesOrder.PlazoMeses;
            string error = "";

            if (salesOrder.PlazoMeses<=0)
            {
                return BadRequest("El plazo de la cotizacion no puede ser cero");
            }

            foreach (var item in salesOrder.SalesOrderLines)
            {
                plazoPrecioFijo = plazoPrecioFijo - (item.TipoCobroName.Equals("Cobro Fijo")?item.PeriodoCobro:0);
                plazoPrecioVariable = plazoPrecioFijo - (item.TipoCobroName.Equals("Cobro Variable") ? item.PeriodoCobro : 0);

            }
            if (plazoPrecioFijo!= 0)
            {
                return BadRequest("El plazo del detalle de costos fijos no coincide con el plazo de la cotizacion , Favor haga coincidir el encabezado con el detalle");
            }

            if (plazoPrecioVariable < 0)
            {
                return BadRequest("El plazo del detalle de costos fijos no coincide con el plazo de la cotizacion , Favor haga coincidir el encabezado con el detalle");
            }

            return Ok();
        
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody]SalesOrder _salesorder)
        {

            var validacotizacion =  ValidSalesOrder(_salesorder);

            if (validacotizacion is BadRequestObjectResult)
            {
                return validacotizacion;
            }
        
            try
            {
                SalesOrder salesOrderq   = (from c in _context.SalesOrder
                                      .Where(q => q.SalesOrderId == _salesorder.SalesOrderId)
                                                    select c
                                     ).FirstOrDefault();

                _salesorder.FechaCreacion = salesOrderq.FechaCreacion;
                _salesorder.UsuarioCreacion = salesOrderq.UsuarioCreacion;

                _context.Entry(salesOrderq).CurrentValues.SetValues((_salesorder));
                //_context.SalesOrder.Update(_salesorder);
                await _context.SaveChangesAsync();


                BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                {
                    IdOperacion = _salesorder.SalesOrderId,
                    DocType = "SalesOrder",

                    ClaseInicial =
                     Newtonsoft.Json.JsonConvert.SerializeObject(salesOrderq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_salesorder, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    Accion = "Update",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = _salesorder.UsuarioCreacion,
                    UsuarioModificacion = _salesorder.UsuarioModificacion,
                    UsuarioEjecucion = _salesorder.UsuarioModificacion,

                });

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_salesorder));
        }

        [HttpPost("[action]")]
        public async  Task<IActionResult> Remove([FromBody]SalesOrder payload)
        {
            try
            {
                SalesOrder salesOrder = _context.SalesOrder

              .Where(x => x.SalesOrderId == (int)payload.SalesOrderId)
              .FirstOrDefault();
                _context.SalesOrder.Remove(salesOrder);
                await _context.SaveChangesAsync();

                BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                {
                    IdOperacion = payload.SalesOrderId,
                    DocType = "SalesOrder",
                    ClaseInicial =
                     Newtonsoft.Json.JsonConvert.SerializeObject(payload, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(salesOrder, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    Accion = "Update",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = payload.UsuarioCreacion,
                    UsuarioModificacion = payload.UsuarioModificacion,
                    UsuarioEjecucion = payload.UsuarioModificacion,

                });
                await _context.SaveChangesAsync();

                return Ok(salesOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
               
            }          

        }



    }
}