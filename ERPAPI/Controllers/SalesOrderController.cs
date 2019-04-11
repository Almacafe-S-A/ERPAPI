using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using coderush.Data;
//using ERPAPI.Services;
//using coderush.Models.SyncfusionViewModels;
using Microsoft.AspNetCore.Authorization;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;

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

        public SalesOrderController(ILogger<CurrencyController> logger,ApplicationDbContext context)
                      //,  INumberSequence numberSequence)
        {
            _context = context;
           _logger= logger  ;
        }

        // GET: api/SalesOrder
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSalesOrder()
        {
            List<SalesOrder> Items = new List<SalesOrder>();
            try
            {
                Items = await _context.SalesOrder.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
           
            //int Count = Items.Count();
            return Ok(Items);
        }

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
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return Ok(salesOrders);
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
            catch (Exception ex )
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");

            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]SalesOrder salesorder)
        {
             SalesOrder salesOrder = salesorder;
            try
            {
               
                //salesOrder.SalesOrderName = _numberSequence.GetNumberSequence("SO");
                _context.SalesOrder.Add(salesOrder);
                await _context.SaveChangesAsync();
               // this.UpdateSalesOrder(salesOrder.SalesOrderId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
          
            return Ok(salesOrder);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody]SalesOrder payload)
        {
           SalesOrder salesOrder = payload;
            try
            {
              
                _context.SalesOrder.Update(salesOrder);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
         
            return Ok(salesOrder);
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