using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/DashBoard")]
    [ApiController]
    public class DashBoardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public DashBoardController(ILogger<DashBoardController> logger
            , ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("[action]")]
        public ActionResult FacturacionMes([FromBody]Fechas _Fecha)
        {

            try
            {
                var result = (from allSales in
                             (from o in _context.Invoice
                              where o.InvoiceDate >= _Fecha.FechaInicio && o.InvoiceDate <= _Fecha.FechaFin
                              && o.InvoiceDate != null //&& o.estado != "Anulada"
                               select new
                              {
                                  Date = o.InvoiceDate,
                                  Sales = o.Total,
                              }
                                 ).ToList()
                              group allSales by new DateTime(allSales.Date.Year, allSales.Date.Month, 1) into g
                              select new
                              {

                                  Facturacion = g.Sum(x => x.Sales),
                                  Date = g.Key,
                              }
                );


                return Json(result);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }          
               
              
            
        }
    }
}