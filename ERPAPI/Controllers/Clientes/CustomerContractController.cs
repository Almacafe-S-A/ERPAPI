using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
    [Route("api/CustomerContract")]
    [ApiController]
    public class CustomerContractController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CustomerContractController(ILogger<CustomerContractController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CustomerContract paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerContractPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CustomerContract> Items = new List<CustomerContract>();
            try
            {
                var query = _context.CustomerContract.AsQueryable();
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
        /// Obtiene el Listado de CustomerContractes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerContract()
        {
            List<CustomerContract> Items = new List<CustomerContract>();
            try
            {
                Items = await _context.CustomerContract
                    //.Where(q => q.CustomerContractId_Source == null)
                    .ToListAsync();
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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerContractList()
        {
            List<CustomerContract> Items = new List<CustomerContract>();
            try
            {
                Items = await _context.CustomerContract
                    .Where(q => q.CustomerContractId_Source == null)
                    .ToListAsync();
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
        /// Obtiene el detalle del contrato
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetCustomerContractLines(int id)
        {
            List<CustomerContractLines> Items = new List<CustomerContractLines>();
            try
            {
                Items = await _context.CustomerContractLines.Where(q => q.CustomerContractId == id).ToListAsync();
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
        /// Obtiene el detalle del contrato
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> CustomerContractLinesTermsById(int id)
        {
            CustomerContractLinesTerms Item = new CustomerContractLinesTerms();
            try
            {
                Item = await _context.CustomerContractLinesTerms.Where(q => q.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Item));
        }



        /// <summary>
        /// Obtiene el detalle del contrato
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetCustomerContractLinesTerms(int id)
        {
            List<CustomerContractLinesTerms> Items = new List<CustomerContractLinesTerms>();
            try
            {
                Items = await _context.CustomerContractLinesTerms.Where(q => q.CustomerContractId == id).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetCustomerContractByCustomerId(Int64 CustomerId)
        {
            List<CustomerContract> Items = new List<CustomerContract>();
            try
            {
                Items = await _context.CustomerContract.Where(q => q.CustomerId == CustomerId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }
        


            [HttpGet("[action]/{ContractId}")]
        public async Task<IActionResult> CustomerAdendumByContract(Int64 ContractId)
        {
            List<CustomerContract> Items = new List<CustomerContract>();
            try
            {
                Items = await _context.CustomerContract.Where(q => q.CustomerContractId_Source == ContractId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }



        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetCustomerContractActiveByCustomerId(Int64 CustomerId)
        {
            List<CustomerContract> Items = new List<CustomerContract>();
            try
            {
                Items = await _context.CustomerContract
                    .Where(q => q.CustomerId == CustomerId && 
                    //q.Estado == "Vigente" && 
                    q.TypeContractId == 1)
                    .ToListAsync();
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
        /// Obtiene los Datos de la CustomerContract por medio del Id enviado.
        /// </summary>
        /// <param name="CustomerContractId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerContractId}")]
        public async Task<IActionResult> GetCustomerContractById(Int64 CustomerContractId)
        {
            CustomerContract Items = new CustomerContract();
            try
            {
                Items = await _context.CustomerContract
                    .Include(i => i.customerContractLines)
                    .Include(i => i.customerContractLinesTerms)
                    .Where(q => q.CustomerContractId == CustomerContractId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }




        /// <summary>
        /// Inserta un nuevo contrato de cliente a partir de una cotizacion 
        /// </summary>
        /// <param name="salesorderid"></param>
        /// <returns></returns>
        [HttpGet("[action]/{salesorderid}")]
        public async Task<ActionResult<CustomerContract>> GenerarContrato(int salesorderid)
        {
            CustomerContract _CustomerContractq = new CustomerContract();
            try
            {
                SalesOrder salesOrder = await _context.SalesOrder
                    .Include(i => i.SalesOrderLines)
                    .Where(q => q.SalesOrderId == salesorderid).FirstOrDefaultAsync();
                int? adendumno = null;
                if (salesOrder.CustomerContractId_Source != null)
                {
                    var contract = await _context.CustomerContract
                        .Where(q => q.CustomerContractId_Source == salesOrder.CustomerContractId_Source)
                        .ToListAsync();
                    adendumno = contract.Count()+1;
                }

                if (salesOrder != null)
                {
                    _CustomerContractq.CustomerManager = salesOrder.Representante;
                    _CustomerContractq.CustomerId = salesOrder.CustomerId;
                    _CustomerContractq.CustomerName = salesOrder.CustomerName;
                    _CustomerContractq.ComisionMax = salesOrder.ComisionMax;
                    _CustomerContractq.ComisionMin = salesOrder.ComisionMin;
                    _CustomerContractq.Estado = "Generado";
                    _CustomerContractq.FechaContrato = DateTime.Now;
                    _CustomerContractq.FechaCreacion = DateTime.Now;
                    _CustomerContractq.Manager = salesOrder.FirmaAlmacafe;
                    _CustomerContractq.PrecioBaseProducto = salesOrder.PrecioBaseProducto;
                    _CustomerContractq.PrecioServicio = salesOrder.PrecioBaseProducto;
                    _CustomerContractq.RTNCustomerManager = salesOrder.RTN;
                    _CustomerContractq.SalesOrderId = salesorderid;
                    _CustomerContractq.StorageTime = salesOrder.PlazoMeses.ToString();
                    _CustomerContractq.TypeContractId = salesOrder.TypeContractId;
                    _CustomerContractq.TypeContractName = salesOrder.NameContract;
                    _CustomerContractq.TypeInvoiceId = salesOrder.TypeInvoiceId;
                    _CustomerContractq.TypeInvoiceName = salesOrder.TypeInvoiceName;
                    _CustomerContractq.UsuarioCreacion = User.Identity.Name;
                    _CustomerContractq.ProductId = salesOrder.ProductId;
                    _CustomerContractq.ProductName = salesOrder.ProductName;
                    _CustomerContractq.TypeInvoiceId = salesOrder.TypeInvoiceId;
                    _CustomerContractq.TypeInvoiceName = salesOrder.TypeInvoiceName;
                    _CustomerContractq.ComisionMax = salesOrder.ComisionMax;
                    _CustomerContractq.ComisionMin = salesOrder.ComisionMin;
                    _CustomerContractq.PrecioBaseProducto = salesOrder.PrecioBaseProducto;
                    _CustomerContractq.Plazo = salesOrder.PlazoMeses;
                    _CustomerContractq.FechaInicioContrato = null;
                    _CustomerContractq.FechaVencimiento = null;
                    _CustomerContractq.IncrementoAnual = salesOrder.IncrementoAnual;
                    _CustomerContractq.AdendumNo = adendumno;
                    _CustomerContractq.CustomerContractId_Source = salesOrder.CustomerContractId_Source;
                    _CustomerContractq.customerContractLines = new List<CustomerContractLines>();

                    foreach (var item in salesOrder.SalesOrderLines)
                    {
                        _CustomerContractq.customerContractLines.Add(new CustomerContractLines()
                        {
                            Description = item.Description,
                            PeriodoCobro = item.PeriodoCobro,
                            Porcentaje = item.Porcentaje,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            SubProductId = item.SubProductId,
                            SubProductName = item.SubProductName,
                            TipoCobroId = item.TipoCobroId,
                            TipoCobroName = item.TipoCobroName,
                            UnitOfMeasureId = item.UnitOfMeasureId,
                            UnitOfMeasureName = item.UnitOfMeasureName,
                            Valor = item.Valor


                        });

                    }

                    List<CustomerContractTerms> terminos = await _context.CustomerContractTerms
                        .Where(q => q.ProductId == _CustomerContractq.ProductId && q.TypeInvoiceId == _CustomerContractq.TypeInvoiceId)
                        .OrderBy(o => o.Position)
                        .ToListAsync();

                    _CustomerContractq.customerContractLinesTerms = new List<CustomerContractLinesTerms>();


                    foreach (var item in terminos)
                    {
                        _CustomerContractq.customerContractLinesTerms.Add(
                            new CustomerContractLinesTerms {
                                ContractTermId = item.Id,
                                Position = item.Position,
                                Term = item.Term,
                                TermTitle = item.TermTitle
                            }
                        );
                    }


                }
                else
                {
                    return BadRequest("No se encontro cotización");
                }

                _context.CustomerContract.Add(_CustomerContractq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerContractq));
        }
        
        /// <summary>
        /// Inserta una nueva CustomerContract
        /// </summary>
        /// <param name="_CustomerContract"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerContract>> Insert([FromBody]CustomerContract _CustomerContract)
        {
            CustomerContract _CustomerContractq = new CustomerContract();
            try
            {
                _CustomerContractq = _CustomerContract;
                _context.CustomerContract.Add(_CustomerContractq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerContractq));
        }

        /// <summary>
        /// Actualiza la CustomerContract
        /// </summary>
        /// <param name="_CustomerContract"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CustomerContract>> Update([FromBody]CustomerContract _CustomerContract)
        {
            CustomerContract _CustomerContractq = _CustomerContract;
            try
            {
                _CustomerContractq = await (from c in _context.CustomerContract
                                 .Where(q => q.CustomerContractId == _CustomerContract.CustomerContractId)
                                            select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CustomerContractq).CurrentValues.SetValues((_CustomerContract));

                //_context.CustomerContract.Update(_CustomerContractq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerContractq));
        }



        /// <summary>
        /// Actualiza la CustomerContract
        /// </summary>
        /// <param name="_CustomerContract"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CustomerContract>> ActivarContrato([FromBody] CustomerContract _CustomerContract)
        {
            CustomerContract _CustomerContractq = _CustomerContract;
            try
            {
                _CustomerContractq = await (from c in _context.CustomerContract
                                 .Where(q => q.CustomerContractId == _CustomerContract.CustomerContractId)
                                            select c
                                ).FirstOrDefaultAsync();

                //_context.Entry(_CustomerContractq).CurrentValues.SetValues((_CustomerContract));
                _CustomerContractq.FechaInicioContrato = _CustomerContract.FechaInicioContrato;
                _CustomerContractq.Estado = "Vigente";
                _CustomerContractq.IdEstado = 7;


                //_context.CustomerContract.Update(_CustomerContractq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerContractq));
        }
        /// <summary>
        /// Elimina una CustomerContract       
        /// </summary>
        /// <param name="_CustomerContract"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CustomerContract _CustomerContract)
        {
            CustomerContract _CustomerContractq = new CustomerContract();
            try
            {
                _CustomerContractq = _context.CustomerContract
                .Where(x => x.CustomerContractId == (Int64)_CustomerContract.CustomerContractId)
                .FirstOrDefault();

                _context.CustomerContract.Remove(_CustomerContractq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerContractq));

        }







    }
}