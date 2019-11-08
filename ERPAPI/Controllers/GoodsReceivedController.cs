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
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/GoodsReceived")]
    [ApiController]
    public class GoodsReceivedController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GoodsReceivedController(ILogger<GoodsReceivedController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de GoodsReceived paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsReceivedPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<GoodsReceived> Items = new List<GoodsReceived>();
            try
            {
                var query = _context.GoodsReceived.AsQueryable();
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
        /// Obtiene el Listado de GoodsReceivedes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsReceived()
        {
            List<GoodsReceived> Items = new List<GoodsReceived>();
            try
            {
                Items = await _context.GoodsReceived.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la GoodsReceived por medio del Id enviado.
        /// </summary>
        /// <param name="GoodsReceivedId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{GoodsReceivedId}")]
        public async Task<IActionResult> GetGoodsReceivedById(Int64 GoodsReceivedId)
        {
            GoodsReceived Items = new GoodsReceived();
            try
            {
                Items = await _context.GoodsReceived.Where(q => q.GoodsReceivedId == GoodsReceivedId).Include(q => q._GoodsReceivedLine).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsReceivedNoSelected()
        {
            List<GoodsReceived> Items = new List<GoodsReceived>();
            try
            {
                List<Int64> listayaprocesada = _context.RecibosCertificado
                                              .Where(q => q.IdRecibo > 0)
                                              .Select(q => q.IdRecibo).ToList();
                Items = await _context.GoodsReceived.Where(q => !listayaprocesada.Contains(q.GoodsReceivedId)).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }
        [HttpGet("[action]/{SubProductId}")]
        public async Task<ActionResult<SubProduct>> GetSubProductById(Int64 SubProductId)
        {
            SubProduct Items = new SubProduct();
            try
            {
                Items = await _context.SubProduct.Where(q => q.SubproductId == SubProductId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Inserta una nueva Alert
        /// </summary>
        /// <param name="_Alert"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Alert>> InsertAlert([FromBody]Alert _Alert)
        {
            Alert _Alertq = new Alert();
            try
            {
               // using (var transaction = _context.Database.BeginTransaction())
                //{
                   // try
                   // {
                        _Alertq = _Alert;
                        _context.Alert.Add(_Alertq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Alert.AlertId,
                            DocType = "Alert",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Alert, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Alert.UsuarioCreacion,
                            UsuarioModificacion = _Alert.UsuarioModificacion,
                            UsuarioEjecucion = _Alert.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                   //     transaction.Commit();
                    //}
                    //catch (Exception ex)
                  //  {
                       // transaction.Rollback();
                        //_logger.LogError($"Ocurrio un error: { ex.ToString() }");
                      //  throw ex;
                        // return BadRequest($"Ocurrio un error:{ex.Message}");
                    //}
               // }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Alertq));
        }
        /// <summary>
        /// Inserta una nueva GoodsReceived
        /// </summary>
        /// <param name="_GoodsReceived"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<GoodsReceived>> Insert([FromBody]GoodsReceivedDTO _GoodsReceived)
        {
            GoodsReceived _GoodsReceivedq = new GoodsReceived();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _GoodsReceivedq = _GoodsReceived;

                        BoletaDeSalida _boletadesalida = new BoletaDeSalida
                        {
                            BranchId = _GoodsReceived.BranchId,
                            BranchName = _GoodsReceived.BranchName,
                            CustomerId = _GoodsReceived.CustomerId,
                            CustomerName = _GoodsReceived.CustomerName,
                            DocumentDate = _GoodsReceived.DocumentDate,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Marca = _GoodsReceived.Marca,
                            Placa = _GoodsReceived.Placa,
                            Motorista = _GoodsReceived.Name,
                            Quantity = _GoodsReceived._GoodsReceivedLine.Select(q => q.QuantitySacos).Sum(),
                            SubProductId = _GoodsReceivedq._GoodsReceivedLine[0].SubProductId,
                            SubProductName = _GoodsReceivedq._GoodsReceivedLine[0].SubProductName,
                            CargadoId = 14,
                            Cargadoname = "Vacío",
                            UsuarioCreacion = _GoodsReceived.UsuarioCreacion,
                            UsuarioModificacion = _GoodsReceived.UsuarioModificacion,
                            UnitOfMeasureId = _GoodsReceivedq._GoodsReceivedLine[0].UnitOfMeasureId,
                            UnitOfMeasureName = _GoodsReceivedq._GoodsReceivedLine[0].UnitOfMeasureName,
                            WeightBallot = _GoodsReceivedq.WeightBallot,  
                            VigilanteId = _GoodsReceivedq.VigilanteId,
                            Vigilante = _GoodsReceivedq.VigilanteName,

                        };

                        _context.BoletaDeSalida.Add(_boletadesalida);
                         await  _context.SaveChangesAsync();


                        _GoodsReceivedq.ExitTicket = _boletadesalida.BoletaDeSalidaId;

                        _context.GoodsReceived.Add(_GoodsReceivedq);
                        // await _context.SaveChangesAsync();

                        foreach (var item in _GoodsReceivedq._GoodsReceivedLine)
                        {
                            item.GoodsReceivedId = _GoodsReceivedq.GoodsReceivedId;


                            Kardex _kardexmax = await (from c in _context.Kardex
                                                        .OrderByDescending(q => q.DocumentDate)
                                                           // .Take(1)
                                                       join d in _context.KardexLine on c.KardexId equals d.KardexId
                                                       where c.CustomerId == _GoodsReceivedq.CustomerId && d.SubProducId == item.SubProductId
                                                        && c.DocumentName != "CD" && d.WareHouseId == item.WareHouseId
                                                       select c
                                                      ).FirstOrDefaultAsync();

                            //Kardex _kardexmax = await (from kdx in _context.Kardex
                            //            .Where(q => q.CustomerId == _GoodsReceivedq.CustomerId)
                            //            from kdxline in _context.KardexLine
                            //                .Where(q => q.KardexId == kdx.KardexId)
                            //                .Where(o => o.SubProducId == item.SubProductId)
                            //                //.Where(q=>q.BranchId ==  _GoodsReceivedq.BranchId)
                            //                //.Where(q => q.WareHouseId == _GoodsReceivedq.WarehouseId)
                            //                .OrderByDescending(o => o.DocumentDate).Take(1)
                            //            select kdx).FirstOrDefaultAsync();

                            if (_kardexmax == null) { _kardexmax = new Kardex(); }


                            KardexLine _KardexLine = await _context.KardexLine
                                                                         .Where(q=>q.KardexId== _kardexmax.KardexId)
                                                                         .Where(q => q.SubProducId == item.SubProductId)
                                                                         .Where(q=>q.WareHouseId==item.WareHouseId)
                                                                         .Where(q => q.BranchId == _GoodsReceivedq.BranchId)
                                                                         .OrderByDescending(q => q.KardexLineId)
                                                                         .Take(1)
                                                                        .FirstOrDefaultAsync();

                            if (_KardexLine == null)
                            {
                                _KardexLine = new KardexLine();

                            }

                            SubProduct _subproduct = await (from c in _context.SubProduct
                                                     .Where(q => q.SubproductId == item.SubProductId)
                                                            select c
                                                     ).FirstOrDefaultAsync();
                            if (_subproduct.ProductTypeId == 3)
                            {
                                //Alert AlertP = new Alert();
                                Alert Alerta = new Alert();
                                Alerta.DocumentId = item.SubProductId;
                                Alerta.DocumentName = "LISTA PROHIBIDA";
                                Alerta.AlertName = "Productos";
                                Alerta.Code = "PRODUCT01";
                                Alerta.DescriptionAlert = "Lista de producto Prohibida";
                                Alerta.FechaCreacion = Convert.ToDateTime(item.FechaCreacion);
                                Alerta.FechaModificacion = Convert.ToDateTime(item.FechaModificacion);
                                Alerta.UsuarioCreacion = item.UsuarioCreacion;
                                Alerta.UsuarioModificacion = item.UsuarioModificacion;
                                // var AlertaP = await InsertAlert(Alerta);
                                _context.Alert.Add(Alerta);
                                //await _context.SaveChangesAsync();

                                BitacoraWrite _writealert = new BitacoraWrite(_context, new Bitacora
                                {
                                    IdOperacion = Alerta.AlertId,
                                    DocType = "Alert",
                                    ClaseInicial =
                                    Newtonsoft.Json.JsonConvert.SerializeObject(Alerta, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                                    Accion = "Insertar",
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = Alerta.UsuarioCreacion,
                                    UsuarioModificacion = Alerta.UsuarioModificacion,
                                    UsuarioEjecucion = Alerta.UsuarioModificacion,

                                });

                               // await _context.SaveChangesAsync();

                            }

                            _context.GoodsReceivedLine.Add(item);

                            item.Total = item.Quantity + _KardexLine.Total;
                           

                            _GoodsReceived.Kardex._KardexLine.Add(new KardexLine
                            {
                                DocumentDate = _GoodsReceivedq.DocumentDate,
                                ProducId = item.ProducId,
                                ProductName = item.ProductName,
                                SubProducId = item.SubProductId,
                                SubProductName = item.SubProductName,
                                QuantityEntry = item.Quantity,
                                QuantityOut = 0,
                                QuantityEntryBags = item.QuantitySacos,
                                BranchId = _GoodsReceivedq.BranchId,
                                BranchName = _GoodsReceivedq.BranchName,
                                WareHouseId = item.WareHouseId,
                                WareHouseName = item.WareHouseName,
                                UnitOfMeasureId = item.UnitOfMeasureId,
                                UnitOfMeasureName = item.UnitOfMeasureName,
                                TypeOperationId = 1,
                                TypeOperationName = "Entrada",
                                Total = item.Total,
                                TotalBags = item.QuantitySacos + _KardexLine.TotalBags,
                                QuantityEntryCD = item.Quantity - (item.Quantity * _subproduct.Merma),
                                TotalCD = _KardexLine.TotalCD + (item.Quantity - (item.Quantity * _subproduct.Merma)),
                            });
                        }//Fin Foreach

                        await _context.SaveChangesAsync();
                        _GoodsReceived.Kardex.DocType = 0;                      
                        _GoodsReceived.Kardex.DocName = "ReciboMercaderia/GoodsReceived";
                        _GoodsReceived.Kardex.DocumentDate = _GoodsReceivedq.DocumentDate;
                        _GoodsReceived.Kardex.FechaCreacion = DateTime.Now;
                        _GoodsReceived.Kardex.FechaModificacion = DateTime.Now;
                        _GoodsReceived.Kardex.TypeOperationId = 1;
                        _GoodsReceived.Kardex.TypeOperationName = "Entrada";
                        _GoodsReceived.Kardex.KardexDate = DateTime.Now;
                        _GoodsReceived.Kardex.DocumentName = "RM";

                        _GoodsReceived.Kardex.CustomerId = _GoodsReceivedq.CustomerId;
                        _GoodsReceived.Kardex.CustomerName = _GoodsReceivedq.CustomerName;
                        _GoodsReceived.Kardex.CurrencyId = _GoodsReceivedq.CurrencyId;
                        _GoodsReceived.Kardex.CurrencyName = _GoodsReceivedq.CurrencyName;
                        _GoodsReceived.Kardex.DocumentId = _GoodsReceivedq.GoodsReceivedId;
                        _GoodsReceived.Kardex.UsuarioCreacion = _GoodsReceivedq.UsuarioCreacion;
                        _GoodsReceived.Kardex.UsuarioModificacion = _GoodsReceivedq.UsuarioModificacion;

                        

                        if (_GoodsReceived.ControlId > 0)
                        {
                            _context.Kardex.Add(_GoodsReceived.Kardex);
                        }

                        await _context.SaveChangesAsync();

                        foreach (var item in _GoodsReceivedq._GoodsReceivedLine)
                        {

                            if (item.ControlPalletsId == null) { item.ControlPalletsId = 0;  }
                            ControlPallets _ControlPalletsq = await _context.ControlPallets.Where(q => q.ControlPalletsId == item.ControlPalletsId)
                           .FirstOrDefaultAsync();

                            if (_ControlPalletsq != null)
                            {
                                _ControlPalletsq.QQPesoBruto = _GoodsReceivedq.PesoBruto;
                                _ControlPalletsq.QQPesoNeto = _GoodsReceivedq.PesoNeto;
                                _ControlPalletsq.QQPesoFinal = _GoodsReceivedq.PesoNeto2;

                                _context.Entry(_ControlPalletsq).CurrentValues.SetValues((_ControlPalletsq));
                            }


                          
                        }

                        await _context.SaveChangesAsync();

                        BoletaDeSalida _bol = await _context.BoletaDeSalida
                                              .Where(q => q.BoletaDeSalidaId == _boletadesalida.BoletaDeSalidaId).FirstOrDefaultAsync();

                        _bol.GoodsReceivedId = _GoodsReceivedq.GoodsReceivedId;
                        _context.Entry(_bol).CurrentValues.SetValues((_bol));

                        await _context.SaveChangesAsync();
                        JournalEntryConfiguration _journalentryconfiguration = await (_context.JournalEntryConfiguration
                                                                       .Where(q => q.TransactionId == 1)
                                                                       .Where(q => q.BranchId == _GoodsReceivedq.BranchId)
                                                                       .Where(q => q.EstadoName == "Activo")
                                                                       .Include(q => q.JournalEntryConfigurationLine)
                                                                       ).FirstOrDefaultAsync();

                        BitacoraWrite _writejec = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _GoodsReceived.CustomerId,
                            DocType = "JournalEntryConfiguration",
                            ClaseInicial =
                             Newtonsoft.Json.JsonConvert.SerializeObject(_journalentryconfiguration, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_journalentryconfiguration, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "InsertGoodsReceived",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _GoodsReceived.UsuarioCreacion,
                            UsuarioModificacion = _GoodsReceived.UsuarioModificacion,
                            UsuarioEjecucion = _GoodsReceived.UsuarioModificacion,

                        });

                        // await _context.SaveChangesAsync();

                        double sumacreditos = 0, sumadebitos = 0;
                        if (_journalentryconfiguration != null)
                        {
                            //Crear el asiento contable configurado
                            //.............................///////
                            JournalEntry _je = new JournalEntry
                            {
                                Date = _GoodsReceivedq.OrderDate,
                                Memo = "Bienes Recibidos",
                                DatePosted = _GoodsReceivedq.OrderDate,
                                ModifiedDate = DateTime.Now,
                                CreatedDate = DateTime.Now,
                                ModifiedUser = _GoodsReceivedq.UsuarioModificacion,
                                CreatedUser = _GoodsReceivedq.UsuarioCreacion,
                                DocumentId = _GoodsReceivedq.GoodsReceivedId,
                            };



                            foreach (var item in _journalentryconfiguration.JournalEntryConfigurationLine)
                            {

                                GoodsReceivedLine _iline = new GoodsReceivedLine();
                                _iline = _GoodsReceivedq._GoodsReceivedLine.Where(q => q.SubProductId == item.SubProductId).FirstOrDefault();
                                if (_iline != null || item.SubProductName.ToUpper().Contains(("Impuesto").ToUpper()))
                                {
                                    if (!item.AccountName.ToUpper().Contains(("Impuestos sobre ventas").ToUpper())
                                           && !item.AccountName.ToUpper().Contains(("Sobre Servicios Diversos").ToUpper()))
                                    {
                                        _je.JournalEntryLines.Add(new JournalEntryLine
                                        {
                                            AccountId = Convert.ToInt32(item.AccountId),
                                            Description = item.AccountName,
                                            Credit = item.DebitCredit == "Credito" ? _iline.Total : 0,
                                            Debit = item.DebitCredit == "Debito" ? _iline.Total : 0,
                                            CreatedDate = DateTime.Now,
                                            ModifiedDate = DateTime.Now,
                                            CreatedUser = _GoodsReceivedq.UsuarioCreacion,
                                            ModifiedUser = _GoodsReceivedq.UsuarioModificacion,
                                            Memo = "",
                                        });

                                        sumacreditos += item.DebitCredit == "Credito" ? _iline.Total : 0;
                                        sumadebitos += item.DebitCredit == "Debito" ? _iline.Total : 0;
                                    }
                                    else
                                    {
                                        _je.JournalEntryLines.Add(new JournalEntryLine
                                        {
                                            AccountId = Convert.ToInt32(item.AccountId),
                                            Description = item.AccountName,
                                            CreatedDate = DateTime.Now,
                                            ModifiedDate = DateTime.Now,
                                            CreatedUser = _GoodsReceivedq.UsuarioCreacion,
                                            ModifiedUser = _GoodsReceivedq.UsuarioModificacion,
                                            Memo = "",
                                        });

                                        //sumacreditos += item.DebitCredit == "Credito" ? _Invoiceq.Tax + _Invoiceq.Tax18 : 0;
                                        //sumadebitos += item.DebitCredit == "Debito" ? _Invoiceq.Tax + _Invoiceq.Tax18 : 0;
                                    }
                                }

                                // _context.JournalEntryLine.Add(_je);

                            }


                            if (sumacreditos != sumadebitos)
                            {
                                transaction.Rollback();
                                _logger.LogError($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos{sumacreditos}");
                                return BadRequest($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos{sumacreditos}");
                            }

                            _je.TotalCredit = sumacreditos;
                            _je.TotalDebit = sumadebitos;
                            _context.JournalEntry.Add(_je);

                            await _context.SaveChangesAsync();
                        }

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _GoodsReceivedq.GoodsReceivedId,
                            DocType = "GoodsReceived",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsReceivedq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsReceivedq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _GoodsReceivedq.UsuarioCreacion,
                            UsuarioModificacion = _GoodsReceivedq.UsuarioModificacion,
                            UsuarioEjecucion = _GoodsReceivedq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();


                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsReceivedq));
        }

        /// <summary>
        /// Actualiza la GoodsReceived
        /// </summary>
        /// <param name="_GoodsReceived"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<GoodsReceived>> Update([FromBody]GoodsReceived _GoodsReceived)
        {
            GoodsReceived _GoodsReceivedq = _GoodsReceived;
            try
            {
                _GoodsReceivedq = await (from c in _context.GoodsReceived
                                 .Where(q => q.GoodsReceivedId == _GoodsReceived.GoodsReceivedId)
                                         select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_GoodsReceivedq).CurrentValues.SetValues((_GoodsReceived));

                //_context.GoodsReceived.Update(_GoodsReceivedq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsReceivedq));
        }

        /// <summary>
        /// Elimina una GoodsReceived       
        /// </summary>
        /// <param name="_GoodsReceived"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]GoodsReceived _GoodsReceived)
        {
            GoodsReceived _GoodsReceivedq = new GoodsReceived();
            try
            {
                _GoodsReceivedq = _context.GoodsReceived
                .Where(x => x.GoodsReceivedId == (Int64)_GoodsReceived.GoodsReceivedId)
                .FirstOrDefault();

                _context.GoodsReceived.Remove(_GoodsReceivedq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsReceivedq));

        }


        [HttpPost("[action]")]
        public async Task<ActionResult<GoodsReceived>> AgruparRecibos([FromBody]List<Int64> listarecibos)
        {
            GoodsReceived _goodsreceivedlis = new GoodsReceived();
            try
            {
                string inparams = "";
                foreach (var item in listarecibos)
                {
                    inparams += item +",";
                }

                inparams = inparams.Substring(0,inparams.Length-1);
                // string[] ids = listarecibos.Split(',');
                _goodsreceivedlis = await _context.GoodsReceived.Where(q => q.GoodsReceivedId == Convert.ToInt64(listarecibos[0])).FirstOrDefaultAsync();
                //List<GoodsReceivedLineDTO> d = _context.Query<GoodsReceivedLineDTO>().FromSql (
                //("  SELECT  grl.SubProductId, grl.SubProductName, grl.UnitOfMeasureName         "
                // + " , SUM(Quantity) AS Cantidad, SUM(grl.QuantitySacos) AS CantidadSacos         "
                // + "  , SUM(grl.Price) Precio, SUM(grl.Total) AS Total                            "
                // + $"  FROM GoodsReceivedLine grl                 where  GoodsReceivedId in ({inparams})                                "
                // + "  GROUP BY grl.SubProductId, grl.SubProductName, grl.UnitOfMeasureName        "
                // )
                //    ).AsNoTracking().ToList();
                //.Where(q => listarecibos.Contains(q.GoodsReceivedId)).ToList();

              //  List<GoodsReceivedLineDTO> d = new List<GoodsReceivedLineDTO>();
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = ("  SELECT  grl.SubProductId,grl.UnitOfMeasureId, grl.SubProductName, grl.UnitOfMeasureName         "
                 + " , SUM(Quantity) AS Cantidad, SUM(grl.QuantitySacos) AS CantidadSacos         "
                  //+ "  , SUM(grl.Price) Precio, SUM(grl.Total) AS Total                            "
                  + "  , grl.Price as Precio, SUM(grl.Quantity) * (grl.Price)  AS Total                            "
                 + $"  FROM GoodsReceivedLine grl                 where  GoodsReceivedId in ({inparams})                                "
                 + "  GROUP BY grl.SubProductId,grl.UnitOfMeasureId, grl.SubProductName, grl.UnitOfMeasureName,grl.Price        "
                 );

                   _context.Database.OpenConnection();
                    using (var result = command.ExecuteReader())
                    {
                        // do something with result
                        while (await result.ReadAsync())
                        {
                            _goodsreceivedlis._GoodsReceivedLine.Add(new GoodsReceivedLine {
                                SubProductId = Convert.ToInt64(result["SubProductId"]),
                                SubProductName = result["SubProductName"].ToString(),
                                UnitOfMeasureId = Convert.ToInt64(result["UnitOfMeasureId"]),
                                UnitOfMeasureName = result["UnitOfMeasureName"].ToString(),
                                Quantity = Convert.ToDouble(result["Cantidad"]),
                                QuantitySacos = Convert.ToInt32(result["CantidadSacos"]),
                                Price = Convert.ToDouble(result["Precio"]),
                                Total = Convert.ToDouble(result["Total"]),
                                
                            });
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                throw ex;
            }

            return await Task.Run(() => Ok(_goodsreceivedlis));
        }





    }
}