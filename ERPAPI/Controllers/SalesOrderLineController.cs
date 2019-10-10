using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERPAPI.Models;
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

        public SalesOrderLineController(ILogger<SalesOrderLineController> logger, ApplicationDbContext context)
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

            return await Task.Run(() => Ok(Items));
            // return Ok(Items);
        }

        /// <summary>
        /// Obtiene la Linea por Id enviado
        /// </summary>
        /// <param name="_salesorderline"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> GetSalesOrderLineById([FromBody]SalesOrderLine _salesorderline)
        {
            SalesOrderLine Items = new SalesOrderLine();

            try
            {
            
                Items = await _context.SalesOrderLine
                    .Where(x => x.SalesOrderLineId== _salesorderline.SalesOrderLineId)
                    .FirstOrDefaultAsync();
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
            
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
            // return Ok(Items);
            return await Task.Run(() => Ok(Items));
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

            //int Count = Items.Count();
            // return Ok(Items);
            return await Task.Run(() => Ok(Items));
        }

        private SalesOrderLine Recalculate(SalesOrderLine salesOrderLine)
        {
            try
            {
                var discount = salesOrderLine.DiscountPercentage==0?0: salesOrderLine.DiscountPercentage / 100.0;
                var taxamount = (salesOrderLine.TaxPercentage == 0 ? 0 : (salesOrderLine.TaxPercentage / 100.0));
                salesOrderLine.Amount = Math.Round((salesOrderLine.Quantity * salesOrderLine.Price),2,MidpointRounding.AwayFromZero);
                salesOrderLine.DiscountAmount = Math.Round((((discount) * salesOrderLine.Amount) ),2,MidpointRounding.AwayFromZero);
                salesOrderLine.SubTotal = Math.Round((salesOrderLine.Amount - salesOrderLine.DiscountAmount),2,MidpointRounding.AwayFromZero);
                salesOrderLine.TaxAmount = Math.Round(((  (taxamount) * salesOrderLine.SubTotal) ),2,MidpointRounding.AwayFromZero);
                salesOrderLine.Total = Math.Round((salesOrderLine.SubTotal + salesOrderLine.TaxAmount),2,MidpointRounding.AwayFromZero);

            }
            catch (Exception ex)
            {
                   _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                throw ex;
            }

            return salesOrderLine;
        }

        private async void UpdateSalesOrder(int salesOrderId)
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

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                 _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                throw ex;
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
                    ,SubProductId = payload.SubProductId
                    ,SubProductName = payload.SubProductName
                    , ProductName = payload.ProductName
                     , ProductId = payload.ProductId
                    , Description = payload.Description
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
                    //return Ok(salesOrderLine);
                    return await Task.Run(() => Ok(salesOrderLine));
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
                //return Ok(salesOrderLine);
                return await Task.Run(() => Ok(salesOrderLine));
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
                //return Ok(salesOrderLine);
                return await Task.Run(() => Ok(salesOrderLine));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }        

        }


        [HttpGet("[action]/{SalesOrderId}")]
        public async Task<IActionResult> GetSalesOrderIdBySalesOrderLineId(Int64 SalesOrderId)
        {
            List<SalesOrderLine> Items = new List<SalesOrderLine>();
            try
            {
                Items = await _context.SalesOrderLine.Where(q => q.SalesOrderId == SalesOrderId).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(Items));
        }




    }
}