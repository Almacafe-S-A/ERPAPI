/********************************************************************************************************
-- NAME   :  CRUDDashBoard
-- PROPOSE:  show methods DashBoard
REVISIONS:
version              Date                Author                        Description
----------           -------------       ---------------               -------------------------------
6.0                  11/12/2019          Marvin.Guillen                 Changes of modificate date
5.0                  25/05/2019          Oscar.Gomez                    Metodo de seguridad
4.0                  25/05/2019          Freddy.Chinchilla              Changes of Dashbord
3.0                  19/06/2019          Freddy.Chinchilla              Changes of task return
2.0                  21/05/2019          Freddy.Chinchilla              Changes of dashboard mejora
1.0                  06/05/2019          Freddy.Chinchilla              Creation of controller
********************************************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Helpers;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost("[action]")]
        public async Task<ActionResult> FacturacionByProductByYear([FromBody]Fechas _Fecha)
        {

            try
            {
                _Fecha.FechaInicio = Convert.ToDateTime(_Fecha.Year + "-01-01");
                _Fecha.FechaFin = Convert.ToDateTime(_Fecha.Year + "-12-31");

                var result = (from allSales in
                             (from o in _context.Invoice
                              .OrderBy(q => q.InvoiceDate)
                              where o.InvoiceDate >= _Fecha.FechaInicio && o.InvoiceDate <= _Fecha.FechaFin
                              && o.InvoiceDate != null && o.ProductId == _Fecha.Id //&& o.estado != "Anulada"
                              select new
                              {
                                  Date = o.InvoiceDate,
                                  Sales = o.Total,
                              }
                                 ).ToList()
                              group allSales by new DateTime(allSales.Date.Year, allSales.Date.Month, 1) into g
                              select new FacturacionMensual
                              {
                                  Facturacion = g.Sum(x => x.Sales),
                                  Date = g.Key,
                              }
                           ).ToList();


                for (int i = 1; i <= 12; i++)
                {
                    bool existe = result.Where(q => q.Date.Month == i).Count() > 0 ? true : false;
                    if (!existe)
                    {
                        result.Add(new FacturacionMensual
                        { Date = Convert.ToDateTime(_Fecha.Year + "-" + i.ToString().PadLeft(2,'0') + "-01"), Facturacion = 0 });
                    }
                }

                result = result.OrderBy(q => q.Date).ToList();

                return await Task.Run(() => Json(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityBranch([FromBody]DashBoard _sarpara)
        {
            try
            {
                var Items = await _context.Branch.
                    Where(a => a.FechaCreacion >= _sarpara.BeginDate && 
                    a.FechaCreacion <= _sarpara.EndDate).                    
                    CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityRoles([FromBody]DashBoard _sarpara)
        {
            try
            {
                var Items = await _context.Roles.
                    Where(a => a.FechaCreacion >= _sarpara.BeginDate &&
                    a.FechaCreacion <= _sarpara.EndDate).
                    CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityDepartamentos([FromBody]DashBoard _sarpara)
        {
            try
            {
                var Items = await _context.Departamento.
                    Where(a => a.FechaCreacion >= _sarpara.BeginDate &&
                    a.FechaCreacion <= _sarpara.EndDate).
                    CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityUserRol([FromBody]DashBoard _sarpara)
        {
            try
            {
                var Items = await _context.UserRoles.
                    Where(a => a.FechaCreacion >= _sarpara.BeginDate &&
                    a.FechaCreacion <= _sarpara.EndDate).
                    CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityInvoices()
        {
            try
            {
                var Items = await _context.Invoice.CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantitySalesOrders()
        {
            try
            {
                var Items = await _context.SalesOrder.CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityCertificadoDeposito()
        {
            try
            {
                var Items = await _context.CertificadoDeposito.CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityJournalEntriesAprovement()
        {
            try
            {
                var Items = await _context.JournalEntry.Where(w => w.EstadoId == 5  &&  w.TypeOfAdjustmentId ==65 ).CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityJournalPendientes()
        {
            try
            {
                var Items = await _context.JournalEntry.Where(w => w.EstadoId == 5 && w.TypeOfAdjustmentId == 66).CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityFacturasproveedor()
        {
            try
            {
                DateTime fechaactual = DateTime.Now.AddDays(-1);
          
            
                var Items = await _context.VendorInvoice.Where(w => w.ExpirationDate.ToString("yyyy-MM-dd") == fechaactual.ToString("yyyy-MM-dd")).CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityJournalEntriestotal()
        {
            try
            {
                DateTime fechadehoy = DateTime.Now;
                var Items = await _context.JournalEntry.Where(w => w.CreatedDate.ToString("yyyy-MM-dd") == fechadehoy.ToString("yyyy-MM-dd") && w.TypeOfAdjustmentId ==65 ).CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityJournalEntriestotaldehoy()
        {
            try
            {
                DateTime fechadehoy = DateTime.Now;
                var Items = await _context.JournalEntry.Where(w => w.CreatedDate.ToString("yyyy-MM-dd") == fechadehoy.ToString("yyyy-MM-dd")).CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }
        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityCheckAccountLines()
        {
            try
            {
                var Items = await _context.CheckAccountLines.Where(w => w.IdEstado == 53).CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityCustomers()
        {
            try
            {
                var Items = await _context.Customer.CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityProduct()
        {
            try
            {
                var Items = await _context.Product.CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityVendor()
        {
            try
            {
                var Items = await _context.Vendor.CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityEmployees([FromBody]DashBoard _sarpara)
        {
            try
            {
                var Items = await _context.Employees.
                    Where(a => a.FechaCreacion >= _sarpara.BeginDate &&
                    a.FechaCreacion <= _sarpara.EndDate).
                    CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }




        //=========================DAHSBOARD DE RIESGOS=================================================

        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityAlerts()
        {
            try
            {

                DateTime _FechaActual = DateTime.Now;

                var Items = await _context.Alert.Where(a => a.FechaCreacion.ToString("yyyy-MM-dd") == _FechaActual.ToString("yyyy-MM-dd")).CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityInformacionMediatica()
        {
            try
            {
                var Items = await _context.BlackListCustomers.CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityProductoProhibidos()
        {
            try
            {
                var Items = await _context.SubProduct.Where(x => x.ProductTypeId == 3 ).CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityPEPS()
        {
            try
            {
                var Items = await _context.PEPS.CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }

        //=====================FIN DAHSBOARD DE RIESGOS=================================================


        //=========================DAHSBOARD PRECIDENCIAS=================================================

        [HttpGet("[action]")]
        public async Task<ActionResult<decimal>> GetCertificadosbyFecha()
        {
            try
            {
                var MesAcual = DateTime.Now;
                var Items = await _context.CertificadoDeposito.Where(b => b.FechaCertificado.ToString("MM-yyyy") == MesAcual.ToString("MM-yyyy")).ToListAsync();



                decimal Total = Convert.ToDecimal(Items.Sum(s => s.Total));

                Total.ToString("N2");
                return await Task.Run(() => Ok(Total));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetCertificadosFechCatidad()
        {
            try
            {
                var MesAcual = DateTime.Now;
                var Items = await _context.CertificadoDeposito.Where(b => b.FechaCertificado.ToString("MM-yyyy") == MesAcual.ToString("MM-yyyy")).CountAsync();                
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetSaldoCuentasBanco()
        {
            List<Accounting> Items = new List<Accounting>();
            try
            {
                Items = await (from c in _context.Accounting                                          
                               join d in _context.AccountManagement on c.AccountId equals d.AccountId
                               where c.AccountId == d.AccountId
                               select c).ToListAsync();

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }



        //=========================FIN DAHSBOARD PRECIDENCIAS=============================================



        //=========================DAHSBOARD RRHH=================================================
        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityEmployees()
        {
            try
            {
                var Items = await _context.Employees.CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityDepartamentos()
        {
            try
            {
                var Items = await _context.Departamento.CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }
        //===============================FIN DAHSBOARD RRHH========================================



        //===================DAHSBOARD OPERACIONES=================================================
        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityIngresos()
        {
            try
            {

                DateTime _FechaAcutal = DateTime.Now;

                var Items = await _context.Boleto_Ent.Where(X => X.fecha_e.ToString("yyyy-MM-dd") == _FechaAcutal.ToString("yyyy-MM-dd")).CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantitySalidas()
        {
            try
            {

                DateTime _FechaAcutal = DateTime.Now;

                var Items = await _context.Boleto_Sal.Where(X => X.fecha_s.ToString("yyyy-MM-dd") == _FechaAcutal.ToString("yyyy-MM-dd")).CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityAreasOcupadas()
        {
            try
            {
                var Items = await _context.CustomerArea.CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }




        //============================FIN DAHSBOARD OPERACIONES====================================
    }
}