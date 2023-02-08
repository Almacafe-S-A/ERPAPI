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
    [Route("api/InvoiceLine")]
    [ApiController]
    public class InvoiceLineController : Controller 
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InvoiceLineController(ILogger<InvoiceLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de InvoiceLine paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInvoiceLinePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<InvoiceLine> Items = new List<InvoiceLine>();
            try
            {
                var query = _context.InvoiceLine.AsQueryable();
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
        /// Obtiene el detalle de la factura por el Id proporcionado 
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InvoiceId}")]
        public async Task<IActionResult> GetByInvoiceId(Int64 InvoiceId)
        {
            List<InvoiceLine> Items = new List<InvoiceLine>();
            try
            {
                Items = await _context.InvoiceLine
                             .Where(q => q.InvoiceId == InvoiceId).ToListAsync();
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
        /// Obtiene el detalle a facturar por cliente contrato y servicio utilizado
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="ContractId"></param>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerId}/{ContractId}/{ProductId}")]
        public async Task<IActionResult> GetByServiciosUtilizados(int CustomerId,int ContractId, int ProductId)
        {
            List<InvoiceLine> Items = new List<InvoiceLine>();
            try
            {
                //Servicios Utilizados
                List<SubServicesWareHouse> subServicesWareHouses= new List<SubServicesWareHouse>(); 

                subServicesWareHouses = await _context.SubServicesWareHouse
                    .Where(q => q.InvoiceLineId == null 
                    && q.CustomerId == CustomerId
                    && q.ServiceId== ProductId
                    ).ToListAsync();

                //Area Ocupada
                CustomerArea customerArea = await _context.CustomerArea
                    .Where(q => q.CustomerId == (long)CustomerId)
                    .LastOrDefaultAsync();

                if (customerArea!=null)
                {
                    subServicesWareHouses.Add(new SubServicesWareHouse
                    {
                        SubServiceId = 47,
                        SubServiceName = "ALMACENAJE",
                        QuantityHours = (decimal)customerArea.UsedArea,
                        ServiceId = 1,
                        ServiceName = "AlmacenajeGeneral",


                    });
                }

                



                Tax tax= _context.Tax.Where(q => q.TaxId == 1).FirstOrDefault();

                if (tax == null)
                {
                    return BadRequest("No se encontro ninngun impuesto para aplicar");
                }

                Customer customer = _context.Customer.Where(q => q.CustomerId == CustomerId).FirstOrDefault();



                Items = (from c in subServicesWareHouses
                         .GroupBy(g => new {
                                    g.ServiceId,
                                    g.ServiceName,
                                    g.CustomerId,
                                    g.CustomerName,
                                    g.SubServiceId,
                                    g.SubServiceName
                
                                 })
                         select new InvoiceLine {
                            SubProductId = c.Key.SubServiceId,
                            SubProductName = c.Key.SubServiceName,
                            ProductId = c.Key.ServiceId,
                            ProductName = c.Key.ServiceName,
                            Quantity = c.Sum(s => s.QuantityHours),
                            //UnitOfMeasureId = c.
                            Price = 0,
                            Amount = 0,
                            TaxId = tax.TaxId,
                            TaxAmount= 0,
                            TaxCode= tax.TaxCode,
                            TaxPercentage= tax.TaxPercentage,
                            
                         
                         } ).ToList();

                CustomerContract customerContract = await _context.CustomerContract
                    .Where( q => q.CustomerContractId == ContractId)
                    .Include(i => i.customerContractLines)
                    .FirstOrDefaultAsync();

                
                    foreach (var item in Items)
                    {
                        if (customerContract != null)
                        {
                            CustomerContractLines customerContractLines = customerContract.customerContractLines
                                .Where(q => q.SubProductId == item.SubProductId).FirstOrDefault();
                                if (customerContractLines != null)
                                {
                                    item.UnitOfMeasureName= customerContractLines.UnitOfMeasureName;
                                    item.UnitOfMeasureId = (int)customerContractLines.UnitOfMeasureId;
                                    item.Price = customerContractLines.Price;
                                    item.Amount = item.Price * item.Quantity;
                                }
                            
                        }
                        if (!customer.Exonerado)
                        {
                            item.TaxAmount = item.Amount * (item.TaxPercentage/100);
                            item.Total = item.Amount + item.TaxAmount;
                        }                     
                        

                    }
                



            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }






        /// <summary>
        /// Obtiene el Listado de InvoiceLinees 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInvoiceLine()
        {
            List<InvoiceLine> Items = new List<InvoiceLine>();
            try
            {
                Items = await _context.InvoiceLine.ToListAsync();
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
        /// Obtiene los Datos de la InvoiceLine por medio del Id enviado.
        /// </summary>
        /// <param name="InvoiceLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InvoiceLineId}")]
        public async Task<IActionResult> GetInvoiceLineById(Int64 InvoiceLineId)
        {
            InvoiceLine Items = new InvoiceLine();
            try
            {
                Items = await _context.InvoiceLine.Where(q => q.InvoiceLineId == InvoiceLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva InvoiceLine
        /// </summary>
        /// <param name="_InvoiceLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InvoiceLine>> Insert([FromBody]InvoiceLine _InvoiceLine)
        {
            InvoiceLine _InvoiceLineq = new InvoiceLine();
            try
            {
                _InvoiceLineq = _InvoiceLine;
                _context.InvoiceLine.Add(_InvoiceLineq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_InvoiceLineq));
        }

        /// <summary>
        /// Actualiza la InvoiceLine
        /// </summary>
        /// <param name="_InvoiceLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<InvoiceLine>> Update([FromBody]InvoiceLine _InvoiceLine)
        {
            InvoiceLine _InvoiceLineq = _InvoiceLine;
            try
            {
                _InvoiceLineq = await (from c in _context.InvoiceLine
                                 .Where(q => q.InvoiceLineId == _InvoiceLine.InvoiceLineId)
                                       select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_InvoiceLineq).CurrentValues.SetValues((_InvoiceLine));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                //_context.InvoiceLine.Update(_InvoiceLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InvoiceLineq));
        }

        /// <summary>
        /// Elimina una InvoiceLine       
        /// </summary>
        /// <param name="_InvoiceLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]InvoiceLine _InvoiceLine)
        {
            InvoiceLine _InvoiceLineq = new InvoiceLine();
            try
            {
                _InvoiceLineq = _context.InvoiceLine
                .Where(x => x.InvoiceLineId == (Int64)_InvoiceLine.InvoiceLineId)
                .FirstOrDefault();

                _context.InvoiceLine.Remove(_InvoiceLineq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InvoiceLineq));

        }







    }
}