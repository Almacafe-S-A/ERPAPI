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
                CertificadoDeposito _tcd = await _context.CertificadoDeposito
                                                       .Where(q => q.IdCD == _Kardexq.Ids[0]).FirstOrDefaultAsync();

                string fechainicio = DateTime.Now.Year + "-" + DateTime.Now.Month + "-01";
                string fechafin = DateTime.Now.Year + "-" + DateTime.Now.Month + "-"+ DateTime.DaysInMonth(DateTime.Now.Year,DateTime.Now.Month);

                Guid Identificador = Guid.NewGuid();
               
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                       

                        Customer _customer = new Customer();
                        _customer = _context.Customer
                            .Where(q => q.CustomerId == _tcd.CustomerId).FirstOrDefault();

                        foreach (var CertificadoId in _Kardexq.Ids)
                        {
                            _kardexproduct =await _context.Kardex
                                                      .Where(q => q.DocumentId == CertificadoId)
                                                      .Where(q => q.DocumentName == _Kardexq.DocumentName)
                                                      .Where(q => q.DocumentDate >= Convert.ToDateTime(fechainicio))
                                                      .Where(q => q.DocumentDate <= Convert.ToDateTime(fechafin))
                                                      //.Select(q => q.KardexId)
                                                      .Include(q => q._KardexLine)
                                                      .ToListAsync();

                            //Si no hubo movimientos de kardex durante el mes , y sigue estando activo
                            //buscamos el ultimo movimiento
                            if (_kardexproduct.Count == 0)
                            {
                                _kardexproduct = await _context.Kardex
                                     .Where(q => q.DocumentId == CertificadoId)
                                     .Where(q => q.DocumentName == _Kardexq.DocumentName)  
                                     .OrderByDescending(q=>q.DocumentDate) 
                                     .Include(q => q._KardexLine)
                                     .ToListAsync();
                            }


                            bool facturo = false;
                            //Despues de buscar todos los movimientos que tiene el certificado en inventario
                            //se calculan los valores
                            foreach (var item in _kardexproduct)
                            {
                                //  item._KardexLine = item._KardexLine.Where(q => q.TotalCD > 0).ToList();
                                if (!facturo)
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

                                    int dias = 0;

                                    if (item.TypeOperationName == "Entrada")
                                    {
                                        dias = item.DocumentDate.Day <= 15 ? 30 : 15;
                                    }
                                    else if(item.TypeOperationName=="Salida")
                                    {
                                        dias = item.DocumentDate.Day <= 15 ? 15 : 30;
                                    }

                                    double totalfacturar = 0;
                                    double totalfacturarmerma = 0;
                                    foreach (var condicion in _cc)
                                    {
                                        foreach (var lineascertificadas in _cd._CertificadoLine)
                                        {
                                            double cantidad = 0;
                                            if (item.TypeOperationName == "Entrada")
                                            {
                                                cantidad =   (item._KardexLine
                                                                .Where(q => q.SubProducId == lineascertificadas.SubProductId)
                                                                .Select(q => q.QuantityEntry)
                                                             ).FirstOrDefault() ;


                                            }
                                            else if(item.TypeOperationName == "Salida")
                                            {
                                                List<Kardex> salidas = await (_context.Kardex.Where(q => q.DocumentId == _cd.IdCD)
                                                                             .Where(q => q.DocumentName == _Kardexq.DocumentName)
                                                                             .Where(q=>q.TypeOperationName=="Salida")
                                                                             ).ToListAsync();

                                                foreach (var salida in salidas)
                                                {
                                                   cantidad += (salida._KardexLine
                                                                .Where(q => q.SubProducId == lineascertificadas.SubProductId)
                                                                .Select(q => q.QuantityOut)
                                                               ).FirstOrDefault();
                                                }

                                               double entrada =  (item._KardexLine
                                                                .Where(q => q.SubProducId == lineascertificadas.SubProductId)
                                                                .Select(q => q.QuantityEntry)
                                                             ).FirstOrDefault();

                                                cantidad = entrada - cantidad;
                                            }


                                            SubProduct _subproduct = await _context.SubProduct
                                                                  .Where(q => q.SubproductId == lineascertificadas.SubProductId).FirstOrDefaultAsync();
                                            switch (condicion.LogicalCondition)
                                            {
                                                case ">=":
                                                    if (lineascertificadas.Price >= Convert.ToDouble(condicion.ValueToEvaluate))
                                                        totalfacturar += ((condicion.ValueDecimal * (lineascertificadas.Price * cantidad)) / 30) * dias;
                                                        totalfacturarmerma += ((totalfacturarmerma / (1 - _subproduct.Merma)) * _subproduct.Merma) * condicion.ValueDecimal;
                                                    break;
                                                case "<=":
                                                    if (lineascertificadas.Price <= Convert.ToDouble(condicion.ValueToEvaluate))
                                                        totalfacturar += ((condicion.ValueDecimal * (lineascertificadas.Price * cantidad)) / 30) * dias;
                                                        totalfacturarmerma += ((totalfacturarmerma / (1 - _subproduct.Merma)) * _subproduct.Merma) * condicion.ValueDecimal;
                                                    break;
                                                case ">":
                                                    if (lineascertificadas.Price > Convert.ToDouble(condicion.ValueToEvaluate))
                                                        totalfacturar += ((condicion.ValueDecimal * (lineascertificadas.Price * cantidad)) / 30) * dias;
                                                        totalfacturarmerma += ((totalfacturarmerma / (1 - _subproduct.Merma)) * _subproduct.Merma) * condicion.ValueDecimal;
                                                    break;
                                                case "<":
                                                    if (lineascertificadas.Price < Convert.ToDouble(condicion.ValueToEvaluate))
                                                        totalfacturar += ((condicion.ValueDecimal * (lineascertificadas.Price * cantidad)) / 30) * dias;
                                                        totalfacturarmerma += ((totalfacturarmerma / (1 - _subproduct.Merma)) * _subproduct.Merma) * condicion.ValueDecimal;
                                                    break;
                                                case "=":
                                                    if (lineascertificadas.Price == Convert.ToDouble(condicion.ValueToEvaluate))
                                                        totalfacturar += ((condicion.ValueDecimal * (lineascertificadas.Price * cantidad)) / 30) * dias;
                                                        totalfacturarmerma += ((totalfacturarmerma / (1 - _subproduct.Merma)) * _subproduct.Merma) * condicion.ValueDecimal;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }


                                    foreach (var linea in item._KardexLine)
                                    {
                                        CertificadoLine cdline = _cd._CertificadoLine
                                                   .Where(q => q.SubProductId == linea.SubProducId).FirstOrDefault();

                                        double cantidad = 0;
                                        if (item.TypeOperationName == "Entrada")
                                        {
                                            cantidad = (item._KardexLine
                                                            .Where(q => q.SubProducId == cdline.SubProductId)
                                                            .Select(q => q.QuantityEntry)
                                                         ).FirstOrDefault();


                                        }
                                        else if (item.TypeOperationName == "Salida")
                                        {
                                            List<Kardex> salidas = await (_context.Kardex.Where(q => q.DocumentId == _cd.IdCD)
                                                                         .Where(q => q.DocumentName == _Kardexq.DocumentName)
                                                                         .Where(q => q.TypeOperationName == "Salida")
                                                                         ).ToListAsync();

                                            foreach (var salida in salidas)
                                            {
                                                cantidad += (salida._KardexLine
                                                             .Where(q => q.SubProducId == cdline.SubProductId)
                                                             .Select(q => q.QuantityOut)
                                                            ).FirstOrDefault();
                                            }

                                            double entrada = (item._KardexLine
                                                             .Where(q => q.SubProducId == cdline.SubProductId)
                                                             .Select(q => q.QuantityEntry)
                                                          ).FirstOrDefault();

                                            cantidad = entrada - cantidad;
                                        }

                                        SubProduct _subproduct = await _context.SubProduct
                                                                      .Where(q => q.SubproductId == linea.SubProducId).FirstOrDefaultAsync();


                                        double valormerma = ((cantidad / (1 - _subproduct.Merma)) * _subproduct.Merma) * cdline.Price;

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
                                            Quantity = cantidad,
                                            IngresoMercadería = cantidad/_subproduct.Merma,
                                            MercaderiaCertificada = cantidad,
                                            ValorLps = cdline.Price * cantidad,
                                            ValorFacturar = totalfacturar,
                                            Identificador = Identificador,                                           
                                            PorcentajeMerma = _subproduct.Merma,
                                            ValorLpsMerma = valormerma,
                                            ValorAFacturarMerma = (totalfacturar / (1 - _subproduct.Merma)) * _subproduct.Merma,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            UsuarioCreacion = _Kardexq.UsuarioCreacion,
                                            UsuarioModificacion = _Kardexq.UsuarioModificacion,
                                        });


                                        if (item.TypeOperationName == "Entrada")
                                        {
                                            facturo = true;
                                        }
                                    }


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

                        double valfacturar = _InvoiceCalculationlist.Sum(q => q.ValorFacturar) + _InvoiceCalculationlist.Sum(q=>q.ValorAFacturarMerma);
                        List<ProformaInvoiceLine> ProformaInvoiceLineT = new List<ProformaInvoiceLine>();
                        ProformaInvoiceLineT.Add(new ProformaInvoiceLine
                        {
                             SubProductId = 1,
                             SubProductName = "Almacenaje",
                             Price = _InvoiceCalculationlist[0].UnitPrice,
                             Quantity = _InvoiceCalculationlist.Sum(q=>q.Quantity),
                             Amount = _InvoiceCalculationlist[0].UnitPrice * _InvoiceCalculationlist.Sum(q => q.Quantity),
                             // UnitOfMeasureId = _tcd._CertificadoLine[0].UnitMeasureId,
                             //  UnitOfMeasureName = _tcd._CertificadoLine[0].UnitMeasurName,
                             SubTotal = valfacturar,
                             TaxAmount = valfacturar * (_tax.TaxPercentage/100),
                             TaxId = _tax.TaxId,
                             TaxCode = _tax.TaxCode,                              
                             Total = valfacturar + (valfacturar * (_tax.TaxPercentage / 100)),

                        });

                        _proforma = new ProformaInvoiceDTO
                        {
                             CustomerId = _customer.CustomerId,
                             CustomerName = _customer.CustomerName,
                             CustomerRefNumber = _customer.CustomerRefNumber,
                             Correo = _customer.Email,
                             Direccion = _customer.Address,
                             Tefono = _customer.Phone,
                             RTN = _customer.RTN,
                            
                             ProformaName = _customer.CustomerName,
                             BranchId = _tcd.BranchId,
                             BranchName = _tcd.BranchName,
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