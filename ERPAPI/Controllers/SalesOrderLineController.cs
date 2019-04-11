﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using coderush.Data;
using ERPAPI.Models;
//using coderush.Models.SyncfusionViewModels;
using Microsoft.AspNetCore.Authorization;
using ERP.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using ERPAPI.Helpers;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    //[Produces("application/json")]
    [Route("api/SalesOrderLine")]
    public class SalesOrderLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public SalesOrderLineController(ILogger<TaxController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;

        }

        // GET: api/SalesOrderLine
        [HttpGet]
        public async Task<IActionResult> GetSalesOrderLine()
        {
            List<SalesOrderLine> Items = new List<SalesOrderLine>();

            try
            {
                var headers = Request.Headers["SalesOrderId"];
                int salesOrderId = Convert.ToInt32(headers);
                Items = await _context.SalesOrderLine
                    .Where(x => x.SalesOrderId.Equals(salesOrderId))
                    .ToListAsync();
                int Count = Items.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
           
            return Ok(Items);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSalesOrderLineByShipmentId()
        {
             List<SalesOrderLine> Items = new List<SalesOrderLine>();

            try
            {
                var headers = Request.Headers["ShipmentId"];
                int shipmentId = Convert.ToInt32(headers);
                Shipment shipment = await _context.Shipment.SingleOrDefaultAsync(x => x.ShipmentId.Equals(shipmentId));

                if (shipment != null)
                {
                    int salesOrderId = shipment.SalesOrderId;
                    Items = await _context.SalesOrderLine
                        .Where(x => x.SalesOrderId.Equals(salesOrderId))
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
        
           // int Count = Items.Count();
            return Ok(Items);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSalesOrderLineByInvoiceId()
        {
            List<SalesOrderLine> Items = new List<SalesOrderLine>();

            try
            {
                var headers = Request.Headers["InvoiceId"];
                int invoiceId = Convert.ToInt32(headers);
                Invoice invoice = await _context.Invoice.SingleOrDefaultAsync(x => x.InvoiceId.Equals(invoiceId));

                if (invoice != null)
                {
                    int shipmentId = invoice.ShipmentId;
                    Shipment shipment = await _context.Shipment.SingleOrDefaultAsync(x => x.ShipmentId.Equals(shipmentId));
                    if (shipment != null)
                    {
                        int salesOrderId = shipment.SalesOrderId;
                        Items = await _context.SalesOrderLine
                            .Where(x => x.SalesOrderId.Equals(salesOrderId))
                            .ToListAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
          
          //  int Count = Items.Count();
            return Ok(Items);
        }

        private SalesOrderLine Recalculate(SalesOrderLine salesOrderLine)
        {
            try
            {
                salesOrderLine.Amount = Math.Round((salesOrderLine.Quantity * salesOrderLine.Price),2,MidpointRounding.AwayFromZero);
                salesOrderLine.DiscountAmount = Math.Round((((salesOrderLine.DiscountPercentage/ 100.0) * salesOrderLine.Amount) ),2,MidpointRounding.AwayFromZero);
                salesOrderLine.SubTotal = Math.Round((salesOrderLine.Amount - salesOrderLine.DiscountAmount),2,MidpointRounding.AwayFromZero);
                salesOrderLine.TaxAmount = Math.Round((((salesOrderLine.TaxPercentage/ 100.0) * salesOrderLine.SubTotal) ),2,MidpointRounding.AwayFromZero);
                salesOrderLine.Total = Math.Round((salesOrderLine.SubTotal + salesOrderLine.TaxAmount),2,MidpointRounding.AwayFromZero);

            }
            catch (Exception ex)
            {
                   _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                throw ex;
            }

            return salesOrderLine;
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

                    //update master data by its lines
                    salesOrder.Amount = lines.Sum(x => x.Amount);
                    salesOrder.SubTotal = lines.Sum(x => x.SubTotal);

                    salesOrder.Discount = lines.Sum(x => x.DiscountAmount);
                    salesOrder.Tax = lines.Sum(x => x.TaxAmount);

                    salesOrder.Total = salesOrder.Freight + lines.Sum(x => x.Total);

                    _context.Update(salesOrder);

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                 _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                throw;
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]SalesOrderLine payload)
        {
            try
            {
                SalesOrderLine salesOrderLine = new SalesOrderLine { Quantity= payload.Quantity,Price=payload.Price
                     ,SalesOrderId = payload.SalesOrderId
                    ,DiscountAmount=payload.DiscountAmount  
                    ,TaxPercentage=payload.TaxPercentage
                    ,DiscountPercentage=payload.DiscountPercentage };
               // salesOrderLine = payload;

                salesOrderLine = this.Recalculate(salesOrderLine);

                List<string> _propiedadesAComparar = new List<string>();
                _propiedadesAComparar.Add("Amount");
                 _propiedadesAComparar.Add("DiscountAmount");
                 _propiedadesAComparar.Add("SubTotal");                
                 _propiedadesAComparar.Add("TaxAmount");
                _propiedadesAComparar.Add("Total");

                EntityComparer<SalesOrderLine> comparer = new EntityComparer<SalesOrderLine>(_propiedadesAComparar,"SalesOrderId", 0);
                var res =  comparer.Compare(payload, salesOrderLine);

                if (res)
                {
                    _context.SalesOrderLine.Add(salesOrderLine);
                    await _context.SaveChangesAsync();
                    //Falta comparar los totales , haciendo suma de las lineas
                    //this.UpdateSalesOrder(salesOrderLine.SalesOrderId);
                     return Ok(salesOrderLine);
                }
                else
                {
                     return BadRequest($"Ocurrio un error, en el envio de los datos!");
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
           
        }

        [HttpPost("[action]")]
        public async  Task<IActionResult> Update([FromBody]SalesOrderLine payload)
        {
            try
            {
                SalesOrderLine salesOrderLine = payload;
                salesOrderLine = this.Recalculate(salesOrderLine);
                _context.SalesOrderLine.Update(salesOrderLine);
                await _context.SaveChangesAsync();
                this.UpdateSalesOrder(salesOrderLine.SalesOrderId);
                return Ok(salesOrderLine);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
       
        }

        [HttpPost("[action]")]
        public async  Task<IActionResult> Delete([FromBody]SalesOrderLine payload)
        {
            try
            {
                SalesOrderLine salesOrderLine = _context.SalesOrderLine
                .Where(x => x.SalesOrderLineId == (Int64)payload.SalesOrderLineId)
                .FirstOrDefault();
                _context.SalesOrderLine.Remove(salesOrderLine);
                await _context.SaveChangesAsync();
                this.UpdateSalesOrder(salesOrderLine.SalesOrderId);
                return Ok(salesOrderLine);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
         

        }


    }
}