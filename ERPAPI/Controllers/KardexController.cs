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

                string fechainicio = _Kardexq.StartDate.Value.Year 
                        + "-" + _Kardexq.StartDate.Value.Month + "-"+ _Kardexq.StartDate.Value.Day;
                string fechafin = _Kardexq.EndDate.Value.Year + "-" + _Kardexq.EndDate.Value.Month
                                 + "-" + _Kardexq.EndDate.Value.Day;
                //string fechainicio = DateTime.Now.Year + "-" + DateTime.Now.Month + "-01";
                //string fechafin = DateTime.Now.Year + "-" + DateTime.Now.Month + "-"+ DateTime.DaysInMonth(DateTime.Now.Year,DateTime.Now.Month);

                Guid Identificador = Guid.NewGuid();               
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                       

                        Customer _customer = new Customer();
                        _customer = _context.Customer
                            .Where(q => q.CustomerId == _tcd.CustomerId).FirstOrDefault();

                        SalesOrder _so = new SalesOrder();

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

                                   _so = await _context.SalesOrder
                                                           .Where(q => q.CustomerId == _cd.CustomerId)
                                                           .Where(q=>q.SalesOrderId == _Kardexq.SalesOrderId)
                                                           .Include(q=>q.SalesOrderLines)
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
                            .Where(q => q.TaxCode == "I.S.V")
                            .FirstOrDefaultAsync();

                        SalesOrderLine _soline = new SalesOrderLine();
                        SubProduct _su = new SubProduct();

                        //1. Almacenaje
                        _su = await _context.SubProduct.Where(q => q.SubproductId == 1).FirstOrDefaultAsync();
                        _soline =  _so.SalesOrderLines.Where(q => q.SubProductId == _su.SubproductId).FirstOrDefault();
                        double valfacturar = _InvoiceCalculationlist.Sum(q => q.ValorFacturar) + _InvoiceCalculationlist.Sum(q=>q.ValorAFacturarMerma);
                        List<ProformaInvoiceLine> ProformaInvoiceLineT = new List<ProformaInvoiceLine>();
                        ProformaInvoiceLineT.Add(new ProformaInvoiceLine
                        {
                             SubProductId = 1,
                             SubProductName = "Almacenaje",
                             Price = _InvoiceCalculationlist[0].UnitPrice,
                             Quantity = _InvoiceCalculationlist.Sum(q=>q.Quantity),
                             Amount = valfacturar,//_InvoiceCalculationlist[0].UnitPrice * _InvoiceCalculationlist.Sum(q => q.Quantity),
                             UnitOfMeasureId = _soline.UnitOfMeasureId,
                             UnitOfMeasureName = _soline.UnitOfMeasureName,
                             SubTotal = valfacturar,
                             TaxAmount = valfacturar * (_tax.TaxPercentage/100),
                             TaxId = _tax.TaxId,
                             TaxCode = _tax.TaxCode,      
                             TaxPercentage = _tax.TaxPercentage,
                             Total = valfacturar + (valfacturar * (_tax.TaxPercentage / 100)),

                        });

                        //2 Seguro
                        _su = new SubProduct();
                        _su = await _context.SubProduct.Where(q => q.SubproductId == 2).FirstOrDefaultAsync();
                        _soline = new SalesOrderLine();
                        _soline =  _so.SalesOrderLines.Where(q => q.SubProductId == _su.SubproductId).FirstOrDefault();

                        double preciocot = 0;
                        double quantitycot = _InvoiceCalculationlist.Sum(q => q.Quantity);
                        double taxamount = ((preciocot * valfacturar) / 1000) * (_tax.TaxPercentage/100);
                        if (_soline != null)
                        {
                             preciocot = _soline.Price;                           
                             taxamount = ((preciocot * valfacturar) / 1000) * (_tax.TaxPercentage/100);
                            ProformaInvoiceLineT.Add(new ProformaInvoiceLine
                            {
                                SubProductId = _su.SubproductId,
                                SubProductName = _su.ProductName,
                                Price = preciocot,
                                Quantity = quantitycot,
                                Amount = (preciocot * valfacturar) / 1000, //preciocot * quantitycot,
                                SubTotal = (preciocot * valfacturar)/1000,
                                TaxAmount = taxamount,
                                TaxId = _tax.TaxId,
                                UnitOfMeasureId = _soline.UnitOfMeasureId,
                                UnitOfMeasureName = _soline.UnitOfMeasureName,
                                TaxCode = _tax.TaxCode,
                                TaxPercentage = _tax.TaxPercentage,
                                Total = (preciocot * valfacturar) / 1000 + taxamount,

                            });
                        }

                        //4 Bascula
                        _su = new SubProduct();
                        _su = await _context.SubProduct.Where(q => q.SubproductId == 4).FirstOrDefaultAsync();
                        _soline = new SalesOrderLine();
                        _soline =  _so.SalesOrderLines.Where(q => q.SubProductId == _su.SubproductId).FirstOrDefault();

                        if (_soline != null)
                        {

                            preciocot = _soline.Price ;

                            List<Int64> IdsRecibos = await _context.RecibosCertificado.Where(q => _Kardexq.Ids.Contains(q.IdCD)).Select(q=>q.IdRecibo).ToListAsync();

                            List<GoodsReceived> _entradas = await _context.GoodsReceived
                                                                   .Include(q=>q._GoodsReceivedLine)
                                                                   .Where(q => q.CustomerId == _customer.CustomerId)
                                                                   .Where(q=> IdsRecibos.Contains(q.GoodsReceivedId))
                                                                   .Where(q => q.DocumentDate >= Convert.ToDateTime(fechainicio))
                                                                   .Where(q => q.DocumentDate <= Convert.ToDateTime(fechafin)).ToListAsync();
                            double sumaentradas = 0;
                            foreach (var entrada in _entradas)
                            {
                                sumaentradas += entrada._GoodsReceivedLine.Where(q => q.UnitOfMeasureName == "QUINTALES").Sum(q => q.Quantity);
                            }

                            List<Int64> IdsEntregas = await _context.RecibosCertificado.Where(q => _Kardexq.Ids.Contains(q.IdCD)).Select(q => q.IdRecibo).ToListAsync();
                            List<GoodsDelivered> _salidas = await _context.GoodsDelivered

                                                                  .Include(q => q._GoodsDeliveredLine)
                                                                  .Where(q=>q.CustomerId== _customer.CustomerId)
                                                                    .Where(q => IdsEntregas.Contains(q.GoodsDeliveredId))
                                                                  .Where(q => q.DocumentDate >= Convert.ToDateTime(fechainicio))
                                                                  .Where(q => q.DocumentDate <= Convert.ToDateTime(fechafin)).ToListAsync();
                            double sumasalidas = 0;
                            foreach (var _salida in _salidas)
                            {
                                sumasalidas += _salida._GoodsDeliveredLine.Where(q => q.UnitOfMeasureName == "QUINTALES").Sum(q => q.Quantity);
                            }

                            double totalentradassalidas = 0;
                            totalentradassalidas = sumaentradas + sumasalidas;
                            quantitycot = totalentradassalidas;
                            taxamount = ((preciocot * totalentradassalidas)) * (_tax.TaxPercentage / 100);
                            ProformaInvoiceLineT.Add(new ProformaInvoiceLine
                            {
                                SubProductId = _su.SubproductId,
                                SubProductName = _su.ProductName,
                                UnitOfMeasureId = _soline.UnitOfMeasureId,
                                UnitOfMeasureName = _soline.UnitOfMeasureName,
                                Price = preciocot,
                                Quantity = quantitycot,
                                Amount = (preciocot * quantitycot), //totalentradassalidas * preciocot ,
                                SubTotal = (preciocot * quantitycot) ,
                                TaxAmount = taxamount,
                                TaxId = _tax.TaxId,
                                TaxCode = _tax.TaxCode,
                                TaxPercentage = _tax.TaxPercentage,
                                Total = (preciocot * quantitycot) + taxamount,

                            });
                        }

                        //5 Banda
                        _su = new SubProduct();
                        _su = await _context.SubProduct.Where(q => q.SubproductId == 5).FirstOrDefaultAsync();
                        _soline = new SalesOrderLine();
                        _soline = _so.SalesOrderLines.Where(q => q.SubProductId == _su.SubproductId).FirstOrDefault();

                        if (_soline != null)
                        {
                            taxamount = preciocot = quantitycot = 0;                           
                            preciocot = _soline.Price;
                            SubServicesWareHouse _subservicewarehouse = new SubServicesWareHouse();
                            _subservicewarehouse = await _context.SubServicesWareHouse
                                .Where(q => q.SubServiceId == _su.SubproductId)
                                .Where(q=>q.DocumentDate>=Convert.ToDateTime(fechainicio))
                                .Where(q => q.DocumentDate <= Convert.ToDateTime(fechafin))
                                .FirstOrDefaultAsync();
                            if (_subservicewarehouse != null)
                            {
                                quantitycot = _subservicewarehouse.QuantityHours;

                                taxamount = ((preciocot * quantitycot)) * (_tax.TaxPercentage / 100);
                                ProformaInvoiceLineT.Add(new ProformaInvoiceLine
                                {
                                    SubProductId = _su.SubproductId,
                                    SubProductName = _su.ProductName,
                                    UnitOfMeasureId = _soline.UnitOfMeasureId,
                                    UnitOfMeasureName = _soline.UnitOfMeasureName,
                                    Price = preciocot,
                                    Quantity = quantitycot,
                                    Amount = (preciocot * quantitycot),
                                    SubTotal = (preciocot * quantitycot),
                                    TaxAmount = taxamount,
                                    TaxId = _tax.TaxId,
                                    TaxCode = _tax.TaxCode,
                                    TaxPercentage = _tax.TaxPercentage,
                                    Total = (preciocot * quantitycot) + taxamount,

                                });
                            }
                        }

                        //6 Monta carga
                        _su = new SubProduct();
                        _su = await _context.SubProduct.Where(q => q.SubproductId == 6).FirstOrDefaultAsync();
                        _soline = new SalesOrderLine();
                        _soline = _so.SalesOrderLines.Where(q => q.SubProductId == _su.SubproductId).FirstOrDefault();

                        if (_soline != null)
                        {
                            taxamount = preciocot = quantitycot = 0;
                            preciocot = _soline.Price;
                            SubServicesWareHouse _subservicewarehouse = new SubServicesWareHouse();
                            _subservicewarehouse = await _context.SubServicesWareHouse
                                .Where(q => q.SubServiceId == _su.SubproductId)
                                .Where(q => q.DocumentDate >= Convert.ToDateTime(fechainicio))
                                .Where(q => q.DocumentDate <= Convert.ToDateTime(fechafin))
                                .FirstOrDefaultAsync();
                            if (_subservicewarehouse != null)
                            {
                                quantitycot = _subservicewarehouse.QuantityHours;

                                taxamount = ((preciocot * quantitycot)) * (_tax.TaxPercentage / 100);
                                ProformaInvoiceLineT.Add(new ProformaInvoiceLine
                                {
                                    SubProductId = _su.SubproductId,
                                    SubProductName = _su.ProductName,
                                    UnitOfMeasureId = _soline.UnitOfMeasureId,
                                    UnitOfMeasureName = _soline.UnitOfMeasureName,
                                    Price = preciocot,
                                    Quantity = quantitycot,
                                    Amount = (preciocot * quantitycot),
                                    SubTotal = (preciocot * quantitycot),
                                    TaxAmount = taxamount,
                                    TaxId = _tax.TaxId,
                                    TaxCode = _tax.TaxCode,
                                    TaxPercentage = _tax.TaxPercentage,
                                    Total = (preciocot * quantitycot) + taxamount,

                                });
                            }
                        }

                        //7 Tarimas
                        _su = new SubProduct();
                        _su = await _context.SubProduct.Where(q => q.SubproductId == 7).FirstOrDefaultAsync();
                        _soline = new SalesOrderLine();
                        _soline = _so.SalesOrderLines.Where(q => q.SubProductId == _su.SubproductId).FirstOrDefault();

                        if (_soline != null)
                        {
                            taxamount = preciocot = quantitycot = 0;
                            preciocot = _soline.Price;
                            SubServicesWareHouse _subservicewarehouse = new SubServicesWareHouse();
                            _subservicewarehouse = await _context.SubServicesWareHouse
                                .Where(q => q.SubServiceId == _su.SubproductId)
                                .Where(q => q.DocumentDate >= Convert.ToDateTime(fechainicio))
                                .Where(q => q.DocumentDate <= Convert.ToDateTime(fechafin))
                                .FirstOrDefaultAsync();

                            if (_subservicewarehouse != null)
                            {
                                quantitycot = _subservicewarehouse.QuantityHours;

                                taxamount = ((preciocot * quantitycot)) * (_tax.TaxPercentage / 100);
                                ProformaInvoiceLineT.Add(new ProformaInvoiceLine
                                {
                                    SubProductId = _su.SubproductId,
                                    SubProductName = _su.ProductName,
                                    UnitOfMeasureId = _soline.UnitOfMeasureId,
                                    UnitOfMeasureName = _soline.UnitOfMeasureName,
                                    Price = preciocot,
                                    Quantity = quantitycot,
                                    Amount = (preciocot * quantitycot),
                                    SubTotal = (preciocot * quantitycot),
                                    TaxAmount = taxamount,
                                    TaxId = _tax.TaxId,
                                    TaxCode = _tax.TaxCode,
                                    TaxPercentage = _tax.TaxPercentage,
                                    Total = (preciocot * quantitycot) + taxamount,

                                });
                            }
                        }



                        //8 Comisión arancelarias
                        _su = new SubProduct();
                        _su = await _context.SubProduct.Where(q => q.SubproductId == 8).FirstOrDefaultAsync();
                        _soline = new SalesOrderLine();
                        _soline =  _so.SalesOrderLines.Where(q => q.SubProductId == _su.SubproductId).FirstOrDefault();

                        if(_soline!=null)
                        {
                            quantitycot = _InvoiceCalculationlist.Sum(q => q.Quantity);
                            preciocot = _soline.Price/100;
                            taxamount = ((preciocot * valfacturar) ) * (_tax.TaxPercentage/100);
                            ProformaInvoiceLineT.Add(new ProformaInvoiceLine
                            {
                                SubProductId = _su.SubproductId,
                                SubProductName = _su.ProductName,
                                UnitOfMeasureId = _soline.UnitOfMeasureId,
                                UnitOfMeasureName = _soline.UnitOfMeasureName,
                                Price = preciocot,
                                Quantity = quantitycot,
                                Amount = ((preciocot) * valfacturar), //preciocot * quantitycot,
                                SubTotal = ((preciocot) * valfacturar) ,
                                TaxAmount = taxamount,
                                TaxId = _tax.TaxId,
                                TaxCode = _tax.TaxCode,
                                TaxPercentage = _tax.TaxPercentage,
                                Total = (preciocot * valfacturar) + taxamount,
                            });

                        }


                        //9 Horas Extras
                        _su = new SubProduct();
                        _su = await _context.SubProduct.Where(q => q.SubproductId == 9).FirstOrDefaultAsync();
                        _soline = new SalesOrderLine();
                        _soline = _so.SalesOrderLines.Where(q => q.SubProductId == _su.SubproductId).FirstOrDefault();

                        if (_soline != null)
                        {
                            taxamount = preciocot = quantitycot = 0;
                           
                            List<Int64> _EmployeeExtraHours = new List<long>();
                            _EmployeeExtraHours = await _context.EmployeeExtraHours
                               // .Where(q => q. == _so.ProductId)
                                .Where(q => q.WorkDate >= Convert.ToDateTime(fechainicio))
                                .Where(q => q.WorkDate <= Convert.ToDateTime(fechafin))
                                .Select(q=>q.EmployeeExtraHoursId)
                                .ToListAsync();

                            List<Int64> _schedulelist = new List<Int64>();
                            _schedulelist = await _context.ScheduleSubservices
                                    .Where(q => q.ServiceId == _so.ProductId)
                                    .Where(q => q.SubServiceId == _su.SubproductId)
                                    .Select(q => q.ScheduleSubservicesId)
                                    .ToListAsync();

                            List<Int64> schedulecustomer =
                                   await _context.PaymentScheduleRulesByCustomer
                                           .Where(q => _schedulelist.Contains(q.ScheduleSubservicesId))
                                           .Where(q => q.CustomerId == _customer.CustomerId)
                                           .OrderByDescending(q => q.ScheduleSubservicesId)
                                           .Select(q=>q.ScheduleSubservicesId)
                                           .ToListAsync();

                            List<ScheduleSubservices> _schedule = new List<ScheduleSubservices>();
                            _schedule = await _context.ScheduleSubservices
                               .Where(q => schedulecustomer.Contains( q.ScheduleSubservicesId))
                               .ToListAsync();

                            List<EmployeeExtraHoursDetail> _employeeextra = await _context.EmployeeExtraHoursDetail
                                               .Where(q => q.CustomerId == _customer.CustomerId)
                                               .Where(q => _EmployeeExtraHours.Contains(q.EmployeeExtraHoursId))
                                               .ToListAsync();

                            double subtotal = 0;
                           
                            foreach (var item in _schedule)
                            {
                                List<EmployeeExtraHoursDetail> _EmployeeExtraHoursDetail = new List<EmployeeExtraHoursDetail>();
                                _EmployeeExtraHoursDetail = _employeeextra
                                                              .Where(q => q.StartTime >= item.StartTime)
                                                              .Where(q => q.EndTime <= item.EndTime)
                                                              .ToList();

                                //EmployeeExtraHours _EmployeeExtraHoursf = new EmployeeExtraHours();
                                //_EmployeeExtraHoursf = await _context.EmployeeExtraHours
                                //    .Where(q => q.EmployeeExtraHoursId == _EmployeeExtraHoursDetail.EmployeeExtraHoursId)
                                //    .FirstOrDefaultAsync();
                                preciocot += item.FactorHora;
                                subtotal += _EmployeeExtraHoursDetail.Sum(q=>q.QuantityHours) * item.FactorHora;
                            }

                            preciocot = preciocot / _schedule.Count;
                            quantitycot = await _context.EmployeeExtraHoursDetail
                                               .Where(q => q.CustomerId == _customer.CustomerId)
                                               .Where(q => _EmployeeExtraHours.Contains(q.EmployeeExtraHoursId))
                                              .Select(q => q.QuantityHours).SumAsync();


                            taxamount = (subtotal) * (_tax.TaxPercentage / 100);
                            ProformaInvoiceLineT.Add(new ProformaInvoiceLine
                            {
                                SubProductId = _su.SubproductId,
                                SubProductName = _su.ProductName,
                                UnitOfMeasureId = _soline.UnitOfMeasureId,
                                UnitOfMeasureName = _soline.UnitOfMeasureName,
                                Price = preciocot,
                                Quantity = quantitycot,
                                Amount = (subtotal),
                                SubTotal = subtotal,
                                TaxAmount = taxamount,
                                TaxId = _tax.TaxId,
                                TaxCode = _tax.TaxCode,
                                TaxPercentage = _tax.TaxPercentage,
                                Total = (subtotal) + taxamount,

                            });
                        }

                        //10 Alimentación
                        _su = new SubProduct();
                        _su = await _context.SubProduct.Where(q => q.SubproductId == 10).FirstOrDefaultAsync();
                        _soline = new SalesOrderLine();
                        _soline = _so.SalesOrderLines.Where(q => q.SubProductId == _su.SubproductId).FirstOrDefault();

                        if (_soline != null)
                        {
                            taxamount = preciocot = quantitycot = 0;
                            //preciocot = _soline.Price;
                            List<Int64> _EmployeeExtraHours = new List<long>();
                            _EmployeeExtraHours = await _context.EmployeeExtraHours
                                .Where(q => q.WorkDate >= Convert.ToDateTime(fechainicio))
                                .Where(q => q.WorkDate <= Convert.ToDateTime(fechafin))
                                .Select(q => q.EmployeeExtraHoursId)
                                .ToListAsync();

                            quantitycot = await _context.EmployeeExtraHoursDetail
                                                .Where(q => q.CustomerId == _customer.CustomerId)
                                                .Where(q => _EmployeeExtraHours.Contains(q.EmployeeExtraHoursId))
                                               .Select(q => q.QuantityHours).SumAsync();

                            List<EmployeeExtraHoursDetail> _EmployeeExtraHoursDetail = new List<EmployeeExtraHoursDetail>();

                            _EmployeeExtraHoursDetail = await _context.EmployeeExtraHoursDetail
                                                .Where(q => _EmployeeExtraHours.Contains(q.EmployeeExtraHoursId))
                                                 .ToListAsync();

                            foreach (var item in _EmployeeExtraHoursDetail)
                            {
                                List<Int64> _schedulelist = new List<Int64>();
                                _schedulelist = await _context.ScheduleSubservices
                                        .Where(q => q.SubServiceId == _su.SubproductId)
                                        .Select(q => q.ScheduleSubservicesId)
                                         .ToListAsync();

                                PaymentScheduleRulesByCustomer _scheduledetail
                                    = await _context.PaymentScheduleRulesByCustomer
                                             .Where(q => _schedulelist.Contains(q.ScheduleSubservicesId))
                                             .Where(q => q.CustomerId == _customer.CustomerId)
                                             .OrderByDescending(q => q.ScheduleSubservicesId)
                                             .FirstOrDefaultAsync();

                                ScheduleSubservices _schedule = new ScheduleSubservices();
                                 _schedule = await _context.ScheduleSubservices
                                    .Where(q => q.ScheduleSubservicesId == _scheduledetail.ScheduleSubservicesId)
                                    .FirstOrDefaultAsync();

                                if (_schedule.StartTime >= item.StartTime && _schedule.EndTime <= item.EndTime)
                                {
                                    preciocot += _schedule.Almuerzo + _schedule.Desayuno + _schedule.Cena;
                                }
                            }

                            taxamount = ((preciocot)) * (_tax.TaxPercentage / 100);
                            ProformaInvoiceLineT.Add(new ProformaInvoiceLine
                            {
                                SubProductId = _su.SubproductId,
                                SubProductName = _su.ProductName,
                                UnitOfMeasureId = _soline.UnitOfMeasureId,
                                UnitOfMeasureName = _soline.UnitOfMeasureName,
                                Price = preciocot,
                                Quantity = quantitycot,
                                Amount = (preciocot ),
                                SubTotal = (preciocot ),
                                TaxAmount = taxamount,
                                TaxId = _tax.TaxId,
                                TaxCode = _tax.TaxCode,
                                TaxPercentage = _tax.TaxPercentage,
                                Total = (preciocot * quantitycot) + taxamount,

                            });
                        }




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
                             ProductId = _so.ProductId,
                             ProductName = _so.ProductName,
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