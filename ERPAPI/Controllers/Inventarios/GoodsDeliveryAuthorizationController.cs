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
        public async Task<ActionResult<GoodsDeliveryAuthorization>> Aprobar(int IdCD)
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
            autorizacion.Estado = "Aprobado";
            autorizacion.UsuarioModificacion = User.Identity.Name;
            autorizacion.FechaModificacion = DateTime.Now;

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

            if (_context.EndososCertificados.Where(q => certificados.Any(a =>a == q.IdCD) && q.FechaCancelacion!=null).Count()>0)
            {
                return BadRequest("El Certificado se encuentra endosado, no se puede emitir autorización");
            }
            
            try
            {
                pendientes = await (from cd in _context.CertificadoLine
                                           where
                                           //lineasrecibo.GoodsReceived.CustomerId == customerid && 
                                           //lineasrecibo.GoodsReceived.ProductId == servicio &&
                                           certificados.Any(q => q == cd.IdCD)  
                                           && cd.CantidadDisponibleAutorizar !=0
                                           //  && !_context.CertificadoLine.Any(a => a.CertificadoLineId == lineasrecibo.GoodsReceiveLinedId)
                                           select new GoodsDeliveryAuthorizationLine()
                                           {
                                               GoodsDeliveryAuthorizationId = 0,
                                               UnitOfMeasureName = cd.UnitMeasurName,
                                               UnitOfMeasureId = (long)cd.UnitMeasureId,
                                               Quantity = cd.CantidadDisponibleAutorizar==null? cd.Quantity : (decimal)cd.CantidadDisponibleAutorizar,
                                               SubProductId = (long)cd.SubProductId,
                                               SubProductName = cd.SubProductName,
                                               CertificadoLineId = cd.CertificadoLineId,
                                               NoCertificadoDeposito = (int)cd.IdCD,
                                               Price = (long)cd.Price,
                                               WarehouseId = (int)cd.WarehouseId,
                                               WarehouseName = cd.WarehouseName,
                                               valorcertificado = cd.Amount,
                                               SaldoProducto = cd.CantidadDisponibleAutorizar == null ? cd.Quantity : (decimal)cd.CantidadDisponibleAutorizar,
                                               ValorUnitarioDerechos = cd.ValorUnitarioDerechos,
                                               ValorImpuestos = 0,
                                               DerechosFiscales = cd.DerechosFiscales,
                                               Pda = cd.PdaNo,
                                           }).ToListAsync();

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
                    //.Include(q=>q.GoodsDeliveryAuthorizationLine)
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
                                              //.Where(q => q.GoodsDeliveryAuthorizationId > 0)
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
                        _GoodsDeliveryAuthorizationq = _GoodsDeliveryAuthorization;
                        _GoodsDeliveryAuthorizationq.Estado = "Revisión";
                        _GoodsDeliveryAuthorizationq.EstadoId = 5;

                        
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


                        foreach (var item in _GoodsDeliveryAuthorizationq.GoodsDeliveryAuthorizationLine)
                        {
                            item.GoodsDeliveryAuthorizationId = _GoodsDeliveryAuthorizationq.GoodsDeliveryAuthorizationId;
                            _context.GoodsDeliveryAuthorizationLine.Add(item);

                            CertificadoLine certificadoLine = _context.CertificadoLine
                             .Where(q => q.CertificadoLineId == item.CertificadoLineId).FirstOrDefault();

                            CertificadoDeposito certificadoDeposito = await _context.CertificadoDeposito
                                //.Include(i => i._CertificadoLine)
                                .Where(q => q.IdCD == item.NoCertificadoDeposito).FirstOrDefaultAsync();

                            List<Kardex> kardexCertificadodeposito = await _context.Kardex.Where(q =>
                                    q.DocType == 2
                                    && q.DocumentId == certificadoDeposito.IdCD 
                                    && q.SubProducId == item.SubProductId
                                    ).ToListAsync();
                            Kardex kardexMaxCertificadodeposito =
                                kardexCertificadodeposito
                                .Where(q => q.KardexId == kardexCertificadodeposito
                                .Select(s => s.KardexId).Max()).FirstOrDefault();

                            

                            

                            if (kardexMaxCertificadodeposito == null) { 
                                BadRequest($"No se Encontro Kardex Para este Producto en el Certificado"); 
                            }

                          

                            

                            SubProduct _subproduct = await (from c in _context.SubProduct
                                                     .Where(q => q.SubproductId == item.SubProductId)
                                                            select c
                                                     ).FirstOrDefaultAsync();

                            //if(kardexMaxCertificadodeposito.TotalCD< item.Quantity)
                            //{
                            //    return await Task.Run(() => BadRequest($"La cantidad a retirar no puede ser superior al total del ciertificado"));
                            //}
                            if (item.SaldoProducto < item.Quantity)
                            {
                                return await Task.Run(() => BadRequest($"La cantidad a retirar no puede ser superior al total del ciertificado"));
                            }
                            ////////Kardex Autorizacion/////////////
                            _context.Kardex.Add(new Kardex
                            {
                                KardexDate = DateTime.Now,
                                DocumentDate = _GoodsDeliveryAuthorization.DocumentDate,
                                ProducId = _GoodsDeliveryAuthorizationq.ProductId,
                                ProductName = _GoodsDeliveryAuthorizationq.ProductName,
                                SubProducId = Convert.ToInt32(item.SubProductId),
                                SubProductName = item.SubProductName,
                                QuantityEntry = item.Quantity,
                                QuantityOut = 0,
                                QuantityEntryBags = 0,
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
                                Total = item.Quantity,
                            });

                            ///////////////////////Sale del Certificado////////////////
                            _context.Kardex.Add(new Kardex
                            {
                                KardexDate = DateTime.Now,
                                DocumentDate = kardexMaxCertificadodeposito.DocumentDate,
                                ProducId = kardexMaxCertificadodeposito.ProducId,
                                ProductName = kardexMaxCertificadodeposito.ProductName,
                                SubProducId = Convert.ToInt32(kardexMaxCertificadodeposito.SubProducId),
                                SubProductName = item.SubProductName,
                                QuantityEntry = 0,
                                QuantityOut = item.Quantity,
                                QuantityEntryBags = 0,
                                BranchId = kardexMaxCertificadodeposito.BranchId,
                                BranchName = kardexMaxCertificadodeposito.BranchName,
                                WareHouseId = Convert.ToInt32(kardexMaxCertificadodeposito.WareHouseId),
                                WareHouseName = kardexMaxCertificadodeposito.WareHouseName,
                                UnitOfMeasureId = kardexMaxCertificadodeposito.UnitOfMeasureId,
                                UnitOfMeasureName = kardexMaxCertificadodeposito.UnitOfMeasureName,
                                TypeOperationId = TipoOperacion.Salida,
                                TypeOperationName = "Salida",
                                DocumentName = "Certificado Deposito/Autorizacion Retiro",
                                DocType = 2,
                                Total = kardexMaxCertificadodeposito.Total - item.valorcertificado,
                            }) ;

                            certificadoLine.CantidadDisponibleAutorizar = certificadoLine.CantidadDisponibleAutorizar == null ?certificadoLine.Quantity - item.Quantity
                                :certificadoLine.CantidadDisponibleAutorizar -  item.Quantity;
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

                        await _context.SaveChangesAsync();

                       

                        foreach (var item in _GoodsDeliveryAuthorization.CertificadosAsociados)
                        {
                            decimal? saldoPendienteautorizar = 0;

                            CertificadoDeposito cd = _context.CertificadoDeposito.Where(q => q.IdCD == item).Include(i => i._CertificadoLine).FirstOrDefault();
                            foreach (var linea in cd._CertificadoLine)
                            {
                                saldoPendienteautorizar += linea.CantidadDisponibleAutorizar;
                                
                            }
                            if (saldoPendienteautorizar<=0)
                            {
                                cd.PendienteAutorizar = false;
                            }

                        }

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
                      .Where(p =>
                           p.CustomerId == clienteid
                          && p.ProductId == servicioid
                          //&& (p.Porcertificar == null || (bool)p.Porcertificar)
                          )
                      .OrderByDescending(b => b.GoodsDeliveryAuthorizationId).ToListAsync();

                    return await Task.Run(() => Ok(Items));
                   
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