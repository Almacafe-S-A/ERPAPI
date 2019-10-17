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
    [Route("api/Kardex")]
    [ApiController]
    public class KardexController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public KardexController(ILogger<KardexController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Kardex paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetKardexPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Kardex> Items = new List<Kardex>();
            try
            {
                var query = _context.Kardex.AsQueryable();
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
        /// Obtiene el Listado de Kardexes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetKardex()
        {
            List<Kardex> Items = new List<Kardex>();
            try
            {
                Items = await _context.Kardex.ToListAsync();
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
        /// Obtiene los Datos de la Kardex por medio del Id enviado.
        /// </summary>
        /// <param name="KardexId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{KardexId}")]
        public async Task<IActionResult> GetKardexById(Int64 KardexId)
        {
            Kardex Items = new Kardex();
            try
            {
                Items = await _context.Kardex.Where(q => q.KardexId == KardexId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> GetSaldoProductoByCertificado([FromBody]Kardex _Kardexq)
        {
            List<KardexLine> _kardexproduct = new List<KardexLine>();
          //  List<Kardex> _kardexproduct = new List<Kardex>();
            try
            {
                Int64 KardexId = await _context.Kardex
                                              .Where(q => q.DocumentId == _Kardexq.DocumentId)
                                              .Where(q => q.DocumentName == _Kardexq.DocumentName)
                                              .Select(q => q.KardexId)
                                              .MaxAsync();

                _kardexproduct = await (_context.KardexLine
                                              .Where(q => q.KardexId == KardexId)
                                             )
                                              .ToListAsync();


                //string fechainicio = DateTime.Now.Year + "-" + DateTime.Now.Month + "-01" ;
                //string fechafin = DateTime.Now.Year + "-" + DateTime.Now.Month + "-30";

                //_kardexproduct = await _context.Kardex
                //                              .Where(q => q.DocumentId == _Kardexq.DocumentId)
                //                              .Where(q => q.DocumentName == _Kardexq.DocumentName)                                             
                //                              .Where(q => q.DocumentDate >=Convert.ToDateTime(fechainicio))
                //                              .Where(q => q.DocumentDate <=Convert.ToDateTime(fechafin))
                //                              //.Select(q => q.KardexId)
                //                              .ToListAsync();


            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

           
            return await Task.Run(() => Ok(_kardexproduct));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> GetMovimientosCertificados([FromBody]KardexDTO _Kardexq)
        {
            ProformaInvoice _proforma = new ProformaInvoice();
            //<KardexLine> _kardexproduct = new List<KardexLine>();
            List<Kardex> _kardexproduct = new List<Kardex>();
            try
            {                

                string fechainicio = DateTime.Now.Year + "-" + DateTime.Now.Month + "-01";
                string fechafin = DateTime.Now.Year + "-" + DateTime.Now.Month + "-30";

                Guid Identificador = Guid.NewGuid();
               
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        CertificadoDeposito _tcd = await _context.CertificadoDeposito
                                                        .Where(q => q.IdCD == _Kardexq.Ids[0]).FirstOrDefaultAsync();

                        Customer _customer = new Customer();
                        _customer = _context.Customer
                            .Where(q => q.CustomerId == _tcd.CustomerId).FirstOrDefault();

                        foreach (var CertificadoId in _Kardexq.Ids)
                        {
                            _kardexproduct = await _context.Kardex
                                                          .Where(q => q.DocumentId == CertificadoId)
                                                          .Where(q => q.DocumentName == _Kardexq.DocumentName)
                                                          .Where(q => q.DocumentDate >= Convert.ToDateTime(fechainicio))
                                                          .Where(q => q.DocumentDate <= Convert.ToDateTime(fechafin))
                                                          //.Select(q => q.KardexId)
                                                          .Include(q => q._KardexLine)
                                                          .ToListAsync();

                            foreach (var item in _kardexproduct)
                            {
                                CertificadoDeposito _cd = new CertificadoDeposito();
                                _cd = await _context.CertificadoDeposito
                                             .Where(q => q.IdCD == item.DocumentId)
                                             .Include(q => q._CertificadoLine)
                                             .FirstOrDefaultAsync();

                                SalesOrder _so = await _context.SalesOrder
                                                       .Where(q => q.CustomerId == _cd.CustomerId)
                                                       .OrderByDescending(q => q.SalesOrderId).FirstOrDefaultAsync();

                                List<CustomerConditions> _cc = await _context.CustomerConditions
                                    .Where(q => q.CustomerId == _so.CustomerId)
                                    .Where(q => q.IdTipoDocumento == 12)
                                    .Where(q => q.DocumentId == _so.SalesOrderId)
                                    .ToListAsync();

                                int dias = item.DocumentDate.Day <= 15 ? 30 : 15;

                                double totalfacturar = 0;
                                foreach (var condicion in _cc)
                                {
                                    foreach (var lineascertificadas in _cd._CertificadoLine)
                                    {

                                        switch (condicion.LogicalCondition)
                                        {
                                            case ">=":
                                                if (lineascertificadas.Price >= Convert.ToDouble(condicion.ValueToEvaluate))
                                                    totalfacturar += ((condicion.ValueDecimal * (lineascertificadas.Price)) / 30) * dias;
                                                break;
                                            case "<=":
                                                if (lineascertificadas.Price <= Convert.ToDouble(condicion.ValueToEvaluate))
                                                    totalfacturar += ((condicion.ValueDecimal * (lineascertificadas.Price)) / 30) * dias;
                                                break;
                                            case ">":
                                                if (lineascertificadas.Price > Convert.ToDouble(condicion.ValueToEvaluate))
                                                    totalfacturar += ((condicion.ValueDecimal * (lineascertificadas.Price)) / 30) * dias;
                                                break;
                                            case "<":
                                                if (lineascertificadas.Price < Convert.ToDouble(condicion.ValueToEvaluate))
                                                    totalfacturar += ((condicion.ValueDecimal * (lineascertificadas.Price)) / 30) * dias;
                                                break;
                                            case "=":
                                                if (lineascertificadas.Price == Convert.ToDouble(condicion.ValueToEvaluate))
                                                    totalfacturar += ((condicion.ValueDecimal * (lineascertificadas.Price)) / 30) * dias;
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }

                                //GoodsDeliveryAuthorization _GoodsDeliveryAuthorization = new GoodsDeliveryAuthorization();
                                //if (_cd==null)
                                //{
                                //    //Si es nulo buscar en la tabla Autorización
                                //    _GoodsDeliveryAuthorization = await _context.GoodsDeliveryAuthorization
                                //                                    .Where(q=>q.id)
                                //}

                                foreach (var linea in item._KardexLine)
                                {
                                    CertificadoLine cdline = _cd._CertificadoLine
                                               .Where(q => q.SubProductId == linea.SubProducId).FirstOrDefault();

                                    _context.InvoiceCalculation.Add(new InvoiceCalculation
                                    {
                                        CustomerId = item.CustomerId,
                                        CustomerName = item.CustomerName,
                                        DocumentDate = item.DocumentDate,
                                        NoCD = _cd.NoCD,
                                        Dias = dias,
                                        ProductId = linea.SubProducId,
                                        ProductName = linea.SubProductName,
                                        UnitPrice = cdline.Price,
                                        Quantity = item.TypeOperationName == "Entrada" ? linea.QuantityEntry : linea.QuantityOut,
                                        ValorLps = cdline.Price * (item.TypeOperationName == "Entrada" ? linea.QuantityEntry : linea.QuantityOut),
                                        ValorFacturar = totalfacturar,
                                        Identificador = Identificador,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = _Kardexq.UsuarioCreacion,
                                        UsuarioModificacion = _Kardexq.UsuarioModificacion,
                                    });
                                }

                            }

                        }


                        await _context.SaveChangesAsync();
                        transaction.Commit();
                        //Retornar la proforma con calculos(Resumen: Almacenaje,Bascula,Banda Transportadora)

                        List<InvoiceCalculation> _InvoiceCalculationlist = await  _context.InvoiceCalculation
                                                                                    .Where(q=>q.Identificador==Identificador)  
                                                                                    .ToListAsync();

                        Tax _tax = await _context.Tax
                            .Where(q=>q.TaxCode=="I.S.V")
                            .FirstOrDefaultAsync();

                        List<ProformaInvoiceLine> ProformaInvoiceLineT = new List<ProformaInvoiceLine>();
                        ProformaInvoiceLineT.Add(new ProformaInvoiceLine
                        {
                             SubProductId = 1,
                             SubProductName = "Almacenaje",
                             Price = _InvoiceCalculationlist[0].UnitPrice,
                             Quantity = _InvoiceCalculationlist[0].Quantity,
                             SubTotal = _InvoiceCalculationlist[0].UnitPrice * _InvoiceCalculationlist[0].Quantity,
                             TaxAmount = (_InvoiceCalculationlist[0].UnitPrice * _InvoiceCalculationlist[0].Quantity) * (_tax.TaxPercentage/100),
                             TaxCode = _tax.TaxCode,                              
                             Total = _InvoiceCalculationlist[0].UnitPrice * _InvoiceCalculationlist[0].Quantity,

                        });

                        _proforma = new ProformaInvoiceDTO
                        {
                             CustomerId = _customer.CustomerId,
                             CustomerName = _customer.CustomerName,
                         
                             SubTotal = _InvoiceCalculationlist.Sum(q=>q.ValorFacturar),
                             Identificador = Identificador,
                             ProformaInvoiceLine = ProformaInvoiceLineT,
                        };


                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        transaction.Rollback();
                        return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
                    }


                }
             }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(_proforma));
        }



        /// <summary>
        /// Inserta una nueva Kardex
        /// </summary>
        /// <param name="_Kardex"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Kardex>> Insert([FromBody]Kardex _Kardex)
        {
            Kardex _Kardexq = new Kardex();
            try
            {
                _Kardexq = _Kardex;
                _context.Kardex.Add(_Kardexq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Kardexq));
        }

        /// <summary>
        /// Actualiza la Kardex
        /// </summary>
        /// <param name="_Kardex"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Kardex>> Update([FromBody]Kardex _Kardex)
        {
            Kardex _Kardexq = _Kardex;
            try
            {
                _Kardexq = await (from c in _context.Kardex
                                 .Where(q => q.KardexId == _Kardex.KardexId)
                                  select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Kardexq).CurrentValues.SetValues((_Kardex));

                //_context.Kardex.Update(_Kardexq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Kardexq));
        }

        /// <summary>
        /// Elimina una Kardex       
        /// </summary>
        /// <param name="_Kardex"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Kardex _Kardex)
        {
            Kardex _Kardexq = new Kardex();
            try
            {
                _Kardexq = _context.Kardex
                .Where(x => x.KardexId == (Int64)_Kardex.KardexId)
                .FirstOrDefault();

                _context.Kardex.Remove(_Kardexq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Kardexq));

        }







    }
}