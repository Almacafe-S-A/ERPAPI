using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using ERP.Contexts;
using ERPAPI.Contexts;
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
    [Route("api/CertificadoDeposito")]
    [ApiController]
    public class CertificadoDepositoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper mapper;

        public CertificadoDepositoController(ILogger<CertificadoDepositoController> logger, ApplicationDbContext context
            , IMapper mapper
            )
        {
            this.mapper = mapper;
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CertificadoDeposito paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCertificadoDepositoPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CertificadoDeposito> Items = new List<CertificadoDeposito>();
            try
            {
                var query = _context.CertificadoDeposito.AsQueryable();
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
        /// Obtiene el Listado de Certificado Deposito 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCertificadoDeposito()
        {
            List<CertificadoDeposito> Items = new List<CertificadoDeposito>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.CertificadoDeposito.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.IdCD).ToListAsync();
                }
                else
                {
                    Items = await _context.CertificadoDeposito.OrderByDescending(b => b.IdCD).ToListAsync();
                }
                
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(()=> Ok(Items));
        }


        /// <summary>
        /// Obtiene el Listado de recibnos de mercaderias segun el cliente y el servicio
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{clienteid}/{servicioid}/{pendienteliquidacion}")]
        public async Task<IActionResult> GetCertificadosPendientesAutorizar(int clienteid, int servicioid, int pendienteliquidacion)
        {
            List<CertificadoDeposito> Items = new List<CertificadoDeposito>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();

                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                Items = await _context.CertificadoDeposito
                    .Include(i => i._CertificadoLine)
                     .Where(p =>
                          //branchlist.Any(b => p.BranchId == b.BranchId) &&
                          p.CustomerId == clienteid &&
                          p.ServicioId == servicioid &&
                          p.PendienteAutorizar == null || p.PendienteAutorizar == true
                          // p._CertificadoLine.Sum(s=>s.CantidadDisponibleAutorizar).
                         //&& p.IdEstado != 6
                         //&& _context.LiquidacionLine.Any(a => a.GoodsReceivedLine.GoodsReceivedId == p.GoodsReceivedId)
                         )

                     .OrderByDescending(b => b.IdCD).ToListAsync();
               
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
        /// Obtiene el Listado de recibnos de mercaderias segun el cliente y el servicio
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{clienteid}/{servicioid}/{pendienteliquidacion}")]
        public async Task<IActionResult> CertificadosPendientesCustomerService(int clienteid, int servicioid, int pendienteliquidacion)
        {
            List<CertificadoDeposito> Items = new List<CertificadoDeposito>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();

                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (true)
                {

                    if (pendienteliquidacion == 1)
                    {
                        Items = await _context.CertificadoDeposito
                        .Where(p =>
                            //branchlist.Any(b => p.BranchId == b.BranchId) &&
                            p.CustomerId == clienteid &&
                            p.ServicioId == servicioid
                           //&& p.IdEstado != 6
                           //&& !_context.CertificadoDeposito.Any(a => a. == p.GoodsReceivedId)
                           )

                        .OrderByDescending(b => b.IdCD).ToListAsync();
                    }
                    else
                    {
                        Items = await _context.CertificadoDeposito
                       .Where(p =>
                            //branchlist.Any(b => p.BranchId == b.BranchId) &&
                            p.CustomerId == clienteid &&
                            p.ServicioId == servicioid
                           //&& p.IdEstado != 6
                           //&& _context.LiquidacionLine.Any(a => a.GoodsReceivedLine.GoodsReceivedId == p.GoodsReceivedId)
                           )

                       .OrderByDescending(b => b.IdCD).ToListAsync();
                    }
                }
                else
                {
                    return await Task.Run(() => Ok(Items));
                    // Items = await _context.GoodsReceived.OrderByDescending(b => b.GoodsReceivedId).ToListAsync();
                }
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
        /// Obtiene los certificados liberados
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetCertificadoDepositoLiberados(Int64 CustomerId)
        {
            List<CertificadoDeposito> Items = new List<CertificadoDeposito>();
            try
            {
                if (CustomerId > 0)
                {
                    List<Int64> Liberados = new List<long>();


                    List<Int64> CertId = await _context.CertificadoDeposito
                                           .Where(q => q.CustomerId == CustomerId).Select(q => q.IdCD).ToListAsync();

                    List<Int64> EndosoId = new List<long>();

                    EndosoId = await _context.EndososCertificados
                        .Where(q => q.CustomerId == CustomerId)
                        .Where(q => CertId.Contains(q.IdCD))
                        .Select(q => q.EndososCertificadosId).ToListAsync();

                    Liberados = await _context.EndososLiberacion
                           .Where(q => EndosoId.Contains(q.EndososId))
                              .Select(q => q.EndososId)
                              .ToListAsync();

                    List<Int64> PendientesLiberacion = EndosoId.Where(q => !Liberados.Contains(q)).ToList();

                    List<Int64> cdidpendientes = await _context.EndososCertificados
                                       .Where(q => PendientesLiberacion.Contains(q.EndososCertificadosId))
                                       .Select(q => q.IdCD).ToListAsync();

                    List<Int64> NoEndosadosYLiberados = CertId.Except(cdidpendientes).ToList();

                    //                NoEndosadosYLiberados.AddRange(EndosoId);

                    Items = await _context.CertificadoDeposito
                        .Where(q => NoEndosadosYLiberados.Contains(q.IdCD))
                        .ToListAsync();
                }
                else
                {
                    Items = await _context.CertificadoDeposito                     
                      .ToListAsync();
                }


            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los certificados de deposito por cliente.
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetCertificadoDepositoByCustomer(Int64 CustomerId)
        {
            List<CertificadoDeposito> Items = new List<CertificadoDeposito>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.CertificadoDeposito
                        .Where(p => branchlist.Any(b => p.BranchId == b.BranchId)
                        && p.CustomerId == CustomerId
                        //&& p.FechaVencimientoCertificado >= DateTime.Now
                        )
                        .OrderByDescending(b => b.IdCD).ToListAsync();
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Obtiene los certificados de deposito por cliente.
        /// </summary>
        /// <param name="_CertificadoDeposito"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSumCertificadoDepositoByCustomer([FromBody]CertificadoDepositoDTO _CertificadoDeposito)
        {
            List<CertificadoDeposito> Items = new List<CertificadoDeposito>();
            try
            {
               // Items = await _context.CertificadoDeposito.Where(q => q.CustomerId == CustomerId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }



        /// <summary>
        /// Obtiene los Datos de la CertificadoDeposito por medio del Id enviado.
        /// </summary>
        /// <param name="IdCD"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdCD}")]
        public async Task<IActionResult> GetCertificadoDepositoById(Int64 IdCD)
        {
            CertificadoDeposito Items = new CertificadoDeposito();
            try
            {
                EndososCertificados endoso = _context.EndososCertificados.Where(q => q.IdCD == IdCD ).FirstOrDefault();

                Items = await _context.CertificadoDeposito.Include(q=>q._CertificadoLine).Where(q => q.IdCD == IdCD).FirstOrDefaultAsync();
                if (Items == null)
                {
                    return NotFound();
                }
                Items.Endoso = endoso;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(()=> Ok(Items));
        }


        private Kardex GeneraKardexCertificado(CertificadoLine ccertificadoLine, CertificadoDeposito _CertificadoDeposito) {
            Kardex kardex = new Kardex
            {
                DocumentDate = _CertificadoDeposito.FechaCertificado,
                KardexDate = DateTime.Now,
                ProducId = _CertificadoDeposito.ServicioId,
                ProductName = _CertificadoDeposito.ServicioName,
                SubProducId = Convert.ToInt32(ccertificadoLine.SubProductId),
                SubProductName = ccertificadoLine.SubProductName,
                QuantityEntry = ccertificadoLine.Quantity,
                QuantityOut = 0,
                QuantityEntryBags = ccertificadoLine.Quantity,
                BranchId = _CertificadoDeposito.BranchId,
                BranchName = _CertificadoDeposito.BranchName,
                WareHouseId = Convert.ToInt32(ccertificadoLine.WarehouseId),
                WareHouseName = ccertificadoLine.WarehouseName,
                UnitOfMeasureId = ccertificadoLine.UnitMeasureId,
                UnitOfMeasureName = ccertificadoLine.UnitMeasurName,
                TypeOperationId = TipoOperacion.Entrada,
                TypeOperationName = "Entrada",
                Total = ccertificadoLine.Quantity,
                QuantityEntryCD = ccertificadoLine.Quantity,
                TotalCD = ccertificadoLine.Quantity,
                DocumentName = "Certficado de Depósito",
                DocumentId = ccertificadoLine.IdCD,
                DocType = 2

            };

            return kardex;


        }


        /// <summary>
        /// Obtiene los certificados de deposito por cliente.
        /// </summary>
        /// <param name="IdCD"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdCD}")]
        public async Task<ActionResult<CertificadoDeposito>> AprobarCertificado(int IdCD) {

            CertificadoDeposito certificado = await _context.CertificadoDeposito
                .Where(q => q.IdCD == IdCD)
                .Include(i => i._CertificadoLine)
                .FirstOrDefaultAsync();

            if (certificado == null)
            {
                return BadRequest();
            }
            foreach (var item in certificado._CertificadoLine)
            {
                ///Actualiza el saldo del detalle del recibo
                GoodsReceivedLine linearecibo = _context.GoodsReceivedLine
                    .Where(q => q.GoodsReceiveLinedId == item.GoodsReceivedLineId)
                    .FirstOrDefault();
                linearecibo.SaldoporCertificar = item.CantidadDisponible - item.Quantity;

                ///Actualiza el estado del recibo de Mercaderias
                GoodsReceived recibo = _context.GoodsReceived
                       .Where(q => q.GoodsReceivedId == item.ReciboId)
                       .Include(a => a._GoodsReceivedLine)
                       .FirstOrDefault();

                if (recibo._GoodsReceivedLine.Count() == recibo._GoodsReceivedLine.Where(q => q.SaldoporCertificar == 0).Count())
                {
                    recibo.Porcertificar = false;
                }
                _context.Kardex.Add(GeneraKardexCertificado(item, certificado));
                //_context.CertificadoLine.Add(item);
            }

            certificado.IdEstado = 6;
            certificado.Estado = "Vigente";

            await _context.SaveChangesAsync();

            return certificado;

        }


        /// <summary>
        /// Inserta una nueva CertificadoDeposito
        /// </summary>
        /// <param name="_CertificadoDeposito"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CertificadoDeposito>> Insert([FromBody]CertificadoDepositoDTO _CertificadoDeposito)
        {
            
            SolicitudCertificadoDeposito _SolicitudCertificado ;
            CertificadoDeposito _CertificadoDepositoq = new CertificadoDeposito();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //Solicitud de certificado
                    _context.CertificadoDeposito.Add(_CertificadoDeposito);
                    

                    _CertificadoDeposito.IdEstado = 5;
                    _CertificadoDeposito.Estado = "Enviado a Aprobación";
                    _CertificadoDeposito.Total = _CertificadoDeposito._CertificadoLine.Sum(s => s.Amount);
                    _CertificadoDeposito.Quantitysum = _CertificadoDeposito._CertificadoLine.Sum(s => s.Quantity);
                    _CertificadoDeposito.TotalDerechos = _CertificadoDeposito._CertificadoLine.Sum(s => s.DerechosFiscales);


                    _CertificadoDeposito.Producto = "Productos Varios";
                    if (_CertificadoDeposito._CertificadoLine.Where(q => q.PdaNo ==2).FirstOrDefault()==null)
                    {
                        _CertificadoDeposito.Producto = _CertificadoDeposito._CertificadoLine.FirstOrDefault().SubProductName;
                    }



                    _SolicitudCertificado = new SolicitudCertificadoDeposito(_CertificadoDeposito);

                    //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                    new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                    await _context.SaveChangesAsync();

                    _context.SolicitudCertificadoDeposito.Add(_SolicitudCertificado);
                    _context.SolicitudCertificadoLine.AddRange(_SolicitudCertificado._SolicitudCertificadoLine);
                    

                    await _context.SaveChangesAsync();

                    _CertificadoDeposito.SolicitudCertificadoId = (int)_SolicitudCertificado.IdSCD;                                      

                    await _context.SaveChangesAsync();

                    BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                    {
                        IdOperacion = _CertificadoDeposito.IdCD,
                        DocType = "CertificadoDeposito",
                        ClaseInicial =
                        Newtonsoft.Json.JsonConvert.SerializeObject(_CertificadoDeposito, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_CertificadoDeposito, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        Accion = "Insertar",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = _CertificadoDeposito.UsuarioCreacion,
                        UsuarioModificacion = _CertificadoDeposito.UsuarioModificacion,
                        UsuarioEjecucion = _CertificadoDeposito.UsuarioModificacion,

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

            return await Task.Run(()=> Ok(_CertificadoDeposito));
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<CertificadoDeposito>> AnularCD([FromBody]CertificadoDeposito _CertificadoDeposito)
        {
            CertificadoDeposito _CertificadoDepositoq = _CertificadoDeposito;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _CertificadoDepositoq = await _context.CertificadoDeposito
                                 .Where(q => q.IdCD == _CertificadoDeposito.IdCD)
                                .FirstOrDefaultAsync();

                        _CertificadoDepositoq.IdEstado = _CertificadoDeposito.IdEstado;
                        _CertificadoDepositoq.Estado = _CertificadoDeposito.Estado;

                        //SolicitudCertificadoDeposito _solicitudq = await (from c in _context.SolicitudCertificadoDeposito
                        //              .Where(q => q.NoCD == _CertificadoDepositoq.NoCD)
                        //                                                  select c
                        //              ).FirstOrDefaultAsync();

                        _context.Entry(_CertificadoDepositoq).CurrentValues.SetValues((_CertificadoDepositoq));

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _CertificadoDeposito.IdCD,
                            DocType = "CertificadoDeposito",
                            ClaseInicial =
                           Newtonsoft.Json.JsonConvert.SerializeObject(_CertificadoDepositoq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_CertificadoDeposito, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Anular",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _CertificadoDeposito.UsuarioCreacion,
                            UsuarioModificacion = _CertificadoDeposito.UsuarioModificacion,
                            UsuarioEjecucion = _CertificadoDeposito.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();


                        //SolicitudCertificadoDeposito _solicitud = _solicitudq;
                        //_solicitud.IdEstado = 3;
                        //_solicitud.Estado = "Anulado";
                        //_context.Entry(_solicitudq).CurrentValues.SetValues((_solicitud));

                        Kardex _kardexentrada = await (from c in _context.Kardex
                                                       .Include(q => q._KardexLine)
                                                       .Where(q => q.DocumentId == _CertificadoDeposito.IdCD)
                                                       .Where(q => q.DocumentName == "CD")
                                                       select c).FirstOrDefaultAsync();

                        Kardex _kardexsalida = new Kardex
                        {
                            KardexDate = _kardexentrada.KardexDate,
                            TypeOperationId = _kardexentrada.TypeOperationId,
                            TypeOperationName = "Salida",
                            DocumentId = _kardexentrada.DocumentId,
                            //DocumentName = _kardexentrada.DocumentName,
                            DocType = _kardexentrada.DocType,
                            DocumentName = _kardexentrada.DocumentName,
                            CustomerId = _kardexentrada.CustomerId,
                            CustomerName = _kardexentrada.CustomerName,
                            CurrencyId = _kardexentrada.CurrencyId,
                            CurrencyName = _kardexentrada.CustomerName,
                            DocumentDate = DateTime.Now,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _kardexentrada.UsuarioCreacion,
                            UsuarioModificacion = _CertificadoDeposito.UsuarioModificacion,

                        };
                        _kardexsalida.DocumentDate = DateTime.Now;
                        _kardexsalida.KardexDate = DateTime.Now;
                        _kardexsalida.TypeOperationName = "Salida";
                        List<KardexLine> _entradas = new List<KardexLine>();
                        _entradas.AddRange(_kardexentrada._KardexLine);
                        //  _kardexsalida._KardexLine.Clear();

                        _kardexsalida.KardexId = 0;

                        // await _context.SaveChangesAsync();

                        foreach (var item in _entradas)
                        {
                            _kardexsalida._KardexLine.Add(new KardexLine
                            {
                                //KardexId = _kardexsalida.KardexId,
                                //KardexLineId=0,
                                DocumentDate = item.DocumentDate,
                                // ProducId = _CertificadoDeposito.,
                                // ProductName = _GoodsReceivedq.ProductName,
                                //TotalBags = item.QuantitySacos + _KardexLine.TotalBags,
                                //QuantityEntryCD = item.Quantity / (1 + _subproduct.Merma),
                                SubProducId = item.SubProducId,
                                SubProductName = item.SubProductName,
                                QuantityEntry = 0,
                                QuantityOut = item.QuantityEntry,
                                QuantityEntryBags = 0,
                                BranchId = item.BranchId,
                                BranchName = item.BranchName,
                                WareHouseId = item.WareHouseId,
                                WareHouseName = item.WareHouseName,
                                UnitOfMeasureId = item.UnitOfMeasureId,
                                UnitOfMeasureName = item.UnitOfMeasureName,
                                TypeOperationId = 1,
                                TypeOperationName = "Salida",
                                Total = item.Total,
                                KardexDate = DateTime.Now,
                                QuantityOutCD = item.QuantityEntry,
                                TotalCD = item.TotalCD - (item.QuantityEntry),
                            });
                        }



                        _context.Kardex.Add(_kardexsalida);
                        //_context.CertificadoDeposito.Update(_CertificadoDepositoq);
                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        //return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_CertificadoDepositoq);
        }

        [HttpGet("[action]/{NoCD}")]
        public async Task<ActionResult> GetCertificadoDepositoByNoCD(Int64 NoCD)
        {
            CertificadoDeposito Items = new CertificadoDeposito();
            try
            {
                Items = await _context.CertificadoDeposito.Include(q => q._CertificadoLine).Where(q => q.IdCD == NoCD).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Actualiza la CertificadoDeposito
        /// </summary>
        /// <param name="_CertificadoDeposito"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CertificadoDeposito>> Update([FromBody]CertificadoDeposito _CertificadoDeposito)
        {
            CertificadoDeposito _CertificadoDepositoq = _CertificadoDeposito;
            try
            {
                _CertificadoDepositoq = await (from c in _context.CertificadoDeposito
                                 .Where(q => q.IdCD == _CertificadoDeposito.IdCD)
                                               select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CertificadoDepositoq).CurrentValues.SetValues((_CertificadoDeposito));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                //_context.CertificadoDeposito.Update(_CertificadoDepositoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(()=> Ok(_CertificadoDepositoq));
        }

        /// <summary>
        /// Elimina una CertificadoDeposito       
        /// </summary>
        /// <param name="_CertificadoDeposito"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CertificadoDeposito _CertificadoDeposito)
        {
            CertificadoDeposito _CertificadoDepositoq = new CertificadoDeposito();
            try
            {
                _CertificadoDepositoq = _context.CertificadoDeposito
                .Where(x => x.IdCD == (Int64)_CertificadoDeposito.IdCD)
                .FirstOrDefault();

                _context.CertificadoDeposito.Remove(_CertificadoDepositoq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(()=> Ok(_CertificadoDepositoq));

        }




        [HttpPost("[action]")]
        public async Task<ActionResult<GoodsReceived>> AgruparCertificados([FromBody]List<Int64> listacertificados)
        {
            List<CertificadoDeposito> _goodsreceivedlis = new List<CertificadoDeposito>();
            try
            {
                string inparams = "";
                foreach (var item in listacertificados)
                {
                    inparams += item + ",";
                }

                inparams = inparams.Substring(0, inparams.Length - 1);


                _goodsreceivedlis = await _context.CertificadoDeposito.Include(q => q._CertificadoLine)
                    .Where(q => listacertificados.Contains(q.IdCD)).ToListAsync();

                foreach (var item in _goodsreceivedlis)
                {
                    Int64 Id = 0;
                    Id= await _context.EndososCertificados                  
                    .Where(q => q.IdCD==item.IdCD)
                    .Select(q => q.EndososCertificadosId).FirstOrDefaultAsync();

                    if(Id>0)
                    {
                        EndososLiberacion _endosoliberado = new EndososLiberacion();
                        _endosoliberado= await _context.EndososLiberacion
                          .Where(q =>q.EndososId== Id)
                          .FirstOrDefaultAsync();

                        EndososCertificadosLine line = await _context.EndososCertificadosLine
                                                        .Where(q => q.EndososCertificadosLineId == _endosoliberado.EndososLineId)
                                                        .FirstOrDefaultAsync();

                        CertificadoLine _cline = item._CertificadoLine.Where(q => q.SubProductId == line.SubProductId).FirstOrDefault();
                        _cline.Quantity = _cline.Quantity - _endosoliberado.Saldo;


                    }
                    
                }


                



                //_goodsreceivedlis = await _context.CertificadoDeposito.Where(q => q.IdCD == Convert.ToInt64(listacertificados[0])).FirstOrDefault();

                //using (var command = _context.Database.GetDbConnection().CreateCommand())
                //{
                //    command.CommandText = ("  SELECT  grl.SubProductId,grl.UnitMeasureId, grl.SubProductName, grl.UnitMeasurName         "
                //   + " , SUM(Quantity) AS Cantidad, SUM(grl.IdCD) AS IdCD         "                    
                //   + " , SUM(grl.Quantity) * (grl.Price)  AS Total                            "
                //   + " ,Price "
                //   + $"  FROM CertificadoLine grl                 where  CertificadoLineId in ({inparams})                                "
                //   + "  GROUP BY grl.SubProductId,grl.UnitMeasureId, grl.SubProductName, grl.UnitMeasurName,grl.IdCD,grl.Price       "
                // );

                //    _context.Database.OpenConnection();
                //    using (var result = command.ExecuteReader())
                //    {
                //        // do something with result
                //        while (await result.ReadAsync())
                //        {
                //            _goodsreceivedlis._CertificadoLine.Add(new CertificadoLine
                //            {                                
                //                SubProductId = Convert.ToInt64(result["SubProductId"]),
                //                SubProductName = result["SubProductName"].ToString(),
                //                UnitMeasureId = Convert.ToInt64(result["UnitMeasureId"]),
                //                UnitMeasurName = result["UnitMeasurName"].ToString(),
                //                Quantity = Convert.ToInt32(result["Cantidad"]),
                //                IdCD = Convert.ToInt32(result["IdCD"]),
                //                Price = Convert.ToDouble(result["Price"]),

                //               // Total = Convert.ToDouble(result["Total"]),

                //            });
                //        }
                //    }
                //}


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