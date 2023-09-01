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
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/GoodsDeliveryAuthorization")]
    [ApiController]
    public class GoodsDeliveryAuthorizationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GoodsDeliveryAuthorizationController(ILogger<GoodsDeliveryAuthorizationController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de GoodsDeliveryAuthorization paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveryAuthorizationPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<GoodsDeliveryAuthorization> Items = new List<GoodsDeliveryAuthorization>();
            try
            {
                var query = _context.GoodsDeliveryAuthorization.AsQueryable();
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

           
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el Listado de GoodsDeliveryAuthorizationes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveryAuthorization()
        {
            List<GoodsDeliveryAuthorization> Items = new List<GoodsDeliveryAuthorization>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.GoodsDeliveryAuthorization.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.GoodsDeliveryAuthorizationId).ToListAsync();
                }
                else
                {
                    Items = await _context.GoodsDeliveryAuthorization.OrderByDescending(b => b.GoodsDeliveryAuthorizationId).ToListAsync();
                }
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
        /// Obtiene los certificados de deposito por cliente.
        /// </summary>
        /// <param name="IdCD"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdCD}")]
        public async Task<ActionResult<GoodsDeliveryAuthorization>> Revisar(int IdCD)
        {

            GoodsDeliveryAuthorization autorizacion = await _context.GoodsDeliveryAuthorization
                .Where(q => q.GoodsDeliveryAuthorizationId == IdCD)
                //.Include(i => i.autor)
                .FirstOrDefaultAsync();

            if (autorizacion == null)
            {
                return BadRequest();
            }



            autorizacion.EstadoId = 6;
            autorizacion.Estado = "Revisado";
            autorizacion.UsuarioModificacion = User.Identity.Name;
            autorizacion.FechaModificacion = DateTime.Now;
            autorizacion.UsuarioRevisor = User.Identity.Name;

            await _context.SaveChangesAsync();

            return autorizacion;

        }

        /// <summary>
        /// Obtiene los certificados de deposito por cliente.
        /// </summary>
        /// <param name="IdCD"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdCD}")]
        public async Task<ActionResult<GoodsDeliveryAuthorization>> Aprobar(int IdCD)
        {

            GoodsDeliveryAuthorization autorizacion = await _context.GoodsDeliveryAuthorization
                .Include(i => i.GoodsDeliveryAuthorizationLine)
                .Where(q => q.GoodsDeliveryAuthorizationId == IdCD)
                //.Include(i => i.autor)
                .FirstOrDefaultAsync();

            if (autorizacion == null)
            {
                return BadRequest();
            }



            autorizacion.EstadoId = 6;
            autorizacion.Estado = "Aprobado";

            autorizacion.UsuarioModificacion = User.Identity.Name;
            autorizacion.FechaModificacion = DateTime.Now;
            autorizacion.UsuarioAprobacion = User.Identity.Name;
            


            Numalet let;
            let = new Numalet();
            let.SeparadorDecimalSalida = autorizacion.GoodsDeliveryAuthorizationLine
                .FirstOrDefault().UnitOfMeasureName;
            let.MascaraSalidaDecimal = "00/100 ";
            let.ApocoparUnoParteEntera = true;
            autorizacion.TotalUnidadesLetras = let.ToCustomCardinal((autorizacion.TotalCantidad))
                .ToUpper();

            foreach (var item in autorizacion.GoodsDeliveryAuthorizationLine)
            {
                item.Saldo = item.Quantity;
            }
            await _context.SaveChangesAsync();

            return autorizacion;

        }



        /// <summary>
        /// Obtienne los productos de los recibos de mercaderias que han sido liquidados 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDetalleCertificadosPendientes([FromQuery(Name = "Recibos")] int[] certificados)
        {

            
            List<GoodsDeliveryAuthorizationLine> pendientes = new List<GoodsDeliveryAuthorizationLine>();
            if (certificados.Count() == 0) return Ok(pendientes);

            List<EndososCertificados> endosos = _context.EndososCertificados
                .Include(i => i.EndososCertificadosLine)
                .Where(q => certificados.Any(a => a == q.IdCD) && q.Saldo == q.CantidadEndosar).ToList();

            if (endosos.Count>0)
            {
                return BadRequest($"Los Certificados {String.Join(",",endosos.Select(s => s.IdCD).ToList())} se encuentran endosados y no tienen saldo disponible a autorizar, no se puede emitir autorización");
            }

            try
            {
                List<CertificadoLine> certificadoLines = await _context.CertificadoLine
                    .Where(q => certificados.Any(idCD => idCD == q.IdCD)).ToListAsync();
                certificadoLines = (from cd in certificadoLines
                                    select new CertificadoLine() {
                                        Amount = cd.Amount,
                                        CantidadDisponible = cd.CantidadDisponible,
                                        CantidadDisponibleAutorizar = cd.CantidadDisponibleAutorizar.HasValue ? cd.CantidadDisponibleAutorizar: cd.Quantity,
                                        CertificadoLineId = cd.CertificadoLineId,
                                        Observaciones = cd.Observaciones,
                                        Merma = cd.Merma,
                                        PdaNo = cd.PdaNo,
                                        Quantity =cd.Quantity,
                                        Description = cd.Description,
                                        IdCD = cd.IdCD,
                                        DerechosFiscales = cd.DerechosFiscales, 
                                        GoodsReceivedLine = cd.GoodsReceivedLine,
                                        GoodsReceivedLineId = cd.GoodsReceivedLineId,
                                        Price = cd.Price,
                                        ReciboId = cd.ReciboId,
                                        Saldo = cd.Saldo,
                                        SaldoEndoso = cd.SaldoEndoso,
                                        //SubProduct = cd.SubProduct,
                                        SubProductId = cd.SubProductId,
                                        SubProductName = cd.SubProductName,
                                        TotalCantidad = cd.TotalCantidad,
                                        UnitMeasureId = cd.UnitMeasureId,   
                                        UnitMeasurName = cd.UnitMeasurName,
                                        ValorUnitarioDerechos = cd.ValorUnitarioDerechos,
                                        WarehouseId = cd.WarehouseId,
                                        WarehouseName = cd.WarehouseName,
                                        
                                    
                                    }).ToList();

               

                pendientes =  (from cd in certificadoLines.Where(q => q.CantidadDisponibleAutorizar >0 || q.CantidadDisponibleAutorizar == null)
                                           .GroupBy(g => new
                                           {
                                               g.IdCD,
                                               g.PdaNo,
                                               g.SubProductId,
                                               g.WarehouseName,
                                               g.UnitMeasurName,
                                               g.UnitMeasureId,
                                               g.WarehouseId,
                                               g.SubProductName,
                                               //g.Amount,
                                               g.Price,
                                               g.ValorUnitarioDerechos,
                                           })
                                    select new GoodsDeliveryAuthorizationLine()
                                           {
                                               GoodsDeliveryAuthorizationId = 0,
                                               UnitOfMeasureName = cd.Key.UnitMeasurName,
                                               UnitOfMeasureId = (long)cd.Key.UnitMeasureId,
                                               Quantity = 0,
                                               SubProductId = (long)cd.Key.SubProductId,
                                               SubProductName = cd.Key.SubProductName,
                                               NoCertificadoDeposito = (int)cd.Key.IdCD,
                                               Price = (decimal)cd.Key.Price,
                                               WarehouseId = (int)cd.Key.WarehouseId,
                                               WarehouseName = cd.Key.WarehouseName,
                                               valorcertificado = 0,                                          
                                               SaldoProducto = (decimal)cd.Sum(s => s.CantidadDisponibleAutorizar),
                                               ValorUnitarioDerechos = cd.Key.ValorUnitarioDerechos,
                                               ValorImpuestos = 0,
                                               DerechosFiscales = cd.Sum(s =>s.DerechosFiscales),
                                               Pda = cd.Key.PdaNo,
                                           }).ToList();

                 endosos = _context.EndososCertificados
                .Include(i => i.EndososCertificadosLine)
                .Where(q => certificados.Any(a => a == q.IdCD) ).ToList();

                List<EndososCertificadosLine> endososCertificadosLines = _context.EndososCertificadosLine
                    .Include(i => i.EndososCertificados)
                    .Include(i => i.EndososLiberacion)
                   .Where(q => endosos.Any(a => a.EndososCertificadosId == q.EndososCertificadosId)).ToList();

                foreach (var endosodetalle in endososCertificadosLines)
                {
                    decimal saldoendoso = endosodetalle.Quantity - endosodetalle.EndososLiberacion.Sum(s => s.Quantity);
                    foreach (var item in pendientes)
                    {
                        
                        if (endosodetalle.Pda == item.Pda && endosodetalle.EndososCertificados.NoCD == item.NoCertificadoDeposito )
                        {
                            decimal saldooriginal = item.SaldoProducto;
                            item.SaldoProducto =saldoendoso> item.SaldoProducto?item.SaldoProducto:item.SaldoProducto - saldoendoso;
                            saldoendoso = saldoendoso - (saldooriginal - item.SaldoProducto);
                        }
                    }
                }

                    
                //if (pendientes.Count>7)
                //{
                //    return BadRequest("La autorizacion solo puede contener 7 lineas de detalle");
                //}

                return Ok(pendientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Ocurrio un error:" + ex.Message);
            }
        }
        /// <summary>
        /// Obtiene los Datos de la GoodsDeliveryAuthorization por medio del Id enviado.
        /// </summary>
        /// <param name="GoodsDeliveryAuthorizationId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{GoodsDeliveryAuthorizationId}")]
        public async Task<IActionResult> GetGoodsDeliveryAuthorizationById(Int64 GoodsDeliveryAuthorizationId)
        {
            GoodsDeliveryAuthorization Items = new GoodsDeliveryAuthorization();
            try
            {
                Items = await _context.GoodsDeliveryAuthorization
                       .Include(q=>q.GoodsDeliveryAuthorizationLine)
                    //.Include(i => i.goodsDeliveryAuthorizedSignatures)
                       .Where(q => q.GoodsDeliveryAuthorizationId == GoodsDeliveryAuthorizationId).FirstOrDefaultAsync();
                if (Items!=null)
                {
                    Items.Firmas = _context.goodsDeliveryAuthorizedSignatures.Where(q => q.GoodsDeliveryAuthorizationId == GoodsDeliveryAuthorizationId)
                   .Select(s => s.CustomerAuthorizedSignatureId).ToArray();
                }
               
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveryAuthorizationNoSelected()
        {
            List<GoodsDeliveryAuthorization> Items = new List<GoodsDeliveryAuthorization>();
            try
            {
                List<Int64> listayaprocesada = _context.GoodsDeliveredLine
                                              .Where(q => q.NoAR > 0)
                                              .Select(q => q.NoAR).ToList();

                Items = await _context.GoodsDeliveryAuthorization.Where(q => !listayaprocesada.Contains(q.GoodsDeliveryAuthorizationId)).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveryAuthorizationNoSelectedBoletaSalida()
        {
            List<GoodsDeliveryAuthorization> Items = new List<GoodsDeliveryAuthorization>();
            try
            {
                List<int> listayaprocesada = _context.BoletaDeSalida
                                              .Select(q => q.DocumentoId).ToList();

                Items = await _context.GoodsDeliveryAuthorization.Where(q => !listayaprocesada.Contains((int)q.GoodsDeliveryAuthorizationId)).ToListAsync();
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
        /// Inserta una nueva GoodsDeliveryAuthorization
        /// </summary>
        /// <param name="_GoodsDeliveryAuthorization"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<GoodsDeliveryAuthorization>> Insert([FromBody]GoodsDeliveryAuthorizationDTO _GoodsDeliveryAuthorization)
        {
            GoodsDeliveryAuthorization _GoodsDeliveryAuthorizationq = new GoodsDeliveryAuthorization();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _GoodsDeliveryAuthorization.TotalAutorizado = _GoodsDeliveryAuthorization.GoodsDeliveryAuthorizationLine.Sum(s => s.valorcertificado);
                        _GoodsDeliveryAuthorization.TotalCantidad = _GoodsDeliveryAuthorization.GoodsDeliveryAuthorizationLine.Sum(s => s.Quantity); 
                        _GoodsDeliveryAuthorization.ProductoAutorizado = _GoodsDeliveryAuthorization.GoodsDeliveryAuthorizationLine.FirstOrDefault().SubProductName;
                        _GoodsDeliveryAuthorizationq = _GoodsDeliveryAuthorization;
                        _GoodsDeliveryAuthorizationq.Estado = "Revisión";
                        _GoodsDeliveryAuthorizationq.EstadoId = 5;
                        _GoodsDeliveryAuthorizationq.Certificados = String.Join(", ", _GoodsDeliveryAuthorization.GoodsDeliveryAuthorizationLine.Select(s => s.NoCertificadoDeposito).Distinct().ToArray());

                        
                        //Firmas

                        foreach (var firma in _GoodsDeliveryAuthorization.Firmas)
                        {
                            CustomerAuthorizedSignature firmaautorizada = _context.CustomerAuthorizedSignature
                                .Where(q => q.CustomerAuthorizedSignatureId == firma).FirstOrDefault();
                            _GoodsDeliveryAuthorizationq.goodsDeliveryAuthorizedSignatures.Add(
                                new GoodsDeliveryAuthorizedSignatures
                                {
                                    CustomerAuthorizedSignatureId = firma,
                                    NombreAutorizado=firmaautorizada.Nombre,

                                });
                            
                        }

                        _context.GoodsDeliveryAuthorization.Add(_GoodsDeliveryAuthorizationq);

                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();


                        foreach (var item in _GoodsDeliveryAuthorizationq.GoodsDeliveryAuthorizationLine)
                        {
                            CertificadoDeposito certificadoDeposito = await _context.CertificadoDeposito
                                .Where(q => q.IdCD == item.NoCertificadoDeposito).FirstOrDefaultAsync();

                            if (item.UnitOfMeasure != null )
                            {
                                item.UnitOfMeasureId = item.UnitOfMeasure.UnitOfMeasureId;
                                item.UnitOfMeasureName = item.UnitOfMeasure.UnitOfMeasureName;
                                item.UnitOfMeasure = null;
                            }

                            if (item.Product != null) {
                                item.SubProductId = item.Product.SubProductId;
                                item.SubProductName = item.Product.SubProductName;
                                item.Product= null;
                            
                            }
                            
                            SubProduct _subproduct = await (from c in _context.SubProduct
                                                     .Where(q => q.SubproductId == item.SubProductId)
                                                            select c
                                                     ).FirstOrDefaultAsync();
                            if (item.CertificadoLineId>0 && item.SaldoProducto < item.Quantity)
                            {
                                return await Task.Run(() => BadRequest($"La cantidad a retirar no puede ser superior al total del ciertificado"));
                            }
                            ////////Kardex Autorizacion/////////////
                            _context.Kardex.Add(new Kardex
                            {
                                KardexDate = DateTime.Now,
                                DocumentDate = _GoodsDeliveryAuthorization.DocumentDate,
                                CustomerId = _GoodsDeliveryAuthorization.CustomerId,
                                CustomerName = _GoodsDeliveryAuthorization.CustomerName,
                                ProducId = _GoodsDeliveryAuthorizationq.ProductId,
                                ProductName = _GoodsDeliveryAuthorizationq.ProductName,
                                SubProducId = Convert.ToInt32(item.SubProductId),
                                SubProductName = item.SubProductName,
                                QuantityEntry = item.Quantity,
                                QuantityOut = 0,
                                QuantityEntryBags = 0,
                                Precio = item.Price,
                                BranchId = _GoodsDeliveryAuthorization.BranchId,
                                BranchName = _GoodsDeliveryAuthorization.BranchName,
                                WareHouseId = Convert.ToInt32(item.WarehouseId),
                                WareHouseName = item.WarehouseName,
                                UnitOfMeasureId = item.UnitOfMeasureId,
                                UnitOfMeasureName = item.UnitOfMeasureName,
                                TypeOperationId = TipoOperacion.Entrada,
                                TypeOperationName = "Entrada",
                                DocumentName = "Autorizacion de Retiro",
                                DocType = 3,
                                DocumentId = _GoodsDeliveryAuthorizationq.GoodsDeliveryAuthorizationId,
                                DocumentLine= (int)item.GoodsDeliveryAuthorizationLineId,
                                Total = item.Quantity,
                                SourceDocumentId = (int)_GoodsDeliveryAuthorizationq.GoodsDeliveryAuthorizationId,
                                SourceDocumentName = "Autorizacion de Retiro",
                                SourceDocumentLine = (int)item.GoodsDeliveryAuthorizationLineId,
                                PdaNo = item.Pda,

                            });

                            


                            decimal cantautorizar = item.Quantity;

                            List<CertificadoLine> certificadoLines =  _context.CertificadoLine.Where(q => q.IdCD == item.NoCertificadoDeposito && q.PdaNo == item.Pda).ToList();
                            foreach (var detallecert in certificadoLines)
                            {
                                if (cantautorizar<=0)                                
                                {
                                    break;
                                }

                                detallecert.CantidadDisponibleAutorizar = detallecert.CantidadDisponibleAutorizar.HasValue ? detallecert.CantidadDisponibleAutorizar : detallecert.Quantity;
                                decimal autorizado = detallecert.CantidadDisponibleAutorizar >= cantautorizar ? cantautorizar : (decimal)detallecert.CantidadDisponibleAutorizar;
                                decimal saldo = (decimal)detallecert.CantidadDisponibleAutorizar - autorizado;
                                detallecert.CantidadDisponibleAutorizar = saldo;

                                cantautorizar -= autorizado;

                                List<Kardex> kardexCertificadodeposito = await _context.Kardex.Where(q =>
                                    q.DocType == 2
                                    && q.DocumentId == certificadoDeposito.IdCD
                                    && q.SubProducId == item.SubProductId
                                    && q.DocumentLine == detallecert.CertificadoLineId
                                    ).ToListAsync();
                                Kardex kardexMaxCertificadodeposito =
                                    kardexCertificadodeposito
                                    .Where(q => q.KardexId == kardexCertificadodeposito
                                    .Select(s => s.KardexId).Max()).FirstOrDefault();

                                if (kardexMaxCertificadodeposito == null)
                                {
                                    BadRequest($"No se Encontro Kardex Para este Producto en el Certificado");
                                }
                                
                            }

                        }
                          

                                                                     
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _GoodsDeliveryAuthorization.GoodsDeliveryAuthorizationId,
                            DocType = "GoodsDeliveryAuthorization",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsDeliveryAuthorization, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsDeliveryAuthorization, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _GoodsDeliveryAuthorization.UsuarioCreacion,
                            UsuarioModificacion = _GoodsDeliveryAuthorization.UsuarioModificacion,
                            UsuarioEjecucion = _GoodsDeliveryAuthorization.UsuarioModificacion,

                        });
                        if (_GoodsDeliveryAuthorization.CertificadosAsociados != null)
                        {
                            foreach (var item in _GoodsDeliveryAuthorization.CertificadosAsociados)
                            {
                                decimal? saldoPendienteautorizar = 0;

                                CertificadoDeposito cd = _context.CertificadoDeposito.Where(q => q.IdCD == item).Include(i => i._CertificadoLine).FirstOrDefault();
                                foreach (var linea in cd._CertificadoLine)
                                {
                                    saldoPendienteautorizar += linea.CantidadDisponibleAutorizar;

                                }
                                if (saldoPendienteautorizar <= 0)
                                {
                                    cd.PendienteAutorizar = false;
                                }

                            }

                        }

                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

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
                return await Task.Run(()=> BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return Ok(_GoodsDeliveryAuthorizationq);
        }


        /// <summary>
        /// Obtiene el Listado de recibnos de mercaderias segun el cliente y el servicio
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{clienteid}/{servicioid}/{sucursal}")]
        public async Task<IActionResult> AutorizacionesPendientes(int clienteid, int servicioid, int sucursal)
        {
            List<GoodsDeliveryAuthorization> Items = new List<GoodsDeliveryAuthorization>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();

                UserBranch branch = _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id && w.BranchId == sucursal).FirstOrDefault();
                if (branch != null)
                {
                    Items = await _context.GoodsDeliveryAuthorization
                        .Include(g => g.GoodsDeliveryAuthorizationLine)
                      .Where(p =>
                           p.CustomerId == clienteid
                          && p.ProductId == servicioid
                          && p.GoodsDeliveryAuthorizationLine.Sum(s => s.Saldo)>0
                          )
                      .OrderByDescending(b => b.GoodsDeliveryAuthorizationId).ToListAsync();

                    return await Task.Run(() => Ok(Items));
                   
                }
                else
                {
                    return await Task.Run(() => Ok(Items));
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }

        /// <summary>
        /// Actualiza la GoodsDeliveryAuthorization
        /// </summary>
        /// <param name="_GoodsDeliveryAuthorization"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<GoodsDeliveryAuthorization>> Update([FromBody]GoodsDeliveryAuthorization _GoodsDeliveryAuthorization)
        {
            GoodsDeliveryAuthorization _GoodsDeliveryAuthorizationq = _GoodsDeliveryAuthorization;
            try
            {
                _GoodsDeliveryAuthorizationq = await (from c in _context.GoodsDeliveryAuthorization
                                 .Where(q => q.GoodsDeliveryAuthorizationId == _GoodsDeliveryAuthorization.GoodsDeliveryAuthorizationId)
                                                      select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_GoodsDeliveryAuthorizationq).CurrentValues.SetValues((_GoodsDeliveryAuthorization));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                //_context.GoodsDeliveryAuthorization.Update(_GoodsDeliveryAuthorizationq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveryAuthorizationq));
        }

        /// <summary>
        /// Elimina una GoodsDeliveryAuthorization       
        /// </summary>
        /// <param name="_GoodsDeliveryAuthorization"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]GoodsDeliveryAuthorization _GoodsDeliveryAuthorization)
        {
            GoodsDeliveryAuthorization _GoodsDeliveryAuthorizationq = new GoodsDeliveryAuthorization();
            try
            {
                _GoodsDeliveryAuthorizationq = _context.GoodsDeliveryAuthorization
                .Where(x => x.GoodsDeliveryAuthorizationId == (Int64)_GoodsDeliveryAuthorization.GoodsDeliveryAuthorizationId)
                .FirstOrDefault();

                _context.GoodsDeliveryAuthorization.Remove(_GoodsDeliveryAuthorizationq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveryAuthorizationq));

        }







    }
}